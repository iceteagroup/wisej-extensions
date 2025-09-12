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

namespace Wisej.Ext.ClientClipboard
{
	/// <summary>
	/// Indicates the type of clipboard action taken by the user.
	/// </summary>
	public enum ClientClipboardChangeType
	{
		/// <summary>
		/// Content has been copied to the clipboard.
		/// </summary>
		Copy,

		/// <summary>
		/// Content has been copied to the clipboard and rmeoved, if the source is 
		/// an editable field.
		/// </summary>
		Cut,

		/// <summary>
		/// Content has been pasted from the clipbard to the target control.
		/// </summary>
		Paste
	}
}
