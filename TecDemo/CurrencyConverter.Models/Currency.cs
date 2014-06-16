using System;
using TecDemo.CurrencyConverter.Contracts.Models;
using TecDemo.Mobile;

namespace TecDemo.CurrencyConverter.Models
{
	public class Currency : BindableBase, ICurrency
	{
		private string _shortName;
		private string _longName;
		private DateTime _lastUpdate;
		private decimal _value;
		private ICurrency _reference;

		/// <summary>
		/// Short name of the currency (e.g. USD, EUR)
		/// </summary>
		public string ShortName
		{
			get { return _shortName; }
			set { Set(ref _shortName, value); }
		}

		/// <summary>
		/// Long name of the currency (e.g. US-Dollar, Euro)
		/// </summary>
		public string LongName
		{
			get { return _longName; }
			set { Set(ref _longName, value); }
		}

		/// <summary>
		/// Last update of this currency
		/// </summary>
		public DateTime LastUpdate
		{
			get { return _lastUpdate; }
			set { Set(ref _lastUpdate, value); }
		}

		/// <summary>
		/// All values are referenced to US-Dollar
		/// </summary>
		public Decimal Value
		{
			get { return _value; }
			set { Set(ref _value, value); }
		}

		/// <summary>
		/// Should always be USD
		/// </summary>
		public ICurrency Reference
		{
			get { return _reference; }
			set { Set(ref _reference, value); }
		}
	}
}