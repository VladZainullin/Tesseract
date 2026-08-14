namespace Tesseract.Tests;

internal sealed class TesseractBoolMarshallerTests
{
    [Test]
    public async Task ConvertToManagedReturnsFalseForZero()
    {
        await Assert.That(TesseractBoolMarshaller.ConvertToManaged(0)).IsFalse();
    }

    [Test]
    public async Task ConvertToManagedReturnsTrueForAnyNonZeroValue()
    {
        await Assert.That(TesseractBoolMarshaller.ConvertToManaged(1)).IsTrue();
        await Assert.That(TesseractBoolMarshaller.ConvertToManaged(-1)).IsTrue();
    }
}
