using System;
using GoogleAnalytics.Core;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TecDemo.Mobile.Touch.Analytics
{
	public class PlatformInformationProvider : IPlatformInfoProvider
	{
		private string _anonymousClientId;
		private int? _screenColorDepthBits;
		private Dimensions _screenResolution;
		private string _userLanguage;
		private Dimensions _viewPortResolution;
		private bool _isWindowInitialized;

		public PlatformInformationProvider()
		{
			InitializeWindow();
		}

		private void InitializeWindow()
		{
			try
			{
				if (null != UIScreen.MainScreen)
				{
					var mainScreen = UIScreen.MainScreen;
					var scale = mainScreen.Scale;

					if (UIInterfaceOrientation.Portrait == UIApplication.SharedApplication.StatusBarOrientation)
					{
						_screenResolution = new Dimensions((int) (mainScreen.Bounds.Width * scale),
							(int) (mainScreen.Bounds.Height * scale));
					}
					else
					{
						_screenResolution = new Dimensions((int) (mainScreen.Bounds.Height * scale),
							(int) (mainScreen.Bounds.Width * scale));
					}

					_viewPortResolution = new Dimensions((int) mainScreen.Bounds.Width, (int) mainScreen.Bounds.Height);

					_userLanguage = NSLocale.PreferredLanguages[0];
					_screenColorDepthBits = null;

					InitializeAnonymousClientId();

					_isWindowInitialized = true;
				}
			}
			catch
			{ 
				// Just catch all and do nothing, not all screen settings are initialized yet
			}
		}

		private void InitializeAnonymousClientId()
		{
			// TODO: This should be saved to device and loaded on initializing, to recognize user again
			_anonymousClientId = Guid.NewGuid().ToString();
		}

		public void OnTracking()
		{
			if (!_isWindowInitialized)
			{
				InitializeWindow();
			}
		}

		public string GetUserAgent()
		{
			var sysInfo = UIDevice.CurrentDevice;

			return
				String.Format(
					"Mozilla/5.0 ({0}; CPU {0} OS {1} like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/{1} Mobile/11A465 Safari/9537.53", sysInfo.Model, sysInfo.SystemVersion);
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
			private set
			{
				if (value != _screenResolution)
				{
					_screenResolution = value;
					var handler = ScreenResolutionChanged;
					if (null != handler)
					{
						handler(this, EventArgs.Empty);
					}
				}
			}
		}

		public string UserLanguage
		{
			get { return _userLanguage; }
		}

		public Dimensions ViewPortResolution
		{
			get { return _viewPortResolution; }
			private set
			{
				if (value != ViewPortResolution)
				{
					_viewPortResolution = value;
					var handler = ViewPortResolutionChanged;
					if (null != handler)
					{
						handler(this, EventArgs.Empty);
					}
				}
			}
		}

		public event EventHandler ViewPortResolutionChanged;
		public event EventHandler ScreenResolutionChanged;
	}
}