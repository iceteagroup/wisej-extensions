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
	/// <remarks>
	/// A <see cref="LazyMessage"/> is added to the <see cref="ChatBox"/> with empty content and displays an animated
	/// loading indicator until <see cref="SetResult(string)"/> is called with the final content. Use it for
	/// responses that take time to produce, such as replies from a remote service or an AI model.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var bot = new User("bot", "Assistant");
	/// var reply = new LazyMessage(bot);
	/// chatBox.DataSource.Add(reply);
	///
	/// // later, when the response is available.
	/// reply.SetResult("Here is your answer.");
	/// ]]></code>
	/// </example>
	public class LazyMessage : Message
	{
		/// <summary>
		/// Creates a new instance of <see cref="LazyMessage"/> with the given user.
		/// </summary>
		/// <param name="user">The user associated with the message. When <c>null</c>, the <see cref="ChatBox"/>
		/// assigns its own <see cref="ChatBox.User"/> when the message is added.</param>
		/// <example>
		/// <code><![CDATA[
		/// var pending = new LazyMessage(new User("bot", "Assistant"));
		/// chatBox.DataSource.Add(pending);
		/// ]]></code>
		/// </example>
		public LazyMessage(User user = null) : this(user, null)
		{
		}

		/// <summary>
		/// Creates a new instance of <see cref="LazyMessage"/> with the given user and content type.
		/// </summary>
		/// <param name="user">The user associated with the message. When <c>null</c>, the <see cref="ChatBox"/>
		/// assigns its own <see cref="ChatBox.User"/> when the message is added.</param>
		/// <param name="contentType">The content type of the message, stored in <see cref="Message.ContentType"/>; can be <c>null</c>.</param>
		/// <remarks>
		/// The message starts with an empty <see cref="Message.Content"/>. When its control is assigned, the
		/// control shows a loading image (<c>Images/loading.svg</c>) with a minimum size of 60x16 pixels.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var pending = new LazyMessage(new User("bot", "Assistant"), "text/markdown");
		/// chatBox.DataSource.Add(pending);
		/// ]]></code>
		/// </example>
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
		/// Sets the content of the message and removes the loading indicator.
		/// </summary>
		/// <param name="content">The message content.</param>
		/// <remarks>
		/// Updates <see cref="Message.Content"/> and the text of <see cref="Message.Control"/>, clears the loading
		/// image and resets the control's minimum size. Call it only after the message has been added to a
		/// <see cref="ChatBox"/>, since <see cref="Message.Control"/> is <c>null</c> until then.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var reply = new LazyMessage(new User("bot", "Assistant"));
		/// chatBox.DataSource.Add(reply);
		/// reply.SetResult("The weather today is sunny.");
		/// ]]></code>
		/// </example>
		public void SetResult(string content)
		{
			this.Content = content;
			this.Control.Text = content;
			this.Control.BackgroundImageSource = "";
			this.Control.MinimumSize = new Size(0, 0);
		}
	}
}
