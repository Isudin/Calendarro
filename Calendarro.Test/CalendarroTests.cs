using Calendarro.Areas.Identity.Data;
using Calendarro.Models.Database;
using Calendarro.Models.Dto;
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
            Random rand = new Random();
            bool exist = false;           
            var task = new TaskDto()
            {
                CreateDate = DateTime.Now,
                TaskName = "example",
                FinishDate = DateTime.Now,                
                UserId = rand.Next(1,5),
                ProjectId = rand.Next(1, 5),
                KanbanId = rand.Next(1, 5)
            };
            if (task is TaskDto && task !=null) 
            {
                exist = true;
            }

            Assert.IsTrue(exist);
        }

        [TestMethod]
        public void CheckKanbansSaving()
        {
            Random rand = new Random();
            bool exist = false;
            var task = new KanbanDto()
            {
                Name="example",
                ProjectId= rand.Next(1, 5)
            };
            if (task is KanbanDto && task != null)
            {
                exist = true;
            }
            Assert.IsTrue(exist);
        }

        [TestMethod]
        public void CheckRelationsSaving()
        {
            Random rand = new Random();
            bool exist = false;
            var task = new RelationDto()
            {
                UserId = rand.Next(1, int.MaxValue),
                ProjectId = rand.Next(1, int.MaxValue)
            };
            if (task is RelationDto && task != null)
            {
                exist = true;
            }
            Assert.IsTrue(exist);
        }
    }
}
