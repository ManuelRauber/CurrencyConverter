using System;
using System.IO;
using TecDemo.Mobile.Contracts.Storage;
using TecDemo.Mobile.Text;

namespace TecDemo.Mobile.Touch.Storage
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
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var cacheFolder = Path.Combine(documents, "..", "Library", "Cache", "JsonStorage", BasePath, fileName);
			return cacheFolder;
		}
	}
}