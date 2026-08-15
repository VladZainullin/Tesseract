using Tesseract.Contracts;

namespace Tesseract.IntegrationTests;

internal sealed class TesseractEngineIntegrationTests
{
    [Test]
    public async Task NativeEngineHasVersionAndValidHandle()
    {
        TesseractTestEnvironment.Configure();

        using var engine = new TesseractEngine();

        await Assert.That(TesseractEngine.Version).IsNotNullOrEmpty();
        await Assert.That(engine.Handle.IsInvalid).IsFalse();
        await Assert.That(engine.Handle.IsClosed).IsFalse();
    }

    [Test]
    public async Task InitializeLoadsEnglishLanguageData()
    {
        var dataPath = TesseractTestEnvironment.Configure();
        using var engine = new TesseractEngine();

        var initialized = engine.TryInitialize(dataPath, "eng", OcrEngineMode.LstmOnly);

        await Assert.That(initialized).IsTrue();
        await Assert.That(engine.GetInitializationLanguages()).IsEqualTo("eng");
        await Assert.That(engine.GetLoadedLanguages()).Contains("eng");
        await Assert.That(engine.GetAvailableLanguages()).Contains("eng");
        await Assert.That(engine.GetDataPath()).IsNotNullOrEmpty();
    }

    [Test]
    public async Task EngineVariablesAndSegmentationModeRoundTrip()
    {
        var dataPath = TesseractTestEnvironment.Configure();
        using var engine = new TesseractEngine();
        await Assert.That(engine.TryInitialize(dataPath, "eng")).IsTrue();

        engine.SetSegmentationMode(PageSegmentationMode.SingleLine);
        engine.SetInputName("generated-test-image");
        var variableSet = engine.TrySetVariable("tessedit_char_whitelist", "TEST");

        await Assert.That(engine.PageSegmentationMode).IsEqualTo(PageSegmentationMode.SingleLine);
        await Assert.That(engine.InputName).IsEqualTo("generated-test-image");
        await Assert.That(variableSet).IsTrue();
        await Assert.That(engine.GetVariable("tessedit_char_whitelist")).IsEqualTo("TEST");
    }

    [Test]
    public async Task RecognizeProcessesManagedImageBuffer()
    {
        var dataPath = TesseractTestEnvironment.Configure();
        using var engine = new TesseractEngine();
        await Assert.That(engine.TryInitialize(dataPath, "eng", OcrEngineMode.LstmOnly)).IsTrue();

        engine.SetSegmentationMode(PageSegmentationMode.SingleLine);
        engine.SetImage(TestImage.Create(), TestImage.Width, TestImage.Height, 1);
        engine.SetSourceResolution(300);

        using var monitor = new TesseractMonitor();
        var recognized = engine.TryRecognize(monitor);

        await Assert.That(recognized).IsTrue();
        await Assert.That(monitor.Progress).IsGreaterThanOrEqualTo(0);
        await Assert.That(monitor.Progress).IsLessThanOrEqualTo(100);
        await Assert.That(engine.Text).IsNotNull();
        await Assert.That(engine.MeanTextConfidence).IsGreaterThanOrEqualTo(0);
        await Assert.That(engine.MeanTextConfidence).IsLessThanOrEqualTo(100);
        await Assert.That(engine.GetHOcrText(0)).Contains("ocr_page");
        await Assert.That(engine.GetTsvText(0)).Contains("TEST");
    }

    [Test]
    public async Task ResultIteratorExposesRecognizedWordMetadata()
    {
        var dataPath = TesseractTestEnvironment.Configure();
        using var engine = new TesseractEngine();
        await Assert.That(engine.TryInitialize(dataPath, "eng", OcrEngineMode.LstmOnly)).IsTrue();
        engine.SetSegmentationMode(PageSegmentationMode.SingleLine);
        engine.SetImage(TestImage.Create(), TestImage.Width, TestImage.Height, 1);
        using var monitor = new TesseractMonitor();
        await Assert.That(engine.TryRecognize(monitor)).IsTrue();

        using var iterator = engine.GetIterator();
        iterator.Begin();

        await Assert.That(iterator.GetText(PageIteratorLevel.Word)).IsEqualTo("TEST");
        await Assert.That(iterator.WordRecognitionLanguage()).IsEqualTo("eng");
        await Assert.That(iterator.TryGetBoundingBox(
            PageIteratorLevel.Word, out var left, out var top, out var right, out var bottom)).IsTrue();
        await Assert.That(right).IsGreaterThan(left);
        await Assert.That(bottom).IsGreaterThan(top);

        _ = iterator.GetWordFontAttributes(
            out _, out _, out _, out _, out _, out _, out var pointSize, out _);
        await Assert.That(pointSize).IsGreaterThanOrEqualTo(0);

        using var copy = iterator.Copy();
        await Assert.That(copy.Handle.IsInvalid).IsFalse();
    }

    [Test]
    public async Task ChoiceIteratorsKeepDisposedResultIteratorAlive()
    {
        var dataPath = TesseractTestEnvironment.Configure();
        using var engine = new TesseractEngine();
        await Assert.That(engine.TryInitialize(dataPath, "eng", OcrEngineMode.LstmOnly)).IsTrue();
        engine.SetSegmentationMode(PageSegmentationMode.SingleLine);
        engine.SetImage(TestImage.Create(), TestImage.Width, TestImage.Height, 1);
        using var monitor = new TesseractMonitor();
        await Assert.That(engine.TryRecognize(monitor)).IsTrue();

        var resultIterator = engine.GetIterator();
        resultIterator.Begin();
        using var firstChoiceIterator = resultIterator.GetChoiceIterator();
        using var secondChoiceIterator = resultIterator.GetChoiceIterator();

        resultIterator.Dispose();

        await Assert.That(firstChoiceIterator.GetText()).IsNotNullOrEmpty();
        await Assert.That(secondChoiceIterator.GetText()).IsNotNullOrEmpty();
    }

    [Test]
    public async Task ResultIteratorKeepsDisposedEngineAlive()
    {
        var dataPath = TesseractTestEnvironment.Configure();
        var engine = new TesseractEngine();
        await Assert.That(engine.TryInitialize(dataPath, "eng", OcrEngineMode.LstmOnly)).IsTrue();
        engine.SetSegmentationMode(PageSegmentationMode.SingleLine);
        engine.SetImage(TestImage.Create(), TestImage.Width, TestImage.Height, 1);
        using var monitor = new TesseractMonitor();
        await Assert.That(engine.TryRecognize(monitor)).IsTrue();

        using var iterator = engine.GetIterator();
        engine.Dispose();
        iterator.Begin();

        await Assert.That(iterator.GetText(PageIteratorLevel.Word)).IsEqualTo("TEST");
    }

}
