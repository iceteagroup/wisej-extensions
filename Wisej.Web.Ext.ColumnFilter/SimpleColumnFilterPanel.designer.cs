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
			this.items = new Wisej.Web.CheckedListBox();
			this.selectAll = new Wisej.Web.LinkLabel();
			this.clear = new Wisej.Web.LinkLabel();
			this.line1 = new Wisej.Web.Line();
			this.SuspendLayout();
			// 
			// items
			// 
			this.items.Anchor = ((Wisej.Web.AnchorStyles)((((Wisej.Web.AnchorStyles.Top | Wisej.Web.AnchorStyles.Bottom) 
            | Wisej.Web.AnchorStyles.Left) 
            | Wisej.Web.AnchorStyles.Right)));
			this.items.BorderStyle = Wisej.Web.BorderStyle.None;
			this.items.Items.AddRange(new object[] {
            "Items"});
			this.items.Location = new System.Drawing.Point(10, 43);
			this.items.Name = "items";
			this.items.Size = new System.Drawing.Size(219, 175);
			this.items.Sorted = false;
			this.items.TabIndex = 2;
			// 
			// selectAll
			// 
			this.selectAll.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Top | Wisej.Web.AnchorStyles.Right)));
			this.selectAll.AutoSize = true;
			this.selectAll.LinkBehavior = Wisej.Web.LinkBehavior.NeverUnderline;
			this.selectAll.Location = new System.Drawing.Point(98, 14);
			this.selectAll.Name = "selectAll";
			this.selectAll.Size = new System.Drawing.Size(62, 16);
			this.selectAll.TabIndex = 0;
			this.selectAll.Text = "Select all";
			this.selectAll.LinkClicked += new Wisej.Web.LinkLabelLinkClickedEventHandler(this.selectAll_LinkClicked);
			// 
			// clear
			// 
			this.clear.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Top | Wisej.Web.AnchorStyles.Right)));
			this.clear.AutoSize = true;
			this.clear.LinkBehavior = Wisej.Web.LinkBehavior.NeverUnderline;
			this.clear.Location = new System.Drawing.Point(177, 14);
			this.clear.Name = "clear";
			this.clear.Size = new System.Drawing.Size(40, 16);
			this.clear.TabIndex = 1;
			this.clear.Text = "Clear";
			this.clear.LinkClicked += new Wisej.Web.LinkLabelLinkClickedEventHandler(this.clear_LinkClicked);
			// 
			// line1
			// 
			this.line1.Anchor = ((Wisej.Web.AnchorStyles)(((Wisej.Web.AnchorStyles.Bottom | Wisej.Web.AnchorStyles.Left) 
            | Wisej.Web.AnchorStyles.Right)));
			this.line1.LineStyle = Wisej.Web.LineStyle.Dotted;
			this.line1.Location = new System.Drawing.Point(0, 234);
			this.line1.Name = "line1";
			this.line1.Size = new System.Drawing.Size(240, 10);
			// 
			// SimpleColumnFilterPanel
			// 
			this.Controls.Add(this.line1);
			this.Controls.Add(this.clear);
			this.Controls.Add(this.selectAll);
			this.Controls.Add(this.items);
			this.Name = "SimpleColumnFilterPanel";
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
