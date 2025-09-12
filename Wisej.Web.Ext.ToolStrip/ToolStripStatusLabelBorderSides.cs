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
	/// Specifies which sides of a <see cref="ToolStripStatusLabel" /> have borders.
	///</summary>
	public enum ToolStripStatusLabelBorderSides
	{

		/// <summary>
		/// All sides of the <see cref="ToolStripStatusLabel" /> have borders.
		///</summary>
		All = 15,

		/// <summary>
		/// Only the bottom side of the <see cref="ToolStripStatusLabel" /> has borders.
		///</summary>
		Bottom = 8,

		/// <summary>
		/// Only the left side of the <see cref="ToolStripStatusLabel" /> has borders.
		///</summary>
		Left = 1,

		/// <summary>
		/// Only the right side of the <see cref="ToolStripStatusLabel" /> has borders.
		///</summary>
		Right = 4,

		/// <summary>
		/// Only the top side of the <see cref="ToolStripStatusLabel" /> has borders.
		///</summary>
		Top = 2,

		/// <summary>
		/// The <see cref="ToolStripStatusLabel" /> has no borders.
		///</summary>
		None = 0,
	}

}