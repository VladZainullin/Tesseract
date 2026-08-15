using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public sealed class TesseractStringArraySafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public TesseractStringArraySafeHandle() : base(true)
    {
    }
    
    internal IReadOnlyList<string> ToManagedStrings()
    {
        if (IsInvalid)
            return Array.Empty<string>();

        var result = new List<string>();

        for (var index = 0;; index++)
        {
            var stringPointer = Marshal.ReadIntPtr(handle, checked(index * nint.Size));

            if (stringPointer == nint.Zero)
                break;

            var value = Marshal.PtrToStringUTF8(stringPointer);
            if (value is not null)
                result.Add(value);
        }

        return result.AsReadOnly();
    }

    protected override bool ReleaseHandle()
    {
        TesseractNative.TessDeleteTextArray(handle);
        return true;
    }
}