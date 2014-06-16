using Autofac;
using TecDemo.Views.Pages;
using Xamarin.Forms;

namespace TecDemo.Views.Contracts
{
	public interface IViewLocator
	{
		IContainer Container { get; }
		void WirePlatformDependentServices(ContainerBuilder builder);
		MainPage MainPage { get; }
		CurrencyListPage CurrencyListPage { get; }
	}
}