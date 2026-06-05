using System.ComponentModel.DataAnnotations;
using bilet3.Models.Base;

namespace bilet3.Models
{
    public class Person : BaseEntity
    {
        //[Required(ErrorMessage = "Fullname is required")]
        //[StringLength(20, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 and 20 characters")]
        public string Fullname { get; set; }


        //[Required(ErrorMessage = "Designation is required")]
        //[StringLength(30, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 and 20 characters")]
        public string Designation { get; set; }

        public string Image { get; set; }

    }
}
