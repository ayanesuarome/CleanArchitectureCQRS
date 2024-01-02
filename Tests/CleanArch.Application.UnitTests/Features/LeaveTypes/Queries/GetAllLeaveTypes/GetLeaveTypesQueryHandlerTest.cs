using AutoMapper;
using CleanArch.Application.AutoMapper;
using CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;
using CleanArch.Application.UnitTests.Features.Mocks;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using Moq;

namespace CleanArch.Application.UnitTests.Features.LeaveTypes.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandlerTest : IDisposable
{
    #region Fields

    private GetLeaveTypeListQueryHandler _handler;
    private IMapper _mapper;
    private Mock<ILeaveTypeRepository> _repositoryMock;

    #endregion

    #region Setup and Cleanup

    public GetLeaveTypesQueryHandlerTest()
    {
        _repositoryMock = MockListTypeRepository.GetLeaveTypeRepositoryMock();
        MapperConfiguration mapperConfig = new(cfg => cfg.AddProfile<LeaveTypeProfile>());
        _mapper = mapperConfig.CreateMapper();

        _handler = new GetLeaveTypeListQueryHandler(_mapper, _repositoryMock.Object);
    }

    public void Dispose()
    {
        _repositoryMock = null;
        _mapper = null;
        _handler = null;
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
        List<LeaveTypeDto> leaveTypes = await _handler.Handle(It.IsAny<GetLeaveTypeListQuery>(), default);
        Assert.NotNull(leaveTypes);
        Assert.Equal(3, leaveTypes.Count);
    }

    #endregion

    #region Mocking

    private GetLeaveTypeListQueryHandler Build()
    {
        return _handler;
    }

    private GetLeaveTypesQueryHandlerTest SetupRepositoryMock()
    {
        _repositoryMock
            .Setup(m => m.GetAsync())
            .ReturnsAsync(new List<LeaveType>());

        return this;
    }

    #endregion
}
