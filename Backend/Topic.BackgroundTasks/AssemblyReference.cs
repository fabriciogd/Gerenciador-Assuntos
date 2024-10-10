using System.Reflection;

namespace Topic.BackgroundTasks;

internal static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}