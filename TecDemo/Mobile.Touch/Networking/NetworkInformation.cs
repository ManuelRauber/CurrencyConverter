using System.Net;
using MonoTouch.CoreFoundation;
using MonoTouch.SystemConfiguration;
using TecDemo.Mobile.Contracts.Networking;

namespace TecDemo.Mobile.Touch.Networking
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
			var reachability = new NetworkReachability(new IPAddress(0));
			reachability.SetCallback(ReachabilityChanged);
			reachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
		}

		private void ReachabilityChanged(NetworkReachabilityFlags flags)
		{
			IsConnected = (flags & NetworkReachabilityFlags.Reachable) != 0;
		}
	}
}