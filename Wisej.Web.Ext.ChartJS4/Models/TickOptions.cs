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
	/// Represents the options of the tick labels of an axis (Chart.js <c>scales[id].ticks</c>).
	/// </summary>
	/// <remarks>
	/// An instance is lazily created by <see cref="AxisOptions.Ticks"/>. Additional Chart.js tick options,
	/// such as <c>stepSize</c>, <c>maxTicksLimit</c>, <c>autoSkip</c> or <c>precision</c>, can be supplied
	/// through <see cref="ExtensionData"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var ticks = chart.ChartOptions.Scales.X.Ticks;
	/// ticks.Color = System.Drawing.Color.DimGray;
	/// ticks.MaxRotation = 90;
	/// ticks.MinRotation = 45;
	/// ticks.Font.Size = 10;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class TickOptions : OptionsBase
	{
		private bool _display = true;
		private object? _color;
		private FontOptions? _font;
		private int _maxRotation = 50;
		private int _minRotation;
		private bool _mirror;
		private string? _align;
		private int _padding = 3;

		/// <summary>
		/// Returns or sets a value indicating whether the tick labels are displayed (Chart.js <c>ticks.display</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to show the tick labels; otherwise, <c>false</c>. The default is <c>true</c>.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// bool ticksVisible = chart.ChartOptions.Scales.Y.Ticks.Display;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Display tick labels.")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Returns or sets the color of the tick labels (Chart.js <c>ticks.color</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors (one per tick),
		/// or <c>null</c> (default) to use the Chart.js default font color.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Ticks.Color = "#666666";
		/// chart.ChartOptions.Scales.Y.Ticks.Color = System.Drawing.Color.Navy;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("color")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Tick label color.")]
		public object? Color
		{
			get => _color;
			set => SetProperty(ref _color, value);
		}

		/// <summary>
		/// Returns or sets the font used to render the tick labels (Chart.js <c>ticks.font</c>).
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
		/// chart.ChartOptions.Scales.X.Ticks.Font.Family = "Courier New";
		/// chart.ChartOptions.Scales.X.Ticks.Font.Size = 10;
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
		/// Returns or sets the maximum rotation, in degrees, of the tick labels when they are rotated to
		/// condense labels (Chart.js <c>ticks.maxRotation</c>).
		/// </summary>
		/// <value>
		/// The maximum rotation angle in degrees. The default is <c>50</c>.
		/// </value>
		/// <remarks>
		/// Rotation only happens when necessary. Applies only to horizontal scales.
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Ticks.MaxRotation = 90;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("maxRotation")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(50)]
		[Description("Maximum rotation angle.")]
		public int MaxRotation
		{
			get => _maxRotation;
			set => SetProperty(ref _maxRotation, value);
		}

		/// <summary>
		/// Returns or sets the minimum rotation, in degrees, of the tick labels (Chart.js <c>ticks.minRotation</c>).
		/// </summary>
		/// <value>
		/// The minimum rotation angle in degrees. The default is <c>0</c>.
		/// </value>
		/// <remarks>
		/// Applies only to horizontal scales. Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Ticks.MinRotation = 45;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("minRotation")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Minimum rotation angle.")]
		public int MinRotation
		{
			get => _minRotation;
			set => SetProperty(ref _minRotation, value);
		}

		/// <summary>
		/// Returns or sets a value indicating whether the tick labels are flipped around the axis, displaying
		/// the labels inside the chart area instead of outside (Chart.js <c>ticks.mirror</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to mirror the labels; otherwise, <c>false</c>. The default is <c>false</c>.
		/// </value>
		/// <remarks>
		/// Applies only to vertical scales. Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Ticks.Mirror = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("mirror")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Mirror tick labels.")]
		public bool Mirror
		{
			get => _mirror;
			set => SetProperty(ref _mirror, value);
		}

		/// <summary>
		/// Returns or sets the alignment of the tick labels along the axis (Chart.js <c>ticks.align</c>).
		/// </summary>
		/// <value>
		/// One of <c>"start"</c>, <c>"center"</c>, <c>"end"</c> or <c>"inner"</c>, or <c>null</c> (default)
		/// to use the Chart.js default (<c>"center"</c>).
		/// </value>
		/// <remarks>
		/// <c>"inner"</c> aligns the first tick label to the start and the last one to the end of the axis.
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Ticks.Align = "start";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("align")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Alignment of tick labels.")]
		public string? Align
		{
			get => _align;
			set => SetProperty(ref _align, value);
		}

		/// <summary>
		/// Returns or sets the padding, in pixels, between the tick labels and the axis (Chart.js <c>ticks.padding</c>).
		/// </summary>
		/// <value>
		/// The padding in pixels. The default is <c>3</c>.
		/// </value>
		/// <remarks>
		/// When set on a horizontal axis, this applies in the vertical (Y) direction; on a vertical axis, in the
		/// horizontal (X) direction. Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Ticks.Padding = 10;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Padding between label and axis.")]
		[DefaultValue(3)]
		public int Padding
		{
			get => _padding;
			set => SetProperty(ref _padding, value);
		}

		/// <summary>
		/// Returns or sets additional, arbitrary Chart.js tick options that are not exposed as typed properties.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c> (default).
		/// </value>
		/// <remarks>
		/// This property is marked with <c>[JsonExtensionData]</c>: each entry is written as a sibling
		/// property of the <c>ticks</c> JSON object sent to Chart.js (e.g. <c>stepSize</c>, <c>maxTicksLimit</c>,
		/// <c>autoSkip</c>, <c>precision</c>). It is hidden from the designer and property grid.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Ticks.ExtensionData = new Dictionary<string, object>
		/// {
		///     { "stepSize", 10 },
		///     { "maxTicksLimit", 8 }
		/// };
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
		/// Determines whether the <see cref="Align"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Align"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetAlign"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions();
		/// if (ticks.ShouldSerializeAlign())
		///     ticks.ResetAlign();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeAlign() => Align != null;

		/// <summary>
		/// Resets the <see cref="Align"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions { Align = "end" };
		/// ticks.ResetAlign();
		/// ]]></code>
		/// </example>
		public void ResetAlign() => Align = null;

		/// <summary>
		/// Determines whether the <see cref="Display"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Display"/> is not <c>true</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetDisplay"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions();
		/// if (ticks.ShouldSerializeDisplay())
		///     ticks.ResetDisplay();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDisplay() => Display != true;

		/// <summary>
		/// Resets the <see cref="Display"/> property to its default value (<c>true</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions();
		/// ticks.ResetDisplay();
		/// ]]></code>
		/// </example>
		public void ResetDisplay() => Display = true;

		/// <summary>
		/// Determines whether the <see cref="Color"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Color"/> is not <c>null</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetColor"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions();
		/// if (ticks.ShouldSerializeColor())
		///     ticks.ResetColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeColor() => Color != null;

		/// <summary>
		/// Resets the <see cref="Color"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions { Color = "red" };
		/// ticks.ResetColor();
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
		/// var ticks = new TickOptions();
		/// if (ticks.ShouldSerializeFont())
		///     ticks.ResetFont();
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
		/// var ticks = new TickOptions();
		/// ticks.Font.Size = 9;
		/// ticks.ResetFont();
		/// ]]></code>
		/// </example>
		public void ResetFont() => SetProperty(ref _font, null);

		/// <summary>
		/// Determines whether the <see cref="MaxRotation"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="MaxRotation"/> is not <c>50</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetMaxRotation"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions();
		/// if (ticks.ShouldSerializeMaxRotation())
		///     ticks.ResetMaxRotation();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeMaxRotation() => MaxRotation != 50;

		/// <summary>
		/// Resets the <see cref="MaxRotation"/> property to its default value (<c>50</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions { MaxRotation = 90 };
		/// ticks.ResetMaxRotation();
		/// ]]></code>
		/// </example>
		public void ResetMaxRotation() => MaxRotation = 50;

		/// <summary>
		/// Determines whether the <see cref="MinRotation"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="MinRotation"/> is not <c>0</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetMinRotation"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions();
		/// if (ticks.ShouldSerializeMinRotation())
		///     ticks.ResetMinRotation();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeMinRotation() => MinRotation != 0;

		/// <summary>
		/// Resets the <see cref="MinRotation"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions { MinRotation = 30 };
		/// ticks.ResetMinRotation();
		/// ]]></code>
		/// </example>
		public void ResetMinRotation() => MinRotation = 0;

		/// <summary>
		/// Determines whether the <see cref="Mirror"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Mirror"/> is <c>true</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetMirror"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions();
		/// if (ticks.ShouldSerializeMirror())
		///     ticks.ResetMirror();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeMirror() => Mirror != false;

		/// <summary>
		/// Resets the <see cref="Mirror"/> property to its default value (<c>false</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions { Mirror = true };
		/// ticks.ResetMirror();
		/// ]]></code>
		/// </example>
		public void ResetMirror() => Mirror = false;

		/// <summary>
		/// Determines whether the <see cref="Padding"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Padding"/> is not <c>3</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetPadding"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions();
		/// if (ticks.ShouldSerializePadding())
		///     ticks.ResetPadding();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePadding() => Padding != 3;

		/// <summary>
		/// Resets the <see cref="Padding"/> property to its default value (<c>3</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var ticks = new TickOptions { Padding = 10 };
		/// ticks.ResetPadding();
		/// ]]></code>
		/// </example>
		public void ResetPadding() => Padding = 3;

	}
}
