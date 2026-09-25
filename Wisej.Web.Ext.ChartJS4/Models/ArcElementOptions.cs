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
	/// Represents the default styling options of the arc elements used by pie, doughnut and polar area charts (Chart.js <c>options.elements.arc</c>).
	/// </summary>
	/// <remarks>
	/// An instance is available through <see cref="ElementsOptions.Arc"/>. These values apply to all arc datasets unless overridden by the dataset options.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var arc = new ArcElementOptions();
	/// arc.BorderWidth = 1;
	/// arc.BorderColor = System.Drawing.Color.White;
	/// chart.ChartOptions.Elements = new ElementsOptions { Arc = arc };
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class ArcElementOptions : OptionsBase
	{
		private object? _backgroundColor;
		private int _borderWidth = 2;
		private object? _borderColor;

		/// <summary>
		/// Returns or sets the default fill color of the arcs (Chart.js option <c>backgroundColor</c>).
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
		/// var arc = new ArcElementOptions();
		/// arc.BackgroundColor = new[] { "#36a2eb", "#ff6384", "#ffce56" };
		/// chart.ChartOptions.Elements = new ElementsOptions { Arc = arc };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("backgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Arc background color.")]
		public object? BackgroundColor
		{
			get => _backgroundColor;
			set => SetProperty(ref _backgroundColor, value);
		}

		/// <summary>
		/// Returns or sets the default border width, in pixels, of the arcs (Chart.js option <c>borderWidth</c>).
		/// </summary>
		/// <value>
		/// The border width in pixels. The default is <c>2</c>.
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var arc = new ArcElementOptions();
		/// arc.BorderWidth = 1;
		/// chart.ChartOptions.Elements = new ElementsOptions { Arc = arc };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(2)]
		[Description("Arc border width.")]
		public int BorderWidth
		{
			get => _borderWidth;
			set => SetProperty(ref _borderWidth, value);
		}

		/// <summary>
		/// Returns or sets the default border color of the arcs (Chart.js option <c>borderColor</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors. The default is <c>null</c>, which uses the Chart.js default (<c>"#fff"</c>).
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var arc = new ArcElementOptions();
		/// arc.BorderColor = System.Drawing.Color.White;
		/// chart.ChartOptions.Elements = new ElementsOptions { Arc = arc };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Arc border color.")]
		public object? BorderColor
		{
			get => _borderColor;
			set => SetProperty(ref _borderColor, value);
		}

		/// <summary>
		/// Returns or sets a dictionary of additional Chart.js options that are not exposed as typed properties of the arc element options.
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
		/// var arc = new ArcElementOptions();
		/// arc.ExtensionData = new System.Collections.Generic.Dictionary<string, object>
		/// {
		///     ["borderAlign"] = "inner"
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
		/// if (arc.ShouldSerializeBackgroundColor())
		///     arc.ResetBackgroundColor();
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
		/// var arc = new ArcElementOptions();
		/// arc.ResetBackgroundColor();
		/// ]]></code>
		/// </example>
		public void ResetBackgroundColor() => BackgroundColor = null;

		/// <summary>
		/// Returns whether the <see cref="BorderWidth"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BorderWidth"/> is not <c>2</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (arc.ShouldSerializeBorderWidth())
		///     arc.ResetBorderWidth();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBorderWidth() => BorderWidth != 2;

		/// <summary>
		/// Resets the <see cref="BorderWidth"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="BorderWidth"/> to <c>2</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var arc = new ArcElementOptions();
		/// arc.ResetBorderWidth();
		/// ]]></code>
		/// </example>
		public void ResetBorderWidth() => BorderWidth = 2;

		/// <summary>
		/// Returns whether the <see cref="BorderColor"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BorderColor"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (arc.ShouldSerializeBorderColor())
		///     arc.ResetBorderColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBorderColor() => BorderColor != null;

		/// <summary>
		/// Resets the <see cref="BorderColor"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="BorderColor"/> to <c>null</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var arc = new ArcElementOptions();
		/// arc.ResetBorderColor();
		/// ]]></code>
		/// </example>
		public void ResetBorderColor() => BorderColor = null;

	}
}
