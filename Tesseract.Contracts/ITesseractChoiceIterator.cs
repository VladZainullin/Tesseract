namespace Tesseract.Contracts;

public interface ITesseractChoiceIterator
{
    bool TryNext();

    string GetText();

    float GetConfidence();
}