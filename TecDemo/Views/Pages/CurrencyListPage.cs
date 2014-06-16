using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using TecDemo.CurrencyConverter.Contracts.Models;
using TecDemo.CurrencyConverter.Contracts.Services;
using TecDemo.ViewModels.Contracts;
using TecDemo.Views.Contracts;
using TecDemo.Views.Converters;
using TecDemo.Views.Extensions;
using Xamarin.Forms;

namespace TecDemo.Views.Pages
{
	public class CurrencyListPage : ContentPage
	{
		private readonly ICurrencyListPageViewModel _viewModel;
		private readonly IFlagService _flagService;

		public CurrencyListPage(ICurrencyListPageViewModel viewModel, IFlagService flagService)
		{
			Title = "Currency list";
			_viewModel = viewModel;
			SetViewModelEvents();
			_flagService = flagService;
			BindingContext = _viewModel;
			CreateUI();
		
			this.SetDefaultPadding();
		}

		private void SetViewModelEvents()
		{
			_viewModel.NavigateToMainPage += async () => await Navigation.PopAsync();
		}

		private void CreateUI()
		{
			var listView = CreateListView();

			Content = listView;
		}

		private ListView CreateListView()
		{
			var listView = new ListView()
			{
				ItemsSource = _viewModel.Currencies,
				ItemTemplate = new DataTemplate(CreateListViewItemTemplate),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};

			listView.ItemSelected += (sender, args) => _viewModel.CurrencyTapped((ICurrency) args.SelectedItem);

			return listView;
		}

		private ViewCell CreateListViewItemTemplate()
		{
			var image = new Image()
			{
				WidthRequest = 160,
				MinimumWidthRequest = 100,
			};
			image.BindTo<ICurrency>(Image.SourceProperty, c => c.ShortName, BindingMode.OneWay, new StringToFlagConverter(), _flagService);
			
			var longNameLabel = new Label();
			longNameLabel.BindTo<ICurrency>(Label.TextProperty, c => c.LongName, BindingMode.OneWay);

			var shortNameLabel = new Label();
			shortNameLabel.BindTo<ICurrency>(Label.TextProperty, c => c.ShortName, BindingMode.OneWay);

			var labelStackLayout = new StackLayout();
			labelStackLayout.Children.AddRange(longNameLabel, shortNameLabel);
			labelStackLayout.Padding = new Thickness(10, 5, 0, 5);

			var itemStackLayout = new StackLayout()
			{
				Padding = new Thickness(0, 5),
				Orientation = StackOrientation.Horizontal,
			};
			itemStackLayout.Children.AddRange(image, labelStackLayout);

			return new ViewCell()
			{
				View = itemStackLayout
			};
		}

		protected override void OnAppearing()
		{
			_viewModel.AnalyticsService.TrackView("CurrencyList");
		}
	}
}