using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public class TesseractPageIteratorSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public TesseractPageIteratorSafeHandle() : base(true)
    {
    }
    
    public TesseractPageIteratorSafeHandle(nint handle, bool ownsHandle) : base(ownsHandle)
    {
        SetHandle(handle);
    }

    protected override bool ReleaseHandle()
    {
        TesseractNative.TessPageIteratorDelete(handle);
        return true;
    }
}