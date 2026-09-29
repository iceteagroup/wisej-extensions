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
using System.Drawing;
using System.Text.Json.Serialization;

namespace Wisej.Web.Ext.ChartJS4.Models
{
	/// <summary>
	/// Represents the configuration of the Chart.js tooltip plugin (<c>options.plugins.tooltip</c>),
	/// which controls when tooltips are shown, how they are positioned and how they look.
	/// </summary>
	/// <remarks>
	/// An instance is created lazily by <see cref="PluginsOptions.Tooltip"/>. Changing any property
	/// updates the chart. Options that are not exposed as properties can be added through
	/// <see cref="ExtensionData"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
	/// tooltip.Mode = "index";
	/// tooltip.BackgroundColor = Color.Black;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class TooltipOptions : OptionsBase
	{
		private bool _enabled = true;
		private string? _mode;
		private string? _position;
		private bool _intersect = true;
		private object? _backgroundColor;
		private object? _titleColor;
		private Font? _titleFont;
		private int _titleSpacing = 2;
		private int _titleMarginBottom = 6;
		private object? _bodyColor;
		private FontOptions? _bodyFont;
		private int _bodySpacing = 2;
		private int _padding = 6;
		private int _caretPadding = 2;
		private int _caretSize = 5;
		private int _cornerRadius = 6;
		private bool _displayColors = true;
		private int _boxWidth = 40;
		private int _boxHeight;
		private bool _usePointStyle;

		/// <summary>
		/// Returns or sets whether tooltips are enabled (Chart.js <c>enabled</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to show tooltips on hover; otherwise <c>false</c>. The default is <c>true</c>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// bool enabled = chart.ChartOptions.Plugins.Tooltip.Enabled;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("enabled")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Are tooltips enabled?")]
		public bool Enabled
		{
			get => _enabled;
			set => SetProperty(ref _enabled, value);
		}

		/// <summary>
		/// Returns or sets the interaction mode that determines which elements appear in the tooltip (Chart.js <c>mode</c>).
		/// </summary>
		/// <value>
		/// One of <c>"point"</c>, <c>"nearest"</c>, <c>"index"</c>, <c>"dataset"</c>, <c>"x"</c> or <c>"y"</c>.
		/// The default is <c>null</c>, which uses the Chart.js default (the chart's interaction mode).
		/// </value>
		/// <remarks>
		/// <c>"index"</c> shows all items at the same index across datasets, <c>"dataset"</c> shows all items of the
		/// same dataset, <c>"nearest"</c> the closest item, <c>"point"</c> the items that intersect the pointer,
		/// and <c>"x"</c>/<c>"y"</c> the items that intersect along that axis.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.Mode = "index";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("mode")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Positioning mode.")]
		public string? Mode
		{
			get => _mode;
			set => SetProperty(ref _mode, value);
		}

		/// <summary>
		/// Returns or sets the tooltip position mode, which defines how the position of the tooltip is
		/// calculated (Chart.js <c>position</c>).
		/// </summary>
		/// <value>
		/// Accepted values: <c>"average"</c> | <c>"nearest"</c> | <c>"bottom"</c> (or the name of a custom positioner
		/// registered on the client). The default is <c>null</c>, in which case Chart.js uses <c>"average"</c>.
		/// </value>
		/// <remarks>
		/// <c>"average"</c> places the tooltip at the average position of the items displayed;
		/// <c>"nearest"</c> places it at the position of the element closest to the event.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.Position = "nearest";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("position")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The tooltip position mode ('average', 'nearest', 'bottom').")]
		[DefaultValue(null)]
		public string? Position
		{
			get => _position;
			set => SetProperty(ref _position, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Position"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Position"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetPosition"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializePosition())
		///     tooltip.ResetPosition();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePosition() => Position != null;

		/// <summary>
		/// Resets the <see cref="Position"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetPosition();
		/// ]]></code>
		/// </example>
		public void ResetPosition() => Position = null;

		/// <summary>
		/// Returns or sets whether the tooltip mode applies only when the mouse position intersects with an
		/// element (Chart.js <c>intersect</c>).
		/// </summary>
		/// <value>
		/// If <c>true</c> (the default), the tooltip is shown only when the pointer is over an element;
		/// if <c>false</c>, the <see cref="Mode"/> is applied at all times.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.Mode = "index";
		/// bool intersect = chart.ChartOptions.Plugins.Tooltip.Intersect;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("intersect")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Intersect mode.")]
		public bool Intersect
		{
			get => _intersect;
			set => SetProperty(ref _intersect, value);
		}

		/// <summary>
		/// Returns or sets the background color of the tooltip (Chart.js <c>backgroundColor</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/> or a CSS color string. The default is <c>null</c>, which uses the
		/// Chart.js default (<c>rgba(0, 0, 0, 0.8)</c>).
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.BackgroundColor = "rgba(33, 33, 33, 0.9)";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("backgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Background color.")]
		public object? BackgroundColor
		{
			get => _backgroundColor;
			set => SetProperty(ref _backgroundColor, value);
		}

		/// <summary>
		/// Returns or sets the color of the tooltip title text (Chart.js <c>titleColor</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/> or a CSS color string. The default is <c>null</c>, which uses the
		/// Chart.js default (white).
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.TitleColor = Color.Yellow;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("titleColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Title color.")]
		public object? TitleColor
		{
			get => _titleColor;
			set => SetProperty(ref _titleColor, value);
		}

		/// <summary>
		/// Returns or sets the font used for the tooltip title (Chart.js <c>titleFont</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Font"/>, or <c>null</c> (the default) to use the Chart.js default title font.
		/// </value>
		/// <remarks>
		/// Unlike <see cref="BodyFont"/>, which uses <see cref="FontOptions"/>, this property is typed as
		/// <see cref="System.Drawing.Font"/> and is not created automatically.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.TitleFont = new Font("Arial", 12, FontStyle.Bold);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("titleFont")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Title font.")]
		[DefaultValue(null)]
		public Font? TitleFont
		{
			get => _titleFont;
			set => SetProperty(ref _titleFont, value);
		}

		/// <summary>
		/// Returns or sets the spacing, in pixels, added to the top and bottom of each title line (Chart.js <c>titleSpacing</c>).
		/// </summary>
		/// <value>The spacing in pixels. The default is <c>2</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.TitleSpacing = 4;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("titleSpacing")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(2)]
		[Description("Title spacing.")]
		public int TitleSpacing
		{
			get => _titleSpacing;
			set => SetProperty(ref _titleSpacing, value);
		}

		/// <summary>
		/// Returns or sets the margin, in pixels, added below the title section (Chart.js <c>titleMarginBottom</c>).
		/// </summary>
		/// <value>The margin in pixels. The default is <c>6</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.TitleMarginBottom = 10;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("titleMarginBottom")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(6)]
		[Description("Title bottom margin.")]
		public int TitleMarginBottom
		{
			get => _titleMarginBottom;
			set => SetProperty(ref _titleMarginBottom, value);
		}

		/// <summary>
		/// Returns or sets the color of the tooltip body text (Chart.js <c>bodyColor</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/> or a CSS color string. The default is <c>null</c>, which uses the
		/// Chart.js default (white).
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.BodyColor = "#e0e0e0";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("bodyColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Body color.")]
		public object? BodyColor
		{
			get => _bodyColor;
			set => SetProperty(ref _bodyColor, value);
		}

