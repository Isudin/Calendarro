using AutoMapper;
using Calendarro.Models.Database;
using Calendarro.Models.Dto;

namespace Calendarro.Models.Mapper
{
    public class Automap : Profile
    {
        public Automap()
        {
            CreateMap<Projects, ProjectDto>();
            CreateMap<ProjectDto, Projects>();

            CreateMap<CalendarroUsers, UserDto>();
            CreateMap<UserDto, CalendarroUsers>();

            CreateMap<Kanbans, KanbanDto>();
            CreateMap<KanbanDto, Kanbans>();

            CreateMap<ProjectTasks, TaskDto>()
                .ForMember(d => d.TaskId, source => source.MapFrom(s => s.ProjectTaskId));
            CreateMap<TaskDto, ProjectTasks>()
                .ForMember(d => d.ProjectTaskId, source => source.MapFrom(s => s.TaskId));

            CreateMap<ProjectUserRelation, RelationDto>()
                .ForMember(d=> d.RelationId, source => source.MapFrom(s=>s.LinkId));
            CreateMap<RelationDto, ProjectUserRelation>()
                .ForMember(d=> d.LinkId, source => source.MapFrom(s=>s.RelationId));
        }
    }
}
