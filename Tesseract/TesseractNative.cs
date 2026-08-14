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
    private const DllImportSearchPath DefauleDllImportSearchPath = DllImportSearchPath.SafeDirectories;

    private static readonly object Lock = new();

    static TesseractNative()
    {
        NativeLibrary.SetDllImportResolver(typeof(TesseractNative).Assembly, (libraryName, assembly, searchPath) =>
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
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessVersion();

    [LibraryImport(LibraryName, EntryPoint = "TessDeleteText")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessDeleteText(nint text);

    [LibraryImport(LibraryName, EntryPoint = "TessDeleteTextArray")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessDeleteTextArray(nint arr);

    [LibraryImport(LibraryName, EntryPoint = "TessDeleteIntArray")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessDeleteIntArray(nint arr);

    [LibraryImport(LibraryName, EntryPoint = "TessTextRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessTextRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessHOcrRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessHOcrRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessHOcrRendererCreate2", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessHOcrRendererCreate2(string outputBase,
        [MarshalAs(UnmanagedType.Bool)] bool fontInfo);

    [LibraryImport(LibraryName, EntryPoint = "TessAltoRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessAltoRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessTsvRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessTsvRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessPDFRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessPdfRendererCreate(string outputBase, string dataDir,
        [MarshalAs(UnmanagedType.Bool)] bool textOnly);

    [LibraryImport(LibraryName, EntryPoint = "TessUnlvRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessUnlvRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessBoxTextRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBoxTextRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessLSTMBoxRendererCreate", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessLstmBoxRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessWordStrBoxRendererCreate",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessWordStrBoxRendererCreate(string outputBase);

    [LibraryImport(LibraryName, EntryPoint = "TessDeleteResultRenderer")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessDeleteResultRenderer(nint renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererInsert")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessResultRendererInsert(nint renderer, nint subRenderer);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererNext")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessResultRendererNext(nint renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererBeginDocument",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultRendererBeginDocument(nint renderer, string title);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererAddImage")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultRendererAddImage(nint renderer, nint api);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererEndDocument")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultRendererEndDocument(nint renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererExtention", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessResultRendererExtension(nint renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererTitle", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessResultRendererTitle(nint renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessResultRendererImageNum")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessResultRendererImageNum(nint renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPICreate")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiCreate();

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIDelete")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiDelete(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetInputName", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetInputName(nint handle, string name);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetInputName", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessBaseApiGetInputName(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetInputImage")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetInputImage(nint handle, nint pix);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetInputImage")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetInputImage(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetSourceYResolution")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiGetSourceYResolution(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetDatapath", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessBaseApiGetDataPath(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetOutputName", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetOutputName(nint handle, string name);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetVariable", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiSetVariable(nint handle, string name, string value);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetDebugVariable", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiSetDebugVariable(nint handle, string name, string value);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetIntVariable", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiGetIntVariable(nint handle, string name, out int value);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetBoolVariable", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiGetBoolVariable(nint handle, string name,
        [MarshalAs(UnmanagedType.Bool)] out bool value);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetDoubleVariable",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiGetDoubleVariable(nint handle, string name, out double value);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetStringVariable",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessBaseApiGetStringVariable(nint handle, string name);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIPrintVariables")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiPrintVariables(nint handle, nint fp);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIPrintVariablesToFile",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiPrintVariablesToFile(nint handle, string filename);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIInit1", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    internal static partial int TessBaseApiInit1(nint handle, string? dataPath, string? language, OcrEngineMode oem,
        nint configs,
        int configsSize);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIInit2", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiInit2(nint handle, string dataPath, string language, OcrEngineMode oem);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIInit3", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiInit3(nint handle, string dataPath, string language);

    [LibraryImport(
        LibraryName,
        EntryPoint = "TessBaseAPIInit4",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiInit4(nint handle, string? dataPath, string? language, OcrEngineMode mode,
        nint configs, int configsSize, nint varsVec, nint varsValues, nuint varsVecSize,
        [MarshalAs(UnmanagedType.Bool)] bool setOnlyNonDebugParams);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetInitLanguagesAsString",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessBaseApiGetInitLanguagesAsString(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetAvailableLanguagesAsVector")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetAvailableLanguagesAsVector(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetLoadedLanguagesAsVector")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetLoadedLanguagesAsVector(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIInitForAnalysePage")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiInitForAnalysePage(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIReadConfigFile", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiReadConfigFile(nint handle, string filename);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIReadDebugConfigFile",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiReadDebugConfigFile(nint handle, string filename);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetPageSegMode")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetPageSegMode(nint handle, PageSegmentationMode mode);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetPageSegMode")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial PageSegmentationMode TessBaseApiGetPageSegMode(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIRect")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiRect(nint handle, nint imageData, int bytesPerPixel, int bytesPerLine,
        int left, int top, int width, int height);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIClearAdaptiveClassifier")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiClearAdaptiveClassifier(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetImage")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetImage(nint handle, nint imageData, int width, int height,
        int bytesPerPixel, int bytesPerLine);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetImage2")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetImage2(nint handle, nint pix);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetSourceResolution")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetSourceResolution(nint handle, int ppi);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetRectangle")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetRectangle(nint handle, int left, int top, int width, int height);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetThresholdedImage")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetThresholdedImage(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetRegions")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetRegions(nint handle, out nint pixa);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetTextlines")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetTextLines(nint handle, out nint pixa, out nint blockIds);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetTextlines1")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetTextLines1(nint handle, [MarshalAs(UnmanagedType.Bool)] bool rawImage,
        int rawPadding, out nint pixa, out nint blockIds, out nint paraIds);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetStrips")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetStrips(nint handle, out nint pixa, out nint blockIds);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetWords")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetWords(nint handle, out nint pixa);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetConnectedComponents")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetConnectedComponents(nint handle, out nint connectedComponents);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetComponentImages")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetComponentImages(nint handle, PageIteratorLevel level,
        [MarshalAs(UnmanagedType.Bool)] bool textOnly, out nint pixa, out nint blockIds);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetComponentImages1")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetComponentImages1(nint handle, PageIteratorLevel level,
        [MarshalAs(UnmanagedType.Bool)] bool textOnly, [MarshalAs(UnmanagedType.Bool)] bool rawImage,
        int rawPadding, out nint pixa, out nint blockIds, out nint paraIds);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetThresholdedImageScaleFactor")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiGetThresholdedImageScaleFactor(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIAnalyseLayout")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiAnalyseLayout(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIRecognize")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiRecognize(nint handle, nint monitor);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIProcessPages", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiProcessPages(nint handle, string filename, string retryConfig,
        int timeoutMillis, nint renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIProcessPage", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiProcessPage(nint handle, nint pix, int pageIndex, string? fileName,
        string? retryConfig, int timeoutMilliseconds, nint renderer);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetIterator")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetIterator(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetMutableIterator")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetMutableIterator(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetUTF8Text")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetUtf8Text(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetHOCRText")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetHOcrText(nint handle, int pageNumber);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetAltoText")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetAltoText(nint handle, int pageNumber);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetTsvText")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetTsvText(nint handle, int pageNumber);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetBoxText")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetBoxText(nint handle, int pageNumber);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetLSTMBoxText")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetLstmBoxText(nint handle, int pageNumber);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetWordStrBoxText")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetWordStrBoxText(nint handle, int pageNumber);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetUNLVText")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiGetUnlvText(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIMeanTextConf")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiMeanTextConf(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIAllWordConfidences")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessBaseApiAllWordConfidences(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIAdaptToWordStr", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiAdaptToWordStr(nint handle, PageSegmentationMode mode, string wordStr);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIClear")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiClear(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIEnd")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiEnd(nint handle);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIIsValidWord", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessBaseApiIsValidWord(nint handle, string word);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetTextDirection")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessBaseApiGetTextDirection(nint handle, out int offset, out float slope);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPIGetUnichar", StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessBaseApiGetUniChar(nint handle, int uniCharId);

    [LibraryImport(LibraryName, EntryPoint = "TessBaseAPISetMinOrientationMargin")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessBaseApiSetMinOrientationMargin(nint handle, double margin);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorDelete")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessPageIteratorDelete(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorCopy")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessPageIteratorCopy(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorBegin")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessPageIteratorBegin(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorNext")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessPageIteratorNext(nint iterator, PageIteratorLevel level);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorIsAtBeginningOf")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessPageIteratorIsAtBeginningOf(nint iterator, PageIteratorLevel level);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorIsAtFinalElement")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessPageIteratorIsAtFinalElement(nint iterator, PageIteratorLevel level,
        PageIteratorLevel element);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorBoundingBox")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessPageIteratorBoundingBox(nint iterator, PageIteratorLevel level,
        out int left, out int top, out int right, out int bottom);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorBlockType")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial PolygonBlockType TessPageIteratorBlockType(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorGetBinaryImage")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessPageIteratorGetBinaryImage(nint iterator, PageIteratorLevel level);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorGetImage")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessPageIteratorGetImage(nint iterator, PageIteratorLevel level, int padding,
        nint originalImagePtr, out int left, out int top);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorBaseline")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessPageIteratorBaseline(nint iterator, PageIteratorLevel level, out int x1,
        out int y1, out int x2, out int y2);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorOrientation")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessPageIteratorOrientation(
        nint iterator,
        out OrientationPage orientation,
        out WritingDirection writingDirection,
        out TextLineOrder textLineOrder,
        out float deskewAngle);

    [LibraryImport(LibraryName, EntryPoint = "TessPageIteratorParagraphInfo")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessPageIteratorParagraphInfo(
        nint iterator,
        out ParagraphJustification justification,
        [MarshalAs(UnmanagedType.Bool)] out bool isListItem,
        [MarshalAs(UnmanagedType.Bool)] out bool isCrown,
        out int firstLineIndent);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorDelete")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessResultIteratorDelete(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorCopy")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessResultIteratorCopy(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorGetPageIterator")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessResultIteratorGetPageIterator(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorGetPageIteratorConst")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessResultIteratorGetPageIteratorConst(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorGetChoiceIterator")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessResultIteratorGetChoiceIterator(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorNext")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultIteratorNext(nint iterator, PageIteratorLevel level);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorGetUTF8Text")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessResultIteratorGetUtf8Text(nint iterator, PageIteratorLevel level);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorConfidence")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial float TessResultIteratorConfidence(nint iterator, PageIteratorLevel level);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorWordRecognitionLanguage",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string? TessResultIteratorWordRecognitionLanguage(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorWordFontAttributes",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string? TessResultIteratorWordFontAttributes(
        nint iterator,
        [MarshalAs(UnmanagedType.Bool)] out bool isBold,
        [MarshalAs(UnmanagedType.Bool)] out bool isItalic,
        [MarshalAs(UnmanagedType.Bool)] out bool isUnderlined,
        [MarshalAs(UnmanagedType.Bool)] out bool isMonospace,
        [MarshalAs(UnmanagedType.Bool)] out bool isSerif,
        [MarshalAs(UnmanagedType.Bool)] out bool isSmallCaps,
        out int pointSize,
        out int fontId);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorWordIsFromDictionary")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultIteratorWordIsFromDictionary(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorWordIsNumeric")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultIteratorWordIsNumeric(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorSymbolIsSuperscript")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultIteratorSymbolIsSuperscript(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorSymbolIsSubscript")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultIteratorSymbolIsSubscript(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessResultIteratorSymbolIsDropcap")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessResultIteratorSymbolIsDropcap(nint iterator);

    [LibraryImport(LibraryName, EntryPoint = "TessChoiceIteratorDelete")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessChoiceIteratorDelete(nint choiceIterator);

    [LibraryImport(LibraryName, EntryPoint = "TessChoiceIteratorNext")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    [return: MarshalUsing(typeof(TesseractBoolMarshaller))]
    public static partial bool TessChoiceIteratorNext(nint choiceIterator);

    [LibraryImport(LibraryName, EntryPoint = "TessChoiceIteratorGetUTF8Text",
        StringMarshalling = StringMarshalling.Utf8)]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial string TessChoiceIteratorGetUtf8Text(nint choiceIterator);

    [LibraryImport(LibraryName, EntryPoint = "TessChoiceIteratorConfidence")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial float TessChoiceIteratorConfidence(nint choiceIterator);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorCreate")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessMonitorCreate();

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorDelete")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessMonitorDelete(nint monitor);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorSetCancelFunc")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessMonitorSetCancelFunc(nint monitor, nint cancelFunc);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorGetCancelThis")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial nint TessMonitorGetCancelThis(nint monitor);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorSetCancelThis")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessMonitorSetCancelThis(nint monitor, nint cancelThis);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorSetProgressFunc")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessMonitorSetProgressFunc(nint monitor, nint progress);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorGetProgress")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial int TessMonitorGetProgress(nint monitor);

    [LibraryImport(LibraryName, EntryPoint = "TessMonitorSetDeadlineMSecs")]
    [DefaultDllImportSearchPaths(DefauleDllImportSearchPath)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial void TessMonitorSetDeadlineMSecs(nint monitor, int deadline);
}