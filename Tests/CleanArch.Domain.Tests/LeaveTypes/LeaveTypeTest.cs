using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveTypes;
using FluentAssertions;

namespace CleanArch.Domain.Tests.Entities;

public class LeaveTypeTest
{
    [Fact]
    public void Constructor_Should_Throw_ArgumentNullException_WhenNameIsInvalid()
    {
        // Arrange
        DefaultDays defaultDays = DefaultDays.Create(10).Value;
        LeaveTypeNameUniqueRequirement requirement = new(() => Task.FromResult(true));

        // Act
        LeaveType Action() => LeaveType.Create(null, defaultDays, requirement).Value;

        // Assert
        FluentActions.Invoking(Action).Should().Throw<ArgumentNullException>()
            .Which.ParamName.Should().Be("name");
    }

    [Fact]
    public void Constructor_Should_Throw_ArgumentNullException_WhenDefaultDaysIsInvalid()
    {
        // Arrange
        Name name = Name.Create("Test").Value;
        LeaveTypeNameUniqueRequirement requirement = new(() => Task.FromResult(true));

        // Act
        LeaveType Action() => LeaveType.Create(name, null, requirement).Value;

        // Assert
        FluentActions.Invoking(Action).Should().Throw<ArgumentNullException>()
            .Which.ParamName.Should().Be("defaultDays");
    }

    [Fact]
    public void Constructor_Should_CreateLeaveType_WhenNameAndDefaultDaysAreValid()
    {
        // Arrange
        Name name = Name.Create("Test").Value;
        DefaultDays defaultDays = DefaultDays.Create(10).Value;
        LeaveTypeNameUniqueRequirement requirement = new(() => Task.FromResult(true));

        // Act
        LeaveType leaveType = LeaveType.Create(name, defaultDays, requirement).Value;

        // Assert
        leaveType.Should().NotBeNull();
        leaveType.Name.Should().Be(name);
        leaveType.DefaultDays.Should().Be(defaultDays);
    }
}
