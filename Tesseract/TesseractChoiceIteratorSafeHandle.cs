using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public sealed class TesseractChoiceIteratorSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    private SafeHandle? _owner;
    
    public TesseractChoiceIteratorSafeHandle() : base(true)
    {
    }

    public void AttachOwner(SafeHandle owner)
    {
        ArgumentNullException.ThrowIfNull(owner);
        ObjectDisposedException.ThrowIf(owner.IsClosed, owner);
        if (owner.IsInvalid) throw new ArgumentException("Owner handle is invalid.", nameof(owner));
        if (_owner is not null) throw new InvalidOperationException("An owner is already attached.");

        var referenceAdded = false;
        try
        {
            owner.DangerousAddRef(ref referenceAdded);
            if (!referenceAdded) throw new InvalidOperationException("Failed to acquire the owner handle.");

            _owner = owner;
        }
        catch
        {
            if (referenceAdded) owner.DangerousRelease();
            throw;
        }
    }
    
    protected override bool ReleaseHandle()
    {
        try
        {
            TesseractNative.TessChoiceIteratorDelete(handle);
            return true;
        }
        finally
        {
            _owner?.DangerousRelease();
            _owner = null;
        }
    }
}
