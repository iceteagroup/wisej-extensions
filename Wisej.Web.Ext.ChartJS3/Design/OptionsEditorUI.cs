///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.ChartJS3.Design
{
	/// <summary>
	/// Dialog used at design time to edit the chart options in a property grid.
	/// </summary>
	internal partial class OptionsEditorUI : System.Windows.Forms.Form
	{
		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.Design.OptionsEditorUI"/> dialog.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// using (var editorUI = new OptionsEditorUI())
		/// {
		///     editorUI.Value = options;
		///     editorService.ShowDialog(editorUI);
		/// }
		/// ]]></code>
		/// </example>
		public OptionsEditorUI()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Returns or sets the options edited in the dialog.
		/// </summary>
		/// <value>
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> instance displayed in the property grid.
		/// </value>
		/// <remarks>
		/// Setting the value selects it in the property grid and expands all the grid items.
		/// The object is edited in place; <see cref="T:Wisej.Web.Ext.ChartJS3.Design.OptionsEditor"/> assigns a clone so the changes can be discarded.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var editorUI = new OptionsEditorUI();
		/// editorUI.Value = this.chartJS31.Options.Plugins;
		/// if (editorService.ShowDialog(editorUI) == DialogResult.OK)
		///     this.chartJS31.Options.Plugins = (OptionsPlugins)editorUI.Value;
		/// ]]></code>
		/// </example>
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
