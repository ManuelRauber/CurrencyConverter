namespace TecDemo.Mobile.Contracts.Caching
{
	/// <summary>
	/// Just a marker interface, for a special purposed navigation cache (since we can not pass parameters when navigation (WHY?))
	/// </summary>
	public interface INavigationCache : ICache
	{
		/// <summary>
		/// Returns the cached value or null if not cached.
		/// Will remove the cached value after retrieving!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		T Get<T>(string key);

		void Add<T>(string key, T cacheItem);
	}
}