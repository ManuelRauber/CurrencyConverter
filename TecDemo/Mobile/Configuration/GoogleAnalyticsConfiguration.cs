using Newtonsoft.Json;
using TecDemo.Mobile.Contracts.Configuration;

namespace TecDemo.Mobile.Configuration
{
	[JsonObject]
	public class GoogleAnalyticsConfiguration : IGoogleAnalyticsConfiguration
	{
		private int _dispatchPeriod = 30;
		private int _sessionTimeout = 5;

		[JsonProperty]
		public bool IsEnabled { get; set; }

		[JsonProperty]
		public string TrackingId { get; set; }

		[JsonProperty]
		public string AppName { get; set; }

		[JsonProperty]
		public string AppVersion { get; set; }

		[JsonProperty]
		public int SessionTimeout
		{
			get { return _sessionTimeout; }
			set { _sessionTimeout = value; }
		}

		[JsonProperty]
		public int DispatchPeriod
		{
			get { return _dispatchPeriod; }
			set { _dispatchPeriod = value; }
		}

		[JsonProperty]
		public bool AnonymizeIp { get; set; }

		[JsonProperty]
		public bool UseSSL { get; set; }
	}
}