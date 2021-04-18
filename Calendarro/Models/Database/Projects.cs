using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Calendarro.Models.Database
{
    public partial class Projects
    {
        public Projects()
        {
            Kanbans = new HashSet<Kanbans>();
            ProjectTasks = new HashSet<ProjectTasks>();
            ProjectUserRelation = new HashSet<ProjectUserRelation>();
        }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime? FinishingDate { get; set; }
        public string Description { get; set; }

        public virtual CalendarroUsers Creator { get; set; }
        public virtual ICollection<Kanbans> Kanbans { get; set; }
        public virtual ICollection<ProjectTasks> ProjectTasks { get; set; }
        public virtual ICollection<ProjectUserRelation> ProjectUserRelation { get; set; }
    }
}
