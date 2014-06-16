using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Autofac;
using GoogleAnalytics.Core;
using TecDemo.CurrencyConverter.Contracts.Services;
using TecDemo.CurrencyConverter.Services;
using TecDemo.Mobile.Analytics;
using TecDemo.Mobile.Android.Analytics;
using TecDemo.Mobile.Android.Configuration;
using TecDemo.Mobile.Android.Networking;
using TecDemo.Mobile.Android.Storage;
using TecDemo.Mobile.Caching;
using TecDemo.Mobile.Configuration;
using TecDemo.Mobile.Contracts.Analytics;
using TecDemo.Mobile.Contracts.Caching;
using TecDemo.Mobile.Contracts.Configuration;
using TecDemo.Mobile.Contracts.Networking;
using TecDemo.Mobile.Contracts.Storage;
using TecDemo.ViewModels.Contracts;
using TecDemo.Views;
using TecDemo.ViewModels;
using TecDemo.Views.Contracts;
using TecDemo.Views.Pages;
using Xamarin.Forms;

namespace TecDemo.Android.Common
{
	public class ViewLocator : BaseViewLocator
	{
		private static readonly Lazy<ViewLocator> _instance = new Lazy<ViewLocator>(() => new ViewLocator());

		public static ViewLocator Instance
		{
			get { return _instance.Value; }
		}

		public override void WirePlatformDependentServices(ContainerBuilder builder)
		{
			builder.RegisterType<PlatformInformationProvider>().As<IPlatformInfoProvider>();
			builder.RegisterType<NetworkInformation>().As<INetworkInformation>()
				.SingleInstance();
			builder.RegisterType<GoogleAnalyticsService>().As<IAnalyticsService>()
				.SingleInstance();
			builder.RegisterType<MobileConfigurationProvider>().As<IMobileConfigurationProvider>();

			// TODO: JsonStorage currently not tested for Android, so we use a InMemoryCache
			builder.Register(context => new CurrencyListService(context.Resolve<ICurrencyUpdateService>(),
				context.ResolveNamed<ICache>("InMemoryCache")))
				.As<ICurrencyListService>()
				.SingleInstance();
		}
	}
}