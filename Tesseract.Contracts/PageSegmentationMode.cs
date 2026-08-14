namespace Tesseract.Contracts;

/// <summary>
/// Specifies how Tesseract segments an image into blocks, lines, words, and characters.
/// </summary>
public enum PageSegmentationMode
{
    /// <summary>
    /// Performs orientation and script detection (OSD) only.
    /// </summary>
    OsdOnly = 0,

    /// <summary>
    /// Performs automatic page segmentation with orientation and script detection.
    /// </summary>
    AutoOsd = 1,

    /// <summary>
    /// Performs automatic page segmentation without orientation and script detection (OSD) or OCR.
    /// </summary>
    AutoOnly = 2,

    /// <summary>
    /// Performs fully automatic page segmentation without orientation and script detection (OSD).
    /// </summary>
    Auto = 3,

    /// <summary>
    /// Assumes a single column of text of variable sizes.
    /// </summary>
    SingleColumn = 4,

    /// <summary>
    /// Assumes a single uniform block of vertically aligned text.
    /// </summary>
    SingleBlockVerticalText = 5,

    /// <summary>
    /// Assumes a single uniform block of text.
    /// </summary>
    SingleBlock = 6,

    /// <summary>
    /// Treats the image as a single text line.
    /// </summary>
    SingleLine = 7,

    /// <summary>
    /// Treats the image as a single word.
    /// </summary>
    SingleWord = 8,

    /// <summary>
    /// Treats the image as a single word arranged in a circle.
    /// </summary>
    CircleWord = 9,

    /// <summary>
    /// Treats the image as a single character.
    /// </summary>
    SingleCharacter = 10,

    /// <summary>
    /// Finds as much text as possible without assuming a particular order.
    /// </summary>
    SparseText = 11,

    /// <summary>
    /// Sparse text mode with orientation and script detection.
    /// </summary>
    SparseTextOsd = 12,

    /// <summary>
    /// Treats the image as a single text line, bypassing Tesseract-specific line processing.
    /// </summary>
    RawLine = 13,
}