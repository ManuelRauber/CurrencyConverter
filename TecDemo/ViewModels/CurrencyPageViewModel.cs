using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TecDemo.Mobile;
using TecDemo.CurrencyConverter.Contracts.Models;
using TecDemo.CurrencyConverter.Contracts.Services;
using TecDemo.Mobile.Contracts.Analytics;
using TecDemo.Mobile.Contracts.Caching;
using TecDemo.ViewModels.Contracts;

namespace TecDemo.ViewModels
{
	public class CurrencyPageViewModel : BaseViewModel, ICurrencyListPageViewModel
	{
		private readonly ICurrencyListService _currencyListService;
		private ObservableCollection<ICurrency> _currencies;

		public ObservableCollection<ICurrency> Currencies
		{
			get { return _currencies; }
			set { Set(ref _currencies, value); }
		}

		public void CurrencyTapped(ICurrency currency)
		{
			var currencyToUpdate = NavigationCache.Get<UpdateCurrencyTarget>("UpdateCurrency");

			if (currencyToUpdate == UpdateCurrencyTarget.SourceCurrency)
			{
				NavigationCache.Add("SourceCurrency", currency);
				DoNavigateToMainPage();
				return;
			}

			NavigationCache.Add("TargetCurrency", currency);
			DoNavigateToMainPage();
		}

		public event Action NavigateToMainPage;

		protected virtual void DoNavigateToMainPage()
		{
			var handler = NavigateToMainPage;
			if (handler != null)
			{
				handler();
			}
		}

		public CurrencyPageViewModel(ICurrencyListService currencyListService, IAnalyticsService analyticsService, INavigationCache navigationCache) 
			: base(analyticsService, navigationCache)
		{
			_currencyListService = currencyListService;
			AddCurrenciesToList();
		}

		private void AddCurrenciesToList()
		{
			_currencies = new ObservableCollection<ICurrency>(_currencyListService.Currencies);
		}
	}
}