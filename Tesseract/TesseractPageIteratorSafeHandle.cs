using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public class TesseractPageIteratorSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public TesseractPageIteratorSafeHandle() : base(true)
    {
    }

    protected override bool ReleaseHandle()
    {
        TesseractNative.TessPageIteratorDelete(handle);
        return true;
    }
}