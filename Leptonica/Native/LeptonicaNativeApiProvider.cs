using System.Diagnostics.CodeAnalysis;
using Leptonica.Native;

namespace Vlad.Leptonica.Native;

internal static class LeptonicaNativeApiProvider
{
    internal static ILeptonicaNativeApi Current { get; } =
        CreateDefault();

    [SuppressMessage(
        "Performance",
        "CA1859:Use concrete types when possible",
        Justification = "The provider intentionally exposes the native backend through an abstraction.")]
    private static ILeptonicaNativeApi CreateDefault()
    {
#if NET7_0_OR_GREATER
        return LibraryImportLeptonicaNativeApi.Instance;
#else
        throw new PlatformNotSupportedException(
            "No Leptonica native API implementation is available for this target framework.");
#endif
    }
}
