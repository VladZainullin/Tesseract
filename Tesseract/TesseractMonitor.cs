using System;
using System.Runtime.InteropServices;
using Tesseract.Contracts;

namespace Tesseract;

public sealed class TesseractMonitor : ITesseractMonitor
{
    private readonly TesseractMonitorSafeHandle _handle;

    public TesseractMonitor()
    {
        _handle = TesseractNative.TessMonitorCreate();
        if (!_handle.IsInvalid) return;

        _handle.Dispose();
        throw new InvalidOperationException("TessMonitorCreate returned an invalid handle.");
    }

    public SafeHandle Handle => _handle;

    public int Progress => TesseractNative.TessMonitorGetProgress(_handle);

    public void SetDeadline(int milliseconds)
    {
        if (milliseconds < 0)
            throw new ArgumentOutOfRangeException(nameof(milliseconds), milliseconds,
                "The deadline must be non-negative.");

        TesseractNative.TessMonitorSetDeadlineMSecs(_handle, milliseconds);
    }

    public void Dispose()
    {
        _handle.Dispose();
    }
}
