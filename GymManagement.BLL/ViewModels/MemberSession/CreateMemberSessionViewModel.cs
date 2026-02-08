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
        public IEnumerable<MemberSelectModel> Members { get; set; } = new List<MemberSelectModel>();
    }
}
