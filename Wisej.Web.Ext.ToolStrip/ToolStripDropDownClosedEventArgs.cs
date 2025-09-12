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
	/// Provides data for the <see cref="ToolStripDropDown.Closed" /> event. 
	///</summary>
	public class ToolStripDropDownClosedEventArgs
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDownClosedEventArgs" /> class. 
		///</summary>
		/// <param name="reason">One of the <see cref="ToolStripDropDownCloseReason" /> values.</param>
		public ToolStripDropDownClosedEventArgs(ToolStripDropDownCloseReason reason)
		{
			//this._reason = reason;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the reason that the <see cref="ToolStripDropDown" /> closed.
		///</summary>
		/// <returns>One of the <see cref="ToolStripDropDownCloseReason" /> values.</returns>
		public ToolStripDropDownCloseReason CloseReason
		{
			get
			{
				return this._closeReason;
			}
		}

		private ToolStripDropDownCloseReason _closeReason;

		#endregion
	}

}
