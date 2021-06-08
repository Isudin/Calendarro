using Calendarro.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendarro.ViewModels
{
    public class MainViewModel
    {
        public IEnumerable<Calendarro.ViewModels.KanbanWithTasksViewModel> KanbanWithTasks { get; set; }
        public AddNewTaskViewModel AddNewTaskViewModel { get; set; }
        public IEnumerable<Kanbans> KanbanList{ get; set; }
    }
}
