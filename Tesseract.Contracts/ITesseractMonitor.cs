namespace Tesseract.Contracts;

/// <summary>
/// Monitors the progress and execution deadline of a Tesseract recognition operation.
/// </summary>
/// <remarks>
/// Pass an instance to <see cref="ITesseractEngine.TryRecognize"/> and keep it alive until that method returns.
/// The monitor owns a native resource and must be disposed after the recognition operation has completed.
/// </remarks>
public interface ITesseractMonitor : IHasSafeHandle, IDisposable
{
    /// <summary>
    /// Gets the current recognition progress as a percentage.
    /// </summary>
    /// <value>
    /// An integer from <c>0</c> through <c>100</c>. The value is meaningful while recognition is running,
    /// and after it has completed.
    /// </value>
    int Progress { get; }

    /// <summary>
    /// Sets the maximum time allowed for recognition, measured from the moment this method is called.
    /// </summary>
    /// <param name="milliseconds">
    /// The deadline interval in milliseconds. A value of <c>0</c> requests an immediate deadline.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="milliseconds"/> is negative.
    /// </exception>
    /// <remarks>
    /// Set the deadline immediately before calling <see cref="ITesseractEngine.TryRecognize"/>.
    /// Reaching the deadline requests cancellation from the native Tesseract engine; callers should inspect
    /// the recognition method's return value to determine whether recognition completed successfully.
    /// </remarks>
    void SetDeadline(int milliseconds);
}
