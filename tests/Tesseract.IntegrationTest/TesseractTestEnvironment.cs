namespace Tesseract.IntegrationTests;

internal static class TesseractTestEnvironment
{
    private static readonly object Sync = new();
    private static string? _dataPath;

    public static string Configure()
    {
        lock (Sync)
        {
            if (_dataPath is not null)
                return _dataPath;

            var libraryPath = FindLibraryPath();
            Environment.SetEnvironmentVariable("TESSERACT_LIBRARY_PATH", libraryPath);

            _dataPath = FindDataPath();
            return _dataPath;
        }
    }

    private static string FindLibraryPath()
    {
        var configuredPath = Environment.GetEnvironmentVariable("TESSERACT_LIBRARY_PATH");
        if (!string.IsNullOrWhiteSpace(configuredPath))
        {
            if (File.Exists(configuredPath))
                return configuredPath;

            throw new InvalidOperationException(
                $"TESSERACT_LIBRARY_PATH points to a missing file: '{configuredPath}'.");
        }

        var candidates = new[]
        {
            "/opt/homebrew/lib/libtesseract.dylib",
            "/usr/local/lib/libtesseract.dylib",
            "/usr/lib/x86_64-linux-gnu/libtesseract.so.5",
            "/usr/lib/aarch64-linux-gnu/libtesseract.so.5",
            "/usr/lib64/libtesseract.so.5",
            "/usr/lib/libtesseract.so.5",
        };

        return candidates.FirstOrDefault(File.Exists)
               ?? throw new InvalidOperationException(
                   "Tesseract native library was not found. Set TESSERACT_LIBRARY_PATH to its full path.");
    }

    private static string FindDataPath()
    {
        var configuredPath = Environment.GetEnvironmentVariable("TESSDATA_PREFIX");
        if (!string.IsNullOrWhiteSpace(configuredPath))
        {
            var normalizedPath = NormalizeDataPath(configuredPath);
            if (normalizedPath is not null)
                return normalizedPath;

            throw new InvalidOperationException(
                $"TESSDATA_PREFIX does not contain eng.traineddata: '{configuredPath}'.");
        }

        var candidates = new[]
        {
            "/opt/homebrew/share/tessdata",
            "/usr/local/share/tessdata",
            "/usr/share/tesseract-ocr/5/tessdata",
            "/usr/share/tesseract-ocr/4.00/tessdata",
            "/usr/share/tessdata",
        };

        return candidates.Select(NormalizeDataPath).FirstOrDefault(path => path is not null)
               ?? throw new InvalidOperationException(
                   "English trained data was not found. Set TESSDATA_PREFIX to a directory containing eng.traineddata.");
    }

    private static string? NormalizeDataPath(string path)
    {
        var fullPath = Path.GetFullPath(path);
        if (File.Exists(Path.Combine(fullPath, "eng.traineddata")))
            return fullPath;

        var tessdataPath = Path.Combine(fullPath, "tessdata");
        return File.Exists(Path.Combine(tessdataPath, "eng.traineddata")) ? tessdataPath : null;
    }
}
