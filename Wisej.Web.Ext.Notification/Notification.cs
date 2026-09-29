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
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.Notification
{
	/// <summary>
	/// Adds support for the Notification API: <see href="https://developer.mozilla.org/en-US/docs/Web/API/notification"/>.
	/// </summary>
	/// <remarks>
	/// The component displays system (desktop) notifications in the browser of the current session.
	/// The browser asks the user for permission the first time a notification is shown; when the permission is
	/// denied or the browser doesn't support notifications, nothing is displayed and no error is returned.
	/// Browsers allow notifications only on secure (HTTPS or localhost) pages.
	/// The <see cref="Click"/> event is wired to the client only if a handler is attached when the component is rendered.
	/// </remarks>
	/// <example>
	/// Notifying the user and reacting to the click on the notification:
	/// <code><![CDATA[
	/// public MainPage()
	/// {
	///     InitializeComponent();
	///     this.notification1.Click += this.notification1_Click;
	/// }
	/// 
	/// private void OrderReceived(string orderId)
	/// {
	///     this.notification1.Show("New order", "Order " + orderId + " has been received.", id: orderId, showOnClick: true);
	/// }
	/// 
	/// private void notification1_Click(object sender, NotificationClickEventArgs e)
	/// {
	///     OpenOrder(e.Id);
	/// }
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Notification))]
	[ApiCategory("Notification")]
	[SRDescription("Adds support for the Notification API: <see href=\"https://developer.mozilla.org/en-US/docs/Web/API/notification.\"/>")]
	public class Notification : Wisej.Web.Component
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.Notification.Notification" /> class.
		/// </summary>
		public Notification()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.Notification.Notification" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the component. </param>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
		public Notification(IContainer container)
			: this()
		{
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when the user clicks on a desktop notification.
		/// </summary>
		public event NotificationClickEventHandler Click
		{
			add { base.AddHandler(nameof(Click), value); }
			remove { base.RemoveHandler(nameof(Click), value); }
		}

		/// <summary>
		/// Fires the <see cref="Click"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.Ext.Notification.NotificationClickEventArgs" /> that contains the event data. </param>
		protected virtual void OnClick(NotificationClickEventArgs e)
		{
			((NotificationClickEventHandler)base.Events[nameof(Click)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns whether the client browser supports desktop notifications.
		/// </summary>
		/// <returns>true if the browser supports the Notification API.</returns>
		/// <remarks>
		/// This static property checks the browser of the current session (<see cref="Application.Browser"/>).
		/// It doesn't indicate whether the user has granted the permission to display notifications.
		/// </remarks>
		/// <example>
		/// Falling back to an in-page alert when desktop notifications are not supported:
		/// <code><![CDATA[
		/// if (Notification.IsSupported)
		///     this.notification1.Show("Export completed", "The report is ready to download.");
		/// else
		///     AlertBox.Show("The report is ready to download.");
		/// ]]></code>
		/// </example>
		public static bool IsSupported
		{
			get
			{
				return Application.Browser.Features.notification ?? false;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Displays a new desktop notification.
		/// </summary>
		/// <param name="title">The title of the notification.</param>
		/// <param name="body">The optional body string of the notification.</param>
		/// <param name="icon">The URL of the image used as an icon of the notification.</param>
		/// <param name="showOnClick">Indicates whether to activate the browser when the user clicks the notification.</param>
		/// <param name="image">URL of an image to show at the top of the notification window.</param>
		/// <param name="id">Optional unique id, returned in <see cref="NotificationClickEventArgs"/>.</param>
		/// <param name="requireInteraction">Indicates that a notification should remain active until the user clicks or dismisses it, rather than closing automatically.</param>
		/// <remarks>
		/// The notification is displayed asynchronously by the browser. If the user has not yet granted or denied the permission,
		/// the browser asks first and the notification is displayed only if the permission is granted.
		/// The language of the notification is set to the current culture (<see cref="CultureInfo.CurrentCulture"/>).
		/// Support for <paramref name="image"/> and <paramref name="requireInteraction"/> depends on the browser and the operating system.
		/// </remarks>
		/// <example>
		/// Displaying a notification that stays visible until the user dismisses it:
		/// <code><![CDATA[
		/// this.notification1.Show(
		///     "Meeting reminder",
		///     "Project review starts in 5 minutes.",
		///     icon: "Images/calendar.png",
		///     showOnClick: true,
		///     id: "meeting-42",
		///     requireInteraction: true);
		/// ]]></code>
		/// </example>
		public void Show(
			string title,
			string body = null,
			string icon = null,
			bool showOnClick = false,
			string image = null,
			string id = null,
			bool requireInteraction = false)
		{
			Call("show", new
			{
				id = id,
				title = title,
				body = body,
				icon = icon,
				image = image,
				showOnClick = showOnClick,
				requireInteraction = requireInteraction,
				Language = CultureInfo.CurrentCulture.Name
			});
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{

				case "click":
					var data = e.Parameters.Data;
					OnClick(new NotificationClickEventArgs(data.title, data.id));
					break;

				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ext.Notification";

			// subscribe only if the event has been attached to since
			// it's unlikely that this class will be extended to
			// override OnClick.
			if (base.Events[nameof(Click)] != null)
			{
				config.wiredEvents = new WiredEvents();
				config.wiredEvents.Add("click(Data)");
			}
		}

		#endregion

	}
}
