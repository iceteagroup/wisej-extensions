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
	/// Represents the configuration of the legend labels (Chart.js <c>options.plugins.legend.labels</c>),
	/// which controls the colored boxes and text of each legend item.
	/// </summary>
	/// <remarks>
	/// An instance is created lazily by <see cref="LegendOptions.Labels"/>. Changing any property
	/// updates the chart. Options that are not exposed as properties can be added through
	/// <see cref="ExtensionData"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var labels = chart.ChartOptions.Plugins.Legend.Labels;
	/// labels.UsePointStyle = true;
	/// labels.Padding = 20;
	/// labels.Font.Size = 14;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class LegendLabelsOptions : OptionsBase
	{
		private int _boxWidth;
		private int _boxHeight;
		private object? _color;
		private FontOptions? _font;
		private int _padding = 10;
		private bool _usePointStyle;

		/// <summary>
		/// Returns or sets the width, in pixels, of the colored box of each legend item (Chart.js <c>boxWidth</c>).
		/// </summary>
		/// <value>
		/// The width in pixels. The default is <c>0</c>, which is not serialized, so Chart.js uses its default (<c>40</c>).
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Labels.BoxWidth = 12;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("boxWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Width of colored box.")]
		public int BoxWidth
		{
			get => _boxWidth;
			set => SetProperty(ref _boxWidth, value);
		}

		/// <summary>
		/// Returns or sets the height, in pixels, of the colored box of each legend item (Chart.js <c>boxHeight</c>).
		/// </summary>
		/// <value>
		/// The height in pixels. The default is <c>0</c>, which is not serialized, so Chart.js uses its default
		/// (the font size).
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Labels.BoxWidth = 12;
		/// chart.ChartOptions.Plugins.Legend.Labels.BoxHeight = 12;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("boxHeight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Height of colored box.")]
		[DefaultValue(0)]
		public int BoxHeight
		{
			get => _boxHeight;
			set => SetProperty(ref _boxHeight, value);
		}

		/// <summary>
		/// Returns or sets the color of the label text and of the strikethrough drawn on hidden datasets
		/// (Chart.js <c>color</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/> or a CSS color string. The default is <c>null</c>, which uses the
		/// Chart.js default font color.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Labels.Color = Color.DimGray;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("color")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Label color.")]
		public object? Color
		{
			get => _color;
			set => SetProperty(ref _color, value);
		}

		/// <summary>
		/// Returns or sets the font configuration of the legend labels (Chart.js <c>font</c>).
		/// </summary>
		/// <value>
		/// A <see cref="FontOptions"/> instance. The getter creates an empty instance on first access, so nested
		/// properties can be set directly.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Labels.Font.Size = 14;
		/// chart.ChartOptions.Plugins.Legend.Labels.Font.Weight = "bold";
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
		/// Returns or sets the padding, in pixels, between rows of colored boxes (Chart.js <c>padding</c>).
		/// </summary>
		/// <value>The padding in pixels. The default is <c>10</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Labels.Padding = 20;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(10)]
		[Description("Padding between rows.")]
		public int Padding
		{
			get => _padding;
			set => SetProperty(ref _padding, value);
		}

		/// <summary>
		/// Returns or sets whether the legend items use the point style of the dataset rather than the default
		/// square (Chart.js <c>usePointStyle</c>).
		/// </summary>
		/// <value><c>true</c> to use the dataset point style; otherwise <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>When <c>true</c>, the box size is derived from the font size and <see cref="BoxWidth"/> is ignored by Chart.js.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Labels.UsePointStyle = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("usePointStyle")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("If true, use the point style for legend items.")]
		public bool UsePointStyle
		{
			get => _usePointStyle;
			set => SetProperty(ref _usePointStyle, value);
		}

		/// <summary>
		/// Returns or sets additional custom Chart.js legend label options that are not exposed as properties.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c> (the default).
		/// </value>
		/// <remarks>
		/// This property is marked with <c>[JsonExtensionData]</c>: each entry is written as a property of the
		/// <c>labels</c> JSON object, next to the typed properties. It is hidden from the designer and not
		/// serialized in the designer code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var labels = chart.ChartOptions.Plugins.Legend.Labels;
		/// labels.ExtensionData = new Dictionary<string, object>();
		/// labels.ExtensionData["pointStyle"] = "circle";
		/// labels.ExtensionData["textAlign"] = "left";
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
		/// Determines whether the <see cref="BoxWidth"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BoxWidth"/> is not <c>0</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetBoxWidth"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var labels = chart.ChartOptions.Plugins.Legend.Labels;
		/// if (labels.ShouldSerializeBoxWidth())
		///     labels.ResetBoxWidth();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBoxWidth() => BoxWidth != 0;

		/// <summary>
		/// Resets the <see cref="BoxWidth"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.Labels.ResetBoxWidth();
		/// ]]></code>
		/// </example>
		public void ResetBoxWidth() => BoxWidth = 0;

		/// <summary>
		/// Determines whether the <see cref="BoxHeight"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BoxHeight"/> is not <c>0</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetBoxHeight"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var labels = chart.ChartOptions.Plugins.Legend.Labels;
		/// if (labels.ShouldSerializeBoxHeight())
		///     labels.ResetBoxHeight();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBoxHeight() => BoxHeight != 0;

		/// <summary>
		/// Resets the <see cref="BoxHeight"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.Labels.ResetBoxHeight();
		/// ]]></code>
		/// </example>
		public void ResetBoxHeight() => BoxHeight = 0;

		/// <summary>
		/// Determines whether the <see cref="Color"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Color"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetColor"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var labels = chart.ChartOptions.Plugins.Legend.Labels;
		/// if (labels.ShouldSerializeColor())
		///     labels.ResetColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeColor() => Color != null;

		/// <summary>
		/// Resets the <see cref="Color"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.Labels.ResetColor();
		/// ]]></code>
		/// </example>
		public void ResetColor() => Color = null;

		/// <summary>
		/// Determines whether the <see cref="Font"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Font"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// Used by the designer together with <see cref="ResetFont"/>. Because the <see cref="Font"/> getter
		/// creates an instance on demand, this method effectively always returns <c>true</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var labels = chart.ChartOptions.Plugins.Legend.Labels;
		/// if (labels.ShouldSerializeFont())
		///     labels.ResetFont();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeFont() => Font != null;

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
		/// chart.ChartOptions.Plugins.Legend.Labels.ResetFont();
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
		/// var labels = chart.ChartOptions.Plugins.Legend.Labels;
		/// if (labels.ShouldSerializePadding())
		///     labels.ResetPadding();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePadding() => Padding != 10;

		/// <summary>
		/// Resets the <see cref="Padding"/> property to its default value (<c>10</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.Labels.ResetPadding();
		/// ]]></code>
		/// </example>
		public void ResetPadding() => Padding = 10;

	}
}
