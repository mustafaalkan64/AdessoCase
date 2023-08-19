using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdessoCase.Core;

namespace AdessoCase.Repository.Seeds
{
    internal class CitySeed : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasData(new City
            {
                Id = 1,
                Name = "İzmir",
                CreatedDate = DateTime.Now
            },

            new City
            {
                Id = 2,
                Name = "İstanbul",
                CreatedDate = DateTime.Now


            },
            new City
            {
                Id = 3,
                Name = "Ankara",
                CreatedDate = DateTime.Now


            });

        }
    }
}