		/// <summary>
		/// Returns or sets the font configuration of the tooltip body (Chart.js <c>bodyFont</c>).
		/// </summary>
		/// <value>
		/// A <see cref="FontOptions"/> instance. The getter creates an empty instance on first access, so nested
		/// properties can be set directly.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.BodyFont.Size = 14;
		/// chart.ChartOptions.Plugins.Tooltip.BodyFont.Family = "Segoe UI";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("bodyFont")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Body font.")]
		public FontOptions? BodyFont
		{
			get
			{
				if (_bodyFont == null)
					_bodyFont = new FontOptions { Chart = Chart };
				return _bodyFont;
			}
			set => SetProperty(ref _bodyFont, value);
		}

		/// <summary>
		/// Returns or sets the spacing, in pixels, added to the top and bottom of each tooltip item (Chart.js <c>bodySpacing</c>).
		/// </summary>
		/// <value>The spacing in pixels. The default is <c>2</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.BodySpacing = 4;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("bodySpacing")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(2)]
		[Description("Body spacing.")]
		public int BodySpacing
		{
			get => _bodySpacing;
			set => SetProperty(ref _bodySpacing, value);
		}

		/// <summary>
		/// Returns or sets the padding, in pixels, inside the tooltip (Chart.js <c>padding</c>).
		/// </summary>
		/// <value>The padding in pixels applied to all sides. The default is <c>6</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.Padding = 10;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(6)]
		[Description("Tooltip padding.")]
		public int Padding
		{
			get => _padding;
			set => SetProperty(ref _padding, value);
		}

