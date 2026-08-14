using System;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractChoiceIterator : IDisposable, ITesseractChoiceIterator
{
    private readonly nint _iterator;
    private volatile bool _disposed;

    public TesseractChoiceIterator(nint handle)
    {
        if (handle <= 0) throw new ArgumentOutOfRangeException(nameof(handle));
        _iterator = handle;
    }

    public bool TryNext()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return TesseractNative.TessChoiceIteratorNext(_iterator);
    }

    public string GetText()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return TesseractNative.TessChoiceIteratorGetUtf8Text(_iterator);
    }

    public float GetConfidence()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return TesseractNative.TessChoiceIteratorConfidence(_iterator);
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        TesseractNative.TessChoiceIteratorDelete(_iterator);
    }
}