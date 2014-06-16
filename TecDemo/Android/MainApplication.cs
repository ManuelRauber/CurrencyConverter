using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Autofac;
using TecDemo.Android.Common;
using TecDemo.Mobile.Android.Networking;
using TecDemo.Mobile.Android.Platform;
using TecDemo.Mobile.Contracts.Platform;

namespace TecDemo.Android
{
	[Application]
	public class MainApplication : AndroidApplication
	{
		public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
			var builder = new ContainerBuilder();
			builder.Register(x => this).As<IApplicationLifecycle>();
			builder.Update(ViewLocator.Instance.Container);
			RegisterReceiver(new NetworkInformation(), new IntentFilter("android.net.conn.CONNECTIVITY_CHANGE"));
		}
	}
}