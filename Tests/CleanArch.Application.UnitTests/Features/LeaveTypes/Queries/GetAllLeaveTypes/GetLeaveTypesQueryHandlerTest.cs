using AutoMapper;
using CleanArch.Application.AutoMapper;
using CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;
using CleanArch.Application.Models;
using CleanArch.Application.Tests.Features.Mocks;
using CleanArch.Domain.Interfaces.Persistence;
using Moq;
using Shouldly;

namespace CleanArch.Application.Tests.Features.LeaveTypes.Queries.GetAllLeaveTypes;

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
        _repositoryMock = MockLeaveTypeRepository.GetLeaveTypeRepositoryMock();
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
        Result<List<LeaveTypeDto>> result = await _handler.Handle(It.IsAny<GetLeaveTypeListQuery>(), default);

        _repositoryMock
            .Verify(m => m.GetAsync(), Times.Once);
        result.Data.ShouldNotBeNull();
        result.Data.Count.ShouldBe(3);
    }

    #endregion
}
