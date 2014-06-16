using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TecDemo.CurrencyConverter.Contracts.Models;

namespace TecDemo.ViewModels.Contracts
{
	public interface IMainPageViewModel : IBaseViewModel
	{
		/// <summary>
		/// First currency to show
		/// </summary>
		ICurrency SourceCurrency { get; set; }

		/// <summary>
		/// Second currency to show
		/// </summary>
		ICurrency TargetCurrency { get; set; }

		/// <summary>
		/// Should be called when the user wants to select another source currency
		/// </summary>
		/// <returns></returns>
		void SelectSourceCurrency();

		/// <summary>
		/// Should be called when the user wants to select another target currency
		/// </summary>
		/// <returns></returns>
		void SelectTargetCurrency();

		/// <summary>
		/// Will be executed when the MainPage wants to go to the currency list page
		/// </summary>
		event Action NavigateToCurrencyListPage;

		/// <summary>
		/// User input of first currency (used for actual calculation)
		/// </summary>
		Decimal SourceCurrencyInput { get; set; }

		/// <summary>
		/// User input of second currency (used for actual calculation)
		/// </summary>
		Decimal TargetCurrencyInput { get; set; }

		/// <summary>
		/// Updates the rates
		/// </summary>
		/// <returns></returns>
		Task UpdateRatesAsync();

		void SetSelectedCurrenciesIfPossible();
	}
}