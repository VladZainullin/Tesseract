using System;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractResultRenderer : ITesseractResultRenderer, IDisposable
{
    private readonly nint _handle;
    private bool _disposed;

    public TesseractResultRenderer(nint handle)
    {
        _handle = handle;
    }

    public string Extension
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return TesseractNative.TessResultRendererExtension(_handle);
        }
    }

    public string Title
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return TesseractNative.TessResultRendererTitle(_handle);
        }
    }

    public int ImageNumbers
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return TesseractNative.TessResultRendererImageNum(_handle);
        }
    }

    public ITesseractResultRenderer NextRenderer()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        var rendererPtr = TesseractNative.TessResultRendererNext(_handle);
        return new TesseractResultRenderer(rendererPtr);
    }

    public void Insert(ITesseractResultRenderer renderer)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    public bool TryBeginDocument(string title)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return TesseractNative.TessResultRendererBeginDocument(_handle, title);
    }

    public bool TryAddImage(ITesseractEngine engine)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return TesseractNative.TessResultRendererAddImage(_handle, engine.Handle);
    }

    public bool TryEndDocument()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return TesseractNative.TessResultRendererEndDocument(_handle);
    }

    public void Dispose()
    {
        if (_disposed) return;
        
        _disposed = true;
        TesseractNative.TessDeleteResultRenderer(_handle);
    }
}