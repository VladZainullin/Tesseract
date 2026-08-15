using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public sealed class TesseractStringSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public TesseractStringSafeHandle() : base(true)
    {
    }

    public string? ToManagedString()
    {
        return IsInvalid ? null : Marshal.PtrToStringUTF8(handle);
    }

    protected override bool ReleaseHandle()
    {
        TesseractNative.TessDeleteText(handle);
        return true;
    }
}
