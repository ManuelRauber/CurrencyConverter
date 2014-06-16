using System;
using System.Collections.Generic;
using TecDemo.CurrencyConverter.Contracts.Models;
using TecDemo.CurrencyConverter.Contracts.Services;

namespace TecDemo.CurrencyConverter.Services
{
	public class FlagService : IFlagService
	{
		private IDictionary<string, string> _flagMapping;

		public FlagService()
		{
			InitializeFlagMapping();
		}

		private void InitializeFlagMapping()
		{
			_flagMapping = new Dictionary<string, string>()
			{
				{"USD", "us"},
				{"JPY", "jp"},
				{"BGN", "bg"},
				{"CZK", "cz"},
				{"DKK", "dk"},
				{"GBP", "gb"},
				{"HUF", "hu"},
				{"LTL", "lt"},
				{"PLN", "pl"},
				{"RON", "ro"},
				{"SEK", "se"},
				{"CHF", "ch"},
				{"NOK", "ho"},
				{"HRK", "hr"},
				{"RUB", "ru"},
				{"TRY", "tr"},
				{"AUD", "au"},
				{"BRL", "br"},
				{"CAD", "ca"},
				{"CNY", "cn"},
				{"HKD", "hk"},
				{"IDR", "id"},
				{"ILS", "il"},
				{"INR", "in"},
				{"KRW", "kr"},
				{"MXN", "mx"},
				{"MYR", "my"},
				{"NZD", "nz"},
				{"PHP", "ph"},
				{"SGD", "sg"},
				{"THB", "th"},
				{"ZAR", "za"},
				{"ISK", "is"},
				{"EUR", "eu"},
			};
		}

		/// <summary>
		/// Loads flag out of the app packages.
		/// 
		/// Paths:
		///	  * WP: Assets/Flags/
		///   * Android: Assets/Flags/
		///   * iOS: Resources/Flags/
		/// </summary>
		/// <param name="currency"></param>
		/// <returns></returns>
		public string MapCurrencyToFlag(string basePath, ICurrency currency)
		{
			if (null == currency)
			{
				return String.Empty;
			}

			string tmp;

			if (_flagMapping.TryGetValue(currency.ShortName, out tmp))
			{
				return FormatFlagName(basePath, tmp);
			}

			return String.Empty;
		}

		private string FormatFlagName(string basePath, string tmp)
		{
			return String.Format("{0}{1}.png", basePath, tmp);
		}

		public string MapStringToFlag(string basePath, string currencyName)
		{
			string tmp;
			if (_flagMapping.TryGetValue(currencyName, out tmp))
			{
				return FormatFlagName(basePath, tmp);
			}

			return String.Empty;
		}
	}
}