using System;
using System.IO;
using System.Threading.Tasks;
using TecDemo.Mobile.Configuration;
using TecDemo.Mobile.Contracts.Configuration;
using TecDemo.Mobile.Text;

namespace TecDemo.Mobile.Touch.Configuration
{
	public class MobileConfigurationProvider : IMobileConfigurationProvider
	{
		private const string LocalFileName = @"Data/MobileConfiguration.json";

		public async Task<IMobileConfiguration> LoadAsync()
		{
			string jsonConfigurationString = await LoadConfigurationFromFileAsync();

			if (String.IsNullOrWhiteSpace(jsonConfigurationString))
			{
				throw new InvalidDataException("Json configuration is invalid or file not found!");
			}

			var result = JsonConverter.Deserialize<MobileConfiguration>(jsonConfigurationString);

			return result;
		}

		private async Task<string> LoadConfigurationFromFileAsync()
		{
			try
			{
				var jsonConfigurationString = File.ReadAllText(LocalFileName);
				return jsonConfigurationString;
			}
			catch
			{
				return String.Empty;
			}
		}
	}
}