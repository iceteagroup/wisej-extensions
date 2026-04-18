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
	/// Plugins options.
	/// </summary>
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

		public PluginsOptions()
		{
			// Nested options are NOT instantiated by default
			// They will be lazy-loaded on first access
		}

		/// <summary>
		/// Legend configuration.
		/// </summary>
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
		/// Determines whether the Legend property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeLegend() => _legend != null && !_legend.IsDefault;

		/// <summary>
		/// Resets the Legend property to its default value.
		/// </summary>
		public void ResetLegend() => SetProperty(ref _legend, null);

		[JsonPropertyName("legend")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public LegendOptions? LegendForSerialization => _legend;

		/// <summary>
		/// Title configuration.
		/// </summary>
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
		/// Determines whether the Title property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTitle() => _title != null && !_title.IsDefault;

		/// <summary>
		/// Resets the Title property to its default value.
		/// </summary>
		public void ResetTitle() => SetProperty(ref _title, null);

		[JsonPropertyName("title")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public TitleOptions? TitleForSerialization => _title;

		/// <summary>
		/// Tooltip configuration.
		/// </summary>
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
		/// Determines whether the Tooltip property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTooltip() => _tooltip != null && !_tooltip.IsDefault;

		/// <summary>
		/// Resets the Tooltip property to its default value.
		/// </summary>
		public void ResetTooltip() => SetProperty(ref _tooltip, null);

		[JsonPropertyName("tooltip")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public TooltipOptions? TooltipForSerialization => _tooltip;

		/// <summary>
		/// Subtitle configuration.
		/// </summary>
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
		/// Determines whether the Subtitle property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeSubtitle() => _subtitle != null && !_subtitle.IsDefault;

		/// <summary>
		/// Resets the Subtitle property to its default value.
		/// </summary>
		public void ResetSubtitle() => SetProperty(ref _subtitle, null);

		[JsonPropertyName("subtitle")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public SubtitleOptions? SubtitleForSerialization => _subtitle;

		/// <summary>
		/// Decimation configuration for downsampling large datasets.
		/// </summary>
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
		/// Determines whether the Decimation property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeDecimation() => _decimation != null && !_decimation.IsDefault;

		/// <summary>
		/// Resets the Decimation property to its default value.
		/// </summary>
		public void ResetDecimation() => SetProperty(ref _decimation, null);

		[JsonPropertyName("decimation")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DecimationOptions? DecimationForSerialization => _decimation;

		/// <summary>
		/// Filler configuration for area charts.
		/// </summary>
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
		/// Determines whether the Filler property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeFiller() => _filler != null && !_filler.IsDefault;

		/// <summary>
		/// Resets the Filler property to its default value.
		/// </summary>
		public void ResetFiller() => SetProperty(ref _filler, null);

		[JsonPropertyName("filler")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public FillerOptions? FillerForSerialization => _filler;

		/// <summary>
		/// Data labels plugin configuration.
		/// </summary>
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
		/// Determines whether the DataLabels property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeDataLabels() => _dataLabels != null && !_dataLabels.IsDefault;

		/// <summary>
		/// Resets the DataLabels property to its default value.
		/// </summary>
		public void ResetDataLabels() => SetProperty(ref _dataLabels, null);

		[JsonPropertyName("datalabels")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DataLabelsOptions? DataLabelsForSerialization => _dataLabels;

		/// <summary>
		/// Additional custom properties that can be serialized to JSON.
		/// </summary>
		[JsonExtensionData]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <summary>
		/// Sets raw JSON to merge into the plugins options (ExtensionData).
		/// Each top-level JSON key becomes an entry in <see cref="ExtensionData"/>.
		/// </summary>
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
