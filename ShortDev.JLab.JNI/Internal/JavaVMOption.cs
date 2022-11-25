namespace ShortDev.JLab.JNI.Internal;

internal unsafe struct JavaVMOption
{
    public char* optionString;
    public void* extraInfo;
}
