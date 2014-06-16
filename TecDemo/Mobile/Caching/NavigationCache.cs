using System;
using TecDemo.Mobile.Contracts.Caching;

namespace TecDemo.Mobile.Caching
{
	public class NavigationCache : InMemoryCache, INavigationCache
	{
		public T Get<T>(string key)
		{
			var value = GetOrAdd(new CacheSettings(key, DateTime.Now), () => default(T));
			Remove(key);

			return value;
		}

		public void Add<T>(string key, T cacheItem)
		{
			Remove(key);
			GetOrAdd(new CacheSettings(key, Expires.In(1).Years()), () => cacheItem);
		}
	}
}