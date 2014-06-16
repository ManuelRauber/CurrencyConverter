using System.Threading.Tasks;
using Autofac;
using TecDemo.CurrencyConverter.Contracts.Services;
using TecDemo.CurrencyConverter.Services;
using TecDemo.Mobile.Caching;
using TecDemo.Mobile.Configuration;
using TecDemo.Mobile.Contracts.Caching;
using TecDemo.Mobile.Contracts.Configuration;
using TecDemo.ViewModels;
using TecDemo.ViewModels.Contracts;
using TecDemo.Views.Contracts;

namespace TecDemo.Views.Pages
{
	public class BaseViewLocator : IViewLocator
	{
		private IContainer _container;

		public IContainer Container
		{
			get { return _container; }
		}

		public BaseViewLocator()
		{
			BuildIoCContainer();
		}

		private void BuildIoCContainer()
		{
			var builder = new ContainerBuilder();

			WireConfiguration(builder);
			WireViewModels(builder);
			WirePages(builder);
			WireServices(builder);
			WireCaches(builder);
			WirePlatformDependentServices(builder);

			builder.Register(context => this).As<IViewLocator>();

			_container = builder.Build();
		}

		private void WireServices(ContainerBuilder builder)
		{
			builder.RegisterType<DynamicFlagService>().As<IFlagService>();
			builder.RegisterType<ECBCurrencyUpdateService>().As<ICurrencyUpdateService>();
			builder.RegisterType<CurrencyConverterService>().As<ICurrencyConverterService>();
			builder.RegisterType<ModelFactory>().As<IModelFactory>();

			builder.Register(context => new CurrencyListService(context.Resolve<ICurrencyUpdateService>(),
				context.ResolveNamed<ICache>("FileCache")))
				.As<ICurrencyListService>()
				.SingleInstance();
		}

		public virtual void WirePlatformDependentServices(ContainerBuilder builder) { }

		private void WireConfiguration(ContainerBuilder builder)
		{
			builder.RegisterType<MobileConfiguration>().As<IMobileConfiguration>()
				.OnActivating(args =>
				{
					var mobileConfigurationProvider = args.Context.Resolve<IMobileConfigurationProvider>();
					var configurationTask = Task.Run(async () => await mobileConfigurationProvider.LoadAsync());
					var configuration = configurationTask.Result;
					args.ReplaceInstance(configuration);
				})
				.SingleInstance();
		}

		private void WireViewModels(ContainerBuilder builder)
		{
			builder.RegisterType<MainPageViewModel>().As<IMainPageViewModel>();
			builder.RegisterType<CurrencyPageViewModel>().As<ICurrencyListPageViewModel>();
		}

		private void WirePages(ContainerBuilder builder)
		{
			builder.RegisterType<MainPage>();
			builder.RegisterType<CurrencyListPage>();
		}

		private void WireCaches(ContainerBuilder builder)
		{
			builder.RegisterType<InMemoryCache>().Named<ICache>("InMemoryCache").SingleInstance();
			builder.RegisterType<FileCache>().Named<ICache>("FileCache").SingleInstance();
			builder.RegisterType<NavigationCache>().As<INavigationCache>().SingleInstance();
		}

		public MainPage MainPage
		{
			get { return _container.Resolve<MainPage>(); }
		}

		public CurrencyListPage CurrencyListPage
		{
			get { return _container.Resolve<CurrencyListPage>(); }
		}
	}
}