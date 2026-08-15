namespace Tesseract.Tests;

internal sealed class TesseractBoolMarshallerTests
{
    [Test]
    [Arguments(0, false)]
    [Arguments(1, true)]
    [Arguments(-1, true)]
    [Arguments(int.MaxValue, true)]
    [Arguments(int.MinValue, true)]
    public async Task ConvertToManagedReturnsExpectedResult(int nativeValue, bool expected)
    {
        var result = TesseractBoolMarshaller.ConvertToManaged(nativeValue);
        
        await Assert.That(result).IsEqualTo(expected);
    }
}
