using System;
using System.Collections.Generic;

#nullable disable

namespace Calendarro.Models.ContentBase
{
    public partial class ProjectUserRelation
    {
        public int LinkId { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }

        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
