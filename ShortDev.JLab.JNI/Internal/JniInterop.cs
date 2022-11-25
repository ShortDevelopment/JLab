using System.Runtime.InteropServices;

namespace ShortDev.JLab.JNI.Internal;

internal static unsafe class JniInterop
{
    const string DllName = "C:\\Program Files\\Microsoft\\jdk-17.0.3.7-hotspot\\bin\\server\\jvm.dll";

    [DllImport(DllName, EntryPoint = "JNI_GetDefaultJavaVMInitArgs")]
    public static extern jint GetDefaultJavaVMInitArgs(ref JavaVMInitArgs args);

    [DllImport(DllName, EntryPoint = "JNI_CreateJavaVM")]
    public static extern jint CreateJavaVM(JavaVM** pvm, JNIEnv** penv, JavaVMInitArgs* args);

    public static void ThrowOnError(jint status)
    {
        if (status.Value != 0)
            throw new Exception();
    }
}
