using TecDemo.CurrencyConverter.Contracts.Models;
using TecDemo.CurrencyConverter.Contracts.Services;

namespace TecDemo.CurrencyConverter.Services
{
	public class CurrencyConverterService : ICurrencyConverterService
	{
		public decimal Calculate(decimal value, ICurrency @from, ICurrency to)
		{
			return value / @from.Value * to.Value;
		}
	}
}