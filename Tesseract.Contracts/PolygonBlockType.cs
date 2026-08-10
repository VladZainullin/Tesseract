namespace Tesseract.Contracts;

public enum PolygonBlockType
{
    Unknown = 0,
    FlowingText = 1,
    HeadingText = 2,
    PulloutText = 3,
    Equation = 4,
    InlineEquation = 5,
    Table = 6,
    VerticalText = 7,
    CaptionText = 8,
    FlowingImage = 9,
    HeadingImage = 10,
    PulloutImage = 11,
    HorizontalLine = 12,
    VerticalLine = 13,
    Noise = 14,
    Count = 15
}