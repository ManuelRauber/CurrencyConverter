using System.IO;
using System.Threading.Tasks;
using Android.App;
using TecDemo.Mobile.Configuration;
using TecDemo.Mobile.Contracts.Configuration;
using TecDemo.Mobile.Text;

namespace TecDemo.Mobile.Android.Configuration
{
	public class MobileConfigurationProvider : IMobileConfigurationProvider
	{
		private const string LocalFileName = "MobileConfiguration.json";

		public async Task<IMobileConfiguration> LoadAsync()
		{
			var jsonConfigurationString = await LoadConfigurationFileAsync();

			if (System.String.IsNullOrWhiteSpace(jsonConfigurationString))
			{
				throw new InvalidDataException("Json configuration is invalid or file not found!");
			}

			var result = JsonConverter.Deserialize<MobileConfiguration>(jsonConfigurationString);

			return result;
		}

		private async Task<string> LoadConfigurationFileAsync()
		{
			var assets = Application.Context.Assets;

			try
			{
				using (var stream = assets.Open(LocalFileName))
				{
					using (var streamReader = new StreamReader(stream))
					{
						return await streamReader.ReadToEndAsync();
					}
				}
			}
			catch
			{
				return System.String.Empty;
			}
		}
	}
}