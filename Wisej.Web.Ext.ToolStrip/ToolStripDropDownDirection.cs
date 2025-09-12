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
	/// Specifies the direction in which a <see cref="ToolStripDropDown" /> control is displayed relative to its parent control.
	///</summary>
	public enum ToolStripDropDownDirection
	{

		/// <summary>
		/// Uses the mouse position to specify that the <see cref="ToolStripDropDown" /> is displayed above and to the left of its parent control.
		///</summary>
		AboveLeft = 0,

		/// <summary>
		/// Uses the mouse position to specify that the <see cref="ToolStripDropDown" /> is displayed above and to the right of its parent control.
		///</summary>
		AboveRight = 1,

		/// <summary>
		/// Uses the mouse position to specify that the <see cref="ToolStripDropDown" /> is displayed below and to the left of its parent control.
		///</summary>
		BelowLeft = 2,

		/// <summary>
		/// Uses the mouse position to specify that the <see cref="ToolStripDropDown" /> is displayed below and to the right of its parent control.
		///</summary>
		BelowRight = 3,

		/// <summary>
		/// Compensates for nested drop-down controls and specifies that the <see cref="ToolStripDropDown" /> is displayed to the left of its parent control.
		///</summary>
		Left = 4,

		/// <summary>
		/// Compensates for nested drop-down controls and specifies that the <see cref="ToolStripDropDown" /> is displayed to the right of its parent control.
		///</summary>
		Right = 5,

		/// <summary>
		/// Compensates for nested drop-down controls and responds to the <see cref="RightToLeft" /> setting, specifying either <see cref="ToolStripDropDownDirection.Left" /> or <see cref="ToolStripDropDownDirection.Right" /> accordingly.
		///</summary>
		Default = 7,
	}

}
