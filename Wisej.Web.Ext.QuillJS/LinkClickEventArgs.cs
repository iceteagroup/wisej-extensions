using System;

namespace Wisej.Web.Ext.QuillJS
{
	/// <summary>
	/// Event arguments for the LinkClick event.
	/// </summary>
	public class LinkClickEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the URL of the clicked link.
		/// </summary>
		public string Url { get; }

		/// <summary>
		/// Initializes a new instance of the LinkClickEventArgs class.
		/// </summary>
		public LinkClickEventArgs(string url)
		{
			Url = url;
		}
	}
}
