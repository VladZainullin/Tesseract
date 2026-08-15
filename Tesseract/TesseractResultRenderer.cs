using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractResultRenderer : ITesseractResultRenderer
{
    public TesseractResultRenderer(TesseractResultRendererSafeHandle handle)
    {
        ArgumentNullException.ThrowIfNull(handle);
        ObjectDisposedException.ThrowIf(handle.IsClosed, handle);
        if (handle.IsInvalid)
            throw new ArgumentException("Native result renderer handle is invalid.", nameof(handle));

        Handle = handle;
    }

    public SafeHandle Handle { get; }

    public void Insert(ITesseractResultRenderer renderer)
    {
        ArgumentNullException.ThrowIfNull(renderer);

        if (ReferenceEquals(this, renderer))
            throw new ArgumentException("A renderer cannot be inserted into itself.", nameof(renderer));

        TesseractNative.TessResultRendererInsert(Handle, renderer.Handle);
        renderer.Handle.SetHandleAsInvalid();
    }

    public bool TryNext([NotNullWhen(true)] out ITesseractResultRenderer? renderer)
    {
        var rendererPtr = TesseractNative.TessResultRendererNext(Handle);

        if (rendererPtr == 0)
        {
            renderer = null;
            return false;
        }

        renderer = new TesseractResultRenderer(
            new TesseractResultRendererSafeHandle(rendererPtr, ownsHandle: false));

        return true;
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

    public string GetExtension() => Marshal.PtrToStringUTF8(TesseractNative.TessResultRendererExtension(Handle))!;

    public string GetTitle() => Marshal.PtrToStringUTF8(TesseractNative.TessResultRendererTitle(Handle))!;

    public int GetImageNumber() => TesseractNative.TessResultRendererImageNum(Handle);

    public void Dispose()
    {
        Handle.Dispose();
    }
}
