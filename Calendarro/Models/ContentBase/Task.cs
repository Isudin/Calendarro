using System;
using System.Collections.Generic;

#nullable disable

namespace Calendarro.Models.ContentBase
{
    public partial class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}
