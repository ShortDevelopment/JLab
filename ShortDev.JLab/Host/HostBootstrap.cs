using ShortDev.JLab.JNI;
using ShortDev.JLab.JNI.Compiler;
using ShortDev.Linux.SandBox;
using System.Security;

namespace ShortDev.JLab.Host;

public sealed class HostBootstrap
{
    public const string CmdArgumentName = "-host";

    readonly List<JavaClassData> _classes;
    private HostBootstrap(List<JavaClassData> classes)
        => _classes = classes;

    public static HostBootstrap InitializeHost()
        => new(JavaClassData.Read(Console.OpenStandardInput()));

    public void Run()
    {
        using var vm = JavaVirtualMachine.Create();
        foreach (var classData in _classes)
            vm.LoadClass(classData.Name, classData.Data);

        if (OperatingSystem.IsLinux())
            SandBox.EnableStrict();
        else
            throw new SecurityException("No sandbox configured for this platform");

        vm.CallMain();
    }
}
