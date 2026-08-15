using Tesseract.Contracts;

namespace Tesseract.IntegrationTests;

internal sealed class TesseractRendererIntegrationTests
{
    [Test]
    public async Task TextRendererWritesRecognizedPage()
    {
        var dataPath = TesseractTestEnvironment.Configure();
        var outputBase = Path.Combine(Path.GetTempPath(), $"tesseract-{Guid.NewGuid():N}");
        var outputPath = outputBase + ".txt";

        try
        {
            using var engine = new TesseractEngine();
            await Assert.That(engine.TryInitialize(dataPath, "eng", OcrEngineMode.LstmOnly)).IsTrue();
            engine.SetSegmentationMode(PageSegmentationMode.SingleLine);
            engine.SetImage(TestImage.Create(), TestImage.Width, TestImage.Height, 1);

            using var renderer = engine.TextRendererCreate(outputBase);

            await Assert.That(renderer.GetExtension()).IsEqualTo("txt");
            await Assert.That(renderer.TryBeginDocument("integration-test")).IsTrue();
            await Assert.That(renderer.GetTitle()).IsEqualTo("integration-test");
            await Assert.That(renderer.TryAddImage(engine)).IsTrue();
            await Assert.That(renderer.GetImageNumber()).IsEqualTo(0);
            await Assert.That(renderer.TryEndDocument()).IsTrue();
            await Assert.That(File.Exists(outputPath)).IsTrue();
            var renderedText = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);
            await Assert.That(renderedText).IsNotNull();
        }
        finally
        {
            if (File.Exists(outputPath))
                File.Delete(outputPath);
        }
    }
}
