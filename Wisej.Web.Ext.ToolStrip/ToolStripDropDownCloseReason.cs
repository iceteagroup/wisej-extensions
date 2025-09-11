///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Specifies the reason that a <see cref="ToolStripDropDown" /> control was closed.
	///</summary>
	public enum ToolStripDropDownCloseReason
	{

		/// <summary>
		/// Specifies that the <see cref="ToolStripDropDown" /> control was closed because another application has received the focus.
		///</summary>
		AppFocusChange,

		/// <summary>
		/// Specifies that the <see cref="ToolStripDropDown" /> control was closed because an application was launched.
		///</summary>
		AppClicked,

		/// <summary>
		/// Specifies that the <see cref="ToolStripDropDown" /> control was closed because one of its items was clicked.
		///</summary>
		ItemClicked,

		/// <summary>
		/// Specifies that the <see cref="ToolStripDropDown" /> control was closed because of keyboard activity, such as the ESC key being pressed.
		///</summary>
		Keyboard,

		/// <summary>
		/// Specifies that the <see cref="ToolStripDropDown" /> control was closed because the <see cref="ToolStripDropDown.Close" /> method was called.
		///</summary>
		CloseCalled,
	}

}
