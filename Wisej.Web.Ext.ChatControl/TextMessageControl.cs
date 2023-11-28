using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// A simple message control for displaying text messages.
	/// </summary>
	public class TextMessageControl : MessageControl
	{
		public TextMessageControl()
		{
			InitializeComponent();
		}

		public TextMessageControl(string text)
		{
			InitializeComponent();

			this.labelText.Text = text;
		}

		private Label labelText;

		private void InitializeComponent()
		{
			this.labelText = new Wisej.Web.Label();
			this.SuspendLayout();
			// 
			// labelText
			// 
			this.labelText.AutoSize = true;
			this.labelText.EnableNativeContextMenu = true;
			this.labelText.ForeColor = System.Drawing.Color.White;
			this.labelText.Location = new System.Drawing.Point(8, 8);
			this.labelText.MaximumSize = new System.Drawing.Size(400, 0);
			this.labelText.Name = "labelText";
			this.labelText.Padding = new Wisej.Web.Padding(3, 3, 6, 6);
			this.labelText.Selectable = true;
			this.labelText.Size = new System.Drawing.Size(73, 27);
			this.labelText.TabIndex = 0;
			this.labelText.Text = "Undefined";
			// 
			// TextMessageControl
			// 
			this.AutoSize = true;
			this.AutoSizeMode = Wisej.Web.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.labelText);
			this.Name = "TextMessageControl";
			this.Size = new System.Drawing.Size(84, 38);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
