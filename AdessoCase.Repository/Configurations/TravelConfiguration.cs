using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdessoCase.Core;

namespace AdessoCase.Repository.Configurations
{
    internal class TravelConfiguration : IEntityTypeConfiguration<Travel>
    {
        public void Configure(EntityTypeBuilder<Travel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.TravelDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.Status).IsRequired();

            builder.ToTable("Travel");

            builder.HasOne(x => x.Departure).WithMany(x => x.DepartureNavigation).HasForeignKey(X => X.DepartureCityId);
            builder.HasOne(x => x.Arrival).WithMany(x => x.ArrivalNavigation).HasForeignKey(X => X.ArrivalCityId);
        }
    }
}
