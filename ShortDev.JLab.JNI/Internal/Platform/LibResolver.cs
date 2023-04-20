using System.Reflection;
using System.Runtime.InteropServices;

namespace ShortDev.JLab.JNI.Internal.Platform;

internal static class LibResolver
{
    public const string JvmLibName = "jvm";

    static bool _initialized = false;
    public static void EnsureInitialized()
    {
        if (_initialized)
            return;

        NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), DllImportResolver);
        _initialized = true;
    }

    static IntPtr DllImportResolver(string libName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        string? path = null;
        if (libName == JvmLibName)
        {
            string? defaultPath = null;
            if (OperatingSystem.IsWindows())
                defaultPath = @"C:\Program Files\Microsoft\jdk-17.0.3.7-hotspot\bin\server\jvm.dll";

            if (OperatingSystem.IsLinux())
                defaultPath = @"/usr/lib/jvm/java-17-openjdk-amd64/lib/server/libjvm.so";

            path = Environment.GetEnvironmentVariable("LIB_JVM_PATH") ?? defaultPath ?? "Invalid JVM path";
        }

        if (path != null)
            return NativeLibrary.Load(path, assembly, searchPath);

        return IntPtr.Zero;
    }
}
