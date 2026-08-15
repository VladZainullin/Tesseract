using Leptonica.Contracts;

namespace Tesseract.Contracts;

/// <summary>
/// Represents a native Tesseract OCR engine and provides configuration, recognition,
/// layout-analysis, iteration, and result-rendering operations.
/// </summary>
/// <remarks>
/// An engine instance owns native resources and must be disposed. Initialize it before processing images,
/// and do not use the same instance concurrently from multiple threads. Objects returned by iterator,
/// image, and renderer factory methods also own native resources unless their documentation states otherwise.
/// </remarks>
public interface ITesseractEngine : IHasSafeHandle, IDisposable
{
    /// <summary>
    /// Gets the version of the loaded native Tesseract library.
    /// </summary>
    /// <value>The native version string, or <see langword="null"/> when it is unavailable.</value>
    static abstract string? Version { get; }

    /// <summary>
    /// Gets the page segmentation mode currently configured for the engine.
    /// </summary>
    PageSegmentationMode PageSegmentationMode { get; }

    /// <summary>
    /// Gets the name associated with the current input image.
    /// </summary>
    /// <value>The input name, or <see langword="null"/> when no name has been assigned.</value>
    string? InputName { get; }

    /// <summary>
    /// Gets the recognized page text encoded as a managed UTF-16 string.
    /// </summary>
    /// <value>The recognized UTF-8 text converted to a managed string, or <see langword="null"/> on failure.</value>
    /// <remarks>Recognition is performed automatically if required.</remarks>
    string? Text { get; }

    /// <summary>
    /// Gets the mean confidence of the recognized text.
    /// </summary>
    /// <value>A value conventionally ranging from <c>0</c> to <c>100</c>.</value>
    int MeanTextConfidence { get; }

    /// <summary>
    /// Attempts to set a Tesseract configuration variable.
    /// </summary>
    /// <param name="name">The variable name.</param>
    /// <param name="value">The value represented as text.</param>
    /// <returns><see langword="true"/> if the variable was accepted; otherwise, <see langword="false"/>.</returns>
    bool TrySetVariable(string name, string value);

    /// <summary>
    /// Attempts to set a Tesseract debug variable.
    /// </summary>
    /// <param name="name">The debug variable name.</param>
    /// <param name="value">The value represented as text.</param>
    /// <returns><see langword="true"/> if the variable was accepted; otherwise, <see langword="false"/>.</returns>
    bool TrySetDebugVariable(string name, string value);

    /// <summary>
    /// Associates an original Leptonica image with the engine as its input image.
    /// </summary>
    /// <param name="pix">The source image.</param>
    void SetInputImage(IPix pix);

    /// <summary>
    /// Assigns a descriptive name to the current input image.
    /// </summary>
    /// <param name="name">The input name, such as a source filename.</param>
    void SetInputName(string name);

    /// <summary>
    /// Gets the textual value of a Tesseract variable.
    /// </summary>
    /// <param name="name">The variable name.</param>
    /// <returns>The variable value, or <see langword="null"/> if the variable does not exist.</returns>
    string? GetVariable(string name);

    /// <summary>Attempts to get an integer Tesseract variable.</summary>
    /// <param name="name">The variable name.</param>
    /// <param name="value">The value when found; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if the variable exists and has the requested type.</returns>
    bool TryGetVariable(string name, out int? value);

    /// <summary>Attempts to get a floating-point Tesseract variable.</summary>
    /// <param name="name">The variable name.</param>
    /// <param name="value">The value when found; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if the variable exists and has the requested type.</returns>
    bool TryGetVariable(string name, out double? value);

    /// <summary>Attempts to get a Boolean Tesseract variable.</summary>
    /// <param name="name">The variable name.</param>
    /// <param name="value">The value when found; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if the variable exists and has the requested type.</returns>
    bool TryGetVariable(string name, out bool? value);

    /// <summary>Creates a plain-text result renderer.</summary>
    /// <param name="outputName">The output path without the renderer-specific extension.</param>
    /// <returns>A renderer owned by the caller.</returns>
    ITesseractResultRenderer TextRendererCreate(string outputName);

    /// <summary>Creates an hOCR result renderer.</summary>
    /// <param name="outputName">The output path without the renderer-specific extension.</param>
    /// <returns>A renderer owned by the caller.</returns>
    ITesseractResultRenderer HOcrRendererCreate(string outputName);

    /// <summary>Creates an hOCR result renderer with optional font metadata.</summary>
    /// <param name="outputName">The output path without the renderer-specific extension.</param>
    /// <param name="fontInfo"><see langword="true"/> to include font information in the hOCR output.</param>
    /// <returns>A renderer owned by the caller.</returns>
    ITesseractResultRenderer HOcrRendererCreate(string outputName, bool fontInfo);

