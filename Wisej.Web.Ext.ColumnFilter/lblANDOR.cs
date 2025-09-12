///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System;

namespace Wisej.Web.Ext.ColumnFilter
{
    /// <summary>
    /// Custom AND/OR label.
    /// </summary>
    internal class lblANDOR : Label
	{
		#region Constructors
		public lblANDOR ()
		{
			this.AllowHtml = true;
			this.Text = Messages.AND;			
			this.Click += LblANDOR_Click;
		}
		#endregion

		#region Methods
		private void LblANDOR_Click(object sender, EventArgs e)
		{
			if (this.Text == Messages.AND)
				this.Text = Messages.OR;
			else
				this.Text = Messages.AND;
		}
		public string GetOperator()
		{
			if (this.Text == Messages.AND)
				return "AND";
			else
				return "OR";
		}
		#endregion

	}
}