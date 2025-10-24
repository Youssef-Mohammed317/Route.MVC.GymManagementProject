using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Entites
{
    public class Session : BaseEntity
    {
        public string Descripcion { get; set; } = null!;

        public int Capacity { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #region Relationships

        #region Category - Session
        public virtual Category SessionCategory { get; set; } = null!;

        public int CategoryId { get; set; }
        #endregion

        #region Session - Trainer
        public virtual Trainer SessionTrainer { get; set; } = null!;
        public int TrainerId { get; set; }
        #endregion

        #region Session MemberSession
        public virtual ICollection<MemberSession> SessionMembers { get; set; } = null;
        #endregion
        #endregion
    }
}
