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
using AutoMapper;
using Calendarro.Models.Dto;
using Calendarro.ViewModels;
using System.Text.Json;

namespace Calendarro.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly CalendarroDBContext _context;
        private readonly UserManager<CalendarroUser> _userManager;
        private readonly IMapper _mapper;
        public static ProjectDto _currentProject;
        public static List<ProjectDto> _projectsList;

        public HomeController(CalendarroDBContext context, UserManager<CalendarroUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            SaveUserToSession();
            _projectsList = GetProjectsList();
            var kanbans = PrepareCanbansWithTasks();

            //tutaj zmiana Natan


            HttpContext.Session.TryGetValue("Project", out var project);

            var options = new JsonSerializerOptions { WriteIndented = true };
            var proj = System.Text.Json.JsonSerializer.Deserialize<ProjectDto>(project, options);
            var kanbanList = _context.Kanbans.Where(x => x.Project.ProjectId == proj.ProjectId).ToList();

            var model = new MainViewModel
            {
                KanbanWithTasks = kanbans,
                AddNewTaskViewModel = new AddNewTaskViewModel()
                {
                    KanbanList = kanbanList
                }
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewTaskAsync(AddNewTaskViewModel addNewTaskViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _context.CalendarroUsers.FirstOrDefault(x => x.EMail.Equals(User.Identity.Name)).UserId;

                HttpContext.Session.TryGetValue("Project", out var project);

                var options = new JsonSerializerOptions { WriteIndented = true };
                var proj = System.Text.Json.JsonSerializer.Deserialize<ProjectDto>(project, options);

                var projectId = proj.ProjectId;

                var task = new ProjectTasks()
                {
                    CreateDate = DateTime.Now,
                    TaskName = addNewTaskViewModel.Name,
                    FinishDate = addNewTaskViewModel.FinishDate.DateTime,
                    UserId = userId,
                    ProjectId = projectId,
                    KanbanId = addNewTaskViewModel.Kanban
                };

                _context.ProjectTasks.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
            //return View(nameof(Index), addNewTaskViewModel);
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

        private void SaveUserToSession()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var dbUser = _context.CalendarroUsers.Single(u => u.Token == user.Id);
            var mappedUser = _mapper.Map<UserDto>(dbUser);
            var serializedUser = JsonConvert.SerializeObject(mappedUser);
            HttpContext.Session.SetString("User", serializedUser);
            SaveProjectToSession(dbUser);
        }

        private void SaveProjectToSession(CalendarroUsers dbUser)
        {
            //Najprawdopodobniej tylko testowe dodawanie projektu do sesji
            var projectUserRel = _context.ProjectUserRelation.First(rel => rel.User == dbUser);
            var project = _context.Projects.First(project => project.ProjectId == projectUserRel.ProjectId);
            var mappedProject = _currentProject = _mapper.Map<ProjectDto>(project);
            var serializedProject = JsonConvert.SerializeObject(mappedProject);
            HttpContext.Session.SetString("Project", serializedProject);
        }

        public void GetCurrentProject()
        {
            _currentProject = (ProjectDto)JsonConvert.DeserializeObject(HttpContext.Session.GetString("Project"));
        }

        public UserDto GetCurrentUser()
        {
            return (UserDto)JsonConvert.DeserializeObject(HttpContext.Session.GetString("User"), typeof(UserDto));
        }

        public async Task<IActionResult> AddUserToProjectAsync(int userId)
        {
            var relation = new RelationDto()
            {
                UserId = userId,
                ProjectId = HttpContext.Session.GetInt32("ProjectId").Value
            };
            var mappedRelation = _mapper.Map<ProjectUserRelation>(relation);
            _context.ProjectUserRelation.Add(mappedRelation);
            await _context.SaveChangesAsync();

            return View();
        }

        public async Task<IActionResult> AddProjectAsync(string name, string description, DateTime finishingDate)
        {
            var project = new ProjectDto()
            {
                CreateDate = DateTime.Now,
                Description = description,
                ProjectName = name,
                FinishingDate = finishingDate,
                CreatorId = GetCurrentUser().UserId
            };
            var dbProject = _mapper.Map<Projects>(project);
            _context.Projects.Add(dbProject);
            await _context.SaveChangesAsync();

            return View();
        }

        public async Task<IActionResult> AddKanbanAsync(string name)
        {
            var kanban = new KanbanDto()
            {
                Name = name,
                ProjectId = HttpContext.Session.GetInt32("ProjectId").Value
            };
            var dbKanban = _mapper.Map<Kanbans>(kanban);
            _context.Kanbans.Add(dbKanban);
            await _context.SaveChangesAsync();

            return View();
        }

        public async Task<IActionResult> AddTaskAsync(int userId, string name, DateTime finishDate)
        {
            var task = new TaskDto()
            {
                CreateDate = DateTime.Now,
                TaskName = name,
                FinishDate = finishDate,
                UserId = userId,
                ProjectId = HttpContext.Session.GetInt32("KanbanId").Value
            };
            var dbTask = _mapper.Map<ProjectTasks>(task);
            _context.ProjectTasks.Add(dbTask);
            await _context.SaveChangesAsync();

            return View();
        }

        public List<KanbanWithTasksViewModel> PrepareCanbansWithTasks()
        {
            var kanbansWithTasksList = new List<KanbanWithTasksViewModel>();
            var kanbans = GetKanbans();

            foreach (var kanban in kanbans)
                kanbansWithTasksList.Add(new KanbanWithTasksViewModel()
                {
                    Kanban = kanban,
                    Tasks = GetKanbanTasks(kanban.KanbanId)
                });

            return kanbansWithTasksList;
        }

        public List<ProjectDto> GetProjectsList()
        {
            var projectUserRel = _context.ProjectUserRelation.Where(rel => rel.User.UserId == GetCurrentUser().UserId).Select(rel => rel.ProjectId).ToList();
            var projects = _context.Projects.Where(project => projectUserRel.Contains(project.ProjectId)).ToList();
            return _mapper.Map<List<ProjectDto>>(projects);
        }

        public IEnumerable<TaskDto> GetKanbanTasks(int kanbanId)
        {
            var tasks = _context.ProjectTasks.Where(task => task.KanbanId == kanbanId).ToList();
            return _mapper.Map<List<TaskDto>>(tasks);
        }

        public IEnumerable<KanbanDto> GetKanbans()
        {
            var kanbans = _context.Kanbans.Where(k => k.ProjectId == _currentProject.ProjectId).ToList();
            return _mapper.Map<List<KanbanDto>>(kanbans);
        }
    }
}
