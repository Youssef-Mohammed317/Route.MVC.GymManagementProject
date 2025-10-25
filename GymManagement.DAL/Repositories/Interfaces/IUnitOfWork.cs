using GymManagement.DAL.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public int SaveChanges();

        public ICategoryRepository CategoryRepository { get; }
        public IMemberRepository MemberRepository { get; }
        public IMemberSessionRepository MemberSessionRepository { get; }
        public IMembershipRepository MembershipRepository { get; }
        public IPlanRepository PlanRepository { get; }
        public ISessionRepository SessionRepository { get; }
        public ITrainerRepository TrainerRepository { get; }



    }
}
