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
        // Currently ComplexProperty in EF8 does not support seeding.

        //PasswordHasher<User> hasher = new();

        //Result<UserName> firstNameResult = UserName.Create("System");
        //Result<UserName> lastNameResult = UserName.Create("Admin");

        //Result.FirstFailureOrSuccess(firstNameResult, lastNameResult);

        //if (firstNameResult.IsFailure)
        //{
        //    throw new InvalidOperationException(firstNameResult.Error.Message);
        //}

        //User user = new User(firstNameResult.Value, lastNameResult.Value);

        builder.Ignore(user => user.FullName);

        builder.Property(user => user.FirstName)
            .HasConversion(
                firstName => firstName.Value,
                value => UserName.Create(value).Value)
            .HasColumnName(nameof(User.FirstName))
            .HasMaxLength(UserName.MaxLength);

        builder.Property(user => user.LastName)
            .HasConversion(
                lastName => lastName.Value,
                value => UserName.Create(value).Value)
            .HasColumnName(nameof(User.LastName))
            .HasMaxLength(UserName.MaxLength);

        //builder.HasData(user);
    }
}
