using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Member
{
    public class CreateMemberHealthRecordViewModel
    {
        [Required(ErrorMessage = "Height Is Required")]
        [Range(0.1, 300, ErrorMessage = "Height Must Be between 0.1 and 300")]
        public decimal Weight { get; set; }
        [Required(ErrorMessage = "Weight Is Required")]
        [Range(0.1, 500, ErrorMessage = "Height Must Be between 0.1 and 500")]
        public decimal Height { get; set; }
        [Required(ErrorMessage = "BloodType Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "BloodType Must Be between 2 and 30 char")]

        [RegularExpression(@"^(A|B|AB|O)[+-]$", ErrorMessage = "Blood type must include + or - sign (e.g., A+, O-)")]
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }
    }
}
