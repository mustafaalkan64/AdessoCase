using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdessoCase.Core;

namespace AdessoCase.Repository.Configurations
{
    internal class TravelRequestsConfiguration : IEntityTypeConfiguration<TravelRequests>
    {
        public void Configure(EntityTypeBuilder<TravelRequests> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Travel).WithMany(x => x.TravelRequests).HasForeignKey(x => x.TravelId);
        }
    }
}
