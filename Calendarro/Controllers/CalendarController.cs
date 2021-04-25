using Calendarro.Areas.Identity.Data;
using Calendarro.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private UserManager<CalendarroUser> _userManager;

        public CalendarController(CalendarroDBContext context, UserManager<CalendarroUser> userManager)
        {
            _context = context;

            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var test = await _userManager.GetUserAsync(User);

            return View();
        }

        public IActionResult GetAllTasks(int project)
        {
            var tasks = _context.ProjectTasks
                .Where(x => x.ProjectId == project)
                .Select(e => new
                {
                    id = e.ProjectTaskId,
                    title = e.TaskName,
                    start = e.FinishDate.Value.ToString("yyyy-MM-dd")
                    //end = e.FinishDate.Value.ToString("MM-dd-yyyy")
                })
                .ToList();

            return new JsonResult(tasks);
        }
    }
}
