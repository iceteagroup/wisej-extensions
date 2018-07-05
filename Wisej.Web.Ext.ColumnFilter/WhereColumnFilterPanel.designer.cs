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
			this.ok.Anchor = Wisej.Web.AnchorStyles.Bottom;
			this.ok.Location = new System.Drawing.Point(67, 256);
			// 
			// cancel
			// 
			this.cancel.Anchor = Wisej.Web.AnchorStyles.Bottom;
			this.cancel.Location = new System.Drawing.Point(171, 256);
			// 
			// clear
			// 
			this.clear.Anchor = ((Wisej.Web.AnchorStyles)((Wisej.Web.AnchorStyles.Top | Wisej.Web.AnchorStyles.Right)));
			this.clear.AutoSize = true;
			this.clear.LinkBehavior = Wisej.Web.LinkBehavior.NeverUnderline;
			this.clear.Location = new System.Drawing.Point(283, 10);
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
			this.line1.Size = new System.Drawing.Size(330, 10);
			// 
			// cbMatchCase
			// 
			this.flowLayoutPanel.SetFlowBreak(this.cbMatchCase, true);
			this.cbMatchCase.Location = new System.Drawing.Point(3, 3);
			this.cbMatchCase.Name = "cbMatchCase";
			this.cbMatchCase.Size = new System.Drawing.Size(94, 23);
			this.cbMatchCase.TabIndex = 27;
			this.cbMatchCase.Text = "MatchCase";
			// 
			// cmbOperator
			// 
			this.cmbOperator.DropDownStyle = Wisej.Web.ComboBoxStyle.DropDownList;
			this.cmbOperator.Items.AddRange(new object[] {
            "equal to",
            "not equal to",
            "contains",
            "does not contain",
            "starts with",
            "ends with",
            "is empty",
            "is not empty",
            "is null",
            "is not null",
            "=",
            "!=",
            "<",
            ">",
            "<=",
            ">=",
			"is true",
			"is false",
			"is null",
			"is not null"});
			this.cmbOperator.Location = new System.Drawing.Point(3, 32);
			this.cmbOperator.Name = "cmbOperator";
			this.cmbOperator.Size = new System.Drawing.Size(128, 22);
			this.cmbOperator.TabIndex = 28;
			// 
			// txtValue
			// 
			this.txtValue.AutoSize = false;
			this.flowLayoutPanel.SetFillWeight(this.txtValue, 2);
			this.txtValue.Location = new System.Drawing.Point(137, 32);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(70, 22);
			this.txtValue.TabIndex = 29;
			// 
			// labelLogicalOperator
			// 
			this.labelLogicalOperator.AllowHtml = true;
			this.labelLogicalOperator.Location = new System.Drawing.Point(255, 32);
			this.labelLogicalOperator.Name = "labelLogicalOperator";
			this.labelLogicalOperator.Size = new System.Drawing.Size(60, 16);
			this.labelLogicalOperator.TabIndex = 30;
			this.labelLogicalOperator.Text = "<b>AND</b> | OR";
			this.labelLogicalOperator.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.Controls.Add(this.cbMatchCase);
			this.flowLayoutPanel.Controls.Add(this.cmbOperator);
			this.flowLayoutPanel.Controls.Add(this.txtValue);
			this.flowLayoutPanel.Controls.Add(this.dateTimePicker1);
			this.flowLayoutPanel.Controls.Add(this.labelLogicalOperator);
			this.flowLayoutPanel.Location = new System.Drawing.Point(5, 32);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(318, 196);
			this.flowLayoutPanel.TabIndex = 32;
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.AutoSize = false;
			this.flowLayoutPanel.SetFillWeight(this.dateTimePicker1, 1);
			this.dateTimePicker1.Location = new System.Drawing.Point(213, 32);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(36, 22);
			this.dateTimePicker1.TabIndex = 31;
			this.dateTimePicker1.Format = DateTimePickerFormat.Short;
			this.dateTimePicker1.Value = new System.DateTime(2018, 1, 10, 0, 6, 28, 837);
			// 
			// WhereColumnFilterPanel
			// 
			this.Controls.Add(this.flowLayoutPanel);
			this.Controls.Add(this.line1);
			this.Controls.Add(this.clear);
			this.Name = "WhereColumnFilterPanel";
			this.Size = new System.Drawing.Size(331, 309);
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
