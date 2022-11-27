using ShortDev.JLab.JNI;

namespace ShortDev.JLab.Services;

public sealed class JvmService : IDisposable
{
    readonly JavaVirtualMachine _jvm;
    private JvmService(JavaVirtualMachine jvm)
        => _jvm = jvm;

    public static JvmService Create()
    {
        JavaVirtualMachine.SetupPlatform();
        return new(JavaVirtualMachine.Create());
    }

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

    public void Dispose()
        => _jvm.Dispose();
}
