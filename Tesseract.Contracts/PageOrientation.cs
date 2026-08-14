namespace Tesseract.Contracts;

/// <summary>
/// Specifies the orientation of a page detected by Tesseract.
/// </summary>
public enum PageOrientation
{
    /// <summary>
    /// The page is upright and requires no rotation.
    /// </summary>
    Up = 0,

    /// <summary>
    /// The page is rotated 90 degrees clockwise from the upright orientation.
    /// </summary>
    Right = 1,

    /// <summary>
    /// The page is rotated 180 degrees from the upright orientation.
    /// </summary>
    Down = 2,

    /// <summary>
    /// The page is rotated 90 degrees counterclockwise from the upright orientation.
    /// </summary>
    Left = 3,
}