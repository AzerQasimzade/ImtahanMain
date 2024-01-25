using System.ComponentModel.DataAnnotations;

namespace AmoebaProject.Areas.ViewModels
{
    public class UpdateEmployeeVM
    {
        [MinLength(3)]
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

        public string XLink { get; set; }

        public string FacebookLink { get; set; }


        public string InstagramLink { get; set; }


        public string LinkEdinLink { get; set; }

    }
}
