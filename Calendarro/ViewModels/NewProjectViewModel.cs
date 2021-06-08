using System;
using System.ComponentModel.DataAnnotations;

namespace Calendarro.ViewModels
{
    public class NewProjectViewModel
    {
        [Required(ErrorMessage ="Pole wymagane.")]
        [Display(Name = "Nazwa projektu")]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "Nazwa projektu musi mieć od 5 do 20 znaków.", MinimumLength = 5)]
        public string ProjectName { get; set; }

        [Display(Name = "Data ukończenia projektu(opcjonalne)")]
        [DataType(DataType.Date)]
        public DateTime? FinishDate { get; set; }

        [Display(Name = "Opis(opcjonalne)")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Dane nie poprawne.", MinimumLength = 0)]
        public string Description { get; set; }
    }
}
