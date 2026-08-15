using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public sealed class TesseractEngineSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public TesseractEngineSafeHandle(nint handle, bool ownsHandle) : base(ownsHandle)
    {
        SetHandle(handle);
    }

    protected override bool ReleaseHandle()
    {
        TesseractNative.TessBaseApiDelete(this);
        return true;
    }
}