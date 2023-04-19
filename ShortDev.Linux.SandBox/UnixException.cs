using System.Runtime.InteropServices;
using System.Text;

namespace ShortDev.Linux.SandBox;

internal sealed class UnixException : Exception
{
    public UnixException() : this(Marshal.GetLastPInvokeError()) { }
    public UnixException(int errorCode) : base($"Error {errorCode}\n{GetErrorMessage(errorCode)}")
    {
        ErrorCode = errorCode;
    }

    public int ErrorCode { get; }

    static unsafe string GetErrorMessage(int errorCode)
    {
        const int capacity = 1024;
        fixed (char* buf = new char[capacity])
        {
            Native.strerror_r(errorCode, buf, capacity);
            return Encoding.UTF8.GetString((byte*)buf, capacity);
        }
    }
}
