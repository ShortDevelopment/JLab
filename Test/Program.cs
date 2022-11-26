using ShortDev.JLab.JNI;

JavaVirtualMachine.SetupPlatform();
using (JavaVirtualMachine vm = JavaVirtualMachine.Create())
{
    await vm.RunAsync((vm) =>
    {
        Console.WriteLine(vm.Version);
        var compiler = vm.CreateCompiler();
        compiler.Compile(
            "Test2",
            "public record Test2(int abc){}"
        );
        var decompiler = vm.CreateDecompiler();
        var result = decompiler.Decompile("Test2");
        Console.WriteLine(result);
    });
}
