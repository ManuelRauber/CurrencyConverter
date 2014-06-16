using TecDemo.CurrencyConverter.Contracts.Models;

namespace TecDemo.CurrencyConverter.Contracts.Services
{
	public interface IFlagService
	{
		string MapCurrencyToFlag(string basePath, ICurrency currency);
		string MapStringToFlag(string basePath, string currencyName);
	}
}