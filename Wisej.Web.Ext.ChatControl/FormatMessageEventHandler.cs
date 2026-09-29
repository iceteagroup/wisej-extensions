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

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// Represents the method that will handle the <see cref="ChatBox.FormatMessage"/> event, fired when a message
	/// is added to the <see cref="ChatBox"/> and can be formatted before it is displayed.
	/// </summary>
	/// <param name="sender">The source of the event, the <see cref="ChatBox"/>.</param>
	/// <param name="e">An instance of <see cref="MessageEventArgs"/> containing the message event data.</param>
	/// <remarks>
	/// The handler runs before the message control is created, so changes to
	/// <see cref="Message.Content"/>, <see cref="Message.ContentType"/> or <see cref="Message.BubbleVisible"/>
	/// are reflected in the rendered message.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// chatBox.FormatMessage += (object sender, MessageEventArgs e) =>
	/// {
	///     // render messages from other users in bold.
	///     if (!e.IsChatBoxUser)
	///         e.Message.Content = "<b>" + e.Message.Content + "</b>";
	/// };
	/// ]]></code>
	/// </example>
	public delegate void FormatMessageEventHandler(object sender, MessageEventArgs e);
}
