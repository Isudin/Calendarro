using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Calendarro.Models.Database
{
    public partial class Kanbans
    {
        public Kanbans()
        {
            ProjectTasks = new HashSet<ProjectTasks>();
        }

        public int KanbanId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public virtual Projects Project { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProjectTasks> ProjectTasks { get; set; }
    }
}
