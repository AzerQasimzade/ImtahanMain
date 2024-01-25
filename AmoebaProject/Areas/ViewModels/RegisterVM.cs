using System.ComponentModel.DataAnnotations;

namespace AmoebaProject.Areas.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


    }
}
