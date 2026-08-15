namespace Tesseract;

public sealed class TesseractResultIteratorSafeHandle
    : TesseractPageIteratorSafeHandle
{
    protected override bool ReleaseHandle()
    {
        try
        {
            TesseractNative.TessResultIteratorDelete(handle);
            return true;
        }
        finally
        {
            ReleaseOwner();
        }
    }
}
