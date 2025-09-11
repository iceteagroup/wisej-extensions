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
	/// Specifies whether the size of the image on a <see cref="ToolStripItem" /> is automatically adjusted to fit on a <see cref="ToolStrip" /> while retaining the original image proportions.
	///</summary>
	public enum ToolStripItemImageScaling
	{

		/// <summary>
		/// Specifies that the size of the image on a <see cref="ToolStripItem" /> is not automatically adjusted to fit on a <see cref="ToolStrip" />.
		///</summary>
		None,

		/// <summary>
		/// Specifies that the size of the image on a <see cref="ToolStripItem" /> is automatically adjusted to fit on a <see cref="ToolStrip" />.
		///</summary>
		SizeToFit,
	}

}
