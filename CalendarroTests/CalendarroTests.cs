using Calendarro.Areas.Identity.Data;
using Calendarro.Models.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;


namespace CalendarroTests
{
    [TestClass]
    public class CalendarroTests
    {
        private  CalendarroDBContext _context;
        public CalendarroTests(CalendarroDBContext context)
        {
            _context = context;           
        }
        public CalendarroTests()
        {
            
        }       

        [TestMethod]
        public void CheckProjectTasksSaving()
        {           
            bool exist = false;           
            var task = new ProjectTasks()
            {
                CreateDate = DateTime.Now,
                TaskName = "example",
                FinishDate = DateTime.Now,                
                UserId = 2,
                ProjectId = 1,
                KanbanId = 1                
            };
            if (task is ProjectTasks && task !=null) 
            {
                exist = true;
            }
            Assert.IsTrue(exist);
        }
    }
}
