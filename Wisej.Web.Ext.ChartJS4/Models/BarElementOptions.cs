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
	/// Represents the default styling options of the bar elements used by bar charts (Chart.js <c>options.elements.bar</c>).
	/// </summary>
	/// <remarks>
	/// An instance is available through <see cref="ElementsOptions.Bar"/>. These values apply to all bar datasets unless overridden by the dataset options.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var bar = new BarElementOptions();
	/// bar.BorderWidth = 1;
	/// bar.BorderRadius = 6;
	/// chart.ChartOptions.Elements = new ElementsOptions { Bar = bar };
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class BarElementOptions : OptionsBase
	{
		private object? _backgroundColor;
		private int _borderWidth;
		private int _borderRadius;

		/// <summary>
		/// Returns or sets the default fill color of the bars (Chart.js option <c>backgroundColor</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors. The default is <c>null</c>, which uses the Chart.js default color.
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var bar = new BarElementOptions();
		/// bar.BackgroundColor = "rgba(54, 162, 235, 0.5)";
		/// chart.ChartOptions.Elements = new ElementsOptions { Bar = bar };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("backgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Bar background color.")]
		public object? BackgroundColor
		{
			get => _backgroundColor;
			set => SetProperty(ref _backgroundColor, value);
		}

		/// <summary>
		/// Returns or sets the default border width, in pixels, of the bars (Chart.js option <c>borderWidth</c>).
		/// </summary>
		/// <value>
		/// The border width in pixels. The default is <c>0</c> (no border).
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var bar = new BarElementOptions();
		/// bar.BorderWidth = 2;
		/// chart.ChartOptions.Elements = new ElementsOptions { Bar = bar };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Bar border width.")]
		public int BorderWidth
		{
			get => _borderWidth;
			set => SetProperty(ref _borderWidth, value);
		}

		/// <summary>
		/// Returns or sets the default corner radius, in pixels, of the bars (Chart.js option <c>borderRadius</c>).
		/// </summary>
		/// <value>
		/// The corner radius in pixels. The default is <c>0</c> (square corners).
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var bar = new BarElementOptions();
		/// bar.BorderRadius = 8;
		/// chart.ChartOptions.Elements = new ElementsOptions { Bar = bar };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderRadius")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Bar border radius.")]
		public int BorderRadius
		{
			get => _borderRadius;
			set => SetProperty(ref _borderRadius, value);
		}

		/// <summary>
		/// Returns or sets a dictionary of additional Chart.js options that are not exposed as typed properties of the bar element options.
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
		/// var bar = new BarElementOptions();
		/// bar.ExtensionData = new System.Collections.Generic.Dictionary<string, object>
		/// {
		///     ["borderSkipped"] = "start"
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
		/// Returns whether the <see cref="BackgroundColor"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BackgroundColor"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (bar.ShouldSerializeBackgroundColor())
		///     bar.ResetBackgroundColor();
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
		/// var bar = new BarElementOptions();
		/// bar.ResetBackgroundColor();
		/// ]]></code>
		/// </example>
		public void ResetBackgroundColor() => BackgroundColor = null;

		/// <summary>
		/// Returns whether the <see cref="BorderWidth"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BorderWidth"/> is not <c>0</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (bar.ShouldSerializeBorderWidth())
		///     bar.ResetBorderWidth();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBorderWidth() => BorderWidth != default;

		/// <summary>
		/// Resets the <see cref="BorderWidth"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="BorderWidth"/> to <c>0</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bar = new BarElementOptions();
		/// bar.ResetBorderWidth();
		/// ]]></code>
		/// </example>
		public void ResetBorderWidth() => BorderWidth = 0;

		/// <summary>
		/// Returns whether the <see cref="BorderRadius"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BorderRadius"/> is not <c>0</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (bar.ShouldSerializeBorderRadius())
		///     bar.ResetBorderRadius();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBorderRadius() => BorderRadius != default;

		/// <summary>
		/// Resets the <see cref="BorderRadius"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="BorderRadius"/> to <c>0</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bar = new BarElementOptions();
		/// bar.ResetBorderRadius();
		/// ]]></code>
		/// </example>
		public void ResetBorderRadius() => BorderRadius = 0;

	}
}
