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

namespace Calendarro.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CalendarroDBContext _context;
        private UserManager<CalendarroUser> _userManager;

        public HomeController(ILogger<HomeController> logger, CalendarroDBContext context, UserManager<CalendarroUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            SaveUserToSession(userManager);
        }

        private async Task SaveUserToSession(UserManager<CalendarroUser> userManager)
        {
            var user = await userManager.GetUserAsync(User);
            var serializedUser = JsonConvert.SerializeObject(user);
            HttpContext.Session.SetString("User", serializedUser);
        }

        public IActionResult Index()
        {
            //var kanbans = PrepareCanbans();
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
            var project = new Projects()
            {
                CreateDate = DateTime.Now,
                Description = description,
                ProjectName = name,
                FinishingDate = finishingDate,
                //CreatorId = _userManager.GetUserIdAsync().Result,
                //CreatorId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                //CreatorId = _userManager.GetUserAsync(User).Result.Id
                CreatorId = HttpContext.Session.GetInt32("CreatorId").Value

            };
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
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
            var currentProject = (Projects)JsonConvert.DeserializeObject(HttpContext.Session.GetString("Project"));
                    foreach (var kanban in _context.Kanbans)
                        if (kanban.ProjectId == currentProject.ProjectId)
                            kanbansList.Add(kanban);
            return kanbansList;
        }
    }
}
