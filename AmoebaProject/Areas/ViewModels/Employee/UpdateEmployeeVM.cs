using System.ComponentModel.DataAnnotations;

namespace AmoebaProject.Areas.ViewModels.Employee
{
    public class UpdateEmployeeVM
    {
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        public string Profession { get; set; }
        public IFormFile? Photo { get; set; }
        [MinLength(5)]
        [MaxLength(256)]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required]

        public string XLink { get; set; }
        [Required]
        public string FacebookLink { get; set; }
        [Required]

        public string InstagramLink { get; set; }
        [Required]

        public string LinkEdinLink { get; set; }

    }
}
