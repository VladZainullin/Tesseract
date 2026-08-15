using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Tesseract.Contracts;

namespace Tesseract;

internal static partial class TesseractNative
{
    private static nint _handle;
    private const string LibraryName = "tesseract";
    private const DllImportSearchPath DefaultDllImportSearchPath = DllImportSearchPath.SafeDirectories;

    private static readonly object Lock = new();

    static TesseractNative()
    {
        NativeLibrary.SetDllImportResolver(typeof(TesseractNative).Assembly, (libraryName, _, _) =>
        {
            if (!string.Equals(libraryName, LibraryName, StringComparison.Ordinal)) return nint.Zero;
            lock (Lock)
            {
                if (_handle != nint.Zero) return _handle;

                var libraryPath = Environment.GetEnvironmentVariable("TESSERACT_LIBRARY_PATH");
                if (libraryPath == null) throw new InvalidOperationException("TESSERACT_LIBRARY_PATH is not set");

                if (NativeLibrary.TryLoad(libraryPath, out var handle))
                {
                    _handle = handle;
                    return handle;
                }

                throw new InvalidOperationException("TESSERACT_LIBRARY_PATH is not valid");
            }
        });
    }

    [LibraryImport(LibraryName, EntryPoint = "TessVersion", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessVersion();

    [LibraryImport(LibraryName, EntryPoint = "TessDeleteText")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessDeleteText(nint text);

    [LibraryImport(LibraryName, EntryPoint = "TessDeleteTextArray")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessDeleteTextArray(nint arr);

    [LibraryImport(LibraryName, EntryPoint = "TessDeleteIntArray")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessDeleteIntArray(nint arr);

    [LibraryImport(LibraryName, EntryPoint = "TessTextRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultRendererSafeHandle TessTextRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessHOcrRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultRendererSafeHandle TessHOcrRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessHOcrRendererCreate2", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultRendererSafeHandle TessHOcrRendererCreate2(string outputBase,
        [MarshalAs(UnmanagedType.Bool)] bool fontInfo);

    [LibraryImport(LibraryName, EntryPoint = "TessAltoRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultRendererSafeHandle TessAltoRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessTsvRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultRendererSafeHandle TessTsvRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessPDFRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultRendererSafeHandle TessPdfRendererCreate(string outputBase, string dataDir,
        [MarshalAs(UnmanagedType.Bool)] bool textOnly);

    [LibraryImport(LibraryName, EntryPoint = "TessUnlvRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultRendererSafeHandle TessUnlvRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessBoxTextRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultRendererSafeHandle TessBoxTextRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessLSTMBoxRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultRendererSafeHandle TessLstmBoxRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessWordStrBoxRendererCreate",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultRendererSafeHandle TessWordStrBoxRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessDeleteResultRenderer")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessDeleteResultRenderer(SafeHandle renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererInsert")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessResultRendererInsert(SafeHandle renderer, SafeHandle subRenderer);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererNext")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultRendererSafeHandle TessResultRendererNext(SafeHandle renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererBeginDocument",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultRendererBeginDocument(SafeHandle renderer, string title);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererAddImage")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultRendererAddImage(SafeHandle renderer, SafeHandle api);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererEndDocument")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultRendererEndDocument(SafeHandle renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererExtention", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessResultRendererExtension(SafeHandle renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererTitle", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessResultRendererTitle(SafeHandle renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererImageNum")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessResultRendererImageNum(SafeHandle renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPICreate")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractEngineSafeHandle TessBaseApiCreate();

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIDelete")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiDelete(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetInputName", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetInputName(SafeHandle handle, string name);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetInputName", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string? TessBaseApiGetInputName(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetInputImage")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetInputImage(SafeHandle handle, nint pix);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetInputImage")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetInputImage(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetSourceYResolution")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiGetSourceYResolution(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetDatapath", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessBaseApiGetDataPath(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetOutputName", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetOutputName(SafeHandle handle, string name);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetVariable", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiSetVariable(SafeHandle handle, string name, string value);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetDebugVariable", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiSetDebugVariable(SafeHandle handle, string name, string value);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetIntVariable", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiGetIntVariable(SafeHandle handle, string name, out int value);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetBoolVariable", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiGetBoolVariable(SafeHandle handle, string name,
        [MarshalAs(UnmanagedType.Bool)] out bool value);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetDoubleVariable",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiGetDoubleVariable(SafeHandle handle, string name, out double value);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetStringVariable",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessBaseApiGetStringVariable(SafeHandle handle, string name);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIPrintVariables")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiPrintVariables(SafeHandle handle, nint fp);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIPrintVariablesToFile",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiPrintVariablesToFile(SafeHandle handle, string filename);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIInit1", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    internal static partial int TessBaseApiInit1(SafeHandle handle, string? dataPath, string? language, OcrEngineMode oem,
        nint configs,
        int configsSize);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIInit2", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiInit2(SafeHandle handle, string dataPath, string language, OcrEngineMode oem);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIInit3", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiInit3(SafeHandle handle, string dataPath, string language);

    [LibraryImport(
        LibraryName,
        EntryPoint = "TessBaseAPIInit4",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiInit4(SafeHandle handle, string? dataPath, string? language, OcrEngineMode mode,
        nint configs, int configsSize, nint varsVec, nint varsValues, nuint varsVecSize,
        [MarshalAs(UnmanagedType.Bool)] bool setOnlyNonDebugParams);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetInitLanguagesAsString",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessBaseApiGetInitLanguagesAsString(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetAvailableLanguagesAsVector")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetAvailableLanguagesAsVector(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetLoadedLanguagesAsVector")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetLoadedLanguagesAsVector(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIInitForAnalysePage")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiInitForAnalysePage(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIReadConfigFile", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiReadConfigFile(SafeHandle handle, string filename);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIReadDebugConfigFile",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiReadDebugConfigFile(SafeHandle handle, string filename);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetPageSegMode")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetPageSegMode(SafeHandle handle, PageSegmentationMode mode);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetPageSegMode")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial PageSegmentationMode TessBaseApiGetPageSegMode(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIRect")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiRect(SafeHandle handle, nint imageData, int bytesPerPixel, int bytesPerLine,
        int left, int top, int width, int height);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIClearAdaptiveClassifier")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiClearAdaptiveClassifier(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetImage")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetImage(SafeHandle handle, nint imageData, int width, int height,
        int bytesPerPixel, int bytesPerLine);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetImage2")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetImage2(SafeHandle handle, nint pix);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetSourceResolution")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetSourceResolution(SafeHandle handle, int ppi);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetRectangle")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetRectangle(SafeHandle handle, int left, int top, int width, int height);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetThresholdedImage")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetThresholdedImage(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetRegions")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetRegions(SafeHandle handle, out nint pixa);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetTextlines")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetTextLines(SafeHandle handle, out nint pixa, out nint blockIds);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetTextlines1")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetTextLines1(SafeHandle handle, [MarshalAs(UnmanagedType.Bool)] bool rawImage,
        int rawPadding, out nint pixa, out nint blockIds, out nint paraIds);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetStrips")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetStrips(SafeHandle handle, out nint pixa, out nint blockIds);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetWords")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetWords(SafeHandle handle, out nint pixa);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetConnectedComponents")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetConnectedComponents(SafeHandle handle, out nint connectedComponents);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetComponentImages")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetComponentImages(SafeHandle handle, PageIteratorLevel level,
        [MarshalAs(UnmanagedType.Bool)] bool textOnly, out nint pixa, out nint blockIds);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetComponentImages1")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetComponentImages1(SafeHandle handle, PageIteratorLevel level,
        [MarshalAs(UnmanagedType.Bool)] bool textOnly, [MarshalAs(UnmanagedType.Bool)] bool rawImage,
        int rawPadding, out nint pixa, out nint blockIds, out nint paraIds);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetThresholdedImageScaleFactor")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiGetThresholdedImageScaleFactor(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIAnalyseLayout")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractPageIteratorSafeHandle TessBaseApiAnalyseLayout(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIRecognize")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiRecognize(SafeHandle handle, nint monitor);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIProcessPages", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiProcessPages(SafeHandle handle, string filename, string retryConfig,
        int timeoutMillis, nint renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIProcessPage", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiProcessPage(SafeHandle handle, nint pix, int pageIndex, string? fileName,
        string? retryConfig, int timeoutMilliseconds, nint renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetIterator")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultIteratorSafeHandle TessBaseApiGetIterator(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetMutableIterator")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetMutableIterator(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetUTF8Text")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetUtf8Text(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetHOCRText")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetHOcrText(SafeHandle handle, int pageNumber);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetAltoText")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetAltoText(SafeHandle handle, int pageNumber);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetTsvText")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetTsvText(SafeHandle handle, int pageNumber);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetBoxText")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetBoxText(SafeHandle handle, int pageNumber);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetLSTMBoxText")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetLstmBoxText(SafeHandle handle, int pageNumber);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetWordStrBoxText")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetWordStrBoxText(SafeHandle handle, int pageNumber);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetUNLVText")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetUnlvText(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIMeanTextConf")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiMeanTextConf(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIAllWordConfidences")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiAllWordConfidences(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIAdaptToWordStr", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiAdaptToWordStr(SafeHandle handle, PageSegmentationMode mode, string wordStr);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIClear")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiClear(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIEnd")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiEnd(SafeHandle handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIIsValidWord", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiIsValidWord(SafeHandle handle, string word);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetTextDirection")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiGetTextDirection(SafeHandle handle, out int offset, out float slope);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetUnichar", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessBaseApiGetUniChar(SafeHandle handle, int uniCharId);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetMinOrientationMargin")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetMinOrientationMargin(SafeHandle handle, double margin);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorDelete")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessPageIteratorDelete(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorCopy")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractPageIteratorSafeHandle TessPageIteratorCopy(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorBegin")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessPageIteratorBegin(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorNext")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessPageIteratorNext(SafeHandle iterator, PageIteratorLevel level);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorIsAtBeginningOf")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessPageIteratorIsAtBeginningOf(SafeHandle iterator, PageIteratorLevel level);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorIsAtFinalElement")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessPageIteratorIsAtFinalElement(SafeHandle iterator, PageIteratorLevel level,
        PageIteratorLevel element);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorBoundingBox")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessPageIteratorBoundingBox(SafeHandle iterator, PageIteratorLevel level,
        out int left, out int top, out int right, out int bottom);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorBlockType")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial PolygonBlockType TessPageIteratorBlockType(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorGetBinaryImage")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessPageIteratorGetBinaryImage(SafeHandle iterator, PageIteratorLevel level);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorGetImage")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessPageIteratorGetImage(SafeHandle iterator, PageIteratorLevel level, int padding,
        nint originalImagePtr, out int left, out int top);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorBaseline")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessPageIteratorBaseline(SafeHandle iterator, PageIteratorLevel level, out int x1,
        out int y1, out int x2, out int y2);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorOrientation")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessPageIteratorOrientation(
        SafeHandle iterator,
        out PageOrientation pageOrientation,
        out WritingDirection writingDirection,
        out TextLineOrder textLineOrder,
        out float deskewAngle);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorParagraphInfo")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessPageIteratorParagraphInfo(
        SafeHandle iterator,
        out ParagraphJustification justification,
        [MarshalAs(UnmanagedType.Bool)] out bool isListItem,
        [MarshalAs(UnmanagedType.Bool)] out bool isCrown,
        out int firstLineIndent);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorDelete")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessResultIteratorDelete(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorCopy")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractResultIteratorSafeHandle TessResultIteratorCopy(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorGetPageIterator")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractPageIteratorSafeHandle TessResultIteratorGetPageIterator(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorGetPageIteratorConst")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractPageIteratorSafeHandle TessResultIteratorGetPageIteratorConst(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorGetChoiceIterator")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial TesseractChoiceIteratorSafeHandle TessResultIteratorGetChoiceIterator(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorNext")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultIteratorNext(SafeHandle iterator, PageIteratorLevel level);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorGetUTF8Text")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessResultIteratorGetUtf8Text(SafeHandle iterator, PageIteratorLevel level);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorConfidence")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial float TessResultIteratorConfidence(SafeHandle iterator, PageIteratorLevel level);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorWordRecognitionLanguage",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string? TessResultIteratorWordRecognitionLanguage(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorWordFontAttributes",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string? TessResultIteratorWordFontAttributes(
        SafeHandle iterator,
        [MarshalAs(UnmanagedType.Bool)] out bool isBold,
        [MarshalAs(UnmanagedType.Bool)] out bool isItalic,
        [MarshalAs(UnmanagedType.Bool)] out bool isUnderlined,
        [MarshalAs(UnmanagedType.Bool)] out bool isMonospace,
        [MarshalAs(UnmanagedType.Bool)] out bool isSerif,
        [MarshalAs(UnmanagedType.Bool)] out bool isSmallCaps,
        out int pointSize,
        out int fontId);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorWordIsFromDictionary")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultIteratorWordIsFromDictionary(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorWordIsNumeric")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultIteratorWordIsNumeric(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorSymbolIsSuperscript")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultIteratorSymbolIsSuperscript(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorSymbolIsSubscript")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultIteratorSymbolIsSubscript(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorSymbolIsDropcap")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultIteratorSymbolIsDropCap(SafeHandle iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessChoiceIteratorDelete")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessChoiceIteratorDelete(SafeHandle choiceIterator);

    [LibraryImport(LibraryName, EntryPoint = "TessChoiceIteratorNext")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessChoiceIteratorNext(SafeHandle choiceIterator);

    [LibraryImport(LibraryName, EntryPoint = "TessChoiceIteratorGetUTF8Text",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessChoiceIteratorGetUtf8Text(SafeHandle choiceIterator);

    [LibraryImport(LibraryName, EntryPoint = "TessChoiceIteratorConfidence")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial float TessChoiceIteratorConfidence(SafeHandle choiceIterator);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorCreate")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessMonitorCreate();

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorDelete")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessMonitorDelete(nint monitor);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorSetCancelFunc")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessMonitorSetCancelFunc(nint monitor, nint cancelFunc);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorGetCancelThis")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessMonitorGetCancelThis(nint monitor);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorSetCancelThis")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessMonitorSetCancelThis(nint monitor, nint cancelThis);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorSetProgressFunc")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessMonitorSetProgressFunc(nint monitor, nint progress);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorGetProgress")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessMonitorGetProgress(nint monitor);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorSetDeadlineMSecs")]
    [DefaultDllImportSearchPaths(DefaultDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessMonitorSetDeadlineMSecs(nint monitor, int deadline);
}