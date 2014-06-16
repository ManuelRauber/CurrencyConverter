using System.Collections.Generic;
using Xamarin.Forms;

namespace TecDemo.Views.Extensions
{
	public static class ViewExtensions
	{
		public static void AddRange(this IList<View> view, params View[] elements)
		{
			foreach (var element in elements)
			{
				view.Add(element);
			}
		}
	}
}