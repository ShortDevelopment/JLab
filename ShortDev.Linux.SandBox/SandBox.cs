using System.Runtime.Versioning;

namespace ShortDev.Linux.SandBox;

[SupportedOSPlatform("linux")]
public sealed class SandBox
{
    public static void EnableStrict()
    {
        // https://github.com/microsoft/WSL/issues/4035
        if (Native.prctl(PR.SET_SECCOMP, (ulong)SECCOMP.SET_MODE_STRICT, 0, 0, 0) == -1)
            throw new UnixException();
    }
}
