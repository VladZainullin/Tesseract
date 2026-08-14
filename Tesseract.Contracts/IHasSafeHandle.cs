using System.Runtime.InteropServices;

namespace Tesseract.Contracts;

public interface IHasSafeHandle
{
    SafeHandle Handle { get; }
}