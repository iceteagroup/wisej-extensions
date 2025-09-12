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
	/// Specifies what to render (image or text) for this <see cref="ToolStripItem" />.
	///</summary>
	public enum ToolStripItemDisplayStyle
	{

		/// <summary>
		/// Specifies that neither image nor text is to be rendered for this <see cref="ToolStripItem" />.
		///</summary>
		None,

		/// <summary>
		/// Specifies that only text is to be rendered for this <see cref="ToolStripItem" />.
		///</summary>
		Text,

		/// <summary>
		/// Specifies that only an image is to be rendered for this <see cref="ToolStripItem" />.
		///</summary>
		Image,

		/// <summary>
		/// Specifies that both an image and text are to be rendered for this <see cref="ToolStripItem" />.
		///</summary>
		ImageAndText,
	}

}
