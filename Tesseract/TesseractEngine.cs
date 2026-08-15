using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Leptonica;
using Leptonica.Contracts;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractEngine : ITesseractEngine
{
    public SafeHandle Handle { get; } = TesseractNative.TessBaseApiCreate();

    public static string? Version
    {
        get
        {
            var pointer = TesseractNative.TessVersion();
            return Marshal.PtrToStringUTF8(pointer);
        }
    }

    public string GetDataPath() => Marshal.PtrToStringUTF8(TesseractNative.TessBaseApiGetDataPath(Handle))!;

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

    public ITesseractResultRenderer CreateAltoRenderer(string outputName)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessAltoRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer CreateTsvRenderer(string outputName)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessTsvRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer CreatePdfRenderer(string outputName, string dataDir, bool textOnly)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        ArgumentException.ThrowIfNullOrEmpty(dataDir);
        var rendererPtr = TesseractNative.TessPdfRendererCreate(outputName, dataDir, textOnly);
        return new TesseractResultRenderer(rendererPtr);
    }

    public IReadOnlyList<string> GetLoadedLanguages()
    {
        using var listPtr = TesseractNative.TessBaseApiGetLoadedLanguagesAsVector(Handle);
        return listPtr.ToManagedStrings();
    }

    public IReadOnlyList<string> GetAvailableLanguages()
    {
        using var listPtr = TesseractNative.TessBaseApiGetAvailableLanguagesAsVector(Handle);
        return listPtr.ToManagedStrings();
    }

    public ITesseractResultRenderer CreateUnlvRenderer(string outputName)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessUnlvRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer CreateBoxTextRenderer(string outputName)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessBoxTextRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer CreateWordStrBoxRenderer(string outputName)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessWordStrBoxRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public ITesseractResultRenderer CreateLstmBoxRenderer(string outputName)
    {
        ArgumentException.ThrowIfNullOrEmpty(outputName);
        var rendererPtr = TesseractNative.TessLstmBoxRendererCreate(outputName);
        return new TesseractResultRenderer(rendererPtr);
    }

    public bool TrySetVariable(string name, string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(value);
        return TesseractNative.TessBaseApiSetVariable(Handle, name, value);
    }

    public bool TrySetDebugVariable(string name, string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(value);
        return TesseractNative.TessBaseApiSetDebugVariable(Handle, name, value);
    }

    public void SetInputImage(IPix pix)
    {
        ArgumentNullException.ThrowIfNull(pix);
        TesseractNative.TessBaseApiSetInputImage(Handle, pix.Handle);
    }

    public string? GetVariable(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        return Marshal.PtrToStringUTF8(TesseractNative.TessBaseApiGetStringVariable(Handle, name));
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

    public string? InputName => Marshal.PtrToStringUTF8(TesseractNative.TessBaseApiGetInputName(Handle));

    public string? Text
    {
        get
        {
            using var text = TesseractNative.TessBaseApiGetUtf8Text(Handle);
            return text.ToManagedString();
        }
    }

    public int MeanTextConfidence => TesseractNative.TessBaseApiMeanTextConf(Handle);

    public string? GetHOcrText(int pageNumber)
    {
        using var text = TesseractNative.TessBaseApiGetHOcrText(Handle, pageNumber);
        return text.ToManagedString();
    }

    public string? GetAltoText(int pageNumber)
    {
        using var text = TesseractNative.TessBaseApiGetAltoText(Handle, pageNumber);
        return text.ToManagedString();
    }

    public string? GetTsvText(int pageNumber)
    {
        using var text = TesseractNative.TessBaseApiGetTsvText(Handle, pageNumber);
        return text.ToManagedString();
    }

    public string? GetLstmText(int pageNumber)
    {
        using var text = TesseractNative.TessBaseApiGetLstmBoxText(Handle, pageNumber);
        return text.ToManagedString();
    }

    public string? GetBoxText(int pageNumber)
    {
        using var text = TesseractNative.TessBaseApiGetBoxText(Handle, pageNumber);
        return text.ToManagedString();
    }

    public void SetSegmentationMode(PageSegmentationMode mode)
    {
        TesseractNative.TessBaseApiSetPageSegMode(Handle, mode);
    }
    
    public bool TryInitialize(string dataPath, string language, OcrEngineMode oem)
    {
        ArgumentException.ThrowIfNullOrEmpty(dataPath);
        ArgumentException.ThrowIfNullOrEmpty(language);
        return TesseractNative.TessBaseApiInit2(Handle, dataPath, language, oem) == 0;
    }

    public bool TryInitialize(string dataPath, string language)
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
        return Marshal.PtrToStringUTF8(TesseractNative.TessBaseApiGetInitLanguagesAsString(Handle))!;
    }

    public ITesseractResultIterator GetIterator()
    {
        var iterator = TesseractNative.TessBaseApiGetIterator(Handle);
        return new TesseractResultIterator(iterator);
    }

    public ITesseractPageIterator AnalyzeLayout()
    {
        var iterator = TesseractNative.TessBaseApiAnalyseLayout(Handle);
        return new TesseractPageIterator(iterator);
    }

    public bool TryGetTextDirection(out int outOffset, out float slope)
    {
        return TesseractNative.TessBaseApiGetTextDirection(Handle, out outOffset, out slope);
    }

    public string GetUniChar(int uniCharId)
    {
        return Marshal.PtrToStringUTF8(TesseractNative.TessBaseApiGetUniChar(Handle, uniCharId))!;
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
        return new Pix(pixPtr);
    }

    public void Dispose()
    {
        Handle.Dispose();
    }
}
