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
	/// Represents the configuration of the Chart.js legend plugin (<c>options.plugins.legend</c>),
	/// which displays the datasets of the chart and lets the user toggle them.
	/// </summary>
	/// <remarks>
	/// An instance is created lazily by <see cref="PluginsOptions.Legend"/>. Changing any property
	/// updates the chart. Options that are not exposed as properties can be added through
	/// <see cref="ExtensionData"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var legend = chart.ChartOptions.Plugins.Legend;
	/// legend.Position = "bottom";
	/// legend.Align = "start";
	/// legend.Labels.UsePointStyle = true;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[Browsable(true)]
	[TypeConverter(typeof(Converter))]
	public class LegendOptions : OptionsBase
	{
		private bool _display = true;
		private string? _position;
		private string? _align;
		private int _maxHeight;
		private int _maxWidth;
		private bool _fullSize = true;
		private bool _reverse;
		private LegendTitleOptions? _title;
		private LegendLabelsOptions? _labels;

		/// <summary>
		/// Returns or sets whether the legend is shown (Chart.js <c>display</c>).
		/// </summary>
		/// <value><c>true</c> to show the legend; otherwise <c>false</c>. The default is <c>true</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// bool legendVisible = chart.ChartOptions.Plugins.Legend.Display;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Is the legend shown?")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Returns or sets the position of the legend (Chart.js <c>position</c>).
		/// </summary>
		/// <value>
		/// One of <c>"top"</c> | <c>"left"</c> | <c>"bottom"</c> | <c>"right"</c> | <c>"chartArea"</c>.
		/// The default is <c>null</c>, in which case Chart.js uses <c>"top"</c>.
		/// </value>
		/// <remarks>
		/// <c>"chartArea"</c> draws the legend inside the chart area instead of reserving space around it.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Position = "bottom";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("position")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Position of the legend.")]
		public string? Position
		{
			get => _position;
			set => SetProperty(ref _position, value);
		}

		/// <summary>
		/// Returns or sets the alignment of the legend within its box (Chart.js <c>align</c>).
		/// </summary>
		/// <value>
		/// One of <c>"start"</c> | <c>"center"</c> | <c>"end"</c>. The default is <c>null</c>, in which case
		/// Chart.js uses <c>"center"</c>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Align = "end";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("align")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Alignment of the legend.")]
		public string? Align
		{
			get => _align;
			set => SetProperty(ref _align, value);
		}

		/// <summary>
		/// Returns or sets the maximum height of the legend, in pixels (Chart.js <c>maxHeight</c>).
		/// </summary>
		/// <value>
		/// The maximum height in pixels. The default is <c>0</c>, which is not serialized, so Chart.js applies
		/// no explicit limit.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.MaxHeight = 60;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("maxHeight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Maximum height in pixels.")]
		public int MaxHeight
		{
			get => _maxHeight;
			set => SetProperty(ref _maxHeight, value);
		}

		/// <summary>
		/// Returns or sets the maximum width of the legend, in pixels (Chart.js <c>maxWidth</c>).
		/// </summary>
		/// <value>
		/// The maximum width in pixels. The default is <c>0</c>, which is not serialized, so Chart.js applies
		/// no explicit limit.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Position = "right";
		/// chart.ChartOptions.Plugins.Legend.MaxWidth = 150;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("maxWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Maximum width in pixels.")]
		public int MaxWidth
		{
			get => _maxWidth;
			set => SetProperty(ref _maxWidth, value);
		}

		/// <summary>
		/// Returns or sets whether the legend box takes the full width (or height, when placed left or right)
		/// of the canvas, moving other boxes aside (Chart.js <c>fullSize</c>).
		/// </summary>
		/// <value><c>true</c> to take the full width/height of the canvas; otherwise <c>false</c>. The default is <c>true</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// bool fullSize = chart.ChartOptions.Plugins.Legend.FullSize;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("fullSize")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Take full width/height of canvas.")]
		public bool FullSize
		{
			get => _fullSize;
			set => SetProperty(ref _fullSize, value);
		}

		/// <summary>
		/// Returns or sets whether the legend shows the datasets in reverse order (Chart.js <c>reverse</c>).
		/// </summary>
		/// <value><c>true</c> to reverse the order of the legend items; otherwise <c>false</c>. The default is <c>false</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Reverse = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("reverse")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Show datasets in reverse order.")]
		public bool Reverse
		{
			get => _reverse;
			set => SetProperty(ref _reverse, value);
		}

		/// <summary>
		/// Returns or sets the legend title configuration (Chart.js <c>title</c>).
		/// </summary>
		/// <value>
		/// A <see cref="LegendTitleOptions"/> instance. The getter creates an empty instance on first access, so
		/// nested properties can be set directly.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Title.Display = true;
		/// chart.ChartOptions.Plugins.Legend.Title.Text = "Regions";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("title")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Legend title configuration.")]
		public LegendTitleOptions? Title
		{
			get
			{
				if (_title == null)
					_title = new LegendTitleOptions { Chart = Chart };
				return _title;
			}
			set => SetProperty(ref _title, value);
		}

		/// <summary>
		/// Returns or sets the legend labels configuration (Chart.js <c>labels</c>).
		/// </summary>
		/// <value>
		/// A <see cref="LegendLabelsOptions"/> instance. The getter creates an empty instance on first access, so
		/// nested properties can be set directly.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Labels.BoxWidth = 12;
		/// chart.ChartOptions.Plugins.Legend.Labels.Color = Color.DarkGray;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("labels")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Legend labels configuration.")]
		public LegendLabelsOptions? Labels
		{
			get
			{
				if (_labels == null)
					_labels = new LegendLabelsOptions { Chart = Chart };
				return _labels;
			}
			set => SetProperty(ref _labels, value);
		}

		/// <summary>
		/// Returns or sets additional custom Chart.js legend options that are not exposed as properties.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c> (the default).
		/// </value>
		/// <remarks>
		/// This property is marked with <c>[JsonExtensionData]</c>: each entry is written as a property of the
		/// <c>legend</c> JSON object, next to the typed properties. It is hidden from the designer and not
		/// serialized in the designer code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = chart.ChartOptions.Plugins.Legend;
		/// legend.ExtensionData = new Dictionary<string, object>();
		/// legend.ExtensionData["rtl"] = true;
		/// legend.ExtensionData["textDirection"] = "rtl";
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
			if (_title != null)
				_title.Chart = Chart;
			if (_labels != null)
				_labels.Chart = Chart;
		}

		/// <summary>
		/// Determines whether the <see cref="Display"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Display"/> is not <c>true</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetDisplay"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = chart.ChartOptions.Plugins.Legend;
		/// if (legend.ShouldSerializeDisplay())
		///     legend.ResetDisplay();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDisplay() => Display != true;

		/// <summary>
		/// Resets the <see cref="Display"/> property to its default value (<c>true</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.ResetDisplay();
		/// ]]></code>
		/// </example>
		public void ResetDisplay() => Display = true;

		/// <summary>
		/// Determines whether the <see cref="Position"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Position"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetPosition"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = chart.ChartOptions.Plugins.Legend;
		/// if (legend.ShouldSerializePosition())
		///     legend.ResetPosition();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePosition() => Position != null;

		/// <summary>
		/// Resets the <see cref="Position"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.ResetPosition();
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
		/// var legend = chart.ChartOptions.Plugins.Legend;
		/// if (legend.ShouldSerializeAlign())
		///     legend.ResetAlign();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeAlign() => Align != null;

		/// <summary>
		/// Resets the <see cref="Align"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.ResetAlign();
		/// ]]></code>
		/// </example>
		public void ResetAlign() => Align = null;

		/// <summary>
		/// Determines whether the <see cref="MaxHeight"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="MaxHeight"/> is not <c>0</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetMaxHeight"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = chart.ChartOptions.Plugins.Legend;
		/// if (legend.ShouldSerializeMaxHeight())
		///     legend.ResetMaxHeight();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeMaxHeight() => MaxHeight != default;

		/// <summary>
		/// Resets the <see cref="MaxHeight"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.ResetMaxHeight();
		/// ]]></code>
		/// </example>
		public void ResetMaxHeight() => MaxHeight = default;

		/// <summary>
		/// Determines whether the <see cref="MaxWidth"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="MaxWidth"/> is not <c>0</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetMaxWidth"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = chart.ChartOptions.Plugins.Legend;
		/// if (legend.ShouldSerializeMaxWidth())
		///     legend.ResetMaxWidth();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeMaxWidth() => MaxWidth != default;

		/// <summary>
		/// Resets the <see cref="MaxWidth"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.ResetMaxWidth();
		/// ]]></code>
		/// </example>
		public void ResetMaxWidth() => MaxWidth = default;

		/// <summary>
		/// Determines whether the <see cref="FullSize"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="FullSize"/> is not <c>true</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetFullSize"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = chart.ChartOptions.Plugins.Legend;
		/// if (legend.ShouldSerializeFullSize())
		///     legend.ResetFullSize();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeFullSize() => FullSize != true;

		/// <summary>
		/// Resets the <see cref="FullSize"/> property to its default value (<c>true</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.ResetFullSize();
		/// ]]></code>
		/// </example>
		public void ResetFullSize() => FullSize = true;

		/// <summary>
		/// Determines whether the <see cref="Reverse"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Reverse"/> is not <c>false</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetReverse"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = chart.ChartOptions.Plugins.Legend;
		/// if (legend.ShouldSerializeReverse())
		///     legend.ResetReverse();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeReverse() => Reverse != false;

		/// <summary>
		/// Resets the <see cref="Reverse"/> property to its default value (<c>false</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.ResetReverse();
		/// ]]></code>
		/// </example>
		public void ResetReverse() => Reverse = false;

		/// <summary>
		/// Determines whether the <see cref="Labels"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Labels"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// Used by the designer together with <see cref="ResetLabels"/>. Because the <see cref="Labels"/> getter
		/// creates an instance on demand, this method effectively always returns <c>true</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = chart.ChartOptions.Plugins.Legend;
		/// if (legend.ShouldSerializeLabels())
		///     legend.ResetLabels();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeLabels() => Labels != null;

		/// <summary>
		/// Resets the <see cref="Labels"/> property to its default value by discarding the current
		/// <see cref="LegendLabelsOptions"/> instance.
		/// </summary>
		/// <remarks>
		/// Used by the designer to restore the default value. A new, empty <see cref="LegendLabelsOptions"/> is
		/// created the next time <see cref="Labels"/> is read.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.ResetLabels();
		/// ]]></code>
		/// </example>
		public void ResetLabels() => SetProperty(ref _labels, null);

		/// <summary>
		/// Determines whether the <see cref="Title"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Title"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// Used by the designer together with <see cref="ResetTitle"/>. Because the <see cref="Title"/> getter
		/// creates an instance on demand, this method effectively always returns <c>true</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var legend = chart.ChartOptions.Plugins.Legend;
		/// if (legend.ShouldSerializeTitle())
		///     legend.ResetTitle();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTitle() => Title != null;

		/// <summary>
		/// Resets the <see cref="Title"/> property to its default value by discarding the current
		/// <see cref="LegendTitleOptions"/> instance.
		/// </summary>
		/// <remarks>
		/// Used by the designer to restore the default value. A new, empty <see cref="LegendTitleOptions"/> is
		/// created the next time <see cref="Title"/> is read.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Legend.ResetTitle();
		/// ]]></code>
		/// </example>
		public void ResetTitle() => SetProperty(ref _title, null);

	}

	/// <summary>
	/// Represents the configuration of the legend title (Chart.js <c>options.plugins.legend.title</c>).
	/// </summary>
	/// <remarks>
	/// An instance is created lazily by <see cref="LegendOptions.Title"/>. The title is only drawn when
	/// <see cref="Display"/> is <c>true</c>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var title = chart.ChartOptions.Plugins.Legend.Title;
	/// title.Display = true;
	/// title.Text = "Regions";
	/// title.Color = Color.Gray;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	public class LegendTitleOptions : OptionsBase
	{
		private bool _display;
		private string? _text;
		private string? _position;
		private object? _color;
		private int _padding;

		/// <summary>
		/// Returns or sets whether the legend title is displayed (Chart.js <c>display</c>).
		/// </summary>
		/// <value><c>true</c> to display the legend title; otherwise <c>false</c>. The default is <c>false</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Title.Display = true;
		/// chart.ChartOptions.Plugins.Legend.Title.Text = "Regions";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Is the legend title displayed?")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Returns or sets the legend title text (Chart.js <c>text</c>).
		/// </summary>
		/// <value>The title text, or <c>null</c> (the default) for no text.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Title.Display = true;
		/// chart.ChartOptions.Plugins.Legend.Title.Text = "Sales by region";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("text")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The legend title text.")]
		public string? Text
		{
			get => _text;
			set => SetProperty(ref _text, value);
		}

		/// <summary>
		/// Returns or sets the position of the legend title (Chart.js <c>position</c>).
		/// </summary>
		/// <value>
		/// One of <c>"start"</c> | <c>"center"</c> | <c>"end"</c>. The default is <c>null</c>, in which case
		/// Chart.js uses <c>"center"</c>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Title.Position = "start";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("position")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Position of the legend title: 'start', 'center', 'end'.")]
		public string? Position
		{
			get => _position;
			set => SetProperty(ref _position, value);
		}

		/// <summary>
		/// Returns or sets the color of the legend title text (Chart.js <c>color</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/> or a CSS color string. The default is <c>null</c>, which uses the
		/// Chart.js default font color.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Title.Color = "#666666";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("color")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Color of the legend title text.")]
		public object? Color
		{
			get => _color;
			set => SetProperty(ref _color, value);
		}

		/// <summary>
		/// Returns or sets the padding, in pixels, around the legend title (Chart.js <c>padding</c>).
		/// </summary>
		/// <value>
		/// The padding in pixels. The default is <c>0</c>, which is not serialized, so Chart.js uses its default.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Title.Padding = 8;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Padding around the legend title.")]
		public int Padding
		{
			get => _padding;
			set => SetProperty(ref _padding, value);
		}
	}
}
