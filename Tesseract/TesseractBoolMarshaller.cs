using System.Runtime.InteropServices.Marshalling;

namespace Tesseract;

[CustomMarshaller(typeof(bool), MarshalMode.ManagedToUnmanagedOut, typeof(TesseractBoolMarshaller))]
public static class TesseractBoolMarshaller
{
    public static bool ConvertToManaged(int nativeValue)
    {
        return nativeValue != 0;
    }
}