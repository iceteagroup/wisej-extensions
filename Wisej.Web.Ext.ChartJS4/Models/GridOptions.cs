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
	/// Represents the grid line options of an axis (Chart.js <c>scales[id].grid</c>).
	/// </summary>
	/// <remarks>
	/// An instance is lazily created by <see cref="AxisOptions.Grid"/>. Additional Chart.js grid options,
	/// such as <c>drawOnChartArea</c>, <c>drawTicks</c>, <c>tickLength</c> or <c>offset</c>, can be supplied
	/// through <see cref="ExtensionData"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var grid = chart.ChartOptions.Scales.Y.Grid;
	/// grid.Color = "rgba(0, 0, 0, 0.1)";
	/// grid.LineWidth = 2;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class GridOptions : OptionsBase
	{
		private bool _display = true;
		private object? _color;
		private int _lineWidth = 1;
		private bool _drawBorder = true;
		private bool _circular;

		/// <summary>
		/// Returns or sets a value indicating whether the grid lines of the axis are drawn (Chart.js <c>grid.display</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to draw the grid lines; otherwise, <c>false</c>. The default is <c>true</c>.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// bool gridVisible = chart.ChartOptions.Scales.X.Grid.Display;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Display grid lines.")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Returns or sets the color of the grid lines (Chart.js <c>grid.color</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors (the first color is
		/// applied to the first grid line, the second to the second, and so on), or <c>null</c> (default)
		/// to use the Chart.js default color.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Grid.Color = System.Drawing.Color.LightGray;
		/// chart.ChartOptions.Scales.X.Grid.Color = new[] { "red", "green", "blue" };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("color")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Grid line color.")]
		public object? Color
		{
			get => _color;
			set => SetProperty(ref _color, value);
		}

		/// <summary>
		/// Returns or sets the stroke width of the grid lines, in pixels (Chart.js <c>grid.lineWidth</c>).
		/// </summary>
		/// <value>
		/// The grid line width in pixels. The default is <c>1</c>.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Grid.LineWidth = 2;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("lineWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(1)]
		[Description("Grid line width.")]
		public int LineWidth
		{
			get => _lineWidth;
			set => SetProperty(ref _lineWidth, value);
		}

		/// <summary>
		/// Returns or sets a value indicating whether a border is drawn at the edge between the axis and
		/// the chart area (Chart.js 3 <c>grid.drawBorder</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to draw the border; otherwise, <c>false</c>. The default is <c>true</c>.
		/// </value>
		/// <remarks>
		/// In Chart.js 4 this option was replaced by <c>border.display</c>; use
		/// <see cref="AxisOptions.Border"/> and <see cref="BorderOptions.Display"/> to control the axis border.
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// bool drawBorder = chart.ChartOptions.Scales.X.Grid.DrawBorder;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("drawBorder")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Draw border at edge.")]
		public bool DrawBorder
		{
			get => _drawBorder;
			set => SetProperty(ref _drawBorder, value);
		}

		/// <summary>
		/// Returns or sets a value indicating whether the grid lines are circular (Chart.js <c>grid.circular</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to draw circular grid lines; otherwise, <c>false</c> to draw polygonal lines. The default is <c>false</c>.
		/// </value>
		/// <remarks>
		/// Applies only to radial scales (radar and polar area charts). Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Grid.Circular = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("circular")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Circular grid lines (radar only).")]
		public bool Circular
		{
			get => _circular;
			set => SetProperty(ref _circular, value);
		}

		/// <summary>
		/// Returns or sets additional, arbitrary Chart.js grid options that are not exposed as typed properties.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c> (default).
		/// </value>
		/// <remarks>
		/// This property is marked with <c>[JsonExtensionData]</c>: each entry is written as a sibling
		/// property of the <c>grid</c> JSON object sent to Chart.js (e.g. <c>drawOnChartArea</c>, <c>drawTicks</c>,
		/// <c>tickLength</c>). It is hidden from the designer and property grid.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Grid.ExtensionData = new Dictionary<string, object>
		/// {
		///     { "drawOnChartArea", false },
		///     { "tickLength", 8 }
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
		/// Determines whether the <see cref="Display"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Display"/> is not <c>true</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetDisplay"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var grid = new GridOptions();
		/// if (grid.ShouldSerializeDisplay())
		///     grid.ResetDisplay();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDisplay() => Display != true;

		/// <summary>
		/// Resets the <see cref="Display"/> property to its default value (<c>true</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var grid = new GridOptions();
		/// grid.ResetDisplay();
		/// ]]></code>
		/// </example>
		public void ResetDisplay() => Display = true;

		/// <summary>
		/// Determines whether the <see cref="Color"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Color"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetColor"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var grid = new GridOptions();
		/// if (grid.ShouldSerializeColor())
		///     grid.ResetColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeColor() => Color != null;

		/// <summary>
		/// Resets the <see cref="Color"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var grid = new GridOptions { Color = "#eeeeee" };
		/// grid.ResetColor();
		/// ]]></code>
		/// </example>
		public void ResetColor() => Color = null;

		/// <summary>
		/// Determines whether the <see cref="LineWidth"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="LineWidth"/> is not <c>1</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetLineWidth"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var grid = new GridOptions();
		/// if (grid.ShouldSerializeLineWidth())
		///     grid.ResetLineWidth();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeLineWidth() => LineWidth != 1;

		/// <summary>
		/// Resets the <see cref="LineWidth"/> property to its default value (<c>1</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var grid = new GridOptions { LineWidth = 3 };
		/// grid.ResetLineWidth();
		/// ]]></code>
		/// </example>
		public void ResetLineWidth() => LineWidth = 1;

		/// <summary>
		/// Determines whether the <see cref="DrawBorder"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="DrawBorder"/> is not <c>true</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetDrawBorder"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var grid = new GridOptions();
		/// if (grid.ShouldSerializeDrawBorder())
		///     grid.ResetDrawBorder();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDrawBorder() => DrawBorder != true;

		/// <summary>
		/// Resets the <see cref="DrawBorder"/> property to its default value (<c>true</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var grid = new GridOptions();
		/// grid.ResetDrawBorder();
		/// ]]></code>
		/// </example>
		public void ResetDrawBorder() => DrawBorder = true;

		/// <summary>
		/// Determines whether the <see cref="Circular"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Circular"/> is <c>true</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetCircular"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var grid = new GridOptions();
		/// if (grid.ShouldSerializeCircular())
		///     grid.ResetCircular();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeCircular() => Circular != false;

		/// <summary>
		/// Resets the <see cref="Circular"/> property to its default value (<c>false</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var grid = new GridOptions { Circular = true };
		/// grid.ResetCircular();
		/// ]]></code>
		/// </example>
		public void ResetCircular() => Circular = false;

	}
}
