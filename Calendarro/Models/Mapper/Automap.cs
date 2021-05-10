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
            CreateMap<CalendarroUsers, UserDto>();
            CreateMap<Kanbans, KanbanDto>();
            CreateMap<ProjectTasks, TaskDto>()
                .ForMember(d => d.TaskId, source => source.MapFrom(s => s.ProjectTaskId));
            CreateMap<ProjectUserRelation, RelationDto>()
                .ForMember(d=> d.RelationId, source => source.MapFrom(s=>s.LinkId));
        }
    }
}
