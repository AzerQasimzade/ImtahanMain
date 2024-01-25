using System.ComponentModel.DataAnnotations;

namespace AmoebaProject.Areas.ViewModels
{
    public class LoginVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(256)]
        public string UsernameOrEmail { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
    }
}
