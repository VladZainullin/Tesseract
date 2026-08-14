namespace Tesseract.Contracts;

/// <summary>
/// Specifies the writing direction of text detected by Tesseract.
/// </summary>
public enum WritingDirection
{
    /// <summary>
    /// Text is written from left to right.
    /// </summary>
    LeftToRight = 0,

    /// <summary>
    /// Text is written from right to left.
    /// </summary>
    RightToLeft = 1,

    /// <summary>
    /// Text is written from top to bottom.
    /// </summary>
    TopToBottom = 2,
}