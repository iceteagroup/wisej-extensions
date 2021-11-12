namespace Wisej.Web.Ext.ColumnFilter
{
	partial class SimpleColumnFilterPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleColumnFilterPanel));
			this.items = new Wisej.Web.CheckedListBox();
			this.selectAll = new Wisej.Web.LinkLabel();
			this.clear = new Wisej.Web.LinkLabel();
			this.line1 = new Wisej.Web.Line();
			this.SuspendLayout();
			// 
			// items
			// 
			resources.ApplyResources(this.items, "items");
			this.items.BorderStyle = Wisej.Web.BorderStyle.None;
			this.items.Items.AddRange(new object[] {
            resources.GetString("items.Items")});
			this.items.Name = "items";
			// 
			// selectAll
			// 
			resources.ApplyResources(this.selectAll, "selectAll");
			this.selectAll.LinkBehavior = Wisej.Web.LinkBehavior.NeverUnderline;
			this.selectAll.Name = "selectAll";
			this.selectAll.LinkClicked += new Wisej.Web.LinkLabelLinkClickedEventHandler(this.selectAll_LinkClicked);
			// 
			// clear
			// 
			resources.ApplyResources(this.clear, "clear");
			this.clear.LinkBehavior = Wisej.Web.LinkBehavior.NeverUnderline;
			this.clear.Name = "clear";
			this.clear.LinkClicked += new Wisej.Web.LinkLabelLinkClickedEventHandler(this.clear_LinkClicked);
			// 
			// line1
			// 
			resources.ApplyResources(this.line1, "line1");
			this.line1.LineStyle = Wisej.Web.LineStyle.Dotted;
			this.line1.Name = "line1";
			// 
			// SimpleColumnFilterPanel
			// 
			this.Controls.Add(this.line1);
			this.Controls.Add(this.clear);
			this.Controls.Add(this.selectAll);
			this.Controls.Add(this.items);
			this.Name = "SimpleColumnFilterPanel";
			this.Controls.SetChildIndex(this.ok, 0);
			this.Controls.SetChildIndex(this.cancel, 0);
			this.Controls.SetChildIndex(this.items, 0);
			this.Controls.SetChildIndex(this.selectAll, 0);
			this.Controls.SetChildIndex(this.clear, 0);
			this.Controls.SetChildIndex(this.line1, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Web.CheckedListBox items;
		private Web.LinkLabel selectAll;
		private Web.LinkLabel clear;
		private Web.Line line1;
	}
}
