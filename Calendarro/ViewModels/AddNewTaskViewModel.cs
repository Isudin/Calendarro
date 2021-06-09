using Calendarro.Models.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Calendarro.ViewModels
{
    public class AddNewTaskViewModel
    {
        [DisplayName("New tasks")]
        [Required(ErrorMessage = "Task name required.")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Task date required.")]
        public DateTimeOffset FinishDate { get; set; }

        [Display(Name = "Kanban")]
        [Required(ErrorMessage = "Task kanban required.")]
        public int Kanban { get; set; }
        public IEnumerable<Kanbans> KanbanList { get; set; }
    }
}
