using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TecDemo.CurrencyConverter.Contracts.Models;

namespace TecDemo.ViewModels.Contracts
{
	public interface ICurrencyListPageViewModel : IBaseViewModel
	{
		ObservableCollection<ICurrency> Currencies { get; set; }
		void CurrencyTapped(ICurrency currency);
		event Action NavigateToMainPage;
	}
}