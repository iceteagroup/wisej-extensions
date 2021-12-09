///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using Wisej.Web;

namespace Wisej.Ext.ClientClipboard
{
	/// <summary>
	/// Represents the handler for the <see cref="ClientClipboard.ClipboardChange"/> event.
	/// </summary>
	/// <param name="sender">The control that had the focus when the user executed a clipboard action, or null.</param>
	/// <param name="e">A <see cref="ClientClipboardEventArgs"/> containing the event's data.</param>
	public delegate void ClientClipboardEventHandler(object sender, ClientClipboardEventArgs e);

	/// <summary>
	/// Represents the data for the <see cref="ClientClipboard.ClipboardChange"/> event.
	/// </summary>
	public class ClientClipboardEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of <see cref="ClientClipboardEventArgs"/>.
		/// </summary>
		/// <param name="target">The control that had the focus when the user executed a clipboard action, or null.</param>
		/// <param name="type">Clipboard action executed by the user.</param>
		/// <param name="content">Text on the clipboard - available only for the "paste" event as specified by the browser's implementation standard.</param>
		public ClientClipboardEventArgs(Control target, ClientClipboardChangeType type, string content)
		{
			this.Type = type;
			this.Target = target;
			this.Content = content;
		}

		/// <summary>
		/// Indicates the clipboard action executed by the user.
		/// </summary>
		public ClientClipboardChangeType Type
		{
			get;
			private set;
		}

		/// <summary>
		/// Indicates the control that had the focus when the user executed the clipboard action.
		/// </summary>
		public Control Target
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the text content from the <see cref="ClientClipboardChangeType.Paste"/> event type.
		/// </summary>
		public string Content
		{
			get;
			private set;
		}
	}
}
