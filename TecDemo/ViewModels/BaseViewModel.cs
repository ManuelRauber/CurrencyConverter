using TecDemo.Mobile;
using TecDemo.Mobile.Contracts.Analytics;
using TecDemo.Mobile.Contracts.Caching;
using TecDemo.ViewModels.Contracts;

namespace TecDemo.ViewModels
{
	public class BaseViewModel : BindableBase, IBaseViewModel
	{
		private readonly IAnalyticsService _analyticsService;
		private readonly INavigationCache _navigationCache;

		public IAnalyticsService AnalyticsService
		{
			get { return _analyticsService; }
		}

		public INavigationCache NavigationCache
		{
			get { return _navigationCache; }
		}

		public BaseViewModel(IAnalyticsService analyticsService, INavigationCache navigationCache)
		{
			_analyticsService = analyticsService;
			_navigationCache = navigationCache;
		}
	}
}