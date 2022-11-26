using ShortDev.JLab.JNI.Internal;
using ShortDev.JLab.JNI.Internal.Platform;

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
        string jarLocation = Path.Combine(AppContext.BaseDirectory, "ShortDev.JLab.CompilerPipeline.jar");
        fixed (byte* pOption1 = $"-Djava.class.path={jarLocation};".ToUTF8()) // java.library.path
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

    public Compiler.CompilerInvoker CreateCompiler()
    {
        var pCompiler = _env->functions->CallStatic(
            _env,
            "ShortDev/JLab/CompilerPipeline/Compiler/CompilerInvoker",
            "Create",
            "()LShortDev/JLab/CompilerPipeline/Compiler/CompilerInvoker;",
            __arglist()
        );
        return new(_env, pCompiler);
    }

    public Decompiler.DecompilerInvoker CreateDecompiler()
    {
        var pDecompiler = _env->functions->CallStatic(
            _env,
            "ShortDev/JLab/CompilerPipeline/Decompiler/DecompilerInvoker",
            "Create",
            "()LShortDev/JLab/CompilerPipeline/Decompiler/DecompilerInvoker;",
            __arglist()
        );
        return new(_env, pDecompiler);
    }

    public Task<T> RunAsync<T>(Func<JavaVirtualMachine, T> func)
    {
        return Task.Run(() =>
        {
            JNIEnv* env;
            JniInterop.ThrowOnError(_vm->functions->AttachCurrentThread(_vm, &env, (void*)0));
            try
            {
                return func(new(_vm, env));
            }
            finally
            {
                // ToDo: What happens if "DestroyJavaVM" has been called already?!
                JniInterop.ThrowOnError(_vm->functions->DetachCurrentThread(_vm));
            }
        });
    }

    public Task RunAsync(Action<JavaVirtualMachine> func)
        => RunAsync<byte>((vm) => { func(vm); return 0b0; });

    public void Dispose()
        => _vm->functions->DestroyJavaVM(_vm);
}