using System;
using System.Collections.Generic;
using TecDemo.CurrencyConverter.Contracts.Models;

namespace TecDemo.CurrencyConverter.Contracts.Services
{
	public delegate void UpdateErrorHandler(Exception exception); 
	public interface ICurrencyListService
	{
		event Action OnUpdateBegin;
		event Action OnUpdateEnd;
		event UpdateErrorHandler OnUpdateError;

		IEnumerable<ICurrency> Currencies { get; }
		ICurrency GetCurrency(string shortName);
		void UpdateCurrencies();
	}
}