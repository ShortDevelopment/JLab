using ShortDev.JLab.JNI.Internal;

namespace ShortDev.JLab.JNI.Compiler;

public sealed unsafe class CompilationResult
{
    internal void* ptr { get; }
    internal CompilationResult(JNIEnv* env, void* pResult)
    {
        ptr = pResult;
        IsSuccess = JniInterop.AsBool(env->functions->CallInstance(
            env,
            "ShortDev/JLab/CompilerPipeline/CompilationResult",
            pResult,
            "IsSuccess",
            "()Z",
            __arglist()
        ));
        var pStr = env->functions->CallInstance(
            env,
            "ShortDev/JLab/CompilerPipeline/CompilationResult",
            pResult,
            "Error",
            "()Ljava/lang/String;",
            __arglist()
        );
        if (pStr != null)
            Error = env->functions->GetStringContent(env, pStr);
    }

    public bool IsSuccess { get; }
    public string? Error { get; }
}
