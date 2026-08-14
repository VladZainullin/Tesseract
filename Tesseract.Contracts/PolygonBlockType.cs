namespace Tesseract.Contracts;

/// <summary>
/// Specifies the type of polygonal block detected on a page.
/// </summary>
public enum PolygonBlockType
{
    /// <summary>
    /// The block type is unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// A flowing text block containing regular body text.
    /// </summary>
    FlowingText = 1,

    /// <summary>
    /// A text block representing a heading.
    /// </summary>
    HeadingText = 2,

    /// <summary>
    /// A pull-out text block, such as a sidebar or highlighted text.
    /// </summary>
    PulloutText = 3,

    /// <summary>
    /// A block containing an equation.
    /// </summary>
    Equation = 4,

    /// <summary>
    /// An equation embedded within a line of text.
    /// </summary>
    InlineEquation = 5,

    /// <summary>
    /// A block containing tabular content.
    /// </summary>
    Table = 6,

    /// <summary>
    /// A block containing vertically oriented text.
    /// </summary>
    VerticalText = 7,

    /// <summary>
    /// A text block representing a caption.
    /// </summary>
    CaptionText = 8,

    /// <summary>
    /// An image block embedded within flowing content.
    /// </summary>
    FlowingImage = 9,

    /// <summary>
    /// An image block associated with a heading.
    /// </summary>
    HeadingImage = 10,

    /// <summary>
    /// A pull-out image block, such as an illustration or standalone image.
    /// </summary>
    PulloutImage = 11,

    /// <summary>
    /// A horizontal line.
    /// </summary>
    HorizontalLine = 12,

    /// <summary>
    /// A vertical line.
    /// </summary>
    VerticalLine = 13,

    /// <summary>
    /// A block classified as noise.
    /// </summary>
    Noise = 14,

    /// <summary>
    /// Represents the number of defined polygon block types.
    /// This value is not an actual block type.
    /// </summary>
    Count = 15,
}