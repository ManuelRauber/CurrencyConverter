using System;
using System.Linq.Expressions;
using TecDemo.ViewModels.Properties;
using Xamarin.Forms;

namespace TecDemo.Views.Extensions
{
	public static class BindableObjectExtensions
	{
		// Different name to avoid conflict with orginal binding method
		public static void BindTo<TSource>([NotNull] this BindableObject self, [NotNull] BindableProperty targetProperty,
			[NotNull] Expression<Func<TSource, object>> sourceProperty)
		{
			BindTo<TSource>(self, targetProperty, sourceProperty, BindingMode.Default);
		}

		public static void BindTo<TSource>([NotNull] this BindableObject self, [NotNull] BindableProperty targetProperty,
			[NotNull] Expression<Func<TSource, object>> sourceProperty, BindingMode mode)
		{
			BindTo<TSource>(self, targetProperty, sourceProperty, mode, null);
		}

		public static void BindTo<TSource>([NotNull] this BindableObject self, [NotNull] BindableProperty targetProperty,
			[NotNull] Expression<Func<TSource, object>> sourceProperty, BindingMode mode,
			IValueConverter converter)
		{
			BindTo<TSource>(self, targetProperty, sourceProperty, mode, converter, null);
		}

		public static void BindTo<TSource>([NotNull] this BindableObject self, [NotNull] BindableProperty targetProperty,
			[NotNull] Expression<Func<TSource, object>> sourceProperty, BindingMode mode,
			IValueConverter converter, object converterParameter)
		{
			BindTo<TSource>(self, targetProperty, sourceProperty, mode, converter, converterParameter, null);
		}

		public static void BindTo<TSource>([NotNull] this BindableObject self, [NotNull] BindableProperty targetProperty,
			[NotNull] Expression<Func<TSource, object>> sourceProperty, BindingMode mode,
			IValueConverter converter, object converterParameter, string stringFormat)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			if (targetProperty == null)
			{
				throw new ArgumentNullException("targetProperty");
			}
			if (sourceProperty == null)
			{
				throw new ArgumentNullException("sourceProperty");
			}

			var binding = Binding.Create(sourceProperty, mode, converter, converterParameter, stringFormat);
			self.SetBinding(targetProperty, binding);
		}
	}
}