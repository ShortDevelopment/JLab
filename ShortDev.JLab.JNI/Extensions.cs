using System.Text;

namespace ShortDev.JLab.JNI;

internal static class Extensions
{
    public static unsafe Span<byte> ToUTF8(this string @this)
        => ToEncoding((ReadOnlySpan<char>)@this, Encoding.UTF8);

    public static unsafe Span<byte> ToEncoding(this string @this, Encoding encoding)
        => ToEncoding((ReadOnlySpan<char>)@this, encoding);

    public static unsafe Span<byte> ToEncoding(this ReadOnlySpan<char> @this, Encoding encoding)
    {
        var length = encoding.GetByteCount(@this);
        Span<byte> data = new byte[length];
        encoding.GetBytes(@this, data);
        return data;
    }
}
