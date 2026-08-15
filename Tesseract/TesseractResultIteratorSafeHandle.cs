namespace Tesseract;

public sealed class TesseractResultIteratorSafeHandle : TesseractPageIteratorSafeHandle
{
    public TesseractResultIteratorSafeHandle() : base()
    {
    }
    
    public TesseractResultIteratorSafeHandle(nint handle, bool ownsHandle) : base(handle, ownsHandle)
    {
    }

    protected override bool ReleaseHandle()
    {
        TesseractNative.TessResultIteratorDelete(handle);
        return true;
    }
}