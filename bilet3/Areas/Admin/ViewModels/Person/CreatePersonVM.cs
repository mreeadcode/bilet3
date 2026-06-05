using System.ComponentModel.DataAnnotations;

namespace bilet3.Areas.Admin.ViewModels.Person
{
    public class CreatePersonVM
    {
        [Required(ErrorMessage = "Fullname is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 and 20 characters")]
        public string Fullname { get; set; }


        [Required(ErrorMessage = "Designation is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 and 20 characters")]
        public string Designation { get; set; }


        [Required(ErrorMessage = "Photo is required")]
        public IFormFile Photo { get; set; }
    }
}
