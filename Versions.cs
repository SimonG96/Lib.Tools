using System.Reflection;

namespace Lib.Tools;

public static class Versions
{
    public static Version? GetVersion() => Assembly.GetEntryAssembly()?.GetName().Version;
}