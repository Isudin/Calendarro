using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Calendarro.Models.Database
{
    public partial class CalendarroUsers
    {
        public CalendarroUsers()
        {
            ProjectTasks = new HashSet<ProjectTasks>();
            ProjectUserRelation = new HashSet<ProjectUserRelation>();
            Projects = new HashSet<Projects>();
        }

        public int UserId { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string EMail { get; set; }
        public DateTime CreateDate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProjectTasks> ProjectTasks { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProjectUserRelation> ProjectUserRelation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Projects> Projects { get; set; }
    }
}
