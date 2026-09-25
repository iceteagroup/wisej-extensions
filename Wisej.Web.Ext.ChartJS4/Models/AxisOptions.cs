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

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Wisej.Web.Ext.ChartJS4.Models
{
	/// <summary>
	/// Represents the configuration of a chart axis (scale), serialized as a Chart.js <c>scales[id]</c> entry.
	/// </summary>
	/// <remarks>
	/// Instances are lazily created by <see cref="ScalesOptions.X"/> and <see cref="ScalesOptions.Y"/>.
	/// The nested <see cref="Grid"/>, <see cref="Border"/>, <see cref="Title"/> and <see cref="Ticks"/>
	/// options are also created on first access. Scale options not exposed as typed properties
	/// (e.g. <c>grace</c>, <c>bounds</c>, <c>time</c>) can be supplied through <see cref="ExtensionData"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var y = chart.ChartOptions.Scales.Y;
	/// y.Type = "linear";
	/// y.Position = "right";
	/// y.SuggestedMin = 0;
	/// y.SuggestedMax = 100;
	/// y.Title.Display = true;
	/// y.Title.Text = "Percent";
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class AxisOptions : OptionsBase
	{
		private string? _type;
		private bool _display = true;
		private object? _position;
		private string? _stack;
		private int _weight;
		private GridOptions? _grid;
		private BorderOptions? _border;
		private AxisTitleOptions? _title;
		private TickOptions? _ticks;
		private double? _min;
		private double? _max;
		private double? _suggestedMin;
		private double? _suggestedMax;
		private object? _stacked;
		private bool _reverse;
		private bool _offset;

		/// <summary>
		/// Returns or sets the type of scale being employed (Chart.js <c>scales[id].type</c>).
		/// </summary>
		/// <value>
		/// One of <c>"linear"</c>, <c>"logarithmic"</c>, <c>"category"</c>, <c>"time"</c>, <c>"timeseries"</c>
		/// or <c>"radialLinear"</c>, or <c>null</c> (default) to let Chart.js pick the scale type from the chart type.
		/// </value>
		/// <remarks>
		/// The <c>"time"</c> and <c>"timeseries"</c> scales require a date adapter to be loaded in the browser.
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Type = "logarithmic";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("type")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Type of scale being employed.")]
		public string? Type
		{
			get => _type;
			set => SetProperty(ref _type, value);
		}

		/// <summary>
		/// Returns or sets a value indicating whether the axis is displayed (Chart.js <c>scales[id].display</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to display the axis; otherwise, <c>false</c>. The default is <c>true</c>.
		/// </value>
		/// <remarks>
		/// When hidden, the axis is still used to scale the data. Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// bool axisVisible = chart.ChartOptions.Scales.X.Display;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Display the axis?")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Returns or sets the position of the axis (Chart.js <c>scales[id].position</c>).
		/// </summary>
		/// <value>
		/// One of <c>"top"</c>, <c>"left"</c>, <c>"bottom"</c>, <c>"right"</c> or <c>"center"</c>; an object
		/// such as <c>{ x: 0 }</c> to position the axis at a data value of another axis; or <c>null</c> (default)
		/// to use the Chart.js default position.
		/// </value>
		/// <remarks>
		/// The property is typed <see cref="object"/> so that both string positions and value objects can be assigned.
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Position = "top";
		/// chart.ChartOptions.Scales.Y.Position = new Dictionary<string, object> { { "x", 0 } };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("position")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Position of the axis.")]
		public object? Position
		{
			get => _position;
			set => SetProperty(ref _position, value);
		}

		/// <summary>
		/// Returns or sets the stack group of the axis (Chart.js <c>scales[id].stack</c>). Axes on the same
		/// position with the same stack group are stacked together.
		/// </summary>
		/// <value>
		/// The stack group identifier, or <c>null</c> (default) for no stacking of axes.
		/// </value>
		/// <remarks>
		/// Use <see cref="Weight"/> to order stacked axes. Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Stack = "demo";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("stack")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Stack group identifier.")]
		public string? Stack
		{
			get => _stack;
			set => SetProperty(ref _stack, value);
		}

		/// <summary>
		/// Returns or sets the weight used to sort the axis (Chart.js <c>scales[id].weight</c>). Higher weights
		/// are further away from the chart area.
		/// </summary>
		/// <value>
		/// The sort weight. The default is <c>0</c>.
		/// </value>
		/// <remarks>
		/// Also determines the order of axes within the same <see cref="Stack"/> group. Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Weight = 1;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("weight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Axis weight for sorting.")]
		public int Weight
		{
			get => _weight;
			set => SetProperty(ref _weight, value);
		}

		/// <summary>
		/// Returns or sets the grid line options of the axis (Chart.js <c>scales[id].grid</c>).
		/// </summary>
		/// <value>
		/// A <see cref="GridOptions"/> instance. The getter lazily creates a default instance when none is set.
		/// </value>
		/// <remarks>
		/// Changing any of the grid values refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Grid.Color = "rgba(0, 0, 0, 0.05)";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("grid")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Grid line configuration.")]
		public GridOptions? Grid
		{
			get
			{
				if (_grid == null)
					_grid = new GridOptions { Chart = Chart };
				return _grid;
			}
			set => SetProperty(ref _grid, value);
		}

		/// <summary>
		/// Returns or sets the options of the axis border line, drawn between the axis and the chart area
		/// (Chart.js <c>scales[id].border</c>).
		/// </summary>
		/// <value>
		/// A <see cref="BorderOptions"/> instance. The getter lazily creates a default instance when none is set.
		/// </value>
		/// <remarks>
		/// Changing any of the border values refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Border.Color = System.Drawing.Color.Black;
		/// chart.ChartOptions.Scales.X.Border.Width = 2;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("border")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Border configuration.")]
		public BorderOptions? Border
		{
			get
			{
				if (_border == null)
					_border = new BorderOptions { Chart = Chart };
				return _border;
			}
			set => SetProperty(ref _border, value);
		}

		/// <summary>
		/// Returns or sets the options of the axis title (Chart.js <c>scales[id].title</c>).
		/// </summary>
		/// <value>
		/// An <see cref="AxisTitleOptions"/> instance. The getter lazily creates a default instance when none is set.
		/// </value>
		/// <remarks>
		/// The axis title is hidden by default; set <see cref="AxisTitleOptions.Display"/> to <c>true</c> to show it.
		/// Changing any of the title values refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Title.Display = true;
		/// chart.ChartOptions.Scales.X.Title.Text = "Year";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("title")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Axis title configuration.")]
		public AxisTitleOptions? Title
		{
			get
			{
				if (_title == null)
					_title = new AxisTitleOptions { Chart = Chart };
				return _title;
			}
			set => SetProperty(ref _title, value);
		}

		/// <summary>
		/// Returns or sets the options of the axis tick labels (Chart.js <c>scales[id].ticks</c>).
		/// </summary>
		/// <value>
		/// A <see cref="TickOptions"/> instance. The getter lazily creates a default instance when none is set.
		/// </value>
		/// <remarks>
		/// Changing any of the tick values refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Ticks.MaxRotation = 0;
		/// chart.ChartOptions.Scales.Y.Ticks.Color = "#999999";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("ticks")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Tick configuration.")]
		public TickOptions? Ticks
		{
			get
			{
				if (_ticks == null)
					_ticks = new TickOptions { Chart = Chart };
				return _ticks;
			}
			set => SetProperty(ref _ticks, value);
		}

		/// <summary>
		/// Returns or sets the minimum value of the scale (Chart.js <c>scales[id].min</c>). This user-defined
		/// value overrides the minimum value calculated from the data.
		/// </summary>
		/// <value>
		/// The minimum value, or <c>null</c> (default) to calculate it from the data.
		/// </value>
		/// <remarks>
		/// Use <see cref="SuggestedMin"/> instead to extend the range without clipping data.
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Min = 0;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("min")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Minimum value for the scale.")]
		public double? Min
		{
			get => _min;
			set => SetProperty(ref _min, value);
		}

		/// <summary>
		/// Returns or sets the maximum value of the scale (Chart.js <c>scales[id].max</c>). This user-defined
		/// value overrides the maximum value calculated from the data.
		/// </summary>
		/// <value>
		/// The maximum value, or <c>null</c> (default) to calculate it from the data.
		/// </value>
		/// <remarks>
		/// Use <see cref="SuggestedMax"/> instead to extend the range without clipping data.
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Max = 100;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("max")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Maximum value for the scale.")]
		public double? Max
		{
			get => _max;
			set => SetProperty(ref _max, value);
		}

		/// <summary>
		/// Returns or sets the suggested minimum value of the scale (Chart.js <c>scales[id].suggestedMin</c>).
		/// It is used in the calculation of the scale minimum, but data values below it still extend the scale.
		/// </summary>
		/// <value>
		/// The suggested minimum value, or <c>null</c> (default) for none.
		/// </value>
		/// <remarks>
		/// Unlike <see cref="Min"/>, this value never clips the data. Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.SuggestedMin = -10;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("suggestedMin")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Suggested minimum value.")]
		public double? SuggestedMin
		{
			get => _suggestedMin;
			set => SetProperty(ref _suggestedMin, value);
		}

		/// <summary>
		/// Returns or sets the suggested maximum value of the scale (Chart.js <c>scales[id].suggestedMax</c>).
		/// It is used in the calculation of the scale maximum, but data values above it still extend the scale.
		/// </summary>
		/// <value>
		/// The suggested maximum value, or <c>null</c> (default) for none.
		/// </value>
		/// <remarks>
		/// Unlike <see cref="Max"/>, this value never clips the data. Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.SuggestedMax = 100;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("suggestedMax")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Suggested maximum value.")]
		public double? SuggestedMax
		{
			get => _suggestedMax;
			set => SetProperty(ref _suggestedMax, value);
		}

		/// <summary>
		/// Returns or sets a value indicating whether the data of the datasets is stacked on this axis
		/// (Chart.js <c>scales[id].stacked</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to stack the datasets, <c>false</c> to not stack them, the string <c>"single"</c> to stack
		/// only the datasets with the same stack key, or <c>null</c> (default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// For stacked bar charts set it on both the X and Y axes. The property is typed <see cref="object"/>
		/// so that both <see cref="bool"/> values and <c>"single"</c> can be assigned. Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Stacked = true;
		/// chart.ChartOptions.Scales.Y.Stacked = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("stacked")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Enable stacking. Accepts bool or \"single\".")]
		public object? Stacked
		{
			get => _stacked;
			set => SetProperty(ref _stacked, value);
		}

		/// <summary>
		/// Returns or sets a value indicating whether the scale is reversed (Chart.js <c>scales[id].reverse</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to reverse the order of the scale values; otherwise, <c>false</c>. The default is <c>false</c>.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Reverse = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("reverse")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Reverse the scale.")]
		public bool Reverse
		{
			get => _reverse;
			set => SetProperty(ref _reverse, value);
		}

		/// <summary>
		/// Returns or sets a value indicating whether extra space is added to both edges of the axis so that
		/// it is scaled to fit into the chart area (Chart.js <c>scales[id].offset</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to add the offset; otherwise, <c>false</c>. The default is <c>false</c>.
		/// </value>
		/// <remarks>
		/// Chart.js enables the offset by default on the category scale of bar charts.
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Offset = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("offset")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Add offset to edges.")]
		public bool Offset
		{
			get => _offset;
			set => SetProperty(ref _offset, value);
		}

		/// <summary>
		/// Returns or sets additional, arbitrary Chart.js scale options that are not exposed as typed properties.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c> (default).
		/// </value>
		/// <remarks>
		/// This property is marked with <c>[JsonExtensionData]</c>: each entry is written as a sibling
		/// property of the scale JSON object sent to Chart.js (e.g. <c>grace</c>, <c>bounds</c>, <c>time</c>,
		/// <c>beginAtZero</c>). It is hidden from the designer and property grid.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.ExtensionData = new Dictionary<string, object>
		/// {
		///     { "beginAtZero", true },
		///     { "grace", "5%" }
		/// };
		/// ]]></code>
		/// </example>
		[JsonExtensionData]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <inheritdoc/>
		protected override void OnChartChanged()
		{
			if (_grid != null)
				_grid.Chart = Chart;
			if (_border != null)
				_border.Chart = Chart;
			if (_title != null)
				_title.Chart = Chart;
			if (_ticks != null)
				_ticks.Chart = Chart;
		}

		/// <summary>
		/// Determines whether the <see cref="Type"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Type"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetType"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeType())
		///     axis.ResetType();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeType() => Type != null;

		/// <summary>
		/// Resets the <see cref="Type"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions { Type = "logarithmic" };
		/// axis.ResetType();
		/// ]]></code>
		/// </example>
		public void ResetType() => Type = null;

		/// <summary>
		/// Determines whether the <see cref="Display"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Display"/> is not <c>true</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetDisplay"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeDisplay())
		///     axis.ResetDisplay();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDisplay() => Display != true;

		/// <summary>
		/// Resets the <see cref="Display"/> property to its default value (<c>true</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// axis.ResetDisplay();
		/// ]]></code>
		/// </example>
		public void ResetDisplay() => Display = true;

		/// <summary>
		/// Determines whether the <see cref="Position"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Position"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetPosition"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializePosition())
		///     axis.ResetPosition();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePosition() => Position != null;

		/// <summary>
		/// Resets the <see cref="Position"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions { Position = "right" };
		/// axis.ResetPosition();
		/// ]]></code>
		/// </example>
		public void ResetPosition() => Position = null;

		/// <summary>
		/// Determines whether the <see cref="Stack"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Stack"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetStack"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeStack())
		///     axis.ResetStack();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeStack() => Stack != null;

		/// <summary>
		/// Resets the <see cref="Stack"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions { Stack = "demo" };
		/// axis.ResetStack();
		/// ]]></code>
		/// </example>
		public void ResetStack() => Stack = null;

		/// <summary>
		/// Determines whether the <see cref="Weight"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Weight"/> is not <c>0</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetWeight"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeWeight())
		///     axis.ResetWeight();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeWeight() => Weight != default;

		/// <summary>
		/// Resets the <see cref="Weight"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions { Weight = 2 };
		/// axis.ResetWeight();
		/// ]]></code>
		/// </example>
		public void ResetWeight() => Weight = default;

		/// <summary>
		/// Determines whether the <see cref="Grid"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Grid"/> has at least one non-default value; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetGrid"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeGrid())
		///     axis.ResetGrid();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeGrid() => Grid != null && !Grid.IsDefault;

		/// <summary>
		/// Resets the <see cref="Grid"/> property to its default value by discarding the current <see cref="GridOptions"/> instance.
		/// </summary>
		/// <remarks>
		/// A new default instance is created the next time <see cref="Grid"/> is read.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// axis.Grid.LineWidth = 2;
		/// axis.ResetGrid();
		/// ]]></code>
		/// </example>
		public void ResetGrid() => SetProperty(ref _grid, null);

		/// <summary>
		/// Determines whether the <see cref="Border"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Border"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetBorder"/>. Because the
		/// <see cref="Border"/> getter lazily creates an instance, this method currently always returns <c>true</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeBorder())
		///     axis.ResetBorder();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBorder() => Border != null;

		/// <summary>
		/// Resets the <see cref="Border"/> property to its default value by discarding the current <see cref="BorderOptions"/> instance.
		/// </summary>
		/// <remarks>
		/// A new default instance is created the next time <see cref="Border"/> is read.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// axis.Border.Width = 2;
		/// axis.ResetBorder();
		/// ]]></code>
		/// </example>
		public void ResetBorder() => SetProperty(ref _border, null);

		/// <summary>
		/// Determines whether the <see cref="Title"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Title"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetTitle"/>. Because the
		/// <see cref="Title"/> getter lazily creates an instance, this method currently always returns <c>true</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeTitle())
		///     axis.ResetTitle();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTitle() => Title != null;

		/// <summary>
		/// Resets the <see cref="Title"/> property to its default value by discarding the current <see cref="AxisTitleOptions"/> instance.
		/// </summary>
		/// <remarks>
		/// A new default instance is created the next time <see cref="Title"/> is read.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// axis.Title.Text = "Sales";
		/// axis.ResetTitle();
		/// ]]></code>
		/// </example>
		public void ResetTitle() => SetProperty(ref _title, null);

		/// <summary>
		/// Determines whether the <see cref="Ticks"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Ticks"/> has at least one non-default value; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetTicks"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeTicks())
		///     axis.ResetTicks();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTicks() => Ticks != null && !Ticks.IsDefault;

		/// <summary>
		/// Resets the <see cref="Ticks"/> property to its default value by discarding the current <see cref="TickOptions"/> instance.
		/// </summary>
		/// <remarks>
		/// A new default instance is created the next time <see cref="Ticks"/> is read.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// axis.Ticks.MaxRotation = 0;
		/// axis.ResetTicks();
		/// ]]></code>
		/// </example>
		public void ResetTicks() => SetProperty(ref _ticks, null);

		/// <summary>
		/// Determines whether the <see cref="Min"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Min"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetMin"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeMin())
		///     axis.ResetMin();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeMin() => Min != null;

		/// <summary>
		/// Resets the <see cref="Min"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions { Min = 0 };
		/// axis.ResetMin();
		/// ]]></code>
		/// </example>
		public void ResetMin() => Min = null;

		/// <summary>
		/// Determines whether the <see cref="Max"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Max"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetMax"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeMax())
		///     axis.ResetMax();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeMax() => Max != null;

		/// <summary>
		/// Resets the <see cref="Max"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions { Max = 100 };
		/// axis.ResetMax();
		/// ]]></code>
		/// </example>
		public void ResetMax() => Max = null;

		/// <summary>
		/// Determines whether the <see cref="SuggestedMin"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="SuggestedMin"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetSuggestedMin"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeSuggestedMin())
		///     axis.ResetSuggestedMin();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeSuggestedMin() => SuggestedMin != null;

		/// <summary>
		/// Resets the <see cref="SuggestedMin"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions { SuggestedMin = -10 };
		/// axis.ResetSuggestedMin();
		/// ]]></code>
		/// </example>
		public void ResetSuggestedMin() => SuggestedMin = null;

		/// <summary>
		/// Determines whether the <see cref="SuggestedMax"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="SuggestedMax"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetSuggestedMax"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeSuggestedMax())
		///     axis.ResetSuggestedMax();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeSuggestedMax() => SuggestedMax != null;

		/// <summary>
		/// Resets the <see cref="SuggestedMax"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions { SuggestedMax = 100 };
		/// axis.ResetSuggestedMax();
		/// ]]></code>
		/// </example>
		public void ResetSuggestedMax() => SuggestedMax = null;

		/// <summary>
		/// Determines whether the <see cref="Stacked"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Stacked"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetStacked"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeStacked())
		///     axis.ResetStacked();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeStacked() => Stacked != null;

		/// <summary>
		/// Resets the <see cref="Stacked"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions { Stacked = true };
		/// axis.ResetStacked();
		/// ]]></code>
		/// </example>
		public void ResetStacked() => Stacked = null;

		/// <summary>
		/// Determines whether the <see cref="Reverse"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Reverse"/> is <c>true</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetReverse"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeReverse())
		///     axis.ResetReverse();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeReverse() => Reverse != false;

		/// <summary>
		/// Resets the <see cref="Reverse"/> property to its default value (<c>false</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions { Reverse = true };
		/// axis.ResetReverse();
		/// ]]></code>
		/// </example>
		public void ResetReverse() => Reverse = false;

		/// <summary>
		/// Determines whether the <see cref="Offset"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Offset"/> is <c>true</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetOffset"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions();
		/// if (axis.ShouldSerializeOffset())
		///     axis.ResetOffset();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeOffset() => Offset != false;

		/// <summary>
		/// Resets the <see cref="Offset"/> property to its default value (<c>false</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var axis = new AxisOptions { Offset = true };
		/// axis.ResetOffset();
		/// ]]></code>
		/// </example>
		public void ResetOffset() => Offset = false;

	}
}
