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
        private CalendarroDBContext _context;
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
                UserId = rand.Next(1, 5),
                ProjectId = rand.Next(1, 5),
                KanbanId = rand.Next(1, 5)
            };
            if (task is TaskDto && task != null)
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
                Name = "example",
                ProjectId = rand.Next(1, 5)
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

    [TestClass]
    public class CalendarroObjects
    {
        public KanbanDto Kanban {get;set;}
        public RelationDto Relation {get;set;}
        public TaskDto ProjectTask {get;set;}
    }


    [TestClass]
    public class CalendarroFactory
    {
        
        public CalendarroObjects CreateCalendarroObject(int ChooseWhichObject) 
        {
            CalendarroObjects CreatedObject = null;
            Random rand = new Random();
            switch (ChooseWhichObject)
            {
                case 1:
                   CreatedObject = new CalendarroObjects()
                   {
                        Kanban = new KanbanDto
                        {
                            Name = "example",
                            ProjectId = rand.Next(1, 5)
                        }
                   };
                   return CreatedObject;
                case 2:
                    CreatedObject = new CalendarroObjects()
                    {
                        Relation = new RelationDto()
                        {
                            UserId = rand.Next(1, int.MaxValue),
                            ProjectId = rand.Next(1, int.MaxValue)
                        }
                    };
                    return CreatedObject;
                case 3:
                    CreatedObject = new CalendarroObjects()
                    {
                        ProjectTask = new TaskDto()
                        {
                            CreateDate = DateTime.Now,
                            TaskName = "example",
                            FinishDate = DateTime.Now,
                            UserId = rand.Next(1, 5),
                            ProjectId = rand.Next(1, 5),
                            KanbanId = rand.Next(1, 5)
                        }
                    };
                    return CreatedObject;
                default:
                    return CreatedObject;
            }
            
        }
    }

    [TestClass]
    public class CheckFactory 
    {
        Random rand = new Random();
        [TestMethod]
        public void checkCalendarroFactory() 
        {
            bool check = true;
            CalendarroFactory factory = new CalendarroFactory();
            int LoopCount = rand.Next(1, 200);
            for (int i = 0; i < LoopCount; i++) 
            {
                CalendarroObjects objects = factory.CreateCalendarroObject(rand.Next(1, 4));

                if (objects == null)
                {
                    check = false;
                }
            }           

            Assert.IsTrue(check);
        }
    }
}
