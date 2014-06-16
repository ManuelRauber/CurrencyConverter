using System;
using System.Threading.Tasks;
using Autofac;
using GoogleAnalytics.Core;
using TecDemo.CurrencyConverter.Contracts.Services;
using TecDemo.CurrencyConverter.Services;
using TecDemo.Mobile.Analytics;
using TecDemo.Mobile.Caching;
using TecDemo.Mobile.Configuration;
using TecDemo.Mobile.Contracts.Analytics;
using TecDemo.Mobile.Contracts.Caching;
using TecDemo.Mobile.Contracts.Configuration;
using TecDemo.Mobile.Contracts.Networking;
using TecDemo.Mobile.Contracts.Storage;
using TecDemo.Mobile.WindowsPhone.Analytics;
using TecDemo.Mobile.WindowsPhone.Configuration;
using TecDemo.Mobile.WindowsPhone.Networking;
using TecDemo.Mobile.WindowsPhone.Storage;
using TecDemo.ViewModels;
using TecDemo.ViewModels.Contracts;
using TecDemo.Views.Contracts;
using TecDemo.Views.Pages;
using Xamarin.Forms;

namespace TecDemo.WindowsPhone.Common
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
		}
	}
}