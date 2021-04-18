using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendarro.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Calendarro.Controllers
{
    public class LayoutController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CalendarroDBContext _context;

        public LayoutController(ILogger<HomeController> logger, CalendarroDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var projectsList = new List<Projects>();
            foreach (var relation in _context.ProjectUserRelation)
                if (relation.UserId == HttpContext.Session.GetInt32("CreatorId").Value)
                    projectsList.Add(_context.Projects.Where(x => x.ProjectId == relation.ProjectId).First());
            return View();
        }
    }
}
