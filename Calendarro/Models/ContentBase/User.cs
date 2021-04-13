using System;
using System.Collections.Generic;

#nullable disable

namespace Calendarro.Models.ContentBase
{
    public partial class User
    {
        public User()
        {
            ProjectTasks = new HashSet<ProjectTask>();
            ProjectUserRelations = new HashSet<ProjectUserRelation>();
            Projects = new HashSet<Project>();
        }

        public int UserId { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string EMail { get; set; }
        public DateTime CreateDate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<ProjectUserRelation> ProjectUserRelations { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
