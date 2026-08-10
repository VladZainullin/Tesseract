namespace Leptonica.Contracts;

public interface IPix : IDisposable
{
    nint Handle { get; }

    int Width { get; }

    int Height { get; }

    int Depth { get; }
}
