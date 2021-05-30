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

        [HttpGet]
        public IActionResult AddNewProject()
        {
            return View(new NewProjectViewModel());
        }

        [HttpPost]
        [ActionName("AddNewProject")]
        public IActionResult AddNewProjectPost(NewProjectViewModel projectModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(AddNewProject), projectModel);
            }

            HttpContext.Session.TryGetValue("User", out var creator);

            if (creator == null)
            {
                // blad bo nie ma uzytkownika w sesji
            }

            var options = new JsonSerializerOptions { WriteIndented = true };

            var user = JsonSerializer.Deserialize<UserDto>(creator, options);

            //var model = new Projects
            //{
            //    CreateDate = DateTime.Now,
            //    Creator
            //}

            return View();
        }
    }
}
