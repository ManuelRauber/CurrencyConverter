using System;
using System.Globalization;
using Xamarin.Forms;

namespace TecDemo.Views.Converters
{
	public class DateTimeToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			DateTime tmp;

			if (DateTime.TryParse(value.ToString(), out tmp))
			{
				return String.Format("{0:G}", tmp);
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}