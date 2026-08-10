namespace Leptonica.Contracts;

/// <summary>
/// Specifies how a colormap must be removed from a PIX image.
/// Values correspond to the REMOVE_CMAP_* constants declared by Leptonica.
/// </summary>
public enum LeptonicaRemoveColormapMode
{
    ToBinary = 0,
    ToGray = 1,
    ToFullColor = 2,
    BasedOnSource = 3
}
