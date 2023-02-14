using System.Runtime.InteropServices;

namespace ShortDev.Linux.SandBox;

internal static unsafe class Native
{
    [DllImport("libc.so")]
    public static extern int seccomp(SECCOMP operation, SECCOMP_FILTER_FLAG flags, void* args);

    [DllImport("libc.so")]
    public static extern int prctl(PR option, ulong arg2, ulong arg3, ulong arg4, ulong arg5);

    [DllImport("libc.so", ExactSpelling = true)]
    public static extern int strerror_r(int errnum, char* buf, ulong buflen);
}

internal enum PR
{
    SET_SECCOMP = 22
}

internal enum SECCOMP : uint
{
    SET_MODE_STRICT,
    SET_MODE_FILTER,
    GET_ACTION_AVAIL,
    GET_NOTIF_SIZES
}

internal enum SECCOMP_FILTER_FLAG : uint
{
    TSYNC = 1 << 0,
    LOG = 1 << 1,
    SPEC_ALLOW = 1 << 2,
    NEW_LISTENER = 1 << 3,
    TSYNC_ESRCH = 1 << 4,
    /// <summary>
    /// Received notifications wait in killable state (only respond to fatal signals)
    /// </summary>
    WAIT_KILLABLE_RECV = 1 << 5
}