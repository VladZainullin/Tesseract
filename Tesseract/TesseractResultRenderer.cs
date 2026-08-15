using System;
using System.Runtime.InteropServices;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractResultRenderer : ITesseractResultRenderer, IDisposable
{
    public TesseractResultRenderer(TesseractResultRendererSafeHandle handle)
    {
        Handle = handle;
    }

    public SafeHandle Handle { get; }

    public string Extension => TesseractNative.TessResultRendererExtension(Handle);

    public string Title => TesseractNative.TessResultRendererTitle(Handle);

    public int ImageNumbers => TesseractNative.TessResultRendererImageNum(Handle);

    public ITesseractResultRenderer NextRenderer()
    {
        var rendererPtr = TesseractNative.TessResultRendererNext(Handle);
        return new TesseractResultRenderer(rendererPtr);
    }

    public void Insert(ITesseractResultRenderer renderer)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        TesseractNative.TessResultRendererInsert(Handle, renderer.Handle);
    }

    public bool TryBeginDocument(string title)
    {
        return TesseractNative.TessResultRendererBeginDocument(Handle, title);
    }

    public bool TryAddImage(ITesseractEngine engine)
    {
        ArgumentNullException.ThrowIfNull(engine);
        return TesseractNative.TessResultRendererAddImage(Handle, engine.Handle);
    }

    public bool TryEndDocument()
    {
        return TesseractNative.TessResultRendererEndDocument(Handle);
    }

    public void Dispose()
    {
        Handle.Dispose();
    }
}