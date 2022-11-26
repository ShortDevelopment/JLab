using ShortDev.JLab.JNI.Internal;
using System.Runtime.CompilerServices;

namespace ShortDev.JLab.JNI.Compiler;

public sealed unsafe class CompilerInvoker
{
    JNIEnv* _env;
    void* _pCompiler;

    internal CompilerInvoker(JNIEnv* env, void* pCompiler)
    {
        _env = env;
        _pCompiler = pCompiler;
    }

    public CompilationResult Compile(string id, string code)
    {
        var pResult = _env->functions->CallInstance(
            _env,
            "ShortDev/JLab/CompilerPipeline/Compiler/CompilerInvoker",
            _pCompiler,
            "Compile",
            "(Ljava/lang/String;Ljava/lang/String;)LShortDev/JLab/CompilerPipeline/CompilationResult;",
            __arglist(
                _env->functions->CreateString(_env, id),
                _env->functions->CreateString(_env, code)
            )
        );
        return new(_env, pResult);
    }
}
