using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Calendarro.ViewModels
{
    public class AddNewTaskViewModel
    {
        [DisplayName("Nazwa zadania")]
        [Required(ErrorMessage = "Task musi mieć nazwę.")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "Dzień")]
        [Required(ErrorMessage = "Task musi mieć datę.")]
        public DateTimeOffset FinishDate { get; set; }

        [Display(Name = "Kanban")]
        [Required(ErrorMessage = "Task musi być przypisany do kanbanu.")]
        public int Kanban { get; set; }
    }
}
