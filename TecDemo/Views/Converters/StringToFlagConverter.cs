using System;
using System.Globalization;
using TecDemo.CurrencyConverter.Contracts.Services;
using Xamarin.Forms;

namespace TecDemo.Views.Converters
{
	public class StringToFlagConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var flagService = parameter as IFlagService;

			if (null == flagService)
			{
				return null;
			}

			var flag = flagService.MapStringToFlag(Device.OnPlatform("Flags/", "Assets/Flags/", "Assets/Flags/"), value.ToString());
			return new FileImageSource() {File = flag};
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}