namespace Tesseract.Contracts;

/// <summary>
/// Specifies the OCR engine mode used by Tesseract.
/// </summary>
public enum OcrEngineMode
{
    /// <summary>
    /// Uses the legacy Tesseract OCR engine only.
    /// </summary>
    TesseractOnly = 0,

    /// <summary>
    /// Uses the LSTM neural network OCR engine only.
    /// </summary>
    LstmOnly = 1,

    /// <summary>
    /// Uses both the legacy Tesseract and LSTM OCR engines.
    /// </summary>
    TesseractLstmCombined = 2,

    /// <summary>
    /// Lets Tesseract select the OCR engine mode automatically
    /// based on the available language data and configuration.
    /// </summary>
    Default = 3,
}