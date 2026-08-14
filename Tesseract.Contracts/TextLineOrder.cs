namespace Tesseract.Contracts;

/// <summary>
/// Specifies the reading order of text lines on a page.
/// </summary>
public enum TextLineOrder
{
    /// <summary>
    /// Text lines are ordered from left to right.
    /// </summary>
    LeftToRight = 0,

    /// <summary>
    /// Text lines are ordered from right to left.
    /// </summary>
    RightToLeft = 1,

    /// <summary>
    /// Text lines are ordered from top to bottom.
    /// </summary>
    TopToBottom = 2,
}