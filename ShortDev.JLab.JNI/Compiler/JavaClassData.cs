namespace ShortDev.JLab.JNI.Compiler;

public sealed record JavaClassData(string Name, byte[] Data)
{

    const ushort Header = 0x1234;
    const ushort Footer = 0xFaaF;

    public static List<JavaClassData> Read(Stream stdIn)
    {
        List<JavaClassData> result = new();
        using (BinaryReader reader = new(stdIn, System.Text.Encoding.UTF8, leaveOpen: true))
        {
            if (reader.ReadUInt16() != Header)
                throw new InvalidDataException();

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                var name = reader.ReadString();
                int length = reader.ReadInt32();
                var data = reader.ReadBytes(length);
                result.Add(new(name, data));
            }

            if (reader.ReadUInt16() != Footer)
                throw new InvalidDataException();
        }
        return result;
    }

    public static void Write(Stream stdIn, IEnumerable<JavaClassData> classes)
    {
        using (BinaryWriter writer = new(stdIn, System.Text.Encoding.UTF8, leaveOpen: true))
        {
            writer.Write(Header);
            writer.Write(classes.Count());
            foreach (var classData in classes)
            {
                writer.Write(classData.Name);
                writer.Write(classData.Data.Length);
                writer.Write(classData.Data);
            }
            writer.Write(Footer);
        }
    }
}
