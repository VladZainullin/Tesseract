using System;
using System.Runtime.InteropServices;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractChoiceIterator : ITesseractChoiceIterator
{
    private readonly TesseractChoiceIteratorSafeHandle _handle;

    public TesseractChoiceIterator(TesseractChoiceIteratorSafeHandle handle)
    {
        ArgumentNullException.ThrowIfNull(handle);
        ObjectDisposedException.ThrowIf(handle.IsClosed, handle);
        if (handle.IsInvalid)
            throw new ArgumentException("Native choice iterator handle is invalid.", nameof(handle));
        
        _handle = handle;
    }

    public bool TryNext()
    {
        return TesseractNative.TessChoiceIteratorNext(_handle);
    }

    public string GetText()
    {
        var textPtr = TesseractNative.TessChoiceIteratorGetUtf8Text(_handle);

        return textPtr == nint.Zero
            ? throw new InvalidOperationException("TessChoiceIteratorGetUTF8Text returned a null pointer.")
            : Marshal.PtrToStringUTF8(textPtr)!;
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