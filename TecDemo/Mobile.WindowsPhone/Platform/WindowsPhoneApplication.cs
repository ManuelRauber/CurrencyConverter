using System;
using System.Windows;
using Microsoft.Phone.Shell;
using TecDemo.Mobile.Contracts.Platform;

namespace TecDemo.Mobile.WindowsPhone.Platform
{
	public class WindowsPhoneApplication : Application, IApplicationLifecycle
	{
		// Code to execute when the application is launching (eg, from Start)
		// This code will not execute when the application is reactivated
		protected virtual void Application_Launching(object sender, LaunchingEventArgs e)
		{
			DoAppLaunching();
		}

		// Code to execute when the application is activated (brought to foreground)
		// This code will not execute when the application is first launched
		protected virtual void Application_Activated(object sender, ActivatedEventArgs e)
		{
			DoAppResuming();
		}

		// Code to execute when the application is deactivated (sent to background)
		// This code will not execute when the application is closing
		protected virtual void Application_Deactivated(object sender, DeactivatedEventArgs e)
		{
			DoAppSuspending();
		}

		// Code to execute when the application is closing (eg, user hit Back)
		// This code will not execute when the application is deactivated
		protected virtual void Application_Closing(object sender, ClosingEventArgs e)
		{
			DoAppClosing();
		}

		public event Action OnAppResuming;
		public event Action OnAppSuspending;
		public event Action OnAppLaunching;
		public event Action OnAppClosing;

		protected virtual void DoAppResuming()
		{
			var handler = OnAppResuming;
			if (null != handler)
			{
				handler();
			}
		}

		protected virtual void DoAppSuspending()
		{
			var handler = OnAppSuspending;
			if (null != handler)
			{
				handler();
			}
		}

		protected virtual void DoAppLaunching()
		{
			var handler = OnAppLaunching;
			if (null != handler)
			{
				handler();
			}
		}

		protected virtual void DoAppClosing()
		{
			var handler = OnAppClosing;
			if (null != handler)
			{
				handler();
			}
		}
	}
}