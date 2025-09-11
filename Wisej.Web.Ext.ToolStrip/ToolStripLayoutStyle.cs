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
	/// Specifies the possible alignments with which the items of a <see cref="ToolStrip" /> can be displayed.
	///</summary>
	public enum ToolStripLayoutStyle
	{

		/// <summary>
		/// Specifies that items are laid out automatically.
		///</summary>
		StackWithOverflow,

		/// <summary>
		/// Specifies that items are laid out horizontally and overflow as necessary.
		///</summary>
		HorizontalStackWithOverflow,

		/// <summary>
		/// Specifies that items are laid out vertically, are centered within the control, and overflow as necessary.
		///</summary>
		VerticalStackWithOverflow,

		/// <summary>
		/// Specifies that items flow horizontally or vertically as necessary.
		///</summary>
		Flow,

		/// <summary>
		/// Specifies that items are laid out flush left.
		///</summary>
		Table,
	}
}
