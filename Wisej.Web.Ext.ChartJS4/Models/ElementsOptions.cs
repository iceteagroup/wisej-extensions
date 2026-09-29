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
using Wisej.Core;

namespace Wisej.Web.Ext.ChartJS4.Models
{
	/// <summary>
	/// Represents the default styling options of the chart elements (Chart.js <c>options.elements</c>): points, lines, bars and arcs.
	/// </summary>
	/// <remarks>
	/// Assign an instance to <see cref="ChartOptions.Elements"/>. The values defined here apply to all datasets of the chart unless overridden by the dataset options.
	/// The nested element options are created lazily on first access and only configured element options are serialized.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var elements = new ElementsOptions();
	/// elements.Line.Tension = 0.4;
	/// elements.Point.Radius = 5;
	/// elements.Bar.BorderRadius = 6;
	/// chart.ChartOptions.Elements = elements;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class ElementsOptions : OptionsBase
	{
		private PointElementOptions? _point;
		private LineElementOptions? _line;
		private BarElementOptions? _bar;
		private ArcElementOptions? _arc;

		/// <summary>
		/// Returns or sets the default styling of the point elements used by line, radar, scatter and bubble charts (Chart.js <c>options.elements.point</c>).
		/// </summary>
		/// <value>
		/// A <see cref="PointElementOptions"/> instance. The getter lazily creates a default instance on first access; the default is <c>null</c> (not configured).
		/// </value>
		/// <remarks>
		/// These defaults apply to all datasets of the chart unless overridden by the dataset options. Only a configured instance is sent to the client.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var elements = new ElementsOptions();
		/// elements.Point.Radius = 5;
		/// elements.Point.PointStyle = "rectRot";
		/// chart.ChartOptions.Elements = elements;
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Point element options.")]
		public PointElementOptions? Point
		{
			get
			{
				if (_point == null)
					_point = new PointElementOptions { Chart = Chart };
				return _point;
			}
			set => SetProperty(ref _point, value);
		}

		/// <summary>
		/// Returns whether the <see cref="Point"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the Point options have been created and at least one of their values differs from its default; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (elements.ShouldSerializePoint())
		///     elements.ResetPoint();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePoint() => _point != null && !_point.IsDefault;

		/// <summary>
		/// Resets the <see cref="Point"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Discards the current <see cref="PointElementOptions"/> instance; a new default instance is created on the next access to <see cref="Point"/>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var elements = new ElementsOptions();
		/// elements.ResetPoint();
		/// ]]></code>
		/// </example>
		public void ResetPoint() => SetProperty(ref _point, null);

		/// <summary>
		/// Returns the point element options instance used for JSON serialization, without lazily creating it.
		/// </summary>
		/// <value>
		/// The current <see cref="PointElementOptions"/> instance, or <c>null</c> when it has never been created. Serialized as <c>"point"</c>.
		/// </value>
		/// <remarks>
		/// This is a serialization-only backing property: unlike <see cref="Point"/> it does not instantiate the options object, so unused element options are omitted from the Chart.js configuration. It is hidden from the property grid and IntelliSense and is not meant to be used in application code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var elements = new ElementsOptions();
		/// var json = System.Text.Json.JsonSerializer.Serialize(elements.PointForSerialization);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("point")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public PointElementOptions? PointForSerialization => _point;

		/// <summary>
		/// Returns or sets the default styling of the line elements used by line and radar charts (Chart.js <c>options.elements.line</c>).
		/// </summary>
		/// <value>
		/// A <see cref="LineElementOptions"/> instance. The getter lazily creates a default instance on first access; the default is <c>null</c> (not configured).
		/// </value>
		/// <remarks>
		/// These defaults apply to all datasets of the chart unless overridden by the dataset options. Only a configured instance is sent to the client.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var elements = new ElementsOptions();
		/// elements.Line.Tension = 0.4;
		/// elements.Line.BorderWidth = 2;
		/// chart.ChartOptions.Elements = elements;
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Line element options.")]
		public LineElementOptions? Line
		{
			get
			{
				if (_line == null)
					_line = new LineElementOptions { Chart = Chart };
				return _line;
			}
			set => SetProperty(ref _line, value);
		}

		/// <summary>
		/// Returns whether the <see cref="Line"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the Line options have been created and at least one of their values differs from its default; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (elements.ShouldSerializeLine())
		///     elements.ResetLine();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeLine() => _line != null && !_line.IsDefault;

		/// <summary>
		/// Resets the <see cref="Line"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Discards the current <see cref="LineElementOptions"/> instance; a new default instance is created on the next access to <see cref="Line"/>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var elements = new ElementsOptions();
		/// elements.ResetLine();
		/// ]]></code>
		/// </example>
		public void ResetLine() => SetProperty(ref _line, null);

		/// <summary>
		/// Returns the line element options instance used for JSON serialization, without lazily creating it.
		/// </summary>
		/// <value>
		/// The current <see cref="LineElementOptions"/> instance, or <c>null</c> when it has never been created. Serialized as <c>"line"</c>.
		/// </value>
		/// <remarks>
		/// This is a serialization-only backing property: unlike <see cref="Line"/> it does not instantiate the options object, so unused element options are omitted from the Chart.js configuration. It is hidden from the property grid and IntelliSense and is not meant to be used in application code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var elements = new ElementsOptions();
		/// var json = System.Text.Json.JsonSerializer.Serialize(elements.LineForSerialization);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("line")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public LineElementOptions? LineForSerialization => _line;

		/// <summary>
		/// Returns or sets the default styling of the bar elements used by bar charts (Chart.js <c>options.elements.bar</c>).
		/// </summary>
		/// <value>
		/// A <see cref="BarElementOptions"/> instance. The getter lazily creates a default instance on first access; the default is <c>null</c> (not configured).
		/// </value>
		/// <remarks>
		/// These defaults apply to all datasets of the chart unless overridden by the dataset options. Only a configured instance is sent to the client.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var elements = new ElementsOptions();
		/// elements.Bar.BorderWidth = 1;
		/// elements.Bar.BorderRadius = 6;
		/// chart.ChartOptions.Elements = elements;
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Bar element options.")]
		public BarElementOptions? Bar
		{
			get
			{
				if (_bar == null)
					_bar = new BarElementOptions { Chart = Chart };
				return _bar;
			}
			set => SetProperty(ref _bar, value);
		}

		/// <summary>
		/// Returns whether the <see cref="Bar"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the Bar options have been created and at least one of their values differs from its default; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (elements.ShouldSerializeBar())
		///     elements.ResetBar();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBar() => _bar != null && !_bar.IsDefault;

		/// <summary>
		/// Resets the <see cref="Bar"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Discards the current <see cref="BarElementOptions"/> instance; a new default instance is created on the next access to <see cref="Bar"/>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var elements = new ElementsOptions();
		/// elements.ResetBar();
		/// ]]></code>
		/// </example>
		public void ResetBar() => SetProperty(ref _bar, null);

		/// <summary>
		/// Returns the bar element options instance used for JSON serialization, without lazily creating it.
		/// </summary>
		/// <value>
		/// The current <see cref="BarElementOptions"/> instance, or <c>null</c> when it has never been created. Serialized as <c>"bar"</c>.
		/// </value>
		/// <remarks>
		/// This is a serialization-only backing property: unlike <see cref="Bar"/> it does not instantiate the options object, so unused element options are omitted from the Chart.js configuration. It is hidden from the property grid and IntelliSense and is not meant to be used in application code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var elements = new ElementsOptions();
		/// var json = System.Text.Json.JsonSerializer.Serialize(elements.BarForSerialization);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("bar")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public BarElementOptions? BarForSerialization => _bar;

		/// <summary>
		/// Returns or sets the default styling of the arc elements used by pie, doughnut and polar area charts (Chart.js <c>options.elements.arc</c>).
		/// </summary>
		/// <value>
		/// A <see cref="ArcElementOptions"/> instance. The getter lazily creates a default instance on first access; the default is <c>null</c> (not configured).
		/// </value>
		/// <remarks>
		/// These defaults apply to all datasets of the chart unless overridden by the dataset options. Only a configured instance is sent to the client.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var elements = new ElementsOptions();
		/// elements.Arc.BorderWidth = 1;
		/// elements.Arc.BorderColor = System.Drawing.Color.White;
		/// chart.ChartOptions.Elements = elements;
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Arc element options.")]
		public ArcElementOptions? Arc
		{
			get
			{
				if (_arc == null)
					_arc = new ArcElementOptions { Chart = Chart };
				return _arc;
			}
			set => SetProperty(ref _arc, value);
		}

		/// <summary>
		/// Returns whether the <see cref="Arc"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the Arc options have been created and at least one of their values differs from its default; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (elements.ShouldSerializeArc())
		///     elements.ResetArc();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeArc() => _arc != null && !_arc.IsDefault;

		/// <summary>
		/// Resets the <see cref="Arc"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Discards the current <see cref="ArcElementOptions"/> instance; a new default instance is created on the next access to <see cref="Arc"/>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var elements = new ElementsOptions();
		/// elements.ResetArc();
		/// ]]></code>
		/// </example>
		public void ResetArc() => SetProperty(ref _arc, null);

		/// <summary>
		/// Returns the arc element options instance used for JSON serialization, without lazily creating it.
		/// </summary>
		/// <value>
		/// The current <see cref="ArcElementOptions"/> instance, or <c>null</c> when it has never been created. Serialized as <c>"arc"</c>.
		/// </value>
		/// <remarks>
		/// This is a serialization-only backing property: unlike <see cref="Arc"/> it does not instantiate the options object, so unused element options are omitted from the Chart.js configuration. It is hidden from the property grid and IntelliSense and is not meant to be used in application code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var elements = new ElementsOptions();
		/// var json = System.Text.Json.JsonSerializer.Serialize(elements.ArcForSerialization);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("arc")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ArcElementOptions? ArcForSerialization => _arc;

		/// <summary>
		/// Returns or sets a dictionary of additional Chart.js options that are not exposed as typed properties of the element options (additional element types).
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
		/// var elements = new ElementsOptions();
		/// elements.ExtensionData = new System.Collections.Generic.Dictionary<string, object>
		/// {
		///     ["arc"] = new { borderAlign = "inner" }
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
			if (_point != null)
				_point.Chart = Chart;
			if (_line != null)
				_line.Chart = Chart;
			if (_bar != null)
				_bar.Chart = Chart;
			if (_arc != null)
				_arc.Chart = Chart;
		}
	}
}
