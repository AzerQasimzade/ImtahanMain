using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace AmoebaProject.Areas.ViewModels.Setting
{
    public class UpdateSettingVM
    {
        [MinLength(2)]
        [MaxLength(50)]
        public string Value { get; set; }
    }
}
