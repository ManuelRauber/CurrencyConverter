using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TecDemo.Mobile.Contracts.Caching;
using TecDemo.Mobile.Contracts.Storage;
using TecDemo.Mobile.Text;

namespace TecDemo.Mobile.Caching
{
	public class FileCache : ICache
	{
		[JsonObject(MemberSerialization.OptOut)]
		internal class InternalCacheStorage
		{
			private List<ICacheSettings> _cacheSettings;

			[JsonProperty(ItemConverterType = typeof (ConcreteTypeConverter<CacheSettings>))]
			internal List<ICacheSettings> CacheSettings
			{
				get { return _cacheSettings ?? (_cacheSettings = new List<ICacheSettings>()); }
				set { _cacheSettings = value; }
			}
		} 

		private readonly IStorage _storage;
		private const string FileCacheName = "FileCache";
		private const string FileCacheFileName = FileCacheName + ".json";
		private InternalCacheStorage _cacheStorage;

		public FileCache(IStorage storage)
		{
			_storage = storage;
			InitializeCache();
		}

		private void InitializeCache()
		{
			_storage.BasePath = FileCacheName;

			_cacheStorage = new InternalCacheStorage();
			LoadCacheFromStorage();
		}

		private void LoadCacheFromStorage()
		{
			if (_storage.Exists(FileCacheFileName))
			{
				_cacheStorage = _storage.LoadFrom<InternalCacheStorage>(FileCacheFileName);
			}

			Invalidate();
		}

		private void SaveCacheToStorage()
		{
			_storage.SaveTo(FileCacheFileName, _cacheStorage);
		}

		public T GetOrAdd<T>(ICacheSettings settings, Func<T> updateFunction)
		{
			CheckKeyForWhitespace(settings);

			Invalidate(settings);

			if (!Exists(settings.Key))
			{
				Add(settings, updateFunction);
			}

			var obj = _storage.LoadFrom<T>(TransformKey(settings.Key));
			return obj;
		}

		private void Add<T>(ICacheSettings settings, Func<T> updateFunction)
		{
			var items = updateFunction();
			_storage.SaveTo(TransformKey(settings.Key), items);
			_cacheStorage.CacheSettings.Add(settings);
			SaveCacheToStorage();
		}

		public void Remove(string key)
		{
			CheckKeyForWhitespace(key);
		
			_storage.Remove(TransformKey(key));
			var item = GetCacheSettingFromInternalStorage(key);
			_cacheStorage.CacheSettings.Remove(item);
			SaveCacheToStorage();
		}

		public void Invalidate()
		{
			for (int i = _cacheStorage.CacheSettings.Count - 1; i >= 0; i--)
			{
				Invalidate(_cacheStorage.CacheSettings[i]);
			}
		}

		private void Invalidate(ICacheSettings setting)
		{
			if (!Exists(setting.Key))
			{
				return;
			}

			var item = GetCacheSettingFromInternalStorage(setting.Key);
			if (DateTime.Now > item.AbsoluteExpiration)
			{
				Remove(item.Key);
			}
		}

		public bool Exists(string key)
		{
			CheckKeyForWhitespace(key);

			return GetCacheSettingFromInternalStorage(key) != null;
		}

		private void CheckKeyForWhitespace(ICacheSettings settings)
		{
			CheckKeyForWhitespace(settings.Key);
		}

		private void CheckKeyForWhitespace(string key)
		{
			if (ContainsWhitespace(key))
			{
				throw new FormatException("Key should not contain a whitespace character");
			}
		}

		private bool ContainsWhitespace(string input)
		{
			return input.IndexOf(' ') > -1;
		}

		private string TransformKey(string key)
		{
			return String.Format("{0}.json", key);
		}

		private ICacheSettings GetCacheSettingFromInternalStorage(string key)
		{
			return _cacheStorage.CacheSettings.SingleOrDefault(x => x.Key.Equals(key));
		}
	}
}