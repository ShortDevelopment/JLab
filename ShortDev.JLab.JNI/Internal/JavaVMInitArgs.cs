namespace ShortDev.JLab.JNI.Internal;

internal unsafe struct JavaVMInitArgs
{
    public JniVersion version;

    public jint nOptions;
    public JavaVMOption* options;
    public jboolean ignoreUnrecognized;
}
