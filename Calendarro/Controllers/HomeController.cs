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

namespace Calendarro.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly CalendarroDBContext _context;

        //public HomeController(ILogger<HomeController> logger, CalendarroDBContext context)
        //{
        //    _logger = logger;
        //    _context = context;
        //}

        public IActionResult Index()
        {
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
            ///TODO
            ///Add projectID
            var relation = new ProjectUserRelation()
            {
                UserId = userId,

            };
            _context.ProjectUserRelation.Add(relation);
            await _context.SaveChangesAsync();
            return View();
        }

        public async Task<IActionResult> AddProjectAsync(string name, string description, DateTime finishingDate)
        {
            ///TODO
            ///Add creatorID
            var project = new Projects()
            {
                CreateDate = DateTime.Now,
                Description = description,
                ProjectName = name,
                FinishingDate = finishingDate,

            };
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return View();
        }

        public async Task<IActionResult> AddKanbanAsync(string name)
        {
            ///TODO
            ///Add projectID
            var kanban = new Kanbans()
            {
                Name = name,
            };
            _context.Kanbans.Add(kanban);
            await _context.SaveChangesAsync();
            return View();
        }

        public async Task<IActionResult> AddTaskAsync(int userId, string name, DateTime finishDate)
        {
            ///TODO
            ///Add kanbanID
            var task = new ProjectTasks()
            {
                CreateDate = DateTime.Now,
                TaskName = name,
                FinishDate = finishDate,
                UserId = userId,

            };
            _context.ProjectTasks.Add(task);
            await _context.SaveChangesAsync();
            return View();
        }
    }
}
