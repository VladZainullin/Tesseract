namespace Tesseract.Contracts;

/// <summary>
/// Specifies the justification of a paragraph detected by Tesseract.
/// </summary>
public enum ParagraphJustification
{
    /// <summary>
    /// The paragraph justification could not be determined.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The paragraph is left-aligned.
    /// </summary>
    Left = 1,

    /// <summary>
    /// The paragraph is center-aligned.
    /// </summary>
    Center = 2,

    /// <summary>
    /// The paragraph is right-aligned.
    /// </summary>
    Right = 3,

    /// <summary>
    /// The paragraph uses fractional justification.
    /// </summary>
    Fractional = 4,
}