///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.ChartJS.Design
{
	/// <summary>
	/// Editor for javascript, JSON or css string values.
	/// </summary>
	internal partial class OptionsEditorUI : System.Windows.Forms.Form
	{
		public OptionsEditorUI()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Returns or sets the code in the editor.
		/// </summary>
		public OptionsBase Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				this.propertyGrid.SelectedObject = this._value;
				this.propertyGrid.ExpandAllGridItems();
			}
		}
		private OptionsBase _value;
	}
}
