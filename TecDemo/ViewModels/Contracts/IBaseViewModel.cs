using System.ComponentModel;
using TecDemo.Mobile.Contracts.Analytics;
using TecDemo.Mobile.Contracts.Caching;

namespace TecDemo.ViewModels.Contracts
{
	public interface IBaseViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// Analytics service for tracking page views, custom events etc.
		/// </summary>
		IAnalyticsService AnalyticsService { get; }

		INavigationCache NavigationCache { get; }
	}
}