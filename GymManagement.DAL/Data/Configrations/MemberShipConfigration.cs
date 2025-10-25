using GymManagement.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.DAL.Data.Configrations
{
    public class MemberShipConfigration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Property(x => x.Created_at)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasKey(x => new { x.MemberId, x.PlanId, x.Created_at });
            builder.Ignore(x => x.Id);


        }
    }
}
