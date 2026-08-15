namespace Tesseract.Contracts;

public interface ITesseractResultRenderer : IHasSafeHandle, IDisposable
{
    string Extension { get; }
    
    string Title { get; }
    
    int ImageNumbers { get; }

    ITesseractResultRenderer NextRenderer();
    
    void Insert(ITesseractResultRenderer renderer);

    bool TryBeginDocument(string title);

    bool TryAddImage(ITesseractEngine engine);
    
    bool TryEndDocument();
}