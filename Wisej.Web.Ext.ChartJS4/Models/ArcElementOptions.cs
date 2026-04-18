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
	/// Arc element styling options (for pie, doughnut, polar area charts).
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class ArcElementOptions : OptionsBase
	{
		private object? _backgroundColor;
		private int _borderWidth = 2;
		private object? _borderColor;

		/// <summary>
		/// Arc background color.
		/// </summary>
		[JsonPropertyName("backgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Arc background color.")]
		public object? BackgroundColor
		{
			get => _backgroundColor;
			set => SetProperty(ref _backgroundColor, value);
		}

		/// <summary>
		/// Arc border width.
		/// </summary>
		[JsonPropertyName("borderWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(2)]
		[Description("Arc border width.")]
		public int BorderWidth
		{
			get => _borderWidth;
			set => SetProperty(ref _borderWidth, value);
		}

		/// <summary>
		/// Arc border color.
		/// </summary>
		[JsonPropertyName("borderColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Arc border color.")]
		public object? BorderColor
		{
			get => _borderColor;
			set => SetProperty(ref _borderColor, value);
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

		/// <summary>
		/// Determines whether the BackgroundColor property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBackgroundColor() => BackgroundColor != null;

		/// <summary>
		/// Resets the BackgroundColor property to its default value.
		/// </summary>
		public void ResetBackgroundColor() => BackgroundColor = null;

		/// <summary>
		/// Determines whether the BorderWidth property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBorderWidth() => BorderWidth != 2;

		/// <summary>
		/// Resets the BorderWidth property to its default value.
		/// </summary>
		public void ResetBorderWidth() => BorderWidth = 2;

		/// <summary>
		/// Determines whether the BorderColor property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBorderColor() => BorderColor != null;

		/// <summary>
		/// Resets the BorderColor property to its default value.
		/// </summary>
		public void ResetBorderColor() => BorderColor = null;

	}
}
