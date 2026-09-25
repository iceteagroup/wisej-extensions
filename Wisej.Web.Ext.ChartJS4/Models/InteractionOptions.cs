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
	/// Represents the Chart.js interaction options (<c>options.interaction</c>) that control how hover and tooltip interactions select chart elements.
	/// </summary>
	/// <remarks>
	/// An instance is available through <see cref="ChartOptions.Interaction"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// var interaction = chart.ChartOptions.Interaction;
	/// interaction.Mode = "index";
	/// interaction.Intersect = false;
	/// interaction.Axis = "x";
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class InteractionOptions : OptionsBase
	{
		private string? _mode = null;
		private bool _intersect = true;
		private string? _axis;
		private bool _includeInvisible;

		/// <summary>
		/// Returns or sets which elements appear in the interaction (Chart.js option <c>mode</c>).
		/// </summary>
		/// <value>
		/// One of <c>"point"</c>, <c>"nearest"</c>, <c>"index"</c>, <c>"dataset"</c>, <c>"x"</c> or <c>"y"</c>, or <c>null</c> (default) to use the Chart.js default (<c>"nearest"</c>).
		/// </value>
		/// <remarks>
		/// <c>"index"</c> selects the items at the same index in all datasets; <c>"dataset"</c> selects all items of the same dataset; <c>"point"</c> selects all items that intersect the point; <c>"nearest"</c> selects the nearest items; <c>"x"</c> and <c>"y"</c> select the items that intersect along the respective axis.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var interaction = chart.ChartOptions.Interaction;
		/// interaction.Mode = "index";
		/// interaction.Intersect = false;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("mode")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Sets which elements appear in the interaction.")]
		[DefaultValue(null)]
		public string? Mode
		{
			get => _mode;
			set => SetProperty(ref _mode, value);
		}

		/// <summary>
		/// Returns or sets whether the interaction mode applies only when the mouse position intersects an item on the chart (Chart.js option <c>intersect</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to require intersection; otherwise <c>false</c>. The default is <c>true</c>.
		/// </value>
		/// <remarks>
		/// This value is always serialized.
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var interaction = chart.ChartOptions.Interaction;
		/// interaction.Intersect = false;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("intersect")]
		[JsonIgnore(Condition = JsonIgnoreCondition.Never)]
		[DefaultValue(true)]
		[Description("If true, the interaction mode only applies when the mouse position intersects an item on the chart.")]
		public bool Intersect
		{
			get => _intersect;
			set => SetProperty(ref _intersect, value);
		}

		/// <summary>
		/// Returns or sets which directions are used when calculating distances between the pointer and the elements (Chart.js option <c>axis</c>).
		/// </summary>
		/// <value>
		/// <c>"x"</c>, <c>"y"</c>, <c>"xy"</c> or <c>"r"</c>, or <c>null</c> (default) to use the Chart.js default (<c>"x"</c> for <c>"index"</c> mode, <c>"xy"</c> otherwise).
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var interaction = chart.ChartOptions.Interaction;
		/// interaction.Mode = "nearest";
		/// interaction.Axis = "x";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("axis")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Axis for interaction calculations.")]
		public string? Axis
		{
			get => _axis;
			set => SetProperty(ref _axis, value);
		}

		/// <summary>
		/// Returns or sets whether points that are outside the chart area are also included when evaluating interactions (Chart.js option <c>includeInvisible</c>).
		/// </summary>
		/// <value>
		/// <c>true</c> to include invisible points; otherwise <c>false</c>. The default is <c>false</c>.
		/// </value>
		/// <remarks>
		/// Changing this property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var interaction = chart.ChartOptions.Interaction;
		/// interaction.IncludeInvisible = true;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("includeInvisible")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Include invisible points in interactions.")]
		public bool IncludeInvisible
		{
			get => _includeInvisible;
			set => SetProperty(ref _includeInvisible, value);
		}

		/// <summary>
		/// Returns or sets a dictionary of additional Chart.js options that are not exposed as typed properties of the interaction options.
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
		/// var interaction = chart.ChartOptions.Interaction;
		/// interaction.ExtensionData = new System.Collections.Generic.Dictionary<string, object>
		/// {
		///     ["includeInvisible"] = true
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
		/// Returns whether the <see cref="Mode"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Mode"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (interaction.ShouldSerializeMode())
		///     interaction.ResetMode();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeMode() => Mode != null;

		/// <summary>
		/// Resets the <see cref="Mode"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Mode"/> to <c>null</c> (Chart.js default).
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var interaction = chart.ChartOptions.Interaction;
		/// interaction.ResetMode();
		/// ]]></code>
		/// </example>
		public void ResetMode() => Mode = null;

		/// <summary>
		/// Returns whether the <see cref="Intersect"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Intersect"/> is <c>false</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (interaction.ShouldSerializeIntersect())
		///     interaction.ResetIntersect();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeIntersect() => Intersect != true;

		/// <summary>
		/// Resets the <see cref="Intersect"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Intersect"/> to <c>true</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var interaction = chart.ChartOptions.Interaction;
		/// interaction.ResetIntersect();
		/// ]]></code>
		/// </example>
		public void ResetIntersect() => Intersect = true;

		/// <summary>
		/// Returns whether the <see cref="Axis"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Axis"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (interaction.ShouldSerializeAxis())
		///     interaction.ResetAxis();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeAxis() => Axis != null;

		/// <summary>
		/// Resets the <see cref="Axis"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="Axis"/> to <c>null</c> (Chart.js default).
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var interaction = chart.ChartOptions.Interaction;
		/// interaction.ResetAxis();
		/// ]]></code>
		/// </example>
		public void ResetAxis() => Axis = null;

		/// <summary>
		/// Returns whether the <see cref="IncludeInvisible"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="IncludeInvisible"/> is <c>true</c>; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (interaction.ShouldSerializeIncludeInvisible())
		///     interaction.ResetIncludeInvisible();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeIncludeInvisible() => IncludeInvisible != false;

		/// <summary>
		/// Resets the <see cref="IncludeInvisible"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="IncludeInvisible"/> to <c>false</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var interaction = chart.ChartOptions.Interaction;
		/// interaction.ResetIncludeInvisible();
		/// ]]></code>
		/// </example>
		public void ResetIncludeInvisible() => IncludeInvisible = false;

		/// <summary>
		/// Returns whether the <see cref="ExtensionData"/> property has been changed from its default value and should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="ExtensionData"/> contains at least one entry; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// This method is used by the Visual Studio designer and the property grid to determine whether the property value is persisted in the generated code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (interaction.ShouldSerializeExtensionData())
		///     interaction.ResetExtensionData();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeExtensionData() => ExtensionData != null && ExtensionData.Count > 0;

		/// <summary>
		/// Resets the <see cref="ExtensionData"/> property to its default value.
		/// </summary>
		/// <remarks>
		/// Sets <see cref="ExtensionData"/> to <c>null</c>.
		/// This method is used by the Visual Studio designer and the property grid ("Reset" command).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// var interaction = chart.ChartOptions.Interaction;
		/// interaction.ResetExtensionData();
		/// ]]></code>
		/// </example>
		public void ResetExtensionData() => ExtensionData = null;
	}
}
