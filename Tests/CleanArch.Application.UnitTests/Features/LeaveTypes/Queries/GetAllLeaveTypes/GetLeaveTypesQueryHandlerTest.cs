using AutoMapper;
using CleanArch.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using Moq;

namespace CleanArch.Application.UnitTests.Features.LeaveTypes.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandlerTest : IDisposable
{
    #region Fields

    private GetLeaveTypeListQueryHandler handler;
    private Mock<IMapper> mapperMock;
    private Mock<ILeaveTypeRepository> repositoryMock;

    #endregion

    #region Setup and Cleanup

    public GetLeaveTypesQueryHandlerTest()
    {
        mapperMock = new Mock<IMapper>();
        repositoryMock = new Mock<ILeaveTypeRepository>();
        handler = new GetLeaveTypeListQueryHandler(mapperMock.Object, repositoryMock.Object);
    }

    public void Dispose()
    {
        mapperMock = null;
        repositoryMock = null;
        handler = null;
    }

    #endregion

    #region Test Methods

    /// <summary>
    /// Test that <see cref="GetLeaveTypeListQueryHandler.Handle(GetLeaveTypeListQuery, CancellationToken)"/>
    /// returns <see cref="List{LeaveTypeDto}"/>.
    /// </summary>
    [Fact]
    public async Task HandleReturnListOfLeaveTypeDto()
    {
        var handler = SetupRepositoryMock()
            .SetupMapperMock()
            .Build();

        List<LeaveTypeDto> leaveTypes = await handler.Handle(It.IsAny<GetLeaveTypeListQuery>(), default);
        Assert.NotNull(leaveTypes);
    }

    #endregion

    #region Mocking

    private GetLeaveTypeListQueryHandler Build()
    {
        return handler;
    }

    private GetLeaveTypesQueryHandlerTest SetupMapperMock()
    {
        mapperMock
            .Setup(m => m.Map<List<LeaveTypeDto>>(It.IsAny<List<LeaveType>>()))
            .Returns(new List<LeaveTypeDto>());

        return this;
    }

    private GetLeaveTypesQueryHandlerTest SetupRepositoryMock()
    {
        repositoryMock
            .Setup(m => m.GetAsync())
            .ReturnsAsync(new List<LeaveType>());

        return this;
    }

    #endregion
}
