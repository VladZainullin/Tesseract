using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public sealed class TesseractEngineSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public TesseractEngineSafeHandle() : base(true)
    {
    }

    protected override bool ReleaseHandle()
    {
        TesseractNative.TessBaseApiDelete(handle);
        return true;
    }
}