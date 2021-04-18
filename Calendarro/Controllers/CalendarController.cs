using Calendarro.Models.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendarro.Controllers
{
    public class CalendarController : Controller
    {
        private readonly CalendarroDBContext _context;

        public CalendarController(CalendarroDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult GetAllTasks(int projectId)
        //{
        //    var tasks = _context.ProjectTasks
        //        .Where(x => x.ProjectId == projectId).ToList();

        //    return new JsonResult(tasks);
        //}
    }
}