		/// <summary>
		/// Returns or sets the extra distance, in pixels, to move the end of the tooltip arrow away from the
		/// tooltip point (Chart.js <c>caretPadding</c>).
		/// </summary>
		/// <value>The distance in pixels. The default is <c>2</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.CaretPadding = 6;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("caretPadding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(2)]
		[Description("Caret padding.")]
		public int CaretPadding
		{
			get => _caretPadding;
			set => SetProperty(ref _caretPadding, value);
		}

		/// <summary>
		/// Returns or sets the size, in pixels, of the tooltip arrow (Chart.js <c>caretSize</c>).
		/// </summary>
		/// <value>The arrow size in pixels; <c>0</c> hides the arrow. The default is <c>5</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.CaretSize = 8;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("caretSize")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(5)]
		[Description("Caret size.")]
		public int CaretSize
		{
			get => _caretSize;
			set => SetProperty(ref _caretSize, value);
		}

		/// <summary>
		/// Returns or sets the radius, in pixels, of the tooltip corner curves (Chart.js <c>cornerRadius</c>).
		/// </summary>
		/// <value>The corner radius in pixels. The default is <c>6</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.CornerRadius = 0;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("cornerRadius")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(6)]
		[Description("Corner radius.")]
		public int CornerRadius
		{
			get => _cornerRadius;
			set => SetProperty(ref _cornerRadius, value);
		}

		/// <summary>
		/// Returns or sets whether color boxes are shown in the tooltip next to each item (Chart.js <c>displayColors</c>).
		/// </summary>
		/// <value><c>true</c> to show the color boxes; otherwise <c>false</c>. The default is <c>true</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// bool showColors = chart.ChartOptions.Plugins.Tooltip.DisplayColors;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("displayColors")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Display color boxes.")]
		public bool DisplayColors
		{
			get => _displayColors;
			set => SetProperty(ref _displayColors, value);
		}

		/// <summary>
		/// Returns or sets the width, in pixels, of the color box when <see cref="DisplayColors"/> is <c>true</c>
		/// (Chart.js <c>boxWidth</c>).
		/// </summary>
		/// <value>The width in pixels. The default is <c>40</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.BoxWidth = 12;
		/// chart.ChartOptions.Plugins.Tooltip.BoxHeight = 12;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("boxWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(40)]
		[Description("Color box width.")]
		public int BoxWidth
		{
			get => _boxWidth;
			set => SetProperty(ref _boxWidth, value);
		}

