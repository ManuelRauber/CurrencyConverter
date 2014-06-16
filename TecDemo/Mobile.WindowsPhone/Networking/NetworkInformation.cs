using Windows.Networking.Connectivity;
using TecDemo.Mobile.Contracts.Networking;

namespace TecDemo.Mobile.WindowsPhone.Networking
{
	public class NetworkInformation : INetworkInformation
	{
		private bool _isConnected;

		public bool IsConnected
		{
			get { return _isConnected; }
			private set
			{
				if (value != _isConnected)
				{
					_isConnected = value;
					DoConnectionStateChanged(value);
				}
			}
		}

		private void DoConnectionStateChanged(bool value)
		{
			var handler = OnConnectionStateChanged;

			if (null != handler)
			{
				handler(value);
			}
		}

		public event ConnectionStateHandler OnConnectionStateChanged;

		public NetworkInformation()
		{
			Setup();
		}

		private void Setup()
		{
			Windows.Networking.Connectivity.NetworkInformation.NetworkStatusChanged += NetworkStatusChanged;
		}

		private void NetworkStatusChanged(object sender)
		{
			var profile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();

			IsConnected = (null != profile) &&
			               (NetworkConnectivityLevel.InternetAccess == profile.GetNetworkConnectivityLevel());
		}
	}
}