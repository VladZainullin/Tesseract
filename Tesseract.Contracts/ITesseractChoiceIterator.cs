namespace Tesseract.Contracts;

/// <summary>
/// Provides access to alternative recognition choices for a recognized symbol.
/// </summary>
/// <remarks>
/// The iterator allows enumeration of alternative recognition results produced by Tesseract,
/// including their text and confidence values.
/// </remarks>
public interface ITesseractChoiceIterator : IDisposable
{
    /// <summary>
    /// Advances the iterator to the next recognition choice.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the iterator was successfully advanced to the next choice;
    /// otherwise, <see langword="false"/> if there are no more choices.
    /// </returns>
    bool TryNext();

    /// <summary>
    /// Gets the text associated with the current recognition choice.
    /// </summary>
    /// <returns>
    /// The text of the current recognition choice.
    /// </returns>
    string GetText();

    /// <summary>
    /// Gets the confidence value of the current recognition choice.
    /// </summary>
    /// <returns>
    /// The confidence value associated with the current recognition choice.
    /// </returns>
    float GetConfidence();
}