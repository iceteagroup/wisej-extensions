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
	/// Determines the alignment of a <see cref="ToolStripItem" /> in a <see cref="ToolStrip" />.
	///</summary>
	public enum ToolStripItemAlignment
	{

		/// <summary>
		/// Specifies that the <see cref="ToolStripItem" /> is to be anchored toward the left or top end of the <see cref="ToolStrip" />, depending on the <see cref="ToolStrip" /> orientation. If the value of <see cref="RightToLeft" /> is Yes, items marked as <see cref="Wisej.Web.Ext.ToolStripItemAlignment.Left" /> are aligned to the right side of the <see cref="ToolStrip" />.
		///</summary>
		Left,

		/// <summary>
		/// Specifies that the <see cref="ToolStripItem" /> is to be anchored toward the right or bottom end of the <see cref="ToolStrip" />, depending on the <see cref="ToolStrip" /> orientation. If the value of <see cref="RightToLeft" /> is Yes, items marked as <see cref="Wisej.Web.Ext.ToolStripItemAlignment.Right" /> are aligned to the left side of the <see cref="ToolStrip" />.
		///</summary>
		Right,
	}

}
