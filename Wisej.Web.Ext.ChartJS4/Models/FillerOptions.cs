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
	/// Filler plugin options for area charts.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class FillerOptions : OptionsBase
	{
		private bool _propagate = true;
		private string? _drawTime;

		/// <summary>
		/// If true, the filler plugin is enabled.
		/// </summary>
		[JsonPropertyName("propagate")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Propagate fill to visible datasets.")]
		public bool Propagate
		{
			get => _propagate;
			set => SetProperty(ref _propagate, value);
		}

		/// <summary>
		/// Draw time: 'beforeDatasetsDraw' or 'beforeDatasetDraw'.
		/// </summary>
		[JsonPropertyName("drawTime")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("When to draw the fill.")]
		public string? DrawTime
		{
			get => _drawTime;
			set => SetProperty(ref _drawTime, value);
		}

		/// <summary>
		/// Additional custom properties that can be serialized to JSON.
		/// </summary>
		[JsonExtensionData]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <summary>
		/// Determines whether the Propagate property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializePropagate() => Propagate != true;

		/// <summary>
		/// Resets the Propagate property to its default value.
		/// </summary>
		public void ResetPropagate() => Propagate = true;

		/// <summary>
		/// Determines whether the DrawTime property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeDrawTime() => DrawTime != null;

		/// <summary>
		/// Resets the DrawTime property to its default value.
		/// </summary>
		public void ResetDrawTime() => DrawTime = null;

	}
}
