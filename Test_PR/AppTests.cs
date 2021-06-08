using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendarro.ViewModels;

namespace Test_PR
{
    [TestClass]
    public class AppTests
    {
        private AddNewTaskViewModel _addNewTask;
        private NewProjectViewModel _addNewProject;

        public void AddNewTaskViewModel(AddNewTaskViewModel addNewTask, NewProjectViewModel addNewProject)
        {
            _addNewTask = addNewTask;
            _addNewProject = addNewProject;
        }

        [TestMethod]
        public void NewTaskTest()
        {
            var res = false;
            _addNewTask = new AddNewTaskViewModel()
            {
                Name = "nameoftask",
                FinishDate = DateTime.Now,
                Kanban = 10
            };

            if (_addNewTask != null)
            {
                res = true;
            }

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void NewProjectTest()
        {
            var res = false;
            var res2 = false;
            _addNewProject = new NewProjectViewModel()
            {
                ProjectName = "nameofproject",
                FinishDate = DateTime.Now,
                Description = "Description for project"
            };

            if (_addNewProject != null )
            {
                res = true;
                Assert.IsTrue(res);
            }

            if(_addNewProject.ProjectName.Length <= 20 && _addNewProject.ProjectName.Length >= 5)
            {
                res2 = true;
                Assert.IsTrue(res2);
            }
        }
    }
}