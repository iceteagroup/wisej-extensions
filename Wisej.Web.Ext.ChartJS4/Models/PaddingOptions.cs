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
	/// Represents padding values, in pixels, for each side of an element (Chart.js padding object
	/// with <c>top</c>, <c>right</c>, <c>bottom</c> and <c>left</c>).
	/// </summary>
	/// <remarks>
	/// Used by <see cref="LayoutOptions.Padding"/>. Sides left at their default (0) are not serialized.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var padding = chart.ChartOptions.Layout.Padding;
	/// padding.Top = 10;
	/// padding.Right = 20;
	/// padding.Bottom = 10;
	/// padding.Left = 20;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class PaddingOptions : OptionsBase
	{
		private int _top;
		private int _right;
		private int _bottom;
		private int _left;

		/// <summary>
		/// Returns or sets the top padding, in pixels (Chart.js <c>padding.top</c>).
		/// </summary>
		/// <value>
		/// The top padding in pixels. The default is <c>0</c>.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Layout.Padding.Top = 25;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("top")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Top padding.")]
		public int Top
		{
			get => _top;
			set => SetProperty(ref _top, value);
		}

		/// <summary>
		/// Returns or sets the right padding, in pixels (Chart.js <c>padding.right</c>).
		/// </summary>
		/// <value>
		/// The right padding in pixels. The default is <c>0</c>.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Layout.Padding.Right = 25;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("right")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Right padding.")]
		public int Right
		{
			get => _right;
			set => SetProperty(ref _right, value);
		}

		/// <summary>
		/// Returns or sets the bottom padding, in pixels (Chart.js <c>padding.bottom</c>).
		/// </summary>
		/// <value>
		/// The bottom padding in pixels. The default is <c>0</c>.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Layout.Padding.Bottom = 25;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("bottom")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Bottom padding.")]
		public int Bottom
		{
			get => _bottom;
			set => SetProperty(ref _bottom, value);
		}

		/// <summary>
		/// Returns or sets the left padding, in pixels (Chart.js <c>padding.left</c>).
		/// </summary>
		/// <value>
		/// The left padding in pixels. The default is <c>0</c>.
		/// </value>
		/// <remarks>
		/// Setting this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Layout.Padding.Left = 25;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("left")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Left padding.")]
		public int Left
		{
			get => _left;
			set => SetProperty(ref _left, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Top"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Top"/> is not <c>0</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetTop"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var padding = new PaddingOptions();
		/// if (padding.ShouldSerializeTop())
		///     padding.ResetTop();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTop() => Top != default;

		/// <summary>
		/// Resets the <see cref="Top"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var padding = new PaddingOptions { Top = 10 };
		/// padding.ResetTop();
		/// ]]></code>
		/// </example>
		public void ResetTop() => Top = default;

		/// <summary>
		/// Determines whether the <see cref="Right"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Right"/> is not <c>0</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetRight"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var padding = new PaddingOptions();
		/// if (padding.ShouldSerializeRight())
		///     padding.ResetRight();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeRight() => Right != default;

		/// <summary>
		/// Resets the <see cref="Right"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var padding = new PaddingOptions { Right = 10 };
		/// padding.ResetRight();
		/// ]]></code>
		/// </example>
		public void ResetRight() => Right = default;

		/// <summary>
		/// Determines whether the <see cref="Bottom"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Bottom"/> is not <c>0</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetBottom"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var padding = new PaddingOptions();
		/// if (padding.ShouldSerializeBottom())
		///     padding.ResetBottom();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBottom() => Bottom != default;

		/// <summary>
		/// Resets the <see cref="Bottom"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var padding = new PaddingOptions { Bottom = 10 };
		/// padding.ResetBottom();
		/// ]]></code>
		/// </example>
		public void ResetBottom() => Bottom = default;

		/// <summary>
		/// Determines whether the <see cref="Left"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Left"/> is not <c>0</c>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Used by the Visual Studio designer together with <see cref="ResetLeft"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var padding = new PaddingOptions();
		/// if (padding.ShouldSerializeLeft())
		///     padding.ResetLeft();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeLeft() => Left != default;

		/// <summary>
		/// Resets the <see cref="Left"/> property to its default value (<c>0</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var padding = new PaddingOptions { Left = 10 };
		/// padding.ResetLeft();
		/// ]]></code>
		/// </example>
		public void ResetLeft() => Left = default;

	}
}
