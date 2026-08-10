using System.Reflection;
using System.Runtime.InteropServices;

namespace Leptonica.Native;

internal static class LeptonicaNativeLibrary
{
    internal const string LogicalName = "leptonica";

    internal static nint Resolve(
        string libraryName,
        Assembly assembly,
        DllImportSearchPath? searchPath)
    {
        if (!string.Equals(
                libraryName,
                LogicalName,
                StringComparison.Ordinal))
        {
            return nint.Zero;
        }

        foreach (var candidate in GetCandidatePaths())
        {
            if (NativeLibrary.TryLoad(
                    candidate,
                    out var handle))
            {
                return handle;
            }
        }

        foreach (var candidate in GetCandidateNames())
        {
            if (NativeLibrary.TryLoad(
                    candidate,
                    assembly,
                    searchPath,
                    out var handle))
            {
                return handle;
            }
        }

        return nint.Zero;
    }

    internal static nint Load()
    {
        foreach (var candidate in GetCandidatePaths())
        {
            if (NativeLibrary.TryLoad(
                    candidate,
                    out var handle))
            {
                return handle;
            }
        }

        foreach (var candidate in GetCandidateNames())
        {
            if (NativeLibrary.TryLoad(
                    candidate,
                    out var handle))
            {
                return handle;
            }
        }

        throw new DllNotFoundException(
            "Unable to load the Leptonica native library.");
    }

    private static IEnumerable<string> GetCandidatePaths()
    {
        foreach (var name in GetCandidateNames())
        {
            yield return Path.Combine(
                AppContext.BaseDirectory,
                name);

            yield return Path.Combine(
                AppContext.BaseDirectory,
                GetArchitectureDirectoryName(),
                name);
        }
    }

    private static IEnumerable<string> GetCandidateNames()
    {
        if (OperatingSystem.IsWindows())
        {
            yield return "leptonica-1.82.0.dll";
            yield return "leptonica-1.87.0.dll";
            yield return "libleptonica-1.82.0.dll";
            yield return "leptonica.dll";
            yield return "liblept.dll";

            yield break;
        }

        if (OperatingSystem.IsMacOS())
        {
            yield return "libleptonica.6.dylib";
            yield return "libleptonica.dylib";
            yield return "libleptonica-1.82.0.dylib";
            yield return "liblept.dylib";
            yield return "/opt/homebrew/lib/libleptonica.dylib";
            yield return "/usr/local/lib/libleptonica.dylib";

            yield break;
        }

        yield return "libleptonica.so.6";
        yield return "libleptonica.so";
        yield return "liblept.so.5";
        yield return "liblept.so";
    }

    private static string GetArchitectureDirectoryName()
    {
        return RuntimeInformation.ProcessArchitecture switch
        {
            Architecture.X64 => "x64",
            Architecture.X86 => "x86",
            Architecture.Arm64 => "arm64",
            Architecture.Arm => "arm",
            _ => RuntimeInformation.ProcessArchitecture.ToString()
        };
    }
}
