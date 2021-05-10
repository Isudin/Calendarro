using Calendarro.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendarro.ViewModels
{
    public class KanbanWithTasksViewModel
    {
        public KanbanDto Kanban { get; set; }
        public IEnumerable<TaskDto> Tasks { get; set; }
    }
}
