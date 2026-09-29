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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;


namespace Wisej.Web.Ext.ChartJS.Design
{
    /// <summary>
    /// Design time editor for the chart's options.
    /// </summary>
    internal class OptionsEditor : System.Drawing.Design.UITypeEditor
    {
		/// <summary>
		/// Returns the editing style used by this editor.
		/// </summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides information about the property being edited.</param>
		/// <returns>Always <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.Modal"/>: the options are edited in a modal dialog.</returns>
		/// <example>
		/// <code><![CDATA[
		/// [Editor(typeof(OptionsEditor), typeof(UITypeEditor))]
		/// public Options ChartOptions
		/// {
		///     get { return this.chartJS1.Options; }
		/// }
		/// ]]></code>
		/// </example>
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return System.Drawing.Design.UITypeEditorEditStyle.Modal;
		}

		/// <summary>
		/// Edits the property value.
		/// </summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides information about the property being edited.</param>
		/// <param name="provider">An <see cref="T:System.IServiceProvider"/> used to obtain the designer services.</param>
		/// <param name="value">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsBase"/> instance to edit.</param>
		/// <returns>The edited copy of the options if the user clicks OK; otherwise the original <paramref name="value"/>.</returns>
		/// <remarks>
		/// Shows the <see cref="T:Wisej.Web.Ext.ChartJS.Design.OptionsEditorUI"/> dialog with a property grid bound to a clone of the options,
		/// using the dialog font of the IDE. Changes are discarded when the user cancels the dialog.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// [Editor(typeof(Wisej.Web.Ext.ChartJS.Design.OptionsEditor), typeof(System.Drawing.Design.UITypeEditor))]
		/// [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		/// public Options Options
		/// {
		///     get { return this.chartJS1.Options; }
		///     set { this.chartJS1.Options = value; }
		/// }
		/// ]]></code>
		/// </example>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				IUIService uiService = (IUIService)provider.GetService(typeof(IUIService));
				if (service != null)
				{
					// create our editor form.
					using (OptionsEditorUI editorUI = new OptionsEditorUI())
					{

						// sync the font with the IDE.
						if (uiService != null)
							editorUI.Font = ((Font)uiService.Styles["DialogFont"]) ?? editorUI.Font;

						// clone the set of options to cancel
						// the changed values if the user cancels.
						var clone = ((OptionsBase)value).Clone();
						editorUI.Value = clone;

						if (service.ShowDialog(editorUI) == System.Windows.Forms.DialogResult.OK)
						{
							value = editorUI.Value;
						}
					}
				}
			}

			return value;
		}
	}
}
