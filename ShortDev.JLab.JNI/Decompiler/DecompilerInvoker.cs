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

    public string Decompile(string id)
    {
        var result = _env->functions->CallInstance(
            _env,
            "ShortDev/JLab/CompilerPipeline/Decompiler/DecompilerInvoker",
            _pDecompiler,
            "Decompile",
            "(Ljava/lang/String;)Ljava/lang/String;",
            __arglist(
                _env->functions->CreateString(_env, id)
            )
        );
        _env->functions->ThrowOnError(_env);
        return _env->functions->GetStringContent(_env, result);
    }
}
