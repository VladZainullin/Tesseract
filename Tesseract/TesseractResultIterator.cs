using System.Runtime.InteropServices;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractResultIterator
    : TesseractPageIterator, ITesseractResultIterator
{
    public TesseractResultIterator(TesseractResultIteratorSafeHandle handle) : base(handle)
    {
    }
    
    public override ITesseractResultIterator Copy()
    {
        var pointer = TesseractNative.TessResultIteratorCopy(Handle);
        return new TesseractResultIterator(pointer);
    }

    public ITesseractPageIterator GetPageIterator()
    {
        var borrowed = TesseractNative.TessResultIteratorGetPageIterator(Handle);
        var copy = TesseractNative.TessPageIteratorCopy(borrowed);
        return new TesseractPageIterator(copy);
    }

    public ITesseractPageIterator GetPageIteratorConst()
    {
        var pointer = TesseractNative.TessResultIteratorGetPageIteratorConst(Handle);
        return new TesseractPageIterator(pointer);
    }

    public ITesseractChoiceIterator GetChoiceIterator()
    {
        var pointer = TesseractNative.TessResultIteratorGetChoiceIterator(Handle);
        return new TesseractChoiceIterator(pointer);
    }

    public override bool TryNext(PageIteratorLevel level)
    {
        return TesseractNative.TessResultIteratorNext(Handle, level);
    }

    public string? GetText(PageIteratorLevel level)
    {
        var pointer = TesseractNative.TessResultIteratorGetUtf8Text(Handle, level);
        if (pointer == 0)
            return null;
        try
        {
            return Marshal.PtrToStringUTF8(pointer);
        }
        finally
        {
            TesseractNative.TessDeleteText(pointer);
        }
    }

    public float GetConfidence(PageIteratorLevel level)
    {
        return TesseractNative.TessResultIteratorConfidence(Handle, level);
    }

    public string? WordRecognitionLanguage()
    {
        return TesseractNative.TessResultIteratorWordRecognitionLanguage(Handle);
    }

    public string? GetWordFontAttributes(out bool isBold, out bool isItalic, out bool isUnderlined, out bool isMonospace,
        out bool isSerif, out bool isSmallCaps, out int pointSize, out int fontId)
    {
        return TesseractNative.TessResultIteratorWordFontAttributes(
            Handle, out isBold, out isItalic, out isUnderlined, out isMonospace, out isSerif, out isSmallCaps,
            out pointSize, out fontId);
    }

    public bool IsWordFromDictionary()
    {
        return TesseractNative.TessResultIteratorWordIsFromDictionary(Handle);
    }

    public bool IsWordNumeric()
    {
        return TesseractNative.TessResultIteratorWordIsNumeric(Handle);
    }

    public bool IsSymbolSuperscript()
    {
        return TesseractNative.TessResultIteratorSymbolIsSuperscript(Handle);
    }

    public bool IsSymbolSubscript()
    {
        return TesseractNative.TessResultIteratorSymbolIsSubscript(Handle);
    }

    public bool IsSymbolDropCap()
    {
        return TesseractNative.TessResultIteratorSymbolIsDropCap(Handle);
    }
}