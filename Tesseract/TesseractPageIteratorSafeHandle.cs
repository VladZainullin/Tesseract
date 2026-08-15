using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Tesseract;

public class TesseractPageIteratorSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    private SafeHandle? _owner;

    public TesseractPageIteratorSafeHandle() : base(true)
    {
    }

    internal void AttachOwner(SafeHandle owner)
    {
        ObjectDisposedException.ThrowIf(IsClosed, this);
        if (IsInvalid)
            throw new InvalidOperationException("Cannot attach an owner to an invalid page iterator handle.");

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

    protected void ReleaseOwner()
    {
        var owner = _owner;
        _owner = null;
        owner?.DangerousRelease();
    }

    protected override bool ReleaseHandle()
    {
        try
        {
            TesseractNative.TessPageIteratorDelete(handle);
            return true;
        }
        finally
        {
            ReleaseOwner();
        }
    }
}
