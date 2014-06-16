using System;
using Autofac;
using GoogleAnalytics.Core;
using TecDemo.CurrencyConverter.Contracts.Services;
using TecDemo.CurrencyConverter.Services;
using TecDemo.Mobile.Analytics;
using TecDemo.Mobile.Contracts.Analytics;
using TecDemo.Mobile.Contracts.Caching;
using TecDemo.Mobile.Contracts.Configuration;
using TecDemo.Mobile.Contracts.Networking;
using TecDemo.Mobile.Contracts.Storage;
using TecDemo.Mobile.Touch.Analytics;
using TecDemo.Mobile.Touch.Configuration;
using TecDemo.Mobile.Touch.Networking;
using TecDemo.Mobile.Touch.Storage;
using TecDemo.Views.Pages;

namespace TecDemo.TouchUniversal.Common
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
			builder.RegisterType<JsonStorage>().As<IStorage>();
			builder.RegisterType<MobileConfigurationProvider>().As<IMobileConfigurationProvider>();

			// TODO: JsonStorage currently not tested for iOS, so we use a InMemoryCache
			builder.Register(context => new CurrencyListService(context.Resolve<ICurrencyUpdateService>(),
				context.ResolveNamed<ICache>("InMemoryCache")))
				.As<ICurrencyListService>()
				.SingleInstance();
		}
	}
}