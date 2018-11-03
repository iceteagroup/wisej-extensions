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
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.WebARIA
{
	/// <summary>
	/// Represents the WAI-ARIA properties associated to a <see cref="Control"/>.
	/// </summary>
	[TypeConverter(typeof(ARIA.ExpandableObjectConverter))]
	public class ARIA
	{
		private Control owner;

		internal ARIA(Control owner)
		{
			this.owner = owner;
		}

		#region Properties

		/// <summary>
		/// Returns or sets whether the element is visible.
		/// </summary>
		[DefaultValue(TriState.NotSet)]
		[SRDescription("ARIAHiddenDescr")]
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
		/// Returns or set whether a value is required.
		/// </summary>
		[DefaultValue(TriState.NotSet)]
		[SRDescription("ARIARequiredDescr")]
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
		/// Returns or sets whether the element is read only.
		/// </summary>
		[DefaultValue(TriState.NotSet)]
		[SRDescription("ARIAReadOnlyDescr")]
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
		/// Returns or sets whether the element is selected.
		/// </summary>
		[DefaultValue(TriState.NotSet)]
		[SRDescription("ARIASelectedDescr")]
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
		/// Returns or sets whether the element is expanded.
		/// </summary>
		[DefaultValue(TriState.NotSet)]
		[SRDescription("ARIAExpandedDescr")]
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
		/// Returns or sets the label that the element is labeled by.
		/// </summary>
		[DefaultValue(null)]
		[SRDescription("ARIALabeledByDescr")]
		public ControlBase LabeledBy
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
		private ControlBase _labeledBy;

		/// <summary>
		/// Returns or sets the control that the element is described by.
		/// </summary>
		[DefaultValue(null)]
		[SRDescription("ARIADescribedByDescr")]
		public ControlBase DescribedBy
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
		private ControlBase _describedBy;

		/// <summary>
		/// Returns or sets the current value.
		/// </summary>
		[DefaultValue(0)]
		[SRDescription("ARIAValueNowDescr")]
		public int ValueNow
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
		private int _valueNow;

		/// <summary>
		/// Returns or sets the minimum value.
		/// </summary>
		[DefaultValue(0)]
		[SRDescription("ARIAValueMinDescr")]
		public int ValueMin
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
		private int _valueMin;

		/// <summary>
		/// Returns or sets the maximum value.
		/// </summary>
		[DefaultValue(0)]
		[SRDescription("ARIAValueMaxDescr")]
		public int ValueMax
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
		private int _valueMax;

		/// <summary>
		/// Returns or sets the label.
		/// </summary>
		[DefaultValue("")]
		[SRDescription("ARIALabelDescr")]
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
		/// Returns or sets the value text.
		/// </summary>
		[DefaultValue("")]
		[SRDescription("ARIAValueTextDescr")]
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
		[SRDescription("ARIAInvalidDescr")]
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
			// else if
		}

		private void SetAutoValues(TextBoxBase owner)
		{
		}

		#endregion

		#region Wisej Implementation

		internal object Render()
		{
			var config = new DynamicObject();

			if (this._required != TriState.NotSet)
				config["aria-required"] = this._required;

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
