using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.ChartJS4.Design
{
	using System;
	using System.ComponentModel;
	using System.ComponentModel.Design;
	using System.Drawing;
	using System.Drawing.Design;
	using System.Windows.Forms;
	using System.Windows.Forms.Design;

	public class ChartColorUIEditor : UITypeEditor
	{

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			var editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			
			// create selection panel
			var panel = new Panel { Height = 90, Width = 200, BorderStyle = BorderStyle.FixedSingle };

			var btnColor = new Button
			{
				Text = "Edit as Color",
				Top = 5,
				Width = 180,
				Left = 10,
				BackColor = Color.White
			};

			var btnCode = new Button
			{
				Text = "Edit as Code",
				Top = 40,
				Width = 180,
				Left = 10,
				BackColor = Color.White
			};

			btnColor.Click += (s, e) =>
			{
				value = EditAsColor(context, provider, value);
			};
			btnCode.Click += (s, e) =>
			{
				value = EditAsCode(context, provider, value);
			};

			panel.Controls.Add(btnColor);
			panel.Controls.Add(btnCode);

			editorService.DropDownControl(panel);

			return value;
		}

		private object EditAsColor(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (value is string str)
				value = Color.White;

			var editorType = Type.GetType("Wisej.Design.ColorEditor, Wisej.Framework.Design, Version=3.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171");
			var colorEditor = (UITypeEditor)Activator.CreateInstance(editorType);
			return colorEditor.EditValue(context, provider, value);
		}

		private object EditAsCode(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if(value is Color)
				value = @"(ctx)=>
				{
					// write a custom function to return the color.
				}";
			
			var editorType = Type.GetType("Wisej.Design.CodeEditor, Wisej.Framework.Design, Version=3.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171");
			var colorEditor = (UITypeEditor)Activator.CreateInstance(editorType);
			return colorEditor.EditValue(context, provider, value);
		}
	}

}
