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
	/// Represents the configuration of the Chart.js subtitle plugin (<c>options.plugins.subtitle</c>),
	/// which draws a second line of text below the chart title.
	/// </summary>
	/// <remarks>
	/// An instance is created lazily by <see cref="PluginsOptions.Subtitle"/>. The subtitle is only drawn when
	/// <see cref="Display"/> is <c>true</c>. Changing any property updates the chart. Options that are not
	/// exposed as properties can be added through <see cref="ExtensionData"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var subtitle = chart.ChartOptions.Plugins.Subtitle;
	/// subtitle.Display = true;
	/// subtitle.Text = "Figures in thousands of USD";
	/// subtitle.Font.Style = "italic";
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class SubtitleOptions : OptionsBase
	{
		private bool _display;
		private object? _text;
		private object? _color;
		private FontOptions? _font;
		private int _padding;

		/// <summary>
		/// Returns or sets whether the subtitle is shown (Chart.js <c>display</c>).
		/// </summary>
		/// <value><c>true</c> to show the subtitle; otherwise <c>false</c>. The default is <c>false</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Subtitle.Display = true;
		/// chart.ChartOptions.Plugins.Subtitle.Text = "Q1 2026";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Is the subtitle shown?")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Returns or sets the subtitle text (Chart.js <c>text</c>).
		/// </summary>
		/// <value>
		/// A <see cref="string"/>, or an array of strings to render the subtitle on multiple lines.
		/// The default is <c>null</c>.
		/// </value>
		/// <remarks>The subtitle is only drawn when <see cref="Display"/> is <c>true</c>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Subtitle.Display = true;
		/// chart.ChartOptions.Plugins.Subtitle.Text = "Figures in thousands of USD";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("text")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Subtitle text.")]
		public object? Text
		{
			get => _text;
			set => SetProperty(ref _text, value);
		}

		/// <summary>
		/// Returns or sets the color of the subtitle text (Chart.js <c>color</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/> or a CSS color string. The default is <c>null</c>, which uses the
		/// Chart.js default font color.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Subtitle.Color = "#888888";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("color")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Text color.")]
		public object? Color
		{
			get => _color;
			set => SetProperty(ref _color, value);
		}

		/// <summary>
		/// Returns or sets the font configuration of the subtitle (Chart.js <c>font</c>).
		/// </summary>
		/// <value>
		/// A <see cref="FontOptions"/> instance. The getter creates an empty instance on first access, so nested
		/// properties can be set directly.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Subtitle.Font.Size = 12;
		/// chart.ChartOptions.Plugins.Subtitle.Font.Style = "italic";
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
		/// Returns or sets the padding, in pixels, applied around the subtitle (Chart.js <c>padding</c>).
		/// </summary>
		/// <value>
		/// The padding in pixels. The default is <c>0</c>, which is not serialized, so Chart.js uses its default.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Subtitle.Padding = 10;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Padding around subtitle.")]
		public int Padding
		{
			get => _padding;
			set => SetProperty(ref _padding, value);
		}

		/// <summary>
		/// Returns or sets additional custom Chart.js subtitle options that are not exposed as properties.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c> (the default).
		/// </value>
		/// <remarks>
		/// This property is marked with <c>[JsonExtensionData]</c>: each entry is written as a property of the
		/// <c>subtitle</c> JSON object, next to the typed properties. It is hidden from the designer and not
		/// serialized in the designer code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var subtitle = chart.ChartOptions.Plugins.Subtitle;
		/// subtitle.ExtensionData = new Dictionary<string, object>();
		/// subtitle.ExtensionData["position"] = "bottom";
		/// subtitle.ExtensionData["align"] = "end";
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
			if (_font != null)
				_font.Chart = Chart;
		}

		/// <summary>
		/// Determines whether the <see cref="Display"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Display"/> is not <c>false</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetDisplay"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var subtitle = chart.ChartOptions.Plugins.Subtitle;
		/// if (subtitle.ShouldSerializeDisplay())
		///     subtitle.ResetDisplay();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDisplay() => Display != false;

		/// <summary>
		/// Resets the <see cref="Display"/> property to its default value (<c>false</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Subtitle.ResetDisplay();
		/// ]]></code>
		/// </example>
		public void ResetDisplay() => Display = false;

		/// <summary>
		/// Determines whether the <see cref="Text"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Text"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetText"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var subtitle = chart.ChartOptions.Plugins.Subtitle;
		/// if (subtitle.ShouldSerializeText())
		///     subtitle.ResetText();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeText() => Text != null;

		/// <summary>
		/// Resets the <see cref="Text"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Subtitle.ResetText();
		/// ]]></code>
		/// </example>
		public void ResetText() => Text = null;

		/// <summary>
		/// Determines whether the <see cref="Color"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Color"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetColor"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var subtitle = chart.ChartOptions.Plugins.Subtitle;
		/// if (subtitle.ShouldSerializeColor())
		///     subtitle.ResetColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeColor() => Color != null;

		/// <summary>
		/// Resets the <see cref="Color"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Subtitle.ResetColor();
		/// ]]></code>
		/// </example>
		public void ResetColor() => Color = null;

		/// <summary>
		/// Determines whether the <see cref="Font"/> property should be serialized by the designer.
		/// </summary>
		/// <returns>
		/// <c>true</c> if <see cref="Font"/> is not <c>null</c> and has at least one non-default value
		/// (see <see cref="OptionsBase.IsDefault"/>); otherwise <c>false</c>.
		/// </returns>
		/// <remarks>Used by the designer together with <see cref="ResetFont"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var subtitle = chart.ChartOptions.Plugins.Subtitle;
		/// if (subtitle.ShouldSerializeFont())
		///     subtitle.ResetFont();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeFont() => Font != null && !Font.IsDefault;

		/// <summary>
		/// Resets the <see cref="Font"/> property to its default value by discarding the current
		/// <see cref="FontOptions"/> instance.
		/// </summary>
		/// <remarks>
		/// Used by the designer to restore the default value. A new, empty <see cref="FontOptions"/> is created the
		/// next time <see cref="Font"/> is read.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Subtitle.ResetFont();
		/// ]]></code>
		/// </example>
		public void ResetFont() => SetProperty(ref _font, null);

		/// <summary>
		/// Determines whether the <see cref="Padding"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Padding"/> is not <c>0</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetPadding"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var subtitle = chart.ChartOptions.Plugins.Subtitle;
		/// if (subtitle.ShouldSerializePadding())
		///     subtitle.ResetPadding();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePadding() => Padding != 0;

		/// <summary>
		/// Resets the <see cref="Padding"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Subtitle.ResetPadding();
		/// ]]></code>
		/// </example>
		public void ResetPadding() => Padding = 0;

	}
}
