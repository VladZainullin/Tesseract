using System.Runtime.Serialization;
using Tesseract.Contracts;

namespace Tesseract.UnitTests;

internal sealed class TesseractResultRendererTests
{
    [Test]
    public async Task ConstructorThrowsWhenHandleIsNull()
    {
        await Assert.That(() => new TesseractResultRenderer(null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ConstructorThrowsWhenHandleIsInvalid()
    {
        using var handle = new TesseractResultRendererSafeHandle();

        await Assert.That(() => new TesseractResultRenderer(handle))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ConstructorThrowsWhenHandleIsClosed()
    {
        var handle = new TesseractResultRendererSafeHandle();
        handle.Dispose();

        await Assert.That(() => new TesseractResultRenderer(handle))
            .ThrowsExactly<ObjectDisposedException>();
    }

    [Test]
    public async Task InsertThrowsWhenRendererIsNull()
    {
        var renderer = CreateWithoutNativeHandle();

        await Assert.That(() => renderer.Insert(null!)).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task InsertThrowsWhenRendererIsItself()
    {
        var renderer = CreateWithoutNativeHandle();

        await Assert.That(() => renderer.Insert(renderer)).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task TryAddImageThrowsWhenEngineIsNull()
    {
        var renderer = CreateWithoutNativeHandle();

        await Assert.That(() => renderer.TryAddImage((ITesseractEngine)null!))
            .ThrowsExactly<ArgumentNullException>();
    }

#pragma warning disable SYSLIB0050
    private static TesseractResultRenderer CreateWithoutNativeHandle() =>
        (TesseractResultRenderer)FormatterServices.GetUninitializedObject(typeof(TesseractResultRenderer));
#pragma warning restore SYSLIB0050
}
