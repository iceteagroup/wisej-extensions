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
	/// Represents the default styling options of the line elements used by line and radar charts (Chart.js <c>options.elements.line</c>).
	/// </summary>
	/// <remarks>
	/// An instance is available through <see cref="ElementsOptions.Line"/>. These values apply to all line datasets unless overridden by the dataset options.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var line = new LineElementOptions();
	/// line.Tension = 0.4;
	/// line.BorderWidth = 2;
	/// chart.ChartOptions.Elements = new ElementsOptions { Line = line };
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class LineElementOptions : OptionsBase
	{
		private double _tension;
		private object? _backgroundColor;
		private int _borderWidth = 3;
		private string? _borderCapStyle;

		/// <summary>
		/// Returns or sets the Bezier curve tension of the lines (Chart.js option <c>tension</c>).
		/// </summary>
		/// <value>
		/// A value typically between <c>0</c> and <c>1</c>. The default is <c>0</c>, which draws straight lines.
		/// </value>
		/// <remarks>
		/// Values such as <c>0.4</c> produce smooth curves.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var line = new LineElementOptions();
		/// line.Tension = 0.4;
		/// chart.ChartOptions.Elements = new ElementsOptions { Line = line };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("tension")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Line tension (Bezier curve).")]
		public double Tension
		{
			get => _tension;
			set => SetProperty(ref _tension, value);
		}

		/// <summary>
		/// Returns or sets the default fill color of the area under the lines (Chart.js option <c>backgroundColor</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors. The default is <c>null</c>, which uses the Chart.js default color.
		/// </value>
		/// <remarks>
		/// The area is filled only when the dataset <c>fill</c> option is enabled.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var line = new LineElementOptions();
		/// line.BackgroundColor = "rgba(75, 192, 192, 0.2)";
		/// chart.ChartOptions.Elements = new ElementsOptions { Line = line };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("backgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Line background color.")]
		public object? BackgroundColor
		{
			get => _backgroundColor;
			set => SetProperty(ref _backgroundColor, value);
		}

		/// <summary>
		/// Returns or sets the default width, in pixels, of the lines (Chart.js option <c>borderWidth</c>).
		/// </summary>
		/// <value>
		/// The line width in pixels. The default is <c>3</c>.
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var line = new LineElementOptions();
		/// line.BorderWidth = 2;
		/// chart.ChartOptions.Elements = new ElementsOptions { Line = line };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(3)]
		[Description("Line border width.")]
		public int BorderWidth
		{
			get => _borderWidth;
			set => SetProperty(ref _borderWidth, value);
		}

		/// <summary>
		/// Returns or sets the cap style of the line ends (Chart.js option <c>borderCapStyle</c>, canvas <c>lineCap</c>).
		/// </summary>
		/// <value>
		/// <c>"butt"</c>, <c>"round"</c> or <c>"square"</c>, or <c>null</c> (default) to use the Chart.js default (<c>"butt"</c>).
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var line = new LineElementOptions();
		/// line.BorderCapStyle = "round";
		/// chart.ChartOptions.Elements = new ElementsOptions { Line = line };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderCapStyle")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Cap style of the line.")]
		public string? BorderCapStyle
		{
			get => _borderCapStyle;
			set => SetProperty(ref _borderCapStyle, value);
		}

		/// <summary>
		/// Returns or sets a dictionary of additional Chart.js options that are not exposed as typed properties of the line element options.
		/// </summary>
		/// <value>
		/// A <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/> of option names and values, or <c>null</c> (default).
		/// </value>
		/// <remarks>
		/// The dictionary is marked with <c>[JsonExtensionData]</c>: each entry is serialized as an additional top-level property of this options object, using the key as the JSON property name. Use it to set any Chart.js option not covered by the typed API.
		/// This property is hidden from the property grid and is not persisted by the designer. Assigning the property does not refresh the chart automatically; the new values are sent with the next chart update.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineElementOptions();
		/// line.ExtensionData = new System.Collections.Generic.Dictionary<string, object>
		/// {
		///     ["stepped"] = true
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
		/// Returns whether the <see cref="Tension"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Tension"/> is not <c>0</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (line.ShouldSerializeTension())
		///     line.ResetTension();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTension() => Tension != 0;

		/// <summary>
		/// Resets the <see cref="Tension"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Tension"/> to <c>0</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineElementOptions();
		/// line.ResetTension();
		/// ]]></code>
		/// </example>
		public void ResetTension() => Tension = 0;

		/// <summary>
		/// Returns whether the <see cref="BackgroundColor"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BackgroundColor"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (line.ShouldSerializeBackgroundColor())
		///     line.ResetBackgroundColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBackgroundColor() => BackgroundColor != null;

		/// <summary>
		/// Resets the <see cref="BackgroundColor"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="BackgroundColor"/> to <c>null</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineElementOptions();
		/// line.ResetBackgroundColor();
		/// ]]></code>
		/// </example>
		public void ResetBackgroundColor() => BackgroundColor = null;

		/// <summary>
		/// Returns whether the <see cref="BorderWidth"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BorderWidth"/> is not <c>3</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (line.ShouldSerializeBorderWidth())
		///     line.ResetBorderWidth();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBorderWidth() => BorderWidth != 3;

		/// <summary>
		/// Resets the <see cref="BorderWidth"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="BorderWidth"/> to <c>3</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineElementOptions();
		/// line.ResetBorderWidth();
		/// ]]></code>
		/// </example>
		public void ResetBorderWidth() => BorderWidth = 3;

		/// <summary>
		/// Returns whether the <see cref="BorderCapStyle"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BorderCapStyle"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (line.ShouldSerializeBorderCapStyle())
		///     line.ResetBorderCapStyle();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBorderCapStyle() => BorderCapStyle != null;

		/// <summary>
		/// Resets the <see cref="BorderCapStyle"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="BorderCapStyle"/> to <c>null</c> (Chart.js default).
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineElementOptions();
		/// line.ResetBorderCapStyle();
		/// ]]></code>
		/// </example>
		public void ResetBorderCapStyle() => BorderCapStyle = null;

	}
}
