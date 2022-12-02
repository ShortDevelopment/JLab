using ShortDev.JLab.JNI;
using ShortDev.JLab.JNI.Compiler;
using System.Diagnostics;

namespace ShortDev.JLab.Host;

public sealed class HostBootstrap
{
    public const string CmdArgumentName = "-host";

    private List<JavaClassData> _classes;
    private HostBootstrap(List<JavaClassData> classes)
        => _classes = classes;

    public static HostBootstrap InitializeHost()
        => new(JavaClassData.Read(Console.OpenStandardInput()));

    public void Run()
    {
        using (var vm = JavaVirtualMachine.Create())
        {
            foreach (var classData in _classes)
                vm.LoadClass(classData.Name, classData.Data);
            vm.CallMain();
        }
    }
}
