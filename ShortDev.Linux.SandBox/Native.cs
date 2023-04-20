using System.Runtime.InteropServices;

namespace ShortDev.Linux.SandBox;

internal static unsafe class Native
{
    const string LibC = "libc";
    const string LibSeccomp = "libseccomp.so.2";

    [DllImport(LibC, SetLastError = true)]
    public static extern int prctl(PR option, ulong arg2, ulong arg3, ulong arg4, ulong arg5);

    [DllImport(LibC, ExactSpelling = true)]
    public static extern byte* strerror(int errnum);

    [DllImport(LibSeccomp, ExactSpelling = true)]
    public static extern scmp_filter_ctx seccomp_init(SCMP_ACT action);

    [DllImport(LibSeccomp, ExactSpelling = true)]
    public static extern int seccomp_rule_add(scmp_filter_ctx ctx, SCMP_ACT action, int sysCall, uint argCnt);

    [DllImport(LibSeccomp, ExactSpelling = true)]
    public static extern int seccomp_syscall_resolve_name(byte* name);

    [DllImport(LibSeccomp, ExactSpelling = true)]
    public static extern int seccomp_load(scmp_filter_ctx ctx);

    [DllImport(LibSeccomp, ExactSpelling = true)]
    public static extern void seccomp_release(scmp_filter_ctx ctx);
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

internal unsafe struct scmp_filter_ctx
{
    void* _ptr;
}

internal enum SCMP_ACT : uint
{
    KILL_PROCESS = 0x80000000U,
    KILL_THREAD = 0x00000000U,
    LOG = 0x7ffc0000U,
    ALLOW = 0x7fff0000U
}