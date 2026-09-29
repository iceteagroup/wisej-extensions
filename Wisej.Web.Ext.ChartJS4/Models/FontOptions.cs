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
	/// Represents the font options used to render text elements of the chart (Chart.js font object with
	/// <c>family</c>, <c>size</c>, <c>style</c>, <c>weight</c> and <c>lineHeight</c>).
	/// </summary>
	/// <remarks>
	/// Used by several option classes, such as <see cref="TickOptions.Font"/>, <see cref="AxisTitleOptions.Font"/>,
	/// <see cref="TitleOptions.Font"/> and <see cref="LegendLabelsOptions.Font"/>. Properties left at
	/// <c>null</c> are not serialized, letting Chart.js apply its global font defaults.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var font = chart.ChartOptions.Scales.X.Ticks.Font;
	/// font.Family = "Segoe UI";
	/// font.Size = 14;
	/// font.Weight = "bold";
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class FontOptions : OptionsBase
	{
		private string? _family;
		private int _size = 12;
		private string? _style;
		private object? _weight;
		private object? _lineHeight;

		/// <summary>
		/// Returns or sets the font family (Chart.js <c>font.family</c>).
		/// </summary>
		/// <value>
		/// A CSS font-family string, e.g. <c>"Arial"</c> or <c>"'Helvetica Neue', Helvetica, sans-serif"</c>,
		/// or <c>null</c> (default) to use the Chart.js default font family.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Ticks.Font.Family = "Verdana, sans-serif";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("family")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Font family.")]
		public string? Family
		{
			get => _family;
			set => SetProperty(ref _family, value);
		}

		/// <summary>
		/// Returns or sets the font size, in pixels (Chart.js <c>font.size</c>).
		/// </summary>
		/// <value>
		/// The font size in pixels. The default is <c>12</c>.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Title.Font.Size = 16;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("size")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(12)]
		[Description("Font size.")]
		public int Size
		{
			get => _size;
			set => SetProperty(ref _size, value);
		}

		/// <summary>
		/// Returns or sets the font style (Chart.js <c>font.style</c>).
		/// </summary>
		/// <value>
		/// One of <c>"normal"</c>, <c>"italic"</c>, <c>"oblique"</c>, <c>"initial"</c> or <c>"inherit"</c>,
		/// or <c>null</c> (default) to use the Chart.js default (<c>"normal"</c>).
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Title.Font.Style = "italic";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("style")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Font style.")]
		public string? Style
		{
			get => _style;
			set => SetProperty(ref _style, value);
		}

		/// <summary>
		/// Returns or sets the font weight (Chart.js <c>font.weight</c>).
		/// </summary>
		/// <value>
		/// A string such as <c>"normal"</c>, <c>"bold"</c>, <c>"lighter"</c> or <c>"bolder"</c>, a numeric
		/// weight (e.g. <c>600</c>), or <c>null</c> (default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Ticks.Font.Weight = "bold";
		/// chart.ChartOptions.Scales.Y.Ticks.Font.Weight = 600;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("weight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Font weight.")]
		public object? Weight
		{
			get => _weight;
			set => SetProperty(ref _weight, value);
		}

		/// <summary>
		/// Returns or sets the height of an individual line of text (Chart.js <c>font.lineHeight</c>).
		/// </summary>
		/// <value>
		/// A number used as a multiplier of the font size (e.g. <c>1.2</c>), a CSS length string
		/// (e.g. <c>"20px"</c> or <c>"150%"</c>), or <c>null</c> (default) to use the Chart.js default (<c>1.2</c>).
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Title.Font.LineHeight = 1.5;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("lineHeight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Line height.")]
		public object? LineHeight
		{
			get => _lineHeight;
			set => SetProperty(ref _lineHeight, value);
		}

		/// <summary>
		/// Returns or sets additional, arbitrary Chart.js font options that are not exposed as typed properties.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c> (default).
		/// </value>
		/// <remarks>
		/// This property is marked with <c>[JsonExtensionData]</c>: each entry is written as a sibling
		/// property of the <c>font</c> JSON object sent to Chart.js. It is hidden from the designer and property grid.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Ticks.Font.ExtensionData = new Dictionary<string, object>
		/// {
		///     { "variant", "small-caps" }
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
		/// Determines whether the <see cref="Family"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Family"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetFamily"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var font = new FontOptions();
		/// if (font.ShouldSerializeFamily())
		///     font.ResetFamily();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeFamily() => Family != null;

		/// <summary>
		/// Resets the <see cref="Family"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var font = new FontOptions { Family = "Arial" };
		/// font.ResetFamily();
		/// ]]></code>
		/// </example>
		public void ResetFamily() => Family = null;

		/// <summary>
		/// Determines whether the <see cref="Size"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Size"/> is not <c>12</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetSize"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var font = new FontOptions();
		/// if (font.ShouldSerializeSize())
		///     font.ResetSize();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeSize() => Size != 12;

		/// <summary>
		/// Resets the <see cref="Size"/> property to its default value (<c>12</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var font = new FontOptions { Size = 20 };
		/// font.ResetSize();
		/// ]]></code>
		/// </example>
		public void ResetSize() => Size = 12;

		/// <summary>
		/// Determines whether the <see cref="Style"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Style"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetStyle"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var font = new FontOptions();
		/// if (font.ShouldSerializeStyle())
		///     font.ResetStyle();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeStyle() => Style != null;

		/// <summary>
		/// Resets the <see cref="Style"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var font = new FontOptions { Style = "italic" };
		/// font.ResetStyle();
		/// ]]></code>
		/// </example>
		public void ResetStyle() => Style = null;

		/// <summary>
		/// Determines whether the <see cref="Weight"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Weight"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetWeight"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var font = new FontOptions();
		/// if (font.ShouldSerializeWeight())
		///     font.ResetWeight();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeWeight() => Weight != null;

		/// <summary>
		/// Resets the <see cref="Weight"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var font = new FontOptions { Weight = "bold" };
		/// font.ResetWeight();
		/// ]]></code>
		/// </example>
		public void ResetWeight() => Weight = null;

		/// <summary>
		/// Determines whether the <see cref="LineHeight"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="LineHeight"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetLineHeight"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var font = new FontOptions();
		/// if (font.ShouldSerializeLineHeight())
		///     font.ResetLineHeight();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeLineHeight() => LineHeight != null;

		/// <summary>
		/// Resets the <see cref="LineHeight"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var font = new FontOptions { LineHeight = 1.5 };
		/// font.ResetLineHeight();
		/// ]]></code>
		/// </example>
		public void ResetLineHeight() => LineHeight = null;

	}
}
