using System.ComponentModel.DataAnnotations;

namespace AmoebaProject.Areas.ViewModels.Employee
{
    public class CreateEmployeeVM
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Profession { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(256)]
        public string Description { get; set; }
        [Required]
        public string XLink { get; set; }
        [MinLength(2)]
        [MaxLength(50)]
        public string FacebookLink { get; set; }
        [MinLength(2)]
        [MaxLength(50)]
        public string InstagramLink { get; set; }
        [MinLength(2)]
        [MaxLength(50)]
        public string LinkEdinLink { get; set; }


    }
}
