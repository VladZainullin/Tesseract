namespace Tesseract;

public sealed class TesseractResultIteratorSafeHandle : TesseractPageIteratorSafeHandle
{
    public TesseractResultIteratorSafeHandle(nint handle, bool ownsHandle) : base(handle, ownsHandle)
    {
    }


    protected override bool ReleaseHandle()
    {
        TesseractNative.TessResultIteratorDelete(this);
        return true;
    }
}