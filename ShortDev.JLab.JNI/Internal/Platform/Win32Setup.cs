using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ShortDev.JLab.JNI.Internal.Platform;

internal static class Win32Setup
{
    public static void Setup()
    {
        if (AddDllDirectory(@"C:\Program Files\Microsoft\jdk-17.0.3.7-hotspot\bin\") == 0)
            throw new Win32Exception();
        if (AddDllDirectory(@"C:\Program Files\Microsoft\jdk-17.0.3.7-hotspot\bin\server\") == 0)
            throw new Win32Exception();
    }

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern int AddDllDirectory(string NewDirectory);
}
