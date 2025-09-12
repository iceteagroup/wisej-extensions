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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnFilterPanel));
			this.ok = new Wisej.Web.Button();
			this.cancel = new Wisej.Web.Button();
			this.SuspendLayout();
			// 
			// ok
			// 
			resources.ApplyResources(this.ok, "ok");
			this.ok.AppearanceKey = "button-ok";
			this.ok.Name = "ok";
			this.ok.Click += new System.EventHandler(this.ok_Click);
			// 
			// cancel
			// 
			resources.ApplyResources(this.cancel, "cancel");
			this.cancel.AppearanceKey = "button-cancel";
			this.cancel.Name = "cancel";
			this.cancel.Click += new System.EventHandler(this.cancel_Click);
			// 
			// ColumnFilterPanel
			// 
			this.Accelerators = new Wisej.Web.Keys[] {
        Wisej.Web.Keys.Enter,
        Wisej.Web.Keys.Escape};
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = Wisej.Web.AutoScaleMode.Font;
			this.BorderStyle = Wisej.Web.BorderStyle.Solid;
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.ok);
			this.Name = "ColumnFilterPanel";
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
