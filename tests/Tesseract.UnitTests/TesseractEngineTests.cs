using System.Runtime.Serialization;
using Leptonica.Contracts;
using Tesseract.Contracts;

namespace Tesseract.UnitTests;

internal sealed class TesseractEngineTests
{
    private readonly TesseractEngine _engine = CreateWithoutNativeHandle();

    [Test]
    public async Task TrySetVariableThrowsWhenNameIsNull()
    {
        await Assert.That(() => _engine.TrySetVariable(null!, "value"))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task TrySetVariableThrowsWhenValueIsNull()
    {
        await Assert.That(() => _engine.TrySetVariable("name", null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task TrySetDebugVariableThrowsWhenNameIsEmpty()
    {
        await Assert.That(() => _engine.TrySetDebugVariable(string.Empty, "value"))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task TrySetDebugVariableThrowsWhenValueIsNull()
    {
        await Assert.That(() => _engine.TrySetDebugVariable("name", null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    public async Task GetVariableThrowsWhenNameIsMissing(string? name)
    {
        await Assert.That(() => _engine.GetVariable(name!)).Throws<ArgumentException>();
    }

    [Test]
    public async Task SetInputImageThrowsWhenImageIsNull()
    {
        await Assert.That(() => _engine.SetInputImage((IPix)null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task SetImageThrowsWhenImageIsNull()
    {
        await Assert.That(() => _engine.SetImage((IPix)null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task SetImageThrowsWhenImageDataIsNull()
    {
        await Assert.That(() => _engine.SetImage(null!, 1, 1, 1))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task TryRecognizeThrowsWhenMonitorIsNull()
    {
        await Assert.That(() => _engine.TryRecognize((ITesseractMonitor)null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    public async Task RendererFactoriesThrowWhenOutputNameIsMissing(string? outputName)
    {
        await Assert.That(() => _engine.CreateAltoRenderer(outputName!)).Throws<ArgumentException>();
        await Assert.That(() => _engine.CreateTsvRenderer(outputName!)).Throws<ArgumentException>();
        await Assert.That(() => _engine.CreateUnlvRenderer(outputName!)).Throws<ArgumentException>();
        await Assert.That(() => _engine.CreateBoxTextRenderer(outputName!)).Throws<ArgumentException>();
        await Assert.That(() => _engine.CreateWordStrBoxRenderer(outputName!)).Throws<ArgumentException>();
        await Assert.That(() => _engine.CreateLstmBoxRenderer(outputName!)).Throws<ArgumentException>();
    }

    [Test]
    public async Task CreatePdfRendererThrowsWhenDataDirectoryIsEmpty()
    {
        await Assert.That(() => _engine.CreatePdfRenderer("output", string.Empty, false))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    public async Task TryInitializeThrowsWhenDataPathIsMissing(string? dataPath)
    {
        await Assert.That(() => _engine.TryInitialize(dataPath!, "eng", OcrEngineMode.LstmOnly))
            .Throws<ArgumentException>();
    }

#pragma warning disable SYSLIB0050
    private static TesseractEngine CreateWithoutNativeHandle() =>
        (TesseractEngine)FormatterServices.GetUninitializedObject(typeof(TesseractEngine));
#pragma warning restore SYSLIB0050
}
