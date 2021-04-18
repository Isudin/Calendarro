using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Calendarro.Models.Database
{
    public partial class ProjectTasks
    {
        public int ProjectTaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int ProjectId { get; set; }
        public int KanbanId { get; set; }
        public int UserId { get; set; }

        public virtual Kanbans Kanban { get; set; }
        public virtual Projects Project { get; set; }
        public virtual CalendarroUsers User { get; set; }
    }
}
