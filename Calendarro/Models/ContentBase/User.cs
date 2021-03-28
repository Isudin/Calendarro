using System;
using System.Collections.Generic;

#nullable disable

namespace Calendarro.Models.ContentBase
{
    public partial class User
    {
        public User()
        {
            ProjectUserRelations = new HashSet<ProjectUserRelation>();
            Projects = new HashSet<Project>();
        }

        public int UserId { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public string EMail { get; set; }
        public DateTime CreateDate { get; set; }
        public int BelongsToProjects { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProjectUserRelation> ProjectUserRelations { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
