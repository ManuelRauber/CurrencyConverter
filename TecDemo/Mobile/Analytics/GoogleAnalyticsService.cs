using System;
using GoogleAnalytics.Core;
using TecDemo.Mobile.Contracts.Analytics;
using TecDemo.Mobile.Contracts.Configuration;
using TecDemo.Mobile.Contracts.Networking;
using TecDemo.Mobile.Contracts.Platform;

namespace TecDemo.Mobile.Analytics
{
	public class GoogleAnalyticsService : IAnalyticsService
	{
		private readonly IGoogleAnalyticsConfiguration _configuration;
		private readonly IPlatformInfoProvider _plattformInfoProvider;
		private readonly INetworkInformation _networkInformation;
		private readonly IApplicationLifecycle _applicationLifecycle;
		private GAServiceManager _serviceManager;
		private TrackerManager _manager;
		private Tracker _tracker;
		private bool _isEnabled;
		private DateTime _suspendTime;

		public GoogleAnalyticsService(IMobileConfiguration configuration, IPlatformInfoProvider plattformInfoProvider, INetworkInformation networkInformation, IApplicationLifecycle applicationLifecycle)
		{
			_configuration = configuration.GoogleAnalyticsConfiguration;

			if ((null == _configuration) ||
				(!_configuration.IsEnabled))
			{
				_isEnabled = false;
				return;
			}
			
			_plattformInfoProvider = plattformInfoProvider;
			_networkInformation = networkInformation;
			_applicationLifecycle = applicationLifecycle;
			SetupTracker();
			SetupNetworkInformation();
			SetupApplicationLifecycle();
			_isEnabled = true;
		}

		private void SetupApplicationLifecycle()
		{
			_applicationLifecycle.OnAppClosing += () =>
			{
				EndSession();
				TrackCustomEvent("ApplicationLifecycle", "Closing", String.Empty);
				_serviceManager.Dispatch();
			};

			_applicationLifecycle.OnAppLaunching += () =>
			{
				StartSession();
				TrackCustomEvent("ApplicationLifecycle", "Launching", String.Empty);
				_serviceManager.Dispatch();
			};

			_applicationLifecycle.OnAppResuming += () =>
			{
				if (DateTime.Now.Subtract(_suspendTime) > TimeSpan.FromMinutes(_configuration.SessionTimeout))
				{
					StartSession();
				}

				TrackCustomEvent("ApplicationLifecycle", "Resuming", String.Empty);
			};

			_applicationLifecycle.OnAppSuspending += () =>
			{
				TrackCustomEvent("ApplicationLifecycle", "Suspending", String.Empty);
				_suspendTime = DateTime.Now;
			};
		}

		private void SetupNetworkInformation()
		{
			_networkInformation.OnConnectionStateChanged += state => _serviceManager.IsConnected = state;
		}

		private void SetupTracker()
		{
			_serviceManager = GAServiceManager.Current;
			_manager = new TrackerManager(_plattformInfoProvider);
			_tracker = _manager.GetTracker(_configuration.TrackingId);
			_tracker.AppName = _configuration.AppName;
			_tracker.AppVersion = _configuration.AppVersion;
			_tracker.IsAnonymizeIpEnabled = _configuration.AnonymizeIp;
			_tracker.IsUseSecure = _configuration.UseSSL;
			_serviceManager.DispatchPeriod = TimeSpan.FromSeconds(_configuration.DispatchPeriod);
		}

		public void TrackView(string viewName)
		{
			if (!_isEnabled)
			{
				return;
			}

			_tracker.SendView(viewName);
		}

		public void TrackCustomEvent(string category, string action, string label)
		{
			if (!_isEnabled)
			{
				return;
			}

			_tracker.SendEvent(category, action, label, 1);
		}

		private void StartSession()
		{
			if (!_isEnabled)
			{
				return;
			}

			_tracker.SetStartSession(true);
		}

		private void EndSession()
		{
			if (!_isEnabled)
			{
				return;
			}

			_tracker.SetEndSession(true);
		}
	}
}