///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Drawing;

namespace Wisej.Web.Ext.ChartJS4
{
	/// <summary>
	/// Represents the options for the axes.
	/// </summary>
	[ApiCategory("ChartJS4")]
	public class OptionsScales : OptionsBase
	{

		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsScales()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.OptionsScales"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		public OptionsScales(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Options for the x-axes.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public OptionScalesAxesX[] xAxes
		{
			get
			{
				if (this._xAxes == null)
				{
					this.xAxes = new OptionScalesAxesX[1]
					{
						(OptionScalesAxesX)this.DefaultOptionScalesAxesX.Clone()
					};
				}

				return this._xAxes;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				this._xAxes = value;

				if (this._xAxes != null)
				{
					foreach (var x in this._xAxes)
						x.Owner = this;
				}
			}
		}
		private OptionScalesAxesX[] _xAxes;

		private OptionScalesAxesX DefaultOptionScalesAxesX
		{
			get
			{
				if (this._defaultOptionScalesAxesX == null)
					this._defaultOptionScalesAxesX = new OptionScalesAxesX(this);

				return this._defaultOptionScalesAxesX;
			}
		}
		private OptionScalesAxesX _defaultOptionScalesAxesX;

		private bool ShouldSerializexAxes()
		{
			return this._xAxes != null && this._xAxes.Length > 0 && !Object.Equals(this._xAxes[0], DefaultOptionScalesAxesX);
		}

		/// <summary>
		/// Options for the y-axes.
		/// </summary>
		[TypeConverter(typeof(ArrayConverter))]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public OptionScalesAxesY[] yAxes
		{
			get
			{
				if (this._yAxes == null)
				{
					this.yAxes = new OptionScalesAxesY[1]
					{
						(OptionScalesAxesY)this.DefaultOptionScalesAxesY.Clone()
					};
				}

				return this._yAxes;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				this._yAxes = value;

				if (this._yAxes != null)
				{
					foreach (var x in this._yAxes)
						x.Owner = this;
				}
			}
		}
		private OptionScalesAxesY[] _yAxes;

		private OptionScalesAxesY DefaultOptionScalesAxesY
		{
			get
			{
				if (this._defaultOptionScalesAxesY == null)
					this._defaultOptionScalesAxesY = new OptionScalesAxesY(this);

				return this._defaultOptionScalesAxesY;
			}
		}
		private OptionScalesAxesY _defaultOptionScalesAxesY;

		private bool ShouldSerializeyAxes()
		{
			return this._yAxes != null && this._yAxes.Length > 0 && !Object.Equals(this._yAxes[0], DefaultOptionScalesAxesY);
		}

		/// <summary>
		/// Options for the scale R axes.
		/// </summary>
		public OptionsScaleR R
		{
			get
			{
				if (this._r == null)
					this._r = new OptionsScaleR(this);
				return this._r;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				this._r = value;
			}
		}
		private OptionsScaleR _r = null;
	}
	/// <summary>
	/// Represents the options for the scale axes.
	/// </summary>
	public abstract class OptionScalesAxes : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionScalesAxes()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.OptionScalesAxes"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		public OptionScalesAxes(OptionsBase owner)
		{
			this.Owner = owner;

			var chart = this.Chart;
			if (chart != null)
			{
				switch (chart.ChartType)
				{
					case ChartType.Pie:
					case ChartType.Radar:
					case ChartType.Doughnut:
					case ChartType.PolarArea:
						this.Display = false;
						break;
				}
			}
		}

