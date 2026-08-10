using Leptonica.Native;
using Microsoft.Win32.SafeHandles;
using Vlad.Leptonica.Native;

namespace Leptonica;

public sealed class SafePixHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    private readonly ILeptonicaNativeApi _api;

    public SafePixHandle()
        : base(ownsHandle: true)
    {
        _api = LeptonicaNativeApiProvider.Current;
    }

    internal SafePixHandle(
        nint handle,
        bool ownsHandle,
        ILeptonicaNativeApi api)
        : base(ownsHandle)
    {
        _api = api;
        SetHandle(handle);
    }

    protected override bool ReleaseHandle()
    {
        var pix = handle;
        _api.PixDestroy(ref pix);
        handle = nint.Zero;

        return true;
    }
}
