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
	/// Determines whether a <see cref="ToolStripItem" /> is placed in the overflow <see cref="ToolStrip" />.
	///</summary>
	public enum ToolStripItemOverflow
	{

		/// <summary>
		/// Specifies that a <see cref="ToolStripItem" /> is never a candidate for the overflow <see cref="ToolStrip" />. If the <see cref="ToolStripItem" /> cannot fit on the main <see cref="ToolStrip" />, it will not be shown.
		///</summary>
		Never,

		/// <summary>
		/// Specifies that a <see cref="ToolStripItem" /> is permanently shown in the overflow <see cref="ToolStrip" />.
		///</summary>
		Always,

		/// <summary>
		/// Specifies that a <see cref="ToolStripItem" /> drifts between the main <see cref="ToolStrip" /> and the overflow <see cref="ToolStrip" /> as required if space is not available on the main <see cref="ToolStrip" />.
		///</summary>
		AsNeeded,
	}

}
