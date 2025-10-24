using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Entites
{
    public class MemberShip : BaseEntity
    {
        public DateTime EndDate { get; set; }
        // created_at => startdate

        public string Status
        {
            get
            {
                if (EndDate >= DateTime.Now)
                {
                    return "Expired";
                }
                else
                {
                    return "Active";
                }
            }
        }
        public virtual Member Member { get; set; } = null!;
        public int MemberId { get; set; }
        public virtual Plan Plan { get; set; } = null!;
        public int PlanId { get; set; }


    }
}
