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
	/// Scales options.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class ScalesOptions : OptionsBase
	{
		private AxisOptions? _x;
		private AxisOptions? _y;

		/// <summary>
		/// X axis configuration.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("X axis configuration.")]
		public AxisOptions? X
		{
			get
			{
				if (_x == null)
					_x = new AxisOptions { Chart = Chart };
				return _x;
			}
			set => SetProperty(ref _x, value);
		}

		/// <summary>
		/// Determines whether the X property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeX() => _x != null && !_x.IsDefault;

		/// <summary>
		/// Resets the X property to its default value.
		/// </summary>
		public void ResetX() => SetProperty(ref _x, null);

		[JsonPropertyName("x")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public AxisOptions? XForSerialization => _x;

		/// <summary>
		/// Y axis configuration.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Y axis configuration.")]
		public AxisOptions? Y
		{
			get
			{
				if (_y == null)
					_y = new AxisOptions { Chart = Chart };
				return _y;
			}
			set => SetProperty(ref _y, value);
		}

		/// <summary>
		/// Determines whether the Y property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeY() => _y != null && !_y.IsDefault;

		/// <summary>
		/// Resets the Y property to its default value.
		/// </summary>
		public void ResetY() => SetProperty(ref _y, null);

		[JsonPropertyName("y")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public AxisOptions? YForSerialization => _y;

		/// <summary>
		/// Additional custom properties that can be serialized to JSON.
		/// This allows for custom axis configurations.
		/// </summary>
		[JsonExtensionData]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <inheritdoc/>
		protected override void OnChartChanged()
		{
			if (_x != null)
				_x.Chart = Chart;
			if (_y != null)
				_y.Chart = Chart;
		}
	}
}
