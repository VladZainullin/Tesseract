namespace Leptonica.Contracts;

/// <summary>
/// Image format identifiers used by Leptonica.
/// Values correspond to the IFF_* constants declared by Leptonica.
/// </summary>
public enum LeptonicaImageFormat
{
    Unknown = 0,
    Bmp = 1,
    Jpeg = 2,
    Png = 3,
    Tiff = 4,
    TiffPackbits = 5,
    TiffRle = 6,
    TiffG3 = 7,
    TiffG4 = 8,
    TiffLzw = 9,
    TiffZip = 10,
    Pnm = 11,
    Ps = 12,
    Gif = 13,
    Jp2 = 14,
    WebP = 15,
    Lpdf = 16,
    Default = 17,
    SpiX = 18
}
