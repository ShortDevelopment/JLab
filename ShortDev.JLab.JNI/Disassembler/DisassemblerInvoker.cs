using ShortDev.JLab.JNI.Compiler;
using ShortDev.JLab.JNI.Internal;

namespace ShortDev.JLab.JNI.Disassembler;

public sealed unsafe class DisassemblerInvoker
{
    JNIEnv* _env;
    void* _pDisassembler;

    internal DisassemblerInvoker(JNIEnv* env, void* pDisassembler)
    {
        _env = env;
        _pDisassembler = pDisassembler;
    }

    public string Disassemble(CompilationResult compilationResult)
    {
        var result = _env->functions->CallInstance(
            _env,
            "ShortDev/JLab/CompilerPipeline/Disassembler/DisassemblerInvoker",
            _pDisassembler,
            "Disassemble",
            "(LShortDev/JLab/CompilerPipeline/CompilationResult;)Ljava/lang/String;",
            __arglist(
                compilationResult.ptr
            )
        );
        _env->functions->ThrowOnError(_env);
        return _env->functions->GetStringContent(_env, result);
    }
}
