using System;

namespace TecDemo.Mobile.Contracts.Caching
{
	public interface ICacheSettings
	{
		string Key { get; set; }
		DateTime AbsoluteExpiration { get; set; }
	}
}