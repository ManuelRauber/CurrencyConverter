using System.IO;
using Android.OS;
using TecDemo.Mobile.Contracts.Storage;
using TecDemo.Mobile.Text;
using File = System.IO.File;

namespace TecDemo.Mobile.Android.Storage
{
	public class JsonStorage : IStorage
	{
		public string BasePath { get; set; }

		public void SaveTo<T>(string fileName, T objectToSave)
		{
			var path = GetStoragePath(fileName);

			using (var fileStream = new FileStream(path, FileMode.Create))
			{
				JsonConverter.Serialize(fileStream, objectToSave);
			}
		}

		public T LoadFrom<T>(string fileName)
		{
			try
			{
				var path = GetStoragePath(fileName);

				using (var fileStream = new FileStream(path, FileMode.Open))
				{
					var obj = JsonConverter.Deserialize<T>(fileStream);
					return obj;
				}
			}
			catch
			{
				return default(T);
			}
		}

		public bool Exists(string fileName)
		{
			return File.Exists(GetStoragePath(fileName));
		}

		public void Remove(string fileName)
		{
			File.Delete(GetStoragePath(fileName));
		}

		private string GetStoragePath(string fileName)
		{
			var documents = Environment.DataDirectory.Path;
			var path = Path.Combine(documents, "JsonStorage", BasePath, fileName);
			return path;
		}
	}
}