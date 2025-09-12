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
	/// Specifies the display and layout information for text strings.
	///</summary>
	public enum TextFormatFlags
	{

		/// <summary>
		/// Aligns the text on the bottom of the bounding rectangle. Applied only when the text is a single line.
		///</summary>
		Bottom = 8,

		/// <summary>
		/// Removes the end of trimmed lines, and replaces them with an ellipsis.
		///</summary>
		EndEllipsis = 32768,

		/// <summary>
		/// Expands tab characters. The default number of characters per tab is eight. The <see cref="TextFormatFlags.WordEllipsis" />, <see cref="TextFormatFlags.PathEllipsis" />, and <see cref="TextFormatFlags.EndEllipsis" /> values cannot be used with <see cref="TextFormatFlags.ExpandTabs" />.
		///</summary>
		ExpandTabs = 64,

		/// <summary>
		/// Includes the font external leading in line height. Typically, external leading is not included in the height of a line of text.
		///</summary>
		ExternalLeading = 512,

		/// <summary>
		/// Applies the default formatting, which is left-aligned.
		///</summary>
		Default = 0,

		/// <summary>
		/// Applies to Windows 2000 and Windows XP only: 
		///</summary>
		HidePrefix = 1048576,

		/// <summary>
		/// Centers the text horizontally within the bounding rectangle.
		///</summary>
		HorizontalCenter = 1,

		/// <summary>
		/// Uses the system font to calculate text metrics.
		///</summary>
		Internal = 4096,

		/// <summary>
		/// Aligns the text on the left side of the clipping area.
		///</summary>
		Left = 0,

		/// <summary>
		/// Has no effect on the drawn text.
		///</summary>
		ModifyString = 65536,

		/// <summary>
		/// Allows the overhanging parts of glyphs and unwrapped text reaching outside the formatting rectangle to show.
		///</summary>
		NoClipping = 256,

		/// <summary>
		/// Turns off processing of prefix characters. Typically, the ampersand (&amp;) mnemonic-prefix character is interpreted as a directive to underscore the character that follows, and the double-ampersand (&amp;&amp;) mnemonic-prefix characters as a directive to print a single ampersand. By specifying <see cref="TextFormatFlags.NoPrefix" />, this processing is turned off. For example, an input string of "A&amp;bc&amp;&amp;d" with <see cref="TextFormatFlags.NoPrefix" /> applied would result in output of "A&amp;bc&amp;&amp;d".
		///</summary>
		NoPrefix = 2048,

		/// <summary>
		/// Applies to Windows 98, Windows Me, Windows 2000, or Windows XP only:
		///</summary>
		NoFullWidthCharacterBreak = 524288,

		/// <summary>
		/// Removes the center of trimmed lines and replaces it with an ellipsis. 
		///</summary>
		PathEllipsis = 16384,

		/// <summary>
		/// Applies to Windows 2000 or Windows XP only: 
		///</summary>
		PrefixOnly = 2097152,

		/// <summary>
		/// Aligns the text on the right side of the clipping area.
		///</summary>
		Right = 2,

		/// <summary>
		/// Displays the text from right to left.
		///</summary>
		RightToLeft = 131072,

		/// <summary>
		/// Displays the text in a single line.
		///</summary>
		SingleLine = 32,

		/// <summary>
		/// Specifies the text should be formatted for display on a <see cref="TextBox" /> control.
		///</summary>
		TextBoxControl = 8192,

		/// <summary>
		/// Aligns the text on the top of the bounding rectangle.
		///</summary>
		Top = 0,

		/// <summary>
		/// Centers the text vertically, within the bounding rectangle.
		///</summary>
		VerticalCenter = 4,

		/// <summary>
		/// Breaks the text at the end of a word.
		///</summary>
		WordBreak = 16,

		/// <summary>
		/// Trims the line to the nearest word and an ellipsis is placed at the end of a trimmed line.
		///</summary>
		WordEllipsis = 262144,

		/// <summary>
		/// Preserves the clipping specified by a <see cref="System.Drawing.Graphics" /> object. Applies only to methods receiving an <see cref="System.Drawing.IDeviceContext" /> that is a <see cref="System.Drawing.Graphics" />.
		///</summary>
		PreserveGraphicsClipping = 16777216,

		/// <summary>
		/// Preserves the transformation specified by a <see cref="System.Drawing.Graphics" />. Applies only to methods receiving an <see cref="System.Drawing.IDeviceContext" /> that is a <see cref="System.Drawing.Graphics" />.
		///</summary>
		PreserveGraphicsTranslateTransform = 33554432,

		/// <summary>
		/// Adds padding to the bounding rectangle to accommodate overhanging glyphs. 
		///</summary>
		GlyphOverhangPadding = 0,

		/// <summary>
		/// Does not add padding to the bounding rectangle.
		///</summary>
		NoPadding = 268435456,

		/// <summary>
		/// Adds padding to both sides of the bounding rectangle.
		///</summary>
		LeftAndRightPadding = 536870912,
	}

}