using BlogArray.Domain.DTOs;

namespace BlogArray.Domain.Interfaces;

public interface IAppOptionsRepository
{
    Task<List<AppOptionsBase>> GetAutoLoadOptions();
    
    Task CacheAutoLoadOptions();
}
