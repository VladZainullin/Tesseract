namespace Tesseract.Tests;

internal sealed class TesseractEngineTests
{
    [Before(Class)]
    public static void Setup()
    {
        Environment.SetEnvironmentVariable("TESSERACT_LIBRARY_PATH", "/opt/homebrew/opt/tesseract/lib/libtesseract.5.dylib");
    }
    
    [Test]
    public async Task ConstructorCreatesValidHandle()
    {
        using var engine = new TesseractEngine();

        await Assert.That(engine.Handle.IsInvalid).IsFalse();
        await Assert.That(engine.Handle.IsClosed).IsFalse();
    }
    
    [Test]
    public async Task DisposeClosesHandle()
    {
        var engine = new TesseractEngine();
        var handle = engine.Handle;

        engine.Dispose();

        await Assert.That(handle.IsClosed).IsTrue();
    }
    
    [Test]
    public async Task VersionReturnsNonEmptyString()
    {
        var version = TesseractEngine.Version;

        await Assert.That(version).IsNotNull();
        await Assert.That(version.Length).IsGreaterThan(0);
    }
    
    [Test]
    public async Task DisposeCanBeCalledTwice()
    {
        var engine = new TesseractEngine();

        engine.Dispose();
        engine.Dispose();

        await Assert.That(engine.Handle.IsClosed).IsTrue();
    }
}