using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendarro.Dto
{
    public class KanbanDto
    {
        public int KanbanId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
    }
}
