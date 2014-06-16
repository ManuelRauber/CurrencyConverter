using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TecDemo.CurrencyConverter.Contracts.Models;
using TecDemo.CurrencyConverter.Contracts.Services;
using TecDemo.CurrencyConverter.Models;
using TecDemo.Mobile.Caching;
using TecDemo.Mobile.Contracts.Caching;
using TecDemo.Mobile.Text;

namespace TecDemo.CurrencyConverter.Services
{
	[JsonObject]
	internal class CurrencyList
	{
		private List<ICurrency> _currencies;

		[JsonProperty(ItemConverterType = typeof(ConcreteTypeConverter<Currency>))]
		public List<ICurrency> Currencies
		{
			get { return _currencies ?? (_currencies = new List<ICurrency>()); }
			set { _currencies = value; }
		}
	}

	public class CurrencyListService : ICurrencyListService
	{
		private readonly ICurrencyUpdateService _updateService;
		private readonly ICache _cache;
		private CurrencyList _currencies;
		public event Action OnUpdateBegin;
		public event Action OnUpdateEnd;
		public event UpdateErrorHandler OnUpdateError;

		public CurrencyListService(ICurrencyUpdateService updateService, ICache cache)
		{
			_updateService = updateService;
			_cache = cache;
		}

		public IEnumerable<ICurrency> Currencies
		{
			get { return _currencies.Currencies; }
		}

		public ICurrency GetCurrency(string shortName)
		{
			var currency = _currencies.Currencies.SingleOrDefault(c => c.ShortName.Equals(shortName));
			if (currency == null)
			{
				throw new KeyNotFoundException(String.Format("Currency '{0}' not found", shortName));
			}

			return currency;
		}

		public void UpdateCurrencies()
		{
			_currencies = _cache.GetOrAdd(new CacheSettings("CurrencyList", Expires.In(10).Minutes()), () =>
			{
				var task = Task.Run(async () => await InternalUpdateCurrenciesAsync());
				return task.Result;
			});
		}

		private async Task<CurrencyList> InternalUpdateCurrenciesAsync()
		{
			DoUpdateBegin();

			try
			{
				var currencies = await _updateService.DownloadAllAsync();

				if (null == currencies)
				{
					return null;
				}

				var currencyList = new CurrencyList();
				currencyList.Currencies.AddRange(currencies);
				
		
				var underlyingCurrency = _updateService.GetUnderlyingCurrency();
				currencyList.Currencies.Add(underlyingCurrency);

				DoUpdateEnd();

				return currencyList;
			}
			catch (Exception exception)
			{
				DoUpdateError(exception);
			}

			return new CurrencyList();
		}

		private void DoUpdateBegin()
		{
			var handler = OnUpdateBegin;
			if (null != handler)
			{
				handler();
			}
		}

		private void DoUpdateEnd()
		{
			var handler = OnUpdateEnd;
			if (null != handler)
			{
				handler();
			}
		}
		private void DoUpdateError(Exception exception)
		{
			var handler = OnUpdateError;
			if (handler != null)
			{
				handler(exception);
			}
		}
	}
}