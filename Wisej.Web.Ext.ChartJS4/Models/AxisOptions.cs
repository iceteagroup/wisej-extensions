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
		public AxisOptions()
		{
			Grid = new GridOptions();
			Border = new BorderOptions();
			Title = new AxisTitleOptions();
			Ticks = new TickOptions();
		}

		/// <summary>
		/// Type of scale being employed: 'linear', 'logarithmic', 'category', 'time', 'timeseries', 'radialLinear'.
		/// </summary>
		[JsonPropertyName("type")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Type of scale being employed.")]
		public string? Type { get; set; }

		/// <summary>
		/// Display the axis?
		/// </summary>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Display the axis?")]
		public bool Display { get; set; } = true;

		/// <summary>
		/// Position of the axis: 'top', 'left', 'bottom', 'right'.
		/// </summary>
		[JsonPropertyName("position")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Position of the axis.")]
		public object? Position { get; set; }

		/// <summary>
		/// Stack group for this axis. Axes with the same stack are stacked together.
		/// </summary>
		[JsonPropertyName("stack")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Stack group identifier.")]
		public string? Stack { get; set; }

		/// <summary>
		/// Weight used to sort the axis. Higher weights are further away from the chart area.
		/// </summary>
		[JsonPropertyName("weight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Axis weight for sorting.")]
		public int Weight { get; set; }
		
		/// <summary>
		/// Grid line configuration.
		/// </summary>
		[JsonPropertyName("grid")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Grid line configuration.")]
		public GridOptions? Grid
		{
			get => _grid ??= new GridOptions();
			set => _grid = value;
		}
		private GridOptions? _grid;

		/// <summary>
		/// Border configuration.
		/// </summary>
		[JsonPropertyName("border")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Border configuration.")]
		public BorderOptions? Border
		{
			get => _border ??= new BorderOptions();
			set => _border = value;
		}
		private BorderOptions? _border;

		/// <summary>
		/// Title configuration.
		/// </summary>
		[JsonPropertyName("title")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Axis title configuration.")]
		public AxisTitleOptions? Title
		{
			get => _title ??= new AxisTitleOptions();
			set => _title = value;
		}
		private AxisTitleOptions? _title;

		/// <summary>
		/// Ticks configuration.
		/// </summary>
		[JsonPropertyName("ticks")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Tick configuration.")]
		public TickOptions? Ticks
		{
			get => _ticks ??= new TickOptions();
			set => _ticks = value;
		}
		private TickOptions? _ticks;

		/// <summary>
		/// Minimum value for the scale.
		/// </summary>
		[JsonPropertyName("min")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Minimum value for the scale.")]
		public double? Min { get; set; }

		/// <summary>
		/// Maximum value for the scale.
		/// </summary>
		[JsonPropertyName("max")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Maximum value for the scale.")]
		public double? Max { get; set; }

		/// <summary>
		/// User defined minimum value for the scale, overrides minimum value from data.
		/// </summary>
		[JsonPropertyName("suggestedMin")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Suggested minimum value.")]
		public double? SuggestedMin { get; set; }

		/// <summary>
		/// User defined maximum value for the scale, overrides maximum value from data.
		/// </summary>
		[JsonPropertyName("suggestedMax")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Suggested maximum value.")]
		public double? SuggestedMax { get; set; }

		/// <summary>
		/// If true, data will be comprised between datasets of data.
		/// Also accepts <c>"single"</c> to stack only the datasets with the same stack key.
		/// </summary>
		[JsonPropertyName("stacked")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Enable stacking. Accepts bool or \"single\".")]
		public object? Stacked { get; set; }

		/// <summary>
		/// Reverse the scale.
		/// </summary>
		[JsonPropertyName("reverse")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Reverse the scale.")]
		public bool Reverse { get; set; }

		/// <summary>
		/// If true, extra space is added to the both edges and the axis is scaled to fit into the chart area.
		/// </summary>
		[JsonPropertyName("offset")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Add offset to edges.")]
		public bool Offset { get; set; }

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
		public void ResetGrid() => Grid = null;

		/// <summary>
		/// Determines whether the Border property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBorder() => Border != null;

		/// <summary>
		/// Resets the Border property to its default value.
		/// </summary>
		public void ResetBorder() => Border = null;

		/// <summary>
		/// Determines whether the Title property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTitle() => Title != null;

		/// <summary>
		/// Resets the Title property to its default value.
		/// </summary>
		public void ResetTitle() => Title = null;

		/// <summary>
		/// Determines whether the Ticks property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTicks() => Ticks != null && !Ticks.IsDefault;

		/// <summary>
		/// Resets the Ticks property to its default value.
		/// </summary>
		public void ResetTicks() => Ticks = null;

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
