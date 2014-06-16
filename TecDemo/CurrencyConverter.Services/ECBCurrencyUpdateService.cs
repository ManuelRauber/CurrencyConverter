using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using TecDemo.CurrencyConverter.Contracts.Models;
using TecDemo.CurrencyConverter.Contracts.Services;

namespace TecDemo.CurrencyConverter.Services
{
// ReSharper disable InconsistentNaming
	/// <summary>
	/// Provides current exchange rates from the european central bank
	/// </summary>
	public class ECBCurrencyUpdateService : ICurrencyUpdateService
// ReSharper restore InconsistentNaming
	{
		private readonly IModelFactory _modelFactory;
		private readonly ICurrency _underlyingCurrency;
		private const string EcbXmlSourceUrl = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
		private readonly XNamespace _ecbNamespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref";

		public ECBCurrencyUpdateService(IModelFactory modelFactory)
		{
			_modelFactory = modelFactory;
			_underlyingCurrency = _modelFactory.CreateCurrency();

			_underlyingCurrency.LongName = "Euro";
			_underlyingCurrency.ShortName = "EUR";
			_underlyingCurrency.Value = 1;
			_underlyingCurrency.LastUpdate = DateTime.Today;
		}

		public async Task<IEnumerable<ICurrency>> DownloadAllAsync()
		{
			var xmlSource = await DownloadXml();
			IEnumerable<ICurrency> currencies = ReadCurrenciesFrom(xmlSource);
			return currencies;
		}

		private IEnumerable<ICurrency> ReadCurrenciesFrom(string xmlSource)
		{
			var document = XDocument.Parse(xmlSource);
			var updateTimeNode = document.Descendants(_ecbNamespace + "Cube")
				.First(node => node.Attribute("time") != null);
			var updateTime = DateTime.Parse(updateTimeNode.Attribute("time").Value);

			var nodes = document.Descendants(_ecbNamespace + "Cube")
				.Where(node => node.Attribute("currency") != null)
				.Select(node =>
				{
					var currency = _modelFactory.CreateCurrency();

					currency.LastUpdate = updateTime;
					currency.LongName = node.Attribute("currency").Value;
					currency.Reference = GetUnderlyingCurrency();
					currency.ShortName = node.Attribute("currency").Value;
					currency.Value = Decimal.Parse(node.Attribute("rate").Value, CultureInfo.InvariantCulture.NumberFormat);

					return currency;
				})
				.ToList();

			return nodes;
		}

		private async Task<string> DownloadXml()
		{
			var httpClient = new HttpClient();
			var xml = await httpClient.GetStringAsync(EcbXmlSourceUrl);
			return xml;
		}

		public ICurrency GetUnderlyingCurrency()
		{
			return _underlyingCurrency;
		}
	}
}