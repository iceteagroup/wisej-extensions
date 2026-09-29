///////////////////////////////////////////////////////////////////////////////
//
// (C) 2026 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.Mermaid
{
	/// <summary>
	/// Represents the method that will handle the <see cref="Mermaid.Error" /> event 
	/// in a <see cref="Mermaid"/> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="MermaidErrorEventArgs" /> that contains the event data. </param>
	public delegate void MermaidErrorEventHandler(object sender, MermaidErrorEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="Mermaid.Error"/> event.
	/// </summary>
	/// <remarks>
	/// This type is raised when Mermaid reports a client-side validation/parsing error.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// mermaid.Error += (s, e) =>
	/// {
	///     // Inspect error details provided by the client, if any.
	///     System.Diagnostics.Debug.WriteLine(e.Message);
	///     System.Diagnostics.Debug.WriteLine(e.Error);
	/// };
	/// ]]></code>
	/// </example>
	public class MermaidErrorEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MermaidErrorEventArgs"/> class.
		/// </summary>
		/// <param name="message">Human-readable error message.</param>
		/// <param name="error">Optional raw error payload from the browser.</param>
		/// <example>
		/// Raising the event from a derived control:
		/// <code><![CDATA[
		/// var args = new Wisej.Web.Ext.Mermaid.MermaidErrorEventArgs("Invalid diagram.");
		/// ]]></code>
		/// </example>
		public MermaidErrorEventArgs(string message, object error = null)
		{
			Message = message;
			Error = error;
		}

		/// <summary>
		/// Returns the human-readable error message.
		/// </summary>
		/// <remarks>
		/// When raised by the <see cref="Mermaid"/> widget, this is the message of the Mermaid parser error,
		/// or "Invalid Mermaid diagram." when the browser didn't provide one.
		/// </remarks>
		/// <example>
		/// Showing the error to the user:
		/// <code><![CDATA[
		/// mermaid.Error += (s, e) => Wisej.Web.MessageBox.Show(e.Message);
		/// ]]></code>
		/// </example>
		public string Message { get; }

		/// <summary>
		/// Returns the optional error payload sent from the browser (typically a JSON-serializable object), or null.
		/// </summary>
		/// <remarks>
		/// This value is passed through as-is and may be an anonymous JSON-like object.
		/// </remarks>
		/// <example>
		/// Inspecting the raw error payload:
		/// <code><![CDATA[
		/// mermaid.Error += (s, e) =>
		/// {
		///     // In many cases this will be a JSON object with Mermaid-specific fields.
		///     var raw = e.Error;
		/// };
		/// ]]></code>
		/// </example>
		public object Error { get; }
	}
}
