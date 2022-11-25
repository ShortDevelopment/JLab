using System.Diagnostics;

namespace ShortDev.JLab.JNI.Internal;

[DebuggerDisplay("Value = {Value}")]
internal readonly struct jboolean
{
    public jboolean(bool value)
        => Value = value ? (byte)1 : (byte)0;

    public readonly byte Value;

    public static implicit operator jboolean(bool value)
        => new(value);
    public static implicit operator bool(jboolean value)
        => value.Value == 0 ? false : true;
}
