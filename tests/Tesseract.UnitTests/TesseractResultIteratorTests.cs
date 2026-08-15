namespace Tesseract.UnitTests;

internal sealed class TesseractResultIteratorTests
{
    [Test]
    public async Task ConstructorThrowsWhenHandleIsNull()
    {
        await Assert.That(() => new TesseractResultIterator(null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ConstructorThrowsWhenHandleIsInvalid()
    {
        using var handle = new TesseractResultIteratorSafeHandle();

        await Assert.That(() => new TesseractResultIterator(handle))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ConstructorThrowsWhenHandleIsClosed()
    {
        var handle = new TesseractResultIteratorSafeHandle();
        handle.Dispose();

        await Assert.That(() => new TesseractResultIterator(handle))
            .ThrowsExactly<ObjectDisposedException>();
    }
}
