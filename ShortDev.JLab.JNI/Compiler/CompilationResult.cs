using ShortDev.JLab.JNI.Internal;

namespace ShortDev.JLab.JNI.Compiler;

public sealed unsafe class CompilationResult
{
    internal void* Ptr { get; }
    JNIEnv* _env;
    internal CompilationResult(JNIEnv* env, void* pResult)
    {
        _env = env;
        this.Ptr = pResult;
        IsSuccess = JniInterop.AsBool(env->functions->CallInstance(
            env,
            "ShortDev/JLab/CompilerPipeline/CompilationResult",
            pResult,
            "IsSuccess",
            "()Z",
            __arglist()
        ));
        var pErrorStr = env->functions->CallInstance(
            env,
            "ShortDev/JLab/CompilerPipeline/CompilationResult",
            pResult,
            "Error",
            "()Ljava/lang/String;",
            __arglist()
        );
        if (pErrorStr != null)
            Error = env->functions->GetStringContent(env, pErrorStr);

        var pDiagnosticStr = env->functions->CallInstance(
            env,
            "ShortDev/JLab/CompilerPipeline/CompilationResult",
            pResult,
            "getDiagnosticJson",
            "()Ljava/lang/String;",
            __arglist()
        );
        DiagnosticJson = env->functions->GetStringContent(env, pDiagnosticStr);
    }

    public bool IsSuccess { get; }
    public string? Error { get; }
    public string DiagnosticJson { get; }

    public JavaClassData[] GetClasses()
    {
        var pFileManager = _env->functions->CallInstance(
            _env,
            "ShortDev/JLab/CompilerPipeline/CompilationResult",
            Ptr,
            "FileManager",
            "()LShortDev/JLab/CompilerPipeline/JLabFileManager;",
            __arglist()
        );
        var pFilesArray = _env->functions->CallInstance(
            _env,
            "ShortDev/JLab/CompilerPipeline/JLabFileManager",
            pFileManager,
            "GetFiles",
            "()[LShortDev/JLab/CompilerPipeline/JavaClassMemoryFile;",
            __arglist()
        );
        var length = _env->functions->GetArrayLength(_env, pFilesArray);
        _env->functions->ThrowOnError(_env);

        var result = new JavaClassData[length];
        for (int i = 0; i < length; i++)
        {
            var pEle = _env->functions->GetObjectArrayElement(_env, pFilesArray, i);
            _env->functions->ThrowOnError(_env);
            var name = _env->functions->GetStringContent(
                _env,
                _env->functions->CallInstance(
                    _env,
                    "ShortDev/JLab/CompilerPipeline/JavaClassMemoryFile",
                    pEle,
                    "GetName",
                    "()Ljava/lang/String;",
                    __arglist()
                )
            );
            var pData = _env->functions->CallInstance(
                _env,
                "ShortDev/JLab/CompilerPipeline/JavaClassMemoryFile",
                pEle,
                "GetClassData",
                "()[B",
                __arglist()
            );
            var dataLength = _env->functions->GetArrayLength(_env, pData);
            _env->functions->ThrowOnError(_env);

            var pDataBuffer = _env->functions->GetByteArrayElements(_env, pData, null);
            _env->functions->ThrowOnError(_env);

            var data = new Span<byte>(pDataBuffer, dataLength);
            result[i] = new(name, data.ToArray());
        }
        return result;
    }
}
