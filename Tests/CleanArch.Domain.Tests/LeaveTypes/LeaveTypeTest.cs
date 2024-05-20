using CleanArch.Domain.Entities;
using CleanArch.Domain.ValueObjects;
using FluentAssertions;

namespace CleanArch.Domain.Tests.Entities;

public class LeaveTypeTest
{
    [Fact]
    public void Constructor_Should_Throw_ArgumentNullException_WhenNameIsInvalid()
    {
        // Arrange
        DefaultDays defaultDays = DefaultDays.Create(10).Value;

        // Act
        LeaveType Action() => new(null, defaultDays);

        // Assert
        FluentActions.Invoking(Action).Should().Throw<ArgumentNullException>()
            .Which.ParamName.Should().Be("name");
    }

    [Fact]
    public void Constructor_Should_Throw_ArgumentNullException_WhenDefaultDaysIsInvalid()
    {
        // Arrange
        Name name = Name.Create("Test").Value;

        // Act
        LeaveType Action() => new(name, null);

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

        // Act
        LeaveType leaveType = new(name, defaultDays);

        // Assert
        leaveType.Should().NotBeNull();
        leaveType.Name.Should().Be(name);
        leaveType.DefaultDays.Should().Be(defaultDays);
    }
}
