using Leptonica.Contracts;
using Leptonica.Native;

namespace Leptonica;

public sealed class Pix : IPix
{
    private readonly ILeptonicaNativeApi _api;
    private readonly SafePixHandle _handle;

    private Pix(
        SafePixHandle handle,
        ILeptonicaNativeApi api)
    {
        _handle = handle;
        _api = api;
    }

    public nint Handle
    {
        get
        {
#pragma warning disable CA1513
            if (_handle.IsClosed)
            {
                throw new ObjectDisposedException(nameof(Pix));
            }
#pragma warning restore CA1513

            return _handle.DangerousGetHandle();
        }
    }

    public int Width => _api.PixGetWidth(Handle);

    public int Height => _api.PixGetHeight(Handle);

    public int Depth => _api.PixGetDepth(Handle);

    public int WordsPerLine => _api.PixGetWordsPerLine(Handle);

    public int XResolution => _api.PixGetXResolution(Handle);

    public int YResolution => _api.PixGetYResolution(Handle);

    public static Pix Create(
        int width,
        int height,
        int depth)
    {
        ThrowIfNegativeOrZero(
            width,
            nameof(width));
        ThrowIfNegativeOrZero(
            height,
            nameof(height));
        ThrowIfNegativeOrZero(
            depth,
            nameof(depth));

        var api = LeptonicaNativeApiProvider.Current;

        return FromOwnedHandle(
            api,
            api.PixCreate(
                width,
                height,
                depth),
            "pixCreate");
    }

    public static unsafe Pix Read(
        ReadOnlySpan<byte> data)
    {
        if (data.IsEmpty)
        {
            throw new ArgumentException(
                "Image data cannot be empty.",
                nameof(data));
        }

        fixed (byte* ptr = data)
        {
            var api = LeptonicaNativeApiProvider.Current;

            return FromOwnedHandle(
                api,
                api.PixReadMem(
                    (nint)ptr,
                    checked((nuint)data.Length)),
                "pixReadMem");
        }
    }

    public static Pix Read(
        string filename)
    {
        ThrowIfNullOrWhiteSpace(
            filename,
            nameof(filename));

        var api = LeptonicaNativeApiProvider.Current;

        return FromOwnedHandle(
            api,
            api.PixRead(filename),
            "pixRead");
    }

    public static Pix FromHandle(
        nint handle,
        bool ownsHandle = true)
    {
        return FromHandle(
            LeptonicaNativeApiProvider.Current,
            handle,
            ownsHandle,
            "PIX handle");
    }

    public Pix Clone()
    {
        return FromOwnedHandle(
            _api,
            _api.PixClone(Handle),
            "pixClone");
    }

    public Pix Copy()
    {
        return FromOwnedHandle(
            _api,
            _api.PixCopy(
                nint.Zero,
                Handle),
            "pixCopy");
    }

    public void Save(
        string filename,
        LeptonicaImageFormat format)
    {
        ThrowIfNullOrWhiteSpace(
            filename,
            nameof(filename));

        ThrowIfError(
            _api.PixWrite(
                filename,
                Handle,
                format),
            "pixWrite");
    }

    public void SetResolution(
        int xResolution,
        int yResolution)
    {
        ThrowIfNegative(
            xResolution,
            nameof(xResolution));
        ThrowIfNegative(
            yResolution,
            nameof(yResolution));

        ThrowIfError(
            _api.PixSetResolution(
                Handle,
                xResolution,
                yResolution),
            "pixSetResolution");
    }

    public uint GetPixel(
        int x,
        int y)
    {
        ThrowIfError(
            _api.PixGetPixel(
                Handle,
                x,
                y,
                out var value),
            "pixGetPixel");

        return value;
    }

    public void SetPixel(
        int x,
        int y,
        uint value)
    {
        ThrowIfError(
            _api.PixSetPixel(
                Handle,
                x,
                y,
                value),
            "pixSetPixel");
    }

    public Pix ConvertTo8(
        bool createColormap = false)
    {
        return FromOwnedHandle(
            _api,
            _api.PixConvertTo8(
                Handle,
                createColormap ? 1 : 0),
            "pixConvertTo8");
    }

    public Pix ConvertTo32()
    {
        return FromOwnedHandle(
            _api,
            _api.PixConvertTo32(Handle),
            "pixConvertTo32");
    }

    public Pix ConvertRgbToGray(
        float redWeight = 0,
        float greenWeight = 0,
        float blueWeight = 0)
    {
        return FromOwnedHandle(
            _api,
            _api.PixConvertRgbToGray(
                Handle,
                redWeight,
                greenWeight,
                blueWeight),
            "pixConvertRGBToGray");
    }

    public Pix RemoveColormap(
        LeptonicaRemoveColormapMode mode)
    {
        return FromOwnedHandle(
            _api,
            _api.PixRemoveColormap(
                Handle,
                mode),
            "pixRemoveColormap");
    }

