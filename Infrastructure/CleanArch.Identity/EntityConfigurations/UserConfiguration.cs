using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Identity.EntityConfigurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Ignore(user => user.FullName);

        builder.Property(user => user.FirstName)
            .HasConversion(
                firstName => firstName.Value,
                value => UserName.Create(value).Value)
            .HasMaxLength(UserName.MaxLength);

        builder.Property(user => user.LastName)
            .HasConversion(
                lastName => lastName.Value,
                value => UserName.Create(value).Value)
            .HasMaxLength(UserName.MaxLength);

        // Seeding
        PasswordHasher<User> hasher = new();

        Result<UserName> firstNameResult = UserName.Create("System");
        Result<UserName> lastNameResult = UserName.Create("Admin");

        Result.FirstFailureOrSuccess(firstNameResult, lastNameResult);

        if (firstNameResult.IsFailure)
        {
            throw new InvalidOperationException(firstNameResult.Error.Message);
        }

        User user = new User(firstNameResult.Value, lastNameResult.Value)
        {
            Id = Guid.Parse("82ef7b08-5017-4718-988e-e4f119594fca"),
            Email = "admin@localhost.com",
            NormalizedEmail = "ADMIN@LOCALHOST.COM",
            UserName = "admin@localhost.com",
            NormalizedUserName = "ADMIN@LOCALHOST.COM",
            PasswordHash = hasher.HashPassword(null, "P@ssword1")
        };

        builder.HasData(user);
    }
}
