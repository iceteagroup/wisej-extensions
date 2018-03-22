///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// 
//
// ALL INFORMATION CONTAINED HEREIN IS, AND REMAINS
// THE PROPERTY OF ICE TEA GROUP LLC AND ITS SUPPLIERS, IF ANY.
// THE INTELLECTUAL PROPERTY AND TECHNICAL CONCEPTS CONTAINED
// HEREIN ARE PROPRIETARY TO ICE TEA GROUP LLC AND ITS SUPPLIERS
// AND MAY BE COVERED BY U.S. AND FOREIGN PATENTS, PATENT IN PROCESS, AND
// ARE PROTECTED BY TRADE SECRET OR COPYRIGHT LAW.
//
// DISSEMINATION OF THIS INFORMATION OR REPRODUCTION OF THIS MATERIAL
// IS STRICTLY FORBIDDEN UNLESS PRIOR WRITTEN PERMISSION IS OBTAINED
// FROM ICE TEA GROUP LLC.
//
///////////////////////////////////////////////////////////////////////////////

using System;

namespace Wisej.Web.Ext.Notification
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.Notification.Notification.Click"/> event.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void NotificationClickEventHandler(object sender, NotificationClickEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.Notification.Notification.Click"/> events.
	/// </summary>
	public class NotificationClickEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.Notification.NotificationClickEventArgs" /> class.
		///</summary>
		/// <param name="title">Title of the clicked notification.</param>
		public NotificationClickEventArgs(string title) {

			this.Title = title;
		}

		/// <summary>
		/// Returns the title of the notification that was clicked.
		/// </summary>
		public string Title
		{
			get;
			private set;
		}
	}
}
