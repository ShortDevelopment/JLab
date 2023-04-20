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

    readonly string[] CompilerOptions = new[] { "-Xlint" };

    public Task<JvmResult> DecompileAsync(string source)
    {
        return _jvm.RunAsync((vm) =>
        {
            string id = "Test";
            var compiler = vm.CreateCompiler();
            compiler.SetOptions(CompilerOptions);
            var result = compiler.Compile(
                id,
                source
            );
            if (!result.IsSuccess)
                return new JvmResult(result.Error ?? "Unkown error!!", result.DiagnosticJson);

            var decompiler = vm.CreateDecompiler();
            return new JvmResult(decompiler.Decompile(result), result.DiagnosticJson);
        });
    }

    public Task<JvmResult> DisassembleAsync(string source, params string[] options)
    {
        return _jvm.RunAsync((vm) =>
        {
            string id = "Test";
            var compiler = vm.CreateCompiler();
            compiler.SetOptions(CompilerOptions);
            var result = compiler.Compile(
                id,
                source
            );
            if (!result.IsSuccess)
                return new JvmResult(result.Error ?? "Unkown error!!", result.DiagnosticJson);

            var disassembler = vm.CreateDisassembler();
            disassembler.SetOptions(options);
            return new JvmResult(disassembler.Disassemble(result), result.DiagnosticJson);
        });
    }

    public async Task<JvmResult> RunAsync(string source)
    {
        JavaClassData[]? classes = null;
        string? diagnosticsJson = null;
        var resultStr = await _jvm.RunAsync((vm) =>
        {
            string id = "Test";
            var compiler = vm.CreateCompiler();
            compiler.SetOptions(CompilerOptions);
            var result = compiler.Compile(
                id,
                source
            );

            diagnosticsJson = result.DiagnosticJson;
            if (!result.IsSuccess)
                return result.Error ?? "Unkown error!!";

            classes = result.GetClasses();
            return string.Empty;
        });
        if (!string.IsNullOrEmpty(resultStr) || classes == null)
            return new(resultStr, diagnosticsJson);

        HostLauncher launcher = new();
        launcher.AddClasses(classes);
        return new(await launcher.LaunchForResultAsync(), diagnosticsJson);
    }

    public void Dispose()
        => _jvm.Dispose();
}
