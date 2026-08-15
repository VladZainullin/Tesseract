using System;
using System.Runtime.InteropServices;
using Leptonica;
using Leptonica.Contracts;
using Tesseract.Contracts;

namespace Tesseract;

public class TesseractPageIterator : ITesseractPageIterator
{
    public TesseractPageIterator(TesseractPageIteratorSafeHandle handle)
    {
        ArgumentNullException.ThrowIfNull(handle);
        ObjectDisposedException.ThrowIf(handle.IsClosed, handle);
        if (handle.IsInvalid) throw new ArgumentException("Native page iterator handle is invalid.", nameof(handle));
        Handle = handle;
    }

    public SafeHandle Handle { get; }
    public void Begin()
    {
        TesseractNative.TessPageIteratorBegin(Handle);
    }

    public void GetOrientation(out PageOrientation pageOrientation, out WritingDirection writingDirection,
        out TextLineOrder textLineOrder, out float deskewAngle)
    {
        TesseractNative.TessPageIteratorOrientation(Handle, out pageOrientation, out writingDirection, out textLineOrder
            , out deskewAngle);
    }

    public IPix GetBinaryImage(PageIteratorLevel level)
    {
        var pixPtr = TesseractNative.TessPageIteratorGetBinaryImage(Handle, level);
        return new Pix(pixPtr);
    }

    public IPix GetImage(PageIteratorLevel level, int padding, IPix originalImage, out int left, out int top)
    {
        ArgumentNullException.ThrowIfNull(originalImage);
        var pixPtr =
            TesseractNative.TessPageIteratorGetImage(Handle, level, padding, originalImage.Handle, out left, out top);

        return new Pix(pixPtr);
    }

    public void GetParagraphInfo(
        out ParagraphJustification justification, out bool isListItem, out bool isCrown, out int firstLineIndent)
    {
        TesseractNative.TessPageIteratorParagraphInfo(Handle, out justification, out isListItem, out isCrown,
            out firstLineIndent);
    }

    public virtual bool TryNext(PageIteratorLevel level)
    {
        return TesseractNative.TessPageIteratorNext(Handle, level);
    }

    public bool IsAtBeginningOf(PageIteratorLevel level)
    {
        return TesseractNative.TessPageIteratorIsAtBeginningOf(Handle, level);
    }

    public bool IsAtFinalElement(PageIteratorLevel level, PageIteratorLevel element)
    {
        return TesseractNative.TessPageIteratorIsAtFinalElement(Handle, level, element);
    }

    public bool TryGetBaseline(PageIteratorLevel level, out int x1, out int y1, out int x2, out int y2)
    {
        return TesseractNative.TessPageIteratorBaseline(Handle, level,
            out x1, out y1, out x2, out y2);
    }

    public bool TryGetBoundingBox(PageIteratorLevel level, out int left, out int top, out int right, out int bottom)
    {
        return TesseractNative.TessPageIteratorBoundingBox(Handle, level, out left, out top, out right, out bottom);
    }

    public PolygonBlockType GetBlockType()
    {
        return TesseractNative.TessPageIteratorBlockType(Handle);
    }

    public virtual ITesseractPageIterator Copy()
    {
        var copy = TesseractNative.TessPageIteratorCopy(Handle);
        try
        {
            if (copy.IsInvalid)
                throw new InvalidOperationException("TessPageIteratorCopy returned an invalid handle.");

            copy.AttachOwner(Handle);
            return new TesseractPageIterator(copy);
        }
        catch
        {
            copy.Dispose();
            throw;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) Handle.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
