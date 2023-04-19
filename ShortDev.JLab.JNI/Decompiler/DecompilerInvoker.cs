using ShortDev.JLab.JNI.Compiler;
using ShortDev.JLab.JNI.Internal;

namespace ShortDev.JLab.JNI.Decompiler;

public sealed unsafe class DecompilerInvoker
{
    JNIEnv* _env;
    void* _pDecompiler;

    internal DecompilerInvoker(JNIEnv* env, void* pDecompiler)
    {
        _env = env;
        _pDecompiler = pDecompiler;
    }

    public string Decompile(CompilationResult compilationResult)
    {
        var result = _env->functions->CallInstance(
            _env,
            "ShortDev/JLab/CompilerPipeline/Decompiler/DecompilerInvoker",
            _pDecompiler,
            "Decompile",
            "(LShortDev/JLab/CompilerPipeline/CompilationResult;)Ljava/lang/String;",
            (IntPtr)compilationResult.Ptr
        );
        _env->functions->ThrowOnError(_env);
        return _env->functions->GetStringContent(_env, result);
    }
}
