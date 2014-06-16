using System;

namespace TecDemo.Mobile.Contracts.Platform
{
	public interface IApplicationLifecycle
	{
		/// <summary>
		/// Is fired when the app resumes
		/// </summary>
		event Action OnAppResuming;

		/// <summary>
		/// Is fired when the app suspends
		/// </summary>
		event Action OnAppSuspending;

		/// <summary>
		/// Is fired when the app starts
		/// </summary>
		event Action OnAppLaunching;

		/// <summary>
		/// Is fired when the app closes
		/// </summary>
		event Action OnAppClosing;
	}
}