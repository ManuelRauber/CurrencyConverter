using Android.App;
using Android.OS;
using Android.Widget;
using TecDemo.Android.Common;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace TecDemo.Android
{
	[Activity(Label = "Currency Converter", MainLauncher = true, Icon = "@drawable/Launcher")]
	public class StartActivity : AndroidActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			Forms.Init(this, bundle);

			SetPage(new NavigationPage(ViewLocator.Instance.MainPage));

		}
	}
}