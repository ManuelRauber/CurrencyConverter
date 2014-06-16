using System.Threading.Tasks;

namespace TecDemo.Mobile.Contracts.Configuration
{
	public interface IMobileConfigurationProvider
	{
		Task<IMobileConfiguration> LoadAsync();
	}
}