using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendarro.Models.Dto
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime? FinishingDate { get; set; }
        public string Description { get; set; }
    }
}
