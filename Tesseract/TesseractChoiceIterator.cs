using System;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractChoiceIterator : IDisposable, ITesseractChoiceIterator
{
    private readonly TesseractChoiceIteratorSafeHandle _handle;
    private volatile bool _disposed;

    public TesseractChoiceIterator(TesseractChoiceIteratorSafeHandle handle)
    {
        _handle = handle;
    }

    public bool TryNext()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return TesseractNative.TessChoiceIteratorNext(_handle);
    }

    public string GetText()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return TesseractNative.TessChoiceIteratorGetUtf8Text(_handle);
    }

    public float GetConfidence()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return TesseractNative.TessChoiceIteratorConfidence(_handle);
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _handle.Dispose();
    }
}