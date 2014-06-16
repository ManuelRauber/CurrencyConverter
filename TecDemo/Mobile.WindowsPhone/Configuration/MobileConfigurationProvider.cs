using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using TecDemo.Mobile.Configuration;
using TecDemo.Mobile.Contracts.Configuration;
using TecDemo.Mobile.Text;

namespace TecDemo.Mobile.WindowsPhone.Configuration
{
	public class MobileConfigurationProvider : IMobileConfigurationProvider
	{
		private const string LocalFileName = @"ms-appx:///Assets/MobileConfiguration.json";

		/// <summary>
		///   Tries to load the configuration
		/// </summary>
		/// <returns></returns>
		public async Task<IMobileConfiguration> LoadAsync()
		{
			string jsonConfigurationString = await LoadConfigurationFileAsync();

			if (String.IsNullOrWhiteSpace(jsonConfigurationString))
			{
				throw new InvalidDataException("Json configuration is invalid or file not found!");
			}

			var result = JsonConverter.Deserialize<MobileConfiguration>(jsonConfigurationString);

			return result;
		}

		private async Task<String> LoadConfigurationFileAsync()
		{
			try
			{
				StorageFile file = await LoadStorageFile();

				using (Stream stream = await file.OpenStreamForReadAsync())
				{
					using (var streamReader = new StreamReader(stream))
					{
						return await streamReader.ReadToEndAsync();
					}
				}
			}
			catch
			{
				// Catch all errors and return an empty file, so we can load a default configuration
				return String.Empty;
			}
		}

		private async Task<StorageFile> LoadStorageFile()
		{
			var uri = new Uri(LocalFileName);
			StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
			return file;
		}
	}
}