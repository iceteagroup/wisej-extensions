namespace Wisej.Web.Ext.ChatControl
{
	partial class DateSelectorMessageControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Wisej Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.monthCalendar1 = new Wisej.Web.MonthCalendar();
			this.SuspendLayout();
			// 
			// monthCalendar1
			// 
			this.monthCalendar1.AutoSize = true;
			this.monthCalendar1.Dock = Wisej.Web.DockStyle.Fill;
			this.monthCalendar1.Location = new System.Drawing.Point(16, 16);
			this.monthCalendar1.Name = "monthCalendar1";
			this.monthCalendar1.Size = new System.Drawing.Size(268, 343);
			this.monthCalendar1.TabIndex = 0;
			this.monthCalendar1.DateChanged += new Wisej.Web.DateRangeEventHandler(this.monthCalendar1_DateChanged);
			// 
			// DateSelectorMessageControl
			// 
			this.Controls.Add(this.monthCalendar1);
			this.Name = "DateSelectorMessageControl";
			this.Padding = new Wisej.Web.Padding(16);
			this.Size = new System.Drawing.Size(300, 375);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MonthCalendar monthCalendar1;
	}
}
