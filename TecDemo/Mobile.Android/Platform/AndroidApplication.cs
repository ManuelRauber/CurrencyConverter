using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using TecDemo.Mobile.Contracts.Platform;

namespace TecDemo.Mobile.Android.Platform
{
	public class AndroidApplication : Application, IApplicationLifecycle, Application.IActivityLifecycleCallbacks
	{
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

		public AndroidApplication(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
			SetupEvents();
		}

		private void SetupEvents()
		{
			RegisterActivityLifecycleCallbacks(this);
		}

		public void OnActivityCreated(Activity activity, Bundle savedInstanceState) {}

		public void OnActivityDestroyed(Activity activity) {}

		public void OnActivityPaused(Activity activity)
		{
			DoAppSuspending();
		}

		public void OnActivityResumed(Activity activity)
		{
			DoAppResuming();
		}

		public void OnActivitySaveInstanceState(Activity activity, Bundle outState) {}

		public void OnActivityStarted(Activity activity) {}

		public void OnActivityStopped(Activity activity) {}

		public override void OnCreate()
		{
			base.OnCreate();

			DoAppLaunching();
		}

		public override void OnTerminate()
		{
			base.OnTerminate();

			DoAppClosing();
		}
	}
}