///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// 
//
// ALL INFORMATION CONTAINED HEREIN IS, AND REMAINS
// THE PROPERTY OF ICE TEA GROUP LLC AND ITS SUPPLIERS, IF ANY.
// THE INTELLECTUAL PROPERTY AND TECHNICAL CONCEPTS CONTAINED
// HEREIN ARE PROPRIETARY TO ICE TEA GROUP LLC AND ITS SUPPLIERS
// AND MAY BE COVERED BY U.S. AND FOREIGN PATENTS, PATENT IN PROCESS, AND
// ARE PROTECTED BY TRADE SECRET OR COPYRIGHT LAW.
//
// DISSEMINATION OF THIS INFORMATION OR REPRODUCTION OF THIS MATERIAL
// IS STRICTLY FORBIDDEN UNLESS PRIOR WRITTEN PERMISSION IS OBTAINED
// FROM ICE TEA GROUP LLC.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Wisej.Web.Ext.ColumnFilter
{
	/// <summary>
	/// Where column filter panel showing a list of operators and values based on the Columns ValueType
	///	
	/// </summary>
	[ToolboxItem(true)]
	[ApiCategory("ColumnFilter")]
	public partial class WhereColumnFilterPanel : ColumnFilterPanel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WhereColumnFilterPanel"/> class.
		/// </summary>
		public WhereColumnFilterPanel()
		{
			InitializeComponent();
		}

		private List<string> lstOperators = new List<string>();

		/// <summary>
		/// Show hide controls based on the ValueType
		/// </summary>
		protected override void OnBeforeShow()
		{
			foreach (Control c in this.flowLayoutPanel.Controls)
			{
				if (this.DataGridViewColumn.ValueType == typeof(System.DateTime))
				{
					if (c is TextBox)
						c.Hide();
					else if (c is DateTimePicker)
						c.Show();
				}
				else if (this.DataGridViewColumn.ValueType == typeof(System.Boolean))
				{
					if (c is TextBox)
						c.Hide();
					else if (c is DateTimePicker)
						c.Hide();
				}
				else
				{
					if (c is TextBox)
						c.Show();
					else if (c is DateTimePicker)
						c.Hide();
				}
			}
			// Match case only visible for strings
			if (this.DataGridViewColumn.ValueType == typeof(System.String))
				cbMatchCase.Show();
			else
				cbMatchCase.Hide();
		}

		/// <summary>
		/// Applies the filters registered with each
		/// column in the order of creation in the <see cref="DataGridView.Columns"/> collection.
		/// </summary>
		protected override void ApplyFilters()
		{
			try
			{
				// make all rows visible before applying the filters.
				var dataGrid = this.DataGridViewColumn.DataGridView;
				foreach (var row in dataGrid.Rows)
				{
					row.Visible = true;
				}

                // reset current cell 
                dataGrid.CurrentCell = null;

                // remove all summary rows.
                dataGrid.Rows
					.Where(r => r is DataGridViewSummaryRow)
					.ToList().ForEach(r => dataGrid.Rows.Remove(r));

				// reset Combined where
				dataGrid.UserData.columFiltercombinedWhere = "";

				// apply all the filters.
				base.ApplyFilters();

				// do the actual filtering
				string combinedWhere = dataGrid.UserData.columFiltercombinedWhere;
				if (combinedWhere.Length > 0)
				{
					var indexes = dataGrid.Rows.AsQueryable().Where(combinedWhere).Select(r => r.Index).ToArray();
					foreach (var row in dataGrid.Rows)
					{
						if (Array.BinarySearch(indexes, row.Index) < 0)						
							row.Visible = false;						
					}					
				}

				if (this.ColumnFilter != null)
				{
					this.ColumnFilter.OnRowsFiltered(
						dataGrid.Rows.GetRowCount(DataGridViewElementStates.Visible));
				}
			}
			finally
			{
				Close();
			}
		}

		/// <summary>
		/// Invoked when the <see cref="ColumnFilterPanel"/>
		/// should apply the filter criteria selected by the user.
		/// </summary>
		/// <returns>True to indicate that the filter has been applied. False if the filter has been cleared.</returns>
		protected override bool OnApplyFilter()
		{
			string where = "";

			Type type = this.DataGridViewColumn.ValueType;
			if (Nullable.GetUnderlyingType(type) != null)
				type = Nullable.GetUnderlyingType(type);

			if (type == typeof(System.String) || this.DataGridViewColumn is DataGridViewComboBoxColumn)
			{
				where = GetWhereForString();
			}
			else if (type == typeof(System.DateTime))
			{
				where = GetWhereForDateTime();
			}
			else if (type == typeof(System.Int32) ||
					 type == typeof(System.Decimal) ||
					 type == typeof(System.Double) ||
					 type == typeof(System.Int64))				 
			{
				where = GetWhereForNumber();
			}			
			else if (type == typeof(System.Boolean))
			{
				where = GetWhereForBool();
			}

			// Combine where clauses, always connected with " AND "
			if (where.Length > 0)
			{
				var dataGrid = this.DataGridViewColumn.DataGridView;
				string combinedWhere = dataGrid.UserData.columFiltercombinedWhere ?? "";

				if (combinedWhere.Length > 0)
					combinedWhere += " AND ";

				combinedWhere += where;
				dataGrid.UserData.columFiltercombinedWhere = combinedWhere;
			}

			return where.Length > 0;
		}

		private string GetWhereForNumber()
		{
			string where = "";
			string condition = "";

			Type type = this.DataGridViewColumn.ValueType;
			if (Nullable.GetUnderlyingType(type) != null)
				type = Nullable.GetUnderlyingType(type);

			string Type = type.ToString().Replace("System.", "");			
			string Value1 = Value1 = "Convert.To" + Type + "(Cells[" + this.DataGridViewColumn.Index.ToString() + "].Value)";

			if (cmbOperator.SelectedIndex > -1)
			{
				condition = Value1 + cmbOperator.SelectedItem.ToString() + "Convert.To" + Type + "(\"" + txtValue.Text + "\")";
				where = AppendCondition(condition, "", where);
			}

			// only check cloned fields when the first condition is valid.
			string LogicalOperator = labelLogicalOperator.GetOperator();
			if (where.Length > 0)
			{
				TextBox txt = null;
				ComboBox cmb = null;

				// iterate clones
				foreach (Control c in this.flowLayoutPanel.Controls)
				{
					if (c is ComboBox && c != cmbOperator)
					{
						cmb = c as ComboBox;
					}
					else if (c is TextBox && c != txtValue && cmb != null)
					{
						txt = c as TextBox;
						if (cmb.SelectedIndex > -1 && txt.Text != null)
						{
							condition = Value1 + cmb.SelectedItem.ToString() + "Convert.To" + Type + "(\"" + txt.Text + "\")";
							where = AppendCondition(condition, LogicalOperator, where);
						}
					}
					else if (c is lblANDOR && c != labelLogicalOperator)
					{
						// save logical operator for next condition
						var lbl = c as lblANDOR;
						LogicalOperator = lbl.GetOperator();
					}
				}
			}
			return where;
		}

		private string GetWhereForBool()
		{
			string where = "";
			string condition = "";

			string Type = "Boolean";
			string Value1 = Value1 = "Convert.To" + Type + "(Cells[" + this.DataGridViewColumn.Index.ToString() + "].Value)";

			if (cmbOperator.SelectedIndex > -1)
			{
				condition = GetBoolCondition(Value1, cmbOperator.SelectedItem.ToString());
				where = AppendCondition(condition, "", where);
			}
			string LogicalOperator = labelLogicalOperator.GetOperator();
			// only check cloned fields when the first condition is valid.
			if (where.Length > 0)
			{
				ComboBox cmb = null;				

				// iterate clones
				foreach (Control c in this.flowLayoutPanel.Controls)
				{
					if (c is ComboBox && c != cmbOperator)
					{
						cmb = c as ComboBox;
						if (cmb.SelectedIndex > -1)
						{
							condition = GetBoolCondition(Value1, cmb.SelectedItem.ToString());
							where = AppendCondition(condition, LogicalOperator, where);
						}
					}					
					else if (c is lblANDOR && c != labelLogicalOperator)
					{
						// save logical operator for next condition
						var lbl = c as lblANDOR;
						LogicalOperator = lbl.GetOperator();
					}
				}
			}
			return where;
		}

		private string GetWhereForDateTime()
		{
			string where = "";
			string condition = "";

			string Value1 = "Cells[" + this.DataGridViewColumn.Index.ToString() + "].Value.ToString().Length > 0 && Convert.ToDateTime(Cells[" + this.DataGridViewColumn.Index.ToString() + "].Value).Date";

			if (cmbOperator.SelectedIndex > -1)
			{
				condition = Value1 + cmbOperator.SelectedItem.ToString() + "Convert.ToDateTime(\"" + dateTimePicker1.Value + "\").Date";
				where = AppendCondition(condition, "", where);
			}
			string LogicalOperator = this.labelLogicalOperator.GetOperator();
			// only check cloned fields when the first condition is valid.
			if (where.Length > 0)
			{
				ComboBox cmb = null;
				DateTimePicker dtp;

				// iterate clones
				foreach (Control c in this.flowLayoutPanel.Controls)
				{
					if (c is ComboBox && c != cmbOperator)
					{
						cmb = c as ComboBox;
					}
					else if (c is DateTimePicker && c != dateTimePicker1)
					{
						dtp = c as DateTimePicker;
						if (cmb.SelectedIndex > -1 && dtp.NullableValue != null)
						{
							condition = Value1 + cmb.SelectedItem.ToString() + "Convert.ToDateTime(\"" + dtp.Value + "\").Date";
							where = AppendCondition(condition, LogicalOperator, where);
						}
					}
					else if (c is lblANDOR && c != labelLogicalOperator)
					{
						// save logical operator for next condition
						var lbl = c as lblANDOR;
						LogicalOperator = lbl.GetOperator();
					}
				}
			}
			return where;
		}

		private string GetWhereForString()
		{
			string where = "";
			string condition = "";
			string value = "Cells[" + this.DataGridViewColumn.Index.ToString() + "].FormattedValue.ToString()";
			if (!cbMatchCase.Checked)
				value += ".ToUpper()";

			if (cmbOperator.SelectedIndex > -1)
			{
				if (cbMatchCase.Checked)
					condition = GetStrCondition(value, "(\"" + this.txtValue.Text + "\")", cmbOperator.SelectedItem.ToString());
				else
					condition = GetStrCondition(value, "(\"" + this.txtValue.Text.ToUpper() + "\")", cmbOperator.SelectedItem.ToString());

				where = AppendCondition(condition, "", where);
			}
			string LogicalOperator = this.labelLogicalOperator.GetOperator();

			// only check cloned fields when the first condition is valid.
			if (where.Length > 0)
			{
				TextBox txt = null;
				ComboBox cmb = null;

				// iterate clones
				foreach (Control c in this.flowLayoutPanel.Controls)
				{
					if (c is ComboBox && c != cmbOperator)
					{
						cmb = c as ComboBox;
					}
					else if (c is TextBox && c != txtValue && cmb != null)
					{
						txt = c as TextBox;

						// an operator is required, TextBox can be empty for some operators
						if (cmb.SelectedIndex > -1)
						{
							if (cbMatchCase.Checked)
								condition = GetStrCondition(value, "(\"" + txt.Text + "\")", cmb.SelectedItem.ToString());
							else
								condition = GetStrCondition(value, "(\"" + txt.Text.ToUpper() + "\")", cmb.SelectedItem.ToString());

							where = AppendCondition(condition, LogicalOperator, where);
						}
					}
					else if (c is Label && c != labelLogicalOperator)
					{
						// save logical operator for next condition
						var lbl = c as lblANDOR;
						LogicalOperator = lbl.GetOperator();
					}
				}
			}
			return where;
		}

		private string AppendCondition(string condition, string LogicalOperator, string where)
		{
			string ret = "";
			if (condition.Length > 0)
			{
				if (LogicalOperator == "AND")
					ret = where + " AND " + condition;
				else if (LogicalOperator == "OR")
					ret = "(" + where + " OR " + condition + ")";
				else
					ret = condition;
			}
			return ret;
		}

		private string GetStrCondition(string value1, string value2, string operation)
		{
			if (operation == Messages.equal_to)
				return value2.Length == 0 ? "" : value1 + " == " + value2;
			else if (operation == Messages.not_equal_to)
				return value2.Length == 0 ? "" : value1 + " != " + value2;
			else if (operation == Messages.contains)
				return value2.Length == 0 ? "" : value1 + ".Contains " + value2;
			else if (operation == Messages.does_not_contain)
				return value2.Length == 0 ? "" : "!" + value1 + ".Contains " + value2;
			else if (operation == Messages.starts_with)
				return value2.Length == 0 ? "" : value1 + ".StartsWith " + value2;
			else if (operation == Messages.ends_with)
				return value2.Length == 0 ? "" : value1 + ".EndsWith " + value2;
			else if (operation == Messages.is_empty)
				return value1 + " == \"\"";
			else if (operation == Messages.is_not_empty)
				return value1 + " != \"\"";
			else if (operation == Messages.is_null)
				return value1 + " == null";
			else if (operation == Messages.is_not_null)
				return value1 + " != null";

			return "";
		}

		private string GetBoolCondition(string value1, string operation)
		{
			if (operation == Messages.is_true)
			{
				return value1 + " == true";
			}
			else if (operation == Messages.is_false)
			{
				return value1 + " == false";
			}
			else if (operation == Messages.is_null)
			{
				return value1 + " == null";
			}
			else if (operation == Messages.is_not_null)
			{
				return value1 + " != null";
			}
			return "";
		}

		private void clear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Clear(false);
		}

		/// <summary>
		/// Clear the filter panel
		/// </summary>
		/// <param name="applyFilters"></param>
		public override void Clear(bool applyFilters = true)
		{
			foreach (Control c in this.flowLayoutPanel.Controls)
			{
				if (c is ComboBox)
				{
					var combo = c as ComboBox;
					combo.SelectedIndex = -1;
				}
				else if (c is TextBox)
				{
					var textBox = c as TextBox;
					textBox.Clear();
				}
				else if (c is DateTimePicker)
				{
					var dtp = c as DateTimePicker;
					dtp.Value = DateTime.MinValue;
				}
			}
			if (applyFilters)
			{
				ApplyFilters();				
			}			
		}

		private void WhereColumnFilterPanel_Load(object sender, EventArgs e)
		{
			CloneControls();

			// Operators
			cmbOperator.Items.Clear();

			cmbOperator.Items.Add(Messages.equal_to);
			cmbOperator.Items.Add(Messages.not_equal_to);
			cmbOperator.Items.Add(Messages.contains);
			cmbOperator.Items.Add(Messages.does_not_contain);
			cmbOperator.Items.Add(Messages.starts_with);
			cmbOperator.Items.Add(Messages.ends_with);
			cmbOperator.Items.Add(Messages.is_empty);
			cmbOperator.Items.Add(Messages.is_not_empty);
			cmbOperator.Items.Add(Messages.is_null);
			cmbOperator.Items.Add(Messages.is_not_null);
			cmbOperator.Items.Add("=");
			cmbOperator.Items.Add("!=");
			cmbOperator.Items.Add("<");
			cmbOperator.Items.Add(">");
			cmbOperator.Items.Add("<=");
			cmbOperator.Items.Add(">=");
			cmbOperator.Items.Add(Messages.is_true);
			cmbOperator.Items.Add(Messages.is_false);
			cmbOperator.Items.Add(Messages.is_null);
			cmbOperator.Items.Add(Messages.is_not_null);

			// store complete list of operators for later reference.
			var lstItems = cmbOperator.Items.Cast<Object>().Select(item => item.ToString()).ToList();
			lstOperators = lstItems.ToList();
			int startIndex = 0;
			int count = 0;
			// String =  0-9
			// Number/Date = 10-15
			// Bool = 16-19
			if (this.DataGridViewColumn.ValueType == typeof(string) || this.DataGridViewColumn is DataGridViewComboBoxColumn)
			{
				startIndex = 0;
				count = 10;
			}
			else if (this.DataGridViewColumn.ValueType == typeof(bool))
			{
				startIndex = 16;
				count = 4;
			}
			else 
			{
				startIndex = 10;
				count = 6;
			}
			// fill all Comboboxes (include cmbOperator)
			foreach (Control c in flowLayoutPanel.Controls)
			{
				ComboBox cmb = c as ComboBox;
				if (cmb != null)
				{
					cmb.Items.Clear();
					cmb.Items.AddRange((lstOperators.GetRange(startIndex, count).ToArray()));
				}
			}
		}

		#region Clone Controls

		private void CloneControls()
		{
			int count = 4;
			for (int i = 0; i < count; i++)
			{				
				this.flowLayoutPanel.Controls.Add(CloneCombo(this.cmbOperator));
				this.flowLayoutPanel.Controls.Add(CloneTextBox(this.txtValue));
				this.flowLayoutPanel.Controls.Add(CloneDateTimePicker(this.dateTimePicker1));
				this.flowLayoutPanel.Controls.Add(CloneLabel(this.labelLogicalOperator));
			}
		}

		private ComboBox CloneCombo(ComboBox cmbOriginal)
		{
			var newCombo = (ComboBox)Activator.CreateInstance(cmbOriginal.GetType());
			newCombo.Width = cmbOriginal.Width;
			newCombo.DropDownStyle = cmbOriginal.DropDownStyle;
			newCombo.Items.AddRange(cmbOriginal.Items.Cast<Object>().ToArray());
			this.flowLayoutPanel.SetFillWeight(newCombo, this.flowLayoutPanel.GetFillWeight(cmbOriginal));
			return newCombo;
		}

		private TextBox CloneTextBox(TextBox txtOriginal)
		{
			var newTextBox = (TextBox)Activator.CreateInstance(txtOriginal.GetType());
			newTextBox.Width = txtOriginal.Width;
			this.flowLayoutPanel.SetFillWeight(newTextBox, this.flowLayoutPanel.GetFillWeight(txtOriginal));
			return newTextBox;
		}

		private Label CloneLabel(Label lblOriginal)
		{
			var newLabel = (Label)Activator.CreateInstance(lblOriginal.GetType());
			newLabel.Width = lblOriginal.Width;
			newLabel.Text = lblOriginal.Text;
			newLabel.TextAlign = lblOriginal.TextAlign;
			newLabel.AllowHtml = lblOriginal.AllowHtml;
			this.flowLayoutPanel.SetFillWeight(newLabel, this.flowLayoutPanel.GetFillWeight(lblOriginal));
			return newLabel;
		}

		private DateTimePicker CloneDateTimePicker(DateTimePicker dtpOriginal)
		{
			var newdtp = (DateTimePicker)Activator.CreateInstance(dtpOriginal.GetType());
			newdtp.AutoSize = dtpOriginal.AutoSize;
			newdtp.Width = dtpOriginal.Width;
			newdtp.Format = dtpOriginal.Format;
			this.flowLayoutPanel.SetFillWeight(newdtp, this.flowLayoutPanel.GetFillWeight(dtpOriginal));
			return newdtp;
		}

		#endregion
	}
}


