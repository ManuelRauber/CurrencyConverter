using System;

namespace TecDemo.CurrencyConverter.Contracts.Models
{
	public interface ICurrency
	{
		/// <summary>
		/// Short name of the currency (e.g. USD, EUR)
		/// </summary>
		string ShortName { get; set; }

		/// <summary>
		/// Long name of the currency (e.g. US-Dollar, Euro)
		/// </summary>
		string LongName { get; set; }

		/// <summary>
		/// Last update of this currency
		/// </summary>
		DateTime LastUpdate { get; set; }

		/// <summary>
		/// All values are referenced to US-Dollar
		/// </summary>
		Decimal Value { get; set; }

		/// <summary>
		/// Should always be USD
		/// </summary>
		ICurrency Reference { get; set; }
	}
}