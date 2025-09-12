///////////////////////////////////////////////////////////////////////////////
//
// (C) 2024 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Drawing;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// A <see cref="Message"/> with a deferred result.
	/// </summary>
	public class LazyMessage : Message
	{
		/// <summary>
		/// Creates a new instance of <see cref="LazyMessage"/> with the given user.
		/// </summary>
		/// <param name="user">The user.</param>
		public LazyMessage(User user = null) : this(user, null)
		{
		}

		/// <summary>
		/// Creates a new instance of <see cref="LazyMessage"/> with the given user and content type.
		/// </summary>
		/// <param name="user">The message's user.</param>
		/// <param name="contentType">The content type of the message.</param>
		public LazyMessage(User user, string contentType) : base("", contentType, user)
		{
			MessageControlAssigned += LazyMessage_MessageControlAssigned;
		}

		private void LazyMessage_MessageControlAssigned(object sender, EventArgs e)
		{
			this.Control.MinimumSize = new Size(60, 16);
			this.Control.BackgroundImageLayout = ImageLayout.Zoom;
			this.Control.BackgroundImageSource = "resource.wx/Wisej.Web.Ext.ChatControl/Images/loading.svg";
		}

		/// <summary>
		/// Sets the content of the message.
		/// </summary>
		/// <param name="content">The message content.</param>
		public void SetResult(string content)
		{
			this.Content = content;
			this.Control.Text = content;
			this.Control.BackgroundImageSource = "";
			this.Control.MinimumSize = new Size(0, 0);
		}
	}
}
