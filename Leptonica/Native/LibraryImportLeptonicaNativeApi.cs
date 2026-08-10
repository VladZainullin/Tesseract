#if NET7_0_OR_GREATER
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Leptonica.Contracts;

namespace Leptonica.Native;

internal sealed partial class LibraryImportLeptonicaNativeApi
    : ILeptonicaNativeApi
{
    internal static LibraryImportLeptonicaNativeApi Instance { get; } =
        new();

    static LibraryImportLeptonicaNativeApi()
    {
        NativeLibrary.SetDllImportResolver(
            typeof(LibraryImportLeptonicaNativeApi).Assembly,
            LeptonicaNativeLibrary.Resolve);
    }

    private LibraryImportLeptonicaNativeApi()
    {
    }

    public nint GetLeptonicaVersion()
    {
        return NativeGetLeptonicaVersion();
    }

    public nint PixCreate(
        int width,
        int height,
        int depth)
    {
        return NativePixCreate(
            width,
            height,
            depth);
    }

    public nint PixCreateHeader(
        int width,
        int height,
        int depth)
    {
        return NativePixCreateHeader(
            width,
            height,
            depth);
    }

    public nint PixCreateTemplate(
        nint source)
    {
        return NativePixCreateTemplate(source);
    }

    public nint PixClone(
        nint source)
    {
        return NativePixClone(source);
    }

    public nint PixCopy(
        nint destination,
        nint source)
    {
        return NativePixCopy(
            destination,
            source);
    }

    public void PixDestroy(
        ref nint pix)
    {
        NativePixDestroy(ref pix);
    }

    public nint PixRead(
        string filename)
    {
        return NativePixRead(filename);
    }

    public nint PixReadMem(
        nint data,
        nuint size)
    {
        return NativePixReadMem(
            data,
            size);
    }

    public int PixWrite(
        string filename,
        nint pix,
        LeptonicaImageFormat format)
    {
        return NativePixWrite(
            filename,
            pix,
            format);
    }

    public int PixGetDimensions(
        nint pix,
        out int width,
        out int height,
        out int depth)
    {
        return NativePixGetDimensions(
            pix,
            out width,
            out height,
            out depth);
    }

    public int PixGetWidth(
        nint pix)
    {
        return NativePixGetWidth(pix);
    }

    public int PixGetHeight(
        nint pix)
    {
        return NativePixGetHeight(pix);
    }

    public int PixGetDepth(
        nint pix)
    {
        return NativePixGetDepth(pix);
    }

    public int PixGetWordsPerLine(
        nint pix)
    {
        return NativePixGetWordsPerLine(pix);
    }

    public nint PixGetData(
        nint pix)
    {
        return NativePixGetData(pix);
    }

    public int PixSetData(
        nint pix,
        nint data)
    {
        return NativePixSetData(
            pix,
            data);
    }

    public int PixGetPixel(
        nint pix,
        int x,
        int y,
        out uint value)
    {
        return NativePixGetPixel(
            pix,
            x,
            y,
            out value);
    }

    public int PixSetPixel(
        nint pix,
        int x,
        int y,
        uint value)
    {
        return NativePixSetPixel(
            pix,
            x,
            y,
            value);
    }

    public int PixSetResolution(
        nint pix,
        int xResolution,
        int yResolution)
    {
        return NativePixSetResolution(
            pix,
            xResolution,
            yResolution);
    }

    public int PixGetXResolution(
        nint pix)
    {
        return NativePixGetXResolution(pix);
    }

    public int PixGetYResolution(
        nint pix)
    {
        return NativePixGetYResolution(pix);
    }

    public nint PixConvertTo8(
        nint source,
        int cmapFlag)
    {
        return NativePixConvertTo8(
            source,
            cmapFlag);
    }

    public nint PixConvertTo32(
        nint source)
    {
        return NativePixConvertTo32(source);
    }

    public nint PixConvertRgbToGray(
        nint source,
        float redWeight,
        float greenWeight,
        float blueWeight)
    {
        return NativePixConvertRgbToGray(
            source,
            redWeight,
            greenWeight,
            blueWeight);
    }

    public nint PixRemoveColormap(
        nint source,
        LeptonicaRemoveColormapMode type)
    {
        return NativePixRemoveColormap(
            source,
            type);
    }

    public nint PixThresholdToBinary(
        nint source,
        int threshold)
    {
        return NativePixThresholdToBinary(
            source,
            threshold);
    }

    public int PixOtsuAdaptiveThreshold(
        nint source,
        int sx,
        int sy,
        int smoothX,
        int smoothY,
        float scoreFraction,
        out nint thresholdMap,
        out nint destination)
    {
        return NativePixOtsuAdaptiveThreshold(
            source,
            sx,
            sy,
            smoothX,
            smoothY,
            scoreFraction,
            out thresholdMap,
            out destination);
    }

    public nint PixScale(
        nint source,
        float scaleX,
        float scaleY)
    {
        return NativePixScale(
            source,
            scaleX,
            scaleY);
    }

    public nint PixRotateOrth(
        nint source,
        int quarterTurns)
    {
        return NativePixRotateOrth(
            source,
            quarterTurns);
    }

    public nint PixDeskew(
        nint source,
        int reduction)
    {
        return NativePixDeskew(
            source,
            reduction);
    }

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "getLeptonicaVersion")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativeGetLeptonicaVersion();

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixCreate")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixCreate(
        int width,
        int height,
        int depth);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixCreateHeader")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixCreateHeader(
        int width,
        int height,
        int depth);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixCreateTemplate")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixCreateTemplate(
        nint source);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixClone")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixClone(
        nint source);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixCopy")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixCopy(
        nint destination,
        nint source);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixDestroy")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial void NativePixDestroy(
        ref nint pix);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixRead",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixRead(
        string filename);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixReadMem")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixReadMem(
        nint data,
        nuint size);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixWrite",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixWrite(
        string filename,
        nint pix,
        LeptonicaImageFormat format);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixGetDimensions")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixGetDimensions(
        nint pix,
        out int width,
        out int height,
        out int depth);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixGetWidth")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixGetWidth(
        nint pix);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixGetHeight")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixGetHeight(
        nint pix);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixGetDepth")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixGetDepth(
        nint pix);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixGetWpl")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixGetWordsPerLine(
        nint pix);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixGetData")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixGetData(
        nint pix);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixSetData")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixSetData(
        nint pix,
        nint data);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixGetPixel")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixGetPixel(
        nint pix,
        int x,
        int y,
        out uint value);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixSetPixel")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixSetPixel(
        nint pix,
        int x,
        int y,
        uint value);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixSetResolution")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixSetResolution(
        nint pix,
        int xResolution,
        int yResolution);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixGetXRes")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixGetXResolution(
        nint pix);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixGetYRes")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixGetYResolution(
        nint pix);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixConvertTo8")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixConvertTo8(
        nint source,
        int cmapFlag);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixConvertTo32")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixConvertTo32(
        nint source);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixConvertRGBToGray")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixConvertRgbToGray(
        nint source,
        float redWeight,
        float greenWeight,
        float blueWeight);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixRemoveColormap")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixRemoveColormap(
        nint source,
        LeptonicaRemoveColormapMode type);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixThresholdToBinary")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixThresholdToBinary(
        nint source,
        int threshold);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixOtsuAdaptiveThreshold")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial int NativePixOtsuAdaptiveThreshold(
        nint source,
        int sx,
        int sy,
        int smoothX,
        int smoothY,
        float scoreFraction,
        out nint thresholdMap,
        out nint destination);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixScale")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixScale(
        nint source,
        float scaleX,
        float scaleY);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixRotateOrth")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixRotateOrth(
        nint source,
        int quarterTurns);

    [LibraryImport(
        LeptonicaNativeLibrary.LogicalName,
        EntryPoint = "pixDeskew")]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial nint NativePixDeskew(
        nint source,
        int reduction);
}
#endif