		/// <summary>
		/// Returns or sets the height, in pixels, of the color box when <see cref="DisplayColors"/> is <c>true</c>
		/// (Chart.js <c>boxHeight</c>).
		/// </summary>
		/// <value>
		/// The height in pixels. The default is <c>0</c>, which is not serialized, so Chart.js uses its default
		/// (the body font size).
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.BoxHeight = 12;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("boxHeight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Color box height.")]
		public int BoxHeight
		{
			get => _boxHeight;
			set => SetProperty(ref _boxHeight, value);
		}

		/// <summary>
		/// Returns or sets whether the tooltip color boxes use the point style of the dataset rather than the
		/// default square (Chart.js <c>usePointStyle</c>).
		/// </summary>
		/// <value><c>true</c> to use the dataset point style; otherwise <c>false</c>. The default is <c>false</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Tooltip.UsePointStyle = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("usePointStyle")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("If true, use the point style for tooltip items.")]
		public bool UsePointStyle
		{
			get => _usePointStyle;
			set => SetProperty(ref _usePointStyle, value);
		}

		/// <summary>
		/// Returns or sets additional custom Chart.js tooltip options that are not exposed as properties.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c> (the default).
		/// </value>
		/// <remarks>
		/// This property is marked with <c>[JsonExtensionData]</c>: each entry is written as a property of the
		/// <c>tooltip</c> JSON object, next to the typed properties. It is hidden from the designer and not
		/// serialized in the designer code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// tooltip.ExtensionData = new Dictionary<string, object>();
		/// tooltip.ExtensionData["xAlign"] = "center";
		/// tooltip.ExtensionData["footerColor"] = "#ff0000";
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
			if (_bodyFont != null)
				_bodyFont.Chart = Chart;
		}

		/// <summary>
		/// Determines whether the <see cref="Enabled"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Enabled"/> is not <c>true</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetEnabled"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeEnabled())
		///     tooltip.ResetEnabled();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeEnabled() => Enabled != true;

		/// <summary>
		/// Resets the <see cref="Enabled"/> property to its default value (<c>true</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetEnabled();
		/// ]]></code>
		/// </example>
		public void ResetEnabled() => Enabled = true;

		/// <summary>
		/// Determines whether the <see cref="Mode"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Mode"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetMode"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeMode())
		///     tooltip.ResetMode();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeMode() => Mode != null;

		/// <summary>
		/// Resets the <see cref="Mode"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetMode();
		/// ]]></code>
		/// </example>
		public void ResetMode() => Mode = null;

		/// <summary>
		/// Determines whether the <see cref="Intersect"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Intersect"/> is not <c>true</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetIntersect"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeIntersect())
		///     tooltip.ResetIntersect();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeIntersect() => Intersect != true;

		/// <summary>
		/// Resets the <see cref="Intersect"/> property to its default value (<c>true</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetIntersect();
		/// ]]></code>
		/// </example>
		public void ResetIntersect() => Intersect = true;

		/// <summary>
		/// Determines whether the <see cref="BackgroundColor"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BackgroundColor"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetBackgroundColor"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeBackgroundColor())
		///     tooltip.ResetBackgroundColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBackgroundColor() => BackgroundColor != null;

		/// <summary>
		/// Resets the <see cref="BackgroundColor"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetBackgroundColor();
		/// ]]></code>
		/// </example>
		public void ResetBackgroundColor() => BackgroundColor = null;

		/// <summary>
		/// Determines whether the <see cref="TitleColor"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="TitleColor"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetTitleColor"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeTitleColor())
		///     tooltip.ResetTitleColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTitleColor() => TitleColor != null;

		/// <summary>
		/// Resets the <see cref="TitleColor"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetTitleColor();
		/// ]]></code>
		/// </example>
		public void ResetTitleColor() => TitleColor = null;

		/// <summary>
		/// Determines whether the <see cref="TitleFont"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="TitleFont"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetTitleFont"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeTitleFont())
		///     tooltip.ResetTitleFont();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTitleFont() => TitleFont != null;

		/// <summary>
		/// Resets the <see cref="TitleFont"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetTitleFont();
		/// ]]></code>
		/// </example>
		public void ResetTitleFont() => TitleFont = null;

		/// <summary>
		/// Determines whether the <see cref="TitleSpacing"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="TitleSpacing"/> is not <c>2</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetTitleSpacing"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeTitleSpacing())
		///     tooltip.ResetTitleSpacing();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTitleSpacing() => TitleSpacing != 2;

		/// <summary>
		/// Resets the <see cref="TitleSpacing"/> property to its default value (<c>2</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetTitleSpacing();
		/// ]]></code>
		/// </example>
		public void ResetTitleSpacing() => TitleSpacing = 2;

		/// <summary>
		/// Determines whether the <see cref="TitleMarginBottom"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="TitleMarginBottom"/> is not <c>6</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetTitleMarginBottom"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeTitleMarginBottom())
		///     tooltip.ResetTitleMarginBottom();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTitleMarginBottom() => TitleMarginBottom != 6;

		/// <summary>
		/// Resets the <see cref="TitleMarginBottom"/> property to its default value (<c>6</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetTitleMarginBottom();
		/// ]]></code>
		/// </example>
		public void ResetTitleMarginBottom() => TitleMarginBottom = 6;

		/// <summary>
		/// Determines whether the <see cref="BodyColor"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BodyColor"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetBodyColor"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeBodyColor())
		///     tooltip.ResetBodyColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBodyColor() => BodyColor != null;

		/// <summary>
		/// Resets the <see cref="BodyColor"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetBodyColor();
		/// ]]></code>
		/// </example>
		public void ResetBodyColor() => BodyColor = null;

		/// <summary>
		/// Determines whether the <see cref="BodyFont"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BodyFont"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// Used by the designer together with <see cref="ResetBodyFont"/>. Because the <see cref="BodyFont"/> getter
		/// creates an instance on demand, this method effectively always returns <c>true</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeBodyFont())
		///     tooltip.ResetBodyFont();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBodyFont() => BodyFont != null;

		/// <summary>
		/// Resets the <see cref="BodyFont"/> property to its default value by discarding the current
		/// <see cref="FontOptions"/> instance.
		/// </summary>
		/// <remarks>
		/// Used by the designer to restore the default value. A new, empty <see cref="FontOptions"/> is created the
		/// next time <see cref="BodyFont"/> is read.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetBodyFont();
		/// ]]></code>
		/// </example>
		public void ResetBodyFont() => SetProperty(ref _bodyFont, null);

		/// <summary>
		/// Determines whether the <see cref="BodySpacing"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BodySpacing"/> is not <c>2</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetBodySpacing"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeBodySpacing())
		///     tooltip.ResetBodySpacing();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBodySpacing() => BodySpacing != 2;

		/// <summary>
		/// Resets the <see cref="BodySpacing"/> property to its default value (<c>2</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetBodySpacing();
		/// ]]></code>
		/// </example>
		public void ResetBodySpacing() => BodySpacing = 2;

		/// <summary>
		/// Determines whether the <see cref="Padding"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Padding"/> is not <c>6</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetPadding"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializePadding())
		///     tooltip.ResetPadding();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePadding() => Padding != 6;

		/// <summary>
		/// Resets the <see cref="Padding"/> property to its default value (<c>6</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetPadding();
		/// ]]></code>
		/// </example>
		public void ResetPadding() => Padding = 6;

		/// <summary>
		/// Determines whether the <see cref="CaretPadding"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="CaretPadding"/> is not <c>2</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetCaretPadding"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeCaretPadding())
		///     tooltip.ResetCaretPadding();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeCaretPadding() => CaretPadding != 2;

		/// <summary>
		/// Resets the <see cref="CaretPadding"/> property to its default value (<c>2</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetCaretPadding();
		/// ]]></code>
		/// </example>
		public void ResetCaretPadding() => CaretPadding = 2;

		/// <summary>
		/// Determines whether the <see cref="CaretSize"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="CaretSize"/> is not <c>5</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetCaretSize"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeCaretSize())
		///     tooltip.ResetCaretSize();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeCaretSize() => CaretSize != 5;

		/// <summary>
		/// Resets the <see cref="CaretSize"/> property to its default value (<c>5</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetCaretSize();
		/// ]]></code>
		/// </example>
		public void ResetCaretSize() => CaretSize = 5;

		/// <summary>
		/// Determines whether the <see cref="CornerRadius"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="CornerRadius"/> is not <c>6</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetCornerRadius"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeCornerRadius())
		///     tooltip.ResetCornerRadius();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeCornerRadius() => CornerRadius != 6;

		/// <summary>
		/// Resets the <see cref="CornerRadius"/> property to its default value (<c>6</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetCornerRadius();
		/// ]]></code>
		/// </example>
		public void ResetCornerRadius() => CornerRadius = 6;

		/// <summary>
		/// Determines whether the <see cref="DisplayColors"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="DisplayColors"/> is not <c>true</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetDisplayColors"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeDisplayColors())
		///     tooltip.ResetDisplayColors();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDisplayColors() => DisplayColors != true;

		/// <summary>
		/// Resets the <see cref="DisplayColors"/> property to its default value (<c>true</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetDisplayColors();
		/// ]]></code>
		/// </example>
		public void ResetDisplayColors() => DisplayColors = true;

		/// <summary>
		/// Determines whether the <see cref="BoxWidth"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BoxWidth"/> is not <c>40</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetBoxWidth"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeBoxWidth())
		///     tooltip.ResetBoxWidth();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBoxWidth() => BoxWidth != 40;

		/// <summary>
		/// Resets the <see cref="BoxWidth"/> property to its default value (<c>40</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetBoxWidth();
		/// ]]></code>
		/// </example>
		public void ResetBoxWidth() => BoxWidth = 40;

		/// <summary>
		/// Determines whether the <see cref="BoxHeight"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BoxHeight"/> is not <c>0</c> (its default); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer together with <see cref="ResetBoxHeight"/>.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = chart.ChartOptions.Plugins.Tooltip;
		/// if (tooltip.ShouldSerializeBoxHeight())
		///     tooltip.ResetBoxHeight();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBoxHeight() => BoxHeight != 0;

		/// <summary>
		/// Resets the <see cref="BoxHeight"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <remarks>Used by the designer to restore the default value.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Plugins.Tooltip.ResetBoxHeight();
		/// ]]></code>
		/// </example>
		public void ResetBoxHeight() => BoxHeight = 0;

	}
}
