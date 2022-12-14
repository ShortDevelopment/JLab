using ShortDev.JLab.Host;
using ShortDev.JLab.JNI;
using ShortDev.JLab.JNI.Compiler;

namespace ShortDev.JLab.Services;

public sealed class JvmService : IDisposable
{
    readonly JavaVirtualMachine _jvm;
    private JvmService(JavaVirtualMachine jvm)
        => _jvm = jvm;

    public static JvmService Create()
        => new(JavaVirtualMachine.Create());

    public Task<string> DecompileAsync(string source)
    {
        return _jvm.RunAsync((vm) =>
        {
            string id = "Test";
            var compiler = vm.CreateCompiler();
            var result = compiler.Compile(
                id,
                source
            );
            if (!result.IsSuccess)
                return result.Error ?? "Unkown error!!";

            var decompiler = vm.CreateDecompiler();
            return decompiler.Decompile(result);
        });
    }

    public Task<string> DisassembleAsync(string source, params string[] options)
    {
        return _jvm.RunAsync((vm) =>
        {
            string id = "Test";
            var compiler = vm.CreateCompiler();
            var result = compiler.Compile(
                id,
                source
            );
            if (!result.IsSuccess)
                return result.Error ?? "Unkown error!!";

            var disassembler = vm.CreateDisassembler();
            disassembler.SetOptions(options);
            return disassembler.Disassemble(result);
        });
    }

    public async Task<string> RunAsync(string source)
    {
        JavaClassData[]? classes = null;
        var resultStr = await _jvm.RunAsync((vm) =>
        {
            string id = "Test";
            var compiler = vm.CreateCompiler();
            var result = compiler.Compile(
                id,
                source
            );
            if (!result.IsSuccess)
                return result.Error ?? "Unkown error!!";

            classes = result.GetClasses();
            return string.Empty;
        });
        if (!string.IsNullOrEmpty(resultStr) || classes == null)
            return resultStr;

        HostLauncher launcher = new();
        launcher.AddClasses(classes);
        return await launcher.LaunchForResultAsync();
    }

    public void Dispose()
        => _jvm.Dispose();
}
