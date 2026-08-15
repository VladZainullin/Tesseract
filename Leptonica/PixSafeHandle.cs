using Leptonica.Native;
using Microsoft.Win32.SafeHandles;

namespace Leptonica;

public sealed class PixSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    internal PixSafeHandle(nint handle, bool ownsHandle)
        : base(ownsHandle)
    {
        SetHandle(handle);
    }

    protected override bool ReleaseHandle()
    {
        LeptonicaNative.NativePixDestroy(ref handle);

        return true;
    }
}
