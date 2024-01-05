using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Identity.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "bcfd3118-db1d-4bd6-af03-35536c4c5169",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "fac1f94e-1c7b-4c6f-b0f3-1e1a697a39f9",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            });
    }
}
