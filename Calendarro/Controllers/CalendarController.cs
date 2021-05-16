using Calendarro.Areas.Identity.Data;
using Calendarro.Models.Database;
using Calendarro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Calendarro.Controllers
{
    public class CalendarController : Controller
    {
        private readonly CalendarroDBContext _context;
        private readonly UserManager<CalendarroUser> _userManager;

        public CalendarController(CalendarroDBContext context, UserManager<CalendarroUser> userManager)
        {
            _context = context;

            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTaskAsync(AddNewTaskViewModel addNewTaskViewModel)
        {
            if (ModelState.IsValid)
            {
                var task = new ProjectTasks()
                {
                    CreateDate = DateTime.Now,
                    TaskName = addNewTaskViewModel.Name,
                    FinishDate = addNewTaskViewModel.FinishDate.DateTime,
                    // do zmiany
                    UserId = 2,
                    ProjectId = 1,
                    KanbanId = addNewTaskViewModel.Kanban
                    //ProjectId = HttpContext.Session.GetInt32("KanbanId").Value
                };

                _context.ProjectTasks.Add(task);
                await _context.SaveChangesAsync();

                return View(nameof(Index));
            }
            return View(nameof(Index), addNewTaskViewModel);
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
