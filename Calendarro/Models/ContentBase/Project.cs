using System;
using System.Collections.Generic;

#nullable disable

namespace Calendarro.Models.ContentBase
{
    public partial class Project
    {
        public Project()
        {
            ProjectUserRelations = new HashSet<ProjectUserRelation>();
            Tasks = new HashSet<Task>();
        }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime? FinishingDate { get; set; }
        public string Description { get; set; }
        public int HasTasks { get; set; }

        public virtual User Creator { get; set; }
        public virtual ICollection<ProjectUserRelation> ProjectUserRelations { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
