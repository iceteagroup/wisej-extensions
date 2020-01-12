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
		/// Hides the dataset.
		/// </summary>
		[DefaultValue(false)]
		[Description("Hides the dataset.")]
		public bool Hidden
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
		/// Formatted representation of the data to plot displayed when <see cref="OptionsDataLabel.Display"/> is true.
		/// </summary>
		public string[] Formatted
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

		/// <summary>
		/// Binds the <see cref="DataSet"/> to the specified y axis
		/// </summary>
		[DefaultValue("")]
		[Description("Binds the dataset to the specified y axis")]
		public String yAxisID
		{
			get;
			set;
		}

		/// <summary>
		/// Binds the <see cref="DataSet"/> to the specified x axis
		/// </summary>
		[DefaultValue("")]
		[Description("Binds the dataset to the specified x axis")]
		public String xAxisID
		{
			get;
			set;
		}

		/// <summary>
		/// The drawing order of the dataset. Also affects order for stacking, tooltip, and legend.
		/// </summary>
		[DefaultValue(0)]
		[Description("The drawing order of the dataset. Also affects order for stacking, tooltip, and legend.")]
		public int Order
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
		public LineDataSet() : base()
		{
			this.Fill = false;
			this.ShowLine = true;
			this.SteppedLine = SteppedLine.False;
			this.SpanGaps = false;
			this.PointStyle = new PointStyle[] { Wisej.Web.Ext.ChartJS.PointStyle.Circle };
			this.PointRadius = new int[] { 5 };
			this.PointHoverRadius = new int[] { 5 };
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
		/// Length and spacing of dashes. See https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/setLineDash.
		/// </summary>
		[DefaultValue(null)]
		[Description("Length and spacing of dashes.")]
		public int[] BorderDash
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
		/// If false lines between points are not drawn.
		/// </summary>
		[DefaultValue(true)]
		[Description("If false lines between points are not drawn.")]
		public bool ShowLine
		{
			get;
			set;
		}

		/// <summary>
		/// If true, lines will be drawn between points with no or null data. If false, points with NaN data will create a break in the line
		/// </summary>
		[DefaultValue(false)]
		[Description("If true, lines will be drawn between points with no or null data. If false, points with NaN data will create a break in the line.")]
		public bool SpanGaps
		{
			get;
			set;
		}

		/// <summary>
		/// If true, show the line.
		/// </summary>
		[DefaultValue(SteppedLine.False)]
		[Description("Show a stepped line rather than a curve.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public SteppedLine SteppedLine
		{
			get;
			set;
		}

		/// <summary>
		/// The style of a point on the line.One entry is the default for all points, otherwise each point can define a style.
		/// </summary>
		[DefaultValue(new PointStyle[] { Wisej.Web.Ext.ChartJS.PointStyle.Circle })]
		[MergableProperty(false)]
		[Description("The style of a point on the line. One entry is the default for all points, otherwise each point can define a style.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public PointStyle[] PointStyle
		{
			get;
			set;
		}

		/// <summary>
		/// The radius of the point shape. If set to 0, nothing is rendered.One entry is the default radius for all points, otherwise each point can define a radius.
		/// </summary>
		[DefaultValue(new int[] { 5 })]
		[MergableProperty(false)]
		[Description("The radius of the point shape. If set to 0, nothing is rendered. One entry is the default radius for all points, otherwise each point can define a radius.")]
		public int[] PointRadius
		{
			get;
			set;
		}

		/// <summary>
		/// The radius of the point when hovered. One entry is the default hover radius for all points, otherwise each point can define a hover radius.
		/// </summary>
		[DefaultValue(new int[] { 5 })]
		[MergableProperty(false)]
		[Description("The radius of the point when hovered")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public int[] PointHoverRadius
		{
			get;
			set;
		}

		/// <summary>
		/// The fill colors of the points in the data set. One entry is the default color for all the points, otherwise
		/// each point can be defined as a different color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the points for the data set. One entry is the default color for all the points, otherwise each point can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public Color[] PointBackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// The border colors of the points in the data set. One entry is the default color for all the points, otherwise
		/// each point can be defined as a different color.
		/// </summary>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the points for the data set. One entry is the default color for all the points, otherwise each point can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public Color[] PointBorderColor
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
