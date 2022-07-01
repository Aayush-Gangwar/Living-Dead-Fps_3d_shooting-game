using System;
using System.Runtime.InteropServices;
using AOT;
using NUnit.Framework;
using Unity.Burst;
using UnityEngine;
using UnityEngine.TestTools;
#if UNITY_2021_2_OR_NEWER
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
#endif

[TestFixture, BurstCompile]
public class FunctionPointerTests
{
    [BurstCompile(new[] { BurstCompilerOptions.DoNotEagerCompile }, CompileSynchronously = true)]
    private static T StaticFunctionNoArgsGenericReturnType<T>()
    {
        return default;
    }

    private delegate int DelegateNoArgsIntReturnType();

    [Test]
    public void TestCompileFunctionPointerNoArgsGenericReturnType()
    {
        Assert.Throws<InvalidOperationException>(
            () => BurstCompiler.CompileFunctionPointer<DelegateNoArgsIntReturnType>(StaticFunctionNoArgsGenericReturnType<int>),
            "The method `Int32 StaticFunctionNoArgsGenericReturnType[Int32]()` must be a non-generic method");
    }

#if UNITY_2019_4_OR_NEWER
    private unsafe delegate void ExceptionDelegate(int* a);

    [BurstCompile(CompileSynchronously = true)]
    [MonoPInvokeCallback(typeof(ExceptionDelegate))]
    private static unsafe void DereferenceNull(int* a)
    {
        *a = 42;
    }

    [Test]
    [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor, RuntimePlatform.LinuxEditor)]
    [Description("Requires ENABLE_UNITY_COLLECTIONS_CHECKS which is currently only enabled in the Editor")]
    public unsafe void TestDereferenceNull()
    {
        var funcPtr = BurstCompiler.CompileFunctionPointer<ExceptionDelegate>(DereferenceNull);
        Assert.Throws<InvalidOperationException>(
            () => funcPtr.Invoke(null),
            "NullReferenceException: Object reference not set to an instance of an object");
    }

    [BurstCompile(CompileSynchronously = true)]
    [MonoPInvokeCallback(typeof(ExceptionDelegate))]
    private static unsafe void DivideByZero(int* a)
    {
        *a = 42 / *a;
    }

    [Test]
    [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor, RuntimePlatform.LinuxEditor)]
    [Description("Requires ENABLE_UNITY_COLLECTIONS_CHECKS which is currently only enabled in the Editor")]
    public unsafe void TestDivideByZero()
    {
        if (RuntimeInformation.OSArchitecture == Architecture.Arm64)
        {
            // Arm64 does not throw a divide-by-zero exception, instead it flushes the result to zero.
            return;
        }

        var funcPtr = BurstCompiler.CompileFunctionPointer<ExceptionDelegate>(DivideByZero);
        var i = stackalloc int[1];
        *i = 0;
        Assert.Throws<InvalidOperationException>(
            () => funcPtr.Invoke(i),
            "DivideByZeroException: Attempted to divide by zero");
    }

    private unsafe delegate void ParentDelegate(IntPtr ptr, int* a);

    [BurstCompile(CompileSynchronously = true)]
    [MonoPInvokeCallback(typeof(ParentDelegate))]
    private static unsafe void Parent(IntPtr ptr, int* a)
    {
        new FunctionPointer<ExceptionDelegate>(ptr).Invoke(a);
    }

    [Test]
    [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor, RuntimePlatform.LinuxEditor)]
    [Description("Requires ENABLE_UNITY_COLLECTIONS_CHECKS which is currently only enabled in the Editor")]
    public unsafe void TestSubFunctionPointerFails()
    {
        var parentFuncPtr = BurstCompiler.CompileFunctionPointer<ParentDelegate>(Parent);
        var funcPtr = BurstCompiler.CompileFunctionPointer<ExceptionDelegate>(DereferenceNull);
        Assert.Throws<InvalidOperationException>(
            () => parentFuncPtr.Invoke(funcPtr.Value, null),
            "NullReferenceException: Object reference not set to an instance of an object");
    }

    [Test]
    [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor, RuntimePlatform.LinuxEditor)]
    [Description("Requires ENABLE_UNITY_COLLECTIONS_CHECKS which is currently only enabled in the Editor")]
    public unsafe void TestSubFunctionPointerIsNullFails()
    {
        var parentFuncPtr = BurstCompiler.CompileFunctionPointer<ParentDelegate>(Parent);
        var funcPtr = new FunctionPointer<ExceptionDelegate>((IntPtr)0);
        Assert.Throws<InvalidOperationException>(
            () => parentFuncPtr.Invoke(funcPtr.Value, null),
            "NullReferenceException: Object reference not set to an instance of an object");
    }

    private static unsafe void ManagedDereferenceNull(int* a)
    {
        *a = 42;
    }

    [Test]
    [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor, RuntimePlatform.LinuxEditor)]
    [Description("Requires ENABLE_UNITY_COLLECTIONS_CHECKS which is currently only enabled in the Editor")]
    public unsafe void TestManagedSubFunctionPointerFails()
    {
        var parentFuncPtr = BurstCompiler.CompileFunctionPointer<ParentDelegate>(Parent);
        ExceptionDelegate del = ManagedDereferenceNull;
        var funcPtr = new FunctionPointer<ExceptionDelegate>(Marshal.GetFunctionPointerForDelegate(del));
        Assert.Throws<NullReferenceException>(
            () => parentFuncPtr.Invoke(funcPtr.Value, null),
            "Object reference not set to an instance of an object");
    }
