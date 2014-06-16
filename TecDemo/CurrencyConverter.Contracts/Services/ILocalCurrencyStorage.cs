using System.Collections.Generic;
using TecDemo.CurrencyConverter.Contracts.Models;

namespace TecDemo.CurrencyConverter.Contracts.Services
{
	public interface ILocalCurrencyStorage
	{
		/// <summary>
		/// Saves <paramref name="list"/> to a local storage
		/// </summary>
		/// <param name="list"></param>
		void Save(IEnumerable<ICurrency> list);

		/// <summary>
		/// Loads a saved <see cref="ICurrency"/> list from a local storage
		/// </summary>
		/// <returns></returns>
		IEnumerable<ICurrency> Load();
	}
}