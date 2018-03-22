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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using WinForms = System.Windows.Forms;

namespace Wisej.Web.Ext.ChartJS.Design
{
	/// <summary>
	/// Design time editor for the chart's options.
	/// </summary>
	internal class OptionsEditor : UITypeEditor
	{
		/// <summary>
		/// Return the drop down style for this editor.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		/// <summary>
		/// Edit the property value.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="provider"></param>
		/// <param name="value"></param>
		/// <returns></returns>
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

						if (service.ShowDialog(editorUI) == WinForms.DialogResult.OK)
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
