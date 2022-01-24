namespace Wisej.Web.Ext.ColumnFilter
{
	partial class WhereColumnFilterPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhereColumnFilterPanel));
			this.clear = new Wisej.Web.LinkLabel();
			this.line1 = new Wisej.Web.Line();
			this.cbMatchCase = new Wisej.Web.CheckBox();
			this.cmbOperator = new Wisej.Web.ComboBox();
			this.txtValue = new Wisej.Web.TextBox();
			this.labelLogicalOperator = new Wisej.Web.Ext.ColumnFilter.lblANDOR();
			this.flowLayoutPanel = new Wisej.Web.FlowLayoutPanel();
			this.dateTimePicker1 = new Wisej.Web.DateTimePicker();
			this.flowLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ok
			// 
			resources.ApplyResources(this.ok, "ok");
			// 
			// cancel
			// 
			resources.ApplyResources(this.cancel, "cancel");
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
			// cbMatchCase
			// 
			this.flowLayoutPanel.SetFlowBreak(this.cbMatchCase, true);
			resources.ApplyResources(this.cbMatchCase, "cbMatchCase");
			this.cbMatchCase.Name = "cbMatchCase";
			// 
			// cmbOperator
			// 
			this.cmbOperator.DropDownStyle = Wisej.Web.ComboBoxStyle.DropDownList;
			this.cmbOperator.Items.AddRange(new object[] {
            resources.GetString("cmbOperator.Items"),
            resources.GetString("cmbOperator.Items1"),
            resources.GetString("cmbOperator.Items2"),
            resources.GetString("cmbOperator.Items3"),
            resources.GetString("cmbOperator.Items4"),
            resources.GetString("cmbOperator.Items5"),
            resources.GetString("cmbOperator.Items6"),
            resources.GetString("cmbOperator.Items7"),
            resources.GetString("cmbOperator.Items8"),
            resources.GetString("cmbOperator.Items9"),
            resources.GetString("cmbOperator.Items10"),
            resources.GetString("cmbOperator.Items11"),
            resources.GetString("cmbOperator.Items12"),
            resources.GetString("cmbOperator.Items13"),
            resources.GetString("cmbOperator.Items14"),
            resources.GetString("cmbOperator.Items15"),
            resources.GetString("cmbOperator.Items16"),
            resources.GetString("cmbOperator.Items17"),
            resources.GetString("cmbOperator.Items18"),
            resources.GetString("cmbOperator.Items19")});
			resources.ApplyResources(this.cmbOperator, "cmbOperator");
			this.cmbOperator.Name = "cmbOperator";
			// 
			// txtValue
			// 
			this.flowLayoutPanel.SetFillWeight(this.txtValue, 2);
			resources.ApplyResources(this.txtValue, "txtValue");
			this.txtValue.Name = "txtValue";
			// 
			// labelLogicalOperator
			// 
			resources.ApplyResources(this.labelLogicalOperator, "labelLogicalOperator");
			this.labelLogicalOperator.Name = "labelLogicalOperator";
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.Controls.Add(this.cbMatchCase);
			this.flowLayoutPanel.Controls.Add(this.cmbOperator);
			this.flowLayoutPanel.Controls.Add(this.txtValue);
			this.flowLayoutPanel.Controls.Add(this.dateTimePicker1);
			this.flowLayoutPanel.Controls.Add(this.labelLogicalOperator);
			resources.ApplyResources(this.flowLayoutPanel, "flowLayoutPanel");
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.TabStop = true;
			// 
			// dateTimePicker1
			// 
			resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
			this.flowLayoutPanel.SetFillWeight(this.dateTimePicker1, 1);
			this.dateTimePicker1.Format = Wisej.Web.DateTimePickerFormat.Short;
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Value = new System.DateTime(2018, 1, 10, 0, 6, 28, 837);
			// 
			// WhereColumnFilterPanel
			// 
			this.Controls.Add(this.flowLayoutPanel);
			this.Controls.Add(this.line1);
			this.Controls.Add(this.clear);
			this.Name = "WhereColumnFilterPanel";
			resources.ApplyResources(this, "$this");
			this.Load += new System.EventHandler(this.WhereColumnFilterPanel_Load);
			this.Controls.SetChildIndex(this.ok, 0);
			this.Controls.SetChildIndex(this.cancel, 0);
			this.Controls.SetChildIndex(this.clear, 0);
			this.Controls.SetChildIndex(this.line1, 0);
			this.Controls.SetChildIndex(this.flowLayoutPanel, 0);
			this.flowLayoutPanel.ResumeLayout(false);
			this.flowLayoutPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Web.LinkLabel clear;
		private Web.Line line1;
		private Web.CheckBox cbMatchCase;
		private Web.ComboBox cmbOperator;
		private Web.TextBox txtValue;
		private lblANDOR labelLogicalOperator;
		private Web.FlowLayoutPanel flowLayoutPanel;
		private Web.DateTimePicker dateTimePicker1;
	}
}
