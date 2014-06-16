using TecDemo.CurrencyConverter.Contracts.Models;

namespace TecDemo.CurrencyConverter.Contracts.Services
{
	public interface IModelFactory
	{
		ICurrency CreateCurrency();
	}
}