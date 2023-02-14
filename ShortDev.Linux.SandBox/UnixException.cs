using System.Runtime.InteropServices;

namespace ShortDev.Linux.SandBox;

internal sealed class UnixException : Exception
{
    public UnixException() : this(Marshal.GetLastPInvokeError()) { }
    public UnixException(int errorCode) : base(GetErrorMessage(errorCode))
    {
        ErrorCode = errorCode;
    }

    public int ErrorCode { get; }

    static unsafe string GetErrorMessage(int errorCode)
    {
        const int capacity = 256;
        char* buf = stackalloc char[capacity];
        if (Native.strerror_r(errorCode, buf, capacity) != 0)
            return "Error while getting error message";
        return new string(buf);
    }
}
