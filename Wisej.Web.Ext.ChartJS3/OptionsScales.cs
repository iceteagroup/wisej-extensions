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

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// Represents the options for the axes (scales) of the chart.
	/// </summary>
	/// <remarks>
	/// The axes are exposed as the arrays <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScales.xAxes"/> and
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScales.yAxes"/> to support charts with multiple axes. Chart.js 3 expects
	/// <c>options.scales</c> to be an object keyed by the scale id: the client script converts the arrays
	/// into that object using the keys "x0", "x1", ... for the x-axes and "y0", "y1", ... for the y-axes.
	/// An instance is available through the <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/> property of the chart options.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var scales = this.chartJS31.Options.Scales;
	/// scales.xAxes[0].Title.Text = "Month";
	/// scales.yAxes[0].Title.Text = "Revenue";
	/// scales.yAxes[0].Min = 0;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS3")]
	public class OptionsScales : OptionsBase
	{

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a set of scale options that is not attached to any chart yet. It is attached when
		/// assigned to <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var scales = new OptionsScales();
		/// scales.yAxes[0].Max = 100;
		/// this.chartJS31.Options.Scales = scales;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionsScales()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsScales"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> set of options that owns this instance.</param>
		/// <exception cref="T:System.InvalidOperationException">Thrown by the <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Scales"/> setter when the new instance is owned by a different set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var options = this.chartJS31.Options;
		/// var scales = new OptionsScales(options);
		/// scales.xAxes[0].Display = false;
		/// options.Scales = scales;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionsScales(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Options for the x-axes.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:Wisej.Web.Ext.ChartJS3.OptionScalesAxesX"/> objects, one for each x-axis.
		/// When not set, the getter creates an array containing a single default x-axis.
		/// </value>
		/// <remarks>
		/// Each axis is sent to Chart.js 3 as <c>options.scales.x0</c>, <c>options.scales.x1</c>, etc., according
		/// to its index in the array. To bind a data set to a specific x-axis, set <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.xAxisID"/>
		/// to that key. Assigning a new array doesn't update the chart automatically; call
		/// <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> or the chart's Update() method to refresh it.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An axis in the array already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var xAxis = this.chartJS31.Options.Scales.xAxes[0];
		/// xAxis.Position = HeaderPosition.Top;
		/// xAxis.Grid.Display = false;
		/// xAxis.Title.Text = "Quarter";
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
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
		/// <value>
		/// An array of <see cref="T:Wisej.Web.Ext.ChartJS3.OptionScalesAxesY"/> objects, one for each y-axis.
		/// When not set, the getter creates an array containing a single default y-axis.
		/// </value>
		/// <remarks>
		/// Each axis is sent to Chart.js 3 as <c>options.scales.y0</c>, <c>options.scales.y1</c>, etc., according
		/// to its index in the array. To bind a data set to a specific y-axis, set <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.yAxisID"/>
		/// to that key. Assigning a new array doesn't update the chart automatically; call
		/// <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> or the chart's Update() method to refresh it.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An axis in the array already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var scales = this.chartJS31.Options.Scales;
		/// scales.yAxes = new[]
		/// {
		/// 	new OptionScalesAxesY { Position = HeaderPosition.Left },
		/// 	new OptionScalesAxesY { Position = HeaderPosition.Right, Max = 100 }
		/// };
		///
		/// // the second y-axis is registered on the client as "y1".
		/// this.chartJS31.DataSets[1].yAxisID = "y1";
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
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
	}

	/// <summary>
	/// Represents the options for the scale axes.
	/// </summary>
	/// <remarks>
	/// This is the base class of <see cref="T:Wisej.Web.Ext.ChartJS3.OptionScalesAxesX"/> and
	/// <see cref="T:Wisej.Web.Ext.ChartJS3.OptionScalesAxesY"/> and maps to a single entry of the Chart.js 3
	/// <c>options.scales</c> object.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// foreach (OptionScalesAxes axis in this.chartJS31.Options.Scales.yAxes)
	/// {
	/// 	axis.Grid.Display = false;
	/// 	axis.Ticks.Color = Color.Gray;
	/// }
	/// this.chartJS31.Update();
	/// ]]></code>
	/// </example>
	public abstract class OptionScalesAxes : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// <see cref="T:Wisej.Web.Ext.ChartJS3.OptionScalesAxes"/> is abstract; this constructor is invoked by the constructors
		/// of the derived axis classes.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // OptionScalesAxes is abstract; create one of the derived axis classes.
		/// OptionScalesAxes axis = new OptionScalesAxesY();
		/// axis.Stacked = true;
		/// this.chartJS31.Options.Scales.yAxes = new[] { (OptionScalesAxesY)axis };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionScalesAxes()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionScalesAxes"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> set of options that owns this instance.</param>
		/// <remarks>
		/// When the owner belongs to a chart of type <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Pie"/>,
		/// <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Radar"/>, <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Doughnut"/>
		/// or <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.PolarArea"/>, <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Display"/>
		/// is initialized to false.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var scales = this.chartJS31.Options.Scales;
		/// OptionScalesAxes axis = new OptionScalesAxesX(scales);
		/// axis.Display = true;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the background color of the scale area.
		/// </summary>
		/// <value>
		/// Default is <see cref="F:System.Drawing.Color.Empty"/>.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>backgroundColor</c> option of the scale. Changing this property doesn't
		/// update the chart automatically; call <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var yAxis = this.chartJS31.Options.Scales.yAxes[0];
		/// yAxis.BackgroundColor = Color.FromArgb(20, Color.SteelBlue);
		/// yAxis.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[Description("Background color of the scale area.")]
		public Color BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the user defined maximum number for the scale, overrides maximum value from data.
		/// </summary>
		/// <value>
		/// Default is null: the maximum is calculated from the data.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>max</c> option of the scale. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // show percentages on a fixed 0-100 scale.
		/// var yAxis = this.chartJS31.Options.Scales.yAxes[0];
		/// yAxis.Min = 0;
		/// yAxis.Max = 100;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the user defined minimum number for the scale, overrides minimum value from data.
		/// </summary>
		/// <value>
		/// Default is null: the minimum is calculated from the data.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>min</c> option of the scale. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var yAxis = this.chartJS31.Options.Scales.yAxes[0];
		/// yAxis.Min = -50;
		/// yAxis.Max = 50;
		/// ]]></code>
		/// </example>
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
		/// <value>
		/// Default is null.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>suggestedMax</c> option of the scale. Unlike <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Max"/>,
		/// the scale extends beyond this value when the data contains a larger value.
		/// Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // the scale reaches at least 50, or more if the data requires it.
		/// this.chartJS31.Options.Scales.yAxes[0].SuggestedMax = 50;
		/// ]]></code>
		/// </example>
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
		/// <value>
		/// Default is null.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>suggestedMin</c> option of the scale. Unlike <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Min"/>,
		/// the scale extends below this value when the data contains a smaller value.
		/// Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var yAxis = this.chartJS31.Options.Scales.yAxes[0];
		/// yAxis.SuggestedMin = 0;
		/// yAxis.SuggestedMax = 10;
		/// ]]></code>
		/// </example>
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
		/// <value>
		/// An <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsAxisGrid"/> object with the grid line options.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>grid</c> option of the scale (<c>gridLines</c> in Chart.js 2).
		/// Assigning a new instance doesn't update the chart automatically; call the chart's Update() method to refresh it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var grid = this.chartJS31.Options.Scales.yAxes[0].Grid;
		/// grid.DrawBorder = false;
		/// grid.BorderDash = new[] { 4, 4 };
		/// grid.Color = new[] { Color.LightGray };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionsAxisGrid Grid
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
		private OptionsAxisGrid _gridLines = new OptionsAxisGrid();

		/// <summary>
		/// If true, show the scale including grid lines, ticks, and labels.
		/// </summary>
		/// <value>
		/// Default is true. It is initialized to false for pie, radar, doughnut and polar area charts.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>display</c> option of the scale. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // hide the x-axis completely.
		/// this.chartJS31.Options.Scales.xAxes[0].Display = false;
		/// ]]></code>
		/// </example>
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
		/// Used to identify the scale options for multi-axes charts.
		/// </summary>
		/// <value>
		/// Default is an empty string.
		/// </value>
		/// <remarks>
		/// The value is serialized as the <c>id</c> field of the scale, but Chart.js 3 identifies a scale by its key in
		/// <c>options.scales</c>, which the client script generates from the axis index ("x0", "x1", ... and "y0", "y1", ...).
		/// Use those keys in <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.xAxisID"/> and <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.yAxisID"/> to bind a data set
		/// to an axis. Changing this property doesn't update the chart automatically.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var scales = this.chartJS31.Options.Scales;
		/// scales.yAxes = new[] { new OptionScalesAxesY(), new OptionScalesAxesY() };
		/// scales.yAxes[0].Id = "amount";
		/// scales.yAxes[1].Id = "percent";
		/// scales.yAxes[1].Position = HeaderPosition.Right;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
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
		private string _id = "";

		/// <summary>
		/// Options for the chart ticks on the axes.
		/// </summary>
		/// <value>
		/// An <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsScalesTicks"/> object, created on first access.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>ticks</c> option of the scale. Assigning a new instance doesn't update
		/// the chart automatically; call the chart's Update() method to refresh it.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The value already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = this.chartJS31.Options.Scales.yAxes[0].Ticks;
		/// ticks.StepSize = 25;
		/// ticks.Color = Color.DimGray;
		/// ]]></code>
		/// </example>
		[Description("Options for the chart ticks on the axes.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsScalesTicks Ticks
		{
			get
			{
				if (this._ticks == null)
					this._ticks = new OptionsScalesTicks(this);

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
		private OptionsScalesTicks _ticks;

		/// <summary>
		/// Options for the title displayed along the axis.
		/// </summary>
		/// <value>
		/// An <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsScaleTitle"/> object, created on first access.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>title</c> option of the scale (<c>scaleLabel</c> in Chart.js 2). The title is
		/// shown only when <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScaleTitle.Text"/> is not empty. Assigning a new instance
		/// doesn't update the chart automatically; call the chart's Update() method to refresh it.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The value already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var title = this.chartJS31.Options.Scales.xAxes[0].Title;
		/// title.Text = "Day of the week";
		/// title.Color = Color.DarkBlue;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the type of scale being employed.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS3.ScaleType"/> values. Default is <see cref="F:Wisej.Web.Ext.ChartJS3.ScaleType.Linear"/>.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>type</c> option of the scale. When the type is
		/// <see cref="F:Wisej.Web.Ext.ChartJS3.ScaleType.Time"/>, the <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Time"/> options are also used.
		/// Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // use a logarithmic y-axis.
		/// this.chartJS31.Options.Scales.yAxes[0].Type = ScaleType.Logarithmic;
		/// ]]></code>
		/// </example>
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
		/// <value>
		/// Default is false.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>stacked</c> option of the scale. To create a stacked bar chart,
		/// set this property to true on both the x-axis and the y-axis. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.ChartType = ChartType.Bar;
		/// var scales = this.chartJS31.Options.Scales;
		/// scales.xAxes[0].Stacked = true;
		/// scales.yAxes[0].Stacked = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("When true, the bars are stacked.")]
		public bool Stacked
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
		private bool _stacked = false;

		/// <summary>
		/// Options for the time scale type - ignored if not a time scale type.
		/// </summary>
		/// <value>
		/// A <see cref="T:Wisej.Web.Ext.ChartJS3.ScaleTime"/> object, created on first access.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>time</c> option of the scale. These options are used only when
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Type"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ScaleType.Time"/>; the designer
		/// serializes them only in that case. Assigning a new instance updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var xAxis = this.chartJS31.Options.Scales.xAxes[0];
		/// xAxis.Type = ScaleType.Time;
		/// xAxis.Time.Unit = TimeScaleTimeUnit.month;
		/// xAxis.Time.TooltipFormat = "MMM YYYY";
		/// ]]></code>
		/// </example>
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
		/// <value>
		/// Default is 0.9. A value of 1.0 makes the bars take the whole category width.
		/// </value>
		/// <remarks>
		/// Serialized as the <c>barPercentage</c> option of the scale. Note that Chart.js 3 reads <c>barPercentage</c>
		/// from the bar data set options instead of the scale options.
		/// Changing this property doesn't update the chart automatically; call
		/// <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var xAxis = this.chartJS31.Options.Scales.xAxes[0];
		/// xAxis.BarPercentage = 1.0F;
		/// xAxis.CategoryPercentage = 1.0F;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
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
		/// <value>
		/// Default is 0.8.
		/// </value>
		/// <remarks>
		/// Serialized as the <c>categoryPercentage</c> option of the scale. Note that Chart.js 3 reads
		/// <c>categoryPercentage</c> from the bar data set options instead of the scale options.
		/// Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // leave more space between the groups of bars.
		/// this.chartJS31.Options.Scales.xAxes[0].CategoryPercentage = 0.5F;
		/// ]]></code>
		/// </example>
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
	/// <remarks>
	/// The default <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxesX.Type"/> of an x-axis is
	/// <see cref="F:Wisej.Web.Ext.ChartJS3.ScaleType.Category"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var xAxis = new OptionScalesAxesX();
	/// xAxis.Position = HeaderPosition.Top;
	/// xAxis.Title.Text = "Product";
	/// this.chartJS31.Options.Scales.xAxes = new[] { xAxis };
	/// this.chartJS31.Update();
	/// ]]></code>
	/// </example>
	public class OptionScalesAxesX : OptionScalesAxes
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Initializes <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxesX.Type"/> to <see cref="F:Wisej.Web.Ext.ChartJS3.ScaleType.Category"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var xAxis = new OptionScalesAxesX { Type = ScaleType.Time };
		/// xAxis.Time.Unit = TimeScaleTimeUnit.day;
		/// this.chartJS31.Options.Scales.xAxes = new[] { xAxis };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionScalesAxesX()
		{
			this.Type = ScaleType.Category;
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionScalesAxesX"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> set of options that owns this instance.</param>
		/// <remarks>
		/// Initializes <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxesX.Type"/> to <see cref="F:Wisej.Web.Ext.ChartJS3.ScaleType.Category"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var scales = this.chartJS31.Options.Scales;
		/// var xAxis = new OptionScalesAxesX(scales);
		/// xAxis.Grid.Display = false;
		/// scales.xAxes = new[] { xAxis };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionScalesAxesX(OptionsBase owner)
			: base(owner)
		{
			this.Type = ScaleType.Category;
		}

		/// <summary>
		/// Returns or sets the position of the X axes scale.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.HeaderPosition"/> values. Default is <see cref="F:Wisej.Web.HeaderPosition.Bottom"/>.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>position</c> option of the scale. Use Top or Bottom for an x-axis.
		/// Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Scales.xAxes[0].Position = HeaderPosition.Top;
		/// ]]></code>
		/// </example>
		[DefaultValue(HeaderPosition.Bottom)]
		[Description("Position of the x axes scale.")]
		public HeaderPosition Position
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
		private HeaderPosition _position = HeaderPosition.Bottom;

		/// <summary>
		/// Returns or sets the type of scale being employed.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS3.ScaleType"/> values. Default is <see cref="F:Wisej.Web.Ext.ChartJS3.ScaleType.Category"/>.
		/// </value>
		/// <remarks>
		/// With <see cref="F:Wisej.Web.Ext.ChartJS3.ScaleType.Category"/> the axis labels are taken from
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Labels"/>. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.ChartType = ChartType.Scatter;
		/// this.chartJS31.Options.Scales.xAxes[0].Type = ScaleType.Linear;
		/// ]]></code>
		/// </example>
		[DefaultValue(ScaleType.Category)]
		[Description("Type of scale being employed")]
		public new ScaleType Type
		{
			get { return base.Type; }
			set { base.Type = value; }
		}

		/// <summary>
		/// Returns or sets the rotation of the X Axis labels, in degrees.
		/// </summary>
		/// <value>
		/// Default is 0.
		/// </value>
		/// <remarks>
		/// This is a helper property: setting a value greater than 0 sets both <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScalesTicks.MinRotation"/>
		/// and <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScalesTicks.MaxRotation"/> of <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Ticks"/>
		/// to the value, so that all labels are rotated by the same angle. Any value other than 0 turns off
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScalesTicks.AutoSkip"/>; 0 turns it back on but leaves the rotation values unchanged.
		/// Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // rotate the category labels by 45 degrees.
		/// this.chartJS31.ChartType = ChartType.Bar;
		/// this.chartJS31.Options.Scales.xAxes[0].LabelRotation = 45;
		/// ]]></code>
		/// </example>
		[Description("Rotation value of the X Axis")]
		public int LabelRotation
		{
			get { return this._labelRotation; }
			set
			{
				if (value != this._labelRotation)
				{
					this.Ticks.MaxRotation = value > 0 ? value : this.Ticks.MaxRotation;
					this.Ticks.MinRotation = value > 0 ? value : this.Ticks.MinRotation;
					this.Ticks.AutoSkip = value == 0 ? true : false;

					this._labelRotation = value;
					Update();
				}
			}
		}

		private int _labelRotation;
	}

	/// <summary>
	/// Represents the options for the scale Y axes.
	/// </summary>
	/// <remarks>
	/// The default <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Type"/> of a y-axis is
	/// <see cref="F:Wisej.Web.Ext.ChartJS3.ScaleType.Linear"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var yAxis = new OptionScalesAxesY { Position = HeaderPosition.Right, SuggestedMin = 0 };
	/// yAxis.Title.Text = "Units sold";
	/// this.chartJS31.Options.Scales.yAxes = new[] { yAxis };
	/// this.chartJS31.Update();
	/// ]]></code>
	/// </example>
	public class OptionScalesAxesY : OptionScalesAxes
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var yAxis = new OptionScalesAxesY();
		/// yAxis.Title.Text = "Temperature (°C)";
		/// this.chartJS31.Options.Scales.yAxes = new[] { yAxis };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionScalesAxesY()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionScalesAxesY"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> set of options that owns this instance.</param>
		/// <example>
		/// <code><![CDATA[
		/// var scales = this.chartJS31.Options.Scales;
		/// var yAxis = new OptionScalesAxesY(scales);
		/// yAxis.Ticks.Mirror = true;
		/// scales.yAxes = new[] { yAxis };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionScalesAxesY(OptionsBase owner)
					: base(owner)
		{
		}

		/// <summary>
		/// Returns or sets the position of the Y axes scale.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.HeaderPosition"/> values. Default is <see cref="F:Wisej.Web.HeaderPosition.Left"/>.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>position</c> option of the scale. Use Left or Right for a y-axis.
		/// Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Scales.yAxes[0].Position = HeaderPosition.Right;
		/// ]]></code>
		/// </example>
		[DefaultValue(HeaderPosition.Left)]
		[Description("Position of the Y axes scale.")]
		public HeaderPosition Position
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
		private HeaderPosition _position = HeaderPosition.Left;

		/// <summary>
		/// Returns or sets the rotation of the Y Axis labels, in degrees.
		/// </summary>
		/// <value>
		/// Default is 0.
		/// </value>
		/// <remarks>
		/// This is a helper property: setting a value greater than 0 sets both <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScalesTicks.MinRotation"/>
		/// and <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScalesTicks.MaxRotation"/> of <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Ticks"/>
		/// to the value. Any value other than 0 turns off <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScalesTicks.AutoSkip"/>; 0 turns it
		/// back on but leaves the rotation values unchanged. Chart.js 3 applies the tick rotation only to horizontal
		/// scales, so on a vertical y-axis only the <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScalesTicks.AutoSkip"/> change has an effect.
		/// Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // horizontal bar chart: the y-axis is the category axis.
		/// this.chartJS31.Options.IndexAxis = "y";
		/// this.chartJS31.Options.Scales.yAxes[0].LabelRotation = 30;
		/// ]]></code>
		/// </example>
		[Description("Rotation value of the Y Axis")]
		public int LabelRotation
		{
			get { return this._labelRotation; }
			set
			{
				if (value != this._labelRotation)
				{
					this.Ticks.MaxRotation = value > 0 ? value : this.Ticks.MaxRotation;
					this.Ticks.MinRotation = value > 0 ? value : this.Ticks.MinRotation;
					this.Ticks.AutoSkip = value == 0 ? true : false;

					this._labelRotation = value;
					Update();
				}
			}
		}

		private int _labelRotation;
	}

	/// <summary>
	/// Represents the options for the ticks element of the axes.
	/// </summary>
	/// <remarks>
	/// Maps to the Chart.js 3 <c>ticks</c> option of a scale. Use the
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Ticks"/> property to access it.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var ticks = this.chartJS31.Options.Scales.yAxes[0].Ticks;
	/// ticks.StepSize = 10;
	/// ticks.Color = Color.SteelBlue;
	/// ]]></code>
	/// </example>
	public class OptionsScalesTicks : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new OptionsScalesTicks();
		/// ticks.Reverse = true;
		/// this.chartJS31.Options.Scales.yAxes[0].Ticks = ticks;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionsScalesTicks()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsScalesTicks"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> set of options that owns this instance.</param>
		/// <example>
		/// <code><![CDATA[
		/// var xAxis = this.chartJS31.Options.Scales.xAxes[0];
		/// var ticks = new OptionsScalesTicks(xAxis);
		/// ticks.AutoSkip = false;
		/// xAxis.Ticks = ticks;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionsScalesTicks(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Returns or sets the user defined fixed step size for the scale.
		/// </summary>
		/// <value>
		/// Default is null.
		/// </value>
		/// <remarks>
		/// If set, the scale ticks will be enumerated by multiple of stepSize, having one tick per increment.
		/// If not set, the ticks are labeled automatically using the nice numbers algorithm.
		/// Maps to the Chart.js 3 <c>ticks.stepSize</c> option. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var yAxis = this.chartJS31.Options.Scales.yAxes[0];
		/// yAxis.Min = 0;
		/// yAxis.Max = 100;
		/// yAxis.Ticks.StepSize = 20;
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("User defined fixed step size for the scale.")]
		public int? StepSize
		{
			get { return this._stepSize; }
			set
			{
				if (this._stepSize != value)
				{
					this._stepSize = value;
					Update();
				}
			}
		}
		private int? _stepSize;

		/// <summary>
		/// Returns or sets the padding between the tick labels and the axis, in pixels.
		/// </summary>
		/// <value>
		/// Default is 10.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>ticks.padding</c> option. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Scales.xAxes[0].Ticks.Padding = 4;
		/// ]]></code>
		/// </example>
		[DefaultValue(10)]
		[Description("Padding between the tick label and the axis. Only applicable to horizontal scales.")]
		public int Padding
		{
			get { return this._padding; }
			set
			{
				if (this._padding != value)
				{
					this._padding = value;
					Update();
				}
			}
		}
		private int _padding = 10;

		/// <summary>
		/// Flips tick labels around axis, displaying the labels inside the chart instead of outside. Only applicable to vertical scales.
		/// </summary>
		/// <value>
		/// Default is false.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>ticks.mirror</c> option. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // draw the y-axis labels inside the chart area.
		/// this.chartJS31.Options.Scales.yAxes[0].Ticks.Mirror = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("Flips tick labels around axis, displaying the labels inside the chart instead of outside. Only applicable to vertical scales.")]
		public bool Mirror
		{
			get { return this._mirror; }
			set
			{
				if (this._mirror != value)
				{
					this._mirror = value;
					Update();
				}
			}
		}
		private bool _mirror = false;

		/// <summary>
		/// Reverses order of tick labels.
		/// </summary>
		/// <value>
		/// Default is false.
		/// </value>
		/// <remarks>
		/// Serialized as <c>ticks.reverse</c>. Note that Chart.js 3 reads the <c>reverse</c> option from the
		/// scale options instead of the ticks options. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // show the highest values at the bottom.
		/// this.chartJS31.Options.Scales.yAxes[0].Ticks.Reverse = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("Reverses order of tick labels.")]
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
		/// Returns or sets the font of the tick labels.
		/// </summary>
		/// <value>
		/// When not set, returns the <see cref="P:Wisej.Web.Control.Font"/> of the chart control.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>ticks.font</c> option; the client script converts the font to the
		/// Chart.js font object (size, family and style). Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = this.chartJS31.Options.Scales.xAxes[0].Ticks;
		/// ticks.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
		/// ]]></code>
		/// </example>
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

		/// <summary>
		/// Returns or sets the color of the tick labels.
		/// </summary>
		/// <value>
		/// When not set, returns the <see cref="P:Wisej.Web.Control.ForeColor"/> of the chart control.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>ticks.color</c> option. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var scales = this.chartJS31.Options.Scales;
		/// scales.xAxes[0].Ticks.Color = Color.DarkSlateGray;
		/// scales.yAxes[0].Ticks.Color = Color.DarkSlateGray;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[Description("Tick labels color.")]
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
		private Color _color;

		/// <summary>
		/// If true, scale will include 0 if it is not already included.
		/// </summary>
		/// <value>
		/// Default is true.
		/// </value>
		/// <remarks>
		/// Serialized as <c>ticks.beginAtZero</c>. Note that Chart.js 3 reads the <c>beginAtZero</c> option from the
		/// scale options instead of the ticks options; use <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Min"/> or
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.SuggestedMin"/> to force the scale to start at 0.
		/// Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var yAxis = this.chartJS31.Options.Scales.yAxes[0];
		/// yAxis.Ticks.BeginAtZero = false;
		/// yAxis.SuggestedMin = 0;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("If true, scale will include 0 if it is not already included.")]
		public bool BeginAtZero
		{
			get { return this._beginAtZero; }
			set
			{
				if (this._beginAtZero != value)
				{
					this._beginAtZero = value;
					Update();
				}
			}
		}
		private bool _beginAtZero = true;

		/// <summary>
		/// Returns or sets the minimum rotation of the tick labels, in degrees.
		/// </summary>
		/// <value>
		/// Default is 0.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>ticks.minRotation</c> option, which applies only to horizontal scales.
		/// Changing this property doesn't update the chart automatically; call
		/// <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh it. See also <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxesX.LabelRotation"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = this.chartJS31.Options.Scales.xAxes[0].Ticks;
		/// ticks.MinRotation = 30;
		/// ticks.MaxRotation = 90;
		/// ticks.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(0)]
		[Description("Min Rotation value of the tick")]
		public int MinRotation
		{
			get { return this._minRotation; }
			set
			{
				if (this._minRotation != value)
				{
					this._minRotation = value;
				}
			}
		}
		private int _minRotation = 0;

		/// <summary>
		/// Returns or sets the maximum rotation of the tick labels, in degrees.
		/// </summary>
		/// <value>
		/// Default is 50.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>ticks.maxRotation</c> option, which applies only to horizontal scales.
		/// Labels are rotated up to this angle, when needed, before being skipped.
		/// Changing this property doesn't update the chart automatically; call
		/// <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // never rotate the labels.
		/// var ticks = this.chartJS31.Options.Scales.xAxes[0].Ticks;
		/// ticks.MaxRotation = 0;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(50)]
		[Description("Max Rotation value of the tick")]
		public int MaxRotation
		{
			get { return this._maxRotation; }
			set
			{
				if (this._maxRotation != value)
				{
					this._maxRotation = value;
				}
			}
		}

		private int _maxRotation = 50;

		/// <summary>
		/// If true, automatically calculates how many labels can be shown and hides labels accordingly.
		/// Labels will be rotated up to <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScalesTicks.MaxRotation"/> before skipping any.
		/// Turn AutoSkip off to show all labels no matter what.
		/// </summary>
		/// <value>
		/// Default is true.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>ticks.autoSkip</c> option. Changing this property doesn't update the chart
		/// automatically; call <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // show every label, even when they overlap.
		/// var ticks = this.chartJS31.Options.Scales.xAxes[0].Ticks;
		/// ticks.AutoSkip = false;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("Automatically calculates how many labels can be shown and hides labels accordingly")]
		public bool AutoSkip
		{
			get
			{
				return this._autoSkip;
			}

			set
			{
				if (value != this._autoSkip)
				{
					this._autoSkip = value;
				}
			}
		}
		private bool _autoSkip = true;
	}

	/// <summary>
	/// Represents the options for the title of an axis.
	/// </summary>
	/// <remarks>
	/// Maps to the Chart.js 3 <c>title</c> option of a scale. Use the
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Title"/> property to access it.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var title = this.chartJS31.Options.Scales.yAxes[0].Title;
	/// title.Text = "Sales (USD)";
	/// title.Font = new Font("Arial", 10F, FontStyle.Italic);
	/// ]]></code>
	/// </example>
	public class OptionsScaleTitle : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var title = new OptionsScaleTitle { Text = "Hours" };
		/// this.chartJS31.Options.Scales.xAxes[0].Title = title;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionsScaleTitle()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsScaleTitle"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> set of options that owns this instance.</param>
		/// <example>
		/// <code><![CDATA[
		/// var yAxis = this.chartJS31.Options.Scales.yAxes[0];
		/// var title = new OptionsScaleTitle(yAxis);
		/// title.Text = "Visitors";
		/// yAxis.Title = title;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionsScaleTitle(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Returns or sets the font of the axis title.
		/// </summary>
		/// <value>
		/// When not set, returns the <see cref="P:Wisej.Web.Control.Font"/> of the chart control.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>title.font</c> option of the scale; the client script converts the font to the
		/// Chart.js font object (size, family and style). Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = this.chartJS31.Options.Scales.xAxes[0].Title;
		/// title.Text = "Year";
		/// title.Font = new Font("Verdana", 11F, FontStyle.Bold);
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
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

		/// <summary>
		/// Returns or sets whether the axis title is displayed.
		/// </summary>
		/// <value>
		/// Default is true. The getter returns false when <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScaleTitle.Text"/> is empty.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>title.display</c> option of the scale. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // temporarily hide the title without clearing its text.
		/// var title = this.chartJS31.Options.Scales.yAxes[0].Title;
		/// title.Display = !title.Display;
		/// ]]></code>
		/// </example>
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

		private bool ShouldSerializeDisplay()
		{
			return !this._display;
		}

		private void ResetDisplay()
		{
			this.Display = true;
		}

		/// <summary>
		/// Returns or sets the text for the title. (i.e. "# of People" or "Response Choices").
		/// </summary>
		/// <value>
		/// Default is an empty string. Setting null stores an empty string.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>title.text</c> option of the scale. The title is displayed only when the text
		/// is not empty. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var scales = this.chartJS31.Options.Scales;
		/// scales.xAxes[0].Title.Text = "Response Choices";
		/// scales.yAxes[0].Title.Text = "# of People";
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the color of the axis title.
		/// </summary>
		/// <value>
		/// When not set, returns the <see cref="P:Wisej.Web.Control.ForeColor"/> of the chart control.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js 3 <c>title.color</c> option of the scale. Setting this property updates the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = this.chartJS31.Options.Scales.yAxes[0].Title;
		/// title.Text = "Errors";
		/// title.Color = Color.Firebrick;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
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
	}
}
