using TecDemo.CurrencyConverter.Contracts.Models;
using TecDemo.CurrencyConverter.Contracts.Services;
using TecDemo.CurrencyConverter.Models;

namespace TecDemo.CurrencyConverter.Services
{
	public class ModelFactory : IModelFactory
	{
		public ICurrency CreateCurrency()
		{
			return new Currency();
		}
	}
}