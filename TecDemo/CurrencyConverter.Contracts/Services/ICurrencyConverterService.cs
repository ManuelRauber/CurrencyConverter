using System;
using TecDemo.CurrencyConverter.Contracts.Models;

namespace TecDemo.CurrencyConverter.Contracts.Services
{
	public interface ICurrencyConverterService
	{
		Decimal Calculate(Decimal value, ICurrency from, ICurrency to);
	}
}