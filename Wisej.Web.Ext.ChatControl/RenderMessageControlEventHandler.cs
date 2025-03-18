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

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// Event handler for providing message controls.
	/// </summary>
	/// <param name="e"></param>
	public delegate void RenderMessageControlEventHandler(RenderMessageControlEventArgs e);

	/// <summary>
	/// Event args for providing message controls.
	/// </summary>
	public class RenderMessageControlEventArgs : EventArgs
	{
		/// <summary>
		/// Creates a new instance of <see cref="RenderMessageControlEventArgs"/> with the given Message.
		/// </summary>
		/// <param name="message"></param>
		public RenderMessageControlEventArgs(Message message) => Message = message;

		/// <summary>
		/// Gets the Message that is requesting a control.
		/// </summary>
		public Message Message { get; }

		/// <summary>
		/// Gets or sets the <see cref="Web.Control"/> to use with the message.
		/// </summary>
		public Control Control { get; set; }
	}
}