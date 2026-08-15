using Leptonica.Contracts;

namespace Tesseract.Contracts;

public interface ITesseractEngine : IHasSafeHandle
{
    static abstract string Version { get; }
    
    PageSegmentationMode PageSegmentationMode { get; }
    string? InputName { get; }
    
    string? Text { get; }
    int MeanTextConfidence { get; }
    bool TrySetVariable(string name, string value);
    bool TrySetDebugVariable(string name, string value);
    void SetInputImage(IPix pix);
    void SetInputName(string name);
    string? GetVariable(string name);
    bool TryGetVariable(string name, out int? value);
    bool TryGetVariable(string name, out double? value);
    bool TryGetVariable(string name, out bool? value);

    ITesseractResultRenderer TextRendererCreate(string outputName);

    ITesseractResultRenderer HOcrRendererCreate(string outputName);

    ITesseractResultRenderer HOcrRendererCreate(string outputName, bool fontInfo);

    ITesseractResultRenderer CreateAltoRenderer(string outputName);

    ITesseractResultRenderer CreateTsvRenderer(string outputName);

    ITesseractResultRenderer CreatePdfRenderer(string outputName, string dataDir, bool textOnly);

    IReadOnlyList<string> GetLoadedLanguages();

    ITesseractResultRenderer CreateUnlvRenderer(string outputName);

    ITesseractResultRenderer CreateBoxTextRenderer(string outputName);

    ITesseractResultRenderer CreateWordStrBoxRenderer(string outputName);

    ITesseractResultRenderer CreateLstmBoxRenderer(string outputName);
    string? GetHOcrText(int pageNumber);
    string? GetAltoText(int pageNumber);
    string? GetTsvText(int pageNumber);
    string? GetLstmText(int pageNumber);
    string? GetBoxText(int pageNumber);
    string? GetUniChar(int uniCharId);
    void SetSegmentationMode(PageSegmentationMode mode);
    bool TryInitialize(string dataPath, string language);
    bool TryInitialize(string dataPath, string language, OcrEngineMode oem);
    int GetSourceYResolution();
    void SetSourceResolution(int ppi);
    void SetImage(IPix image);
    void SetImage(byte[] imageData, int width, int height, int bytesPerPixel);
    bool TryRecognize(ITesseractMonitor monitor);
    void SetRectangle(int left, int top, int width, int height);
    string GetInitializationLanguages();
    ITesseractResultIterator GetIterator();
    ITesseractPageIterator AnalyzeLayout();
    bool TryGetTextDirection(out int outOffset, out float slope);
    void SetMinimumOrientationMargin(double margin);
    void EndElement();
    void Clear();
    bool IsValidWord(string word);
    IPix GetThresholdedImage();
}
