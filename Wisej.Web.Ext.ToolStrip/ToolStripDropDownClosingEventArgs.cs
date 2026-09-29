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
	/// Provides data for the <see cref="ToolStripDropDown.Closing" /> event.
	/// </summary>
	public class ToolStripDropDownClosingEventArgs
	{

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDownClosingEventArgs" /> class with the specified reason.
		/// </summary>
		/// <param name="reason">One of the <see cref="ToolStripDropDownCloseReason" /> values.</param>
		public ToolStripDropDownClosingEventArgs(ToolStripDropDownCloseReason reason)
		{
			this._closeReason = reason;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the reason that the <see cref="ToolStripDropDown" /> is closing.
		/// </summary>
		/// <returns>One of the <see cref="ToolStripDropDownCloseReason" /> values.</returns>
		/// <example>
		/// Checking why the drop down is closing:
		/// <code><![CDATA[
		/// private void dropDown1_Closing(object sender, ToolStripDropDownClosingEventArgs e)
		/// {
		///     if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
		///         SaveSelection();
		/// }
		/// ]]></code>
		/// </example>
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