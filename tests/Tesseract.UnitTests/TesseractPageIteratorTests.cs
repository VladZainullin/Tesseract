namespace Tesseract.UnitTests;

internal sealed class TesseractPageIteratorTests
{
    [Test]
    [Arguments(null)]
    public async Task ConstructorThrowsWhenHandleIsNull(
        TesseractPageIteratorSafeHandle? handle)
    {
        await Assert.That(() => new TesseractPageIterator(handle!)).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ConstructorThrowsWhenHandleIsInvalid()
    {
        using var handle = new TesseractPageIteratorSafeHandle();

        await Assert.That(() => new TesseractPageIterator(handle)).ThrowsExactly<ArgumentException>();
    }

    [Test]
    [Arguments(true)]
    public async Task ConstructorThrowsWhenHandleIsClosed(
        bool expectedIsClosed)
    {
        var handle = new TesseractPageIteratorSafeHandle();

        handle.Dispose();

        await Assert.That(handle.IsClosed).IsEqualTo(expectedIsClosed);
        await Assert.That(() => new TesseractPageIterator(handle)).ThrowsExactly<ObjectDisposedException>();
    }
}