#if UNITY_2021_2_OR_NEWER
using System;
using NUnit.Framework;
using Unity.Burst;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture, BurstCompile]
public class SpanTests
{
    [BurstCompile(CompileSynchronously = true)]
    private static int IndexOutOfBounds(int index)
    {
        Span<int> span = stackalloc int[4];

        return span[index];
    }

    private delegate int IndexOutOfBoundsDelegate(int index);

    [Test]
    [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor, RuntimePlatform.LinuxEditor)]
    [Description("Requires ENABLE_UNITY_COLLECTIONS_CHECKS which is currently only enabled in the Editor")]
    public void TestIndexOutOfBounds()
    {
        var funcPtr = BurstCompiler.CompileFunctionPointer<IndexOutOfBoundsDelegate>(IndexOutOfBounds);
        Assert.Throws<InvalidOperationException>(() => funcPtr.Invoke(5), "IndexOutOfRangeException: index is less than zero or greater than or equal to Length");
    }

    [BurstCompile(CompileSynchronously = true)]
    private static unsafe int ConstructorArgumentOutOfRange(int index)
    {
        var span = new Span<int>(null, index);

        return span.Length;
    }

    private delegate int ConstructorArgumentOutOfRangeDelegate(int index);

    [Test]
    [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor, RuntimePlatform.LinuxEditor)]
    [Description("Requires ENABLE_UNITY_COLLECTIONS_CHECKS which is currently only enabled in the Editor")]
    public void TestConstructorArgumentOutOfRange()
    {
        var funcPtr = BurstCompiler.CompileFunctionPointer<ConstructorArgumentOutOfRangeDelegate>(ConstructorArgumentOutOfRange);
        Assert.Throws<InvalidOperationException>(() => funcPtr.Invoke(-1), "ArgumentOutOfRangeException: length is negative");
    }

    [BurstCompile(CompileSynchronously = true)]
    private static int CopyToArgument(int start)
    {
        Span<int> from = stackalloc int[4];

        Span<int> to = stackalloc int[start];

        from.CopyTo(to);

        return from[0];
    }

    private delegate int CopyToArgumentDelegate(int index);

    [Test]
    [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor, RuntimePlatform.LinuxEditor)]
    [Description("Requires ENABLE_UNITY_COLLECTIONS_CHECKS which is currently only enabled in the Editor")]
    public void TestCopyToArgument()
    {
        var funcPtr = BurstCompiler.CompileFunctionPointer<CopyToArgumentDelegate>(CopyToArgument);
        Assert.Throws<InvalidOperationException>(() => funcPtr.Invoke(3), "ArgumentException: destination is shorter than the source");
    }

    [BurstCompile(CompileSynchronously = true)]
    private static int SliceArgument0(int index)
    {
        Span<int> span = stackalloc int[4];

        var slice = span.Slice(index);

        return slice[0];
    }

    private delegate int SliceArgument0Delegate(int index);

    [Test]
    [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor, RuntimePlatform.LinuxEditor)]
    [Description("Requires ENABLE_UNITY_COLLECTIONS_CHECKS which is currently only enabled in the Editor")]
    public void TestSliceArgument0()
    {
        var funcPtr = BurstCompiler.CompileFunctionPointer<SliceArgument0Delegate>(SliceArgument0);
        Assert.Throws<InvalidOperationException>(() => funcPtr.Invoke(-1), "ArgumentOutOfRangeException: start is less than zero or greater than Length");
        Assert.Throws<InvalidOperationException>(() => funcPtr.Invoke(5), "ArgumentOutOfRangeException: start is less than zero or greater than Length");
    }

    [BurstCompile(CompileSynchronously = true)]
    private static int SliceArgument1(int start, int length)
    {
        Span<int> span = stackalloc int[4];

        var slice = span.Slice(start, length);

        return slice[0];
    }

    private delegate int SliceArgument1Delegate(int start, int length);

    [Test]
    [UnityPlatform(RuntimePlatform.WindowsEditor, RuntimePlatform.OSXEditor, RuntimePlatform.LinuxEditor)]
    [Description("Requires ENABLE_UNITY_COLLECTIONS_CHECKS which is currently only enabled in the Editor")]
    public void TestSliceArgument1()
    {
        var funcPtr = BurstCompiler.CompileFunctionPointer<SliceArgument1Delegate>(SliceArgument1);
        Assert.Throws<InvalidOperationException>(() => funcPtr.Invoke(1, -2), "ArgumentOutOfRangeException: start + length is less than zero or greater than Length");
        Assert.Throws<InvalidOperationException>(() => funcPtr.Invoke(1, 5), "ArgumentOutOfRangeException: start + length is less than zero or greater than Length");
    }
}
#endif
