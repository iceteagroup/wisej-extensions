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
using System.Text.Json.Serialization;
using Wisej.Core;

namespace Wisej.Web.Ext.ChartJS4.Models
{
	/// <summary>
	/// Base class for all chart data sets.
	/// Represents data to be plotted on a chart with flexible serialization.
	/// </summary>
	/// <remarks>
	/// Each instance is serialized to one entry of the Chart.js <c>data.datasets</c> array.
	/// Use the specialized subclasses (<see cref="LineDataSet"/>, <see cref="BarDataSet"/>, <see cref="PieDataSet"/>,
	/// <see cref="BubbleDataSet"/>, <see cref="ScatterDataSet"/>, <see cref="RadarDataSet"/>, <see cref="PolarAreaDataSet"/>)
	/// to access type-specific options. Changing any property refreshes the owning chart.
	/// Chart.js options not exposed as properties can be added through <see cref="ExtensionData"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// chart.Labels = new[] { "Jan", "Feb", "Mar" };
	/// chart.DataSets.Add(new ChartDataSet
	/// {
	///     Label = "Sales",
	///     Data = new object[] { 10, 20, 15 },
	///     BackgroundColor = Color.SteelBlue
	/// });
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	public class ChartDataSet : ChartModelBase
	{
		private int? _order;
		private string? _stack;
		private string? _yAxisID;
		private string _label = "Dataset";
		private object[]? _data;
		private string? _type;
		private bool _hidden;
		private object? _backgroundColor;
		private object? _borderColor;
		private int _borderWidth;

		/// <summary>
		/// Returns or sets the drawing order of the dataset. Also affects order for stacking, tooltip and legend.
		/// </summary>
		/// <value>
		/// A nullable <see cref="int"/>. The default is <c>null</c> (Chart.js default <c>0</c>). Maps to the Chart.js <c>order</c> option.
		/// </value>
		/// <remarks>
		/// Datasets with a lower order are drawn on top of datasets with a higher order.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Trend", Order = 0 };
		/// var bars = new BarDataSet { Label = "Sales", Order = 1 };
		/// chart.DataSets.Add(line);
		/// chart.DataSets.Add(bars);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("order")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The drawing order of the dataset.")]
		public int? Order
		{
			get => _order;
			set => SetProperty(ref _order, value);
		}

		/// <summary>
		/// Returns or sets the ID of the group to which the dataset belongs (when stacking datasets).
		/// </summary>
		/// <value>
		/// A <see cref="string"/> identifying the stack group. The default is <c>null</c>. Maps to the Chart.js <c>stack</c> option.
		/// </value>
		/// <remarks>
		/// Datasets with the same stack ID are stacked together when the axis <see cref="AxisOptions.Stacked"/> option is enabled.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.DataSets.Add(new BarDataSet { Label = "2023", Data = new object[] { 5, 7 }, Stack = "A" });
		/// chart.DataSets.Add(new BarDataSet { Label = "2024", Data = new object[] { 6, 9 }, Stack = "A" });
		/// chart.ChartOptions.Scales.X.Stacked = true;
		/// chart.ChartOptions.Scales.Y.Stacked = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("stack")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The stack group identifier for grouped/stacked datasets.")]
		public string? Stack
		{
			get => _stack;
			set => SetProperty(ref _stack, value);
		}

		/// <summary>
		/// Returns or sets the ID of the y-axis to plot this dataset on.
		/// </summary>
		/// <value>
		/// A <see cref="string"/> matching a key in the chart's scales configuration (e.g. <c>"y"</c>). The default is <c>null</c>
		/// (the first y-axis). Maps to the Chart.js <c>yAxisID</c> option.
		/// </value>
		/// <remarks>
		/// Additional axes (e.g. <c>"y1"</c>) can be defined through <see cref="ScalesOptions.ExtensionData"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new LineDataSet { Label = "Temperature", YAxisID = "y" };
		/// chart.DataSets.Add(dataSet);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("yAxisID")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The ID of the y-axis to plot this dataset on.")]
		public string? YAxisID
		{
			get => _yAxisID;
			set => SetProperty(ref _yAxisID, value);
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="ChartDataSet"/> class.
		/// </summary>
		/// <remarks>
		/// The new dataset has the label <c>"Dataset"</c> and no <see cref="Type"/>, so it is plotted using the chart's main type.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new ChartDataSet();
		/// dataSet.Label = "Visitors";
		/// dataSet.Data = new object[] { 120, 98, 143 };
		/// chart.DataSets.Add(dataSet);
		/// ]]></code>
		/// </example>
		public ChartDataSet()
		{
		}

		/// <summary>
		/// Returns or sets the label for the dataset which appears in the legend and tooltips.
		/// </summary>
		/// <value>
		/// A <see cref="string"/>. The default is <c>"Dataset"</c>. Maps to the Chart.js <c>label</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new BarDataSet { Label = "Revenue 2024" };
		/// chart.DataSets.Add(dataSet);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("label")]
		[Description("The label for the dataset which appears in the legend and tooltips.")]
		public string Label
		{
			get => _label;
			set => SetProperty(ref _label, value);
		}

		/// <summary>
		/// Returns or sets the data to plot.
		/// </summary>
		/// <value>
		/// An array of values. The default is <c>null</c>. Maps to the Chart.js <c>data</c> option.
		/// </value>
		/// <remarks>
		/// Items can be numbers (matched to the chart <see cref="ChartJS4.Labels"/> by index), <c>null</c> to create gaps,
		/// or objects such as <c>new { x = 1, y = 2 }</c> for scatter charts and <c>new { x = 1, y = 2, r = 5 }</c> for bubble charts.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Sales" };
		/// line.Data = new object[] { 12, 19, 3, 5, 2 };
		///
		/// var scatter = new ScatterDataSet { Label = "Points" };
		/// scatter.Data = new object[] { new { x = 1, y = 4 }, new { x = 3, y = 7 } };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("data")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The data to plot.")]
		public object[]? Data
		{
			get => _data;
			set => SetProperty(ref _data, value);
		}

		/// <summary>
		/// Returns or sets the type of chart that plots this data set (overrides the main chart type).
		/// </summary>
		/// <value>
		/// A Chart.js chart type string such as <c>"line"</c>, <c>"bar"</c>, <c>"bubble"</c>, <c>"scatter"</c>, <c>"radar"</c>,
		/// <c>"pie"</c>, <c>"doughnut"</c> or <c>"polarArea"</c>. The default is <c>null</c> (use the chart's type).
		/// Maps to the Chart.js <c>type</c> dataset option.
		/// </value>
		/// <remarks>
		/// Setting this property allows mixed charts, e.g. a line dataset drawn on top of a bar chart.
		/// The specialized dataset classes set this value in their constructors.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartType = ChartType.Bar;
		/// chart.DataSets.Add(new ChartDataSet { Label = "Sales", Data = new object[] { 5, 8, 6 } });
		/// chart.DataSets.Add(new ChartDataSet { Label = "Target", Data = new object[] { 6, 6, 6 }, Type = "line" });
		/// ]]></code>
		/// </example>
		[JsonPropertyName("type")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The type of chart that plots this data set.")]
		public string? Type
		{
			get => _type;
			set => SetProperty(ref _type, value);
		}

		/// <summary>
		/// Returns or sets a value indicating whether the dataset is hidden.
		/// </summary>
		/// <value>
		/// <c>true</c> to hide the dataset; otherwise <c>false</c>. The default is <c>false</c>. Maps to the Chart.js <c>hidden</c> option.
		/// </value>
		/// <remarks>
		/// A hidden dataset is not rendered but still appears (struck through) in the legend.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = chart.DataSets[0];
		/// dataSet.Hidden = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("hidden")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Hides the dataset.")]
		public bool Hidden
		{
			get => _hidden;
			set => SetProperty(ref _hidden, value);
		}

		/// <summary>
		/// Returns or sets the fill color or pattern.
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string (e.g. <c>"rgba(75, 192, 192, 0.2)"</c>), or an array of colors
		/// (one per data point). The default is <c>null</c> (Chart.js default). Maps to the Chart.js <c>backgroundColor</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var bars = new BarDataSet { Label = "Sales" };
		/// bars.BackgroundColor = Color.FromArgb(128, Color.SteelBlue);
		///
		/// var pie = new PieDataSet { Label = "Share" };
		/// pie.BackgroundColor = new object[] { Color.Red, "#36a2eb", "rgb(255, 205, 86)" };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("backgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The fill color or pattern.")]
		public object? BackgroundColor
		{
			get => _backgroundColor;
			set => SetProperty(ref _backgroundColor, value);
		}

		/// <summary>
		/// Returns or sets the border color.
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors (one per data point).
		/// The default is <c>null</c> (Chart.js default). Maps to the Chart.js <c>borderColor</c> option.
		/// </value>
		/// <remarks>
		/// For line charts this is the color of the line itself.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Sales" };
		/// line.BorderColor = Color.SteelBlue;
		/// line.BorderWidth = 2;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The border color.")]
		public object? BorderColor
		{
			get => _borderColor;
			set => SetProperty(ref _borderColor, value);
		}

		/// <summary>
		/// Returns or sets the border width in pixels.
		/// </summary>
		/// <value>
		/// An <see cref="int"/>. The default is <c>0</c>, which is not serialized so the Chart.js default applies.
		/// Maps to the Chart.js <c>borderWidth</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var bars = new BarDataSet { Label = "Sales" };
		/// bars.BorderColor = Color.Navy;
		/// bars.BorderWidth = 1;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("The border width.")]
		public int BorderWidth
		{
			get => _borderWidth;
			set => SetProperty(ref _borderWidth, value);
		}

		/// <summary>
		/// Returns or sets additional custom properties that can be serialized to JSON.
		/// This allows for maximum flexibility when working with Chart.js options.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c>. The default is <c>null</c>.
		/// </value>
		/// <remarks>
		/// Marked with <see cref="JsonExtensionDataAttribute"/>: every entry is written as an additional property of the
		/// dataset JSON object, next to the typed properties. Use it for any Chart.js dataset option that is not exposed
		/// as a property (e.g. <c>hoverBackgroundColor</c>, <c>clip</c>, or plugin-specific dataset options).
		/// Changes to the dictionary do not refresh the chart automatically.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new BarDataSet { Label = "Sales" };
		/// dataSet.ExtensionData = new Dictionary<string, object>
		/// {
		///     ["hoverBackgroundColor"] = "rgba(255, 99, 132, 0.8)",
		///     ["clip"] = 5
		/// };
		/// ]]></code>
		/// </example>
		[JsonExtensionData]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <summary>
		/// Determines whether the <see cref="Hidden"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Hidden"/> is not <c>false</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (dataSet.ShouldSerializeHidden())
		///     dataSet.ResetHidden();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeHidden() => Hidden != false;

		/// <summary>
		/// Resets the <see cref="Hidden"/> property to its default value of <c>false</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// dataSet.ResetHidden();
		/// ]]></code>
		/// </example>
		public void ResetHidden() => Hidden = false;

		/// <summary>
		/// Determines whether the <see cref="BorderWidth"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BorderWidth"/> is not <c>0</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (dataSet.ShouldSerializeBorderWidth())
		///     dataSet.ResetBorderWidth();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBorderWidth() => BorderWidth != 0;

		/// <summary>
		/// Resets the <see cref="BorderWidth"/> property to its default value of <c>0</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// dataSet.ResetBorderWidth();
		/// ]]></code>
		/// </example>
		public void ResetBorderWidth() => BorderWidth = 0;
	}

	/// <summary>
	/// Data set for line charts.
	/// </summary>
	/// <remarks>
	/// Sets <see cref="ChartDataSet.Type"/> to <c>"line"</c> and exposes line-specific Chart.js options such as
	/// <see cref="Tension"/>, <see cref="Fill"/>, <see cref="Stepped"/> and the point styling options.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4 { ChartType = ChartType.Line };
	/// chart.Labels = new[] { "Mon", "Tue", "Wed", "Thu" };
	/// chart.DataSets.Add(new LineDataSet
	/// {
	///     Label = "Visitors",
	///     Data = new object[] { 120, 150, 90, 180 },
	///     BorderColor = Color.SteelBlue,
	///     Tension = 0.4
	/// });
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	public class LineDataSet : ChartDataSet
	{
		private double _tension;
		private object? _fill;
		private object? _stepped;
		private object[]? _borderDash;
		private object? _pointStyle;
		private object? _pointRadius;
		private object? _pointHoverRadius;
		private object? _pointBorderColor;
		private object? _pointHoverBackgroundColor;
		private object? _animations;
		private object? _radius;
		private string? _cubicInterpolationMode;
		private object? _spanGaps;

		/// <summary>
		/// Initializes a new instance of the <see cref="LineDataSet"/> class.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="ChartDataSet.Type"/> to <c>"line"</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet();
		/// line.Label = "Sales";
		/// line.Data = new object[] { 3, 7, 4 };
		/// chart.DataSets.Add(line);
		/// ]]></code>
		/// </example>
		public LineDataSet()
		{
			Type = "line";
		}

		/// <summary>
		/// Returns or sets the Bezier curve tension of the line (0 for straight lines).
		/// </summary>
		/// <value>
		/// A <see cref="double"/>, typically between <c>0</c> and <c>1</c>. The default is <c>0.0</c> (straight lines).
		/// Maps to the Chart.js <c>tension</c> option.
		/// </value>
		/// <remarks>
		/// Ignored when <see cref="CubicInterpolationMode"/> is <c>"monotone"</c> or when <see cref="Stepped"/> is enabled.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Smooth" };
		/// line.Tension = 0.4;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("tension")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0.0)]
		[Description("Bezier curve tension (0 for straight lines).")]
		public double Tension
		{
			get => _tension;
			set => SetProperty(ref _tension, value);
		}

		/// <summary>
		/// Returns or sets how the area under the line is filled.
		/// Accepts bool (true/false), int (dataset index), or string ('-1', 'origin', 'start', 'end', 'stack', 'shape').
		/// </summary>
		/// <value>
		/// A <see cref="bool"/>, an <see cref="int"/> (absolute dataset index), or a <see cref="string"/>:
		/// a relative index such as <c>"-1"</c> or <c>"+1"</c>, or one of <c>"origin"</c> | <c>"start"</c> | <c>"end"</c> |
		/// <c>"stack"</c> | <c>"shape"</c>. The default is <c>null</c> (no fill). Maps to the Chart.js <c>fill</c> option.
		/// </value>
		/// <remarks>
		/// The fill color is taken from <see cref="ChartDataSet.BackgroundColor"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Area" };
		/// line.Fill = "origin";
		/// line.BackgroundColor = "rgba(54, 162, 235, 0.3)";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("fill")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Fill area under the line (bool, int, or string).")]
		public object? Fill
		{
			get => _fill;
			set => SetProperty(ref _fill, value);
		}

		/// <summary>
		/// Returns or sets whether the line is drawn as a stepped line. Accepts bool or one of 'before', 'after', 'middle'.
		/// </summary>
		/// <value>
		/// <c>true</c> (same as <c>"before"</c>), <c>false</c>, or one of <c>"before"</c> | <c>"after"</c> | <c>"middle"</c>.
		/// The default is <c>null</c> (not stepped). Maps to the Chart.js <c>stepped</c> option.
		/// </value>
		/// <remarks>
		/// When a stepped line is enabled, <see cref="Tension"/> is ignored.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Steps" };
		/// line.Stepped = "middle";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("stepped")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Draw the line as stepped. Accepts bool or 'before', 'after', 'middle'.")]
		public object? Stepped
		{
			get => _stepped;
			set => SetProperty(ref _stepped, value);
		}

		/// <summary>
		/// Returns or sets the length and spacing of the line dashes. Refer to MDN (<c>CanvasRenderingContext2D.setLineDash</c>) for details.
		/// </summary>
		/// <value>
		/// An array of numbers alternating dash and gap lengths in pixels (e.g. <c>[5, 5]</c>). The default is <c>null</c> (solid line).
		/// Maps to the Chart.js <c>borderDash</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var target = new LineDataSet { Label = "Target" };
		/// target.BorderDash = new object[] { 6, 4 };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderDash")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Length and spacing of dashes (e.g., [5, 5]).")]
		public object[]? BorderDash
		{
			get => _borderDash;
			set => SetProperty(ref _borderDash, value);
		}

		/// <summary>
		/// Returns or sets the style of the points. Accepts a string or array of strings ('circle', 'cross', 'crossRot', 'dash', 'line', 'rect', 'rectRounded', 'rectRot', 'star', 'triangle', 'false').
		/// </summary>
		/// <value>
		/// A point style string, <c>false</c> to hide the points, or an array with one style per data point.
		/// The default is <c>null</c> (Chart.js default <c>"circle"</c>). Maps to the Chart.js <c>pointStyle</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Sales" };
		/// line.PointStyle = "rectRot";
		/// line.PointRadius = 6;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("pointStyle")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Style of the point.")]
		public object? PointStyle
		{
			get => _pointStyle;
			set => SetProperty(ref _pointStyle, value);
		}

		/// <summary>
		/// Returns or sets the radius of the point shape. Accepts a number or array of numbers.
		/// </summary>
		/// <value>
		/// A number in pixels, or an array with one radius per data point. The property is <c>null</c> unless set
		/// (the designer default is <c>5</c>). Set to <c>0</c> to hide the points. Maps to the Chart.js <c>pointRadius</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Sales" };
		/// line.PointRadius = new object[] { 3, 3, 8, 3 };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("pointRadius")]
		[DefaultValue(5)]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Radius of the point shape.")]
		public object? PointRadius
		{
			get => _pointRadius;
			set => SetProperty(ref _pointRadius, value);
		}

		/// <summary>
		/// Returns or sets the point radius when hovered.
		/// </summary>
		/// <value>
		/// A number in pixels, or an array with one value per data point. The default is <c>null</c> (Chart.js default).
		/// Maps to the Chart.js <c>pointHoverRadius</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Sales" };
		/// line.PointRadius = 4;
		/// line.PointHoverRadius = 8;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("pointHoverRadius")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Point radius when hovered.")]
		public object? PointHoverRadius
		{
			get => _pointHoverRadius;
			set => SetProperty(ref _pointHoverRadius, value);
		}

		/// <summary>
		/// Returns or sets the border color of the points.
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors (one per data point).
		/// The default is <c>null</c> (Chart.js default). Maps to the Chart.js <c>pointBorderColor</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Sales" };
		/// line.PointBorderColor = Color.White;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("pointBorderColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Point border color.")]
		public object? PointBorderColor
		{
			get => _pointBorderColor;
			set => SetProperty(ref _pointBorderColor, value);
		}

		/// <summary>
		/// Returns or sets the point background color when hovered.
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors (one per data point).
		/// The default is <c>null</c> (Chart.js default). Maps to the Chart.js <c>pointHoverBackgroundColor</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Sales" };
		/// line.PointHoverBackgroundColor = "#ff6384";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("pointHoverBackgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Point background color when hovered.")]
		public object? PointHoverBackgroundColor
		{
			get => _pointHoverBackgroundColor;
			set => SetProperty(ref _pointHoverBackgroundColor, value);
		}

		/// <summary>
		/// Returns or sets the per-dataset animation configuration. Accepts an object like <c>new { y = new { duration = 2000 } }</c>.
		/// </summary>
		/// <value>
		/// An object (typically an anonymous type) whose properties are the animated properties (e.g. <c>x</c>, <c>y</c>, <c>tension</c>)
		/// and whose values are Chart.js animation configurations. The default is <c>null</c>. Maps to the Chart.js <c>animations</c> dataset option.
		/// </value>
		/// <remarks>
		/// Overrides the chart-level <see cref="ChartOptions.Animations"/> for this dataset only.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Sales" };
		/// line.Animations = new
		/// {
		///     tension = new { duration = 1000, easing = "linear", from = 1, to = 0, loop = true }
		/// };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("animations")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Per-dataset animation configuration.")]
		public object? Animations
		{
			get => _animations;
			set => SetProperty(ref _animations, value);
		}

		/// <summary>
		/// Returns or sets the radius of the point shape for all points in the dataset. This is a shorthand for <see cref="PointRadius"/> when a single value is needed.
		/// </summary>
		/// <value>
		/// A number in pixels. The property is <c>null</c> unless set (the designer default is <c>5</c>).
		/// Maps to the Chart.js <c>radius</c> option.
		/// </value>
		/// <remarks>
		/// Unlike most other options, this property is written to the dataset JSON even when <c>null</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Sales" };
		/// line.Radius = 0; // hide all points
		/// ]]></code>
		/// </example>
		[JsonPropertyName("radius")]
		[DefaultValue(5)]
		//[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Radius of all points in the dataset.")]
		public object? Radius
		{
			get => _radius;
			set => SetProperty(ref _radius, value);
		}

		/// <summary>
		/// Returns or sets the algorithm to use when interpolating a smooth curve from the discrete data points.
		/// Accepted values: 'default' | 'monotone'.
		/// </summary>
		/// <value>
		/// <c>"default"</c> (uses <see cref="Tension"/>) or <c>"monotone"</c> (preserves monotonicity of the data).
		/// The default is <c>null</c> (Chart.js default <c>"default"</c>). Maps to the Chart.js <c>cubicInterpolationMode</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Monotone" };
		/// line.CubicInterpolationMode = "monotone";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("cubicInterpolationMode")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Algorithm for smooth curve interpolation. 'default' or 'monotone'.")]
		[DefaultValue(null)]
		public string? CubicInterpolationMode
		{
			get => _cubicInterpolationMode;
			set => SetProperty(ref _cubicInterpolationMode, value);
		}

		/// <summary>
		/// Returns or sets whether lines are drawn across missing data.
		/// If <c>true</c>, lines will be drawn between points with no or null data. If <c>false</c>, points with NaN data will create a break in the line.
		/// Can also be a number specifying the maximum gap length to span.
		/// </summary>
		/// <value>
		/// A <see cref="bool"/> or a number (maximum gap to span, in scale units). The default is <c>null</c> (Chart.js default <c>false</c>).
		/// Maps to the Chart.js <c>spanGaps</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet { Label = "Readings" };
		/// line.Data = new object[] { 5, null, 7, 9 };
		/// line.SpanGaps = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("spanGaps")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("If true, lines will be drawn between points with no or null data.")]
		[DefaultValue(null)]
		public object? SpanGaps
		{
			get => _spanGaps;
			set => SetProperty(ref _spanGaps, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Tension"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Tension"/> is not <c>0.0</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (line.ShouldSerializeTension())
		///     line.ResetTension();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTension() => Tension != 0.0;

		/// <summary>
		/// Resets the <see cref="Tension"/> property to its default value of <c>0.0</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// line.ResetTension();
		/// ]]></code>
		/// </example>
		public void ResetTension() => Tension = 0.0;

		/// <summary>
		/// Determines whether the <see cref="Fill"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Fill"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (line.ShouldSerializeFill())
		///     line.ResetFill();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeFill() => Fill != null;

		/// <summary>
		/// Resets the <see cref="Fill"/> property to its default value of <c>null</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// line.ResetFill();
		/// ]]></code>
		/// </example>
		public void ResetFill() => Fill = null;
	}

	/// <summary>
	/// Data set for bar charts.
	/// </summary>
	/// <remarks>
	/// Sets <see cref="ChartDataSet.Type"/> to <c>"bar"</c> and exposes bar-specific Chart.js options such as
	/// <see cref="BarPercentage"/>, <see cref="CategoryPercentage"/>, <see cref="BorderRadius"/> and <see cref="BorderSkipped"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4 { ChartType = ChartType.Bar };
	/// chart.Labels = new[] { "Q1", "Q2", "Q3", "Q4" };
	/// chart.DataSets.Add(new BarDataSet
	/// {
	///     Label = "Revenue",
	///     Data = new object[] { 40, 55, 48, 70 },
	///     BackgroundColor = Color.SteelBlue,
	///     BorderRadius = 6
	/// });
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	public class BarDataSet : ChartDataSet
	{
		private double _barPercentage = 0.9;
		private double _categoryPercentage = 0.8;
		private int _hoverBorderWidth = 1;
		private object? _hoverBorderColor;
		private int _borderRadius;
		private object? _borderSkipped;

		/// <summary>
		/// Initializes a new instance of the <see cref="BarDataSet"/> class.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="ChartDataSet.Type"/> to <c>"bar"</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bars = new BarDataSet();
		/// bars.Label = "Orders";
		/// bars.Data = new object[] { 12, 18, 9 };
		/// chart.DataSets.Add(bars);
		/// ]]></code>
		/// </example>
		public BarDataSet()
		{
			Type = "bar";
		}

		/// <summary>
		/// Returns or sets the percent (0-1) of the available width each bar should be within the category width.
		/// </summary>
		/// <value>
		/// A <see cref="double"/> between <c>0</c> and <c>1</c>. The default is <c>0.9</c>. Maps to the Chart.js <c>barPercentage</c> option.
		/// </value>
		/// <remarks>
		/// A value of <c>1.0</c> makes bars of the same category touch each other.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bars = new BarDataSet { Label = "Sales" };
		/// bars.BarPercentage = 1.0;
		/// bars.CategoryPercentage = 1.0;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("barPercentage")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0.9)]
		[Description("Percent (0-1) of the available width each bar should be within the category width.")]
		public double BarPercentage
		{
			get => _barPercentage;
			set => SetProperty(ref _barPercentage, value);
		}

		/// <summary>
		/// Returns or sets the percent (0-1) of the available width each category should be within the sample width.
		/// </summary>
		/// <value>
		/// A <see cref="double"/> between <c>0</c> and <c>1</c>. The default is <c>0.8</c>. Maps to the Chart.js <c>categoryPercentage</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var bars = new BarDataSet { Label = "Sales" };
		/// bars.CategoryPercentage = 0.6;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("categoryPercentage")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0.8)]
		[Description("Percent (0-1) of the available width each category should be within the sample width.")]
		public double CategoryPercentage
		{
			get => _categoryPercentage;
			set => SetProperty(ref _categoryPercentage, value);
		}

		/// <summary>
		/// Returns or sets the border width of the bar when hovered.
		/// </summary>
		/// <value>
		/// An <see cref="int"/> in pixels. The default is <c>1</c>. Maps to the Chart.js <c>hoverBorderWidth</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var bars = new BarDataSet { Label = "Sales" };
		/// bars.HoverBorderWidth = 3;
		/// bars.HoverBorderColor = Color.Black;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("hoverBorderWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(1)]
		[Description("Border width when hovered.")]
		public int HoverBorderWidth
		{
			get => _hoverBorderWidth;
			set => SetProperty(ref _hoverBorderWidth, value);
		}

		/// <summary>
		/// Returns or sets the border color of the bar when hovered.
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors (one per bar).
		/// The default is <c>null</c> (Chart.js default). Maps to the Chart.js <c>hoverBorderColor</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var bars = new BarDataSet { Label = "Sales" };
		/// bars.HoverBorderColor = "rgba(0, 0, 0, 0.6)";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("hoverBorderColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Border color when hovered.")]
		public object? HoverBorderColor
		{
			get => _hoverBorderColor;
			set => SetProperty(ref _hoverBorderColor, value);
		}

		/// <summary>
		/// Returns or sets the border radius of the bars. Set to a large number (e.g. <see cref="int.MaxValue"/>) for fully rounded bars.
		/// </summary>
		/// <value>
		/// An <see cref="int"/> in pixels. The default is <c>0</c> (square corners). Maps to the Chart.js <c>borderRadius</c> option.
		/// </value>
		/// <remarks>
		/// Use <see cref="BorderSkipped"/> to control which edge is not rounded.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bars = new BarDataSet { Label = "Sales" };
		/// bars.BorderRadius = int.MaxValue;
		/// bars.BorderSkipped = false;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderRadius")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("The border radius of the bars.")]
		public int BorderRadius
		{
			get => _borderRadius;
			set => SetProperty(ref _borderRadius, value);
		}

		/// <summary>
		/// Determines whether the <see cref="BorderRadius"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BorderRadius"/> is not <c>0</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (bars.ShouldSerializeBorderRadius())
		///     bars.ResetBorderRadius();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBorderRadius() => BorderRadius != 0;

		/// <summary>
		/// Resets the <see cref="BorderRadius"/> property to its default value of <c>0</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// bars.ResetBorderRadius();
		/// ]]></code>
		/// </example>
		public void ResetBorderRadius() => BorderRadius = 0;

		/// <summary>
		/// Returns or sets which edge to skip border radius on. Set to <c>false</c> to apply border radius to all edges.
		/// Accepts 'start', 'end', 'left', 'right', 'top', 'bottom', or <c>false</c>.
		/// </summary>
		/// <value>
		/// One of <c>"start"</c> | <c>"end"</c> | <c>"middle"</c> | <c>"left"</c> | <c>"right"</c> | <c>"top"</c> | <c>"bottom"</c>,
		/// <c>true</c>, or <c>false</c>. The default is <c>null</c> (Chart.js default <c>"start"</c>).
		/// Maps to the Chart.js <c>borderSkipped</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var bars = new BarDataSet { Label = "Sales", BorderWidth = 2 };
		/// bars.BorderSkipped = "bottom";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderSkipped")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Which edge to skip border radius on. Set to false to apply to all edges.")]
		public object? BorderSkipped
		{
			get => _borderSkipped;
			set => SetProperty(ref _borderSkipped, value);
		}

		/// <summary>
		/// Determines whether the <see cref="BarPercentage"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BarPercentage"/> is not <c>0.9</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (bars.ShouldSerializeBarPercentage())
		///     bars.ResetBarPercentage();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBarPercentage() => BarPercentage != 0.9;

		/// <summary>
		/// Resets the <see cref="BarPercentage"/> property to its default value of <c>0.9</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// bars.ResetBarPercentage();
		/// ]]></code>
		/// </example>
		public void ResetBarPercentage() => BarPercentage = 0.9;

		/// <summary>
		/// Determines whether the <see cref="CategoryPercentage"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="CategoryPercentage"/> is not <c>0.8</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (bars.ShouldSerializeCategoryPercentage())
		///     bars.ResetCategoryPercentage();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeCategoryPercentage() => CategoryPercentage != 0.8;

		/// <summary>
		/// Resets the <see cref="CategoryPercentage"/> property to its default value of <c>0.8</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// bars.ResetCategoryPercentage();
		/// ]]></code>
		/// </example>
		public void ResetCategoryPercentage() => CategoryPercentage = 0.8;
	}

	/// <summary>
	/// Data set for pie and doughnut charts.
	/// </summary>
	/// <remarks>
	/// Unlike the other specialized datasets, <see cref="ChartDataSet.Type"/> is not set, so the dataset is drawn using the
	/// chart's <see cref="ChartJS4.ChartType"/> (<see cref="ChartType.Pie"/> or <see cref="ChartType.Doughnut"/>).
	/// Colors are usually assigned per slice by setting <see cref="ChartDataSet.BackgroundColor"/> to an array.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4 { ChartType = ChartType.Doughnut };
	/// chart.Labels = new[] { "Red", "Blue", "Yellow" };
	/// chart.DataSets.Add(new PieDataSet
	/// {
	///     Label = "Votes",
	///     Data = new object[] { 300, 50, 100 },
	///     BackgroundColor = new object[] { Color.Red, Color.Blue, Color.Gold }
	/// });
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	public class PieDataSet : ChartDataSet
	{
		private int _weight = 1;

		/// <summary>
		/// Initializes a new instance of the <see cref="PieDataSet"/> class.
		/// </summary>
		/// <remarks>
		/// <see cref="ChartDataSet.Type"/> is left <c>null</c>, so the chart's type (pie or doughnut) is used.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var pie = new PieDataSet();
		/// pie.Data = new object[] { 60, 25, 15 };
		/// chart.DataSets.Add(pie);
		/// ]]></code>
		/// </example>
		public PieDataSet()
		{
		}

		/// <summary>
		/// Returns or sets the relative thickness of the dataset (doughnut only).
		/// </summary>
		/// <value>
		/// An <see cref="int"/>. The default is <c>1</c>. Maps to the Chart.js <c>weight</c> option.
		/// </value>
		/// <remarks>
		/// When a doughnut chart has several datasets, each ring's thickness is proportional to its weight relative to the sum of all weights.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.DataSets.Add(new PieDataSet { Label = "Outer", Data = new object[] { 5, 3 }, Weight = 2 });
		/// chart.DataSets.Add(new PieDataSet { Label = "Inner", Data = new object[] { 4, 4 }, Weight = 1 });
		/// ]]></code>
		/// </example>
		[JsonPropertyName("weight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(1)]
		[Description("The relative thickness of the dataset (doughnut only).")]
		public int Weight
		{
			get => _weight;
			set => SetProperty(ref _weight, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Weight"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Weight"/> is not <c>1</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (pie.ShouldSerializeWeight())
		///     pie.ResetWeight();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeWeight() => Weight != 1;

		/// <summary>
		/// Resets the <see cref="Weight"/> property to its default value of <c>1</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// pie.ResetWeight();
		/// ]]></code>
		/// </example>
		public void ResetWeight() => Weight = 1;
	}

	/// <summary>
	/// Data set for bubble charts.
	/// </summary>
	/// <remarks>
	/// Sets <see cref="ChartDataSet.Type"/> to <c>"bubble"</c>. Each data item is an object with <c>x</c>, <c>y</c>
	/// and <c>r</c> (bubble radius in pixels) properties.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4 { ChartType = ChartType.Bubble };
	/// chart.DataSets.Add(new BubbleDataSet
	/// {
	///     Label = "Cities",
	///     Data = new object[] { new { x = 10, y = 20, r = 15 }, new { x = 25, y = 8, r = 6 } },
	///     BackgroundColor = "rgba(255, 99, 132, 0.5)"
	/// });
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	public class BubbleDataSet : ChartDataSet
	{
		private string? _boxStrokeStyle;

		/// <summary>
		/// Initializes a new instance of the <see cref="BubbleDataSet"/> class.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="ChartDataSet.Type"/> to <c>"bubble"</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bubbles = new BubbleDataSet { Label = "Samples" };
		/// bubbles.Data = new object[] { new { x = 5, y = 5, r = 10 } };
		/// chart.DataSets.Add(bubbles);
		/// ]]></code>
		/// </example>
		public BubbleDataSet()
		{
			Type = "bubble";
		}

		/// <summary>
		/// Returns or sets the stroke style for box elements (custom chart type extension).
		/// </summary>
		/// <value>
		/// A CSS color or stroke style string. The default is <c>null</c>. Serialized as the <c>boxStrokeStyle</c> dataset option.
		/// </value>
		/// <remarks>
		/// This is not a standard Chart.js option; it is read by custom chart types (e.g. a custom bubble controller registered
		/// on the client and selected through <see cref="ChartOptions.Type"/>) and ignored by the built-in bubble chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bubbles = new BubbleDataSet { Label = "Custom" };
		/// bubbles.BoxStrokeStyle = "red";
		/// chart.ChartOptions.Type = "customBubble";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("boxStrokeStyle")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Stroke style for box elements.")]
		public string? BoxStrokeStyle
		{
			get => _boxStrokeStyle;
			set => SetProperty(ref _boxStrokeStyle, value);
		}
	}

	/// <summary>
	/// Data set for scatter charts.
	/// </summary>
	/// <remarks>
	/// Sets <see cref="ChartDataSet.Type"/> to <c>"scatter"</c>. Each data item is an object with <c>x</c> and <c>y</c> properties.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4 { ChartType = ChartType.Scatter };
	/// chart.DataSets.Add(new ScatterDataSet
	/// {
	///     Label = "Measurements",
	///     Data = new object[] { new { x = -10, y = 0 }, new { x = 0, y = 10 }, new { x = 10, y = 5 } },
	///     BackgroundColor = Color.Crimson
	/// });
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	public class ScatterDataSet : ChartDataSet
	{
		private object? _fill;

		/// <summary>
		/// Initializes a new instance of the <see cref="ScatterDataSet"/> class.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="ChartDataSet.Type"/> to <c>"scatter"</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var points = new ScatterDataSet { Label = "Points" };
		/// points.Data = new object[] { new { x = 1, y = 2 }, new { x = 2, y = 3 } };
		/// chart.DataSets.Add(points);
		/// ]]></code>
		/// </example>
		public ScatterDataSet()
		{
			Type = "scatter";
		}

		/// <summary>
		/// Returns or sets whether to fill the area under the scatter points.
		/// Accepts <c>true</c>, <c>false</c>, an index, or a string like '+1' or '-1'.
		/// </summary>
		/// <value>
		/// A <see cref="bool"/>, an <see cref="int"/> (absolute dataset index), or a <see cref="string"/> such as <c>"+1"</c>, <c>"-1"</c>,
		/// <c>"origin"</c>, <c>"start"</c> or <c>"end"</c>. The default is <c>null</c> (no fill). Maps to the Chart.js <c>fill</c> option.
		/// </value>
		/// <remarks>
		/// Filling only has a visible effect when the points are connected by a line (e.g. with <c>showLine</c> set via
		/// <see cref="ChartDataSet.ExtensionData"/>).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var points = new ScatterDataSet { Label = "Range" };
		/// points.ExtensionData = new Dictionary<string, object> { ["showLine"] = true };
		/// points.Fill = "origin";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("fill")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Whether to fill the area under the scatter points.")]
		[DefaultValue(null)]
		public object? Fill
		{
			get => _fill;
			set => SetProperty(ref _fill, value);
		}
	}

	/// <summary>
	/// Data set for radar charts.
	/// </summary>
	/// <remarks>
	/// Sets <see cref="ChartDataSet.Type"/> to <c>"radar"</c>. The data values are matched by index to the chart
	/// <see cref="ChartJS4.Labels"/>, which become the spokes of the radar.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4 { ChartType = ChartType.Radar };
	/// chart.Labels = new[] { "Speed", "Power", "Range", "Comfort", "Price" };
	/// chart.DataSets.Add(new RadarDataSet
	/// {
	///     Label = "Model A",
	///     Data = new object[] { 8, 6, 7, 5, 4 },
	///     Fill = true,
	///     BackgroundColor = "rgba(54, 162, 235, 0.2)"
	/// });
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	public class RadarDataSet : ChartDataSet
	{
		private object? _fill;
		private double _tension;

		/// <summary>
		/// Initializes a new instance of the <see cref="RadarDataSet"/> class.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="ChartDataSet.Type"/> to <c>"radar"</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var radar = new RadarDataSet { Label = "Skills" };
		/// radar.Data = new object[] { 3, 5, 4 };
		/// chart.DataSets.Add(radar);
		/// ]]></code>
		/// </example>
		public RadarDataSet()
		{
			Type = "radar";
		}

		/// <summary>
		/// Returns or sets how the area of the radar is filled. Accepts bool, int (dataset index), or string.
		/// </summary>
		/// <value>
		/// A <see cref="bool"/>, an <see cref="int"/> (absolute dataset index), or a <see cref="string"/> such as <c>"-1"</c>,
		/// <c>"+1"</c>, <c>"origin"</c>, <c>"start"</c> or <c>"end"</c>. The default is <c>null</c> (no fill).
		/// Maps to the Chart.js <c>fill</c> option.
		/// </value>
		/// <remarks>
		/// The fill color is taken from <see cref="ChartDataSet.BackgroundColor"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var radar = new RadarDataSet { Label = "Team" };
		/// radar.Fill = true;
		/// radar.BackgroundColor = "rgba(255, 99, 132, 0.2)";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("fill")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Fill area under the radar (bool, int, or string).")]
		public object? Fill
		{
			get => _fill;
			set => SetProperty(ref _fill, value);
		}

		/// <summary>
		/// Returns or sets the Bezier curve tension of the radar lines (0 for straight lines).
		/// </summary>
		/// <value>
		/// A <see cref="double"/>, typically between <c>0</c> and <c>1</c>. The default is <c>0.0</c> (straight lines).
		/// Maps to the Chart.js <c>tension</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var radar = new RadarDataSet { Label = "Team" };
		/// radar.Tension = 0.3;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("tension")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0.0)]
		[Description("Bezier curve tension (0 for straight lines).")]
		public double Tension
		{
			get => _tension;
			set => SetProperty(ref _tension, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Fill"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Fill"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (radar.ShouldSerializeFill())
		///     radar.ResetFill();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeFill() => Fill != null;

		/// <summary>
		/// Resets the <see cref="Fill"/> property to its default value of <c>null</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// radar.ResetFill();
		/// ]]></code>
		/// </example>
		public void ResetFill() => Fill = null;

		/// <summary>
		/// Determines whether the <see cref="Tension"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Tension"/> is not <c>0.0</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (radar.ShouldSerializeTension())
		///     radar.ResetTension();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTension() => Tension != 0.0;

		/// <summary>
		/// Resets the <see cref="Tension"/> property to its default value of <c>0.0</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// radar.ResetTension();
		/// ]]></code>
		/// </example>
		public void ResetTension() => Tension = 0.0;
	}

	/// <summary>
	/// Data set for polar area charts.
	/// </summary>
	/// <remarks>
	/// Sets <see cref="ChartDataSet.Type"/> to <c>"polarArea"</c>. Each value is drawn as a segment with the same angle
	/// and a radius proportional to the value; colors are usually assigned per segment via an array in
	/// <see cref="ChartDataSet.BackgroundColor"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4 { ChartType = ChartType.PolarArea };
	/// chart.Labels = new[] { "North", "East", "South", "West" };
	/// chart.DataSets.Add(new PolarAreaDataSet
	/// {
	///     Label = "Wind",
	///     Data = new object[] { 11, 16, 7, 3 },
	///     BackgroundColor = new object[] { "#ff6384", "#4bc0c0", "#ffcd56", "#36a2eb" }
	/// });
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	public class PolarAreaDataSet : ChartDataSet
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PolarAreaDataSet"/> class.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="ChartDataSet.Type"/> to <c>"polarArea"</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var polar = new PolarAreaDataSet { Label = "Scores" };
		/// polar.Data = new object[] { 4, 7, 2 };
		/// chart.DataSets.Add(polar);
		/// ]]></code>
		/// </example>
		public PolarAreaDataSet()
		{
			Type = "polarArea";
		}
	}
}
