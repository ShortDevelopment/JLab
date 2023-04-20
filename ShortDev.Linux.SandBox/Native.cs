using System.Runtime.InteropServices;

namespace ShortDev.Linux.SandBox;

internal static unsafe class Native
{
    [DllImport("libc", SetLastError = true)]
    public static extern int prctl(PR option, ulong arg2, ulong arg3, ulong arg4, ulong arg5);

    [DllImport("libc", ExactSpelling = true)]
    public static extern byte* strerror(int errnum);
}

internal enum PR : int
{
    GET_SECCOMP = 21,
    SET_SECCOMP,
    SET_NO_NEW_PRIVS = 38,
    GET_NO_NEW_PRIVS
}

internal enum SECCOMP : uint
{
    MODE_DISABLED,
    MODE_STRICT,
    MODE_FILTER
}