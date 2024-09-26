using BlogArray.Domain.DTOs;
using BlogArray.Domain.Entities;
using BlogArray.Domain.Interfaces;
using BlogArray.Infrastructure.Extensions;
using BlogArray.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NetCore.AutoRegisterDi;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlogArray.Infrastructure.Repositories;

[RegisterAsScoped]
public class AppOptionsRepository(AppDbContext db, ICacheService cache) : IAppOptionsRepository
{

    public async Task<PagedResult<AppOptionsBase>> GetOptions(int pageNumber = 1, int pageSize = 10, bool onlyAutoload = true)
    {
        IQueryable<AppOption> options = db.AppOptions;

        if (onlyAutoload)
        {
            options.Where(o => o.AutoLoad);
        }

        PagedResult<AppOptionsBase> pagedOptions = await options
            .Select(o => new AppOptionsBase
            {
                Key = o.Key,
                Value = o.Value,
                AutoLoad = o.AutoLoad,
                OptionType = o.OptionType
            }).ToPagedListAsync(pageNumber, pageSize);

        return pagedOptions;
    }

    public async Task<List<AppOptionsBase>> GetAutoLoadOptions()
    {
        return await db.AppOptions.Where(o => o.AutoLoad)
            .Select(o => new AppOptionsBase
            {
                Key = o.Key,
                Value = o.Value,
                AutoLoad = o.AutoLoad,
                OptionType = o.OptionType
            }).ToListAsync();
    }

    public async Task<AppOptionsBase?> GetOption(string key)
    {
        return await db.AppOptions.Where(o => o.Key == key)
            .Select(o => new AppOptionsBase
            {
                Key = o.Key,
                Value = o.Value,
                AutoLoad = o.AutoLoad,
                OptionType = o.OptionType
            }).FirstOrDefaultAsync();
    }

    public async Task CacheAutoLoadOptions()
    {
        List<AppOptionsBase> appOptions = await GetAutoLoadOptions();

        if (appOptions.Count != 0)
        {
            foreach (var appOption in appOptions)
            {
                cache.Set(appOption.Key, appOption.Value);
            }
        }
    }

    public async Task<ReturnResult<int>> CreateOrUpdateAsync(AppOptionsBase appOptions)
    {
        AppOption? option = await db.AppOptions.FirstOrDefaultAsync(s => s.Key == appOptions.Key);

        bool isNew = true;

        if (option == null)
        {
            option = new()
            {
                Key = appOptions.Key,
                Value = appOptions.Value,
                OptionType = appOptions.OptionType,
                AutoLoad = appOptions.AutoLoad
            };

            await db.AppOptions.AddAsync(option);
        }
        else
        {
            isNew = false;
            option.Value = appOptions.Value;
        }

        if (option.AutoLoad)
        {
            cache.Set(appOptions.Key, appOptions.Value);
        }

        await db.SaveChangesAsync();

        return new ReturnResult<int>
        {
            Code = StatusCodes.Status200OK,
            Title = "Option." + (isNew ? "Created" : "Updated"),
            Message = $"Option '{option.Key}' was successfully {(isNew ? "created" : "updated")}.",
            Result = 1
        };
    }

    public async Task<T?> TryGetOption<T>(string key)
    {
        if (cache.TryGet(key, out T value))
        {
            return value;
        }

        var keyValue = await db.AppOptions.FirstOrDefaultAsync(o => o.Key == key);

        if (keyValue == null)
        {
            return default;
        }
        else
        {
            if (keyValue.AutoLoad)
            {
                cache.Set(key, keyValue.Value);
            }
        }

        return JsonSerializer.Deserialize<T>(keyValue.Value);
    }

    public async Task<ReturnResult<int>> DeleteAsync(string key)
    {
        AppOption? option = await db.AppOptions.FirstOrDefaultAsync(a => a.Key == key);

        if (option == null)
        {
            return new ReturnResult<int>
            {
                Code = StatusCodes.Status404NotFound,
                Title = "Option.NotFound",
                Message = $"The Option with the key '{key}' could not be found in the system."
            };
        }

        cache.Remove(key);

        db.AppOptions.Remove(option);

        await db.SaveChangesAsync();

        return new ReturnResult<int>
        {
            Code = StatusCodes.Status200OK,
            Title = "Option.Deleted",
            Message = $"Option '{option.Key}' was successfully deleted.",
            Result = 1
        };
    }

}