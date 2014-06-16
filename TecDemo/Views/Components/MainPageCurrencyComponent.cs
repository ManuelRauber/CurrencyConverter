using System;
using TecDemo.CurrencyConverter.Contracts.Models;
using TecDemo.CurrencyConverter.Contracts.Services;
using TecDemo.ViewModels.Contracts;
using TecDemo.Views.Converters;
using TecDemo.Views.Extensions;
using Xamarin.Forms;

namespace TecDemo.Views.Components
{
	internal class MainPageCurrencyComponent : ContentView
	{
		private readonly ICurrency _currency;
		private readonly IFlagService _flagService;
		private readonly Action _tappedAction;
		private Entry _inputBox;

		public Entry InputBox
		{
			get { return _inputBox; }
		}

		internal MainPageCurrencyComponent(ICurrency currency, IFlagService flagService, Action tappedAction)
		{
			_currency = currency;
			_flagService = flagService;
			_tappedAction = tappedAction;
			CreateUI();
		}

		private void CreateUI()
		{
			var layoutPanel = CreateLayoutPanel();
			var rowPanel = CreateRowPanel();
			var currencyImage = CreateCurrencyImage();
			var currencyText = CreateCurrencyText();
			var currencyTextBox = CreateCurrencyTextBox();

			AddTapGestureRecognizer(currencyText);
			AddTapGestureRecognizer(currencyImage);

			rowPanel.Children.AddRange(currencyImage, currencyText);

			layoutPanel.Children.AddRange(rowPanel, currencyTextBox);

			Content = layoutPanel;
		}

		private Entry CreateCurrencyTextBox()
		{
			_inputBox = new Entry()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Keyboard = Keyboard.Numeric,
			};

			return _inputBox;
		}

		private StackLayout CreateCurrencyText()
		{
			var stackLayout = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};

			var longNameLabel = new Label();
			longNameLabel.SetBinding<ICurrency>(Label.TextProperty, c => c.LongName);

			var rateLabel = new Label();
			// Xamarin forms does not allow a binding direct to the object. It has to be a property, so we just use "value" and use currency as parameter for the converter
			rateLabel.BindTo<ICurrency>(Label.TextProperty, c => c.Value, BindingMode.OneWay, new RateToStringConverter(), _currency);

			var dateLabel = new Label();
			dateLabel.SetBinding<ICurrency>(Label.TextProperty, c => c.LastUpdate, BindingMode.OneWay, new DateTimeToStringConverter());

			stackLayout.Children.AddRange(longNameLabel, rateLabel, dateLabel);

			return stackLayout;
		}

		private void AddTapGestureRecognizer(View view)
		{
			view.GestureRecognizers.Add(new TapGestureRecognizer(view1 => _tappedAction()));
		}

		private Image CreateCurrencyImage()
		{
			var image = new Image()
			{
				MinimumWidthRequest = Device.OnPlatform(60d, 150d, 150d),
				WidthRequest = Device.OnPlatform(100d, 150d, 150d),
			};
			image.BindTo<ICurrency>(Image.SourceProperty, c => c.ShortName, BindingMode.OneWay, new StringToFlagConverter(), _flagService);

			return image;
		}

		private StackLayout CreateRowPanel()
		{
			var firstRow = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};

			firstRow.BindingContext = _currency;

			return firstRow;
		}

		private StackLayout CreateLayoutPanel()
		{
			var layoutPanel = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
			};
			return layoutPanel;
		}
	}
}