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
	/// Scales options (Chart.js <c>options.scales</c>).
	/// </summary>
	/// <remarks>
	/// Configures the <c>x</c> and <c>y</c> axes of cartesian charts through <see cref="X"/> and <see cref="Y"/>, which are
	/// created lazily on first access. Additional axes (e.g. a secondary <c>y1</c> axis, or the radial <c>r</c> scale of
	/// radar and polar area charts) can be supplied through <see cref="ExtensionData"/>.
	/// An instance is obtained from <see cref="ChartOptions.Scales"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var scales = chart.ChartOptions.Scales;
	/// scales.X.Title.Display = true;
	/// scales.X.Title.Text = "Month";
	/// scales.Y.Min = 0;
	/// scales.Y.SuggestedMax = 100;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class ScalesOptions : OptionsBase
	{
		private AxisOptions? _x;
		private AxisOptions? _y;

		/// <summary>
		/// Returns or sets the X axis configuration.
		/// </summary>
		/// <value>
		/// An <see cref="AxisOptions"/> instance. The getter never returns <c>null</c>: a new instance is created on first access.
		/// Serialized as the Chart.js <c>scales.x</c> option through <see cref="XForSerialization"/>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.X.Display = true;
		/// chart.ChartOptions.Scales.X.Stacked = true;
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("X axis configuration.")]
		public AxisOptions? X
		{
			get
			{
				if (_x == null)
					_x = new AxisOptions { Chart = Chart };
				return _x;
			}
			set => SetProperty(ref _x, value);
		}

		/// <summary>
		/// Determines whether the <see cref="X"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the X axis options were created and contain non-default values; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property. Does not create the lazy instance.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (scales.ShouldSerializeX())
		///     scales.ResetX();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeX() => _x != null && !_x.IsDefault;

		/// <summary>
		/// Resets the <see cref="X"/> property by discarding the current X axis options.
		/// </summary>
		/// <remarks>
		/// A new default <see cref="AxisOptions"/> instance is created on the next access to <see cref="X"/>.
		/// Used by the Visual Studio designer when the user resets the property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Scales.ResetX();
		/// ]]></code>
		/// </example>
		public void ResetX() => SetProperty(ref _x, null);

		/// <summary>
		/// Returns the X axis options for JSON serialization only.
		/// </summary>
		/// <value>
		/// The backing <see cref="AxisOptions"/> instance, or <c>null</c> if <see cref="X"/> was never accessed.
		/// </value>
		/// <remarks>
		/// Serialization-only backing property written as the Chart.js <c>scales.x</c> option. Unlike <see cref="X"/>,
		/// it does not create the instance lazily, so an unused axis is omitted from the JSON.
		/// It is hidden from the designer and IntelliSense; use <see cref="X"/> in code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// bool hasXAxis = chart.ChartOptions.Scales.XForSerialization != null;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("x")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public AxisOptions? XForSerialization => _x;

		/// <summary>
		/// Returns or sets the Y axis configuration.
		/// </summary>
		/// <value>
		/// An <see cref="AxisOptions"/> instance. The getter never returns <c>null</c>: a new instance is created on first access.
		/// Serialized as the Chart.js <c>scales.y</c> option through <see cref="YForSerialization"/>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Min = 0;
		/// chart.ChartOptions.Scales.Y.Max = 100;
		/// chart.ChartOptions.Scales.Y.Position = "right";
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Y axis configuration.")]
		public AxisOptions? Y
		{
			get
			{
				if (_y == null)
					_y = new AxisOptions { Chart = Chart };
				return _y;
			}
			set => SetProperty(ref _y, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Y"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the Y axis options were created and contain non-default values; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property. Does not create the lazy instance.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (scales.ShouldSerializeY())
		///     scales.ResetY();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeY() => _y != null && !_y.IsDefault;

		/// <summary>
		/// Resets the <see cref="Y"/> property by discarding the current Y axis options.
		/// </summary>
		/// <remarks>
		/// A new default <see cref="AxisOptions"/> instance is created on the next access to <see cref="Y"/>.
		/// Used by the Visual Studio designer when the user resets the property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Scales.ResetY();
		/// ]]></code>
		/// </example>
		public void ResetY() => SetProperty(ref _y, null);

		/// <summary>
		/// Returns the Y axis options for JSON serialization only.
		/// </summary>
		/// <value>
		/// The backing <see cref="AxisOptions"/> instance, or <c>null</c> if <see cref="Y"/> was never accessed.
		/// </value>
		/// <remarks>
		/// Serialization-only backing property written as the Chart.js <c>scales.y</c> option. Unlike <see cref="Y"/>,
		/// it does not create the instance lazily, so an unused axis is omitted from the JSON.
		/// It is hidden from the designer and IntelliSense; use <see cref="Y"/> in code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// bool hasYAxis = chart.ChartOptions.Scales.YForSerialization != null;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("y")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public AxisOptions? YForSerialization => _y;

		/// <summary>
		/// Returns or sets additional custom properties that can be serialized to JSON.
		/// This allows for custom axis configurations.
		/// </summary>
		/// <value>
		/// A dictionary whose keys are axis IDs (e.g. <c>"y1"</c>, <c>"r"</c>) and whose values are axis configurations
		/// (an <see cref="AxisOptions"/> instance or an anonymous object), or <c>null</c>. The default is <c>null</c>.
		/// </value>
		/// <remarks>
		/// Marked with <see cref="JsonExtensionDataAttribute"/>: every entry is written as an additional axis in the Chart.js
		/// <c>scales</c> object, next to <c>x</c> and <c>y</c>. Datasets reference extra y-axes through
		/// <see cref="ChartDataSet.YAxisID"/>. Changes to the dictionary do not refresh the chart automatically.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.Scales.ExtensionData = new Dictionary<string, object>
		/// {
		///     ["y1"] = new { type = "linear", position = "right" }
		/// };
		/// chart.DataSets.Add(new LineDataSet { Label = "Rate", YAxisID = "y1" });
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
			if (_x != null)
				_x.Chart = Chart;
			if (_y != null)
				_y.Chart = Chart;
		}
	}
}
