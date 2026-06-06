using System.ComponentModel.DataAnnotations;

namespace bilet3.Areas.Admin.ViewModels.Person
{
    public class UpdatePersonVM
    {
        [Required(ErrorMessage = "Fullname is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 and 20 characters")]
        public string Fullname { get; set; }


        [Required(ErrorMessage = "Designation is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 and 20 characters")]
        public string Designation { get; set; }

        public IFormFile? Photo { get; set; }

        public string? Image {  get; set; }
    }
}
