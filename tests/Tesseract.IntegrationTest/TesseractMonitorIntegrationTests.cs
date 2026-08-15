namespace Tesseract.IntegrationTests;

internal sealed class TesseractMonitorIntegrationTests
{
    [Test]
    public async Task ConstructorCreatesValidHandle()
    {
        TesseractTestEnvironment.Configure();

        using var monitor = new TesseractMonitor();

        await Assert.That(monitor.Handle.IsInvalid).IsFalse();
        await Assert.That(monitor.Handle.IsClosed).IsFalse();
        await Assert.That(monitor.Progress).IsGreaterThanOrEqualTo(0);
    }

    [Test]
    public async Task SetDeadlineRejectsNegativeValue()
    {
        TesseractTestEnvironment.Configure();
        using var monitor = new TesseractMonitor();

        await Assert.That(() => monitor.SetDeadline(-1)).ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task DisposeClosesHandle()
    {
        TesseractTestEnvironment.Configure();
        var monitor = new TesseractMonitor();
        var handle = monitor.Handle;

        monitor.Dispose();

        await Assert.That(handle.IsClosed).IsTrue();
    }
}
