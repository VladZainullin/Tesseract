using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Leptonica.Contracts;

namespace Leptonica.Native;

internal static partial class LeptonicaNative
{
    static LeptonicaNative()
    {
        NativeLibrary.SetDllImportResolver(
            typeof(LeptonicaNative).Assembly,
            LeptonicaNativeLibrary.Resolve);
    }

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "getLeptonicaVersion")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativeGetLeptonicaVersion();

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixCreate")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixCreate(
        int width,
        int height,
        int depth);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixCreateHeader")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixCreateHeader(int width, int height, int depth);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixCreateTemplate")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixCreateTemplate(nint source);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixClone")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixClone(nint source);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixCopy")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixCopy(nint destination, nint source);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixDestroy")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void NativePixDestroy(ref nint pix);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixRead",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixRead(string filename);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixReadMem")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixReadMem(nint data, nuint size);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixWrite",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixWrite(string filename, nint pix, LeptonicaImageFormat format);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixGetDimensions")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixGetDimensions(nint pix, out int width, out int height, out int depth);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixGetWidth")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixGetWidth(nint pix);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixGetHeight")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixGetHeight(nint pix);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixGetDepth")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixGetDepth(nint pix);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixGetWpl")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixGetWordsPerLine(nint pix);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixGetData")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixGetData(nint pix);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixSetData")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixSetData(nint pix, nint data);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixGetPixel")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixGetPixel(nint pix, int x, int y, out uint value);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixSetPixel")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixSetPixel(nint pix, int x, int y, uint value);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixSetResolution")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixSetResolution(nint pix, int xResolution, int yResolution);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixGetXRes")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixGetXResolution(nint pix);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixGetYRes")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixGetYResolution(nint pix);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixConvertTo8")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixConvertTo8(nint source, int cmapFlag);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixConvertTo32")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixConvertTo32(nint source);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixConvertRGBToGray")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixConvertRgbToGray(nint source, float redWeight, float greenWeight,
        float blueWeight);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixRemoveColormap")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixRemoveColormap(nint source, LeptonicaRemoveColormapMode type);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixThresholdToBinary")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixThresholdToBinary(nint source, int threshold);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixOtsuAdaptiveThreshold")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int NativePixOtsuAdaptiveThreshold(
        nint source,
        int sx,
        int sy,
        int smoothX,
        int smoothY,
        float scoreFraction,
        out nint thresholdMap,
        out nint destination);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixScale")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixScale(nint source, float scaleX, float scaleY);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixRotateOrth")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixRotateOrth(nint source, int quarterTurns);

    [LibraryImport(LeptonicaNativeLibrary.LogicalName, EntryPoint = "pixDeskew")]
    [DefaultDllImportSearchPaths(LeptonicaNativeLibrary.DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint NativePixDeskew(nint source, int reduction);
}