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
	/// Represents the configuration of the Chart.js title plugin (<c>options.plugins.title</c>),
	/// which draws a title above (or beside) the chart.
	/// </summary>
	/// <remarks>
	/// An instance is created lazily by <see cref="PluginsOptions.Title"/>. The title is only drawn when
	/// <see cref="Display"/> is <c>true</c>. Changing any property updates the chart. Options that are not
	/// exposed as properties can be added through <see cref="ExtensionData"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var title = chart.ChartOptions.Plugins.Title;
	/// title.Display = true;
	/// title.Text = "Monthly Sales";
	/// title.Font.Size = 18;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class TitleOptions : OptionsBase
	{
		private bool _display;
		private object? _text;
		private string? _position;
		private string? _align;
		private object? _color;
		private FontOptions? _font;
		private int _padding = 10;

		/// <summary>
		/// Returns or sets whether the title is shown (Chart.js <c>display</c>).
		/// </summary>
		/// <value><c>true</c> to show the title; otherwise <c>false</c>. The default is <c>false</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Title.Display = true;
		/// chart.ChartOptions.Plugins.Title.Text = "Monthly Sales";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Is the title shown?")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Returns or sets the title text (Chart.js <c>text</c>).
		/// </summary>
		/// <value>
		/// A <see cref="string"/>, or an array of strings to render the title on multiple lines.
		/// The default is <c>null</c>.
		/// </value>
		/// <remarks>The title is only drawn when <see cref="Display"/> is <c>true</c>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Title.Display = true;
		/// chart.ChartOptions.Plugins.Title.Text = new[] { "Monthly Sales", "2026" };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("text")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Title text.")]
		public object? Text
		{
			get => _text;
			set => SetProperty(ref _text, value);
		}

		/// <summary>
		/// Returns or sets the position of the title (Chart.js <c>position</c>).
		/// </summary>
		/// <value>
		/// One of <c>"top"</c> | <c>"left"</c> | <c>"bottom"</c> | <c>"right"</c>. The default is <c>null</c>,
		/// in which case Chart.js uses <c>"top"</c>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Title.Position = "bottom";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("position")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Position of title.")]
		public string? Position
		{
			get => _position;
			set => SetProperty(ref _position, value);
		}

		/// <summary>
		/// Returns or sets the alignment of the title (Chart.js <c>align</c>).
		/// </summary>
		/// <value>
		/// One of <c>"start"</c> | <c>"center"</c> | <c>"end"</c>. The default is <c>null</c>, in which case
		/// Chart.js uses <c>"center"</c>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Title.Align = "start";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("align")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Alignment of title.")]
		public string? Align
		{
			get => _align;
			set => SetProperty(ref _align, value);
		}

		/// <summary>
		/// Returns or sets the color of the title text (Chart.js <c>color</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/> or a CSS color string. The default is <c>null</c>, which uses the
		/// Chart.js default font color.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Title.Color = Color.Navy;
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
		/// Returns or sets the font configuration of the title (Chart.js <c>font</c>).
		/// </summary>
		/// <value>
		/// A <see cref="FontOptions"/> instance. The getter creates an empty instance on first access, so nested
		/// properties can be set directly.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Title.Font.Size = 18;
		/// chart.ChartOptions.Plugins.Title.Font.Weight = "bold";
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
		/// Returns or sets the padding, in pixels, applied around the title (Chart.js <c>padding</c>).
		/// </summary>
		/// <value>The padding in pixels. The default is <c>10</c>.</value>
		/// <remarks>Only the top and bottom padding are applied by Chart.js; the left and right padding are ignored.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Title.Padding = 20;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(10)]
		[Description("Padding around title.")]
		public int Padding
		{
			get => _padding;
			set => SetProperty(ref _padding, value);
		}

		/// <summary>
		/// Returns or sets additional custom Chart.js title options that are not exposed as properties.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c> (the default).
		/// </value>
		/// <remarks>
		/// This property is marked with <c>[JsonExtensionData]</c>: each entry is written as a property of the
		/// <c>title</c> JSON object, next to the typed properties. It is hidden from the designer and not
		/// serialized in the designer code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = chart.ChartOptions.Plugins.Title;
		/// title.ExtensionData = new Dictionary<string, object>();
		/// title.ExtensionData["fullSize"] = false;
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
		/// var title = chart.ChartOptions.Plugins.Title;
		/// if (title.ShouldSerializeDisplay())
		///     title.ResetDisplay();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDisplay() => Display != false;

		/// <summary>
		/// Resets the <see cref="Display"/> property to its default value (<c>false</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Title.ResetDisplay();
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
		/// var title = chart.ChartOptions.Plugins.Title;
		/// if (title.ShouldSerializeText())
		///     title.ResetText();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeText() => Text != null;

		/// <summary>
		/// Resets the <see cref="Text"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Title.ResetText();
		/// ]]></code>
		/// </example>
		public void ResetText() => Text = null;

		/// <summary>
		/// Determines whether the <see cref="Position"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Position"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetPosition"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = chart.ChartOptions.Plugins.Title;
		/// if (title.ShouldSerializePosition())
		///     title.ResetPosition();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePosition() => Position != null;

		/// <summary>
		/// Resets the <see cref="Position"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Title.ResetPosition();
		/// ]]></code>
		/// </example>
		public void ResetPosition() => Position = null;

		/// <summary>
		/// Determines whether the <see cref="Align"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Align"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetAlign"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = chart.ChartOptions.Plugins.Title;
		/// if (title.ShouldSerializeAlign())
		///     title.ResetAlign();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeAlign() => Align != null;

		/// <summary>
		/// Resets the <see cref="Align"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Title.ResetAlign();
		/// ]]></code>
		/// </example>
		public void ResetAlign() => Align = null;

		/// <summary>
		/// Determines whether the <see cref="Color"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Color"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetColor"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = chart.ChartOptions.Plugins.Title;
		/// if (title.ShouldSerializeColor())
		///     title.ResetColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeColor() => Color != null;

		/// <summary>
		/// Resets the <see cref="Color"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Title.ResetColor();
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
		/// var title = chart.ChartOptions.Plugins.Title;
		/// if (title.ShouldSerializeFont())
		///     title.ResetFont();
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
		/// chart.ChartOptions.Plugins.Title.ResetFont();
		/// ]]></code>
		/// </example>
		public void ResetFont() => SetProperty(ref _font, null);

		/// <summary>
		/// Determines whether the <see cref="Padding"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Padding"/> is not <c>10</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetPadding"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = chart.ChartOptions.Plugins.Title;
		/// if (title.ShouldSerializePadding())
		///     title.ResetPadding();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePadding() => Padding != 10;

		/// <summary>
		/// Resets the <see cref="Padding"/> property to its default value (<c>10</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Title.ResetPadding();
		/// ]]></code>
		/// </example>
		public void ResetPadding() => Padding = 10;

	}
}
