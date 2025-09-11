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
	internal class ISupportToolStripPanel
	{

		#region Properties

		public virtual ToolStripPanelRow ToolStripPanelRow
		{
			get
			{
				return this._toolStripPanelRow;
			}
			set
			{
				if ((this._toolStripPanelRow != value))
				{
					this._toolStripPanelRow = value;
				}
			}
		}
		private ToolStripPanelRow _toolStripPanelRow;

		public virtual ToolStripPanelCell ToolStripPanelCell
		{
			get
			{
				return this._toolStripPanelCell;
			}
		}
		private ToolStripPanelCell _toolStripPanelCell;

		public virtual bool Stretch
		{
			get
			{
				return this._stretch;
			}
			set
			{
				if ((this._stretch != value))
				{
					this._stretch = value;
				}
			}
		}
		private bool _stretch;

		public virtual bool IsCurrentlyDragging
		{
			get
			{
				return this._isCurrentlyDragging;
			}
		}
		private bool _isCurrentlyDragging;

		#endregion

		#region Methods

		public virtual void BeginDrag()
		{
			// TODO: Implement
		}

		public virtual void EndDrag()
		{
			// TODO: Implement
		}

		#endregion
	}

}