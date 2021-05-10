using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendarro.Models.Dto
{
    public class RelationDto
    {
        public int RelationId { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
