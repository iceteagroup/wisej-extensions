using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.ChatControl
{
	public class OptionsMessageControl : MessageControl
	{

		#region Constructor

		public OptionsMessageControl()
		{
			InitializeComponent();
		}

		public OptionsMessageControl(string option1, string option2, string option3)
		{
			InitializeComponent();

			this.buttonOption1.Text = option1;
			this.buttonOption2.Text = option2;
			this.buttonOption3.Text = option3;
		}

		#endregion

		#region Properties

		public bool DisableOnClick { get; set; }

		#endregion

		#region Events

		private void buttonOption_Click(object sender, EventArgs e)
		{
			if (DisableOnClick) 
			{
				this.buttonOption1.Enabled = false;
				this.buttonOption2.Enabled = false;
				this.buttonOption3.Enabled = false;
			}

			this.SetMessageResult(((Control)sender).Text);
		}

		#endregion

		#region Initialization

		private Button buttonOption2;
		private Button buttonOption3;
		private Spacer spacer1;
		private Spacer spacer2;
		private Button buttonOption1;

		private void InitializeComponent()
		{
			this.buttonOption1 = new Wisej.Web.Button();
			this.buttonOption2 = new Wisej.Web.Button();
			this.buttonOption3 = new Wisej.Web.Button();
			this.spacer1 = new Wisej.Web.Spacer();
			this.spacer2 = new Wisej.Web.Spacer();
			this.SuspendLayout();
			// 
			// buttonOption1
			// 
			this.buttonOption1.Dock = Wisej.Web.DockStyle.Top;
			this.buttonOption1.Focusable = false;
			this.buttonOption1.Location = new System.Drawing.Point(8, 8);
			this.buttonOption1.Name = "buttonOption1";
			this.buttonOption1.Size = new System.Drawing.Size(134, 39);
			this.buttonOption1.TabIndex = 0;
			this.buttonOption1.Text = "Option 1";
			this.buttonOption1.Click += new System.EventHandler(this.buttonOption_Click);
			// 
			// buttonOption2
			// 
			this.buttonOption2.Dock = Wisej.Web.DockStyle.Top;
			this.buttonOption2.Focusable = false;
			this.buttonOption2.Location = new System.Drawing.Point(8, 55);
			this.buttonOption2.Name = "buttonOption2";
			this.buttonOption2.Size = new System.Drawing.Size(134, 39);
			this.buttonOption2.TabIndex = 1;
			this.buttonOption2.Text = "Option 2";
			this.buttonOption2.Click += new System.EventHandler(this.buttonOption_Click);
			// 
			// buttonOption3
			// 
			this.buttonOption3.Dock = Wisej.Web.DockStyle.Top;
			this.buttonOption3.Focusable = false;
			this.buttonOption3.Location = new System.Drawing.Point(8, 102);
			this.buttonOption3.Name = "buttonOption3";
			this.buttonOption3.Size = new System.Drawing.Size(134, 39);
			this.buttonOption3.TabIndex = 2;
			this.buttonOption3.Text = "Option 3";
			this.buttonOption3.Click += new System.EventHandler(this.buttonOption_Click);
			// 
			// spacer1
			// 
			this.spacer1.Dock = Wisej.Web.DockStyle.Top;
			this.spacer1.Location = new System.Drawing.Point(8, 47);
			this.spacer1.Name = "spacer1";
			this.spacer1.Size = new System.Drawing.Size(134, 8);
			// 
			// spacer2
			// 
			this.spacer2.Dock = Wisej.Web.DockStyle.Top;
			this.spacer2.Location = new System.Drawing.Point(8, 94);
			this.spacer2.Name = "spacer2";
			this.spacer2.Size = new System.Drawing.Size(134, 8);
			// 
			// OptionsMessageControl
			// 
			this.Controls.Add(this.buttonOption3);
			this.Controls.Add(this.spacer2);
			this.Controls.Add(this.buttonOption2);
			this.Controls.Add(this.spacer1);
			this.Controls.Add(this.buttonOption1);
			this.Name = "OptionsMessageControl";
			this.Padding = new Padding(8);
			this.ResumeLayout(false);
		}

		#endregion
	}
}
