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
	/// Padding options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class PaddingOptions : OptionsBase
	{
		private int _top;
		private int _right;
		private int _bottom;
		private int _left;

		/// <summary>
		/// Top padding.
		/// </summary>
		[JsonPropertyName("top")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Top padding.")]
		public int Top
		{
			get => _top;
			set => SetProperty(ref _top, value);
		}

		/// <summary>
		/// Right padding.
		/// </summary>
		[JsonPropertyName("right")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Right padding.")]
		public int Right
		{
			get => _right;
			set => SetProperty(ref _right, value);
		}

		/// <summary>
		/// Bottom padding.
		/// </summary>
		[JsonPropertyName("bottom")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Bottom padding.")]
		public int Bottom
		{
			get => _bottom;
			set => SetProperty(ref _bottom, value);
		}

		/// <summary>
		/// Left padding.
		/// </summary>
		[JsonPropertyName("left")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Left padding.")]
		public int Left
		{
			get => _left;
			set => SetProperty(ref _left, value);
		}

		/// <summary>
		/// Determines whether the Top property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTop() => Top != default;

		/// <summary>
		/// Resets the Top property to its default value.
		/// </summary>
		public void ResetTop() => Top = default;

		/// <summary>
		/// Determines whether the Right property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeRight() => Right != default;

		/// <summary>
		/// Resets the Right property to its default value.
		/// </summary>
		public void ResetRight() => Right = default;

		/// <summary>
		/// Determines whether the Bottom property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBottom() => Bottom != default;

		/// <summary>
		/// Resets the Bottom property to its default value.
		/// </summary>
		public void ResetBottom() => Bottom = default;

		/// <summary>
		/// Determines whether the Left property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeLeft() => Left != default;

		/// <summary>
		/// Resets the Left property to its default value.
		/// </summary>
		public void ResetLeft() => Left = default;

	}
}
