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
	/// Legend options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[Browsable(true)]
	[TypeConverter(typeof(Converter))]
	public class LegendOptions : OptionsBase
	{
		public LegendOptions()
		{
			Labels = new LegendLabelsOptions();
		}

		/// <summary>
		/// Is the legend shown?
		/// </summary>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Is the legend shown?")]
		public bool Display { get; set; } = true;

		/// <summary>
		/// Position of the legend: 'top', 'left', 'bottom', 'right', 'chartArea'.
		/// </summary>
		[JsonPropertyName("position")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Position of the legend.")]
		public string? Position { get; set; }

		/// <summary>
		/// Alignment of the legend: 'start', 'center', 'end'.
		/// </summary>
		[JsonPropertyName("align")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Alignment of the legend.")]
		public string? Align { get; set; }

		/// <summary>
		/// Maximum height of the legend, in pixels.
		/// </summary>
		[JsonPropertyName("maxHeight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Maximum height in pixels.")]
		public int MaxHeight { get; set; }

		/// <summary>
		/// Maximum width of the legend, in pixels.
		/// </summary>
		[JsonPropertyName("maxWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Maximum width in pixels.")]
		public int MaxWidth { get; set; }

		/// <summary>
		/// Marks that this box should take the full width/height of the canvas.
		/// </summary>
		[JsonPropertyName("fullSize")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Take full width/height of canvas.")]
		public bool FullSize { get; set; } = true;

		/// <summary>
		/// Legend will show datasets in reverse order.
		/// </summary>
		[JsonPropertyName("reverse")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Show datasets in reverse order.")]
		public bool Reverse { get; set; }

		/// <summary>
		/// Legend title configuration.
		/// </summary>
		[JsonPropertyName("title")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Legend title configuration.")]
		public LegendTitleOptions? Title { get => _title ??= new LegendTitleOptions(); set => _title = value; }
		private LegendTitleOptions? _title;

		/// <summary>
		/// Legend labels configuration.
		/// </summary>
		[JsonPropertyName("labels")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Legend labels configuration.")]
		public LegendLabelsOptions? Labels { get => _labels ??= new LegendLabelsOptions(); set => _labels = value; }
		public LegendLabelsOptions? _labels;

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
		/// Determines whether the Align property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeAlign() => Align != null;

		/// <summary>
		/// Resets the Align property to its default value.
		/// </summary>
		public void ResetAlign() => Align = null;

		/// <summary>
		/// Determines whether the MaxHeight property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeMaxHeight() => MaxHeight != default;

		/// <summary>
		/// Resets the MaxHeight property to its default value.
		/// </summary>
		public void ResetMaxHeight() => MaxHeight = default;

		/// <summary>
		/// Determines whether the MaxWidth property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeMaxWidth() => MaxWidth != default;

		/// <summary>
		/// Resets the MaxWidth property to its default value.
		/// </summary>
		public void ResetMaxWidth() => MaxWidth = default;

		/// <summary>
		/// Determines whether the FullSize property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeFullSize() => FullSize != true;

		/// <summary>
		/// Resets the FullSize property to its default value.
		/// </summary>
		public void ResetFullSize() => FullSize = true;

		/// <summary>
		/// Determines whether the Reverse property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeReverse() => Reverse != false;

		/// <summary>
		/// Resets the Reverse property to its default value.
		/// </summary>
		public void ResetReverse() => Reverse = false;

		/// <summary>
		/// Determines whether the Labels property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeLabels() => Labels != null;

		/// <summary>
		/// Resets the Labels property to its default value.
		/// </summary>
		public void ResetLabels() => Labels = null;

		/// <summary>
		/// Determines whether the Title property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTitle() => Title != null;

		/// <summary>
		/// Resets the Title property to its default value.
		/// </summary>
		public void ResetTitle() => Title = null;

	}

	/// <summary>
	/// Legend title options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	public class LegendTitleOptions : OptionsBase
	{
		/// <summary>
		/// Is the legend title displayed?
		/// </summary>
		[JsonPropertyName("display")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Is the legend title displayed?")]
		public bool Display { get; set; }

		/// <summary>
		/// The legend title text.
		/// </summary>
		[JsonPropertyName("text")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("The legend title text.")]
		public string? Text { get; set; }

		/// <summary>
		/// Position of the title: 'start', 'center', 'end'.
		/// </summary>
		[JsonPropertyName("position")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Position of the legend title: 'start', 'center', 'end'.")]
		public string? Position { get; set; }

		/// <summary>
		/// Color of the legend title text.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Color of the legend title text.")]
		public object? Color { get; set; }

		/// <summary>
		/// Padding around the title.
		/// </summary>
		[JsonPropertyName("padding")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Padding around the legend title.")]
		public int Padding { get; set; }
	}
}
