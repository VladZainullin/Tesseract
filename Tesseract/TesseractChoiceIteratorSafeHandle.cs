using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public sealed class TesseractChoiceIteratorSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public TesseractChoiceIteratorSafeHandle() : base(true)
    {
    }

    protected override bool ReleaseHandle()
    {
        TesseractNative.TessChoiceIteratorDelete(handle);
        return true;
    }
}