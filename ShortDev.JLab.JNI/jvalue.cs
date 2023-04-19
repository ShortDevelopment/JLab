using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ShortDev.JLab.JNI;

[StructLayout(LayoutKind.Explicit)]
internal unsafe ref struct jvalue
{
    [FieldOffset(0)] bool z;
    [FieldOffset(0)] sbyte b;
    [FieldOffset(0)] char c;
    [FieldOffset(0)] short s;
    [FieldOffset(0)] int i;
    [FieldOffset(0)] long j;
    [FieldOffset(0)] float f;
    [FieldOffset(0)] double d;
    [FieldOffset(0)] IntPtr l;

    public static void Initialize(jvalue* @this, object[] args)
    {
        var item = @this;
        foreach (var arg in args)
        {
            var type = arg.GetType();
            var typeCode = Type.GetTypeCode(type);

            switch (typeCode)
            {
                case TypeCode.Boolean:
                    item->z = (bool)(arg);
                    break;
                case TypeCode.Char:
                    item->c = (char)(arg);
                    break;
                case TypeCode.SByte:
                    item->b = (sbyte)(arg);
                    break;
                case TypeCode.Byte:
                    item->b = (sbyte)(byte)(arg);
                    break;
                case TypeCode.Int16:
                    item->s = (short)(arg);
                    break;
                case TypeCode.UInt16:
                    item->s = (short)(ushort)(arg);
                    break;
                case TypeCode.Int32:
                    item->i = (int)(arg);
                    break;
                case TypeCode.UInt32:
                    item->i = (int)(uint)(arg);
                    break;
                case TypeCode.Int64:
                    item->j = (long)(arg);
                    break;
                case TypeCode.UInt64:
                    item->j = (long)(ulong)(arg);
                    break;
                case TypeCode.Single:
                    item->f = (float)(arg);
                    break;
                case TypeCode.Double:
                    item->d = (double)(arg);
                    break;
                case TypeCode.Object:
                    item->l = (IntPtr)(arg);
                    break;
                default:
                    throw new InvalidOperationException($"Type {type} is not supported!");
            }

            item++;
        }
    }
}
