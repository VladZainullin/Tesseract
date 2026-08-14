namespace Tesseract.Contracts;

/// <summary>
/// Specifies the structural level used when iterating over page content.
/// </summary>
public enum PageIteratorLevel
{
    /// <summary>
    /// Represents a block of page content.
    /// </summary>
    Block = 0,

    /// <summary>
    /// Represents a paragraph within a block.
    /// </summary>
    Paragraph = 1,

    /// <summary>
    /// Represents a text line within a paragraph.
    /// </summary>
    TextLine = 2,

    /// <summary>
    /// Represents a word within a text line.
    /// </summary>
    Word = 3,

    /// <summary>
    /// Represents an individual symbol within a word.
    /// </summary>
    Symbol = 4,
}