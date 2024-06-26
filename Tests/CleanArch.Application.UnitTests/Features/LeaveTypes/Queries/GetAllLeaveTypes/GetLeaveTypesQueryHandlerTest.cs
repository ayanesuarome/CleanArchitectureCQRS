﻿using CleanArch.Application.Tests.Features.Mocks;
using Moq;
using Shouldly;
using CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveTypes;

namespace CleanArch.Application.Tests.Features.LeaveTypes.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandlerTest : IDisposable
{
    #region Fields

    private GetLeaveTypeList.Handler _handler;
    private Mock<ILeaveTypeRepository> _repositoryMock;

    #endregion

    #region Setup and Cleanup

    public GetLeaveTypesQueryHandlerTest()
    {
        _repositoryMock = MockLeaveTypeRepository.GetLeaveTypeRepositoryMock();
        //MapperConfiguration mapperConfig = new(cfg => cfg.AddProfile<LeaveTypeProfile>());
        //_mapper = mapperConfig.CreateMapper();

        _handler = new GetLeaveTypeList.Handler(_repositoryMock.Object);
    }

    public void Dispose()
    {
        _repositoryMock = null;
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
        Result<GetLeaveTypeList.Response> result = await _handler.Handle(It.IsAny<GetLeaveTypeList.Query>(), default);

        _repositoryMock
            .Verify(m => m.GetAsync(), Times.Once);
        result.Value.ShouldNotBeNull();
        result.Value.LeaveTypes.Count.ShouldBe(3);
    }

    #endregion
}
