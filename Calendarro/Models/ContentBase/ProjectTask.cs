using System;
using System.Collections.Generic;

#nullable disable

namespace Calendarro.Models.ContentBase
{
    public partial class ProjectTask
    {
        public int ProjectTaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int ProjectId { get; set; }
        public int KanbanId { get; set; }
        public int UserId { get; set; }

        public virtual Kanban Kanban { get; set; }
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
