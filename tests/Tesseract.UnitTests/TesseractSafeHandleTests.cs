namespace Tesseract.UnitTests;

internal sealed class TesseractSafeHandleTests
{
    [Test]
    public async Task DefaultHandlesAreInvalid()
    {
        using var engine = new TesseractEngineSafeHandle();
        using var pageIterator = new TesseractPageIteratorSafeHandle();
        using var resultIterator = new TesseractResultIteratorSafeHandle();
        using var choiceIterator = new TesseractChoiceIteratorSafeHandle();
        using var renderer = new TesseractResultRendererSafeHandle();
        using var monitor = new TesseractMonitorSafeHandle();
        using var text = new TesseractStringSafeHandle();

        await Assert.That(engine.IsInvalid).IsTrue();
        await Assert.That(pageIterator.IsInvalid).IsTrue();
        await Assert.That(resultIterator.IsInvalid).IsTrue();
        await Assert.That(choiceIterator.IsInvalid).IsTrue();
        await Assert.That(renderer.IsInvalid).IsTrue();
        await Assert.That(monitor.IsInvalid).IsTrue();
        await Assert.That(text.IsInvalid).IsTrue();
    }

    [Test]
    public async Task DisposingDefaultHandleClosesIt()
    {
        var handle = new TesseractEngineSafeHandle();

        handle.Dispose();

        await Assert.That(handle.IsClosed).IsTrue();
    }
}