    /// <summary>Creates an ALTO XML result renderer.</summary>
    /// <param name="outputName">The output path without the renderer-specific extension.</param>
    /// <returns>A renderer owned by the caller.</returns>
    ITesseractResultRenderer CreateAltoRenderer(string outputName);

    /// <summary>Creates a TSV result renderer.</summary>
    /// <param name="outputName">The output path without the renderer-specific extension.</param>
    /// <returns>A renderer owned by the caller.</returns>
    ITesseractResultRenderer CreateTsvRenderer(string outputName);

    /// <summary>Creates a searchable or text-only PDF result renderer.</summary>
    /// <param name="outputName">The output path without the <c>.pdf</c> extension.</param>
    /// <param name="dataDir">The directory containing Tesseract data required by the PDF renderer.</param>
    /// <param name="textOnly"><see langword="true"/> to omit the source image and render only the text layer.</param>
    /// <returns>A renderer owned by the caller.</returns>
    ITesseractResultRenderer CreatePdfRenderer(string outputName, string dataDir, bool textOnly);

    /// <summary>
    /// Gets the languages loaded by the current engine initialization.
    /// </summary>
    /// <returns>A read-only list of language identifiers.</returns>
    IReadOnlyList<string> GetLoadedLanguages();

    /// <summary>Creates a UNLV result renderer.</summary>
    /// <param name="outputName">The output path without the renderer-specific extension.</param>
    /// <returns>A renderer owned by the caller.</returns>
    ITesseractResultRenderer CreateUnlvRenderer(string outputName);

    /// <summary>Creates a legacy box-text result renderer.</summary>
    /// <param name="outputName">The output path without the renderer-specific extension.</param>
    /// <returns>A renderer owned by the caller.</returns>
    ITesseractResultRenderer CreateBoxTextRenderer(string outputName);

    /// <summary>Creates a word-string box result renderer.</summary>
    /// <param name="outputName">The output path without the renderer-specific extension.</param>
    /// <returns>A renderer owned by the caller.</returns>
    ITesseractResultRenderer CreateWordStrBoxRenderer(string outputName);

    /// <summary>Creates an LSTM box result renderer.</summary>
    /// <param name="outputName">The output path without the renderer-specific extension.</param>
    /// <returns>A renderer owned by the caller.</returns>
    ITesseractResultRenderer CreateLstmBoxRenderer(string outputName);

    /// <summary>Gets recognition results formatted as hOCR.</summary>
    /// <param name="pageNumber">The zero-based page number embedded in the generated output.</param>
    /// <returns>The hOCR document, or <see langword="null"/> on failure.</returns>
    string? GetHOcrText(int pageNumber);

    /// <summary>Gets recognition results formatted as ALTO XML.</summary>
    /// <param name="pageNumber">The zero-based page number embedded in the generated output.</param>
    /// <returns>The ALTO XML document, or <see langword="null"/> on failure.</returns>
    string? GetAltoText(int pageNumber);

    /// <summary>Gets recognition results formatted as tab-separated values.</summary>
    /// <param name="pageNumber">The zero-based page number embedded in the generated output.</param>
    /// <returns>The TSV content, or <see langword="null"/> on failure.</returns>
    string? GetTsvText(int pageNumber);

    /// <summary>Gets LSTM training box output for the recognition results.</summary>
    /// <param name="pageNumber">The zero-based page number embedded in the generated output.</param>
    /// <returns>The LSTM box content, or <see langword="null"/> on failure.</returns>
    string? GetLstmText(int pageNumber);

    /// <summary>Gets legacy box output for the recognition results.</summary>
    /// <param name="pageNumber">The zero-based page number embedded in the generated output.</param>
    /// <returns>The box content, or <see langword="null"/> on failure.</returns>
    string? GetBoxText(int pageNumber);

    /// <summary>Gets the UTF-8 representation of a Tesseract unichar identifier.</summary>
    /// <param name="uniCharId">The internal unichar identifier.</param>
    /// <returns>The corresponding character, or <see langword="null"/> when the identifier is unknown.</returns>
    string? GetUniChar(int uniCharId);

    /// <summary>Sets the page segmentation mode used for subsequent recognition.</summary>
    /// <param name="mode">The segmentation strategy.</param>
    void SetSegmentationMode(PageSegmentationMode mode);

    /// <summary>Initializes the engine using Tesseract's default OCR engine mode.</summary>
    /// <param name="dataPath">The directory containing trained data files.</param>
    /// <param name="language">A language identifier or a plus-separated language list, such as <c>eng+deu</c>.</param>
    /// <returns><see langword="true"/> when initialization succeeds; otherwise, <see langword="false"/>.</returns>
    bool TryInitialize(string dataPath, string language);

