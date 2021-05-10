using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendarro.Dto
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int ProjectId { get; set; }
        public int KanbanId { get; set; }
        public int UserId { get; set; }
    }
}
