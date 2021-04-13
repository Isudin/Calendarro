using System;
using System.Collections.Generic;

#nullable disable

namespace Calendarro.Models.ContentBase
{
    public partial class Project
    {
        public Project()
        {
            Kanbans = new HashSet<Kanban>();
            ProjectTasks = new HashSet<ProjectTask>();
            ProjectUserRelations = new HashSet<ProjectUserRelation>();
        }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime? FinishingDate { get; set; }
        public string Description { get; set; }

        public virtual User Creator { get; set; }
        public virtual ICollection<Kanban> Kanbans { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<ProjectUserRelation> ProjectUserRelations { get; set; }
    }
}
