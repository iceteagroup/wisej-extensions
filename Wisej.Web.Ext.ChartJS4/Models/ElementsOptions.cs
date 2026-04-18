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
using Wisej.Core;

namespace Wisej.Web.Ext.ChartJS4.Models
{
	/// <summary>
	/// Elements options for default styling.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class ElementsOptions : OptionsBase
	{
		private PointElementOptions? _point;
		private LineElementOptions? _line;
		private BarElementOptions? _bar;
		private ArcElementOptions? _arc;

		/// <summary>
		/// Point element options.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Point element options.")]
		public PointElementOptions? Point
		{
			get
			{
				if (_point == null)
					_point = new PointElementOptions { Chart = Chart };
				return _point;
			}
			set => SetProperty(ref _point, value);
		}

		/// <summary>
		/// Determines whether the Point property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializePoint() => _point != null && !_point.IsDefault;

		/// <summary>
		/// Resets the Point property to its default value.
		/// </summary>
		public void ResetPoint() => SetProperty(ref _point, null);

		[JsonPropertyName("point")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public PointElementOptions? PointForSerialization => _point;

		/// <summary>
		/// Line element options.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Line element options.")]
		public LineElementOptions? Line
		{
			get
			{
				if (_line == null)
					_line = new LineElementOptions { Chart = Chart };
				return _line;
			}
			set => SetProperty(ref _line, value);
		}

		/// <summary>
		/// Determines whether the Line property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeLine() => _line != null && !_line.IsDefault;

		/// <summary>
		/// Resets the Line property to its default value.
		/// </summary>
		public void ResetLine() => SetProperty(ref _line, null);

		[JsonPropertyName("line")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public LineElementOptions? LineForSerialization => _line;

		/// <summary>
		/// Bar element options.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Bar element options.")]
		public BarElementOptions? Bar
		{
			get
			{
				if (_bar == null)
					_bar = new BarElementOptions { Chart = Chart };
				return _bar;
			}
			set => SetProperty(ref _bar, value);
		}

		/// <summary>
		/// Determines whether the Bar property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeBar() => _bar != null && !_bar.IsDefault;

		/// <summary>
		/// Resets the Bar property to its default value.
		/// </summary>
		public void ResetBar() => SetProperty(ref _bar, null);

		[JsonPropertyName("bar")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public BarElementOptions? BarForSerialization => _bar;

		/// <summary>
		/// Arc element options.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Arc element options.")]
		public ArcElementOptions? Arc
		{
			get
			{
				if (_arc == null)
					_arc = new ArcElementOptions { Chart = Chart };
				return _arc;
			}
			set => SetProperty(ref _arc, value);
		}

		/// <summary>
		/// Determines whether the Arc property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeArc() => _arc != null && !_arc.IsDefault;

		/// <summary>
		/// Resets the Arc property to its default value.
		/// </summary>
		public void ResetArc() => SetProperty(ref _arc, null);

		[JsonPropertyName("arc")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ArcElementOptions? ArcForSerialization => _arc;

		/// <summary>
		/// Additional custom properties that can be serialized to JSON.
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
			if (_point != null)
				_point.Chart = Chart;
			if (_line != null)
				_line.Chart = Chart;
			if (_bar != null)
				_bar.Chart = Chart;
			if (_arc != null)
				_arc.Chart = Chart;
		}
	}
}
