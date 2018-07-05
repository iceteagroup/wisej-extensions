///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;

namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Represents the data used by the <see cref="T: Wisej.Web.Ext.ChartJS.ChartJS"/> control to plot the chart.
	/// See http://www.chartjs.org/docs/#line-chart-data-structure for additional information regarding the data structure of ChartJS.
	/// </summary>
	[TypeConverter(typeof(DataSet.Converter))]
	public class DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/>.
		/// </summary>
		public DataSet()
		{
			this.Label = "Data Set";
			this.BorderWidth = 0;
			this.BackgroundColor = Color.Empty;
			this.Type = ChartType.Line;
		}

		/// <summary>
		/// The label for the dataset which appears in the legend and tooltips.
		/// </summary>
		[DefaultValue("")]
		[Description("The label for the dataset which appears in the legend and tooltips.")]
		public string Label
		{
			get;
			set;
		}

		/// <summary>
		/// Returns the type of chart that plots this type of <see cref="T:Wisej.Web.Ext.ChartJS.DataSet"/>.
		/// </summary>
		[Description("Returns the type of chart that plots this data set.")]
		public ChartType Type
		{
			get;
			protected set;
		}

		/// <summary>
		/// The data to plot.
		/// </summary>
		[DefaultValue(null)]
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public object[] Data
		{
			get;
			set;
		}

		/// <summary>
		/// The fill color of the data set. What it fills is up to the chart type.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[Description("The fill color of the data set. What it fills is up to the chart type.")]
		public Color BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// The border color of the data set. What it fills is up to the chart type.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[Description("The border color of the data set. What it fills is up to the chart type.")]
		public Color BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// The width of the border in pixels.
		/// </summary>
		[DefaultValue(0)]
		[Description("The width of the border in pixels.")]
		public int BorderWidth
		{
			get;
			set;
		}

		// Initializes this data set copying the value from another data set.
		internal virtual void CopyFrom(DataSet source)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			Type targetType = GetType();
			Type sourceType = source.GetType();

			var sourceProperties = sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
			var targetProperties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
			foreach (PropertyInfo sourceProperty  in sourceProperties)
			{
				PropertyInfo targetProperty = Array.Find(targetProperties, (p) => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);
				if (targetProperty == null)
					continue;

				try
				{
					if (targetProperty.GetSetMethod() != null && sourceProperty.GetSetMethod() != null)
					{
						targetProperty.SetValue(this, sourceProperty.GetValue(source));
					}
				}
				catch { }
			}
		}

		#region Converter

		internal class Converter : System.ComponentModel.TypeConverter
		{
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					DataSet dataSet = (DataSet)value;
					if (dataSet != null && !String.IsNullOrEmpty(dataSet.Label))
						return dataSet.Label;
				}

				return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		#endregion
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Line"/> chart.
	/// </summary>
	public class LineDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.LineDataSet"/>.
		/// </summary>
		public LineDataSet() :base()
		{
			this.Fill = false;
			this.PointStyle = PointStyle.Circle;
			this.PointRadius = 5;
			this.PointHoverRadius = 5;
			this.Type = ChartType.Line;
		}

		/// <summary>
		/// The color of the line.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[Description("The color of the line.")]
		public new Color BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// If true, fill the area under the line.
		/// </summary>
		[DefaultValue(false)]
		[Description("If true, fill the area under the line.")]
		public bool Fill
		{
			get;
			set;
		}

		/// <summary>
		/// The style of a point on the line.
		/// </summary>
		[DefaultValue(PointStyle.Circle)]
		[Description("The style of a point on the line.")]
		public PointStyle PointStyle
		{
			get;
			set;
		}

		/// <summary>
		/// The radius of the point shape. If set to 0, nothing is rendered.
		/// </summary>
		[DefaultValue(5)]
		[Description("The radius of the point shape. If set to 0, nothing is rendered.")]
		public int PointRadius
		{
			get;
			set;
		}

		/// <summary>
		/// The radius of the point when hovered.
		/// </summary>
		[DefaultValue(5)]
		[Description("The radius of the point when hovered")]
		public int PointHoverRadius
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Bar"/> chart.
	/// </summary>
	public class BarDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.BarDataSet"/>.
		/// </summary>
		public BarDataSet() : base()
		{
			this.Type = ChartType.Bar;
		}

		/// <summary>
		/// The fill colors of the data set. One entry is the default color for all the bars, otherwise
		/// each bar can define a different color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the data set. One entry is the default color for all the bars, otherwise each bar can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public new Color[] BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// The border colors of the data set. One entry is the default color for all the bars, otherwise
		/// each bar can define a different color for each bar.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the data set. One entry is the default color for all the bars, otherwise each bar can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public new Color[] BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// The hover colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different hover background color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The hover colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a different hover background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public Color[] HoverBackgroundColor
		{
			get;
			set;
		}

	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.HorizontalBar"/> chart.
	/// </summary>
	public class HorizontalBarDataSet : BarDataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.HorizontalBarDataSet"/>.
		/// </summary>
		public HorizontalBarDataSet() : base()
		{
			this.Type = ChartType.HorizontalBar;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Doughnut"/> chart.
	/// </summary>
	public class DoughnutDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.DoughnutDataSet"/>.
		/// </summary>
		public DoughnutDataSet() : base()
		{
			this.Type = ChartType.Doughnut;
		}

		/// <summary>
		/// The fill colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a background color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public new Color[] BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// The border colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different border color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public new Color[] BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// The hover colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different hover background color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The hover colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a different hover background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public Color[] HoverBackgroundColor
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Pie"/> chart.
	/// </summary>
	public class PieDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.PieDataSet"/>.
		/// </summary>
		public PieDataSet() : base()
		{
			this.Type = ChartType.Pie;
		}

		/// <summary>
		/// The fill colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a background color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public new Color[] BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// The border colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different border color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public new Color[] BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// The hover colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different hover background color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The hover colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a different hover background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public Color[] HoverBackgroundColor
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.PolarArea"/> chart.
	/// </summary>
	public class PolarAreaDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.PolarAreaDataSet"/>.
		/// </summary>
		public PolarAreaDataSet() : base()
		{
			this.Type = ChartType.PolarArea;
		}

		/// <summary>
		/// The fill colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a background color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public new Color[] BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// The border colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different border color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public new Color[] BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// The hover colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different hover background color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The hover colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a different hover background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public Color[] HoverBackgroundColor
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS.ChartType.Radar"/> chart.
	/// </summary>
	public class RadarDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS.RadarDataSet"/>.
		/// </summary>
		public RadarDataSet() : base()
		{
			this.Type = ChartType.Radar;
		}
	}
}
