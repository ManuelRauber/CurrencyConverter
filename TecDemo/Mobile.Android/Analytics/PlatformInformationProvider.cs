using System;
using Android.Content.Res;
using GoogleAnalytics.Core;
using Java.Util;

namespace TecDemo.Mobile.Android.Analytics
{
	public class PlatformInformationProvider : IPlatformInfoProvider
	{
		private string _anonymousClientId;
		private int? _screenColorDepthBits;
		private Dimensions _screenResolution;
		private string _userLanguage;
		private Dimensions _viewPortResolution;
		private bool _isWindowInitialized;

		public void OnTracking()
		{
			if (!_isWindowInitialized)
			{
				InitializeWindow();
			}
		}

		private void InitializeWindow()
		{
			try
			{
				var metrics = Resources.System.DisplayMetrics;
				var system = Resources.System.Configuration;

				if (Orientation.Portrait == system.Orientation)
				{
					_screenResolution = new Dimensions(metrics.WidthPixels, metrics.HeightPixels);
				}
				else
				{
					_screenResolution = new Dimensions(metrics.HeightPixels, metrics.WidthPixels);
				}

				_viewPortResolution = new Dimensions((int) (metrics.WidthPixels / metrics.Xdpi), (int) (metrics.HeightPixels / metrics.Ydpi));
				_userLanguage = Locale.Default.ToString();

				InitializeAnonymousClientId();

				_isWindowInitialized = true;
			}
			catch
			{
				// Just ignore this, maybe some screen settings have not been initialized yet
			}
		}

		private void InitializeAnonymousClientId()
		{
			// TODO: This should be saved to local storage and load again, to recognize user after app restarts
			_anonymousClientId = Guid.NewGuid().ToString();
		}

		public string GetUserAgent()
		{
			return
				String.Format(
					"Mozilla/5.0");
		}

		public string AnonymousClientId
		{
			get { return _anonymousClientId; }
			set { _anonymousClientId = value; }
		}

		public int? ScreenColorDepthBits
		{
			get { return _screenColorDepthBits; }
		}

		public Dimensions ScreenResolution
		{
			get { return _screenResolution; }
		}

		public string UserLanguage
		{
			get { return _userLanguage; }
		}

		public Dimensions ViewPortResolution
		{
			get { return _viewPortResolution; }
		}

		public event EventHandler ViewPortResolutionChanged;
		public event EventHandler ScreenResolutionChanged;
	}
}