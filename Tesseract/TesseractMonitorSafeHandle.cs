using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public sealed class TesseractMonitorSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public TesseractMonitorSafeHandle() : base(true)
    {
    }

    protected override bool ReleaseHandle()
    {
        TesseractNative.TessMonitorDelete(handle);
        return true;
    }
}
