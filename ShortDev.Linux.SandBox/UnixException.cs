using System.Runtime.InteropServices;
using System.Text;

namespace ShortDev.Linux.SandBox;

internal sealed class UnixException : Exception
{
    public UnixException() : this(Marshal.GetLastPInvokeError()) { }
    public UnixException(int errorCode) : base($"Error {errorCode}: {GetErrorMessage(errorCode)}")
    {
        ErrorCode = errorCode;
    }

    public int ErrorCode { get; }

    static unsafe string GetErrorMessage(int errorCode)
    {
        var start = Native.strerror(errorCode);

        byte* end = start;
        while (*end != 0)
            end++;

        return Encoding.UTF8.GetString(start, (int)(end - start));
    }
}
