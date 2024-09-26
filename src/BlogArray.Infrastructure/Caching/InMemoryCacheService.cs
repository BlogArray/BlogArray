using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NetCore.AutoRegisterDi;

namespace BlogArray.Infrastructure.Caching;

/// <summary>
/// Initializes a new instance of the <see cref="InMemoryCacheService"/> class.
/// Configures the cache expiration based on the provided configuration.
/// </summary>
/// <param name="memoryCache">The in-memory cache instance.</param>
/// <param name="cacheConfig">The cache configuration options.</param>
[RegisterAsScoped]
public class InMemoryCacheService(IMemoryCache memoryCache, IOptions<CacheConfiguration> cacheConfig) : ICacheService
{
    /// <summary>
    /// Attempts to retrieve a value from the cache.
    /// </summary>
    /// <typeparam name="T">The type of the cached value.</typeparam>
    /// <param name="cacheKey">The cache key used to retrieve the value.</param>
    /// <param name="value">The value retrieved from the cache, if found.</param>
    /// <returns>True if the value was found in the cache, otherwise false.</returns>
    public bool TryGet<T>(string cacheKey, out T value)
    {
        memoryCache.TryGetValue(cacheKey, out value);
        return value != null;
    }

    /// <summary>
    /// Sets a value in the cache with the specified key and expiration settings.
    /// </summary>
    /// <typeparam name="T">The type of the value to be cached.</typeparam>
    /// <param name="cacheKey">The cache key used to store the value.</param>
    /// <param name="value">The value to be cached.</param>
    /// <returns>The value that was cached.</returns>
    public T Set<T>(string cacheKey, T value)
    {
        return memoryCache.Set(cacheKey, value, new MemoryCacheEntryOptions
        {
            Priority = CacheItemPriority.High,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(cacheConfig.Value.AbsoluteExpirationInHours),
            SlidingExpiration = TimeSpan.FromMinutes(cacheConfig.Value.SlidingExpirationInMinutes)
        });
    }

    /// <summary>
    /// Tries to get the value from the cache. If it doesn't exist, it will create, cache, and return the value asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the object to cache.</typeparam>
    /// <param name="cacheKey">The cache key to search for.</param>
    /// <param name="factory">The factory function to generate the value if it doesn't exist.</param>
    /// <returns>The cached value or the value created by the factory.</returns>
    public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factory)
    {
        // Check if the value exists in the cache
        if (!memoryCache.TryGetValue(cacheKey, out T? value))
        {
            // Value not in cache, call the factory to create it
            value = await factory();

            // Store the newly created value in the cache and return it
            Set(cacheKey, value);
        }

        return value!;
    }

    /// <summary>
    /// Removes the specified cache entry from the cache.
    /// </summary>
    /// <param name="cacheKey">The cache key to remove.</param>
    public void Remove(string cacheKey)
    {
        memoryCache.Remove(cacheKey);
    }
}
