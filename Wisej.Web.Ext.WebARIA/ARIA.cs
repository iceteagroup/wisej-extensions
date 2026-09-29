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
using System.ComponentModel;
using System.Globalization;
using Wisej.Web;
using Wisej.Core;

namespace Wisej.Web.Ext.WebARIA
{
	/// <summary>
	/// Represents the WAI-ARIA properties associated to a <see cref="Control"/> by the <see cref="WebARIA"/> extender.
	/// </summary>
	/// <remarks>
	/// Instances are created by <see cref="WebARIA.GetAria(Control)"/>. Each property that is set (not <see cref="TriState.NotSet"/>,
	/// null or empty) is rendered as the corresponding <c>aria-*</c> attribute on the control's accessibility element.
	/// </remarks>
	[TypeConverter(typeof(ARIA.ExpandableObjectConverter))]
	[ApiCategory("WebARIA")]
	public class ARIA
	{
		private Control owner;

		internal ARIA(Control owner)
		{
			this.owner = owner;
		}

		#region Properties

		/// <summary>
		/// Returns or sets whether the element is hidden from assistive technologies (<c>aria-hidden</c>).
		/// </summary>
		[DefaultValue(TriState.NotSet)]
		[Description("Returns or sets whether the element is visible.")]
		public TriState Hidden
		{
			get { return this._hidden; }
			set
			{
				if (this._hidden != value)
				{
					this._hidden = value;
					Update();
				}
			}
		}
		private TriState _hidden = TriState.NotSet;

		/// <summary>
		/// Returns or sets whether a value is required (<c>aria-required</c>).
		/// </summary>
		[DefaultValue(TriState.NotSet)]
		[Description("Returns or set whether a value is required.")]
		public TriState Required
		{
			get { return this._required; }
			set
			{
				if (this._required != value)
				{
					this._required = value;
					Update();
				}
			}
		}
		private TriState _required = TriState.NotSet;

		/// <summary>
		/// Returns or sets whether the element is read only (<c>aria-readonly</c>).
		/// </summary>
		[DefaultValue(TriState.NotSet)]
		[Description("Returns or sets whether the element is read only.")]
		public TriState ReadOnly
		{
			get { return this._readOnly; }
			set
			{
				if (this._readOnly != value)
				{
					this._readOnly = value;
					Update();
				}
			}
		}
		private TriState _readOnly = TriState.NotSet;

		/// <summary>
		/// Returns or sets whether the element is selected (<c>aria-selected</c>).
		/// </summary>
		[DefaultValue(TriState.NotSet)]
		[Description("Returns or sets whether the element is selected.")]
		public TriState Selected
		{
			get { return this._selected; }
			set
			{
				if (this._selected != value)
				{
					this._selected = value;
					Update();
				}
			}
		}
		private TriState _selected = TriState.NotSet;

		/// <summary>
		/// Returns or sets whether the element is expanded (<c>aria-expanded</c>).
		/// </summary>
		[DefaultValue(TriState.NotSet)]
		[Description("Returns or sets whether the element is expanded.")]
		public TriState Expanded
		{
			get { return this._expanded; }
			set
			{
				if (this._expanded != value)
				{
					this._expanded = value;
					Update();
				}
			}
		}
		private TriState _expanded = TriState.NotSet;

		/// <summary>
		/// Returns or sets the control that labels the element (<c>aria-labelledby</c>).
		/// </summary>
		/// <example>
		/// Associating a label with a text box:
		/// <code><![CDATA[
		/// this.webARIA1.GetAria(this.textBoxName).LabeledBy = this.labelName;
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Returns or sets the label that the element is labeled by.")]
		public Control LabeledBy
		{
			get { return this._labeledBy; }
			set
			{
				if (this._labeledBy != value)
				{
					this._labeledBy = value;
					Update();
				}
			}
		}
		private Control _labeledBy;

		/// <summary>
		/// Returns or sets the control that describes the element (<c>aria-describedby</c>).
		/// </summary>
		/// <example>
		/// Associating a hint label with a password field:
		/// <code><![CDATA[
		/// this.webARIA1.GetAria(this.textBoxPassword).DescribedBy = this.labelPasswordRules;
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Returns or sets the control that the element is described by.")]
		public Control DescribedBy
		{
			get { return this._describedBy; }
			set
			{
				if (this._describedBy != value)
				{
					this._describedBy = value;
					Update();
				}
			}
		}
		private Control _describedBy;

		/// <summary>
		/// Returns or sets the current value of a range widget (<c>aria-valuenow</c>).
		/// </summary>
		/// <remarks>
		/// Use together with <see cref="ValueMin"/> and <see cref="ValueMax"/>; set to null to omit the attribute.
		/// </remarks>
		/// <example>
		/// Describing a custom progress indicator:
		/// <code><![CDATA[
		/// var aria = this.webARIA1.GetAria(this.panelProgress);
		/// aria.ValueMin = 0;
		/// aria.ValueMax = 100;
		/// aria.ValueNow = 45;
		/// aria.ValueText = "45 percent completed";
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Returns or sets the current value.")]
		public int? ValueNow
		{
			get { return this._valueNow; }
			set
			{
				if (this._valueNow != value)
				{
					this._valueNow = value;
					Update();
				}
			}
		}
		private int? _valueNow;

		/// <summary>
		/// Returns or sets the minimum value of a range widget (<c>aria-valuemin</c>).
		/// </summary>
		/// <remarks>
		/// Set to null to omit the attribute. See <see cref="ValueNow"/> for an example.
		/// </remarks>
		[DefaultValue(null)]
		[Description("Returns or sets the minimum value.")]
		public int? ValueMin
		{
			get { return this._valueMin; }
			set
			{
				if (this._valueMin != value)
				{
					this._valueMin = value;
					Update();
				}
			}
		}
		private int? _valueMin;

