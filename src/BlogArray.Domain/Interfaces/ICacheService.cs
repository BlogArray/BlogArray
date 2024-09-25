namespace BlogArray.Domain.Interfaces;

public interface ICacheService
{
    /// <summary>
    /// Attempts to retrieve a value from the cache.
    /// </summary>
    /// <typeparam name="T">The type of the cached value.</typeparam>
    /// <param name="cacheKey">The cache key used to retrieve the value.</param>
    /// <param name="value">The value retrieved from the cache, if found.</param>
    /// <returns>True if the value was found in the cache, otherwise false.</returns>
    bool TryGet<T>(string cacheKey, out T value);

    /// <summary>
    /// Sets a value in the cache with the specified key and expiration settings.
    /// </summary>
    /// <typeparam name="T">The type of the value to be cached.</typeparam>
    /// <param name="cacheKey">The cache key used to store the value.</param>
    /// <param name="value">The value to be cached.</param>
    /// <returns>The value that was cached.</returns>
    T Set<T>(string cacheKey, T value);

    /// <summary>
    /// Tries to get the value from the cache. If it doesn't exist, it will create, cache, and return the value asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the object to cache.</typeparam>
    /// <param name="cacheKey">The cache key to search for.</param>
    /// <param name="factory">The factory function to generate the value if it doesn't exist.</param>
    /// <returns>The cached value or the value created by the factory.</returns>
    Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factory);

    /// <summary>
    /// Removes the specified cache entry from the cache.
    /// </summary>
    /// <param name="cacheKey">The cache key to remove.</param>
    void Remove(string cacheKey);
}
