using System.Runtime.InteropServices;
using Leptonica.Contracts;

namespace Leptonica;

public sealed class Pix : IPix
{
    private readonly PixSafeHandle _handle;

    public Pix(PixSafeHandle handle)
    {
        _handle = handle;
    }
    
    public SafeHandle Handle => _handle;

    public void Dispose()
    {
        _handle.Dispose();
    }
}
