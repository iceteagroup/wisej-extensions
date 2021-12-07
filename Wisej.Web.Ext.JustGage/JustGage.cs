///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// Author: Gianluca Pivato
// Additions: Nic Adams
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
using System.Drawing;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.JustGage
{
	/// <summary>
	/// Represents a JustGage (http://justgage.com/) control.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(JustGage))]
	[DefaultProperty("Value")]
	[DefaultBindingProperty("Value")]
	[Description("JustGage is an animated, nice & clean gage widget.")]
	[ApiCategory("JustGage")]
	public class JustGage : Control, IWisejControl
	{
		#region Events

		/// <summary>
		/// Fired when the value changes.
		/// </summary>
		public event EventHandler ValueChanged;

		/// <summary>
		/// Fired when the value of the <see cref="P:Wisej.Web.Ext.JustGage.JustGage.AutoSize" /> property changes.
		/// </summary>
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler AutoSizeChanged
		{
			add { base.AutoSizeChanged += value; }
			remove { base.AutoSizeChanged -= value; }
		}

		/// <summary>
		/// Fired when the <see cref="P:Wisej.Web.Ext.JustGage.JustGage.BackgroundImage" /> property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add { base.BackgroundImageChanged += value; }
			remove { base.BackgroundImageChanged -= value; }
		}

		/// <summary>
		/// Fired when the <see cref="P:Wisej.Web.Ext.JustGage.JustGage.BackgroundImageLayout" /> property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add { base.BackgroundImageLayoutChanged += value; }
			remove { base.BackgroundImageLayoutChanged -= value; }
		}

		/// <summary>
		/// Fired when the <see cref="P:Wisej.Web.Ext.JustGage.JustGage.Font" /> property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add { base.FontChanged += value; }
			remove { base.FontChanged -= value; }
		}

		/// <summary>
		/// Fired when the <see cref="P:Wisej.Web.Ext.JustGage.JustGage.ForeColor" /> property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add { base.ForeColorChanged += value; }
			remove { base.ForeColorChanged -= value; }
		}

		/// <summary>
		/// Fired when the <see cref="P:Wisej.Web.Ext.JustGage.JustGage.ImeMode" /> property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add { base.ImeModeChanged += value; }
			remove { base.ImeModeChanged -= value; }
		}

		/// <summary>
		/// Fired when the value of the <see cref="P:Wisej.Web.Ext.JustGage.JustGage.Padding" /> property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add { base.PaddingChanged += value; }
			remove { base.PaddingChanged -= value; }
		}

		/// <summary>
		/// Fired when the <see cref="P:Wisej.Web.Ext.JustGage.JustGage.Text" /> property changes.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add { base.TextChanged += value; }
			remove { base.TextChanged -= value; }
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.JustGage.JustGage.ValueChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected virtual void OnValueChanged(EventArgs e)
		{
			if (this.ValueChanged != null)
				ValueChanged(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets a numeric value that represents the current position of the JustGage widget.
		/// </summary>
		[Bindable(true)]
		[DefaultValue(0)]
		[DesignerActionList]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets a numeric value that represents the current position of the JustGage widget.")]
		public float Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (this._value != value)
				{
					this._value = value;

					Update();

					OnValueChanged(EventArgs.Empty);
				}
			}
		}
		private float _value;

		/// <summary>
		/// Returns or sets the lower limit of the range.
		/// </summary>
		[DefaultValue(0)]
		[DesignerActionList]
		[RefreshProperties(RefreshProperties.All)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the lower limit of the range.")]
		public float Minimum
		{
			get
			{
				return this._minimum;
			}
			set
			{
				if (this._minimum != value)
				{
					if (value > this._maximum)
						this._maximum = value;
					else
						this._minimum = value;

					Update();
				}
			}
		}
		private float _minimum = 0;

		/// <summary>
		/// Returns or sets the upper limit of the range.
		/// </summary>
		[DefaultValue(100)]
		[DesignerActionList]
		[RefreshProperties(RefreshProperties.All)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the upper limit of the range.")]
		public float Maximum
		{
			get
			{
				return this._maximum;
			}
			set
			{
				if (this._maximum != value)
				{
					if (value < this._minimum)
						this._minimum = value;
					else
						this._maximum = value;

					Update();
				}
			}
		}
		private float _maximum = 100;

		/// <summary>
		/// Returns or sets the title of the JustGage control.
		/// </summary>
		[DesignerActionList]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the title of the JustGage control.")]
		public override string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		/// <summary>
		/// Returns or sets a value that indicates whether the title should be above or below the gauge.
		/// </summary>
		[DefaultValue(JustGageTitlePosition.Above)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets a value that indicates whether the title should be above or below the gauge.")]
		public JustGageTitlePosition TitlePosition
		{
			get
			{
				return this._titlePosition;
			}
			set
			{
				if (this._titlePosition != value)
				{
					this._titlePosition = value;

					Update();
				}
			}
		}
		private JustGageTitlePosition _titlePosition = JustGageTitlePosition.Above;

		/// <summary>
		/// Defines the title position - above or below the gauge.
		/// </summary>
		public enum JustGageTitlePosition
		{
			/// <summary>
			/// Title appears above gauge
			/// </summary>
			Above,

			/// <summary>
			/// Title appears below gauge
			/// </summary>
			Below
		}

		/// <summary>
		/// Returns or sets a value that indicates whether the min and max labels should be displayed.
		/// </summary>
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets a value that indicates whether the min and max labels should be displayed.")]
		public bool ShowMinMax
		{
			get
			{
				return this._showMinMax;
			}
			set
			{
				if (this._showMinMax != value)
				{
					this._showMinMax = value;

					Update();
				}
			}
		}
		private bool _showMinMax = true;

		/// <summary>
		/// Returns or sets a value that indicates whether the pointer should be displayed.
		/// </summary>
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets a value that indicates whether the pointer should be displayed.")]
		public bool ShowPointer
		{
			get
			{
				return this._showPointer;
			}
			set
			{
				if (this._showPointer != value)
				{
					this._showPointer = value;

					Update();
				}
			}
		}
		private bool _showPointer = false;

		/// <summary>
		/// Returns or sets a value that indicates whether the gauge display should be reversed.
		/// </summary>
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets a value that indicates whether the gauge display should be reversed.")]
		public bool Reverse
		{
			get
			{
				return this._reverse;
			}
			set
			{
				if (this._reverse != value)
				{
					this._reverse = value;

					Update();
				}
			}
		}
		private bool _reverse = false;

		/// <summary>
		/// Returns or sets a value that indicates whether the value should display under the gauge.
		/// </summary>
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets a value that indicates whether the value should display under the gauge.")]
		public bool ShowValue
		{
			get
			{
				return this._showValue;
			}
			set
			{
				if (this._showValue != value)
				{
					this._showValue = value;

					Update();
				}
			}
		}
		private bool _showValue = true;


		/// <summary>
		/// Returns or sets a value that indicates whether the gauge should display as a donut.
		/// </summary>
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets a value that indicates whether the gauge should display as a donut.")]
		public bool Donut
		{
			get
			{
				return this._donut;
			}
			set
			{
				if (this._donut != value)
				{
					this._donut = value;

					Update();
				}
			}
		}
		private bool _donut = false;

		/// <summary>
		/// Returns or sets a value that indicates whether the gauge sectors should change with a gradient.
		/// </summary>
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets a value that indicates whether the gauge sectors should change with a gradient.")]
		public bool Gradient
		{
			get
			{
				return this._gradient;
			}
			set
			{
				if (this._gradient != value)
				{
					this._gradient = value;

					Update();
				}
			}
		}
		private bool _gradient = true;


		/// <summary>
		/// Returns or sets a value that indicates whether large numbers are displayed in a human friendly fashion for min/max and value.
		/// ie. 1234567 -> 1.2M
		/// </summary>
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets a value that indicates whether large numbers are displayed in a human friendly fashion for min/max and value.")]
		public bool HumanFriendly
		{
			get
			{
				return this._humanFriendly;
			}
			set
			{
				if (this._humanFriendly != value)
				{
					this._humanFriendly = value;

					Update();
				}
			}
		}
		private bool _humanFriendly = false;


		/// <summary>
		/// Returns or sets the number of decimal places to display.
		/// Ignored (by JustGage) if humanFriendly is true.
		/// </summary>
		[DefaultValue(0)]
		[RefreshProperties(RefreshProperties.All)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the number of decimal places to display.")]
		public int Decimals
		{
			get
			{
				return this._decimals;
			}
			set
			{
				this._decimals = value;

				Update();
			}
		}
		private int _decimals = 0;

		/// <summary>
		/// Returns or sets a value that indicates whether the number should be displayed with thousand separators.
		/// (Ignored if humanFriendly is true)
		/// </summary>
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets a value that indicates whether the number should be displayed with thousand separators.")]
		public bool FormatNumber
		{
			get
			{
				return this._formatNumber;
			}
			set
			{
				if (this._formatNumber != value)
				{
					this._formatNumber = value;

					Update();
				}
			}
		}
		private bool _formatNumber = false;

		/// <summary>
		/// Returns or sets a value that indicates whether the value change should be animated.
		/// </summary>
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets a value that indicates whether the value change should be animated.")]
		public bool Counter
		{
			get
			{
				return this._counter;
			}
			set
			{
				if (this._counter != value)
				{
					this._counter = value;

					Update();
				}
			}
		}
		private bool _counter = false;

		/// <summary>
		/// Returns or sets a value that indicates the type of animation used by the gage.
		/// </summary>
		[DefaultValue(JustGageAnimationType.EaseIn)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets a value that indicates the type of animation used by the gage.")]
		public JustGageAnimationType StartAnimationType
		{
			get
			{
				return this._startAnimationType;
			}
			set
			{
				if (this._startAnimationType != value)
				{
					this._startAnimationType = value;

					Update();
				}
			}
		}
		private JustGageAnimationType _startAnimationType = JustGageAnimationType.EaseIn;

		/// <summary>
		/// Returns or sets a value that indicates the type of animation used when the value is refreshed.
		/// </summary>
		[DefaultValue(JustGageAnimationType.EaseIn)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets a value that indicates the type of animation used when the value is refreshed.")]
		public JustGageAnimationType RefreshAnimationType
		{
			get
			{
				return this._refreshAnimationType;
			}
			set
			{
				if (this._refreshAnimationType != value)
				{
					this._refreshAnimationType = value;

					Update();
				}
			}
		}
		private JustGageAnimationType _refreshAnimationType = JustGageAnimationType.EaseIn;

		/// <summary>
		/// Defines how the gauge should be animated.
		/// </summary>
		public enum JustGageAnimationType
		{
			/// <summary>
			/// Same speed for whole of value.
			/// </summary>
			Linear,
			/// <summary>
			/// Speeds up as gets towards value.
			/// </summary>
			EaseOut,
			/// <summary>
			/// Slows down as gets towards value.
			/// </summary>
			EaseIn,
			/// <summary>
			///  Speeds up towards middle of value then slows down towards value.
			/// </summary>
			EaseInOut,
			/// <summary>
			/// Goes over the actual value and "bounces" back.
			/// </summary>
			Bounce
		}

		/// <summary>
		/// Convert the enum to the string needed by the gage control
		/// </summary>
		/// <param name="AnimationType"></param>
		/// <returns></returns>
		private string ConvertAnimationType(JustGageAnimationType AnimationType)
		{
			switch (AnimationType)
			{
				case JustGageAnimationType.Bounce:
					return ("bounce");

				case JustGageAnimationType.EaseIn:
					return (">");

				case JustGageAnimationType.EaseInOut:
					return ("<>");

				case JustGageAnimationType.EaseOut:
					return ("<");

				case JustGageAnimationType.Linear:
					return ("linear");

				default:
					throw (new Exception("Unhandled animation type"));
			}
		}

		/// <summary>
		/// Gets or sets custom sectors for the gauge.
		/// </summary>
		[DefaultValue(null)]
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[SRCategory("CatBehavior")]
		[Description("Gets or sets custom sectors for the gauge.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public CustomSector[] CustomSectors
		{
			get
			{
				return this._customSectors;
			}
			set
			{
				this._customSectors = value;

				Update();
			}
		}
		private CustomSector[] _customSectors;

		/// <summary>
		/// Returns or sets the label to display below the gage.
		/// </summary>
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the label to display below the gage.")]
		public string Label
		{
			get
			{
				return this._label;
			}
			set
			{
				if (value == null)
					value = string.Empty;

				if (this._label != value)
				{
					this._label = value;

					Update();
				}
			}
		}
		private string _label = null;


		/// <summary>
		/// Returns or sets the symbol to display with the value.
		/// </summary>
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the symbol to display with the value.")]
		public string Symbol
		{
			get
			{
				return this._symbol;
			}
			set
			{
				if (value == null)
					value = string.Empty;

				if (this._symbol != value)
				{
					this._symbol = value;

					Update();
				}
			}
		}
		private string _symbol = null;

		/// <summary>
		/// Returns or sets the label color.
		/// </summary>
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the label color.")]
		public Color LabelColor
		{
			get
			{
				return
					this._labelColor == Color.Empty
						? GetThemeLabelColor()
						: this._labelColor;
			}
			set
			{
				if (this._labelColor != value)
				{
					this._labelColor = value;

					Update();
				}
			}
		}
		private Color _labelColor = Color.Empty;

		private bool ShouldSerializeLabelColor()
		{
			return !this._labelColor.IsEmpty;
		}

		private void ResetLabelColor()
		{
			this.LabelColor = Color.Empty;
		}

		private Color GetThemeLabelColor()
		{
			IWisejControl me = this;
			return me.Theme.GetColor("labelColor");
		}

		/// <summary>
		/// Returns or sets the value color.
		/// </summary>
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the value color.")]
		public Color ValueColor
		{
			get
			{
				return
					this._valueColor == Color.Empty
						? GetThemeValueColor()
						: this._valueColor;
			}
			set
			{
				if (this._valueColor != value)
				{
					this._valueColor = value;

					Update();
				}
			}
		}
		private Color _valueColor = Color.Empty;

		private bool ShouldSerializeValueColor()
		{
			return !this._valueColor.IsEmpty;
		}

		private void ResetValueColor()
		{
			this.ValueColor = Color.Empty;
		}

		private Color GetThemeValueColor()
		{
			IWisejControl me = this;
			return me.Theme.GetColor("valueColor");
		}

		/// <summary>
		/// Indicates the border style for the control.
		/// </summary>
		/// <returns>One of the <see cref="T:Wisej.Web.BorderStyle" /> values. The default is BorderStyle.None.</returns>
		[DefaultValue(BorderStyle.None)]
		[SRCategory("CatAppearance")]
		[Description("Indicates the border style for the control.")]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				return this._borderStyle;
			}
			set
			{
				if (this._borderStyle != value)
				{
					this._borderStyle = value;

					Refresh();

					OnStyleChanged(EventArgs.Empty);
				}
			}
		}
		private BorderStyle _borderStyle = BorderStyle.None;

		#endregion

		#region Methods

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Returns the theme appearance key for this control.
		/// </summary>
		string IWisejControl.AppearanceKey
		{
			get { return this.AppearanceKey ?? "justgage"; }
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			IWisejControl me = this;

			config.className = "wisej.web.ext.JustGage";

			config.title = this.Text;
			config.value = this.Value;
			config.label = this.Label;
			config.symbol = this.Symbol;
			config.minimum = this.Minimum;
			config.maximum = this.Maximum;
			config.showMinMax = this.ShowMinMax;
			config.showPointer = this.ShowPointer;
			config.reverse = this.Reverse;
			config.showValue = this.ShowValue;
			config.showDonut = this.Donut;
			config.labelColor = this.LabelColor;
			config.valueColor = this.ValueColor;
			config.ShowGradient = this.Gradient;
			config.borderStyle = this.BorderStyle;
			config.customSectors = this.CustomSectors;
			config.humanFriendly = this.HumanFriendly;
			config.decimals = this.Decimals;
			config.formatNumber = this.FormatNumber;
			config.counter = this.Counter;
			config.titlePosition = this.TitlePosition;
			config.startAnimationType = ConvertAnimationType(this.StartAnimationType);
			config.refreshAnimationType = ConvertAnimationType(this.RefreshAnimationType);
		}

		#endregion

	}
}
