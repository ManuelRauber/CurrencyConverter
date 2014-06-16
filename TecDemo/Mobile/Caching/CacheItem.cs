using TecDemo.Mobile.Contracts.Caching;

namespace TecDemo.Mobile.Caching
{
	internal class CacheItem
	{
		public ICacheSettings CacheSettings { get; set; }
		public object Item { get; set; }
	}
}