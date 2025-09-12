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
	/// Specifies the direction to move when getting items with the 
	/// <see cref="ToolStrip.GetNextItem(ToolStripItem, ArrowDirection)"/>
	/// method.
	///</summary>
	public enum ArrowDirection
	{

		/// <summary>
		/// The direction is up (<see cref="Orientation.Vertical"/>).
		/// </summary>
		Up = 1,

		/// <summary>
		/// The direction is down (<see cref="Orientation.Vertical"/>).
		/// </summary>
		Down = 17,

		/// <summary>
		/// The direction is left (<see cref="Orientation.Horizontal"/>).
		/// </summary>
		Left = 0,

		/// <summary>
		/// The direction is right (<see cref="Orientation.Horizontal"/>).
		/// </summary>
		Right = 16,
	}

}
