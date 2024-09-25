using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using BlogArray.Persistence;
using Microsoft.EntityFrameworkCore;
using NetCore.AutoRegisterDi;

namespace BlogArray.Infrastructure.Repositories;

[RegisterAsScoped]
public class AppOptionsRepository(AppDbContext db, ICacheService cache) : IAppOptionsRepository
{

    public async Task<List<AppOptionsBase>> GetAutoLoadOptions()
    {
        return await db.AppOptions.Where(o => o.AutoLoad)
            .Select(o => new AppOptionsBase
            {
                Key = o.Key,
                Value = o.Value,
            }).ToListAsync();
    }

    public async Task CacheAutoLoadOptions()
    {
        List<AppOptionsBase> appOptions = await GetAutoLoadOptions();

        if (appOptions.Count != 0)
        {
            foreach (var appOption in appOptions)
            {
                await cache.GetOrCreateAsync(appOption.Key, async () =>
                {
                    return await Task.FromResult(appOption.Value);
                });
            }
        }
    }

}