    public Pix ThresholdToBinary(
        int threshold)
    {
        ThrowIfNegative(
            threshold,
            nameof(threshold));
        ThrowIfGreaterThan(
            threshold,
            nameof(threshold),
            256);

        return FromOwnedHandle(
            _api,
            _api.PixThresholdToBinary(
                Handle,
                threshold),
            "pixThresholdToBinary");
    }

    public Pix OtsuAdaptiveThreshold(
        int sx,
        int sy,
        int smoothX,
        int smoothY,
        float scoreFraction,
        out Pix? thresholdMap)
    {
        ThrowIfError(
            _api.PixOtsuAdaptiveThreshold(
                Handle,
                sx,
                sy,
                smoothX,
                smoothY,
                scoreFraction,
                out var thresholdMapHandle,
                out var destinationHandle),
            "pixOtsuAdaptiveThreshold");

        Pix? thresholdMapPix = null;

        try
        {
            thresholdMapPix = thresholdMapHandle == nint.Zero
                ? null
                : FromOwnedHandle(
                    _api,
                    thresholdMapHandle,
                    "pixOtsuAdaptiveThreshold threshold map");

            var destination = FromOwnedHandle(
                _api,
                destinationHandle,
                "pixOtsuAdaptiveThreshold destination");

            thresholdMap = thresholdMapPix;

            return destination;
        }
        catch
        {
            thresholdMapPix?.Dispose();

            if (destinationHandle != nint.Zero)
            {
                var pix = destinationHandle;
                _api.PixDestroy(ref pix);
            }

            throw;
        }
    }

    public Pix Scale(
        float scaleX,
        float scaleY)
    {
        ThrowIfNegativeOrZero(
            scaleX,
            nameof(scaleX));
        ThrowIfNegativeOrZero(
            scaleY,
            nameof(scaleY));

        return FromOwnedHandle(
            _api,
            _api.PixScale(
                Handle,
                scaleX,
                scaleY),
            "pixScale");
    }

    public Pix RotateOrthogonal(
        int quarterTurns)
    {
        ThrowIfLessThan(
            quarterTurns,
            nameof(quarterTurns),
            0);
        ThrowIfGreaterThan(
            quarterTurns,
            nameof(quarterTurns),
            3);

        return FromOwnedHandle(
            _api,
            _api.PixRotateOrth(
                Handle,
                quarterTurns),
            "pixRotateOrth");
    }

    public Pix Deskew(
        int reduction)
    {
        ThrowIfNegativeOrZero(
            reduction,
            nameof(reduction));

        return FromOwnedHandle(
            _api,
            _api.PixDeskew(
                Handle,
                reduction),
            "pixDeskew");
    }

    public void Dispose()
    {
        _handle.Dispose();
    }

    private static Pix FromOwnedHandle(
        ILeptonicaNativeApi api,
        nint handle,
        string operation)
    {
        return FromHandle(
            api,
            handle,
            ownsHandle: true,
            operation);
    }

    private static Pix FromHandle(
        ILeptonicaNativeApi api,
        nint handle,
        bool ownsHandle,
        string operation)
    {
        if (handle == nint.Zero)
        {
            throw new LeptonicaException(
                $"{operation} returned a null PIX pointer.");
        }

        return new Pix(
            new SafePixHandle(
                handle,
                ownsHandle,
                api),
            api);
    }

    private static void ThrowIfError(
        int result,
        string operation)
    {
        if (result != 0)
        {
            throw new LeptonicaException(
                $"{operation} failed with Leptonica status code {result}.");
        }
    }

    private static void ThrowIfNullOrWhiteSpace(
        string value,
        string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Value cannot be null or whitespace.",
                parameterName);
        }
    }

    private static void ThrowIfNegative(
        int value,
        string parameterName)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(
                parameterName,
                value,
                "Value cannot be negative.");
        }
    }

    private static void ThrowIfNegativeOrZero(
        int value,
        string parameterName)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(
                parameterName,
                value,
                "Value must be greater than zero.");
        }
    }

    private static void ThrowIfNegativeOrZero(
        float value,
        string parameterName)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(
                parameterName,
                value,
                "Value must be greater than zero.");
        }
    }

    private static void ThrowIfLessThan(
        int value,
        string parameterName,
        int minimum)
    {
        if (value < minimum)
        {
            throw new ArgumentOutOfRangeException(
                parameterName,
                value,
                $"Value must be greater than or equal to {minimum}.");
        }
    }

    private static void ThrowIfGreaterThan(
        int value,
        string parameterName,
        int maximum)
    {
        if (value > maximum)
        {
            throw new ArgumentOutOfRangeException(
                parameterName,
                value,
                $"Value must be less than or equal to {maximum}.");
        }
    }
}
