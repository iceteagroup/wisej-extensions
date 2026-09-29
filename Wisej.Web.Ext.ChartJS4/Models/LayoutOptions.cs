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
	/// Represents the chart layout options (Chart.js <c>options.layout</c>).
	/// </summary>
	/// <remarks>
	/// Use <see cref="Padding"/> to reserve space between the edge of the canvas and the chart area.
	/// An instance is lazily created by <see cref="ChartOptions.Layout"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// chart.ChartOptions.Layout.Padding.Top = 20;
	/// chart.ChartOptions.Layout.Padding.Bottom = 10;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class LayoutOptions : OptionsBase
	{
		private PaddingOptions? _padding;

		/// <summary>
		/// Returns or sets the padding to add inside the chart, around the chart area (Chart.js <c>layout.padding</c>).
		/// </summary>
		/// <value>
		/// A <see cref="PaddingOptions"/> instance. The getter lazily creates a default instance when none is set.
		/// </value>
		/// <remarks>
		/// Setting any of the padding values refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Layout.Padding.Left = 15;
		/// chart.ChartOptions.Layout.Padding.Right = 15;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Padding inside the chart.")]
		public PaddingOptions? Padding
		{
			get
			{
				if (_padding == null)
					_padding = new PaddingOptions { Chart = Chart };
				return _padding;
			}
			set => SetProperty(ref _padding, value);
		}

		/// <summary>
		/// Returns or sets additional, arbitrary Chart.js layout options that are not exposed as typed properties.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c> (default).
		/// </value>
		/// <remarks>
		/// This property is marked with <c>[JsonExtensionData]</c>: each entry is written as a sibling
		/// property of the <c>layout</c> JSON object sent to Chart.js (e.g. <c>autoPadding</c>).
		/// It is hidden from the designer and property grid.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Layout.ExtensionData = new Dictionary<string, object>
		/// {
		///     { "autoPadding", false }
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
		/// Determines whether the <see cref="Padding"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the designer should persist the <see cref="Padding"/> value; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetPadding"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var layout = new LayoutOptions();
		/// if (layout.ShouldSerializePadding())
		///     layout.ResetPadding();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePadding() => Padding != null && Padding.IsDefault;

		/// <summary>
		/// Resets the <see cref="Padding"/> property to its default value by discarding the current <see cref="PaddingOptions"/> instance.
		/// </summary>
		/// <remarks>
		/// Used by the designer. A new default instance is created the next time <see cref="Padding"/> is read.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Layout.ResetPadding();
		/// ]]></code>
		/// </example>
		public void ResetPadding() => SetProperty(ref _padding, null);

		/// <summary>
		/// Determines whether the <see cref="ExtensionData"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="ExtensionData"/> contains at least one entry; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetExtensionData"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var layout = new LayoutOptions();
		/// if (layout.ShouldSerializeExtensionData())
		///     layout.ResetExtensionData();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeExtensionData() => ExtensionData != null && ExtensionData.Count > 0;

		/// <summary>
		/// Resets the <see cref="ExtensionData"/> property to its default value (<c>null</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var layout = new LayoutOptions();
		/// layout.ResetExtensionData();
		/// ]]></code>
		/// </example>
		public void ResetExtensionData() => ExtensionData = null;

		/// <inheritdoc/>
		protected override void OnChartChanged()
		{
			if (_padding != null)
				_padding.Chart = Chart;
		}
	}
}
