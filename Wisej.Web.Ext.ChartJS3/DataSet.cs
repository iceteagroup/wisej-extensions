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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// Represents the data used by the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control to plot the chart.
	/// See <see href="https://www.chartjs.org/docs/3.5.0/general/data-structures.html"/> for additional information regarding the data structure of Chart.js.
	/// </summary>
	/// <remarks>
	/// This is the base class for the chart-specific data sets: <see cref="T:Wisej.Web.Ext.ChartJS3.LineDataSet"/>,
	/// <see cref="T:Wisej.Web.Ext.ChartJS3.BarDataSet"/>, <see cref="T:Wisej.Web.Ext.ChartJS3.HorizontalBarDataSet"/>,
	/// <see cref="T:Wisej.Web.Ext.ChartJS3.DoughnutDataSet"/>, <see cref="T:Wisej.Web.Ext.ChartJS3.PieDataSet"/>,
	/// <see cref="T:Wisej.Web.Ext.ChartJS3.PolarAreaDataSet"/> and <see cref="T:Wisej.Web.Ext.ChartJS3.RadarDataSet"/>.
	/// <para>
	/// Use <see cref="M:Wisej.Web.Ext.ChartJS3.DataSetCollection.Add(System.String)"/> on <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.DataSets"/>
	/// to create a data set that matches the current <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/>, then cast it
	/// to the specialized type to access the chart-specific properties. The public properties are sent to the client
	/// (in camel case) as the Chart.js dataset configuration.
	/// </para>
	/// <para>
	/// The properties of a data set don't refresh the chart when they change. Call <see cref="M:Wisej.Web.Control.Update"/>
	/// on the chart to redraw it, or <see cref="M:Wisej.Web.Ext.ChartJS3.ChartJS3.UpdateData(System.Int32)"/> when only
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Data"/> has changed.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Bar;
	/// this.chartJS31.Labels = new[] { "Q1", "Q2", "Q3", "Q4" };
	///
	/// var sales = this.chartJS31.DataSets.Add("Sales");
	/// sales.Data = new object[] { 120, 150, 90, 180 };
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS3")]
	[TypeConverter(typeof(DataSet.Converter))]
	public class DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/>.
		/// </summary>
		/// <remarks>
		/// The new data set has the <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Label"/> set to "Data Set" and both
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BackgroundColor"/> and <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderColor"/> set to a
		/// semi-transparent black (alpha 76). The <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Type"/> of a base data set is null.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new DataSet
		/// {
		/// 	Label = "Visitors",
		/// 	Data = new object[] { 10, 20, 15 }
		/// };
		/// this.chartJS31.DataSets.Add(dataSet);
		/// ]]></code>
		/// </example>
		public DataSet()
		{
			this.Label = "Data Set";
			this.BorderColor = Color.FromArgb(76, 0, 0, 0);
			this.BackgroundColor = Color.FromArgb(76, 0, 0, 0);
		}

		/// <summary>
		/// Returns or sets the label for the data set which appears in the legend and tooltips.
		/// </summary>
		/// <value>
		/// The text of the label. The constructor initializes it to "Data Set"; data sets created with
		/// <see cref="M:Wisej.Web.Ext.ChartJS3.DataSetCollection.Add(System.String)"/> use the specified name.
		/// </value>
		/// <remarks>
		/// The label is also used by the string indexer of <see cref="T:Wisej.Web.Ext.ChartJS3.DataSetCollection"/> to find a data set.
		/// Setting this property doesn't redraw the chart; call <see cref="M:Wisej.Web.Control.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = this.chartJS31.DataSets.Add("Revenue");
		///
		/// // rename the data set, the legend shows the new label.
		/// dataSet.Label = "Revenue (USD)";
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue("")]
		[Description("The label for the dataset which appears in the legend and tooltips.")]
		public string Label
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets whether the data set is hidden.
		/// </summary>
		/// <value>
		/// True to hide the data set; otherwise false. The default is false.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hidden" property. A hidden data set is not plotted but its entry
		/// is still shown (crossed out) in the legend and the user can click it to show the data set again.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// private void checkBoxShowCosts_CheckedChanged(object sender, EventArgs e)
		/// {
		/// 	var costs = this.chartJS31.DataSets["Costs"];
		/// 	if (costs != null)
		/// 	{
		/// 		costs.Hidden = !this.checkBoxShowCosts.Checked;
		/// 		this.chartJS31.Update();
		/// 	}
		/// }
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("Hides the dataset.")]
		public bool Hidden
		{
			get;
			set;
		}

		/// <summary>
		/// Returns the type of chart that plots this type of <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/>.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartType"/> values, or null for an instance of the base
		/// <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> class. The value is set by the constructor of the specialized data set class
		/// and cannot be changed from outside the class.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "type" property, which allows Chart.js to mix different chart types in the same chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// foreach (var dataSet in this.chartJS31.DataSets)
		/// {
		/// 	if (dataSet.Type == ChartType.Line)
		/// 		((LineDataSet)dataSet).Fill = true;
		/// }
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[Description("Returns the type of chart that plots this data set.")]
		public ChartType? Type
		{
			get;
			protected set;
		}

		/// <summary>
		/// Returns or sets the data to plot.
		/// </summary>
		/// <value>
		/// An array of values, usually one for each entry in <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Labels"/>. The default is null.
		/// </value>
		/// <remarks>
		/// The array is serialized to the client as the Chart.js dataset "data" property. It usually contains numbers but may
		/// also contain objects, for example objects with x and y properties for scatter charts or x, y and r for bubble charts.
		/// Setting this property doesn't redraw the chart: call <see cref="M:Wisej.Web.Ext.ChartJS3.ChartJS3.UpdateData(System.Int32)"/> to
		/// animate the chart to the new values (it sends only the data and the labels), or <see cref="M:Wisej.Web.Control.Update"/>
		/// for a full refresh.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var temperatures = this.chartJS31.DataSets.Add("Temperature");
		/// temperatures.Data = new object[] { 12.5, 14.1, 17.8, 21.3, 19.0 };
		///
		/// // later: replace the values and animate the transition.
		/// temperatures.Data = new object[] { 13.0, 15.2, 18.4, 22.0, 20.1 };
		/// this.chartJS31.UpdateData(500);
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public object[] Data
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the formatted representation of the data to plot, displayed when <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.Display"/> is true.
		/// </summary>
		/// <value>
		/// An array of strings matching the items in <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Data"/> by position, or null.
		/// </value>
		/// <remarks>
		/// When the array is set and not empty, the default data labels formatter on the client shows the string at the same index
		/// of the data point. When the array is null or empty, the data label shows the value rounded to the nearest integer.
		/// The formatted strings are used only by the data labels plugin, not by the tooltips.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var sales = this.chartJS31.DataSets.Add("Sales");
		/// sales.Data = new object[] { 1200.5, 980.25, 1430 };
		/// sales.Formatted = new[] { "$1,200.50", "$980.25", "$1,430.00" };
		///
		/// this.chartJS31.Options.Plugins.DataLabels.Display = true;
		/// ]]></code>
		/// </example>
		public string[] Formatted
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the fill color of the data set. What it fills is up to the chart type.
		/// </summary>
		/// <value>
		/// The fill <see cref="T:System.Drawing.Color"/>. The default is a semi-transparent black (alpha 76).
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "backgroundColor" property. The bar, doughnut, pie and polar area data sets
		/// hide this property with an array version that allows a different color for each data point.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// DataSet dataSet = this.chartJS31.DataSets.Add("Orders");
		/// dataSet.BackgroundColor = Color.FromArgb(80, Color.SteelBlue);
		/// dataSet.BorderColor = Color.SteelBlue;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "76, 0, 0, 0")]
		[Description("The fill color of the data set. What it fills is up to the chart type.")]
		public Color BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border color of the data set. What it colors is up to the chart type.
		/// </summary>
		/// <value>
		/// The border <see cref="T:System.Drawing.Color"/>. The default is a semi-transparent black (alpha 76).
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "borderColor" property. Some specialized data sets hide this property with
		/// their own version, for example <see cref="P:Wisej.Web.Ext.ChartJS3.LineDataSet.BorderColor"/> or the array version in
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.BarDataSet.BorderColor"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new DataSet { Label = "Signups" };
		/// dataSet.Data = new object[] { 5, 9, 14 };
		/// dataSet.BorderColor = Color.DarkOrange;
		/// dataSet.BorderWidth = 2;
		/// this.chartJS31.DataSets.Add(dataSet);
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "76, 0, 0, 0")]
		[Description("The border color of the data set. What it fills is up to the chart type.")]
		public Color BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the width of the border in pixels.
		/// </summary>
		/// <value>
		/// The border width in pixels. The default is 0; the specialized data sets change it in their constructors
		/// (3 for <see cref="T:Wisej.Web.Ext.ChartJS3.LineDataSet"/> and <see cref="T:Wisej.Web.Ext.ChartJS3.RadarDataSet"/>, 2 for the
		/// doughnut, pie and polar area data sets).
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "borderWidth" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = (LineDataSet)this.chartJS31.DataSets.Add("Trend");
		/// line.Data = new object[] { 3, 7, 4, 9 };
		/// line.BorderWidth = 1;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(0)]
		[Description("The width of the border in pixels.")]
		public int BorderWidth
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the ID of the y axis the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> is bound to.
		/// </summary>
		/// <value>
		/// The ID of the y axis, or null (the default) to use the first y axis.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "yAxisID" property. The client registers the axes defined in
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScales.yAxes"/> as scales with the IDs "y0", "y1", etc.,
		/// according to their position in the array, so this value must use the same naming.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // add a second y axis and plot the "Rate" data set on it.
		/// var scales = this.chartJS31.Options.Scales;
		/// scales.yAxes = new[] { new OptionScalesAxesY(), new OptionScalesAxesY() };
		///
		/// var rate = this.chartJS31.DataSets.Add("Rate");
		/// rate.Data = new object[] { 0.2, 0.35, 0.5 };
		/// rate.yAxisID = "y1";
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Binds the dataset to the specified y axis")]
		public string yAxisID
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the ID of the x axis the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> is bound to.
		/// </summary>
		/// <value>
		/// The ID of the x axis, or null (the default) to use the first x axis.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "xAxisID" property. The client registers the axes defined in
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScales.xAxes"/> as scales with the IDs "x0", "x1", etc.,
		/// according to their position in the array, so this value must use the same naming.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var scales = this.chartJS31.Options.Scales;
		/// scales.xAxes = new[] { new OptionScalesAxesX(), new OptionScalesAxesX() };
		///
		/// var forecast = this.chartJS31.DataSets.Add("Forecast");
		/// forecast.Data = new object[] { 40, 42, 47 };
		/// forecast.xAxisID = "x1";
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Binds the dataset to the specified x axis")]
		public string xAxisID
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the drawing order of the data set. Also affects the order for stacking, tooltip, and legend.
		/// </summary>
		/// <value>
		/// The drawing order. The default is 0.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "order" property. Data sets with a higher order are drawn first,
		/// so a data set with a lower order is drawn on top of the others.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var actual = this.chartJS31.DataSets.Add("Actual");
		/// var target = this.chartJS31.DataSets.Add("Target");
		///
		/// // draw the target on top of the actual values.
		/// actual.Order = 2;
		/// target.Order = 1;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
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
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Line"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSetCollection"/> creates this type of data set when the
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Line"/>,
	/// <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bubble"/> or <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Scatter"/>.
	/// <para>
	/// When the point style, point radius or point hover radius arrays contain a single entry, the client
	/// converts them to a single value that applies to all the points.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Line;
	/// this.chartJS31.Labels = new[] { "Mon", "Tue", "Wed", "Thu", "Fri" };
	///
	/// var visits = (LineDataSet)this.chartJS31.DataSets.Add("Visits");
	/// visits.Data = new object[] { 120, 98, 143, 160, 110 };
	/// visits.BorderColor = Color.SeaGreen;
	/// visits.Fill = true;
	/// ]]></code>
	/// </example>
	public class LineDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS3.LineDataSet"/>.
		/// </summary>
		/// <remarks>
		/// The new data set has <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Type"/> set to <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Line"/>,
		/// a <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderWidth"/> of 3, no fill, the line shown, no gap spanning, no stepping,
		/// and circle points with a radius and hover radius of 5 pixels.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = new LineDataSet
		/// {
		/// 	Label = "Pressure",
		/// 	Data = new object[] { 1012, 1008, 1015 },
		/// 	BorderColor = Color.RoyalBlue
		/// };
		/// this.chartJS31.DataSets.Add(line);
		/// ]]></code>
		/// </example>
		public LineDataSet() : base()
		{
			this.Fill = false;
			this.ShowLine = true;
			this.BorderWidth = 3;
			this.SpanGaps = false;
			this.Type = ChartType.Line;
			this.Stepped = SteppedLine.False;
			this.PointRadius = new int[] { 5 };
			this.PointHoverRadius = new int[] { 5 };
			this.PointStyle = new PointStyle[] { Ext.ChartJS3.PointStyle.Circle };
		}

		/// <summary>
		/// Returns or sets the color of the line.
		/// </summary>
		/// <value>
		/// The line <see cref="T:System.Drawing.Color"/>. The default is <see cref="F:System.Drawing.Color.Empty"/>.
		/// </value>
		/// <remarks>
		/// Hides the inherited <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderColor"/> and maps to the Chart.js dataset "borderColor" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = (LineDataSet)this.chartJS31.DataSets[0];
		/// line.BorderColor = Color.Crimson;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[Description("The color of the line.")]
		public new Color BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the length and spacing of dashes. See <see href="https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/setLineDash"/>.
		/// </summary>
		/// <value>
		/// An array of numbers that alternately specify the length of the dashes and of the gaps, in pixels.
		/// The default is null, which draws a solid line.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "borderDash" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var target = (LineDataSet)this.chartJS31.DataSets.Add("Target");
		/// target.Data = new object[] { 100, 100, 100, 100 };
		///
		/// // 6px dashes separated by 4px gaps.
		/// target.BorderDash = new[] { 6, 4 };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Length and spacing of dashes.")]
		public int[] BorderDash
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets whether to fill the area under the line.
		/// </summary>
		/// <value>
		/// True to fill the area under the line using <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BackgroundColor"/>; otherwise false. The default is false.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "fill" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var area = (LineDataSet)this.chartJS31.DataSets.Add("Usage");
		/// area.Data = new object[] { 30, 45, 38, 60 };
		/// area.Fill = true;
		/// area.BackgroundColor = Color.FromArgb(60, Color.DodgerBlue);
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("If true, fill the area under the line.")]
		public bool Fill
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets whether the line between the points is drawn.
		/// </summary>
		/// <value>
		/// True to draw the line; false to draw only the points. The default is true.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "showLine" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // show only the points, like a scatter plot.
		/// var samples = (LineDataSet)this.chartJS31.DataSets.Add("Samples");
		/// samples.Data = new object[] { 4, 8, 6, 9 };
		/// samples.ShowLine = false;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("If false lines between points are not drawn.")]
		public bool ShowLine
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets whether lines are drawn between points with no or null data.
		/// </summary>
		/// <value>
		/// True to draw lines across the missing points; false to break the line at points with null or NaN data. The default is false.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "spanGaps" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var readings = (LineDataSet)this.chartJS31.DataSets.Add("Readings");
		/// readings.Data = new object[] { 21.5, null, 22.8, null, 23.1 };
		/// readings.SpanGaps = true;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		[Description("If true, lines will be drawn between points with no or null data. If false, points with NaN data will create a break in the line.")]
		public bool SpanGaps
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the style of the points on the line. One entry is the default for all points, otherwise each point can define a style.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:Wisej.Web.Ext.ChartJS3.PointStyle"/> values. The default is a single <see cref="F:Wisej.Web.Ext.ChartJS3.PointStyle.Circle"/> entry.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "pointStyle" property. When the array contains a single entry the client sends it as a single value.
		/// The client expects the array to contain at least one entry.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = (LineDataSet)this.chartJS31.DataSets.Add("Events");
		/// line.Data = new object[] { 3, 5, 2 };
		/// line.PointStyle = new[] { PointStyle.Triangle, PointStyle.Star, PointStyle.RectRot };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(new PointStyle[] { Wisej.Web.Ext.ChartJS3.PointStyle.Circle })]
		[MergableProperty(false)]
		[Description("The style of a point on the line. One entry is the default for all points, otherwise each point can define a style.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public PointStyle[] PointStyle
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the radius of the point shape. If set to 0, nothing is rendered. One entry is the default radius for all points, otherwise each point can define a radius.
		/// </summary>
		/// <value>
		/// An array of radius values in pixels. The default is a single entry of 5.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "pointRadius" property. When the array contains a single entry the client sends it as a single value.
		/// The client expects the array to contain at least one entry.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // hide the points and draw only the line.
		/// var line = (LineDataSet)this.chartJS31.DataSets[0];
		/// line.PointRadius = new[] { 0 };
		/// line.PointHoverRadius = new[] { 4 };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(new int[] { 5 })]
		[MergableProperty(false)]
		[Description("The radius of the point shape. If set to 0, nothing is rendered. One entry is the default radius for all points, otherwise each point can define a radius.")]
		public int[] PointRadius
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the radius of the point when hovered. One entry is the default hover radius for all points, otherwise each point can define a hover radius.
		/// </summary>
		/// <value>
		/// An array of radius values in pixels. The default is a single entry of 5.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "pointHoverRadius" property. When the array contains a single entry the client sends it as a single value.
		/// The client expects the array to contain at least one entry.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = (LineDataSet)this.chartJS31.DataSets.Add("Clicks");
		/// line.Data = new object[] { 12, 19, 7 };
		/// line.PointHoverRadius = new[] { 8 };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(new int[] { 5 })]
		[MergableProperty(false)]
		[Description("The radius of the point when hovered")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public int[] PointHoverRadius
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the fill colors of the points in the data set. One entry is the default color for all the points, otherwise
		/// each point can be defined as a different color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "pointBackgroundColor" property. An array with a single entry is sent to the client as a single color.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = (LineDataSet)this.chartJS31.DataSets.Add("Status");
		/// line.Data = new object[] { 80, 95, 60 };
		///
		/// // color each point according to its value.
		/// line.PointBackgroundColor = new[] { Color.Orange, Color.Green, Color.Red };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the points for the data set. One entry is the default color for all the points, otherwise each point can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public Color[] PointBackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border colors of the points in the data set. One entry is the default color for all the points, otherwise
		/// each point can be defined as a different color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "pointBorderColor" property. An array with a single entry is sent to the client as a single color.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = (LineDataSet)this.chartJS31.DataSets[0];
		/// line.PointBorderColor = new[] { Color.White };
		/// line.PointBackgroundColor = new[] { Color.SteelBlue };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the points for the data set. One entry is the default color for all the points, otherwise each point can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public Color[] PointBorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the point background color when hovered.
		/// </summary>
		/// <value>
		/// The hover background <see cref="T:System.Drawing.Color"/>. The default is <see cref="F:System.Drawing.Color.Empty"/>.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "pointHoverBackgroundColor" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = (LineDataSet)this.chartJS31.DataSets[0];
		/// line.PointHoverBackgroundColor = Color.Gold;
		/// line.PointHoverBorderColor = Color.DarkGoldenrod;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[MergableProperty(false)]
		[Description("Specifies the background color when hovered.")]
		public Color PointHoverBackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the point border color when hovered.
		/// </summary>
		/// <value>
		/// The hover border <see cref="T:System.Drawing.Color"/>. The default is <see cref="F:System.Drawing.Color.Empty"/>.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "pointHoverBorderColor" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = (LineDataSet)this.chartJS31.DataSets.Add("Latency");
		/// line.Data = new object[] { 45, 52, 38 };
		/// line.PointHoverBorderColor = Color.Black;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[MergableProperty(false)]
		[Description("Specifies the point border color when hovered.")]
		public Color PointHoverBorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the Bezier curve tension of the line. Set to 0 to draw straight lines. This option is ignored if monotone cubic interpolation is used.
		/// </summary>
		/// <value>
		/// The curve tension, usually between 0 and 1. The default is 0.4.
		/// </value>
		/// <remarks>
		/// Sent to the client as the dataset "lineTension" property. Note that Chart.js 3 documents this option as "tension".
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var line = (LineDataSet)this.chartJS31.DataSets[0];
		///
		/// // draw straight segments between the points.
		/// line.LineTension = 0;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(0.4)]
		[MergableProperty(false)]
		[Description("Bezier curve tension of the line. Set to 0 to draw straight lines. This option is ignored if monotone cubic interpolation is used.")]
		public double LineTension 
		{ 
			get; 
			set; 
		} = 0.4;

		/// <summary>
		/// Returns or sets the ID of the group to which this data set belongs
		/// (when stacked, each group will be a separate stack).
		/// </summary>
		/// <value>
		/// The ID of the stack group, or null (the default).
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "stack" property. The data sets are stacked only when stacking is enabled on the axes,
		/// for example with <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Stacked"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var online = (LineDataSet)this.chartJS31.DataSets.Add("Online");
		/// var retail = (LineDataSet)this.chartJS31.DataSets.Add("Retail");
		/// online.Stack = "sales";
		/// retail.Stack = "sales";
		///
		/// this.chartJS31.Options.Scales.yAxes[0].Stacked = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Description("The ID of the group to which this dataset belongs to.")]
		public string Stack
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets whether and how the line is drawn as stepped values.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS3.SteppedLine"/> values. The default is <see cref="F:Wisej.Web.Ext.ChartJS3.SteppedLine.False"/>.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "stepped" property. When stepped, the line tension is ignored.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var price = (LineDataSet)this.chartJS31.DataSets.Add("Price");
		/// price.Data = new object[] { 9.99, 9.99, 12.49, 11.99 };
		/// price.Stepped = SteppedLine.After;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[DefaultValue(SteppedLine.False)]
		[Description("When true, Shows the lines as stepped values.")]
		public SteppedLine Stepped
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bar"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSetCollection"/> creates this type of data set when the
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bar"/> or
	/// <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.HorizontalBar"/>. Color arrays with a single entry are sent to the client as a single color.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Bar;
	/// this.chartJS31.Labels = new[] { "North", "South", "East", "West" };
	///
	/// var units = (BarDataSet)this.chartJS31.DataSets.Add("Units");
	/// units.Data = new object[] { 320, 210, 180, 260 };
	/// units.BackgroundColor = new[] { Color.CornflowerBlue };
	/// ]]></code>
	/// </example>
	public class BarDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS3.BarDataSet"/>.
		/// </summary>
		/// <remarks>
		/// The new data set has <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Type"/> set to <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bar"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bars = new BarDataSet
		/// {
		/// 	Label = "Budget",
		/// 	Data = new object[] { 5000, 7200, 6100 }
		/// };
		/// this.chartJS31.DataSets.Add(bars);
		/// ]]></code>
		/// </example>
		public BarDataSet() : base()
		{
			this.Type = ChartType.Bar;
		}

		/// <summary>
		/// Returns or sets the fill colors of the data set. One entry is the default color for all the bars, otherwise
		/// each bar can define a different color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides the inherited <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BackgroundColor"/> and maps to the Chart.js dataset "backgroundColor" property.
		/// An array with a single entry is sent to the client as a single color.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bars = (BarDataSet)this.chartJS31.DataSets.Add("Score");
		/// bars.Data = new object[] { 72, 45, 90 };
		///
		/// // one color per bar.
		/// bars.BackgroundColor = new[] { Color.Goldenrod, Color.IndianRed, Color.SeaGreen };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the data set. One entry is the default color for all the bars, otherwise each bar can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border colors of the data set. One entry is the default color for all the bars, otherwise
		/// each bar can define a different border color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides the inherited <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderColor"/> and maps to the Chart.js dataset "borderColor" property.
		/// The border is visible only when <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderWidth"/> is greater than 0.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bars = (BarDataSet)this.chartJS31.DataSets[0];
		/// bars.BorderColor = new[] { Color.Navy };
		/// bars.BorderWidth = 2;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the data set. One entry is the default color for all the bars, otherwise each bar can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the hover colors of the data set. One entry is the default color for all the bars, otherwise
		/// each bar can define a different hover background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hoverBackgroundColor" property. An array with a single entry is sent to the client as a single color.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bars = (BarDataSet)this.chartJS31.DataSets[0];
		/// bars.BackgroundColor = new[] { Color.LightSteelBlue };
		/// bars.HoverBackgroundColor = new[] { Color.SteelBlue };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The hover colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a different hover background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public Color[] HoverBackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border color when hovered.
		/// </summary>
		/// <value>
		/// The hover border <see cref="T:System.Drawing.Color"/>. The default is <see cref="F:System.Drawing.Color.Empty"/>.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hoverBorderColor" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bars = (BarDataSet)this.chartJS31.DataSets[0];
		/// bars.HoverBorderColor = Color.Black;
		/// bars.HoverBorderWidth = 3;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[MergableProperty(false)]
		[Description("Specifies the border color when hovered.")]
		public Color HoverBorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border width when hovered (in pixels).
		/// </summary>
		/// <value>
		/// The hover border width in pixels. The default is 1.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hoverBorderWidth" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var bars = (BarDataSet)this.chartJS31.DataSets.Add("Tickets");
		/// bars.Data = new object[] { 14, 22, 9 };
		/// bars.HoverBorderColor = Color.DarkRed;
		/// bars.HoverBorderWidth = 2;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(1)]
		[MergableProperty(false)]
		[Description("Specifies the border color when hovered.")]
		public int HoverBorderWidth
		{
			get;
			set;
		} = 1;

		/// <summary>
		/// Returns or sets the border radius when hovered (in pixels).
		/// </summary>
		/// <value>
		/// The hover border radius in pixels. The default is 0.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hoverBorderRadius" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // round the corners of the bar under the pointer.
		/// var bars = (BarDataSet)this.chartJS31.DataSets[0];
		/// bars.HoverBorderRadius = 6;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(0)]
		[MergableProperty(false)]
		[Description("Specifies the border radius when hovered (in pixels).")]
		public int HoverBorderRadius
		{
			get;
			set;
		} = 0;

		/// <summary>
		/// Returns or sets the ID of the group to which this data set belongs
		/// (when stacked, each group will be a separate stack).
		/// </summary>
		/// <value>
		/// The ID of the stack group, or null (the default).
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "stack" property. The bars are stacked only when stacking is enabled, for example
		/// with <see cref="P:Wisej.Web.Ext.ChartJS3.BarOptions.Stacked"/> or <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Stacked"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = (BarOptions)this.chartJS31.Options;
		/// options.Stacked = true;
		///
		/// ((BarDataSet)this.chartJS31.DataSets.Add("2023 Q1")).Stack = "2023";
		/// ((BarDataSet)this.chartJS31.DataSets.Add("2023 Q2")).Stack = "2023";
		/// ((BarDataSet)this.chartJS31.DataSets.Add("2024 Q1")).Stack = "2024";
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Description("The ID of the group to which this dataset belongs to.")]
		public string Stack
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.HorizontalBar"/> chart.
	/// </summary>
	/// <remarks>
	/// Chart.js 3 draws horizontal bars as a "bar" chart with the index axis set to "y". The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSetCollection"/>
	/// creates <see cref="T:Wisej.Web.Ext.ChartJS3.BarDataSet"/> instances for a <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.HorizontalBar"/> chart;
	/// set <see cref="P:Wisej.Web.Ext.ChartJS3.Options.IndexAxis"/> to "y" to lay the bars horizontally.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.HorizontalBar;
	/// this.chartJS31.Options.IndexAxis = "y";
	/// this.chartJS31.Labels = new[] { "Chrome", "Firefox", "Safari" };
	///
	/// var share = (BarDataSet)this.chartJS31.DataSets.Add("Share");
	/// share.Data = new object[] { 64, 18, 12 };
	/// ]]></code>
	/// </example>
	public class HorizontalBarDataSet : BarDataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS3.HorizontalBarDataSet"/>.
		/// </summary>
		/// <remarks>
		/// The new data set has <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Type"/> set to <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.HorizontalBar"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new HorizontalBarDataSet
		/// {
		/// 	Label = "Hours",
		/// 	Data = new object[] { 7.5, 6, 8 }
		/// };
		/// this.chartJS31.DataSets.Add(dataSet);
		/// ]]></code>
		/// </example>
		public HorizontalBarDataSet() : base()
		{
			this.Type = ChartType.HorizontalBar;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Doughnut"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSetCollection"/> creates this type of data set when the
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Doughnut"/>.
	/// Each value in <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Data"/> is drawn as a slice; color arrays with a single entry are sent
	/// to the client as a single color.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Doughnut;
	/// this.chartJS31.Labels = new[] { "Rent", "Food", "Travel" };
	///
	/// var dataSet = (DoughnutDataSet)this.chartJS31.DataSets.Add("Budget");
	/// dataSet.Data = new object[] { 1200, 450, 300 };
	/// dataSet.BackgroundColor = new[] { Color.SlateBlue, Color.Coral, Color.MediumSeaGreen };
	/// ]]></code>
	/// </example>
	public class DoughnutDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS3.DoughnutDataSet"/>.
		/// </summary>
		/// <remarks>
		/// The new data set has <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Type"/> set to <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Doughnut"/>
		/// and a <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderWidth"/> of 2.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new DoughnutDataSet
		/// {
		/// 	Label = "Budget",
		/// 	Data = new object[] { 1200, 450, 300 }
		/// };
		/// this.chartJS31.DataSets.Add(dataSet);
		/// ]]></code>
		/// </example>
		public DoughnutDataSet() : base()
		{
			this.BorderWidth = 2;
			this.Type = ChartType.Doughnut;
		}

		/// <summary>
		/// Returns or sets the fill colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, usually one for each slice, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides the inherited <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BackgroundColor"/> and maps to the Chart.js dataset "backgroundColor" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = (DoughnutDataSet)this.chartJS31.DataSets[0];
		/// dataSet.BackgroundColor = new[] { Color.SlateBlue, Color.Coral, Color.MediumSeaGreen };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different border color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides the inherited <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderColor"/> and maps to the Chart.js dataset "borderColor" property.
		/// The width of the border is set by <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderWidth"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // separate the slices with a white border.
		/// var dataSet = (DoughnutDataSet)this.chartJS31.DataSets[0];
		/// dataSet.BorderColor = new[] { Color.White };
		/// dataSet.BorderWidth = 3;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the hover colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different hover background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hoverBackgroundColor" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = (DoughnutDataSet)this.chartJS31.DataSets[0];
		/// dataSet.BackgroundColor = new[] { Color.SlateBlue, Color.Coral, Color.MediumSeaGreen };
		/// dataSet.HoverBackgroundColor = new[] { Color.DimGray };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The hover colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a different hover background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public Color[] HoverBackgroundColor
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Pie"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSetCollection"/> creates this type of data set when the
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Pie"/>.
	/// Each value in <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Data"/> is drawn as a slice; color arrays with a single entry are sent
	/// to the client as a single color.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Pie;
	/// this.chartJS31.Labels = new[] { "Yes", "No", "Undecided" };
	///
	/// var dataSet = (PieDataSet)this.chartJS31.DataSets.Add("Votes");
	/// dataSet.Data = new object[] { 540, 310, 150 };
	/// dataSet.BackgroundColor = new[] { Color.SeaGreen, Color.Firebrick, Color.Silver };
	/// ]]></code>
	/// </example>
	public class PieDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS3.PieDataSet"/>.
		/// </summary>
		/// <remarks>
		/// The new data set has <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Type"/> set to <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Pie"/>
		/// and a <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderWidth"/> of 2.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new PieDataSet
		/// {
		/// 	Label = "Votes",
		/// 	Data = new object[] { 540, 310, 150 }
		/// };
		/// this.chartJS31.DataSets.Add(dataSet);
		/// ]]></code>
		/// </example>
		public PieDataSet() : base()
		{
			this.BorderWidth = 2;
			this.Type = ChartType.Pie;
		}

		/// <summary>
		/// Returns or sets the fill colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, usually one for each slice, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides the inherited <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BackgroundColor"/> and maps to the Chart.js dataset "backgroundColor" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = (PieDataSet)this.chartJS31.DataSets[0];
		/// dataSet.BackgroundColor = new[] { Color.SeaGreen, Color.Firebrick, Color.Silver };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different border color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides the inherited <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderColor"/> and maps to the Chart.js dataset "borderColor" property.
		/// The width of the border is set by <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderWidth"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // separate the slices with a white border.
		/// var dataSet = (PieDataSet)this.chartJS31.DataSets[0];
		/// dataSet.BorderColor = new[] { Color.White };
		/// dataSet.BorderWidth = 3;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the hover colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different hover background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hoverBackgroundColor" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = (PieDataSet)this.chartJS31.DataSets[0];
		/// dataSet.BackgroundColor = new[] { Color.SeaGreen, Color.Firebrick, Color.Silver };
		/// dataSet.HoverBackgroundColor = new[] { Color.DimGray };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The hover colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a different hover background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public Color[] HoverBackgroundColor
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.PolarArea"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSetCollection"/> creates this type of data set when the
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.PolarArea"/>.
	/// Each value in <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Data"/> is drawn as a slice; color arrays with a single entry are sent
	/// to the client as a single color.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.PolarArea;
	/// this.chartJS31.Labels = new[] { "Network", "Storage", "CPU", "Memory" };
	///
	/// var dataSet = (PolarAreaDataSet)this.chartJS31.DataSets.Add("Incidents");
	/// dataSet.Data = new object[] { 11, 7, 4, 9 };
	/// dataSet.BackgroundColor = new[] { Color.Tomato, Color.Gold, Color.SkyBlue, Color.Plum };
	/// ]]></code>
	/// </example>
	public class PolarAreaDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS3.PolarAreaDataSet"/>.
		/// </summary>
		/// <remarks>
		/// The new data set has <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Type"/> set to <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.PolarArea"/>
		/// and a <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderWidth"/> of 2.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = new PolarAreaDataSet
		/// {
		/// 	Label = "Incidents",
		/// 	Data = new object[] { 11, 7, 4, 9 }
		/// };
		/// this.chartJS31.DataSets.Add(dataSet);
		/// ]]></code>
		/// </example>
		public PolarAreaDataSet() : base()
		{
			this.BorderWidth = 2;
			this.Type = ChartType.PolarArea;
		}

		/// <summary>
		/// Returns or sets the fill colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, usually one for each slice, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides the inherited <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BackgroundColor"/> and maps to the Chart.js dataset "backgroundColor" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = (PolarAreaDataSet)this.chartJS31.DataSets[0];
		/// dataSet.BackgroundColor = new[] { Color.Tomato, Color.Gold, Color.SkyBlue, Color.Plum };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The fill colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the border colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different border color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Hides the inherited <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderColor"/> and maps to the Chart.js dataset "borderColor" property.
		/// The width of the border is set by <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderWidth"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // separate the slices with a white border.
		/// var dataSet = (PolarAreaDataSet)this.chartJS31.DataSets[0];
		/// dataSet.BorderColor = new[] { Color.White };
		/// dataSet.BorderWidth = 3;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The border colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a border color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new Color[] BorderColor
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the hover colors of the data set. One entry is the default color for all the slices, otherwise
		/// each slice can define a different hover background color.
		/// </summary>
		/// <value>
		/// An array of <see cref="T:System.Drawing.Color"/> values, or null (the default) to use the Chart.js default.
		/// </value>
		/// <remarks>
		/// Maps to the Chart.js dataset "hoverBackgroundColor" property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = (PolarAreaDataSet)this.chartJS31.DataSets[0];
		/// dataSet.BackgroundColor = new[] { Color.Tomato, Color.Gold, Color.SkyBlue, Color.Plum };
		/// dataSet.HoverBackgroundColor = new[] { Color.DimGray };
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("The hover colors of the data set. One entry is the default color for all the slices, otherwise each slice can define a different hover background color.")]
		[Editor("System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public Color[] HoverBackgroundColor
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Specialized data set for the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Radar"/> chart.
	/// </summary>
	/// <remarks>
	/// The <see cref="T:Wisej.Web.Ext.ChartJS3.DataSetCollection"/> creates this type of data set when the
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Radar"/>.
	/// Each value in <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Data"/> is plotted on the axis of the label at the same position.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// this.chartJS31.ChartType = ChartType.Radar;
	/// this.chartJS31.Labels = new[] { "Speed", "Power", "Range", "Comfort", "Price" };
	///
	/// var car = (RadarDataSet)this.chartJS31.DataSets.Add("Model A");
	/// car.Data = new object[] { 8, 6, 7, 5, 4 };
	/// car.BackgroundColor = Color.FromArgb(60, Color.Teal);
	/// car.BorderColor = Color.Teal;
	/// ]]></code>
	/// </example>
	public class RadarDataSet : DataSet
	{
		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.ChartJS3.RadarDataSet"/>.
		/// </summary>
		/// <remarks>
		/// The new data set has <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Type"/> set to <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Radar"/>
		/// and a <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.BorderWidth"/> of 3.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var skills = new RadarDataSet
		/// {
		/// 	Label = "Candidate",
		/// 	Data = new object[] { 4, 5, 3, 4 }
		/// };
		/// this.chartJS31.DataSets.Add(skills);
		/// ]]></code>
		/// </example>
		public RadarDataSet() : base()
		{
			this.BorderWidth = 3;
			this.Type = ChartType.Radar;
		}

		/// <summary>
		/// Returns or sets the Bezier curve tension of the line. Set to 0 to draw straight lines.
		/// </summary>
		/// <value>
		/// The curve tension, usually between 0 and 1. The default is 0.
		/// </value>
		/// <remarks>
		/// Sent to the client as the dataset "lineTension" property. Note that Chart.js 3 documents this option as "tension".
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = (RadarDataSet)this.chartJS31.DataSets[0];
		///
		/// // draw smooth curves between the points.
		/// dataSet.LineTension = 0.3;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(0d)]
		[MergableProperty(false)]
		[Description("Bezier curve tension of the line. Set to 0 to draw straight lines.")]
		public double LineTension
		{
			get;
			set;
		}
	}
}
