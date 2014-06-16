using System;
using System.Globalization;
using TecDemo.CurrencyConverter.Contracts.Models;
using Xamarin.Forms;

namespace TecDemo.Views.Converters
{
	public class RateToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// We don't use the value here, due to a restriction of Xamarin.Forms to use a object directly
			var currency = parameter as ICurrency;

			if (null == currency)
			{
				return String.Empty;
			}

			var numberFormatInformation = (NumberFormatInfo) CultureInfo.CurrentCulture.NumberFormat.Clone();
			
			numberFormatInformation.CurrencySymbol = currency.ShortName;
			var firstPart = String.Format(numberFormatInformation, "{0:C}", currency.Value);

			string secondPart = String.Empty;

			if (null != currency.Reference)
			{
				numberFormatInformation.CurrencySymbol = currency.Reference.ShortName;
				secondPart = String.Format(numberFormatInformation, "{0:C}", currency.Reference.Value);
			}
		
			var completeCurrencyString = String.Format(String.IsNullOrWhiteSpace(secondPart) ? "{0}" : "{0} = {1}", firstPart, secondPart);
			return completeCurrencyString;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}