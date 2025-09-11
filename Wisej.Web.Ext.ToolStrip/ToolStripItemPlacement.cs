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
	/// Specifies where a <see cref="ToolStripItem" /> is to be layed out.
	///</summary>
	public enum ToolStripItemPlacement
	{

		/// <summary>
		/// Specifies that a <see cref="ToolStripItem" /> is to be layed out on the main <see cref="ToolStrip" />.
		///</summary>
		Main,

		/// <summary>
		/// Specifies that a <see cref="ToolStripItem" /> is to be layed out on the overflow <see cref="ToolStrip" />.
		///</summary>
		Overflow,

		/// <summary>
		/// Specifies that a <see cref="ToolStripItem" /> is not to be layed out on the screen.
		///</summary>
		None,
	}

}
