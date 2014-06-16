using Newtonsoft.Json;
using TecDemo.Mobile.Contracts.Configuration;
using TecDemo.Mobile.Text;

namespace TecDemo.Mobile.Configuration
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MobileConfiguration : IMobileConfiguration
	{
		[JsonProperty("GoogleAnalytics")]
		[JsonConverter(typeof(ConcreteTypeConverter<GoogleAnalyticsConfiguration>))]
		public IGoogleAnalyticsConfiguration GoogleAnalyticsConfiguration { get; set; }
	}
}