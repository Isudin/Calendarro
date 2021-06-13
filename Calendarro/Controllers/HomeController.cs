using AutoMapper;
using Calendarro.Areas.Identity.Data;
using Calendarro.Models;
using Calendarro.Models.Database;
using Calendarro.Models.Dto;
using Calendarro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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


        public IActionResult Index(int? projectId = null)
        {
            SaveUserToSession(projectId);
            _projectsList = GetProjectsList();
            var kanbans = PrepareKanbansWithTasks();


            HttpContext.Session.TryGetValue("Project", out var project);

            if (project == null)
            {
                return RedirectToAction(nameof(NoProjectFound));
            }


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

            ViewBag.ProjectIdForGenerateTasks = proj.ProjectId;

            return View(model);
        }

        public IActionResult NoProjectFound()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewTaskAsync(AddNewTaskViewModel addNewTaskViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _context.CalendarroUsers.FirstOrDefault(x => x.EMail.Equals(User.Identity.Name)).UserId;

                HttpContext.Session.TryGetValue("Project", out var project);

                if (project == null)
                {
                    return RedirectToAction("Index", "Home");
                }

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

                return RedirectToAction(nameof(Index), new { projectId = proj.ProjectId });
            }

            return RedirectToAction(nameof(Index));
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

        private void SaveUserToSession(int? projectId = null)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var dbUser = _context.CalendarroUsers.Single(u => u.Token == user.Id);
            var mappedUser = _mapper.Map<UserDto>(dbUser);
            var serializedUser = JsonConvert.SerializeObject(mappedUser);
            HttpContext.Session.SetString("User", serializedUser);
            SaveProjectToSession(dbUser, projectId);
        }

        private void SaveProjectToSession(CalendarroUsers dbUser, int? projectId = null)
        {
            //Najprawdopodobniej tylko testowe dodawanie projektu do sesji
            var projectUserRel = _context.ProjectUserRelation.FirstOrDefault(rel => rel.User == dbUser);

            if (projectUserRel == null)
            {
                return;
            }

            Projects project;

            if (projectId != null)
            {
                project = _context.Projects
                    .Where(p => p.ProjectId == projectId)
                    .FirstOrDefault();
            }
            else
            {
                project = _context.Projects.FirstOrDefault(project => project.ProjectId == projectUserRel.ProjectId);
            }

            var allRelations = _context.ProjectUserRelation
                .Where(x => x.User == dbUser)
                .Select(a => a.ProjectId)
                .ToList();

            if (project == null)
            {
                return;
            }

            if (allRelations.Contains(project.ProjectId))
            {
                var mappedProject = _currentProject = _mapper.Map<ProjectDto>(project);
                var serializedProject = JsonConvert.SerializeObject(mappedProject);
                HttpContext.Session.SetString("Project", serializedProject);
            }
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
            HttpContext.Session.TryGetValue("Project", out var project);

            if (project == null)
            {
                return BadRequest();
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            var proj = System.Text.Json.JsonSerializer.Deserialize<ProjectDto>(project, options);

            var kanban = new KanbanDto()
            {
                Name = name,
                ProjectId = proj.ProjectId
            };

            var dbKanban = _mapper.Map<Kanbans>(kanban);

            _context.Kanbans.Add(dbKanban);
            await _context.SaveChangesAsync();

            int.TryParse(HttpContext.Request.Query["projectId"].ToString(), out var projid);

            return RedirectToAction(nameof(Index), new { projectId = projid });
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

        public List<KanbanWithTasksViewModel> PrepareKanbansWithTasks()
        {
            var kanbansWithTasksList = new List<KanbanWithTasksViewModel>();
            var kanbans = GetKanbans();

            if (kanbans == null)
            {
                return null;
            }

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
            if (_currentProject?.ProjectId == null)
            {
                return null;
            }

            var kanbans = _context.Kanbans.Where(k => k.ProjectId == _currentProject.ProjectId).ToList();

            return _mapper.Map<List<KanbanDto>>(kanbans);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTaskFromKanbanAsync(int taskId)
        {
            var task = _context.ProjectTasks.Where(x => x.ProjectTaskId == taskId).FirstOrDefault();

            _context.ProjectTasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveKanbanAsync(int kanbanId)
        {
            var kanban = _context.Kanbans.Where(x => x.KanbanId == kanbanId).FirstOrDefault();
            var tasks = _context.ProjectTasks.Where(x => x.KanbanId == kanbanId).ToList();

            foreach (var task in tasks)
                _context.ProjectTasks.Remove(task);

            _context.Kanbans.Remove(kanban);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