#endif

// Doesn't work with IL2CPP yet - waiting for Unity fix to land.
#if false // UNITY_2021_2_OR_NEWER
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    [BurstCompile]
    private static int CSharpFunctionPointerCallback(int value) => value * 2;

    [BurstCompile(CompileSynchronously = true)]
    public unsafe struct StructWithCSharpFunctionPointer : IJob
    {
        [NativeDisableUnsafePtrRestriction]
        [ReadOnly]
        public IntPtr Callback;

        [ReadOnly]
        public NativeArray<int> Input;

        [WriteOnly]
        public NativeArray<int> Output;

        public void Execute()
        {
            delegate* unmanaged[Cdecl]<int, int> callback = (delegate* unmanaged[Cdecl]<int, int>)Callback;
            Output[0] = callback(Input[0]);
        }
    }

    [Test]
    public unsafe void CSharpFunctionPointerInsideJobStructTest()
    {
        using (var input = new NativeArray<int>(new int[1] { 40 }, Allocator.Persistent))
        using (var output = new NativeArray<int>(new int[1], Allocator.Persistent))
        {
            delegate* unmanaged[Cdecl]<int, int> callback = &CSharpFunctionPointerCallback;

            var job = new StructWithCSharpFunctionPointer
            {
                Callback = (IntPtr)callback,
                Input = input,
                Output = output
            };

            job.Run();

            Assert.AreEqual(40 * 2, output[0]);
        }
    }

    [Test]
    public unsafe void CSharpFunctionPointerInStaticMethodSignature()
    {
        var fp = BurstCompiler.CompileFunctionPointer<DelegateWithCSharpFunctionPointerParameter>(EntryPointWithCSharpFunctionPointerParameter);
        delegate* unmanaged[Cdecl]<int, int> callback = &CSharpFunctionPointerCallback;

        var result = fp.Invoke((IntPtr)callback);

        Assert.AreEqual(10, result);
    }

    [BurstCompile(CompileSynchronously = true)]
    private static unsafe int EntryPointWithCSharpFunctionPointerParameter(IntPtr callback)
    {
        delegate* unmanaged[Cdecl]<int, int> typedCallback = (delegate* unmanaged[Cdecl]<int, int>)callback;
        return typedCallback(5);
    }

    private unsafe delegate int DelegateWithCSharpFunctionPointerParameter(IntPtr callback);
#endif

    [Test]
    public void TestDelegateWithCustomAttributeThatIsNotUnmanagedFunctionPointerAttribute()
    {
        var fp = BurstCompiler.CompileFunctionPointer<TestDelegateWithCustomAttributeThatIsNotUnmanagedFunctionPointerAttributeDelegate>(TestDelegateWithCustomAttributeThatIsNotUnmanagedFunctionPointerAttributeHelper);

        var result = fp.Invoke(42);

        Assert.AreEqual(43, result);
    }

    [BurstCompile(CompileSynchronously = true)]
    private static int TestDelegateWithCustomAttributeThatIsNotUnmanagedFunctionPointerAttributeHelper(int x) => x + 1;

    [MyCustomAttribute("Foo")]
    private delegate int TestDelegateWithCustomAttributeThatIsNotUnmanagedFunctionPointerAttributeDelegate(int x);

    private sealed class MyCustomAttributeAttribute : Attribute
    {
        public MyCustomAttributeAttribute(string param) { }
    }
}

#if UNITY_2021_2_OR_NEWER
// UnmanagedCallersOnlyAttribute is new in .NET 5.0. This attribute is required
// when you declare an unmanaged function pointer with an explicit calling convention.
// Fortunately, Roslyn lets us declare the attribute class ourselves, and it will be used.
// Users will need this same declaration in their own projects, in order to use
// C# 9.0 function pointers.
namespace System.Runtime.InteropServices
{
    [AttributeUsage(System.AttributeTargets.Method, Inherited = false)]
    public sealed class UnmanagedCallersOnlyAttribute : Attribute
    {
        public Type[] CallConvs;
    }
}
#endif