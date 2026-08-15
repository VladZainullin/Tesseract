using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Leptonica;
using Leptonica.Contracts;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractEngine : IDisposable, ITesseractEngine
{
    public SafeHandle Handle { get; } = new TesseractEngineSafeHandle(TesseractNative.TessBaseApiCreate(), true);

    public static string Version => TesseractNative.TessVersion();

    public string GetDataPath() => TesseractNative.TessBaseApiGetDataPath(Handle);

    public PageSegmentationMode PageSegmentationMode => TesseractNative.TessBaseApiGetPageSegMode(Handle);

    public ITesseractResultRenderer TextRendererCreate(string outputName)
    {
        var rendererPtr = TesseractNative.TessTextRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer HOcrRendererCreate(string outputName)
    {
        var rendererPtr = TesseractNative.TessHOcrRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer HOcrRendererCreate(string outputName, bool fontInfo)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessHOcrRendererCreate2(outputName, fontInfo);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer AltoRendererCreate(string outputName)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessAltoRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer TsvRendererCreate(string outputName)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessTsvRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer PdfRendererCreate(string outputName, string dataDir, bool textOnly)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        ArgumentException.ThrowIfNullOrEmpty(dataDir);
        var rendererPtr = TesseractNative.TessPdfRendererCreate(outputName, dataDir, textOnly);
        return new TesseractResultRenderer(rendererPtr);
    }

    public IReadOnlyList<string> GetLoadedLanguages()
    {
        var listPtr = TesseractNative.TessBaseApiGetLoadedLanguagesAsVector(Handle);
        if (listPtr == nint.Zero)
        {
            TesseractNative.TessDeleteTextArray(listPtr);
            return Array.Empty<string>();
        }

        try
        {
            var languages = new List<string>();

            for (var index = 0;; index++)
            {
                var stringPointer = Marshal.ReadIntPtr(listPtr, index * nint.Size);
                if (stringPointer == nint.Zero) break;

                var language = Marshal.PtrToStringUTF8(stringPointer);
                if (language is not null) languages.Add(language);
            }

            return languages.AsReadOnly();
        }
        finally
        {
            TesseractNative.TessDeleteTextArray(listPtr);
        }
    }

    public IReadOnlyList<string> GetAvailableLanguages()
    {
        var listPtr = TesseractNative.TessBaseApiGetAvailableLanguagesAsVector(Handle);
        if (listPtr == nint.Zero)
        {
            TesseractNative.TessDeleteTextArray(listPtr);
            return Array.Empty<string>();
        }

        try
        {
            var languages = new List<string>();

            for (var index = 0;; index++)
            {
                var stringPointer = Marshal.ReadIntPtr(listPtr, index * nint.Size);
                if (stringPointer == nint.Zero) break;

                var language = Marshal.PtrToStringUTF8(stringPointer);
                if (language is not null) languages.Add(language);
            }

            return languages.AsReadOnly();
        }
        finally
        {
            TesseractNative.TessDeleteTextArray(listPtr);
        }
    }

    public ITesseractResultRenderer UnlvRendererCreate(string outputName)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessUnlvRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer BoxTextRendererCreate(string outputName)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessBoxTextRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer WordStrBoxRendererCreate(string outputName)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessWordStrBoxRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer LstmBoxRendererCreate(string outputName)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessLstmBoxRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public void SetVariable(string name, string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(value);
        TesseractNative.TessBaseApiSetVariable(Handle, name, value);
    }

    public void SetDebugVariable(string name, string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(value);
        TesseractNative.TessBaseApiSetDebugVariable(Handle, name, value);
    }

    public void SetInputName(IPix pix)
    {
        ArgumentNullException.ThrowIfNull(pix);
        TesseractNative.TessBaseApiSetInputImage(Handle, pix.Handle);
    }

    public string GetVariable(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        return TesseractNative.TessBaseApiGetStringVariable(Handle, name);
    }

    public bool TryGetVariable(string name, out int? value)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        if (TesseractNative.TessBaseApiGetIntVariable(Handle, name, out var nativeValue))
        {
            value = nativeValue;
            return true;
        }

        value = null;
        return false;
    }

    public bool TryGetVariable(string name, out double? value)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        if (TesseractNative.TessBaseApiGetDoubleVariable(Handle, name, out var nativeValue))
        {
            value = nativeValue;
            return true;
        }

        value = null;
        return false;
    }

    public bool TryGetVariable(string name, out bool? value)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        if (TesseractNative.TessBaseApiGetBoolVariable(Handle, name, out var v))
        {
            value = v;
            return true;
        }

        value = null;
        return false;
    }

    public void SetInputName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        TesseractNative.TessBaseApiSetInputName(Handle, name);
    }

    public string InputName => TesseractNative.TessBaseApiGetInputName(Handle);

    public string? Text
    {
        get
        {
            var textPtr = TesseractNative.TessBaseApiGetUtf8Text(Handle);
            try
            {
                return Marshal.PtrToStringUTF8(textPtr);
            }
            finally
            {
                TesseractNative.TessDeleteText(textPtr);
            }
        }
    }

    public float MeanTextConfidence => TesseractNative.TessBaseApiMeanTextConf(Handle);

    public string? GetHOcrText(int pageNumber)
    {
        var textPtr = TesseractNative.TessBaseApiGetHOcrText(Handle, pageNumber);
        try
        {
            return Marshal.PtrToStringUTF8(textPtr);
        }
        finally
        {
            TesseractNative.TessDeleteText(textPtr);
        }
    }

    public string? GetAltoText(int pageNumber)
    {
        var textPtr = TesseractNative.TessBaseApiGetAltoText(Handle, pageNumber);
        try
        {
            return Marshal.PtrToStringUTF8(textPtr);
        }
        finally
        {
            TesseractNative.TessDeleteText(textPtr);
        }
    }

    public string? GetTsvText(int pageNumber)
    {
        var textPtr = TesseractNative.TessBaseApiGetTsvText(Handle, pageNumber);
        try
        {
            return Marshal.PtrToStringUTF8(textPtr);
        }
        finally
        {
            TesseractNative.TessDeleteText(textPtr);
        }
    }

    public string? GetLstmText(int pageNumber)
    {
        var textPtr = TesseractNative.TessBaseApiGetLstmBoxText(Handle, pageNumber);
        try
        {
            return Marshal.PtrToStringUTF8(textPtr);
        }
        finally
        {
            TesseractNative.TessDeleteText(textPtr);
        }
    }

    public string? GetBoxText(int pageNumber)
    {
        var textPtr = TesseractNative.TessBaseApiGetBoxText(Handle, pageNumber);
        try
        {
            return Marshal.PtrToStringUTF8(textPtr);
        }
        finally
        {
            TesseractNative.TessDeleteText(textPtr);
        }
    }

    public void SetSegmentationMode(PageSegmentationMode mode)
    {
        TesseractNative.TessBaseApiSetPageSegMode(Handle, mode);
    }
    
    public bool TryInitialization(string dataPath, string language, OcrEngineMode oem)
    {
        ArgumentException.ThrowIfNullOrEmpty(dataPath);
        ArgumentException.ThrowIfNullOrEmpty(language);
        return TesseractNative.TessBaseApiInit2(Handle, dataPath, language, oem) == 0;
    }

    public bool TryInitialization(string dataPath, string language)
    {
        ArgumentException.ThrowIfNullOrEmpty(dataPath);
        ArgumentNullException.ThrowIfNull(language);
        return TesseractNative.TessBaseApiInit3(Handle, dataPath, language) == 0;
    }

    public int GetSourceYResolution()
    {
        return TesseractNative.TessBaseApiGetSourceYResolution(Handle);
    }

    public void SetSourceResolution(int ppi)
    {
        TesseractNative.TessBaseApiSetSourceResolution(Handle, ppi);
    }

    public void SetImage(IPix image)
    {
        ArgumentNullException.ThrowIfNull(image);
        TesseractNative.TessBaseApiSetImage2(Handle, image.Handle);
    }

    public bool TryRecognize(ITesseractMonitor monitor)
    {
        ArgumentNullException.ThrowIfNull(monitor);
        return TesseractNative.TessBaseApiRecognize(Handle, monitor.Handle) == 0;
    }

    public void SetRectangle(int left, int top, int width, int height)
    {
        TesseractNative.TessBaseApiSetRectangle(Handle, left, top, width, height);
    }

    public unsafe void SetImage(byte[] imageData, int width, int height, int bytesPerPixel)
    {
        ArgumentNullException.ThrowIfNull(imageData);

        var bytesPerLine = width * bytesPerPixel;
        fixed (byte* imagePtr = imageData)
        {
            TesseractNative.TessBaseApiSetImage(Handle, (nint)imagePtr, width, height, bytesPerPixel, bytesPerLine);
        }
    }

    public string GetInitializationLanguages()
    {
        return TesseractNative.TessBaseApiGetInitLanguagesAsString(Handle);
    }

    public ITesseractResultIterator GetIterator()
    {
        var iterator = TesseractNative.TessBaseApiGetIterator(Handle);
        return new TesseractResultIterator(new TesseractResultIteratorSafeHandle(iterator, true));
    }

    public ITesseractPageIterator AnalyzeLayout()
    {
        var iterator = TesseractNative.TessBaseApiAnalyseLayout(Handle);
        return new TesseractPageIterator(new TesseractResultIteratorSafeHandle(iterator, false));
    }

    public bool TryGetTextDirection(out int outOffset, out float slope)
    {
        return TesseractNative.TessBaseApiGetTextDirection(Handle, out outOffset, out slope);
    }

    public string GetUniChar(int uniCharId)
    {
        return TesseractNative.TessBaseApiGetUniChar(Handle, uniCharId);
    }

    public void SetMinimumOrientationMargin(double margin)
    {
        TesseractNative.TessBaseApiSetMinOrientationMargin(Handle, margin);
    }

    public void EndElement()
    {
        TesseractNative.TessBaseApiEnd(Handle);
    }

    public void Clear()
    {
        TesseractNative.TessBaseApiClear(Handle);
    }

    public bool IsValidWord(string word)
    {
        return TesseractNative.TessBaseApiIsValidWord(Handle, word) != 0;
    }

    public IPix GetThresholdedImage()
    {
        var pixPtr = TesseractNative.TessBaseApiGetThresholdedImage(Handle);
        return Pix.FromHandle(pixPtr);
    }

    public void Dispose()
    {
        Handle.Dispose();
    }
}