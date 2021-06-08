using AutoMapper;
using Calendarro.Models.Database;
using Calendarro.Models.Dto;
using Calendarro.Models.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Calendarro.Test
{
    [TestClass]
    public class Mapper_MapsCorrectly_ReturnTrue
    {
        private readonly IMapper _mapper;

        public Mapper_MapsCorrectly_ReturnTrue()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Automap());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [TestMethod]
        public void TestAutoMapperProfiles()
        {
            //Arrange
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Automap());
            });

            //Act

            //Assert
            mappingConfig.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void MapKanbansToKanbanDto()
        {
            //Arrange
            var kanbans = new Mock<Kanbans>();

            //Act
            var kanbanDto = _mapper.Map<KanbanDto>(kanbans);
            var mappedKanbans = _mapper.Map<Kanbans>(kanbanDto);

            //Assert
            Assert.AreEqual(kanbans, mappedKanbans);
        }

        [TestMethod]
        public void MapProjectsToProjectDto()
        {
            //Arrange
            var projects = new Mock<Projects>();

            //Act
            var projectDto = _mapper.Map<ProjectDto>(projects);
            var mappedProjects = _mapper.Map<Projects>(projectDto);

            //Assert
            Assert.AreEqual(projects, mappedProjects);
        }

        [TestMethod]
        public void MapProjectUserRelationToRelationDto()
        {
            //Arrange
            var projectUserRelation = new Mock<ProjectUserRelation>();

            //Act
            var relationDto = _mapper.Map<RelationDto>(projectUserRelation);
            var mappedProjectUserRelation = _mapper.Map<ProjectUserRelation>(relationDto);

            //Assert
            Assert.AreEqual(projectUserRelation, mappedProjectUserRelation);
        }

        [TestMethod]
        public void MapProjectTasksToTaskDto()
        {
            //Arrange
            var projectTasks = new Mock<ProjectTasks>();

            //Act
            var taskDto = _mapper.Map<TaskDto>(projectTasks);
            var mappedProjectTasks = _mapper.Map<ProjectTasks>(taskDto);

            //Assert
            Assert.AreEqual(projectTasks, mappedProjectTasks);
        }

        [TestMethod]
        public void MapCalendarroUsersToUsersDto()
        {
            //Arrange
            var calendarroUsers = new Mock<CalendarroUsers>();

            //Act
            var userDto = _mapper.Map<UserDto>(calendarroUsers);
            var mappedCalendarroUsers = _mapper.Map<CalendarroUsers>(userDto);

            //Assert
            Assert.AreEqual(calendarroUsers, mappedCalendarroUsers);
        }
    }
}
