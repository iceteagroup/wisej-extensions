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

namespace Wisej.Web.Ext.Mermaid
{
	/// <summary>
	/// Represents the result of the validation of a Mermaid diagram.
	/// </summary>
	/// <remarks>
	/// This type is returned by <see cref="Mermaid.ValidateAsync(string)"/>.
	/// </remarks>
	/// <example>
	/// Checking the current diagram:
	/// <code><![CDATA[
	/// ValidationResult result = await mermaid.ValidateAsync(mermaid.Diagram);
	/// if (!result.Valid)
	///     Wisej.Web.MessageBox.Show(result.Message ?? "Validation failed.");
	/// ]]></code>
	/// </example>
	public class ValidationResult
	{
		// Internal constructor. Parses the error payload.
		internal ValidationResult(dynamic error)
		{
			this.Message = error.message;
			this.Valid = error.valid ?? false;
		}

		/// <summary>
		/// Returns true if the diagram is valid, or false if the diagram is invalid and <see cref="Message"/> contains details about the failure.
		/// </summary>
		public bool Valid { get; }

		/// <summary>
		/// Returns the error message associated with the failure, or null if the validation succeeded.
		/// </summary>
		public string Message { get; }
	}
}
