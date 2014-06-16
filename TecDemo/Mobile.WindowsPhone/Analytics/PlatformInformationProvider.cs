using System;
using System.Threading;
using System.Windows;
using Windows.Graphics.Display;
using GoogleAnalytics.Core;
using Microsoft.Phone.Info;

namespace TecDemo.Mobile.WindowsPhone.Analytics
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
				var displayInformation = DisplayInformation.GetForCurrentView();
				var bounds = Application.Current.Host.Content;

				if (DisplayOrientations.Portrait == displayInformation.CurrentOrientation)
				{
					ScreenResolution = new Dimensions((int) bounds.ActualWidth, (int) bounds.ActualHeight);
				}
				else
				{
					ScreenResolution = new Dimensions((int) bounds.ActualHeight, (int) bounds.ActualWidth);
				}

				ViewPortResolution = new Dimensions((int) (bounds.ActualWidth / displayInformation.RawDpiX), (int) (bounds.ActualHeight / displayInformation.RawDpiY));

				_userLanguage = Thread.CurrentThread.CurrentCulture.ToString();

				_screenColorDepthBits = null;

				InitializeAnonymousClientId();

				_isWindowInitialized = true;
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
			return string.Format("Mozilla/5.0 (Windows Phone 8.1; ARM; Trident/7.0; Touch; rv11.0; IEMobile/11.0; {0}; {1}) like Gecko", 
				DeviceStatus.DeviceManufacturer, DeviceStatus.DeviceName);
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
				if (_screenResolution != value)
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
				if (value != _viewPortResolution)
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