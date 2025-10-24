using GymManagement.BLL.ViewModels.Common;
using GymManagement.DAL.Entites.Enums;
using GymManagement.PL.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Trainer
{
    public class UpdateTrainerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name Must Be between 2 and 50 char")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name Can Only Contain Letters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invaild Email Format")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must Be Between 5 and 100 char")]
        [UniqueEmail]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone Is Required")]
        [Phone(ErrorMessage = "Invaild Phone Format")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Must Be Vaild Egyption Number")]
        [DataType(DataType.PhoneNumber)]
        [UniquePhone]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Binding Number Is Required")]
        [Range(1, 9000, ErrorMessage = "Binding Number Must Be Between 1 and 9000")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street Must Be Between 2 and 30 char")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Name Must Be between 2 and 30 char")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Can Only Contain Letters")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Specialty is required")]
        [EnumDataType(typeof(Specialties))]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid specialty")]
        public Specialties Specialties { get; set; }
    }
}
