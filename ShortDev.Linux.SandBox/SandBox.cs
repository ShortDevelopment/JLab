using System.Runtime.Versioning;

namespace ShortDev.Linux.SandBox;

[SupportedOSPlatform("linux")]
public sealed class SandBox
{
    public static void EnableStrict()
    {
        // https://github.com/dropbox/lepton/blob/f34c7f42d2e0d688239cb880648dc152713c4eae/src/io/Seccomp.cc#L118
        if (Native.prctl(PR.SET_NO_NEW_PRIVS, 1, 0, 0, 0) != 0)
            throw new UnixException();

        // https://github.com/microsoft/WSL/issues/4035
        if (Native.prctl(PR.SET_SECCOMP, (ulong)SECCOMP.MODE_STRICT, 0, 0, 0) != 0)
            throw new UnixException();
    }
}
