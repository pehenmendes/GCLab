using System.Runtime.InteropServices;

namespace GCLab;

// ===================================
// 3) Pinned buffer mantido por muito tempo
// ===================================
class Pinner : IDisposable
{
    private GCHandle _handle;
    private bool _pinned;

    public byte[] PinShortTime()
    {
        var data = new byte[256];
        _handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        _pinned = true;
        return data;
    }

    public void Dispose()
    {
        if (_handle.IsAllocated)
        {
            _handle.Free();
            _pinned = false;
        }
    }
}
