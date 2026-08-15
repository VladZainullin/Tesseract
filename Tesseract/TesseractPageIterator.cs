using System;
using System.Runtime.InteropServices;
using Leptonica;
using Leptonica.Contracts;
using Tesseract.Contracts;

namespace Tesseract;

public class TesseractPageIterator : ITesseractPageIterator
{
    private volatile bool _disposed;

    public TesseractPageIterator(TesseractPageIteratorSafeHandle handle)
    {
        ArgumentNullException.ThrowIfNull(handle);
        Handle = handle;
    }

    public SafeHandle Handle { get; }

    protected bool Disposed => _disposed;

    public void Begin()
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
        TesseractNative.TessPageIteratorBegin(Handle);
    }

    public void GetOrientation(out PageOrientation pageOrientation, out WritingDirection writingDirection,
        out TextLineOrder textLineOrder, out float deskewAngle)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
        TesseractNative.TessPageIteratorOrientation(Handle, out pageOrientation, out writingDirection, out textLineOrder
            , out deskewAngle);
    }

    public IPix GetBinaryImage(PageIteratorLevel level)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
        var pixPtr = TesseractNative.TessPageIteratorGetBinaryImage(Handle, level);
        return pixPtr == 0
            ? throw new InvalidOperationException("TessPageIteratorGetBinaryImage returned a null pointer.")
            : Pix.FromHandle(pixPtr);
    }

    public IPix GetImage(PageIteratorLevel level, int padding, IPix originalImage, out int left, out int top)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
        ArgumentNullException.ThrowIfNull(originalImage);
        var pixPtr =
            TesseractNative.TessPageIteratorGetImage(Handle, level, padding, originalImage.Handle, out left, out top);
        if (pixPtr == 0)
        {
            throw new InvalidOperationException(
                "TessPageIteratorGetImage returned a null pointer.");
        }

        return Pix.FromHandle(pixPtr);
    }

    public void GetParagraphInfo(
        out ParagraphJustification justification, out bool isListItem, out bool isCrown, out int firstLineIndent)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
        TesseractNative.TessPageIteratorParagraphInfo(Handle, out justification, out isListItem, out isCrown,
            out firstLineIndent);
    }

    public virtual bool TryNext(PageIteratorLevel level)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
        return TesseractNative.TessPageIteratorNext(Handle, level);
    }

    public bool IsAtBeginningOf(PageIteratorLevel level)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
        return TesseractNative.TessPageIteratorIsAtBeginningOf(Handle, level);
    }

    public bool IsAtFinalElement(PageIteratorLevel level, PageIteratorLevel element)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
        return TesseractNative.TessPageIteratorIsAtFinalElement(Handle, level, element);
    }

    public bool TryGetBaseline(PageIteratorLevel level, out int x1, out int y1, out int x2, out int y2)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
        return TesseractNative.TessPageIteratorBaseline(Handle, level,
            out x1, out y1, out x2, out y2);
    }

    public bool TryGetBoundingBox(PageIteratorLevel level, out int left, out int top, out int right, out int bottom)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
        return TesseractNative.TessPageIteratorBoundingBox(Handle, level, out left, out top, out right, out bottom);
    }

    public PolygonBlockType GetBlockType()
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
        return TesseractNative.TessPageIteratorBlockType(Handle);
    }

    public virtual ITesseractPageIterator Copy()
    {
        ObjectDisposedException.ThrowIf(Disposed, this);
        var pageIteratorPtr = TesseractNative.TessPageIteratorCopy(Handle);
        return pageIteratorPtr == 0
            ? throw new InvalidOperationException("TessPageIteratorCopy returned a null pointer.")
            : new TesseractPageIterator(new TesseractPageIteratorSafeHandle(pageIteratorPtr, false));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        _disposed = true;
        Handle.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}