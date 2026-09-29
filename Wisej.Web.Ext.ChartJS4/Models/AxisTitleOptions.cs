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
	/// Represents the options of an axis title, the label displayed along an axis (Chart.js <c>scales[id].title</c>).
	/// </summary>
	/// <remarks>
	/// An instance is lazily created by <see cref="AxisOptions.Title"/>. The title is hidden by default:
	/// set <see cref="Display"/> to <c>true</c> and assign <see cref="Text"/> to show it. Additional Chart.js
	/// options, such as <c>align</c>, can be supplied through <see cref="ExtensionData"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var title = chart.ChartOptions.Scales.Y.Title;
	/// title.Display = true;
	/// title.Text = "Revenue (USD)";
	/// title.Color = System.Drawing.Color.DarkBlue;
	/// title.Font.Size = 14;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class AxisTitleOptions : OptionsBase
	{
		private bool _display;
		private string? _text;
		private object? _color;
		private FontOptions? _font;
		private int _padding;

		/// <summary>
		/// Returns or sets a value indicating whether the axis title is displayed (Chart.js <c>title.display</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to display the title; otherwise, <c>false</c>. The default is <c>false</c>.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Title.Display = true;
		/// chart.ChartOptions.Scales.X.Title.Text = "Month";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Display axis title.")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Returns or sets the text of the axis title (Chart.js <c>title.text</c>).
		/// </summary>
		/// <value>
		/// The title text, or <c>null</c> (default) for no text.
		/// </value>
		/// <remarks>
		/// The text is shown only when <see cref="Display"/> is <c>true</c>. Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Title.Display = true;
		/// chart.ChartOptions.Scales.Y.Title.Text = "Temperature (°C)";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("text")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Title text.")]
		public string? Text
		{
			get => _text;
			set => SetProperty(ref _text, value);
		}

		/// <summary>
		/// Returns or sets the color of the axis title text (Chart.js <c>title.color</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/> or a CSS color string, or <c>null</c> (default) to use the
		/// Chart.js default font color.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Title.Color = "#336699";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("color")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Title text color.")]
		public object? Color
		{
			get => _color;
			set => SetProperty(ref _color, value);
		}

		/// <summary>
		/// Returns or sets the font used to render the axis title (Chart.js <c>title.font</c>).
		/// </summary>
		/// <value>
		/// A <see cref="FontOptions"/> instance. The getter lazily creates a default instance when none is set.
		/// </value>
		/// <remarks>
		/// Changing any of the font values refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Title.Font.Size = 16;
		/// chart.ChartOptions.Scales.X.Title.Font.Weight = "bold";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("font")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Font configuration.")]
		public FontOptions? Font
		{
			get
			{
				if (_font == null)
					_font = new FontOptions { Chart = Chart };
				return _font;
			}
			set => SetProperty(ref _font, value);
		}

		/// <summary>
		/// Returns or sets the padding around the axis title, in pixels (Chart.js <c>title.padding</c>).
		/// </summary>
		/// <value>
		/// The padding in pixels. The default is <c>0</c>, which is not serialized so that Chart.js applies its own default (<c>4</c>).
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Title.Padding = 10;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Padding around title.")]
		public int Padding
		{
			get => _padding;
			set => SetProperty(ref _padding, value);
		}

		/// <summary>
		/// Returns or sets additional, arbitrary Chart.js axis title options that are not exposed as typed properties.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c> (default).
		/// </value>
		/// <remarks>
		/// This property is marked with <c>[JsonExtensionData]</c>: each entry is written as a sibling
		/// property of the <c>title</c> JSON object sent to Chart.js (e.g. <c>align</c>: <c>"start"</c> | <c>"center"</c> | <c>"end"</c>).
		/// It is hidden from the designer and property grid.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Title.ExtensionData = new Dictionary<string, object>
		/// {
		///     { "align", "end" }
		/// };
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[JsonExtensionData]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <inheritdoc/>
		protected override void OnChartChanged()
		{
			if (_font != null)
				_font.Chart = Chart;
		}

		/// <summary>
		/// Determines whether the <see cref="Display"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Display"/> is <c>true</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetDisplay"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = new AxisTitleOptions();
		/// if (title.ShouldSerializeDisplay())
		///     title.ResetDisplay();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDisplay() => Display != false;

		/// <summary>
		/// Resets the <see cref="Display"/> property to its default value (<c>false</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var title = new AxisTitleOptions { Display = true };
		/// title.ResetDisplay();
		/// ]]></code>
		/// </example>
		public void ResetDisplay() => Display = false;

		/// <summary>
		/// Determines whether the <see cref="Text"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Text"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetText"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = new AxisTitleOptions();
		/// if (title.ShouldSerializeText())
		///     title.ResetText();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeText() => Text != null;

		/// <summary>
		/// Resets the <see cref="Text"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var title = new AxisTitleOptions { Text = "Sales" };
		/// title.ResetText();
		/// ]]></code>
		/// </example>
		public void ResetText() => Text = null;

		/// <summary>
		/// Determines whether the <see cref="Color"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Color"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetColor"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = new AxisTitleOptions();
		/// if (title.ShouldSerializeColor())
		///     title.ResetColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeColor() => Color != null;

		/// <summary>
		/// Resets the <see cref="Color"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var title = new AxisTitleOptions { Color = "gray" };
		/// title.ResetColor();
		/// ]]></code>
		/// </example>
		public void ResetColor() => Color = null;

		/// <summary>
		/// Determines whether the <see cref="Font"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Font"/> has at least one non-default value; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetFont"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = new AxisTitleOptions();
		/// if (title.ShouldSerializeFont())
		///     title.ResetFont();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeFont() => Font != null && !Font.IsDefault;

		/// <summary>
		/// Resets the <see cref="Font"/> property to its default value by discarding the current <see cref="FontOptions"/> instance.
		/// </summary>
		/// <remarks>
		/// A new default instance is created the next time <see cref="Font"/> is read.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = new AxisTitleOptions();
		/// title.Font.Size = 18;
		/// title.ResetFont();
		/// ]]></code>
		/// </example>
		public void ResetFont() => SetProperty(ref _font, null);

		/// <summary>
		/// Determines whether the <see cref="Padding"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Padding"/> is not <c>0</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetPadding"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = new AxisTitleOptions();
		/// if (title.ShouldSerializePadding())
		///     title.ResetPadding();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePadding() => Padding != default;

		/// <summary>
		/// Resets the <see cref="Padding"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var title = new AxisTitleOptions { Padding = 8 };
		/// title.ResetPadding();
		/// ]]></code>
		/// </example>
		public void ResetPadding() => Padding = default;

	}
}
