using System;
using System.Diagnostics;

namespace TecDemo.Mobile.Caching
{
	[DebuggerStepThrough]
	public class Expires
	{
		internal int Value { get; set; }

		public static ExpireQuantifier In(int value)
		{
			return new ExpireQuantifier(new Expires()
			{
				Value = value,
			});
		}
	}

	[DebuggerStepThrough]
	public class ExpireQuantifier
	{
		private readonly Expires _expires;

		internal ExpireQuantifier(Expires expires)
		{
			_expires = expires;
		}

		public DateTime Seconds()
		{
			return DateTime.Now.AddSeconds(_expires.Value);
		}

		public DateTime Minutes()
		{
			return DateTime.Now.AddMinutes(_expires.Value);
		}

		public DateTime Hours()
		{
			return DateTime.Now.AddHours(_expires.Value);
		}

		public DateTime Days()
		{
			return DateTime.Now.AddDays(_expires.Value);
		}

		public DateTime Months()
		{
			return DateTime.Now.AddMonths(_expires.Value);
		}

		public DateTime Years()
		{
			return DateTime.Now.AddYears(_expires.Value);
		}
	}
}