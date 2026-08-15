using System;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractChoiceIterator : IDisposable, ITesseractChoiceIterator
{
    private readonly TesseractChoiceIteratorSafeHandle _handle;

    public TesseractChoiceIterator(TesseractChoiceIteratorSafeHandle handle)
    {
        _handle = handle;
    }

    public bool TryNext()
    {
        return TesseractNative.TessChoiceIteratorNext(_handle);
    }

    public string GetText()
    {
        return TesseractNative.TessChoiceIteratorGetUtf8Text(_handle);
    }

    public float GetConfidence()
    {
        return TesseractNative.TessChoiceIteratorConfidence(_handle);
    }

    public void Dispose()
    {
        _handle.Dispose();
    }
}