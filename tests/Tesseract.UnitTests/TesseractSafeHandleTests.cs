using Microsoft.Win32.SafeHandles;

namespace Tesseract.UnitTests;

internal sealed class TesseractSafeHandleTests
{
    [Test]
    public async Task DefaultHandlesAreInvalid()
    {
        using var engine = new TesseractEngineSafeHandle();
        using var pageIterator = new TesseractPageIteratorSafeHandle();
        using var resultIterator = new TesseractResultIteratorSafeHandle();
        using var choiceIterator = new TesseractChoiceIteratorSafeHandle();
        using var renderer = new TesseractResultRendererSafeHandle();
        using var monitor = new TesseractMonitorSafeHandle();
        using var text = new TesseractStringSafeHandle();

        await Assert.That(engine.IsInvalid).IsTrue();
        await Assert.That(pageIterator.IsInvalid).IsTrue();
        await Assert.That(resultIterator.IsInvalid).IsTrue();
        await Assert.That(choiceIterator.IsInvalid).IsTrue();
        await Assert.That(renderer.IsInvalid).IsTrue();
        await Assert.That(monitor.IsInvalid).IsTrue();
        await Assert.That(text.IsInvalid).IsTrue();
    }

    [Test]
    public async Task DisposingDefaultHandleClosesIt()
    {
        var handle = new TesseractEngineSafeHandle();

        handle.Dispose();

        await Assert.That(handle.IsClosed).IsTrue();
    }

    [Test]
    public async Task PageIteratorHandlesKeepOwnerAliveUntilAllAreDisposed()
    {
        using var owner = new TrackingOwnerSafeHandle();
        using var firstIterator = new TrackingPageIteratorSafeHandle();
        using var secondIterator = new TrackingPageIteratorSafeHandle();
        firstIterator.AttachOwner(owner);
        secondIterator.AttachOwner(owner);

        owner.Dispose();
        await Assert.That(owner.ReleaseCount).IsEqualTo(0);

        firstIterator.Dispose();
        await Assert.That(owner.ReleaseCount).IsEqualTo(0);

        secondIterator.Dispose();
        await Assert.That(owner.ReleaseCount).IsEqualTo(1);
    }

    [Test]
    public async Task InvalidPageIteratorHandleRejectsOwner()
    {
        using var owner = new TrackingOwnerSafeHandle();
        using var iterator = new TesseractPageIteratorSafeHandle();

        await Assert.That(() => iterator.AttachOwner(owner))
            .ThrowsExactly<InvalidOperationException>();
    }

    private sealed class TrackingOwnerSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal TrackingOwnerSafeHandle() : base(ownsHandle: true)
        {
            SetHandle((nint)1);
        }

        internal int ReleaseCount { get; private set; }

        protected override bool ReleaseHandle()
        {
            ReleaseCount++;
            return true;
        }
    }

    private sealed class TrackingPageIteratorSafeHandle : TesseractPageIteratorSafeHandle
    {
        internal TrackingPageIteratorSafeHandle()
        {
            SetHandle((nint)2);
        }

        protected override bool ReleaseHandle()
        {
            ReleaseOwner();
            return true;
        }
    }
}
