namespace TecDemo.Mobile.Contracts.Networking
{
	public delegate void ConnectionStateHandler(bool currentState);
	public interface INetworkInformation
	{
		bool IsConnected { get; }
		event ConnectionStateHandler OnConnectionStateChanged;
	}
}