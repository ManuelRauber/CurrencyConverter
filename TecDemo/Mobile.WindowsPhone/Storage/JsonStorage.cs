using System.IO;
using System.IO.IsolatedStorage;
using TecDemo.Mobile.Contracts.Storage;
using TecDemo.Mobile.Text;

namespace TecDemo.Mobile.WindowsPhone.Storage
{
	public class JsonStorage : IStorage
	{
		private readonly IsolatedStorageFile _storage;
		private const string FolderLocation = "JsonStorage";

		public JsonStorage()
		{
			_storage = IsolatedStorageFile.GetUserStoreForApplication();
		}

		public void SaveTo<T>(string fileName, T objectToSave)
		{
			var path = GetStoragePath(fileName);
			CreateNecessaryFolders(path);
			using (var isoStream = new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.ReadWrite, _storage))
			{
				JsonConverter.Serialize(isoStream, objectToSave);
			}
		}

		public T LoadFrom<T>(string fileName)
		{
			try
			{
				using (var isoStream = new IsolatedStorageFileStream(GetStoragePath(fileName), FileMode.Open, FileAccess.Read, _storage))
				{
					var obj = JsonConverter.Deserialize<T>(isoStream);
					return obj;
				}
			}
			catch
			{
				return default(T);
			}
		}

		private string GetStoragePath(string fileName)
		{
			var path = Path.Combine(FolderLocation, BasePath, fileName);
			return path;
		}

		public string BasePath { get; set; }
		public void Remove(string fileName)
		{
			_storage.DeleteFile(GetStoragePath(fileName));
		}

		public bool Exists(string fileName)
		{
			return _storage.FileExists(GetStoragePath(fileName));
		}

		private void CreateNecessaryFolders(string path)
		{
			var directory = Path.GetDirectoryName(path);
			if (directory == null)
			{
				return;
			}

			_storage.CreateDirectory(directory);

			//var directories = directory.Split(Path.DirectorySeparatorChar);
			//if (directories.Length == 0)
			//{
			//	return;
			//}

			//foreach (var dir in directories)
			//{
			//	_storage.
			//}
		}
	}
}