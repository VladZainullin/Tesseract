namespace Tesseract.UnitTests;

internal sealed class TesseractChoiceIteratorTests
{
    [Test]
    public async Task ConstructorThrowsWhenHandleIsNull()
    {
        await Assert.That(() => new TesseractChoiceIterator(null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task ConstructorThrowsWhenHandleIsInvalid()
    {
        using var handle = new TesseractChoiceIteratorSafeHandle();

        await Assert.That(() => new TesseractChoiceIterator(handle))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task ConstructorThrowsWhenHandleIsClosed()
    {
        var handle = new TesseractChoiceIteratorSafeHandle();
        handle.Dispose();

        await Assert.That(() => new TesseractChoiceIterator(handle))
            .ThrowsExactly<ObjectDisposedException>();
    }
}
