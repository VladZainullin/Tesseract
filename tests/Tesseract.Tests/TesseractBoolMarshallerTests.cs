namespace Tesseract.Tests;

internal sealed class TesseractBoolMarshallerTests
{
    [Test]
    [Arguments(0)]
    public async Task ConvertToManagedReturnsFalse(int nativeValue)
    {
        var result = TesseractBoolMarshaller.ConvertToManaged(nativeValue);
        
        await Assert.That(result).IsFalse();
    }

    [Test]
    [Arguments(1)]
    [Arguments(-1)]
    [Arguments(int.MaxValue)]
    [Arguments(int.MinValue)]
    public async Task ConvertToManagedReturnsTrue(int nativeValue)
    {
        var result = TesseractBoolMarshaller.ConvertToManaged(nativeValue);

        await Assert.That(result).IsTrue();
    }
}
