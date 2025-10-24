using GymManagement.DAL.Entites.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Entites
{
    public class Trainer : GymUser
    {
        // HireDate => Create_at
        public Specialties Specialties { get; set; }

        public virtual ICollection<Session> TrainerSessions { get; set; } = null!;
    }
}
