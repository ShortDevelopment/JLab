using System.Diagnostics;

namespace ShortDev.JLab.JNI.Internal;

[DebuggerDisplay("Value = {Value}")]
internal readonly struct jint
{
    public jint(int value)
        => Value = value;

    public readonly int Value;

    public static implicit operator jint(int value)
        => new(value);
}
