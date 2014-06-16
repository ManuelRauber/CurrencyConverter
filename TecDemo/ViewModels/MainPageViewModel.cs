using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TecDemo.Mobile;
using TecDemo.CurrencyConverter.Contracts.Models;
using TecDemo.CurrencyConverter.Contracts.Services;
using TecDemo.Mobile.Contracts.Analytics;
using TecDemo.Mobile.Contracts.Caching;
using TecDemo.ViewModels.Contracts;

namespace TecDemo.ViewModels
{
	public class MainPageViewModel : BaseViewModel, IMainPageViewModel
	{
		private readonly ICurrencyConverterService _currencyConverter;
		private readonly ICurrencyListService _currencyList;
		private readonly IModelFactory _modelFactory;
		private ICurrency _sourceCurrency;
		private ICurrency _targetCurrency;
		private Decimal _sourceCurrencyInput;
		private Decimal _targetCurrencyInput;
		private bool _calculateSourceCurrency = true;
		private bool _calculateTargetCurrency = true;

		public MainPageViewModel(ICurrencyConverterService currencyConverter, 
			ICurrencyListService currencyList, 
			IAnalyticsService analyticsService,
			INavigationCache navigationCache,
			IModelFactory modelFactory) 
			: base(analyticsService, navigationCache)
		{
			_currencyConverter = currencyConverter;
			_currencyList = currencyList;
			_modelFactory = modelFactory;

			SetSelectedCurrenciesIfPossible();
		}

		public void SetSelectedCurrenciesIfPossible()
		{
			var sourceCurrency = NavigationCache.Get<ICurrency>("SourceCurrency");
			if (sourceCurrency != null)
			{
				UpdateCurrency(SourceCurrency, sourceCurrency);
			}

			var targetCurrency = NavigationCache.Get<ICurrency>("TargetCurrency");
			if (targetCurrency != null)
			{
				UpdateCurrency(TargetCurrency, targetCurrency);
			}

			ConvertFromSourceToTargetCurrency();
		}

		public ICurrency SourceCurrency
		{
			get { return _sourceCurrency ?? (_sourceCurrency = CreateSourceCurrency()); }
			set { Set(ref _sourceCurrency, value); }
		}

		private ICurrency CreateSourceCurrency()
		{
			var currency = _modelFactory.CreateCurrency();
			currency.LastUpdate = DateTime.Now;
			currency.ShortName = "USD";
			currency.LongName = "USD";
			currency.Value = 1;
			return currency;
		}

		private ICurrency CreateTargetCurrency()
		{
			var currency = _modelFactory.CreateCurrency();
			currency.LastUpdate = DateTime.Now;
			currency.ShortName = "EUR";
			currency.LongName = "EUR";
			currency.Value = 1;
			return currency;
		}

		public ICurrency TargetCurrency
		{
			get { return _targetCurrency  ?? (_targetCurrency = CreateTargetCurrency()); }
			set { Set(ref _targetCurrency, value); }
		}

		public void SelectSourceCurrency()
		{
			SelectCurrency(UpdateCurrencyTarget.SourceCurrency);
		}

		private void SelectCurrency(UpdateCurrencyTarget updateCurrencyTarget)
		{
			NavigationCache.Add("SourceCurrency", SourceCurrency);
			NavigationCache.Add("TargetCurrency", TargetCurrency);
			NavigationCache.Add("UpdateCurrency", updateCurrencyTarget);
			DoNavigateToCurrencyListPage();
		}

		public void SelectTargetCurrency()
		{
			SelectCurrency(UpdateCurrencyTarget.TargetCurrency);
		}

		public event Action NavigateToCurrencyListPage;

		protected virtual void DoNavigateToCurrencyListPage()
		{
			var handler = NavigateToCurrencyListPage;
			if (handler != null)
			{
				handler();
			}
		}

		public Decimal SourceCurrencyInput
		{
			get { return _sourceCurrencyInput; }
			set
			{
				if (Set(ref _sourceCurrencyInput, value)) {
					if (_calculateTargetCurrency)
					{
						ConvertFromSourceToTargetCurrency();
					}
				}
			}
		}

		public Decimal TargetCurrencyInput
		{
			get { return _targetCurrencyInput; }
			set
			{
				if (Set(ref _targetCurrencyInput, value))
				{
					// BUG: due to a bug in Xamarin.Forms (https://bugzilla.xamarin.com/show_bug.cgi?id=20414) we can't use a vice-versa convertion 
					//if (_calculateSourceCurrency)
					//{
					//	ConvertFromTargetToSourceCurrency();
					//}
				}
			}
		}

		public async Task UpdateRatesAsync()
		{
			await Task.Run(() => _currencyList.UpdateCurrencies());

			UpdateCurrency(SourceCurrency, _currencyList.GetCurrency(SourceCurrency.ShortName));
			UpdateCurrency(TargetCurrency, _currencyList.GetCurrency(TargetCurrency.ShortName));
		}

		private void UpdateCurrency(ICurrency storage, ICurrency newCurrency)
		{
			storage.LastUpdate = newCurrency.LastUpdate;
			storage.LongName = newCurrency.LongName;
			storage.ShortName = newCurrency.ShortName;
			storage.Reference = newCurrency.Reference;
			storage.Value = newCurrency.Value;
		}

		private void ConvertFromSourceToTargetCurrency()
		{
			var result = _currencyConverter.Calculate(SourceCurrencyInput, SourceCurrency, TargetCurrency);

			_calculateSourceCurrency = false;
			TargetCurrencyInput = result;
			_calculateSourceCurrency = true;
		}

		private void ConvertFromTargetToSourceCurrency()
		{
			var result = _currencyConverter.Calculate(TargetCurrencyInput, TargetCurrency, SourceCurrency);

			_calculateTargetCurrency = false;
			SourceCurrencyInput = result;
			_calculateTargetCurrency = true;
		}
	}
}