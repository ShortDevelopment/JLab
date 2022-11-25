namespace ShortDev.JLab.JNI.Internal;

internal unsafe struct JavaVM
{
    public JNIInvokeInterface_* functions;
}

internal unsafe struct JNIInvokeInterface_
{
    void* reserved0;
    void* reserved1;
    void* reserved2;

    public delegate* unmanaged[Stdcall]<JavaVM*, jint> DestroyJavaVM;
    // ...
}