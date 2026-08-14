using System;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractResultRenderer : ITesseractResultRenderer, IDisposable
{
    private bool _disposed;

    public TesseractResultRenderer(nint handle)
    {
        Handle = handle;
    }
    
    public nint Handle { get; }

    public string Extension
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return TesseractNative.TessResultRendererExtension(Handle);
        }
    }

    public string Title
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return TesseractNative.TessResultRendererTitle(Handle);
        }
    }

    public int ImageNumbers
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return TesseractNative.TessResultRendererImageNum(Handle);
        }
    }

    public ITesseractResultRenderer NextRenderer()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        var rendererPtr = TesseractNative.TessResultRendererNext(Handle);
        return new TesseractResultRenderer(rendererPtr);
    }

    public void Insert(ITesseractResultRenderer renderer)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        ObjectDisposedException.ThrowIf(_disposed, this);
        TesseractNative.TessResultRendererInsert(Handle, renderer.Handle);
    }

    public bool TryBeginDocument(string title)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return TesseractNative.TessResultRendererBeginDocument(Handle, title);
    }

    public bool TryAddImage(ITesseractEngine engine)
    {
        ArgumentNullException.ThrowIfNull(engine);
        ObjectDisposedException.ThrowIf(_disposed, this);
        return TesseractNative.TessResultRendererAddImage(Handle, engine.Handle);
    }

    public bool TryEndDocument()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return TesseractNative.TessResultRendererEndDocument(Handle);
    }

    public void Dispose()
    {
        if (_disposed) return;
        
        _disposed = true;
        TesseractNative.TessDeleteResultRenderer(Handle);
    }
}