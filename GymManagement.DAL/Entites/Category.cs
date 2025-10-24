using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Entites
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Session> Sessions { get; set; } = null!;
    }
}
