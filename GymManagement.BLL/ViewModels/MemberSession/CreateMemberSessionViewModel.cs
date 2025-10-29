using GymManagement.BLL.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.MemberSession
{
    public class CreateMemberSessionViewModel
    {
        public int SessionId { get; set; }
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Phone Is Required")]
        [Phone(ErrorMessage = "Invaild Phone Format")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Must Be Vaild Egyption Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = string.Empty;
        public IEnumerable<MemberSelectModel> Members { get; set; } = new List<MemberSelectModel>();
    }
}
