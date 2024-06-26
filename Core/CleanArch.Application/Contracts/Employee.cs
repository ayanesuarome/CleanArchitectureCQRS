﻿namespace CleanArch.Application.Contracts;

public sealed record Employee(Guid Id, string Email, string FirstName, string LastName)
{
    public string FullName => $"{FirstName} {LastName}";
}