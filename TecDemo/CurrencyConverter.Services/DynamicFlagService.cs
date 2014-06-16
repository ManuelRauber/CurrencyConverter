using System;
using TecDemo.CurrencyConverter.Contracts.Models;
using TecDemo.CurrencyConverter.Contracts.Services;

namespace TecDemo.CurrencyConverter.Services
{
	public class DynamicFlagService : IFlagService
	{
		public string MapCurrencyToFlag(string basePath, ICurrency currency)
		{
			var tmp = currency.ShortName.Substring(0, 2).ToLower();
			return FormatFlagName(basePath, tmp);
		}

		private string FormatFlagName(string basePath, string tmp)
		{
			return String.Format("{0}{1}.png", basePath, tmp);
		}

		public string MapStringToFlag(string basePath, string currencyName)
		{
			var tmp = currencyName.Substring(0, 2).ToLower();
			return FormatFlagName(basePath, tmp);
		}
	}
}