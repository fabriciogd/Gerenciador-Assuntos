﻿using System.Reflection;

namespace Topic.Application;

internal static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}