using BlogArray.Domain.DTOs;

namespace BlogArray.Domain.Interfaces;

public interface ISettingsRepository
{
    Task<PagedResult<AppOptionsBase>> GetOptions(int pageNumber = 1, int pageSize = 10, bool onlyAutoload = true);

    Task<List<AppOptionsBase>> GetAutoLoadOptions();

    Task<AppOptionsBase?> GetOption(string key);

    Task CacheAutoLoadOptions();

    Task<ReturnResult<int>> CreateOrUpdateAsync(AppOptionsBase appOptions);

    Task<T?> TryGetOption<T>(string key);

    Task<ReturnResult<int>> DeleteAsync(string key);
}
