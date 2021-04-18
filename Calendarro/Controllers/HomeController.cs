using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Calendarro.Models;
using Microsoft.AspNetCore.Authorization;
using Calendarro.Models.ContentBase;

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
            ///Save data to db
            var relation = new ProjectUserRelation()
            {
                UserId = userId,
                
            };
            return View();
        }

        public async Task<IActionResult> AddProjectAsync(string name, string description, DateTime finishingDate)
        {
            ///TODO
            ///Add creatorID, 
            ///Save data to db
            var project = new Project()
            {
                CreateDate = DateTime.Now,
                Description = description,
                ProjectName = name,
                FinishingDate = finishingDate,
                
            };
            return View();
        }

        public async Task<IActionResult> AddKanbanAsync(string name)
        {
            ///TODO
            ///Add projectID, 
            ///Save data to db
            var kanban = new Kanban()
            {
                Name = name,
                
            };
            return View();
        }

        public async Task<IActionResult> AddTaskAsync(int userId, string name, DateTime finishDate)
        {
            ///TODO
            ///Add kanbanID, 
            ///Save data to db
            var task = new ProjectTask()
            {
                CreateDate = DateTime.Now,
                TaskName = name,
                FinishDate = finishDate,
                UserId = userId,
                
            };
            return View();
        }
    }
}
