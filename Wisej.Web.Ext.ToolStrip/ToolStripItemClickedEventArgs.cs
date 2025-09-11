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
	/// Provides data for the <see cref="ToolStrip.ItemClicked" /> event.
	///</summary>
	public class ToolStripItemClickedEventArgs
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripItemClickedEventArgs" /> class, specifying the <see cref="ToolStripItem" /> that was clicked. 
		///</summary>
		/// <param name="clickedItem">The <see cref="ToolStripItem" /> that was clicked.</param>
		public ToolStripItemClickedEventArgs(ToolStripItem clickedItem)
		{
			this._clickedItem = clickedItem;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the item that was clicked on the <see cref="ToolStrip" />.
		///</summary>
		/// <returns>The <see cref="ToolStripItem" /> that was clicked.</returns>
		public ToolStripItem ClickedItem
		{
			get
			{
				return this._clickedItem;
			}
		}

		private ToolStripItem _clickedItem;

		#endregion
	}

}