		/// <summary>
		///	Reverse the scale.
		/// </summary>
		[DefaultValue(false)]
		public bool Reverse
		{
			get { return this._reverse; }
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
		/// Background color of the scale area.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[Description("Background color of the scale area.")]
		public Color BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// User defined maximum number for the scale, overrides maximum value from data.
		/// </summary>
		[DefaultValue(null)]
		[Description("User defined maximum number for the scale, overrides maximum value from data.")]
		public int? Max
		{
			get { return this._max; }
			set
			{
				if (this._max != value)
				{
					this._max = value;
					Update();
				}
			}
		}
		private int? _max;

		/// <summary>
		/// User defined minimum number for the scale, overrides minimum value from data.
		/// </summary>
		[DefaultValue(null)]
		[Description("User defined minimum number for the scale, overrides minimum value from data.")]
		public int? Min
		{
			get { return this._min; }
			set
			{
				if (this._min != value)
				{
					this._min = value;
					Update();
				}
			}
		}
		private int? _min;

		/// <summary>
		/// Returns or sets the adjustment used when calculating the maximum data value.
		/// </summary>
		[DefaultValue(null)]
		[Description("Returns or sets the adjustment used when calculating the maximum data value.")]
		public int? SuggestedMax
		{
			get { return this._suggestedMax; }
			set
			{
				if (this._suggestedMax != value)
				{
					this._suggestedMax = value;
					Update();
				}
			}
		}
		private int? _suggestedMax;

		/// <summary>
		/// Returns or sets the adjustment used when calculating the minimum data value.
		/// </summary>
		[DefaultValue(null)]
		[Description("Returns or sets the adjustment used when calculating the minimum data value.")]
		public int? SuggestedMin
		{
			get { return this._suggestedMin; }
			set
			{
				if (this._suggestedMin != value)
				{
					this._suggestedMin = value;
					Update();
				}
			}
		}
		private int? _suggestedMin;

		/// <summary>
		/// Returns or sets the configuration of the axis' grid lines.
		/// </summary>
		public OptionsScalesGrid Grid
		{
			get
			{
				return this._gridLines;
			}
			set
			{
				if (!this._gridLines.Equals(value))
					this._gridLines = value;
			}
		}
		private OptionsScalesGrid _gridLines = new OptionsScalesGrid();

		/// <summary>
		/// If true, show the scale including grid lines, ticks, and labels.
		/// </summary>
		[Description("If true, show the scale including grid lines, ticks, and labels.")]
		public bool Display
		{
			get { return this._display; }
			set
			{
				if (this._display != value)
				{
					this._display = value;
					Update();
				}
			}
		}
		private bool _display = true;

		private bool ShouldSerializeDisplay()
		{
			return !this._display;
		}

		private void ResetDisplay()
		{
			this.Display = true;
		}

		/// <summary>
		/// Used to identify the scale options for multi-axes charts 
		/// </summary>
		[DefaultValue("")]
		[Description("Used to identify the scale options for multi-axes charts")]
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}
		private string _id = string.Empty;

