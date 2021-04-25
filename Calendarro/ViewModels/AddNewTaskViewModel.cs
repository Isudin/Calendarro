using System;
using System.ComponentModel.DataAnnotations;

namespace Calendarro.ViewModels
{
    public class AddNewTaskViewModel
    {
        [Required(ErrorMessage = "Task musi mieć nazwę.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Task musi mieć datę.")]
        public DateTimeOffset FinishDate { get; set; }

        [Required(ErrorMessage = "Task musi być przypisany do kanbanu.")]
        public int Kanban { get; set; }
    }
}
