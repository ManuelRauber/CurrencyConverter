namespace TecDemo.Mobile.Contracts.Configuration
{
	public interface IGoogleAnalyticsConfiguration
	{
		/// <summary>
		/// Disable google analytics
		/// </summary>
		bool IsEnabled { get; set; }

		/// <summary>
		/// The Google Analytics tracking ID to which to send your data. Dashes in the ID must be unencoded.
		/// </summary>
		string TrackingId { get; set; }

		/// <summary>
		/// The name of your app, used in the app name dimension in your reports.
		/// </summary>
		/// TODO: This should be get automatically from the app itself
		string AppName { get; set; }

		/// <summary>
		/// The version of your application, used in the app version dimension within your reports. 
		/// </summary>
		/// TODO: This should be get automatically from the app itself
		string AppVersion { get; set; }

		/// <summary>
		/// Session timeout in Minutes (defaults to 5)
		/// </summary>
		int SessionTimeout { get; set; }

		/// <summary>
		/// The dispatch period in seconds. Defaults to 30 seconds.
		/// </summary>
		int DispatchPeriod { get; set; }

		/// <summary>
		/// Tells Google Analytics to anonymize the information sent by the tracker objects by removing the last octet of the IP address prior to its storage. Note that this will slightly reduce the accuracy of geographic reporting. false by default.
		/// </summary>
		bool AnonymizeIp { get; set; }
// ReSharper disable InconsistentNaming
		/// <summary>
		/// If true, causes all hits to be sent to the secure (SSL) Google Analytics endpoint. Default is false.
		/// </summary>
		bool UseSSL { get; set; }
// ReSharper restore InconsistentNaming
	}
}