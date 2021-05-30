using Calendarro.Models.Database;
using Calendarro.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Calendarro.Models.Dto;

namespace Calendarro.Controllers
{
    public class ProjectController : Controller
    {

        private readonly CalendarroDBContext _context;

        public ProjectController(CalendarroDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddNewProject()
        {
            return View();
        }

        [HttpPost]
        [ActionName("AddNewProject")]
        public IActionResult AddNewProjectPost(NewProjectViewModel projectModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.StatusMessage = "Model błędny!";
                return View(nameof(AddNewProject), projectModel);
            }

            HttpContext.Session.TryGetValue("User", out var creator);

            if (creator == null)
            {
                ViewBag.StatusMessage = "Błąd! Użytkownik nie został zapisany w sesji!";
                return View();
            }

            var options = new JsonSerializerOptions { WriteIndented = true };

            var user = JsonSerializer.Deserialize<UserDto>(creator, options);

            var dbUser = _context.CalendarroUsers.FirstOrDefault(t => t.UserId == user.UserId);

            if (dbUser == null)
            {
                ViewBag.StatusMessage = "Nie znaleziono użytkownika w bazie";
                return View();
            }

            var project = new Projects
            {
                CreateDate = DateTime.Now,
                Description = projectModel.Description,
                FinishingDate = projectModel.FinishDate,
                ProjectName = projectModel.ProjectName,
                Creator = _context.CalendarroUsers.Find(user.UserId)
            };

            _context.Projects.Add(project);
            _context.SaveChanges();

            var projectRelation = new ProjectUserRelation
            {
                User = dbUser,
                Project = project
            };

            _context.ProjectUserRelation.Add(projectRelation);
            _context.SaveChanges();

            ViewBag.StatusMessage = "Zaakceptowano";

            return RedirectToRoute("/");
        }
    }
}
