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
	/// Decimation plugin options for downsampling large datasets.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class DecimationOptions : OptionsBase
	{
		/// <summary>
		/// Is decimation enabled?
		/// </summary>
		[JsonPropertyName("enabled")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(false)]
		[Description("Enable decimation.")]
		public bool Enabled { get; set; }

		/// <summary>
		/// Decimation algorithm: 'lttb', 'min-max'.
		/// </summary>
		[JsonPropertyName("algorithm")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Decimation algorithm.")]
		public string? Algorithm { get; set; }

		/// <summary>
		/// Number of samples to keep after decimation.
		/// </summary>
		[JsonPropertyName("samples")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Number of samples.")]
		[DefaultValue(0)]
		public int Samples { get; set; }

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
		/// Determines whether the Enabled property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeEnabled() => Enabled != false;

		/// <summary>
		/// Resets the Enabled property to its default value.
		/// </summary>
		public void ResetEnabled() => Enabled = false;

		/// <summary>
		/// Determines whether the Algorithm property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeAlgorithm() => Algorithm != null;

		/// <summary>
		/// Resets the Algorithm property to its default value.
		/// </summary>
		public void ResetAlgorithm() => Algorithm = null;

		/// <summary>
		/// Determines whether the Samples property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeSamples() => Samples != 0;

		/// <summary>
		/// Resets the Samples property to its default value.
		/// </summary>
		public void ResetSamples() => Samples = 0;

	}
}
