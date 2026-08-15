using System.Runtime.InteropServices;

namespace Leptonica.Contracts;

public interface IPix : IDisposable
{
    SafeHandle Handle { get; }
}
