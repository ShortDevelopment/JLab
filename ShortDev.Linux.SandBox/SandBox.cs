using System.Runtime.Versioning;
using System.Text;

namespace ShortDev.Linux.SandBox;

[SupportedOSPlatform("linux")]
public sealed class SandBox
{
    public static void EnableStrict()
    {
        // https://github.com/dropbox/lepton/blob/f34c7f42d2e0d688239cb880648dc152713c4eae/src/io/Seccomp.cc#L118
        ThrowOnError(Native.prctl(PR.SET_NO_NEW_PRIVS, 1, 0, 0, 0));

        // https://blog.cloudflare.com/sandboxing-in-linux-with-zero-lines-of-code/
        var ctx = Native.seccomp_init(SCMP_ACT.ALLOW);

        string[] disallowedSysCalls = {
            "open",
            "openat",
            "creat",
            "socket",
            "open_by_handle_at"
        };

        foreach (var item in disallowedSysCalls)
        {
            DisallowSysCall(ctx, item);
        }

        ThrowOnError(Native.seccomp_load(ctx));
        Native.seccomp_release(ctx);

        static unsafe void DisallowSysCall(scmp_filter_ctx ctx, string sysCall)
        {
            var length = Encoding.ASCII.GetByteCount(sysCall);
            var pName = stackalloc byte[length];
            Encoding.ASCII.GetBytes(sysCall, new(pName, length));

            var callNum = Native.seccomp_syscall_resolve_name(pName);
            if (callNum < 0)
                throw new FileNotFoundException($"Could not find syscall number for {sysCall}");

            ThrowOnError(Native.seccomp_rule_add(ctx, SCMP_ACT.KILL_PROCESS, callNum, 0));
        }
    }

    static void ThrowOnError(int error)
    {
        if (error < 0)
            throw new UnixException(error);
    }
}
