using GymManagement.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Repositories.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {

        public HealthRecord? GetHealthRecordByMemberId(int id);
        public Member? GetByEmail(string email);
        public Member? GetByPhone(string phone);
    }
}
