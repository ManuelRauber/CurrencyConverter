using Xamarin.Forms;

namespace TecDemo.Views.Extensions
{
	public static class PageExtensions
	{
		public static void SetDefaultPadding(this Page page)
		{
			page.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
		}
	}
}