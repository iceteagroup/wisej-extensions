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
		/// The drawing order of the dataset. Also affects order for stacking, tooltip and legend.
		/// </summary>
		[JsonPropertyName("order")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The drawing order of the dataset.")]
		public int? Order
		{
			get => _order;
			set => SetProperty(ref _order, value);
		}

		/// <summary>
		/// The ID of the group to which the dataset belongs to (when stacking datasets).
		/// </summary>
		[JsonPropertyName("stack")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The stack group identifier for grouped/stacked datasets.")]
		public string? Stack
		{
			get => _stack;
			set => SetProperty(ref _stack, value);
		}

		/// <summary>
		/// The ID of the y-axis to plot this dataset on.
		/// </summary>
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
		public ChartDataSet()
		{
		}

		/// <summary>
		/// The label for the dataset which appears in the legend and tooltips.
		/// </summary>
		[JsonPropertyName("label")]
		[Description("The label for the dataset which appears in the legend and tooltips.")]
		public string Label
		{
			get => _label;
			set => SetProperty(ref _label, value);
		}

		/// <summary>
		/// The data to plot.
		/// </summary>
		[JsonPropertyName("data")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The data to plot.")]
		public object[]? Data
		{
			get => _data;
			set => SetProperty(ref _data, value);
		}

		/// <summary>
		/// The type of chart that plots this data set (overrides the main chart type).
		/// </summary>
		[JsonPropertyName("type")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The type of chart that plots this data set.")]
		public string? Type
		{
			get => _type;
			set => SetProperty(ref _type, value);
		}

		/// <summary>
		/// Hides the dataset.
		/// </summary>
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
		/// The fill color or pattern.
		/// </summary>
		[JsonPropertyName("backgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The fill color or pattern.")]
		public object? BackgroundColor
		{
			get => _backgroundColor;
			set => SetProperty(ref _backgroundColor, value);
		}

		/// <summary>
		/// The border color.
		/// </summary>
		[JsonPropertyName("borderColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The border color.")]
		public object? BorderColor
		{
			get => _borderColor;
			set => SetProperty(ref _borderColor, value);
		}

		/// <summary>
		/// The border width.
		/// </summary>
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
		/// Additional custom properties that can be serialized to JSON.
		/// This allows for maximum flexibility when working with Chart.js options.
		/// </summary>
		[JsonExtensionData]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <summary>
		/// Determines whether the Hidden property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeHidden() => Hidden != false;

		/// <summary>
		/// Resets the Hidden property to its default value.
		/// </summary>
		public void ResetHidden() => Hidden = false;

		/// <summary>
		/// Determines whether the BorderWidth property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBorderWidth() => BorderWidth != 0;

		/// <summary>
		/// Resets the BorderWidth property to its default value.
		/// </summary>
		public void ResetBorderWidth() => BorderWidth = 0;
	}

	/// <summary>
	/// Data set for line charts.
	/// </summary>
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
		public LineDataSet()
		{
			Type = "line";
		}

		/// <summary>
		/// Bezier curve tension (0 for straight lines).
		/// </summary>
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
		/// Fill area under the line. Accepts bool (true/false), int (dataset index), or string ('-1', 'origin', 'start', 'end', 'stack', 'shape').
		/// </summary>
		[JsonPropertyName("fill")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Fill area under the line (bool, int, or string).")]
		public object? Fill
		{
			get => _fill;
			set => SetProperty(ref _fill, value);
		}

		/// <summary>
		/// Whether the line is drawn as a stepped line. Accepts bool or one of 'before', 'after', 'middle'.
		/// </summary>
		[JsonPropertyName("stepped")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Draw the line as stepped. Accepts bool or 'before', 'after', 'middle'.")]
		public object? Stepped
		{
			get => _stepped;
			set => SetProperty(ref _stepped, value);
		}

		/// <summary>
		/// Length and spacing of dashes. Refer to MDN for details.
		/// </summary>
		[JsonPropertyName("borderDash")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Length and spacing of dashes (e.g., [5, 5]).")]
		public object[]? BorderDash
		{
			get => _borderDash;
			set => SetProperty(ref _borderDash, value);
		}

		/// <summary>
		/// Style of the point. Accepts a string or array of strings ('circle', 'cross', 'crossRot', 'dash', 'line', 'rect', 'rectRounded', 'rectRot', 'star', 'triangle', 'false').
		/// </summary>
		[JsonPropertyName("pointStyle")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Style of the point.")]
		public object? PointStyle
		{
			get => _pointStyle;
			set => SetProperty(ref _pointStyle, value);
		}

		/// <summary>
		/// Radius of the point shape. Accepts a number or array of numbers.
		/// </summary>
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
		/// Point radius when hovered.
		/// </summary>
		[JsonPropertyName("pointHoverRadius")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Point radius when hovered.")]
		public object? PointHoverRadius
		{
			get => _pointHoverRadius;
			set => SetProperty(ref _pointHoverRadius, value);
		}

		/// <summary>
		/// Point border color.
		/// </summary>
		[JsonPropertyName("pointBorderColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Point border color.")]
		public object? PointBorderColor
		{
			get => _pointBorderColor;
			set => SetProperty(ref _pointBorderColor, value);
		}

		/// <summary>
		/// Point background color when hovered.
		/// </summary>
		[JsonPropertyName("pointHoverBackgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Point background color when hovered.")]
		public object? PointHoverBackgroundColor
		{
			get => _pointHoverBackgroundColor;
			set => SetProperty(ref _pointHoverBackgroundColor, value);
		}

		/// <summary>
		/// Per-dataset animation configuration. Accepts an object like <c>new { y = new { duration = 2000 } }</c>.
		/// </summary>
		[JsonPropertyName("animations")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Per-dataset animation configuration.")]
		public object? Animations
		{
			get => _animations;
			set => SetProperty(ref _animations, value);
		}

		/// <summary>
		/// Radius of the point shape for all points in the dataset. This is a shorthand for <see cref="PointRadius"/> when a single value is needed.
		/// </summary>
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
		/// Algorithm to use when interpolating a smooth curve from the discrete data points.
		/// Accepted values: 'default' | 'monotone'.
		/// </summary>
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
		/// If <c>true</c>, lines will be drawn between points with no or null data. If <c>false</c>, points with NaN data will create a break in the line.
		/// Can also be a number specifying the maximum gap length to span.
		/// </summary>
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
		/// Determines whether the Tension property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTension() => Tension != 0.0;

		/// <summary>
		/// Resets the Tension property to its default value.
		/// </summary>
		public void ResetTension() => Tension = 0.0;

		/// <summary>
		/// Determines whether the Fill property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeFill() => Fill != null;

		/// <summary>
		/// Resets the Fill property to its default value.
		/// </summary>
		public void ResetFill() => Fill = null;
	}

	/// <summary>
	/// Data set for bar charts.
	/// </summary>
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
		public BarDataSet()
		{
			Type = "bar";
		}

		/// <summary>
		/// Percent (0-1) of the available width each bar should be within the category width.
		/// </summary>
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
		/// Percent (0-1) of the available width each category should be within the sample width.
		/// </summary>
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
		/// Border width of the bar when hovered.
		/// </summary>
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
		/// Border color of the bar when hovered.
		/// </summary>
		[JsonPropertyName("hoverBorderColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Border color when hovered.")]
		public object? HoverBorderColor
		{
			get => _hoverBorderColor;
			set => SetProperty(ref _hoverBorderColor, value);
		}

		/// <summary>
		/// The border radius of the bars. Set to a large number (e.g. <see cref="int.MaxValue"/>) for fully rounded bars.
		/// </summary>
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
		/// Determines whether the BorderRadius property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBorderRadius() => BorderRadius != 0;

		/// <summary>
		/// Resets the BorderRadius property to its default value.
		/// </summary>
		public void ResetBorderRadius() => BorderRadius = 0;

		/// <summary>
		/// Which edge to skip border radius on. Set to <c>false</c> to apply border radius to all edges.
		/// Accepts 'start', 'end', 'left', 'right', 'top', 'bottom', or <c>false</c>.
		/// </summary>
		[JsonPropertyName("borderSkipped")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Which edge to skip border radius on. Set to false to apply to all edges.")]
		public object? BorderSkipped
		{
			get => _borderSkipped;
			set => SetProperty(ref _borderSkipped, value);
		}

		/// <summary>
		/// Determines whether the BarPercentage property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBarPercentage() => BarPercentage != 0.9;

		/// <summary>
		/// Resets the BarPercentage property to its default value.
		/// </summary>
		public void ResetBarPercentage() => BarPercentage = 0.9;

		/// <summary>
		/// Determines whether the CategoryPercentage property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeCategoryPercentage() => CategoryPercentage != 0.8;

		/// <summary>
		/// Resets the CategoryPercentage property to its default value.
		/// </summary>
		public void ResetCategoryPercentage() => CategoryPercentage = 0.8;
	}

	/// <summary>
	/// Data set for pie and doughnut charts.
	/// </summary>
	[ApiCategory("ChartJS4")]
	public class PieDataSet : ChartDataSet
	{
		private int _weight = 1;

		/// <summary>
		/// Initializes a new instance of the <see cref="PieDataSet"/> class.
		/// </summary>
		public PieDataSet()
		{
		}

		/// <summary>
		/// The relative thickness of the dataset (doughnut only).
		/// </summary>
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
		/// Determines whether the Weight property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeWeight() => Weight != 1;

		/// <summary>
		/// Resets the Weight property to its default value.
		/// </summary>
		public void ResetWeight() => Weight = 1;
	}

	/// <summary>
	/// Data set for bubble charts.
	/// </summary>
	[ApiCategory("ChartJS4")]
	public class BubbleDataSet : ChartDataSet
	{
		private string? _boxStrokeStyle;

		/// <summary>
		/// Initializes a new instance of the <see cref="BubbleDataSet"/> class.
		/// </summary>
		public BubbleDataSet()
		{
			Type = "bubble";
		}

		/// <summary>
		/// Stroke style for box elements (custom chart type extension).
		/// </summary>
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
	[ApiCategory("ChartJS4")]
	public class ScatterDataSet : ChartDataSet
	{
		private object? _fill;

		/// <summary>
		/// Initializes a new instance of the <see cref="ScatterDataSet"/> class.
		/// </summary>
		public ScatterDataSet()
		{
			Type = "scatter";
		}

		/// <summary>
		/// Whether to fill the area under the scatter points.
		/// Accepts <c>true</c>, <c>false</c>, an index, or a string like '+1' or '-1'.
		/// </summary>
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
	[ApiCategory("ChartJS4")]
	public class RadarDataSet : ChartDataSet
	{
		private object? _fill;
		private double _tension;

		/// <summary>
		/// Initializes a new instance of the <see cref="RadarDataSet"/> class.
		/// </summary>
		public RadarDataSet()
		{
			Type = "radar";
		}

		/// <summary>
		/// Fill area under the radar. Accepts bool, int (dataset index), or string.
		/// </summary>
		[JsonPropertyName("fill")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Fill area under the radar (bool, int, or string).")]
		public object? Fill
		{
			get => _fill;
			set => SetProperty(ref _fill, value);
		}

		/// <summary>
		/// Bezier curve tension (0 for straight lines).
		/// </summary>
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
		/// Determines whether the Fill property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeFill() => Fill != null;

		/// <summary>
		/// Resets the Fill property to its default value.
		/// </summary>
		public void ResetFill() => Fill = null;

		/// <summary>
		/// Determines whether the Tension property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTension() => Tension != 0.0;

		/// <summary>
		/// Resets the Tension property to its default value.
		/// </summary>
		public void ResetTension() => Tension = 0.0;
	}

	/// <summary>
	/// Data set for polar area charts.
	/// </summary>
	[ApiCategory("ChartJS4")]
	public class PolarAreaDataSet : ChartDataSet
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PolarAreaDataSet"/> class.
		/// </summary>
		public PolarAreaDataSet()
		{
			Type = "polarArea";
		}
	}
}
