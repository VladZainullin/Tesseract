namespace Tesseract.Contracts;

public interface ITesseractMonitor : IHasSafeHandle, IDisposable
{
    int Progress { get; }

    void SetDeadline(int milliseconds);
}
