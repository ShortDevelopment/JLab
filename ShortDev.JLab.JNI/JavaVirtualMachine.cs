using ShortDev.JLab.JNI.Internal;
using ShortDev.JLab.JNI.Internal.Platform;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace ShortDev.JLab.JNI;

public sealed unsafe class JavaVirtualMachine : IDisposable
{
    JavaVM* _vm;
    JNIEnv* _env;
    private JavaVirtualMachine(JavaVM* vm, JNIEnv* env)
    {
        _vm = vm;
        _env = env;
    }

    public static void SetupPlatform()
    {
        if (!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException();
        Win32Setup.Setup();
    }

    public static JavaVirtualMachine Create()
    {
        fixed (byte* pOption1 = @"-Djava.class.path=./ShortDev.JLab.CompilerPipeline.jar;".ToUTF8()) // java.library.path
        {
            JavaVM* jvm;
            JNIEnv* env;
            JavaVMInitArgs vmArgs = new();
            vmArgs.version = JniVersion.JNI_VERSION_1_6;
            vmArgs.nOptions = 1;
            JavaVMOption options = new();
            options.optionString = (char*)pOption1;
            vmArgs.options = &options;
            vmArgs.ignoreUnrecognized = false;
            JniInterop.ThrowOnError(JniInterop.CreateJavaVM(&jvm, &env, &vmArgs));
            env->functions->ThrowOnError(env);
            return new(jvm, env);
        }
    }

    public int Version
        => _env->functions->GetVersion(_env).Value;

    public void Throw(string classId, string msg)
    {
        fixed (byte* pClassId = classId.ToUTF8())
        fixed (byte* pMsg = msg.ToUTF8())
        {
            var @class = _env->functions->FindClass(_env, (char*)pClassId);
            if (@class == (void*)0)
                throw new FileNotFoundException($"Could not find class \"{classId}\"");
            JniInterop.ThrowOnError(_env->functions->ThrowNew(_env, @class, (char*)pMsg));
        }
    }

    public void LoadClass(string name, byte[] data)
    {
        fixed (byte* pName = name.ToUTF8())
        fixed (byte* pData = data)
            _env->functions->DefineClass(_env, (char*)pName, (void*)0, pData, data.Length);
        _env->functions->ThrowOnError(_env);
    }

    public void CreateCompiler()
    {
        var pCompiler = _env->functions->CallStatic(
            _env,
            "ShortDev/JLab/CompilerPipeline/CompilerInvoker",            
            "Create",
            "()LShortDev/JLab/CompilerPipeline/CompilerInvoker;",
            __arglist()
        );
        var result = _env->functions->CallInstance(
            _env,
            "ShortDev/JLab/CompilerPipeline/CompilerInvoker",
            pCompiler,
            "Compile",
            "(Ljava/lang/String;Ljava/lang/String;)V",
            __arglist(_env->functions->CreateString(_env, "Test"), _env->functions->CreateString(_env, "public class Test{}"))
        );
    }

    public void Dispose()
        => _vm->functions->DestroyJavaVM(_vm);
}