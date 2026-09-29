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
	/// Represents the options of the chartjs-plugin-datalabels plugin (Chart.js <c>options.plugins.datalabels</c>), which draws labels next to or on top of the data elements.
	/// </summary>
	/// <remarks>
	/// An instance is available through <see cref="PluginsOptions.DataLabels"/>. When it is serialized the client automatically registers the datalabels plugin; unless a formatter is specified through <see cref="ExtensionData"/>, the values are formatted by the ChartJS4 control.
	/// Labels are hidden by default: set <see cref="Display"/> to <c>true</c> to show them.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var labels = chart.ChartOptions.Plugins.DataLabels;
	/// labels.Display = true;
	/// labels.Anchor = "end";
	/// labels.Align = "top";
	/// labels.Color = System.Drawing.Color.DarkBlue;
	/// labels.Font.Size = 14;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class DataLabelsOptions : OptionsBase
	{
		private bool _display;
		private string? _anchor;
		private string? _align;
		private int _offset = 4;
		private object? _backgroundColor;
		private object? _borderColor;
		private int _borderWidth;
		private int _borderRadius;
		private object? _color;
		private FontOptions? _font;
		private int _padding = 4;

		/// <summary>
		/// Returns or sets whether the data labels are displayed (datalabels option <c>display</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to show the labels; otherwise <c>false</c>. The default is <c>false</c>.
		/// </value>
		/// <remarks>
		/// This value is always serialized, so the labels are hidden unless the property is set to <c>true</c>.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.Display = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.Never)]
		[DefaultValue(false)]
		[Description("Display data labels.")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Returns or sets the anchor point of the label on the data element (datalabels option <c>anchor</c>).
		/// </summary>
		/// <value>
		/// One of <c>"center"</c>, <c>"start"</c> or <c>"end"</c>, or <c>null</c> (default) to use the plugin default (<c>"center"</c>).
		/// </value>
		/// <remarks>
		/// <c>"start"</c> and <c>"end"</c> refer to the lowest and highest element boundaries (e.g. base and top of a bar).
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.Display = true;
		/// labels.Anchor = "end";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("anchor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Anchor point.")]
		public string? Anchor
		{
			get => _anchor;
			set => SetProperty(ref _anchor, value);
		}

		/// <summary>
		/// Returns or sets the position of the label relative to its anchor point (datalabels option <c>align</c>).
		/// </summary>
		/// <value>
		/// One of <c>"center"</c>, <c>"start"</c>, <c>"end"</c>, <c>"left"</c>, <c>"right"</c>, <c>"top"</c> or <c>"bottom"</c>, or <c>null</c> (default) to use the plugin default (<c>"center"</c>).
		/// </value>
		/// <remarks>
		/// <c>"start"</c> and <c>"end"</c> are relative to the anchor point and follow the direction of the element (e.g. <c>"end"</c> places the label outside the top of a vertical bar when combined with <see cref="Anchor"/> <c>"end"</c>).
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.Anchor = "end";
		/// labels.Align = "top";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("align")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Alignment.")]
		public string? Align
		{
			get => _align;
			set => SetProperty(ref _align, value);
		}

		/// <summary>
		/// Returns or sets the distance, in pixels, used to pull the label away from its anchor point (datalabels option <c>offset</c>).
		/// </summary>
		/// <value>
		/// The offset in pixels. The default is <c>4</c>.
		/// </value>
		/// <remarks>
		/// The offset is applied in the direction defined by <see cref="Align"/> and has no effect when <see cref="Align"/> is <c>"center"</c>.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.Align = "end";
		/// labels.Offset = 8;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("offset")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(4)]
		[Description("Offset distance.")]
		public int Offset
		{
			get => _offset;
			set => SetProperty(ref _offset, value);
		}

		/// <summary>
		/// Returns or sets the background color of the label box (datalabels option <c>backgroundColor</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors (one per data point). The default is <c>null</c> (no background).
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.BackgroundColor = "rgba(0, 0, 0, 0.6)";
		/// labels.Color = System.Drawing.Color.White;
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
		/// Returns or sets the border color of the label box (datalabels option <c>borderColor</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors. The default is <c>null</c> (no border color).
		/// </value>
		/// <remarks>
		/// The border is drawn only when <see cref="BorderWidth"/> is greater than zero.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.BorderWidth = 1;
		/// labels.BorderColor = System.Drawing.Color.Gray;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Border color.")]
		public object? BorderColor
		{
			get => _borderColor;
			set => SetProperty(ref _borderColor, value);
		}

		/// <summary>
		/// Returns or sets the border width, in pixels, of the label box (datalabels option <c>borderWidth</c>).
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
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.BorderWidth = 2;
		/// labels.BorderColor = "#336699";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Border width.")]
		public int BorderWidth
		{
			get => _borderWidth;
			set => SetProperty(ref _borderWidth, value);
		}

		/// <summary>
		/// Returns or sets the corner radius, in pixels, of the label box (datalabels option <c>borderRadius</c>).
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
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.BackgroundColor = "#eeeeee";
		/// labels.BorderRadius = 4;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderRadius")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Border radius.")]
		public int BorderRadius
		{
			get => _borderRadius;
			set => SetProperty(ref _borderRadius, value);
		}

		/// <summary>
		/// Returns or sets the text color of the labels (datalabels option <c>color</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors (one per data point). The default is <c>null</c>, which uses the Chart.js default font color.
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.Color = System.Drawing.Color.White;
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
		/// Returns or sets the font used to render the labels (datalabels option <c>font</c>).
		/// </summary>
		/// <value>
		/// A <see cref="FontOptions"/> instance. The getter lazily creates a default instance on first access; the default is <c>null</c> (plugin default font).
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.Font.Size = 14;
		/// labels.Font.Weight = "bold";
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
		/// Returns or sets the padding, in pixels, applied inside the label box on all sides (datalabels option <c>padding</c>).
		/// </summary>
		/// <value>
		/// The padding in pixels. The default is <c>4</c>.
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.BackgroundColor = "#ffffcc";
		/// labels.Padding = 6;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Padding.")]
		[DefaultValue(4)]
		public int Padding
		{
			get => _padding;
			set => SetProperty(ref _padding, value);
		}

		/// <summary>
		/// Returns or sets a dictionary of additional Chart.js options that are not exposed as typed properties of the data labels options, such as <c>rotation</c>, <c>textAlign</c>, <c>clamp</c> or <c>clip</c>.
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
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.ExtensionData = new System.Collections.Generic.Dictionary<string, object>
		/// {
		///     ["rotation"] = -45
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
		/// Returns whether the <see cref="Display"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Display"/> is not <c>false</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (labels.ShouldSerializeDisplay())
		///     labels.ResetDisplay();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDisplay() => Display != false;

		/// <summary>
		/// Resets the <see cref="Display"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Note that the current implementation sets <see cref="Display"/> to <c>true</c>, which differs from the declared default value (<c>false</c>).
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.ResetDisplay();
		/// ]]></code>
		/// </example>
		public void ResetDisplay() => Display = true;

		/// <summary>
		/// Returns whether the <see cref="Anchor"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Anchor"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (labels.ShouldSerializeAnchor())
		///     labels.ResetAnchor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeAnchor() => Anchor != null;

		/// <summary>
		/// Resets the <see cref="Anchor"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Anchor"/> to <c>null</c> (plugin default).
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.ResetAnchor();
		/// ]]></code>
		/// </example>
		public void ResetAnchor() => Anchor = null;

		/// <summary>
		/// Returns whether the <see cref="Align"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Align"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (labels.ShouldSerializeAlign())
		///     labels.ResetAlign();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeAlign() => Align != null;

		/// <summary>
		/// Resets the <see cref="Align"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Align"/> to <c>null</c> (plugin default).
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.ResetAlign();
		/// ]]></code>
		/// </example>
		public void ResetAlign() => Align = null;

		/// <summary>
		/// Returns whether the <see cref="Offset"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Offset"/> is not <c>4</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (labels.ShouldSerializeOffset())
		///     labels.ResetOffset();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeOffset() => Offset != 4;

		/// <summary>
		/// Resets the <see cref="Offset"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Offset"/> to <c>4</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.ResetOffset();
		/// ]]></code>
		/// </example>
		public void ResetOffset() => Offset = 4;

		/// <summary>
		/// Returns whether the <see cref="BackgroundColor"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BackgroundColor"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (labels.ShouldSerializeBackgroundColor())
		///     labels.ResetBackgroundColor();
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
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.ResetBackgroundColor();
		/// ]]></code>
		/// </example>
		public void ResetBackgroundColor() => BackgroundColor = null;

		/// <summary>
		/// Returns whether the <see cref="BorderColor"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BorderColor"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (labels.ShouldSerializeBorderColor())
		///     labels.ResetBorderColor();
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
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.ResetBorderColor();
		/// ]]></code>
		/// </example>
		public void ResetBorderColor() => BorderColor = null;

		/// <summary>
		/// Returns whether the <see cref="BorderWidth"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BorderWidth"/> is not <c>0</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (labels.ShouldSerializeBorderWidth())
		///     labels.ResetBorderWidth();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBorderWidth() => BorderWidth != 0;

		/// <summary>
		/// Resets the <see cref="BorderWidth"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="BorderWidth"/> to <c>0</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.ResetBorderWidth();
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
		/// if (labels.ShouldSerializeBorderRadius())
		///     labels.ResetBorderRadius();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBorderRadius() => BorderRadius != 0;

		/// <summary>
		/// Resets the <see cref="BorderRadius"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="BorderRadius"/> to <c>0</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.ResetBorderRadius();
		/// ]]></code>
		/// </example>
		public void ResetBorderRadius() => BorderRadius = 0;

		/// <summary>
		/// Returns whether the <see cref="Color"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Color"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (labels.ShouldSerializeColor())
		///     labels.ResetColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeColor() => Color != null;

		/// <summary>
		/// Resets the <see cref="Color"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Color"/> to <c>null</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.ResetColor();
		/// ]]></code>
		/// </example>
		public void ResetColor() => Color = null;

		/// <summary>
		/// Returns whether the <see cref="Font"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the font options have been created and at least one of their values differs from its default; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (labels.ShouldSerializeFont())
		///     labels.ResetFont();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeFont() => Font != null && !Font.IsDefault;

		/// <summary>
		/// Resets the <see cref="Font"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Font"/> to <c>null</c>; a new default instance is created on the next access.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.ResetFont();
		/// ]]></code>
		/// </example>
		public void ResetFont() => Font = null;

		/// <summary>
		/// Returns whether the <see cref="Padding"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Padding"/> is not <c>4</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (labels.ShouldSerializePadding())
		///     labels.ResetPadding();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePadding() => !Padding.Equals(4);

		/// <summary>
		/// Resets the <see cref="Padding"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Padding"/> to <c>4</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var labels = chart.ChartOptions.Plugins.DataLabels;
		/// labels.ResetPadding();
		/// ]]></code>
		/// </example>
		public void ResetPadding() => Padding = 4;

		/// <inheritdoc/>
		protected override void OnChartChanged()
		{
			if (_font != null)
				_font.Chart = Chart;
		}

	}
}
