using ShortDev.JLab.JNI.Internal;

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

    public void Compile(string id, string code)
    {
        var result = _env->functions->CallInstance(
            _env,
            "ShortDev/JLab/CompilerPipeline/Compiler/CompilerInvoker",
            _pCompiler,
            "Compile",
            "(Ljava/lang/String;Ljava/lang/String;)V",
            __arglist(
                _env->functions->CreateString(_env, id),
                _env->functions->CreateString(_env, code)
            )
        );
    }
}
