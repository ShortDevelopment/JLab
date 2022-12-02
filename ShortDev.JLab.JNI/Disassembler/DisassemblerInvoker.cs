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

    public void SetOptions(string[] options)
    {
        _env->functions->CallInstance(
            _env,
            "ShortDev/JLab/CompilerPipeline/Disassembler/DisassemblerInvoker",
            _pDisassembler,
            "SetFlag",
            "([Ljava/lang/String;)V",
            __arglist(
                _env->functions->CreateStringArray(_env, options)
            )
        );
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
                compilationResult.Ptr
            )
        );
        return _env->functions->GetStringContent(_env, result);
    }
}
