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
	/// Axis options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class AxisOptions : OptionsBase
	{
		private string? _type;
		private bool _display = true;
		private object? _position;
		private string? _stack;
		private int _weight;
		private GridOptions? _grid;
		private BorderOptions? _border;
		private AxisTitleOptions? _title;
		private TickOptions? _ticks;
		private double? _min;
		private double? _max;
		private double? _suggestedMin;
		private double? _suggestedMax;
		private object? _stacked;
		private bool _reverse;
		private bool _offset;

		/// <summary>
		/// Type of scale being employed: 'linear', 'logarithmic', 'category', 'time', 'timeseries', 'radialLinear'.
		/// </summary>
		[JsonPropertyName("type")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Type of scale being employed.")]
		public string? Type
		{
			get => _type;
			set => SetProperty(ref _type, value);
		}

		/// <summary>
		/// Display the axis?
		/// </summary>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Display the axis?")]
		public bool Display
		{
			get => _display;
			set => SetProperty(ref _display, value);
		}

		/// <summary>
		/// Position of the axis: 'top', 'left', 'bottom', 'right'.
		/// </summary>
		[JsonPropertyName("position")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Position of the axis.")]
		public object? Position
		{
			get => _position;
			set => SetProperty(ref _position, value);
		}

		/// <summary>
		/// Stack group for this axis. Axes with the same stack are stacked together.
		/// </summary>
		[JsonPropertyName("stack")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Stack group identifier.")]
		public string? Stack
		{
			get => _stack;
			set => SetProperty(ref _stack, value);
		}

		/// <summary>
		/// Weight used to sort the axis. Higher weights are further away from the chart area.
		/// </summary>
		[JsonPropertyName("weight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Axis weight for sorting.")]
		public int Weight
		{
			get => _weight;
			set => SetProperty(ref _weight, value);
		}

		/// <summary>
		/// Grid line configuration.
		/// </summary>
		[JsonPropertyName("grid")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Grid line configuration.")]
		public GridOptions? Grid
		{
			get
			{
				if (_grid == null)
					_grid = new GridOptions { Chart = Chart };
				return _grid;
			}
			set => SetProperty(ref _grid, value);
		}

		/// <summary>
		/// Border configuration.
		/// </summary>
		[JsonPropertyName("border")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Border configuration.")]
		public BorderOptions? Border
		{
			get
			{
				if (_border == null)
					_border = new BorderOptions { Chart = Chart };
				return _border;
			}
			set => SetProperty(ref _border, value);
		}

		/// <summary>
		/// Title configuration.
		/// </summary>
		[JsonPropertyName("title")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Axis title configuration.")]
		public AxisTitleOptions? Title
		{
			get
			{
				if (_title == null)
					_title = new AxisTitleOptions { Chart = Chart };
				return _title;
			}
			set => SetProperty(ref _title, value);
		}

		/// <summary>
		/// Ticks configuration.
		/// </summary>
		[JsonPropertyName("ticks")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Tick configuration.")]
		public TickOptions? Ticks
		{
			get
			{
				if (_ticks == null)
					_ticks = new TickOptions { Chart = Chart };
				return _ticks;
			}
			set => SetProperty(ref _ticks, value);
		}

		/// <summary>
		/// Minimum value for the scale.
		/// </summary>
		[JsonPropertyName("min")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Minimum value for the scale.")]
		public double? Min
		{
			get => _min;
			set => SetProperty(ref _min, value);
		}

		/// <summary>
		/// Maximum value for the scale.
		/// </summary>
		[JsonPropertyName("max")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Maximum value for the scale.")]
		public double? Max
		{
			get => _max;
			set => SetProperty(ref _max, value);
		}

		/// <summary>
		/// User defined minimum value for the scale, overrides minimum value from data.
		/// </summary>
		[JsonPropertyName("suggestedMin")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Suggested minimum value.")]
		public double? SuggestedMin
		{
			get => _suggestedMin;
			set => SetProperty(ref _suggestedMin, value);
		}

		/// <summary>
		/// User defined maximum value for the scale, overrides maximum value from data.
		/// </summary>
		[JsonPropertyName("suggestedMax")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Suggested maximum value.")]
		public double? SuggestedMax
		{
			get => _suggestedMax;
			set => SetProperty(ref _suggestedMax, value);
		}

		/// <summary>
		/// If true, data will be comprised between datasets of data.
		/// Also accepts <c>"single"</c> to stack only the datasets with the same stack key.
		/// </summary>
		[JsonPropertyName("stacked")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Enable stacking. Accepts bool or \"single\".")]
		public object? Stacked
		{
			get => _stacked;
			set => SetProperty(ref _stacked, value);
		}

		/// <summary>
		/// Reverse the scale.
		/// </summary>
		[JsonPropertyName("reverse")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Reverse the scale.")]
		public bool Reverse
		{
			get => _reverse;
			set => SetProperty(ref _reverse, value);
		}

		/// <summary>
		/// If true, extra space is added to the both edges and the axis is scaled to fit into the chart area.
		/// </summary>
		[JsonPropertyName("offset")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Add offset to edges.")]
		public bool Offset
		{
			get => _offset;
			set => SetProperty(ref _offset, value);
		}

		/// <summary>
		/// Additional custom properties that can be serialized to JSON.
		/// </summary>
		[JsonExtensionData]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <inheritdoc/>
		protected override void OnChartChanged()
		{
			if (_grid != null)
				_grid.Chart = Chart;
			if (_border != null)
				_border.Chart = Chart;
			if (_title != null)
				_title.Chart = Chart;
			if (_ticks != null)
				_ticks.Chart = Chart;
		}

		/// <summary>
		/// Determines whether the Type property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeType() => Type != null;

		/// <summary>
		/// Resets the Type property to its default value.
		/// </summary>
		public void ResetType() => Type = null;

		/// <summary>
		/// Determines whether the Display property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeDisplay() => Display != true;

		/// <summary>
		/// Resets the Display property to its default value.
		/// </summary>
		public void ResetDisplay() => Display = true;

		/// <summary>
		/// Determines whether the Position property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializePosition() => Position != null;

		/// <summary>
		/// Resets the Position property to its default value.
		/// </summary>
		public void ResetPosition() => Position = null;

		/// <summary>
		/// Determines whether the Stack property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeStack() => Stack != null;

		/// <summary>
		/// Resets the Stack property to its default value.
		/// </summary>
		public void ResetStack() => Stack = null;

		/// <summary>
		/// Determines whether the Weight property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeWeight() => Weight != default;

		/// <summary>
		/// Resets the Weight property to its default value.
		/// </summary>
		public void ResetWeight() => Weight = default;

		/// <summary>
		/// Determines whether the Grid property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeGrid() => Grid != null && !Grid.IsDefault;

		/// <summary>
		/// Resets the Grid property to its default value.
		/// </summary>
		public void ResetGrid() => SetProperty(ref _grid, null);

		/// <summary>
		/// Determines whether the Border property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBorder() => Border != null;

		/// <summary>
		/// Resets the Border property to its default value.
		/// </summary>
		public void ResetBorder() => SetProperty(ref _border, null);

		/// <summary>
		/// Determines whether the Title property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTitle() => Title != null;

		/// <summary>
		/// Resets the Title property to its default value.
		/// </summary>
		public void ResetTitle() => SetProperty(ref _title, null);

		/// <summary>
		/// Determines whether the Ticks property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTicks() => Ticks != null && !Ticks.IsDefault;

		/// <summary>
		/// Resets the Ticks property to its default value.
		/// </summary>
		public void ResetTicks() => SetProperty(ref _ticks, null);

		/// <summary>
		/// Determines whether the Min property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeMin() => Min != null;

		/// <summary>
		/// Resets the Min property to its default value.
		/// </summary>
		public void ResetMin() => Min = null;

		/// <summary>
		/// Determines whether the Max property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeMax() => Max != null;

		/// <summary>
		/// Resets the Max property to its default value.
		/// </summary>
		public void ResetMax() => Max = null;

		/// <summary>
		/// Determines whether the SuggestedMin property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeSuggestedMin() => SuggestedMin != null;

		/// <summary>
		/// Resets the SuggestedMin property to its default value.
		/// </summary>
		public void ResetSuggestedMin() => SuggestedMin = null;

		/// <summary>
		/// Determines whether the SuggestedMax property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeSuggestedMax() => SuggestedMax != null;

		/// <summary>
		/// Resets the SuggestedMax property to its default value.
		/// </summary>
		public void ResetSuggestedMax() => SuggestedMax = null;

		/// <summary>
		/// Determines whether the Stacked property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeStacked() => Stacked != null;

		/// <summary>
		/// Resets the Stacked property to its default value.
		/// </summary>
		public void ResetStacked() => Stacked = null;

		/// <summary>
		/// Determines whether the Reverse property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeReverse() => Reverse != false;

		/// <summary>
		/// Resets the Reverse property to its default value.
		/// </summary>
		public void ResetReverse() => Reverse = false;

		/// <summary>
		/// Determines whether the Offset property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeOffset() => Offset != false;

		/// <summary>
		/// Resets the Offset property to its default value.
		/// </summary>
		public void ResetOffset() => Offset = false;

	}
}
