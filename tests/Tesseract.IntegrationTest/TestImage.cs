namespace Tesseract.IntegrationTests;

internal static class TestImage
{
    private static readonly string[][] Glyphs =
    {
        new[] { "11111", "00100", "00100", "00100", "00100", "00100", "00100" },
        new[] { "11111", "10000", "10000", "11110", "10000", "10000", "11111" },
        new[] { "01111", "10000", "10000", "01110", "00001", "00001", "11110" },
        new[] { "11111", "00100", "00100", "00100", "00100", "00100", "00100" },
    };

    public const int Width = 348;
    public const int Height = 124;

    public static byte[] Create()
    {
        const int scale = 12;
        const int leftMargin = 24;
        const int topMargin = 20;
        const int glyphSpacing = 2;
        var pixels = Enumerable.Repeat(byte.MaxValue, Width * Height).ToArray();

        for (var glyphIndex = 0; glyphIndex < Glyphs.Length; glyphIndex++)
        {
            var glyph = Glyphs[glyphIndex];
            for (var row = 0; row < glyph.Length; row++)
            {
                for (var column = 0; column < glyph[row].Length; column++)
                {
                    if (glyph[row][column] != '1')
                        continue;

                    var originX = leftMargin + (glyphIndex * (5 + glyphSpacing) + column) * scale;
                    var originY = topMargin + row * scale;
                    FillBlock(pixels, originX, originY, scale);
                }
            }
        }

        return pixels;
    }

    private static void FillBlock(byte[] pixels, int originX, int originY, int size)
    {
        for (var y = originY; y < originY + size; y++)
        for (var x = originX; x < originX + size; x++)
            pixels[y * Width + x] = 0;
    }
}
