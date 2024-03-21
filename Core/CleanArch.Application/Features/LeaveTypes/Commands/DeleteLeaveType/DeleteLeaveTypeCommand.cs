﻿using CleanArch.Application.Models;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.DeleteLeaveType;

public record DeleteLeaveTypeCommand(int Id) : IRequest<Result<Unit>>
{
}
