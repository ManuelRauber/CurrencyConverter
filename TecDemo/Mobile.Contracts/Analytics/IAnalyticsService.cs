namespace TecDemo.Mobile.Contracts.Analytics
{
	public interface IAnalyticsService
	{
		/// <summary>
		/// Tracks a view (e.g. opening a page)
		/// </summary>
		/// <param name="viewName"></param>
		void TrackView(string viewName);

		/// <summary>
		/// Tracks a custom event
		/// </summary>
		/// <param name="category"></param>
		/// <param name="action"></param>
		/// <param name="label"></param>
		void TrackCustomEvent(string category, string action, string label);
	}
}