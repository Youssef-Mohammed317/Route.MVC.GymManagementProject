using GymManagement.DAL.Data.DataSeed;
using GymManagement.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Data.Context
{
    public class GymDbContext : DbContext
    {
        public GymDbContext() { }

        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Membership> MemberShips { get; set; }

    }
}
