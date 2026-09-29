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
	/// Represents the default styling options of the point elements used by line, radar, scatter and bubble charts (Chart.js <c>options.elements.point</c>).
	/// </summary>
	/// <remarks>
	/// An instance is available through <see cref="ElementsOptions.Point"/>. These values apply to all datasets that draw points unless overridden by the dataset options.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var point = new PointElementOptions();
	/// point.Radius = 5;
	/// point.PointStyle = "rectRot";
	/// chart.ChartOptions.Elements = new ElementsOptions { Point = point };
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class PointElementOptions : OptionsBase
	{
		private int _radius = 3;
		private string? _pointStyle;
		private double? _rotation;
		private object? _backgroundColor;
		private int _borderWidth = 1;

		/// <summary>
		/// Returns or sets the default radius, in pixels, of the points (Chart.js option <c>radius</c>).
		/// </summary>
		/// <value>
		/// The radius in pixels. The default is <c>3</c>.
		/// </value>
		/// <remarks>
		/// Set it to <c>0</c> to hide the points.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var point = new PointElementOptions();
		/// point.Radius = 5;
		/// chart.ChartOptions.Elements = new ElementsOptions { Point = point };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("radius")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(3)]
		[Description("Point radius.")]
		public int Radius
		{
			get => _radius;
			set => SetProperty(ref _radius, value);
		}

		/// <summary>
		/// Returns or sets the default shape of the points (Chart.js option <c>pointStyle</c>).
		/// </summary>
		/// <value>
		/// One of <c>"circle"</c>, <c>"cross"</c>, <c>"crossRot"</c>, <c>"dash"</c>, <c>"line"</c>, <c>"rect"</c>, <c>"rectRounded"</c>, <c>"rectRot"</c>, <c>"star"</c> or <c>"triangle"</c>, or <c>null</c> (default) to use the Chart.js default (<c>"circle"</c>).
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var point = new PointElementOptions();
		/// point.PointStyle = "triangle";
		/// chart.ChartOptions.Elements = new ElementsOptions { Point = point };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("pointStyle")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Point style.")]
		public string? PointStyle
		{
			get => _pointStyle;
			set => SetProperty(ref _pointStyle, value);
		}

		/// <summary>
		/// Returns or sets the default rotation, in degrees, of the point shapes (Chart.js option <c>rotation</c>).
		/// </summary>
		/// <value>
		/// The rotation in degrees, or <c>null</c> (default) to use the Chart.js default (<c>0</c>).
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var point = new PointElementOptions();
		/// point.PointStyle = "rect";
		/// point.Rotation = 45;
		/// chart.ChartOptions.Elements = new ElementsOptions { Point = point };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("rotation")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Point rotation in degrees.")]
		public double? Rotation
		{
			get => _rotation;
			set => SetProperty(ref _rotation, value);
		}

		/// <summary>
		/// Returns or sets the default fill color of the points (Chart.js option <c>backgroundColor</c>).
		/// </summary>
		/// <value>
		/// A <see cref="System.Drawing.Color"/>, a CSS color string, or an array of colors. The default is <c>null</c>, which uses the Chart.js default color.
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var point = new PointElementOptions();
		/// point.BackgroundColor = System.Drawing.Color.Orange;
		/// chart.ChartOptions.Elements = new ElementsOptions { Point = point };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("backgroundColor")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Point background color.")]
		public object? BackgroundColor
		{
			get => _backgroundColor;
			set => SetProperty(ref _backgroundColor, value);
		}

		/// <summary>
		/// Returns or sets the default border width, in pixels, of the points (Chart.js option <c>borderWidth</c>).
		/// </summary>
		/// <value>
		/// The border width in pixels. The default is <c>1</c>.
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var point = new PointElementOptions();
		/// point.BorderWidth = 2;
		/// chart.ChartOptions.Elements = new ElementsOptions { Point = point };
		/// ]]></code>
		/// </example>
		[JsonPropertyName("borderWidth")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(1)]
		[Description("Point border width.")]
		public int BorderWidth
		{
			get => _borderWidth;
			set => SetProperty(ref _borderWidth, value);
		}

		/// <summary>
		/// Returns or sets a dictionary of additional Chart.js options that are not exposed as typed properties of the point element options.
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
		/// var point = new PointElementOptions();
		/// point.ExtensionData = new System.Collections.Generic.Dictionary<string, object>
		/// {
		///     ["hoverRadius"] = 6
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
		/// Returns whether the <see cref="Radius"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Radius"/> is not <c>3</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (point.ShouldSerializeRadius())
		///     point.ResetRadius();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeRadius() => Radius != 3;

		/// <summary>
		/// Resets the <see cref="Radius"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Radius"/> to <c>3</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var point = new PointElementOptions();
		/// point.ResetRadius();
		/// ]]></code>
		/// </example>
		public void ResetRadius() => Radius = 3;

		/// <summary>
		/// Returns whether the <see cref="PointStyle"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="PointStyle"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (point.ShouldSerializePointStyle())
		///     point.ResetPointStyle();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePointStyle() => PointStyle != null;

		/// <summary>
		/// Resets the <see cref="PointStyle"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="PointStyle"/> to <c>null</c> (Chart.js default).
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var point = new PointElementOptions();
		/// point.ResetPointStyle();
		/// ]]></code>
		/// </example>
		public void ResetPointStyle() => PointStyle = null;

		/// <summary>
		/// Returns whether the <see cref="Rotation"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Rotation"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (point.ShouldSerializeRotation())
		///     point.ResetRotation();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeRotation() => Rotation != null;

		/// <summary>
		/// Resets the <see cref="Rotation"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Rotation"/> to <c>null</c> (Chart.js default).
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var point = new PointElementOptions();
		/// point.ResetRotation();
		/// ]]></code>
		/// </example>
		public void ResetRotation() => Rotation = null;

		/// <summary>
		/// Returns whether the <see cref="BackgroundColor"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BackgroundColor"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (point.ShouldSerializeBackgroundColor())
		///     point.ResetBackgroundColor();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBackgroundColor() => BackgroundColor != null;

		/// <summary>
		/// Resets the <see cref="BackgroundColor"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="BackgroundColor"/> to <c>null</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var point = new PointElementOptions();
		/// point.ResetBackgroundColor();
		/// ]]></code>
		/// </example>
		public void ResetBackgroundColor() => BackgroundColor = null;

		/// <summary>
		/// Returns whether the <see cref="BorderWidth"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="BorderWidth"/> is not <c>1</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (point.ShouldSerializeBorderWidth())
		///     point.ResetBorderWidth();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeBorderWidth() => BorderWidth != 1;

		/// <summary>
		/// Resets the <see cref="BorderWidth"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="BorderWidth"/> to <c>1</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var point = new PointElementOptions();
		/// point.ResetBorderWidth();
		/// ]]></code>
		/// </example>
		public void ResetBorderWidth() => BorderWidth = 1;

	}
}
