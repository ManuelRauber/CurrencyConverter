using System.Collections.Generic;
using System.Threading.Tasks;
using TecDemo.CurrencyConverter.Contracts.Models;

namespace TecDemo.CurrencyConverter.Contracts.Services
{
	/// <summary>
	/// Provides methods for getting an updated list of currencies
	/// </summary>
	public interface ICurrencyUpdateService
	{
		/// <summary>
		/// Loads all currencies. Usually by making a request to an external source
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<ICurrency>> DownloadAllAsync();

		/// <summary>
		/// Returns the currency all currencies from <see cref="DownloadAllAsync"/> are quoted against.
		/// </summary>
		/// <returns></returns>
		ICurrency GetUnderlyingCurrency();
	}
}