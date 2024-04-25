using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Identity.EntityConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        PasswordHasher<ApplicationUser> hasher = new();

        Result<UserName> firstNameResult = UserName.Create("System");
        Result<UserName> lastNameResult = UserName.Create("Admin");

        Result.FirstFailureOrSuccess(firstNameResult, lastNameResult);

        if(firstNameResult.IsFailure)
        {
            throw new InvalidOperationException(firstNameResult.Error.Message);
        }

        builder.Ignore(user => user.FullName);

        builder.ComplexProperty(user => user.FirstName, userBuilder =>
        {
            userBuilder.Property(firstName => firstName.Value)
                .HasColumnName(nameof(ApplicationUser.FirstName))
                .HasMaxLength(UserName.MaxLength)
                .IsRequired();
        });

        builder.ComplexProperty(user => user.LastName, userBuilder =>
        {
            userBuilder.Property(lastName => lastName.Value)
                .HasColumnName(nameof(ApplicationUser.LastName))
                .HasMaxLength(UserName.MaxLength)
                .IsRequired();
        });

        builder.HasData(
            new ApplicationUser(firstNameResult.Value, lastNameResult.Value)
            {
                Id = "82ef7b08-5017-4718-988e-e4f119594fca",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                EmailConfirmed = true
            });
    }
}
