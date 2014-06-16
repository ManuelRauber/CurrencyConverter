using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TecDemo.Mobile.Contracts.Platform;

namespace TecDemo.Mobile.Touch.Platform
{
	public class TouchApplication : UIApplicationDelegate, IApplicationLifecycle
	{
		public event Action OnAppResuming;
		public event Action OnAppSuspending;
		public event Action OnAppLaunching;
		public event Action OnAppClosing;

		protected void DoAppResuming()
		{
			var handler = OnAppResuming;
			if (null != handler)
			{
				handler();
			}
		}

		protected void DoAppSuspending()
		{
			var handler = OnAppSuspending;
			if (null != handler)
			{
				handler();
			}
		}

		protected void DoAppLaunching()
		{
			var handler = OnAppLaunching;
			if (null != handler)
			{
				handler();
			}
		}

		protected void DoAppClosing()
		{
			var handler = OnAppClosing;
			if (null != handler)
			{
				handler();
			}
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			DoAppLaunching();

			return true;
		}

		public override void WillTerminate(UIApplication application)
		{
			DoAppClosing();
		}

		public override void WillEnterForeground(UIApplication application)
		{
			DoAppResuming();
		}

		public override void DidEnterBackground(UIApplication application)
		{
			DoAppSuspending();
		}
	}
}