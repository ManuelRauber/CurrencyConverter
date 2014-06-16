using Autofac;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TecDemo.Mobile.Contracts.Platform;
using TecDemo.Mobile.Touch.Platform;
using TecDemo.TouchUniversal.Common;
using Xamarin.Forms;

namespace TecDemo.TouchUniversal
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : TouchApplication
	{
		// class-level declarations
		UIWindow window;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			var builder = new ContainerBuilder();
			builder.Register(x => this).As<IApplicationLifecycle>();
			builder.Update(ViewLocator.Instance.Container);

			base.FinishedLaunching(app, options);
			Forms.Init();

			// create a new window instance based on the screen size
			window = new UIWindow(UIScreen.MainScreen.Bounds);

			window.RootViewController = new NavigationPage(ViewLocator.Instance.MainPage).CreateViewController();

			// make the window visible
			window.MakeKeyAndVisible();

			return true;
		}
	}
}