using System;
using System.Globalization;
using Xamarin.Forms;

namespace TecDemo.Views.Converters
{
	public class StringToCurrencyConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var number = (decimal) value;

			return String.Format("{0:#.###}", number);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			decimal number = 0;
			Decimal.TryParse((string)value, out number);
			return number;
		}
	}
}