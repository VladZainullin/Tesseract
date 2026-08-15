using System;
using System.Runtime.InteropServices;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractResultRenderer : ITesseractResultRenderer
{
    public TesseractResultRenderer(TesseractResultRendererSafeHandle handle)
    {
        Handle = handle;
    }

    public SafeHandle Handle { get; }
    
    public void Insert(ITesseractResultRenderer renderer)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        TesseractNative.TessResultRendererInsert(Handle, renderer.Handle);
    }
    
    public ITesseractResultRenderer NextRenderer()
    {
        var rendererPtr = TesseractNative.TessResultRendererNext(Handle);
        return new TesseractResultRenderer(rendererPtr);
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
    
    public string GetExtension() => TesseractNative.TessResultRendererExtension(Handle);

    public string GetTitle() => TesseractNative.TessResultRendererTitle(Handle);

    public int GetImageNumbers() => TesseractNative.TessResultRendererImageNum(Handle);

    public void Dispose()
    {
        Handle.Dispose();
    }
}