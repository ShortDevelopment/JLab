using System.Runtime.InteropServices;
using System.Text;

namespace ShortDev.JLab.JNI.Internal;

/// <summary>
/// <see cref="https://docs.oracle.com/javase/7/docs/technotes/guides/jni/spec/functions.html"/>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct JNIEnv
{
    public JNINativeInterface_* functions;
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct JNINativeInterface_
{
    void* reserved0;
    void* reserved1;
    void* reserved2;

    void* reserved3;
    public delegate* unmanaged[Stdcall]<JNIEnv*, jint> GetVersion;

    public delegate* unmanaged[Stdcall]<JNIEnv*, char*/*name*/, void*/*loader*/, byte* /*buf*/, jint /*len*/, jint> DefineClass;
    public delegate* unmanaged[Stdcall]<JNIEnv*, char*/*name*/, void*> FindClass;

    void* FromReflectedMethod;
    void* FromReflectedField;

    void* ToReflectedMethod;

    void* GetSuperclass;
    void* IsAssignableFrom;

    void* ToReflectedField;

    void* Throw;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void* /*clazz*/, char*/*msg*/, jint> ThrowNew;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void*> ExceptionOccurred;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void> ExceptionDescribe;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void> ExceptionClear;
    public delegate* unmanaged[Stdcall]<JNIEnv*, char*, void> FatalError;

    void* PushLocalFrame;
    void* PopLocalFrame;

    void* NewGlobalRef;
    void* DeleteGlobalRef;
    void* DeleteLocalRef;
    void* IsSameObject;
    void* NewLocalRef;
    void* EnsureLocalCapacity;

    void* AllocObject;
    void* NewObject;
    void* NewObjectV;
    void* NewObjectA;

    void* GetObjectClass;
    void* IsInstanceOf;

    public delegate* unmanaged[Stdcall]<JNIEnv*, void*/*clazz*/, char*/*name*/, char*/*sig*/, void*> GetMethodID;

    void* CallObjectMethod;
    void* CallObjectMethodV;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void*/*obj*/, void*/*method*/, jvalue*, void*> CallObjectMethodA;

    void* CallBooleanMethod;
    void* CallBooleanMethodV;
    void* CallBooleanMethodA;

    void* CallByteMethod;
    void* CallByteMethodV;
    void* CallByteMethodA;

    void* CallCharMethod;
    void* CallCharMethodV;
    void* CallCharMethodA;

    void* CallShortMethod;
    void* CallShortMethodV;
    void* CallShortMethodA;

    void* CallIntMethod;
    void* CallIntMethodV;
    void* CallIntMethodA;

    void* CallLongMethod;
    void* CallLongMethodV;
    void* CallLongMethodA;

    void* CallFloatMethod;
    void* CallFloatMethodV;
    void* CallFloatMethodA;

    void* CallDoubleMethod;
    void* CallDoubleMethodV;
    void* CallDoubleMethodA;

    void* CallVoidMethod;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void*/*obj*/, void*/*method*/, ArgIterator, void> CallVoidMethodV;
    void* CallVoidMethodA;

    void* CallNonvirtualObjectMethod;
    void* CallNonvirtualObjectMethodV;

    void* CallNonvirtualObjectMethodA;


    void* CallNonvirtualBooleanMethod;
    void* CallNonvirtualBooleanMethodV;

    void* CallNonvirtualBooleanMethodA;


    void* CallNonvirtualByteMethod;
    void* CallNonvirtualByteMethodV;

    void* CallNonvirtualByteMethodA;


    void* CallNonvirtualCharMethod;
    void* CallNonvirtualCharMethodV;

    void* CallNonvirtualCharMethodA;


    void* CallNonvirtualShortMethod;
    void* CallNonvirtualShortMethodV;

    void* CallNonvirtualShortMethodA;


    void* CallNonvirtualIntMethod;
    void* CallNonvirtualIntMethodV;

    void* CallNonvirtualIntMethodA;


    void* CallNonvirtualLongMethod;
    void* CallNonvirtualLongMethodV;

    void* CallNonvirtualLongMethodA;


    void* CallNonvirtualFloatMethod;
    void* CallNonvirtualFloatMethodV;

    void* CallNonvirtualFloatMethodA;


    void* CallNonvirtualDoubleMethod;
    void* CallNonvirtualDoubleMethodV;

    void* CallNonvirtualDoubleMethodA;


    void* CallNonvirtualVoidMethod;
    void* CallNonvirtualVoidMethodV;

    void* CallNonvirtualVoidMethodA;


    void* GetFieldID;

    void* GetObjectField;
    void* GetBooleanField;
    void* GetByteField;
    void* GetCharField;
    void* GetShortField;
    void* GetIntField;
    void* GetLongField;
    void* GetFloatField;
    void* GetDoubleField;

    void* SetObjectField;
    void* SetBooleanField;
    void* SetByteField;
    void* SetCharField;
    void* SetShortField;
    void* SetIntField;
    void* SetLongField;
    void* SetFloatField;
    void* SetDoubleField;

    public delegate* unmanaged[Stdcall]<JNIEnv*, void*, char*, char*, void*> GetStaticMethodID;

    void* CallStaticObjectMethod;
    void* CallStaticObjectMethodV;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void*, void*, jvalue*, void*> CallStaticObjectMethodA;

    void* CallStaticBooleanMethod;
    void* CallStaticBooleanMethodV;
    void* CallStaticBooleanMethodA;

    void* CallStaticByteMethod;
    void* CallStaticByteMethodV;
    void* CallStaticByteMethodA;

    void* CallStaticCharMethod;
    void* CallStaticCharMethodV;
    void* CallStaticCharMethodA;

    void* CallStaticShortMethod;
    void* CallStaticShortMethodV;
    void* CallStaticShortMethodA;

    void* CallStaticIntMethod;
    void* CallStaticIntMethodV;
    void* CallStaticIntMethodA;

    void* CallStaticLongMethod;
    void* CallStaticLongMethodV;
    void* CallStaticLongMethodA;

    void* CallStaticFloatMethod;
    void* CallStaticFloatMethodV;
    void* CallStaticFloatMethodA;

    void* CallStaticDoubleMethod;
    void* CallStaticDoubleMethodV;
    void* CallStaticDoubleMethodA;

    void* CallStaticVoidMethod;
    void* CallStaticVoidMethodV;
    void* CallStaticVoidMethodA;

    void* GetStaticFieldID;
    void* GetStaticObjectField;
    void* GetStaticBooleanField;
    void* GetStaticByteField;
    void* GetStaticCharField;
    void* GetStaticShortField;
    void* GetStaticIntField;
    void* GetStaticLongField;
    void* GetStaticFloatField;
    void* GetStaticDoubleField;

    void* SetStaticObjectField;
    void* SetStaticBooleanField;
    void* SetStaticByteField;
    void* SetStaticCharField;
    void* SetStaticShortField;
    void* SetStaticIntField;
    void* SetStaticLongField;
    void* SetStaticFloatField;
    void* SetStaticDoubleField;

    public delegate* unmanaged[Stdcall]<JNIEnv*, char*, int, void*> NewString;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void*, int> GetStringLength;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void* /*str*/, ref bool /*isCopyk*/, char*> GetStringChars;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void* /*str*/, char*, void> ReleaseStringChars;

    void* NewStringUTF;
    void* GetStringUTFLength;
    void* GetStringUTFChars;
    void* ReleaseStringUTFChars;


    public delegate* unmanaged[Stdcall]<JNIEnv*, void*, int> GetArrayLength;

    public delegate* unmanaged[Stdcall]<JNIEnv*, int/*length*/, void*/*class*/, void*/*initialElement*/, void*> NewObjectArray;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void*/*pArray*/, int, void*> GetObjectArrayElement;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void*/*pArray*/, int/*index*/, void*/*value*/, void> SetObjectArrayElement;

    void* NewBooleanArray;
    void* NewByteArray;
    void* NewCharArray;
    void* NewShortArray;
    void* NewIntArray;
    void* NewLongArray;
    void* NewFloatArray;
    void* NewDoubleArray;

    void* GetBooleanArrayElements;
    public delegate* unmanaged[Stdcall]<JNIEnv*, void*/*parray*/, bool*, byte*> GetByteArrayElements;
    void* GetCharArrayElements;
    void* GetShortArrayElements;
    void* GetIntArrayElements;
    void* GetLongArrayElements;
    void* GetFloatArrayElements;
    void* GetDoubleArrayElements;

    void* ReleaseBooleanArrayElements;
    void* ReleaseByteArrayElements;
    void* ReleaseCharArrayElements;
    void* ReleaseShortArrayElements;
    void* ReleaseIntArrayElements;
    void* ReleaseLongArrayElements;
    void* ReleaseFloatArrayElements;
    void* ReleaseDoubleArrayElements;

    void* GetBooleanArrayRegion;
    void* GetByteArrayRegion;
    void* GetCharArrayRegion;
    void* GetShortArrayRegion;
    void* GetIntArrayRegion;
    void* GetLongArrayRegion;
    void* GetFloatArrayRegion;
    void* GetDoubleArrayRegion;

    void* SetBooleanArrayRegion;
    void* SetByteArrayRegion;
    void* SetCharArrayRegion;
    void* SetShortArrayRegion;
    void* SetIntArrayRegion;
    void* SetLongArrayRegion;
    void* SetFloatArrayRegion;
    void* SetDoubleArrayRegion;

    void* RegisterNatives;

    void* UnregisterNatives;

    void* MonitorEnter;
    void* MonitorExit;

    void* GetJavaVM;

    void* GetStringRegion;
    void* GetStringUTFRegion;

    void* GetPrimitiveArrayCritical;
    void* ReleasePrimitiveArrayCritical;

    void* GetStringCritical;
    void* ReleaseStringCritical;

    void* NewWeakGlobalRef;
    void* DeleteWeakGlobalRef;

    public delegate* unmanaged[Stdcall]<JNIEnv*, jboolean> ExceptionCheck;

    void* NewDirectByteBuffer;
    void* GetDirectBufferAddress;
    void* GetDirectBufferCapacity;

    /* New JNI 1.6 Features */

    void* GetObjectRefType;

    /* Module Features */

    void* GetModule;

    public void ThrowOnError(JNIEnv* env)
    {
        if (env->functions->ExceptionCheck(env))
        {
            void* throwable = ExceptionOccurred(env);
            env->functions->ExceptionDescribe(env);
            // ExceptionClear(env);
            ThrowDotNet(env, throwable);
        }
    }

    public void ThrowDotNet(JNIEnv* env, void* throwable)
    {
        if (throwable == (void*)0)
            return;

        string msg = string.Empty;
        while (throwable != (void*)0)
        {
            void* pStr = CallInstance(
                env,
                "java/lang/Throwable",
                throwable,
                "toString",
                "()Ljava/lang/String;",
                __arglist()
            );
            if (!string.IsNullOrEmpty(msg))
                msg += Environment.NewLine;
            msg += GetStringContent(env, pStr);
            throwable = CallInstance(
                env,
                "java/lang/Throwable",
                throwable,
                "getCause",
                "()Ljava/lang/Throwable;",
                __arglist()
            );
        }
        throw new Exception(msg);
    }

    public void* CallStatic(JNIEnv* env, string className, string methodName, string sig, __arglist)
    {
        ArgIterator args = new(__arglist);
        GetMethodInternal(env, className, (void*)0, methodName, sig, isStatic: true, out var pClass, out var pMethod);

        var pArgs = stackalloc jvalue[args.GetRemainingCount()];
        jvalue.Initialize(pArgs, args);
        var result = CallStaticObjectMethodA(env, pClass, pMethod, pArgs);
        ThrowOnError(env);
        return result;
    }

    public void* CallInstance(JNIEnv* env, string className, void* pObj, string methodName, string sig, __arglist)
    {
        ArgIterator args = new(__arglist);
        GetMethodInternal(env, className, (void*)0, methodName, sig, isStatic: false, out _, out var pMethod);

        var pArgs = stackalloc jvalue[args.GetRemainingCount()];
        jvalue.Initialize(pArgs, args);
        var result = CallObjectMethodA(env, pObj, pMethod, pArgs);
        ThrowOnError(env);
        return result;
    }

    void GetMethodInternal(JNIEnv* env, string className, void* pObj, string methodName, string sig, bool isStatic, out void* pClass, out void* pMethod)
    {
        fixed (byte* pClassName = className.ToUTF8())
        fixed (byte* pMethodName = methodName.ToUTF8())
        fixed (byte* pMethodSig = sig.ToUTF8())
        {
            pClass = FindClass(env, (char*)pClassName);
            ThrowOnError(env);

            if (isStatic)
                pMethod = GetStaticMethodID(env, pClass, (char*)pMethodName, (char*)pMethodSig);
            else
                pMethod = GetMethodID(env, pClass, (char*)pMethodName, (char*)pMethodSig);
            ThrowOnError(env);
        }
    }

    public void* CreateJavaString(JNIEnv* env, string value)
    {
        Span<byte> data = value.ToEncoding(Encoding.Unicode);
        fixed (byte* pValue = data)
        {
            var result = NewString(env, (char*)pValue, value.Length);
            ThrowOnError(env);
            return result;
        }
    }

    public string GetStringContent(JNIEnv* env, void* str)
    {
        var length = GetStringLength(env, str);
        ThrowOnError(env);
        bool isCopy = false;
        char* pContent = GetStringChars(env, str, ref isCopy);
        try
        {
            ThrowOnError(env);
            return Encoding.Unicode.GetString((byte*)pContent, length * 2);
        }
        finally
        {
            ReleaseStringChars(env, str, pContent);
        }
    }

    public void* CreateJavaArray<T>(JNIEnv* env, string className, T[] array, Func<T, nint> converter) where T : class
    {
        void* pClass;
        fixed (byte* pClassName = className.ToUTF8())
            pClass = FindClass(env, (char*)pClassName);
        ThrowOnError(env);

        var pArray = NewObjectArray(env, array.Length, pClass, null);
        ThrowOnError(env);
        for (int i = 0; i < array.Length; i++)
        {
            void* pEle = (void*)converter(array[i]);
            SetObjectArrayElement(env, pArray, i, pEle);
            ThrowOnError(env);
        }
        return pArray;
    }

    public void* CreateStringArray(JNIEnv* env, string[] array)
    {
        var cache = this;
        return CreateJavaArray(env, "java/lang/String", array, (value) => (nint)cache.CreateJavaString(env, value));
    }
}