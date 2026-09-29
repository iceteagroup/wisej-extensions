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
	/// Represents the options of the axis border line, drawn at the edge between the axis and
	/// the chart area (Chart.js <c>scales[id].border</c>).
	/// </summary>
	/// <remarks>
	/// An instance is lazily created by <see cref="AxisOptions.Border"/>. Additional Chart.js border
	/// options, such as <c>dash</c>, <c>dashOffset</c> or <c>z</c>, can be supplied through <see cref="ExtensionData"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var border = chart.ChartOptions.Scales.Y.Border;
	/// border.Color = System.Drawing.Color.Gray;
	/// border.Width = 2;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class BorderOptions : OptionsBase
	{
		private bool _display = true;
		private object? _color;
		private int _width = 1;

		/// <summary>
		/// Returns or sets a value indicating whether the axis border line is drawn (Chart.js <c>border.display</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to draw the border; otherwise, <c>false</c>. The default is <c>true</c>.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// bool borderVisible = chart.ChartOptions.Scales.X.Border.Display;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Display border.")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Returns or sets the color of the axis border line (Chart.js <c>border.color</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/> or a CSS color string (e.g. <c>"#ff0000"</c> or <c>"rgba(0,0,0,0.3)"</c>),
		/// or <c>null</c> (default) to use the Chart.js default color.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Border.Color = "rgba(0, 0, 0, 0.5)";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("color")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Border color.")]
		public object? Color
		{
			get => _color;
			set => SetProperty(ref _color, value);
		}

		/// <summary>
		/// Returns or sets the width of the axis border line, in pixels (Chart.js <c>border.width</c>).
		/// </summary>
		/// <value>
		/// The border width in pixels. The default is <c>1</c>.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Border.Width = 3;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("width")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(1)]
		[Description("Border width.")]
		public int Width
		{
			get => _width;
			set => SetProperty(ref _width, value);
		}

		/// <summary>
		/// Returns or sets additional, arbitrary Chart.js axis border options that are not exposed as typed properties.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c> (default).
		/// </value>
		/// <remarks>
		/// This property is marked with <c>[JsonExtensionData]</c>: each entry is written as a sibling
		/// property of the <c>border</c> JSON object sent to Chart.js (e.g. <c>dash</c>, <c>dashOffset</c>, <c>z</c>).
		/// It is hidden from the designer and property grid.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Border.ExtensionData = new Dictionary<string, object>
		/// {
		///     { "dash", new[] { 4, 4 } }
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
		/// var border = new BorderOptions();
		/// if (border.ShouldSerializeDisplay())
		///     border.ResetDisplay();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDisplay() => Display != true;

		/// <summary>
		/// Resets the <see cref="Display"/> property to its default value (<c>true</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var border = new BorderOptions();
		/// border.ResetDisplay();
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
		/// var border = new BorderOptions();
		/// if (border.ShouldSerializeColor())
		///     border.ResetColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeColor() => Color != null;

		/// <summary>
		/// Resets the <see cref="Color"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var border = new BorderOptions { Color = "red" };
		/// border.ResetColor();
		/// ]]></code>
		/// </example>
		public void ResetColor() => Color = null;

		/// <summary>
		/// Determines whether the <see cref="Width"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Width"/> is not <c>1</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetWidth"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var border = new BorderOptions();
		/// if (border.ShouldSerializeWidth())
		///     border.ResetWidth();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeWidth() => Width != 1;

		/// <summary>
		/// Resets the <see cref="Width"/> property to its default value (<c>1</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var border = new BorderOptions { Width = 3 };
		/// border.ResetWidth();
		/// ]]></code>
		/// </example>
		public void ResetWidth() => Width = 1;

	}
}