    /// <summary>Initializes the engine using a specific OCR engine mode.</summary>
    /// <param name="dataPath">The directory containing trained data files.</param>
    /// <param name="language">A language identifier or a plus-separated language list, such as <c>eng+deu</c>.</param>
    /// <param name="oem">The OCR engine mode.</param>
    /// <returns><see langword="true"/> when initialization succeeds; otherwise, <see langword="false"/>.</returns>
    bool TryInitialize(string dataPath, string language, OcrEngineMode oem);

    /// <summary>Gets the source image's vertical resolution.</summary>
    /// <returns>The vertical resolution in pixels per inch.</returns>
    int GetSourceYResolution();

    /// <summary>Sets the source image resolution used for font-size calculations.</summary>
    /// <param name="ppi">The resolution in pixels per inch.</param>
    /// <remarks>Call this method after setting the image.</remarks>
    void SetSourceResolution(int ppi);

    /// <summary>Sets a Leptonica image as the image to recognize.</summary>
    /// <param name="image">The image. Tesseract takes its own native copy.</param>
    void SetImage(IPix image);

    /// <summary>Sets a tightly packed managed image buffer as the image to recognize.</summary>
    /// <param name="imageData">The image pixels.</param>
    /// <param name="width">The image width in pixels.</param>
    /// <param name="height">The image height in pixels.</param>
    /// <param name="bytesPerPixel">The number of bytes per pixel, typically <c>1</c>, <c>3</c>, or <c>4</c>.</param>
    /// <remarks>The implementation calculates the row stride as <c>width * bytesPerPixel</c>.</remarks>
    void SetImage(byte[] imageData, int width, int height, int bytesPerPixel);

    /// <summary>Performs recognition for the current image.</summary>
    /// <param name="monitor">The progress and deadline monitor, which must remain alive until this method returns.</param>
    /// <returns><see langword="true"/> when recognition completes successfully; otherwise, <see langword="false"/>.</returns>
    bool TryRecognize(ITesseractMonitor monitor);

    /// <summary>Restricts subsequent recognition to a rectangular region of the current image.</summary>
    /// <param name="left">The left coordinate in pixels.</param>
    /// <param name="top">The top coordinate in pixels.</param>
    /// <param name="width">The rectangle width in pixels.</param>
    /// <param name="height">The rectangle height in pixels.</param>
    void SetRectangle(int left, int top, int width, int height);

    /// <summary>Gets the language expression used to initialize the engine.</summary>
    /// <returns>The initialization language or plus-separated language list.</returns>
    string GetInitializationLanguages();

    /// <summary>Creates an iterator over the current recognition results.</summary>
    /// <returns>A result iterator owned by the caller.</returns>
    /// <remarks>
    /// Dispose the iterator before clearing, reinitializing, or disposing the engine, and do not use it after
    /// recognition state changes.
    /// </remarks>
    ITesseractResultIterator GetIterator();

    /// <summary>Analyzes the current page layout without returning recognized text.</summary>
    /// <returns>A page iterator owned by the caller.</returns>
    /// <remarks>Dispose the iterator before changing the engine's image or recognition state.</remarks>
    ITesseractPageIterator AnalyzeLayout();

    /// <summary>Attempts to determine the text-line direction for the current page.</summary>
    /// <param name="outOffset">The intercept of the fitted text baseline.</param>
    /// <param name="slope">The slope of the fitted text baseline.</param>
    /// <returns><see langword="true"/> when a direction can be determined; otherwise, <see langword="false"/>.</returns>
    bool TryGetTextDirection(out int outOffset, out float slope);

    /// <summary>Sets the minimum confidence margin used by orientation detection.</summary>
    /// <param name="margin">The minimum orientation margin.</param>
    void SetMinimumOrientationMargin(double margin);

    /// <summary>Ends the native engine session and releases recognition-specific resources.</summary>
    /// <remarks>The engine must be initialized again before it processes another image.</remarks>
    void EndElement();

    /// <summary>Clears the current page image and recognition results while preserving engine initialization.</summary>
    void Clear();

    /// <summary>Determines whether a word is valid according to the loaded language dictionaries.</summary>
    /// <param name="word">The word to validate.</param>
    /// <returns><see langword="true"/> when the word is valid; otherwise, <see langword="false"/>.</returns>
    bool IsValidWord(string word);

    /// <summary>Gets a copy of the internally thresholded image for the current page.</summary>
    /// <returns>A Leptonica image owned by the caller.</returns>
    IPix GetThresholdedImage();
}
