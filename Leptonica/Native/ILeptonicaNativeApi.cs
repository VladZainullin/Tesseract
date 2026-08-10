using Leptonica.Contracts;

namespace Leptonica.Native;

internal interface ILeptonicaNativeApi
{
    nint GetLeptonicaVersion();

    nint PixCreate(
        int width,
        int height,
        int depth);

    nint PixCreateHeader(
        int width,
        int height,
        int depth);

    nint PixCreateTemplate(
        nint source);

    nint PixClone(
        nint source);

    nint PixCopy(
        nint destination,
        nint source);

    void PixDestroy(
        ref nint pix);

    nint PixRead(
        string filename);

    nint PixReadMem(
        nint data,
        nuint size);

    int PixWrite(
        string filename,
        nint pix,
        LeptonicaImageFormat format);

    int PixGetDimensions(
        nint pix,
        out int width,
        out int height,
        out int depth);

    int PixGetWidth(
        nint pix);

    int PixGetHeight(
        nint pix);

    int PixGetDepth(
        nint pix);

    int PixGetWordsPerLine(
        nint pix);

    nint PixGetData(
        nint pix);

    int PixSetData(
        nint pix,
        nint data);

    int PixGetPixel(
        nint pix,
        int x,
        int y,
        out uint value);

    int PixSetPixel(
        nint pix,
        int x,
        int y,
        uint value);

    int PixSetResolution(
        nint pix,
        int xResolution,
        int yResolution);

    int PixGetXResolution(
        nint pix);

    int PixGetYResolution(
        nint pix);

    nint PixConvertTo8(
        nint source,
        int cmapFlag);

    nint PixConvertTo32(
        nint source);

    nint PixConvertRgbToGray(
        nint source,
        float redWeight,
        float greenWeight,
        float blueWeight);

    nint PixRemoveColormap(
        nint source,
        LeptonicaRemoveColormapMode type);

    nint PixThresholdToBinary(
        nint source,
        int threshold);

    int PixOtsuAdaptiveThreshold(
        nint source,
        int sx,
        int sy,
        int smoothX,
        int smoothY,
        float scoreFraction,
        out nint thresholdMap,
        out nint destination);

    nint PixScale(
        nint source,
        float scaleX,
        float scaleY);

    nint PixRotateOrth(
        nint source,
        int quarterTurns);

    nint PixDeskew(
        nint source,
        int reduction);
}
