﻿namespace CleanArch.Contracts.Identity;

public record LoginRequest(string Email, string Password)
{
}