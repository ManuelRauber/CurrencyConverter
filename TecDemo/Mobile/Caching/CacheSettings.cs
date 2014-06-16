using System;
using TecDemo.Mobile.Contracts.Caching;

namespace TecDemo.Mobile.Caching
{
	public class CacheSettings : ICacheSettings
	{
		public string Key { get; set; }

		public DateTime AbsoluteExpiration { get; set; }

		public CacheSettings(string key, DateTime absoluteExpiration)
		{
			Key = key;
			AbsoluteExpiration = absoluteExpiration;
		}
	}
}