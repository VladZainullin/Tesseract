using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public sealed class TesseractChoiceIteratorSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public TesseractChoiceIteratorSafeHandle() : base(true)
    {
    }
    
    public TesseractChoiceIteratorSafeHandle(nint handle, bool ownsHandle) : base(ownsHandle)
    {
        SetHandle(handle);
    }

    protected override bool ReleaseHandle()
    {
        TesseractNative.TessChoiceIteratorDelete(handle);
        return true;
    }
}