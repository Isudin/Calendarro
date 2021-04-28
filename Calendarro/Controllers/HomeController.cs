using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Calendarro.Models;
using Microsoft.AspNetCore.Authorization;
using Calendarro.Models.Database;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Calendarro.Areas.Identity.Data;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Calendarro.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly CalendarroDBContext _context;
        private readonly UserManager<CalendarroUser> _userManager;
        private Projects _currentProject;

        public HomeController(CalendarroDBContext context, UserManager<CalendarroUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            SaveUserToSession();
        }

        private void SaveUserToSession()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var dbUser = _context.CalendarroUsers.SingleAsync(user => user.Token == user.UserId.ToString());
            var serializedUser = JsonConvert.SerializeObject(dbUser.Result);
            HttpContext.Session.SetString("User", serializedUser);
            SaveProjectToSession(dbUser.Result);
        }

        private void SaveProjectToSession(CalendarroUsers dbUser)
        {
            //Najprawdopodobniej tylko testowe dodawanie projektu do sesji
            var projectUserRel = _context.ProjectUserRelation.FirstAsync(rel => rel.User == dbUser);
            var project = _context.Projects.AllAsync(project => project.ProjectId == projectUserRel.Result.ProjectId);
            var serializedProject = JsonConvert.SerializeObject(project.Result);
            HttpContext.Session.SetString("Project", serializedProject);
        }

        public IActionResult Index()
        {
            var kanbans = PrepareCanbans();


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> AddUserToProjectAsync(int userId)
        {
            var relation = new ProjectUserRelation()
            {
                UserId = userId,
                ProjectId = HttpContext.Session.GetInt32("ProjectId").Value
            };
            _context.ProjectUserRelation.Add(relation);
            await _context.SaveChangesAsync();
            return View();
        }

        public async Task<IActionResult> AddProjectAsync(string name, string description, DateTime finishingDate)
        {
            var serializedUser = HttpContext.Session.GetString("User");
            var user = (CalendarroUsers)JsonConvert.DeserializeObject(serializedUser);
            var project = new Projects()
            {
                CreateDate = DateTime.Now,
                Description = description,
                ProjectName = name,
                FinishingDate = finishingDate,
                CreatorId = user.UserId
            };
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return View();
        }

        public IActionResult AddProjectAsync()
        {
            var serializedUser = HttpContext.Session.GetString("User");
            var user = (CalendarroUsers)JsonConvert.DeserializeObject(serializedUser);
            var project = new Projects()
            {
                CreateDate = DateTime.Now,
                ProjectName = "Test name",
                CreatorId = user.UserId
            };
            _context.Projects.Add(project);
            _context.SaveChanges();
            return View();
        }

        public async Task<IActionResult> AddKanbanAsync(string name)
        {
            var kanban = new Kanbans()
            {
                Name = name,
                ProjectId = HttpContext.Session.GetInt32("ProjectId").Value
            };
            _context.Kanbans.Add(kanban);
            await _context.SaveChangesAsync();
            return View();
        }

        public async Task<IActionResult> AddTaskAsync(int userId, string name, DateTime finishDate)
        {
            var task = new ProjectTasks()
            {
                CreateDate = DateTime.Now,
                TaskName = name,
                FinishDate = finishDate,
                UserId = userId,
                ProjectId = HttpContext.Session.GetInt32("KanbanId").Value
            };
            _context.ProjectTasks.Add(task);
            await _context.SaveChangesAsync();
            return View();
        }

        public List<Kanbans> PrepareCanbans()
        {
            var kanbansList = new List<Kanbans>();
            foreach (var kanban in _context.Kanbans)
                if (kanban.ProjectId == _currentProject.ProjectId)
                    kanbansList.Add(kanban);
            return kanbansList;
        }

        public Projects GetCurrentProject()
        {
            return (Projects)JsonConvert.DeserializeObject(HttpContext.Session.GetString("Project"));
        }
    }
}
