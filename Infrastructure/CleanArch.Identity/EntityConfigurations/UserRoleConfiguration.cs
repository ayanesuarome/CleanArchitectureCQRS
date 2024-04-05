using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Identity.EntityConfigurations;

internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "bcfd3118-db1d-4bd6-af03-35536c4c5169",
                UserId = "82ef7b08-5017-4718-988e-e4f119594fca"
            },
            new IdentityUserRole<string>
            {
                RoleId = "fac1f94e-1c7b-4c6f-b0f3-1e1a697a39f9",
                UserId = "958c1a3b-eceb-4e29-af5b-908e08ab8a28"
            });
    }
}
