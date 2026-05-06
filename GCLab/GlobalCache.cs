using Microsoft.Extensions.Caching.Memory;

namespace GCLab;

// =====================================================
// 2) Cache estático sem política de expiração
// =====================================================
static class GlobalCache
{
    private static MemoryCache _cache =
        new(new MemoryCacheOptions());

    public static void Add(byte[] data)
    {
        _cache.Set(
            Guid.NewGuid(),
            data,
            TimeSpan.FromMinutes(5));
    }

    public static void Clear()
    {
        _cache.Dispose();
        _cache = new MemoryCache(new MemoryCacheOptions());
    }
}
