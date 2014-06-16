using System;
using System.Collections.Generic;
using TecDemo.Mobile.Contracts.Caching;

namespace TecDemo.Mobile.Caching
{
	public class InMemoryCache : ICache
	{
		private Dictionary<string, CacheItem> _cacheItems;

		public InMemoryCache()
		{
			_cacheItems = new Dictionary<string, CacheItem>();
		}

		public T GetOrAdd<T>(ICacheSettings settings, Func<T> updateFunction)
		{
			if (null == settings)
			{
				throw new ArgumentNullException("settings");
			}

			Invalidate(settings.Key);

			if (!Exists(settings.Key))
			{
				Add(settings, updateFunction);
			}

			var item = _cacheItems[settings.Key];
			return (T) item.Item;
		}

		private void Add<T>(ICacheSettings settings, Func<T> updateFunction)
		{
			var items = updateFunction();

			var cacheItem = new CacheItem()
			{
				CacheSettings = settings,
				Item = items,
			};

			_cacheItems.Add(settings.Key, cacheItem);
		}

		public void Remove(string key)
		{
			if (Exists(key))
			{
				_cacheItems.Remove(key);
			}
		}

		public void Invalidate()
		{
			foreach (var key in _cacheItems.Keys)
			{
				Invalidate(key);
			}
		}

		private void Invalidate(string key)
		{
			if (!Exists(key))
			{
				return;
			}

			var item = _cacheItems[key];

			if (DateTime.Now > item.CacheSettings.AbsoluteExpiration)
			{
				_cacheItems.Remove(key);
			}
		}

		public bool Exists(string key)
		{
			return _cacheItems.ContainsKey(key);
		}
	}
}