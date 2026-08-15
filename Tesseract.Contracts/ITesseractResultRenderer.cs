using System.Diagnostics.CodeAnalysis;

namespace Tesseract.Contracts;

/// <summary>
/// Represents a Tesseract result renderer used to produce OCR output
/// in a specific format.
/// </summary>
public interface ITesseractResultRenderer : IHasSafeHandle, IDisposable
{
    /// <summary>
    /// Appends another renderer to this renderer's chain.
    /// </summary>
    /// <param name="renderer">
    /// The renderer to append.
    /// </param>
    /// <remarks>
    /// When multiple renderers are chained together, OCR results can be
    /// produced in multiple output formats during the same processing operation.
    /// </remarks>
    void Insert(ITesseractResultRenderer renderer);
    
    /// <summary>
    /// Attempts to get the next renderer in the renderer chain.
    /// </summary>
    /// <param name="renderer">
    /// When this method returns <see langword="true"/>, contains the next renderer
    /// in the chain; otherwise, <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if another renderer exists in the chain;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    bool TryNext([NotNullWhen(true)] out ITesseractResultRenderer? renderer);
    
    /// <summary>
    /// Begins rendering a new document.
    /// </summary>
    /// <param name="title">
    /// The title of the document to render.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the document was successfully initialized;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    bool TryBeginDocument(string title);
    
    
    /// <summary>
    /// Adds the current image and its recognition results from the specified
    /// Tesseract engine to the document.
    /// </summary>
    /// <param name="engine">
    /// The Tesseract engine containing the image and recognition results
    /// to be rendered.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the image was successfully added;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    bool TryAddImage(ITesseractEngine engine);

    /// <summary>
    /// Finishes rendering the current document and finalizes the output.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the document was successfully finalized;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    bool TryEndDocument();
    
    /// <summary>
    /// Gets the file extension associated with the output format
    /// produced by this renderer.
    /// </summary>
    /// <returns>
    /// The file extension without a leading period.
    /// </returns>
    string GetExtension();

    /// <summary>
    /// Gets the title of the document currently associated with this renderer.
    /// </summary>
    /// <returns>
    /// The document title.
    /// </returns>
    string GetTitle();

    /// <summary>
    /// Gets the number of images that have been processed by this renderer.
    /// </summary>
    /// <returns>
    /// The number of processed images.
    /// </returns>
    int GetImageNumber();
}