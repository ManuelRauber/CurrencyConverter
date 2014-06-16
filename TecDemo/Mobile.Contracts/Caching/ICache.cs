using System;

namespace TecDemo.Mobile.Contracts.Caching
{
	public interface ICache
	{
		T GetOrAdd<T>(ICacheSettings settings, Func<T> updateFunction);
		void Remove(string key);
		void Invalidate();
		bool Exists(string key);
	}
}