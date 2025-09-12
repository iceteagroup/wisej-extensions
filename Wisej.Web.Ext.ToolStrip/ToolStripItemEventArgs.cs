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
	/// Provides data for <see cref="ToolStripItem" /> events.
	///</summary>
	public class ToolStripItemEventArgs
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripItemEventArgs" /> class, specifying a <see cref="ToolStripItem" />. 
		///</summary>
		/// <param name="item">The <see cref="ToolStripItem" /> for which to specify events.</param>
		public ToolStripItemEventArgs(ToolStripItem item)
		{
			this._item = item;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets a <see cref="ToolStripItem" /> for which to handle events.
		///</summary>
		/// <returns>A <see cref="ToolStripItem" />.</returns>
		public ToolStripItem Item
		{
			get
			{
				return this._item;
			}
		}

		private ToolStripItem _item;

		#endregion
	}

}