		/// <summary>
		/// Options for the chart ticks on the axes.
		/// </summary>
		[Description("Options for the chart ticks on the axes.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ScalesTicks Ticks
		{
			get
			{
				if (this._ticks == null)
					this._ticks = new ScalesTicks(this);

				return this._ticks;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._ticks = value;
			}
		}
		private ScalesTicks _ticks;

		/// <summary>
		/// Options for the chart ticks on the axes.
		/// </summary>
		[Description("Options for the title on the axes.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsScaleTitle Title
		{
			get
			{
				if (this._title == null)
					this._title = new OptionsScaleTitle(this);

				return this._title;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._title = value;
			}
		}
		private OptionsScaleTitle _title;

		/// <summary>
		/// Type of scale being employed.
		/// </summary>
		[DefaultValue(ScaleType.Linear)]
		[Description("Type of scale being employed")]
		public ScaleType Type
		{
			get { return this._type; }
			set
			{
				if (this._type != value)
				{
					this._type = value;
					Update();
				}
			}
		}
		private ScaleType _type = ScaleType.Linear;

		/// <summary>
		/// When true, the bars are stacked.
		/// </summary>
		[DefaultValue(false)]
		[Description("When true, the bars are stacked.")]
		public object Stacked
		{
			get { return this._stacked; }
			set
			{
				if (this._stacked != value)
				{
					this._stacked = value;
					Update();
				}
			}
		}
		private object _stacked = false;

		/// <summary>  
		/// Gets or sets the stack group for the scale.  
		/// Scales with the same stack value will be stacked together.  
		/// </summary>  
		[Description("Gets or sets the stack group for the scale. Scales with the same stack value will be stacked together.")]
		[DefaultValue("")]
		public string Stack
		{
			get
			{
				return this._stack;
			}
			set
			{
				if (this._stack != value)
				{
					this._stack = value;
					Update();
				}
			}
		}
		private string _stack = string.Empty;

		/// <summary>  
		/// Gets or sets a value indicating whether the scale is offset.  
		/// </summary>  
		/// <remarks>  
		/// When set to true, the scale will have an offset applied to its position.  
		/// </remarks> 
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether the scale is offset. When set to true, the scale will have an offset applied to its position.")]
		public bool Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				if (this._offset != value)
				{
					this._offset = value;
					Update();
				}
			}
		}
		private bool _offset = false;

		/// <summary>
		/// Gets or sets the stack weight for the scale.
		/// Determines the relative weight of the scale when stacking multiple scales.
		/// </summary>
		public int StackWeight
		{
			get { return this._stackWeight; }
			set
			{
				if (this._stackWeight != value)
				{
					this._stackWeight = value;
					Update();
				}
			}
		}
		private int _stackWeight = 0;

		/// <summary>  
		/// Gets or sets the labels for the scale.  
		/// </summary> 
		[Description("Gets or sets the labels for the scale.")]
		public string[] Labels
		{
			get
			{
				return this._labels;
			}
			set
			{
				if (this._labels != value)
				{
					this._labels = value;
					Update();
				}
			}
		}
		private string[] _labels = null;

		/// <summary>
		/// Gets or sets the border options for the scale.
		/// </summary>
		public OptionsBorder Border
		{
			get
			{
				if (this._border == null)
					this._border = new OptionsBorder(this);
				return this._border;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				value.Owner = this;
				this._border = value;
			}
		}
		private OptionsBorder _border = null;

		/// <summary>
		/// Options for the time scale type - ignored if not a time scale type
		/// </summary>
		[Description("Options for the time scale type - ignored if not a time scale type")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ScaleTime Time
		{
			get
			{
				if (this._time == null)
					this._time = new ScaleTime(this);

				return this._time;
			}
			set
			{
				if (this._time != value)
				{
					this._time = value;
					Update();
				}
			}
		}
		private ScaleTime _time = null;

		private bool ShouldSerializeTime()
		{
			return this.Type == ScaleType.Time && this._time != null;
		}

		/// <summary>
		/// Percent (0-1) of the available width each bar should be within the category width.
		/// </summary>
		[DefaultValue(0.9F)]
		[Description("Percent (0-1) of the available width each bar should be within the category width.")]
		public float BarPercentage
		{
			get
			{
				return this._barPercentage;
			}
			set
			{
				if (this._barPercentage != value)
				{
					this._barPercentage = value;
				}
			}
		}
		private float _barPercentage = 0.9F;

		/// <summary>
		/// Percent (0-1) of the available width each category should be within the sample width.
		/// </summary>
		[DefaultValue(0.8F)]
		[Description("Percent(0 - 1) of the available width each category should be within the sample width.")]
		public float CategoryPercentage
		{
			get
			{
				return this._categoryPercentage;
			}
			set
			{
				if (this._categoryPercentage != value)
				{
					this._categoryPercentage = value;
					Update();
				}
			}
		}
		private float _categoryPercentage = 0.8F;
	}

	/// <summary>
	/// Represents the options for the scale X axes.
	/// </summary>
	public class OptionScalesAxesX : OptionScalesAxes
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionScalesAxesX()
		{
			this.Type = ScaleType.Category;
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.OptionScalesAxesX"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		public OptionScalesAxesX(OptionsBase owner)
			: base(owner)
		{
			this.Type = ScaleType.Category;
		}

		/// <summary>
		/// Position of the X axes scale.
		/// </summary>
		[DefaultValue(HeaderPosition.Bottom)]
		[Description("Position of the x axes scale.")]
		public object Position
		{
			get { return this._position; }
			set
			{
				if (this._position != value)
				{
					this._position = value;
					Update();
				}
			}
		}
		private object _position = HeaderPosition.Bottom;

		/// <summary>
		/// Type of scale being employed.
		/// </summary>
		[DefaultValue(ScaleType.Category)]
		[Description("Type of scale being employed")]
		public new ScaleType Type
		{
			get { return base.Type; }
			set { base.Type = value; }
		}

		/// <summary>
		///	Rotation value of the X Axis.
		/// </summary>
		[DefaultValue(0)]
		[Description("Rotation value of the X Axis")]
		public int LabelRotation
		{
			get { return this._labelRotation; }
			set
			{
				if (value != this._labelRotation)
				{
					this.Ticks.MaxRotation = value > 0 ? value : 50;
					this.Ticks.MinRotation = value > 0 ? value : this.Ticks.MinRotation;
					this.Ticks.AutoSkip = value == 0 ? true : false;

					this._labelRotation = value;
					Update();
				}
			}
		}
		private int _labelRotation = 0;
	}

	/// <summary>
	/// Represents the options for the scale X axes.
	/// </summary>
	public class OptionScalesAxesY : OptionScalesAxes
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionScalesAxesY()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.OptionScalesAxesY"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		public OptionScalesAxesY(OptionsBase owner)
					: base(owner)
		{
		}

		/// <summary>
		/// Position of the Y axes scale.
		/// </summary>
		[DefaultValue(HeaderPosition.Left)]
		[Description("Position of the Y axes scale.")]
		public object Position
		{
			get { return this._position; }
			set
			{
				if (this._position != value)
				{
					this._position = value;
					Update();
				}
			}
		}
		private object _position = HeaderPosition.Left;


		/// <summary>
		///	Rotation value of the Y Axis.
		/// </summary>
		[DefaultValue(0)]
		[Description("Rotation value of the Y Axis")]
		public int LabelRotation
		{
			get { return this._labelRotation; }
			set
			{
				if (value != this._labelRotation)
				{
					this.Ticks.MaxRotation = value > 0 ? value : 50;
					this.Ticks.MinRotation = value > 0 ? value : this.Ticks.MinRotation;
					this.Ticks.AutoSkip = value == 0 ? true : false;

					this._labelRotation = value;
					Update();
				}
			}
		}
		private int _labelRotation = 0;

	}

	/// <summary>
	/// Represents the options for the chart title.
	/// </summary>
	public class OptionsScaleTitle : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsScaleTitle()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.OptionsScaleTitle"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		public OptionsScaleTitle(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Font of the title.
		/// </summary>
		[Description("Scale label font.")]
		public Font Font
		{
			get
			{
				var chart = this.Chart;
				if (this._font == null && chart != null)
					return chart.Font;

				return this._font;
			}
			set
			{
				if (this._font != value)
				{
					this._font = value;
					Update();
				}
			}
		}
		private Font _font;

		private bool ShouldSerializeFont()
		{
			return this._font != null;
		}

		/// <summary>
		/// Show the title block.
		/// </summary>
		[DefaultValue(true)]
		[Description("Show the scale label block.")]
		public bool Display
		{
			get { return this._display && !String.IsNullOrEmpty(this._text); }
			set
			{
				if (this._display != value)
				{
					this._display = value;
					Update();
				}
			}
		}
		private bool _display = true;

		/// <summary>
		/// The text for the title. (i.e. "# of People" or "Response Choices").
		/// </summary>
		[DefaultValue("")]
		[Description("The text for the title. (i.e. '# of People' or 'Response Choices').")]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				value = value ?? string.Empty;

				if (this._text != value)
				{
					this._text = value;
					Update();
				}
			}
		}
		private string _text = string.Empty;

		/// <summary>
		/// Color of label.
		/// </summary>
		[Description("Scale label font color.")]
		public Color Color
		{
			get
			{
				var chart = this.Chart;
				if (this._color.IsEmpty && chart != null)
					return chart.ForeColor;

				return this._color;
			}
			set
			{
				if (this._color != value)
				{
					this._color = value;
					Update();
				}
			}
		}
		private Color _color = Color.Empty;

		private bool ShouldSerializeColor()
		{
			return this._color != Color.Empty;
		}
	}
	/// <summary>
	/// Represents the options for the scale R axes.
	/// </summary>
	public class OptionsScaleR : OptionsBase
	{
		public OptionsScaleR()
		{
		}

		public OptionsScaleR(OptionsBase owner)
		{
			this.Owner = owner;
		}

		public PointLabel PointLabel
		{
			get
			{
				return this._pointLabel;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				this._pointLabel = value;
				Update();
			}
		}
		private PointLabel _pointLabel = new PointLabel();
	}

	[Browsable(true)]
	public class PointLabel : OptionsBase
	{
		public PointLabel()
		{

		}

		[Browsable(true)]
		public bool Display
		{
			get { return this._display; }
			set
			{
				if (this._display != value)
				{
					this._display = value;
				}
			}
		}
		private bool _display = true;

		[Browsable(true)]
		public bool CenterPointLabels
		{
			get { return this._centerPointLabels; }
			set
			{
				if (this._centerPointLabels != value)
				{
					this._centerPointLabels = value;
				}
			}
		}
		private bool _centerPointLabels = false;

		[Browsable(true)]
		public Font Font
		{
			get { return this._font; }
			set
			{
				if (this._font != value)
				{
					this._font = value;
				}
			}
		}
		private Font _font;
	}
}


