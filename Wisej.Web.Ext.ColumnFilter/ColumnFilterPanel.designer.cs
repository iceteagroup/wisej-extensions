namespace Wisej.Web.Ext.ColumnFilter
{
	partial class ColumnFilterPanel
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Wisej Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ok = new Wisej.Web.Button();
			this.cancel = new Wisej.Web.Button();
			this.SuspendLayout();
			// 
			// ok
			// 
			this.ok.Anchor = Wisej.Web.AnchorStyles.Bottom;
			this.ok.AppearanceKey = "button-ok";
			this.ok.Location = new System.Drawing.Point(22, 256);
			this.ok.Name = "ok";
			this.ok.Size = new System.Drawing.Size(92, 32);
			this.ok.TabIndex = 0;
			this.ok.Text = "OK";
			this.ok.Click += new System.EventHandler(this.ok_Click);
			// 
			// cancel
			// 
			this.cancel.Anchor = Wisej.Web.AnchorStyles.Bottom;
			this.cancel.AppearanceKey = "button-cancel";
			this.cancel.Location = new System.Drawing.Point(126, 256);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(92, 32);
			this.cancel.TabIndex = 1;
			this.cancel.Text = "Cancel";
			this.cancel.Click += new System.EventHandler(this.cancel_Click);
			// 
			// ColumnFilterPanel
			// 
			this.Accelerators = new Wisej.Web.Keys[] {
        Wisej.Web.Keys.Return,
        Wisej.Web.Keys.Escape};
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
			this.BorderStyle = Wisej.Web.BorderStyle.Solid;
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.ok);
			this.Name = "ColumnFilterPanel";
			this.Size = new System.Drawing.Size(241, 309);
			this.Accelerator += new Wisej.Web.AcceleratorEventHandler(this.ColumnFilterPanel_Accelerator);
			this.VisibleChanged += new System.EventHandler(this.ColumnFilterPanel_VisibleChanged);
			this.ResumeLayout(false);

		}

		#endregion

		/// <summary>
		/// Reference to the OK button.
		/// </summary>
		protected Web.Button ok;

		/// <summary>
		/// Reference to the Cancel button.
		/// </summary>
		protected Web.Button cancel;
	}
}
