namespace Tesseract.Contracts;

public interface ITesseractChoiceIterator : IDisposable
{
    bool TryNext();

    string GetText();

    float GetConfidence();
}