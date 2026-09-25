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
	/// Represents the configuration of the built-in Chart.js plugins (Chart.js <c>options.plugins</c>):
	/// legend, title, subtitle, tooltip, decimation, filler and the chartjs-plugin-datalabels plugin.
	/// </summary>
	/// <remarks>
	/// Nested plugin options are not instantiated by default. They are created lazily the first time the corresponding property is read, and only configured plugins are serialized to the client.
	/// Options for plugins that are not exposed as typed properties can be added through <see cref="ExtensionData"/> or <see cref="CustomOptions"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// chart.ChartOptions.Plugins.Legend.Position = "bottom";
	/// chart.ChartOptions.Plugins.Title.Display = true;
	/// chart.ChartOptions.Plugins.Title.Text = "Monthly Sales";
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class PluginsOptions : OptionsBase
	{
		private LegendOptions? _legend;
		private TitleOptions? _title;
		private TooltipOptions? _tooltip;
		private SubtitleOptions? _subtitle;
		private DecimationOptions? _decimation;
		private FillerOptions? _filler;
		private DataLabelsOptions? _dataLabels;

		/// <summary>
		/// Initializes a new instance of the <see cref="PluginsOptions"/> class.
		/// </summary>
		/// <remarks>
		/// The nested plugin options (<see cref="Legend"/>, <see cref="Title"/>, <see cref="Tooltip"/>, etc.) are not created by the constructor; they are lazily instantiated on first access.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = new PluginsOptions();
		/// plugins.Tooltip.Mode = "index";
		/// chart.ChartOptions.Plugins = plugins;
		/// ]]></code>
		/// </example>
		public PluginsOptions()
		{
			// Nested options are NOT instantiated by default
			// They will be lazy-loaded on first access
		}

		/// <summary>
		/// Returns or sets the legend configuration (Chart.js <c>options.plugins.legend</c>).
		/// </summary>
		/// <value>
		/// A <see cref="LegendOptions"/> instance. The getter lazily creates a default instance on first access, so it never returns <c>null</c>; the default is <c>null</c> (not configured).
		/// </value>
		/// <remarks>
		/// The legend displays the list of datasets (or data labels for pie/doughnut charts) and allows toggling their visibility.
		/// Only a configured instance is sent to the client; an instance that was never accessed or that still has all default values is omitted from the generated configuration.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.Legend.Display = true;
		/// plugins.Legend.Position = "bottom";
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Legend configuration.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public LegendOptions? Legend
		{
			get
			{
				if (_legend == null)
					_legend = new LegendOptions { Chart = Chart };
				return _legend;
			}
			set => SetProperty(ref _legend, value);
		}

		/// <summary>
		/// Returns whether the <see cref="Legend"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the Legend options have been created and at least one of their values differs from its default; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (plugins.ShouldSerializeLegend())
		///     plugins.ResetLegend();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeLegend() => _legend != null && !_legend.IsDefault;

		/// <summary>
		/// Resets the <see cref="Legend"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Discards the current <see cref="LegendOptions"/> instance; a new default instance is created on the next access to <see cref="Legend"/>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.ResetLegend();
		/// ]]></code>
		/// </example>
		public void ResetLegend() => SetProperty(ref _legend, null);

		/// <summary>
		/// Returns the legend options instance used for JSON serialization, without lazily creating it.
		/// </summary>
		/// <value>
		/// The current <see cref="LegendOptions"/> instance, or <c>null</c> when it has never been created. Serialized as <c>"legend"</c>.
		/// </value>
		/// <remarks>
		/// This is a serialization-only backing property: unlike <see cref="Legend"/> it does not instantiate the options object, so unused options are omitted from the Chart.js configuration. It is hidden from the property grid and IntelliSense and is not meant to be used in application code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// var json = System.Text.Json.JsonSerializer.Serialize(plugins.LegendForSerialization);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("legend")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public LegendOptions? LegendForSerialization => _legend;

		/// <summary>
		/// Returns or sets the chart title configuration (Chart.js <c>options.plugins.title</c>).
		/// </summary>
		/// <value>
		/// A <see cref="TitleOptions"/> instance. The getter lazily creates a default instance on first access, so it never returns <c>null</c>; the default is <c>null</c> (not configured).
		/// </value>
		/// <remarks>
		/// The title is displayed above the chart by default.
		/// Only a configured instance is sent to the client; an instance that was never accessed or that still has all default values is omitted from the generated configuration.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.Title.Display = true;
		/// plugins.Title.Text = "Monthly Sales";
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Title configuration.")]
		[DefaultValue(null)]
		public TitleOptions? Title
		{
			get
			{
				if (_title == null)
					_title = new TitleOptions { Chart = Chart };
				return _title;
			}
			set => SetProperty(ref _title, value);
		}

		/// <summary>
		/// Returns whether the <see cref="Title"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the Title options have been created and at least one of their values differs from its default; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (plugins.ShouldSerializeTitle())
		///     plugins.ResetTitle();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTitle() => _title != null && !_title.IsDefault;

		/// <summary>
		/// Resets the <see cref="Title"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Discards the current <see cref="TitleOptions"/> instance; a new default instance is created on the next access to <see cref="Title"/>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.ResetTitle();
		/// ]]></code>
		/// </example>
		public void ResetTitle() => SetProperty(ref _title, null);

		/// <summary>
		/// Returns the title options instance used for JSON serialization, without lazily creating it.
		/// </summary>
		/// <value>
		/// The current <see cref="TitleOptions"/> instance, or <c>null</c> when it has never been created. Serialized as <c>"title"</c>.
		/// </value>
		/// <remarks>
		/// This is a serialization-only backing property: unlike <see cref="Title"/> it does not instantiate the options object, so unused options are omitted from the Chart.js configuration. It is hidden from the property grid and IntelliSense and is not meant to be used in application code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// var json = System.Text.Json.JsonSerializer.Serialize(plugins.TitleForSerialization);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("title")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public TitleOptions? TitleForSerialization => _title;

		/// <summary>
		/// Returns or sets the tooltip configuration (Chart.js <c>options.plugins.tooltip</c>).
		/// </summary>
		/// <value>
		/// A <see cref="TooltipOptions"/> instance. The getter lazily creates a default instance on first access, so it never returns <c>null</c>; the default is <c>null</c> (not configured).
		/// </value>
		/// <remarks>
		/// The tooltip is displayed when the user hovers over or taps chart elements.
		/// Only a configured instance is sent to the client; an instance that was never accessed or that still has all default values is omitted from the generated configuration.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.Tooltip.Enabled = true;
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Tooltip configuration.")]
		[DefaultValue(null)]
		public TooltipOptions? Tooltip
		{
			get
			{
				if (_tooltip == null)
					_tooltip = new TooltipOptions { Chart = Chart };
				return _tooltip;
			}
			set => SetProperty(ref _tooltip, value);
		}

		/// <summary>
		/// Returns whether the <see cref="Tooltip"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the Tooltip options have been created and at least one of their values differs from its default; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (plugins.ShouldSerializeTooltip())
		///     plugins.ResetTooltip();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTooltip() => _tooltip != null && !_tooltip.IsDefault;

		/// <summary>
		/// Resets the <see cref="Tooltip"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Discards the current <see cref="TooltipOptions"/> instance; a new default instance is created on the next access to <see cref="Tooltip"/>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.ResetTooltip();
		/// ]]></code>
		/// </example>
		public void ResetTooltip() => SetProperty(ref _tooltip, null);

		/// <summary>
		/// Returns the tooltip options instance used for JSON serialization, without lazily creating it.
		/// </summary>
		/// <value>
		/// The current <see cref="TooltipOptions"/> instance, or <c>null</c> when it has never been created. Serialized as <c>"tooltip"</c>.
		/// </value>
		/// <remarks>
		/// This is a serialization-only backing property: unlike <see cref="Tooltip"/> it does not instantiate the options object, so unused options are omitted from the Chart.js configuration. It is hidden from the property grid and IntelliSense and is not meant to be used in application code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// var json = System.Text.Json.JsonSerializer.Serialize(plugins.TooltipForSerialization);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("tooltip")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public TooltipOptions? TooltipForSerialization => _tooltip;

		/// <summary>
		/// Returns or sets the chart subtitle configuration (Chart.js <c>options.plugins.subtitle</c>).
		/// </summary>
		/// <value>
		/// A <see cref="SubtitleOptions"/> instance. The getter lazily creates a default instance on first access, so it never returns <c>null</c>; the default is <c>null</c> (not configured).
		/// </value>
		/// <remarks>
		/// The subtitle is displayed below the title and uses the same options.
		/// Only a configured instance is sent to the client; an instance that was never accessed or that still has all default values is omitted from the generated configuration.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.Subtitle.Display = true;
		/// plugins.Subtitle.Text = "Fiscal year 2025";
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Subtitle configuration.")]
		[DefaultValue(null)]
		public SubtitleOptions? Subtitle
		{
			get
			{
				if (_subtitle == null)
					_subtitle = new SubtitleOptions { Chart = Chart };
				return _subtitle;
			}
			set => SetProperty(ref _subtitle, value);
		}

		/// <summary>
		/// Returns whether the <see cref="Subtitle"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the Subtitle options have been created and at least one of their values differs from its default; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (plugins.ShouldSerializeSubtitle())
		///     plugins.ResetSubtitle();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeSubtitle() => _subtitle != null && !_subtitle.IsDefault;

		/// <summary>
		/// Resets the <see cref="Subtitle"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Discards the current <see cref="SubtitleOptions"/> instance; a new default instance is created on the next access to <see cref="Subtitle"/>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.ResetSubtitle();
		/// ]]></code>
		/// </example>
		public void ResetSubtitle() => SetProperty(ref _subtitle, null);

		/// <summary>
		/// Returns the subtitle options instance used for JSON serialization, without lazily creating it.
		/// </summary>
		/// <value>
		/// The current <see cref="SubtitleOptions"/> instance, or <c>null</c> when it has never been created. Serialized as <c>"subtitle"</c>.
		/// </value>
		/// <remarks>
		/// This is a serialization-only backing property: unlike <see cref="Subtitle"/> it does not instantiate the options object, so unused options are omitted from the Chart.js configuration. It is hidden from the property grid and IntelliSense and is not meant to be used in application code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// var json = System.Text.Json.JsonSerializer.Serialize(plugins.SubtitleForSerialization);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("subtitle")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public SubtitleOptions? SubtitleForSerialization => _subtitle;

		/// <summary>
		/// Returns or sets the decimation plugin configuration used to downsample large line chart datasets (Chart.js <c>options.plugins.decimation</c>).
		/// </summary>
		/// <value>
		/// A <see cref="DecimationOptions"/> instance. The getter lazily creates a default instance on first access, so it never returns <c>null</c>; the default is <c>null</c> (not configured).
		/// </value>
		/// <remarks>
		/// See <see cref="DecimationOptions"/> for the requirements of the Chart.js decimation plugin.
		/// Only a configured instance is sent to the client; an instance that was never accessed or that still has all default values is omitted from the generated configuration.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.Decimation.Enabled = true;
		/// plugins.Decimation.Algorithm = "lttb";
		/// plugins.Decimation.Samples = 500;
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Decimation configuration.")]
		[DefaultValue(null)]
		public DecimationOptions? Decimation
		{
			get
			{
				if (_decimation == null)
					_decimation = new DecimationOptions { Chart = Chart };
				return _decimation;
			}
			set => SetProperty(ref _decimation, value);
		}

		/// <summary>
		/// Returns whether the <see cref="Decimation"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the Decimation options have been created and at least one of their values differs from its default; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (plugins.ShouldSerializeDecimation())
		///     plugins.ResetDecimation();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDecimation() => _decimation != null && !_decimation.IsDefault;

		/// <summary>
		/// Resets the <see cref="Decimation"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Discards the current <see cref="DecimationOptions"/> instance; a new default instance is created on the next access to <see cref="Decimation"/>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.ResetDecimation();
		/// ]]></code>
		/// </example>
		public void ResetDecimation() => SetProperty(ref _decimation, null);

		/// <summary>
		/// Returns the decimation options instance used for JSON serialization, without lazily creating it.
		/// </summary>
		/// <value>
		/// The current <see cref="DecimationOptions"/> instance, or <c>null</c> when it has never been created. Serialized as <c>"decimation"</c>.
		/// </value>
		/// <remarks>
		/// This is a serialization-only backing property: unlike <see cref="Decimation"/> it does not instantiate the options object, so unused options are omitted from the Chart.js configuration. It is hidden from the property grid and IntelliSense and is not meant to be used in application code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// var json = System.Text.Json.JsonSerializer.Serialize(plugins.DecimationForSerialization);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("decimation")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DecimationOptions? DecimationForSerialization => _decimation;

		/// <summary>
		/// Returns or sets the filler plugin configuration used by area (filled line and radar) charts (Chart.js <c>options.plugins.filler</c>).
		/// </summary>
		/// <value>
		/// A <see cref="FillerOptions"/> instance. The getter lazily creates a default instance on first access, so it never returns <c>null</c>; the default is <c>null</c> (not configured).
		/// </value>
		/// <remarks>
		/// Only a configured instance is sent to the client; an instance that was never accessed or that still has all default values is omitted from the generated configuration.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// bool propagate = plugins.Filler.Propagate;
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Filler configuration.")]
		[DefaultValue(null)]
		public FillerOptions? Filler
		{
			get
			{
				if (_filler == null)
					_filler = new FillerOptions { Chart = Chart };
				return _filler;
			}
			set => SetProperty(ref _filler, value);
		}

		/// <summary>
		/// Returns whether the <see cref="Filler"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the Filler options have been created and at least one of their values differs from its default; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (plugins.ShouldSerializeFiller())
		///     plugins.ResetFiller();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeFiller() => _filler != null && !_filler.IsDefault;

		/// <summary>
		/// Resets the <see cref="Filler"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Discards the current <see cref="FillerOptions"/> instance; a new default instance is created on the next access to <see cref="Filler"/>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.ResetFiller();
		/// ]]></code>
		/// </example>
		public void ResetFiller() => SetProperty(ref _filler, null);

		/// <summary>
		/// Returns the filler options instance used for JSON serialization, without lazily creating it.
		/// </summary>
		/// <value>
		/// The current <see cref="FillerOptions"/> instance, or <c>null</c> when it has never been created. Serialized as <c>"filler"</c>.
		/// </value>
		/// <remarks>
		/// This is a serialization-only backing property: unlike <see cref="Filler"/> it does not instantiate the options object, so unused options are omitted from the Chart.js configuration. It is hidden from the property grid and IntelliSense and is not meant to be used in application code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// var json = System.Text.Json.JsonSerializer.Serialize(plugins.FillerForSerialization);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("filler")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public FillerOptions? FillerForSerialization => _filler;

		/// <summary>
		/// Returns or sets the configuration of the chartjs-plugin-datalabels plugin that draws labels on the data elements (Chart.js <c>options.plugins.datalabels</c>).
		/// </summary>
		/// <value>
		/// A <see cref="DataLabelsOptions"/> instance. The getter lazily creates a default instance on first access, so it never returns <c>null</c>; the default is <c>null</c> (not configured).
		/// </value>
		/// <remarks>
		/// When this options object is serialized, the client registers the datalabels plugin for the chart. Labels are drawn only when <see cref="DataLabelsOptions.Display"/> is <c>true</c>.
		/// Only a configured instance is sent to the client; an instance that was never accessed or that still has all default values is omitted from the generated configuration.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.DataLabels.Display = true;
		/// plugins.DataLabels.Anchor = "end";
		/// plugins.DataLabels.Align = "top";
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Data labels configuration.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public DataLabelsOptions? DataLabels
		{
			get
			{
				if (_dataLabels == null)
					_dataLabels = new DataLabelsOptions { Chart = Chart };
				return _dataLabels;
			}
			set => SetProperty(ref _dataLabels, value);
		}

		/// <summary>
		/// Returns whether the <see cref="DataLabels"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the DataLabels options have been created and at least one of their values differs from its default; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (plugins.ShouldSerializeDataLabels())
		///     plugins.ResetDataLabels();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDataLabels() => _dataLabels != null && !_dataLabels.IsDefault;

		/// <summary>
		/// Resets the <see cref="DataLabels"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Discards the current <see cref="DataLabelsOptions"/> instance; a new default instance is created on the next access to <see cref="DataLabels"/>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.ResetDataLabels();
		/// ]]></code>
		/// </example>
		public void ResetDataLabels() => SetProperty(ref _dataLabels, null);

		/// <summary>
		/// Returns the data labels options instance used for JSON serialization, without lazily creating it.
		/// </summary>
		/// <value>
		/// The current <see cref="DataLabelsOptions"/> instance, or <c>null</c> when it has never been created. Serialized as <c>"datalabels"</c>.
		/// </value>
		/// <remarks>
		/// This is a serialization-only backing property: unlike <see cref="DataLabels"/> it does not instantiate the options object, so unused options are omitted from the Chart.js configuration. It is hidden from the property grid and IntelliSense and is not meant to be used in application code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// var json = System.Text.Json.JsonSerializer.Serialize(plugins.DataLabelsForSerialization);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("datalabels")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DataLabelsOptions? DataLabelsForSerialization => _dataLabels;

		/// <summary>
		/// Returns or sets a dictionary of additional Chart.js options that are not exposed as typed properties of the plugins options (for example options of third-party Chart.js plugins).
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
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.ExtensionData = new System.Collections.Generic.Dictionary<string, object>
		/// {
		///     ["colors"] = new { forceOverride = true }
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
		/// Sets raw JSON that is merged into the plugins options through <see cref="ExtensionData"/>.
		/// </summary>
		/// <value>
		/// A JSON object string, e.g. <c>{"colors":{"enabled":false}}</c>. This property is write-only.
		/// </value>
		/// <remarks>
		/// Each top-level property of the JSON object is parsed and stored in <see cref="ExtensionData"/> using the property name as the key, replacing an existing entry with the same name. Entries already present in <see cref="ExtensionData"/> with different keys are preserved.
		/// Invalid JSON and JSON values that are not objects are ignored. Assigning <c>null</c> or a blank string has no effect. When a non-blank value is assigned, the chart is refreshed.
		/// This property is never serialized itself and is hidden from the property grid and IntelliSense.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var plugins = chart.ChartOptions.Plugins;
		/// plugins.CustomOptions = "{\"colors\": {\"enabled\": false}}";
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string? CustomOptions
		{
			set
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					try
					{
						var doc = System.Text.Json.JsonDocument.Parse(value);
						if (doc.RootElement.ValueKind == System.Text.Json.JsonValueKind.Object)
						{
							ExtensionData ??= new System.Collections.Generic.Dictionary<string, object>();
							foreach (var prop in doc.RootElement.EnumerateObject())
								ExtensionData[prop.Name] = prop.Value.Clone();
						}
					}
					catch { /* ignore invalid JSON */ }

					Update();
				}
			}
		}

		/// <inheritdoc/>
		protected override void OnChartChanged()
		{
			if (_legend != null)
				_legend.Chart = Chart;
			if (_title != null)
				_title.Chart = Chart;
			if (_tooltip != null)
				_tooltip.Chart = Chart;
			if (_subtitle != null)
				_subtitle.Chart = Chart;
			if (_decimation != null)
				_decimation.Chart = Chart;
			if (_filler != null)
				_filler.Chart = Chart;
			if (_dataLabels != null)
				_dataLabels.Chart = Chart;
		}
	}
}
