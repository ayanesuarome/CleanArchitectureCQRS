﻿namespace CleanArch.Integration.Tests;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
internal sealed class TestPriorityAttribute : Attribute
{
    public TestPriorityAttribute(int priority) => Priority = priority;

    public int Priority { get; private set; }
}
