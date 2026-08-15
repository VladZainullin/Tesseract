using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public sealed class TesseractResultRendererSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public TesseractResultRendererSafeHandle() : base(true)
    {
    }
    
    public TesseractResultRendererSafeHandle(nint handle, bool ownsHandle) : base(ownsHandle)
    {
        SetHandle(handle);
    }

    protected override bool ReleaseHandle()
    {
        TesseractNative.TessDeleteResultRenderer(this);
        return true;
    }
}