using Leptonica.Contracts;

namespace Tesseract.Contracts;

/// <summary>
/// Provides hierarchical traversal and layout information for the blocks, paragraphs,
/// text lines, words, and symbols detected on a page.
/// </summary>
/// <remarks>
/// The iterator owns native resources and must be disposed. It depends on the engine state from which it was
/// created and must not be used after that state is cleared, reinitialized, or disposed. Unless otherwise noted,
/// coordinates are expressed in pixels relative to the original input image.
/// </remarks>
public interface ITesseractPageIterator : IDisposable, IHasSafeHandle
{
    /// <summary>
    /// Creates a binary image containing the current page element at the specified hierarchy level.
    /// </summary>
    /// <param name="level">The hierarchy level of the element to extract.</param>
    /// <returns>A new image owned by the caller.</returns>
    /// <remarks>
    /// The returned image uses Tesseract's internal binary-image scale, which may differ from the scale of the
    /// original input image.
    /// </remarks>
    IPix GetBinaryImage(PageIteratorLevel level);

    /// <summary>
    /// Creates an image containing the current page element at the specified hierarchy level.
    /// </summary>
    /// <param name="level">The hierarchy level of the element to extract.</param>
    /// <param name="padding">The number of pixels to add around the element.</param>
    /// <param name="originalImage">The original image from which the page element is copied.</param>
    /// <param name="left">The horizontal position of the returned image in the original image.</param>
    /// <param name="top">The vertical position of the returned image in the original image.</param>
    /// <returns>A new image owned by the caller.</returns>
    IPix GetImage(PageIteratorLevel level, int padding, IPix originalImage, out int left, out int top);

    /// <summary>
    /// Gets layout information for the paragraph containing the current iterator position.
    /// </summary>
    /// <param name="justification">The detected paragraph justification.</param>
    /// <param name="isListItem"><see langword="true"/> if the paragraph is a list item.</param>
    /// <param name="isCrown">
    /// <see langword="true"/> if the paragraph is the first paragraph of a text continuation whose first line is
    /// aligned with the following text lines.
    /// </param>
    /// <param name="firstLineIndent">The first-line indentation in pixels.</param>
    void GetParagraphInfo(
        out ParagraphJustification justification, out bool isListItem, out bool isCrown, out int firstLineIndent);

    /// <summary>Gets the detected orientation and reading direction of the current page.</summary>
    /// <param name="pageOrientation">The clockwise rotation required to make the page upright.</param>
    /// <param name="writingDirection">The direction in which symbols are written within a word.</param>
    /// <param name="textLineOrder">The order in which text lines are read.</param>
    /// <param name="deskewAngle">
    /// The clockwise rotation, in radians, required to make the text lines horizontal.
    /// </param>
    void GetOrientation(out PageOrientation pageOrientation, out WritingDirection writingDirection,
        out TextLineOrder textLineOrder, out float deskewAngle);

    /// <summary>Moves the iterator to the first page element.</summary>
    void Begin();

    /// <summary>Moves the iterator to the next page element at the specified hierarchy level.</summary>
    /// <param name="level">The hierarchy level at which to advance.</param>
    /// <returns><see langword="true"/> if the iterator moved to another element; otherwise, <see langword="false"/>.</returns>
    bool TryNext(PageIteratorLevel level);

    /// <summary>Determines whether the iterator is at the beginning of an element at the specified level.</summary>
    /// <param name="level">The hierarchy level to examine.</param>
    /// <returns><see langword="true"/> if the current position begins an element at <paramref name="level"/>.</returns>
    bool IsAtBeginningOf(PageIteratorLevel level);

    /// <summary>
    /// Determines whether the current element is the final child element within a containing page element.
    /// </summary>
    /// <param name="level">The hierarchy level of the containing element.</param>
    /// <param name="element">The hierarchy level of the child element.</param>
    /// <returns>
    /// <see langword="true"/> if the current <paramref name="element"/> is the final one within
    /// <paramref name="level"/>; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsAtFinalElement(PageIteratorLevel level, PageIteratorLevel element);

    /// <summary>Attempts to get the baseline of the current page element.</summary>
    /// <param name="level">The hierarchy level of the element.</param>
    /// <param name="x1">The horizontal coordinate of the baseline start.</param>
    /// <param name="y1">The vertical coordinate of the baseline start.</param>
    /// <param name="x2">The horizontal coordinate of the baseline end.</param>
    /// <param name="y2">The vertical coordinate of the baseline end.</param>
    /// <returns><see langword="true"/> if a baseline is available; otherwise, <see langword="false"/>.</returns>
    bool TryGetBaseline(PageIteratorLevel level, out int x1, out int y1, out int x2, out int y2);

    /// <summary>Attempts to get the bounding box of the current page element.</summary>
    /// <param name="level">The hierarchy level of the element.</param>
    /// <param name="left">The left edge of the bounding box.</param>
    /// <param name="top">The top edge of the bounding box.</param>
    /// <param name="right">The exclusive right edge of the bounding box.</param>
    /// <param name="bottom">The exclusive bottom edge of the bounding box.</param>
    /// <returns><see langword="true"/> if a bounding box is available; otherwise, <see langword="false"/>.</returns>
    bool TryGetBoundingBox(PageIteratorLevel level, out int left, out int top, out int right, out int bottom);

    /// <summary>Gets the detected type of the current page block.</summary>
    /// <returns>The polygon block type.</returns>
    PolygonBlockType GetBlockType();

    /// <summary>Creates a native copy positioned at the same page element as this iterator.</summary>
    /// <returns>A new iterator owned by the caller.</returns>
    /// <remarks>The copy remains dependent on the same engine recognition state as the original iterator.</remarks>
    ITesseractPageIterator Copy();
}
