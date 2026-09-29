using System;

namespace Wisej.Web.Ext.QuillJS
{
	/// <summary>
	/// Provides data for the <see cref="QuillJSEditor.LinkClick"/> event.
	/// </summary>
	public class LinkClickEventArgs : EventArgs
	{
		/// <summary>
		/// Returns the URL of the clicked link.
		/// </summary>
		/// <remarks>
		/// The value is the resolved <c>href</c> of the link element, i.e. an absolute URL.
		/// </remarks>
		public string Url { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LinkClickEventArgs"/> class.
		/// </summary>
		/// <param name="url">The URL of the clicked link.</param>
		public LinkClickEventArgs(string url)
		{
			Url = url;
		}
	}
}