		/// <summary>
		/// Returns or sets the maximum value of a range widget (<c>aria-valuemax</c>).
		/// </summary>
		/// <remarks>
		/// Set to null to omit the attribute. See <see cref="ValueNow"/> for an example.
		/// </remarks>
		[DefaultValue(null)]
		[Description("Returns or sets the maximum value.")]
		public int? ValueMax
		{
			get { return this._valueMax; }
			set
			{
				if (this._valueMax != value)
				{
					this._valueMax = value;
					Update();
				}
			}
		}
		private int? _valueMax;

		/// <summary>
		/// Returns or sets the accessible label of the element (<c>aria-label</c>).
		/// </summary>
		/// <remarks>
		/// Setting it to null is the same as setting it to an empty string, which omits the attribute.
		/// </remarks>
		[DefaultValue("")]
		[Description("Returns or sets the label.")]
		public string Label
		{
			get { return this._label; }
			set
			{
				if (this._label != value)
				{
					this._label = value ?? string.Empty;
					Update();
				}
			}
		}
		private string _label = string.Empty;

		/// <summary>
		/// Returns or sets the human readable text alternative of the current value (<c>aria-valuetext</c>).
		/// </summary>
		/// <remarks>
		/// Setting it to null is the same as setting it to an empty string, which omits the attribute.
		/// </remarks>
		[DefaultValue("")]
		[Description("Returns or sets the value text.")]
		public string ValueText
		{
			get { return this._valueText; }
			set
			{
				if (this._valueText != value)
				{
					this._valueText = value ?? string.Empty;
					Update();
				}
			}
		}
		private string _valueText = String.Empty;

		/// <summary>
		/// Returns or sets whether the control validates ok or not.
		/// </summary>
		[DefaultValue(Invalid.NotSet)]
		[Description("Returns or sets whether the control validates ok or not.")]
		Invalid Invalid
		{
			get { return this._invalid; }
			set
			{
				if (this._invalid != value)
				{
					this._invalid = value;
					Update();
				}
			}
		}
		private Invalid _invalid = Invalid.NotSet;

		private void Update()
		{
			this.owner?.Update();
		}

		#endregion

		#region Methods

		internal void SetAutoValues()
		{
			if (this.owner is TextBoxBase)
				SetAutoValues((TextBoxBase)this.owner);
			else if (this.owner is NumericUpDown)
				SetAutoValues((NumericUpDown)this.owner);
			else if (this.owner is CheckBox)
				SetAutoValues((CheckBox)this.owner);
			else if (this.owner is RadioButton)
				SetAutoValues((RadioButton)this.owner);
			else if (this.owner is Button)
				SetAutoValues((Button)this.owner);
			// DataGridViewCell ??
			// TreeNode ??

			_hidden = owner.Visible ? TriState.False : TriState.True;

			// determine label
			Control prevControl = this.owner.Parent?.GetNextControl(this.owner, false);
			if (prevControl != null && prevControl is Label)
				_labeledBy = prevControl;

		}

		private void SetAutoValues(TextBoxBase owner)
		{
			this._valueText = owner.Text;			
			this._readOnly = owner.ReadOnly ? TriState.True : TriState.False;
		}

		private void SetAutoValues(NumericUpDown owner)
		{
			this._valueMin = Convert.ToInt32(owner.Minimum);
			this._valueMax = Convert.ToInt32(owner.Maximum);
			this._valueNow = Convert.ToInt32 (owner.Value);
		}

		private void SetAutoValues(CheckBox owner)
		{
			switch (owner.CheckState)
			{
				case CheckState.Checked:
				{
					this._selected = TriState.True;
					break;
				}
				case CheckState.Unchecked:
				{
					this._selected = TriState.False;
					break;
				}
				case CheckState.Indeterminate:
				{
					this._selected = TriState.Undefined;
					break;
				}
			}
		}
		private void SetAutoValues(RadioButton owner)
		{
			this._selected = owner.Checked ? TriState.True : TriState.False;
		}

		private void SetAutoValues(Button owner)
		{
			this._valueText = owner.Text;
			this._readOnly = owner.Enabled ? TriState.True : TriState.False;
		}

		#endregion

		#region Wisej Implementation

		internal object Render()
		{
			var config = new DynamicObject();

			if (this._hidden != TriState.NotSet)
				config["aria-hidden"] = this._hidden;
			if (this._required != TriState.NotSet)
				config["aria-required"] = this._required;
			if (this._readOnly != TriState.NotSet)
				config["aria-readonly"] = this._readOnly;
			if (this.Selected != TriState.NotSet)
				config["aria-selected"] = this._selected;
			if (this._expanded != TriState.NotSet)
				config["aria-expanded"] = this._expanded;
			if (this._label != string.Empty)
				config["aria-label"] = this._label;
			if (this._valueText != string.Empty)
				config["aria-valuetext"] = this._valueText;			
			if (this._valueMin != null)
				config["valuemin"] = this._valueMin;
			if (this._valueMax != null)
				config["valueMax"] = this._valueMax;
			if (this._valueNow != null)
				config["valueNow"] = this._valueNow;
			if (this._describedBy != null)
				config["aria-describedby"] = this._describedBy;
			if (this._labeledBy != null)
				config["aria-labeledby"] = this._labeledBy;
			if (this._invalid != Invalid.NotSet)
				config["aria-invalid"] = this._invalid;

			return config;
		}

		#endregion

		#region Type Converter

		internal class ExpandableObjectConverter : System.ComponentModel.ExpandableObjectConverter
		{
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
					return "(...)";

				return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		#endregion
	}

}
