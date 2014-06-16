using Android.Content;
using Android.Net;
using TecDemo.Mobile.Contracts.Networking;

namespace TecDemo.Mobile.Android.Networking
{
	public class NetworkInformation : BroadcastReceiver, INetworkInformation
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

		public override void OnReceive(Context context, Intent intent)
		{
			var connectivityManager = (ConnectivityManager) context.GetSystemService(Context.ConnectivityService);
			var info = connectivityManager.ActiveNetworkInfo;

			if (info == null)
			{
				IsConnected = false;
				return;
			}

			IsConnected = info.IsConnected;
		}
	}
}