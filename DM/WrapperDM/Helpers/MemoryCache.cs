using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.Extensions.Caching.Memory;

namespace WrapperDM.Helpers;

public class MemoryCache<TItem>
{
    private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
 
    public TItem GetOrCreate(object key, Func<TItem> createItem)
    {
        TItem cacheEntry;
        if (!_cache.TryGetValue(key, out cacheEntry)) // Ищем ключ в кэше.
        {
            // Ключ отсутствует в кэше, поэтому получаем данные.
            cacheEntry = createItem();
            
            // Сохраняем данные в кэше. 
            _cache.Set(key, cacheEntry);
        }
        return cacheEntry;
    }
    
    public TItem GetSection(string key)
    {
        var memoryCacheObject = _cache.Get(key);
        return (TItem)memoryCacheObject;
    }
}