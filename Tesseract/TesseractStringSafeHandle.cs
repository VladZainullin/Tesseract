using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public sealed class TesseractStringSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public TesseractStringSafeHandle() : base(true)
    {
    }

    protected override bool ReleaseHandle()
    {
        TesseractNative.TessDeleteText(handle);
        return true;
    }
}