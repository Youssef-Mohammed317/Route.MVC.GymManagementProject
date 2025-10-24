using GymManagement.DAL.Data.Context;
using GymManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext context;

        private readonly ICategoryRepository? categoryRepository;
        private readonly IHealthRecordRepository? healthRecordRepository;
        private readonly IMemberRepository? memberRepository;
        private readonly IMemberSessionRepository? memberSessionRepository;
        private readonly IMemberShipRepository? memberShipRepository;
        private readonly IPlanRepository? planRepository;
        private readonly ISessionRepository? sessionRepository;
        private readonly ITrainerRepository? trainerRepository;

        public UnitOfWork(GymDbContext _context)
        {
            context = _context;
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    return new CategoryRepository(context);
                }
                return categoryRepository;
            }
        }
        public IMemberRepository MemberRepository
        {
            get
            {
                if (memberRepository == null)
                {
                    return new MemberRepository(context);
                }
                return memberRepository;
            }
        }
        public IMemberSessionRepository MemberSessionRepository
        {
            get
            {
                if (memberSessionRepository == null)
                {
                    return new MemberSessionRepository(context);
                }
                return memberSessionRepository;
            }
        }
        public IMemberShipRepository MemberShipRepository
        {
            get
            {
                if (memberShipRepository == null)
                {
                    return new MemberShipRepository(context);
                }
                return memberShipRepository;
            }
        }
        public IPlanRepository PlanRepository
        {
            get
            {
                if (planRepository == null)
                {
                    return new PlanRepository(context);
                }
                return planRepository;
            }
        }
        public ISessionRepository SessionRepository
        {
            get
            {
                if (sessionRepository == null)
                {
                    return new SessionRepository(context);
                }
                return sessionRepository;
            }
        }
        public ITrainerRepository TrainerRepository
        {
            get
            {
                if (trainerRepository == null)
                {
                    return new TrainerRepository(context);
                }
                return trainerRepository;
            }
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
