namespace Tesseract.Contracts;

/// <summary>
/// Extends page-layout traversal with access to recognized text and attributes of the current word or symbol.
/// </summary>
/// <remarks>
/// Position the iterator with <see cref="ITesseractPageIterator.Begin"/> and
/// <see cref="ITesseractPageIterator.TryNext"/> before reading recognition results. The iterator remains dependent
/// on the engine recognition state from which it was created and must not be used after that state is reset.
/// </remarks>
public interface ITesseractResultIterator : ITesseractPageIterator
{
    /// <summary>Creates an iterator over alternative recognition choices for the current symbol.</summary>
    /// <returns>A choice iterator positioned at the first alternative and owned by the caller.</returns>
    /// <remarks>This method is valid when the result iterator is positioned at a symbol.</remarks>
    ITesseractChoiceIterator GetChoiceIterator();

    /// <summary>Gets the recognized text for the current page element at the specified hierarchy level.</summary>
    /// <param name="level">The hierarchy level whose text is requested.</param>
    /// <returns>The recognized text, or <see langword="null"/> when text is unavailable.</returns>
    string? GetText(PageIteratorLevel level);

    /// <summary>Gets the recognition language used for the current word.</summary>
    /// <returns>
    /// The language identifier, or <see langword="null"/> when the language is unavailable or the iterator is not
    /// positioned at a word.
    /// </returns>
    string? WordRecognitionLanguage();

    /// <summary>Gets the detected font name and typography attributes of the current word.</summary>
    /// <param name="isBold"><see langword="true"/> if the word is detected as bold.</param>
    /// <param name="isItalic"><see langword="true"/> if the word is detected as italic.</param>
    /// <param name="isUnderlined"><see langword="true"/> if the word is detected as underlined.</param>
    /// <param name="isMonospace"><see langword="true"/> if the word uses a monospaced font.</param>
    /// <param name="isSerif"><see langword="true"/> if the word uses a serif font.</param>
    /// <param name="isSmallCaps"><see langword="true"/> if the word is detected as small capitals.</param>
    /// <param name="pointSize">The estimated font size in points.</param>
    /// <param name="fontId">The internal identifier of the detected font.</param>
    /// <returns>The detected font name, or <see langword="null"/> when font information is unavailable.</returns>
    string? GetWordFontAttributes(out bool isBold, out bool isItalic, out bool isUnderlined, out bool isMonospace,
        out bool isSerif, out bool isSmallCaps, out int pointSize, out int fontId);

    /// <summary>Determines whether the current word was found in a loaded language dictionary.</summary>
    /// <returns><see langword="true"/> if the current word came from a dictionary; otherwise, <see langword="false"/>.</returns>
    bool IsWordFromDictionary();

    /// <summary>Determines whether the current word is composed entirely of recognized numeric characters.</summary>
    /// <returns><see langword="true"/> if the current word is numeric; otherwise, <see langword="false"/>.</returns>
    bool IsWordNumeric();

    /// <summary>Determines whether the current symbol is detected as superscript.</summary>
    /// <returns><see langword="true"/> if the symbol is superscript; otherwise, <see langword="false"/>.</returns>
    bool IsSymbolSuperscript();

    /// <summary>Determines whether the current symbol is detected as subscript.</summary>
    /// <returns><see langword="true"/> if the symbol is subscript; otherwise, <see langword="false"/>.</returns>
    bool IsSymbolSubscript();

    /// <summary>Determines whether the current symbol is detected as a drop capital.</summary>
    /// <returns><see langword="true"/> if the symbol is a drop capital; otherwise, <see langword="false"/>.</returns>
    bool IsSymbolDropCap();
}
