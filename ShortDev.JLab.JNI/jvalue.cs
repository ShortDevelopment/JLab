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

    public static void Initialize(jvalue* @this, ArgIterator iterator)
    {
        var item = @this;
        while (iterator.GetRemainingCount() > 0)
        {
            var hArg = iterator.GetNextArg();
            var type = __reftype(hArg);
            var typeCode = Type.GetTypeCode(type);

            switch (typeCode)
            {
                case TypeCode.Boolean:
                    item->z = __refvalue(hArg, bool);
                    break;
                case TypeCode.Char:
                    item->c = __refvalue(hArg, char);
                    break;
                case TypeCode.SByte:
                    item->b = __refvalue(hArg, sbyte);
                    break;
                case TypeCode.Byte:
                    item->b = (sbyte)__refvalue(hArg, byte);
                    break;
                case TypeCode.Int16:
                    item->s = __refvalue(hArg, short);
                    break;
                case TypeCode.UInt16:
                    item->s = (short)__refvalue(hArg, ushort);
                    break;
                case TypeCode.Int32:
                    item->i = __refvalue(hArg, int);
                    break;
                case TypeCode.UInt32:
                    item->i = (int)__refvalue(hArg, uint);
                    break;
                case TypeCode.Int64:
                    item->j = __refvalue(hArg, long);
                    break;
                case TypeCode.UInt64:
                    item->j = (long)__refvalue(hArg, ulong);
                    break;
                case TypeCode.Single:
                    item->f = __refvalue(hArg, float);
                    break;
                case TypeCode.Double:
                    item->d = __refvalue(hArg, double);
                    break;
                case TypeCode.Object:
                    if (!type.IsPointer)
                        throw new InvalidOperationException("Only object pointers are supported!");

                    item->l = (IntPtr)__refvalue(hArg, void*);
                    break;
                default:
                    throw new InvalidOperationException($"Type {type} is not supported!");
            }

            item++;
        }
    }
}
