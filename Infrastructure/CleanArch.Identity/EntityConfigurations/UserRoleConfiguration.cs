using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Identity.EntityConfigurations;

internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.HasData(
            new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("d9fca87d-b460-43a1-8d72-e31b189dc353"),
                UserId = Guid.Parse("82ef7b08-5017-4718-988e-e4f119594fca")
            });
    }
}
