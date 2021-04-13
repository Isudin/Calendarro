using System;
using System.Collections.Generic;

#nullable disable

namespace Calendarro.Models.ContentBase
{
    public partial class Kanban
    {
        public Kanban()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public int KanbanId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
