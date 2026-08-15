namespace Tesseract;

public sealed class TesseractResultIteratorSafeHandle
    : TesseractPageIteratorSafeHandle
{
    protected override bool ReleaseHandle()
    {
        TesseractNative.TessResultIteratorDelete(handle);
        return true;
    }
}