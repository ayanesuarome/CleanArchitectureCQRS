﻿using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Errors;
using Newtonsoft.Json;

namespace CleanArch.Domain.LeaveRequests;

public sealed record DateRange
{
    [JsonConstructor]
    private DateRange(DateOnly startDate, DateOnly endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public DateOnly StartDate { get; }
    public DateOnly EndDate { get; }

    public static Result<DateRange> Create(DateOnly startDate, DateOnly endDate)
    {
        if (!IsValidRange(startDate, endDate))
        {
            return Result.Failure<DateRange>(DomainErrors.DateRange.RangeIsNotValid);
        }

        return Result.Success<DateRange>(new(startDate, endDate));
    }

    private static bool IsValidRange(DateOnly startDate, DateOnly endDate)
    {
        if (startDate == DateOnly.MinValue)
        {
            return false;
        }

        if (startDate.CompareTo(endDate) > 0)
        {
            return false;
        }

        return true;
    }
}
