using System;
using System.Windows.Input;
using TecDemo.CurrencyConverter.Contracts.Services;
using TecDemo.ViewModels;
using TecDemo.ViewModels.Contracts;
using TecDemo.Views.Components;
using TecDemo.Views.Contracts;
using TecDemo.Views.Converters;
using TecDemo.Views.Extensions;
using Xamarin.Forms;

namespace TecDemo.Views.Pages
{
	public class MainPage : ContentPage
	{
		private readonly IMainPageViewModel _viewModel;
		private readonly IFlagService _flagService;
		private readonly IViewLocator _viewLocator;

		public IMainPageViewModel ViewModel
		{
			get { return _viewModel; }
		}

		public MainPage(IMainPageViewModel viewModel, IFlagService flagService, IViewLocator viewLocator)
		{
			Title = "Currency Converter";
			_viewModel = viewModel;
			SetViewModelEvents();
			BindingContext = _viewModel;

			_flagService = flagService;
			_viewLocator = viewLocator;
			CreateUI();

			this.SetDefaultPadding();
		}

		private void SetViewModelEvents()
		{
			_viewModel.NavigateToCurrencyListPage += async () => await Navigation.PushAsync(_viewLocator.CurrencyListPage);
		}

		private void CreateUI()
		{
			var stackLayout = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};

			var firstCurrency = new MainPageCurrencyComponent(_viewModel.SourceCurrency, _flagService, () => _viewModel.SelectSourceCurrency());
			var secondCurrency = new MainPageCurrencyComponent(_viewModel.TargetCurrency , _flagService, () => _viewModel.SelectTargetCurrency());

			// Currently we need to bind here, since our CurrencyComponent uses ICurrency directly which does not provide a property for actual user input
			firstCurrency.InputBox.BindTo<IMainPageViewModel>(Entry.TextProperty, model => model.SourceCurrencyInput, BindingMode.TwoWay, new StringToCurrencyConverter(), _viewModel.SourceCurrency);
			secondCurrency.InputBox.BindTo<IMainPageViewModel>(Entry.TextProperty, model => model.TargetCurrencyInput, BindingMode.TwoWay, new StringToCurrencyConverter(), _viewModel.TargetCurrency);

			stackLayout.Children.AddRange(firstCurrency, secondCurrency);

			Content = stackLayout;
		}

		protected async override void OnAppearing()
		{
			_viewModel.AnalyticsService.TrackView("MainPage");
			await _viewModel.UpdateRatesAsync();
			_viewModel.SetSelectedCurrenciesIfPossible();
		}
	}
}