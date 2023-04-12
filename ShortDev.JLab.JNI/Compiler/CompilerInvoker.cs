using ShortDev.JLab.JNI.Internal;

namespace ShortDev.JLab.JNI.Compiler;

public sealed unsafe class CompilerInvoker
{
    readonly JNIEnv* _env;
    readonly void* _pCompiler;

    internal CompilerInvoker(JNIEnv* env, void* pCompiler)
    {
        _env = env;
        _pCompiler = pCompiler;
    }

    public void SetOptions(string[] options)
    {
        _env->functions->CallInstance(
            _env,
            "ShortDev/JLab/CompilerPipeline/Compiler/CompilerInvoker",
            _pCompiler,
            "SetOptions",
            "([Ljava/lang/String;)V",
            __arglist(
                _env->functions->CreateStringArray(_env, options)
            )
        );
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
                _env->functions->CreateJavaString(_env, id),
                _env->functions->CreateJavaString(_env, code)
            )
        );
        return new(_env, pResult);
    }
}
