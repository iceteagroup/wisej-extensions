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
	/// Font options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class FontOptions : OptionsBase
	{
		private string? _family;
		private int _size = 12;
		private string? _style;
		private object? _weight;
		private object? _lineHeight;

		/// <summary>
		/// Font family.
		/// </summary>
		[JsonPropertyName("family")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Font family.")]
		public string? Family
		{
			get => _family;
			set => SetProperty(ref _family, value);
		}

		/// <summary>
		/// Font size in pixels.
		/// </summary>
		[JsonPropertyName("size")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(12)]
		[Description("Font size.")]
		public int Size
		{
			get => _size;
			set => SetProperty(ref _size, value);
		}

		/// <summary>
		/// Font style: 'normal', 'italic', 'oblique', 'initial', 'inherit'.
		/// </summary>
		[JsonPropertyName("style")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Font style.")]
		public string? Style
		{
			get => _style;
			set => SetProperty(ref _style, value);
		}

		/// <summary>
		/// Font weight: 'normal', 'bold', 'lighter', 'bolder', or numeric value.
		/// </summary>
		[JsonPropertyName("weight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Font weight.")]
		public object? Weight
		{
			get => _weight;
			set => SetProperty(ref _weight, value);
		}

		/// <summary>
		/// Line height.
		/// </summary>
		[JsonPropertyName("lineHeight")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Line height.")]
		public object? LineHeight
		{
			get => _lineHeight;
			set => SetProperty(ref _lineHeight, value);
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
		/// Determines whether the Family property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeFamily() => Family != null;

		/// <summary>
		/// Resets the Family property to its default value.
		/// </summary>
		public void ResetFamily() => Family = null;

		/// <summary>
		/// Determines whether the Size property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeSize() => Size != 12;

		/// <summary>
		/// Resets the Size property to its default value.
		/// </summary>
		public void ResetSize() => Size = 12;

		/// <summary>
		/// Determines whether the Style property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeStyle() => Style != null;

		/// <summary>
		/// Resets the Style property to its default value.
		/// </summary>
		public void ResetStyle() => Style = null;

		/// <summary>
		/// Determines whether the Weight property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeWeight() => Weight != null;

		/// <summary>
		/// Resets the Weight property to its default value.
		/// </summary>
		public void ResetWeight() => Weight = null;

		/// <summary>
		/// Determines whether the LineHeight property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeLineHeight() => LineHeight != null;

		/// <summary>
		/// Resets the LineHeight property to its default value.
		/// </summary>
		public void ResetLineHeight() => LineHeight = null;

	}
}
