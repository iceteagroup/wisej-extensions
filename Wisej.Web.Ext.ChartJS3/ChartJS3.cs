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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// ChartJS3 is a simple yet flexible JavaScript charting library for designers and developers from <see href="http://www.chartjs.org/"/>.
	/// </summary>
	/// <remarks>
	/// The control wraps Chart.js 3.5.0 and the chartjs-plugin-datalabels plugin.
	/// Configure the chart using <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/>, <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/>,
	/// <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Labels"/> and <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.DataSets"/>. Changing any of them
	/// redraws the whole chart on the client. To change only the data values with an animated transition, call
	/// <see cref="M:Wisej.Web.Ext.ChartJS3.ChartJS3.UpdateData(System.Int32)"/>.
	/// The chart is always responsive and fills the control: the client sets the Chart.js options <c>responsive</c> to true
	/// and <c>maintainAspectRatio</c> to false.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// private void Page1_Load(object sender, EventArgs e)
	/// {
	///     this.chartJS31.ChartType = ChartType.Bar;
	///     this.chartJS31.Labels = new[] { "Jan", "Feb", "Mar", "Apr" };
	///
	///     var sales = this.chartJS31.DataSets.Add("Sales");
	///     sales.Data = new object[] { 120, 95, 140, 110 };
	/// }
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(ChartJS3))]
	[DefaultEvent("ChartClick")]
	[Description("ChartJS is a simple yet flexible JavaScript charting for designers & developers from <see href=\"http://www.chartjs.org/.\"/>")]
	public class ChartJS3 : Widget, IWisejControl
	{
		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
		/// </summary>
		/// <remarks>
		/// The new control uses the <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Line"/> chart type and has no labels and no data sets.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS3();
		/// chart.Dock = DockStyle.Fill;
		/// chart.ChartType = ChartType.Pie;
		/// chart.Labels = new[] { "Red", "Green", "Blue" };
		/// chart.DataSets.Add("Votes").Data = new object[] { 12, 19, 7 };
		/// this.Controls.Add(chart);
		/// ]]></code>
		/// </example>
		public ChartJS3()
		{
		}

		#region Events

		/// <summary>
		/// Fired when the user clicks a data point on the chart.
		/// </summary>
		/// <remarks>
		/// The event is fired only at runtime and only when the click hits at least one active chart element.
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartClickEventArgs"/> argument contains the data sets, the indexes of the data points
		/// and the values of the elements under the click point, in matching order.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// public Page1()
		/// {
		///     InitializeComponent();
		///     this.chartJS31.ChartClick += this.chartJS31_ChartClick;
		/// }
		///
		/// private void chartJS31_ChartClick(object sender, ChartClickEventArgs e)
		/// {
		///     int point = e.DataPoints[0];
		///     AlertBox.Show($"{e.DataSets[0].Label} - {this.chartJS31.Labels[point]}: {e.Values[0]}");
		/// }
		/// ]]></code>
		/// </example>
		[Description("Fired when the user clicks a data point on the chart.")]
		public event ChartClickEventHandler ChartClick
		{
			add { base.Events.AddHandler(nameof(ChartClick), value); }
			remove { base.Events.RemoveHandler(nameof(ChartClick), value); }
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartClick"/> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnChartClick(ChartClickEventArgs e)
		{
			((ChartClickEventHandler)base.Events[nameof(ChartClick)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartType"/> of the chart.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartType"/> values. The default is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Line"/>.
		/// </value>
		/// <remarks>
		/// Changing the chart type replaces <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Options"/> with a new instance specific
		/// for the new type (i.e. <see cref="T:Wisej.Web.Ext.ChartJS3.BarOptions"/> for <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bar"/> and
		/// <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.HorizontalBar"/>), copying the shared options from the previous instance. It also replaces
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.DataSets"/> with a new collection of data sets matching the new type, copying the properties that
		/// the old and new data set classes have in common. <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bubble"/> and <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Scatter"/>
		/// charts use <see cref="T:Wisej.Web.Ext.ChartJS3.LineDataSet"/> data sets. Then the chart is redrawn.
		/// Set the chart type before configuring the options and the data sets, since references to the previous instances are no longer used by the chart.
		/// <para>
		/// Chart.js 3 has no horizontal bar type: <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.HorizontalBar"/> is rendered as a bar chart.
		/// Set <see cref="P:Wisej.Web.Ext.ChartJS3.Options.IndexAxis"/> to "y" to draw the bars horizontally.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.ChartType = ChartType.Doughnut;
		///
		/// // the options are now an instance of DoughnutOptions.
		/// var options = (DoughnutOptions)this.chartJS31.Options;
		/// options.Cutout = 60;
		/// ]]></code>
		/// </example>
		[DesignerActionList]
		[DefaultValue(ChartType.Line)]
		[RefreshProperties(RefreshProperties.All)]
		[Description("Returns or sets the chart type.")]
		public ChartType ChartType
		{
			get { return this._chartType; }
			set
			{
				if (this._chartType != value)
				{
					this._chartType = value;

					// change the options to match the chart type.
					this._options = CreateOptions();
					// change the data sets to match the chart type.
					this._dataSets = CreateDataSetCollection();

					Update();
				}
			}
		}
		private ChartType _chartType = ChartType.Line;

		/// <summary>
		/// Returns or sets the chart options specific for the value of <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/>.
		/// </summary>
		/// <value>
		/// An <see cref="T:Wisej.Web.Ext.ChartJS3.Options"/> instance. When the chart type is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Line"/>
		/// it is a <see cref="T:Wisej.Web.Ext.ChartJS3.LineOptions"/>, when it is <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.Bar"/> or
		/// <see cref="F:Wisej.Web.Ext.ChartJS3.ChartType.HorizontalBar"/> it is a <see cref="T:Wisej.Web.Ext.ChartJS3.BarOptions"/>, and so on.
		/// </value>
		/// <remarks>
		/// The options are created automatically when first read and are recreated when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> changes.
		/// They are sent to the client as the Chart.js options object, using camel case property names and omitting null values.
		/// On the client, the axes in <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScales.xAxes"/> and <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsScales.yAxes"/> are converted
		/// to the Chart.js 3 <c>scales</c> object using the ids "x0", "x1", ... and "y0", "y1", ..., and the options in
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/> are passed to the title, legend, tooltip and datalabels plugins.
		/// Assigning a new instance attaches it to this control and redraws the chart. The assigned instance should match the current
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/>.
		/// This property hides the inherited <see cref="P:Wisej.Web.Widget.Options"/> property.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The options instance already belongs to another <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.</exception>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.ChartType = ChartType.Line;
		/// this.chartJS31.Options.Plugins.Title.Display = true;
		/// this.chartJS31.Options.Plugins.Title.Text = "Monthly Visitors";
		/// this.chartJS31.Options.Plugins.Legend.Position = HeaderPosition.Bottom;
		///
		/// // or replace the whole set of options.
		/// this.chartJS31.Options = new LineOptions { Stacked = true };
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[MergableProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Chart options specific for the value of the ChartType property.")]
		[WisejSerializerOptions(WisejSerializerOptions.CamelCase | WisejSerializerOptions.IgnoreNulls)]
		public new Options Options
		{
			get
			{
				if (this._options == null)
					this._options = CreateOptions();

				return this._options;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Chart = this;
				this._options = value;

				Update();
			}
		}
		private Options _options = null;

		/// <summary>
		/// Returns the data sets to plot the chart.
		/// </summary>
		/// <value>
		/// A <see cref="T:Wisej.Web.Ext.ChartJS3.DataSetCollection"/> containing the <see cref="T:Wisej.Web.Ext.ChartJS3.DataSet"/> objects to plot.
		/// </value>
		/// <remarks>
		/// The collection is created automatically and is recreated when <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartType"/> changes.
		/// Use <see cref="M:Wisej.Web.Ext.ChartJS3.DataSetCollection.Add(System.String)"/> to create a data set of the type that matches the current chart type.
		/// Adding, removing or replacing data sets redraws the chart. To animate changes to the values of existing data sets,
		/// change their <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Data"/> and call <see cref="M:Wisej.Web.Ext.ChartJS3.ChartJS3.UpdateData(System.Int32)"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.DataSets.Clear();
		///
		/// var revenue = (LineDataSet)this.chartJS31.DataSets.Add("Revenue");
		/// revenue.Data = new object[] { 10, 22, 15, 30 };
		/// revenue.BorderColor = Color.SteelBlue;
		/// revenue.Fill = false;
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[Description("Returns or sets the data sets to plot the chart.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataSetCollection DataSets
		{
			get
			{
				if (this._dataSets == null)
					this._dataSets = CreateDataSetCollection();

				return this._dataSets;
			}
		}
		private DataSetCollection _dataSets = null;

		private bool ShouldSerializeDataSets()
		{
			return
				this._dataSets != null
				&& this._dataSets.Count > 0
				&& this._dataSets[0].Data != null
				&& this._dataSets[0].Data.Length > 0;
		}

		private void ResetDataSets()
		{
			this._dataSets = null;
			Update();
		}

		/// <summary>
		/// Returns or sets the labels for the data points.
		/// </summary>
		/// <value>
		/// An array of strings. The default is an empty array; setting it to null assigns an empty array.
		/// </value>
		/// <remarks>
		/// Each label corresponds to the value at the same index in the <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Data"/> array
		/// of each data set. Setting this property redraws the chart.
		/// At design time, when there are no data sets with data, the designer draws a sample data set with random values, one for each label.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Labels = new[] { "Q1", "Q2", "Q3", "Q4" };
		/// this.chartJS31.DataSets.Add("Profit").Data = new object[] { 4.2, 5.1, 3.8, 6.0 };
		/// ]]></code>
		/// </example>
		[DesignerActionList]
		[MergableProperty(false)]
		[TypeConverter(typeof(ArrayConverter))]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string[] Labels
		{
			get
			{
				return this._labels;
			}
			set
			{
				this._labels = value ?? new string[0];
				Update();
			}
		}
		private string[] _labels = new string[0];

		private bool ShouldSerializeLabels()
		{
			return this._labels != null && this._labels.Length > 0;
		}
		private void ResetLabels()
		{
			this.Labels = null;
		}

		/// <summary>
		/// Overridden to return the initialization script that creates the Chart.js chart on the client.
		/// </summary>
		/// <remarks>
		/// The script is loaded from the embedded resource "Wisej.Web.Ext.ChartJS3.JavaScript.startup.js". It registers the datalabels plugin,
		/// converts the options and data sets to the Chart.js 3 format and attaches the click handler that fires
		/// <see cref="E:Wisej.Web.Ext.ChartJS3.ChartJS3.ChartClick"/>. Assigning a value has no effect.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // a derived chart that adds a client-side function to the widget.
		/// public class MyChart : ChartJS3
		/// {
		///     public override string InitScript
		///     {
		///         get { return base.InitScript + "\r\nthis.refresh = function() { this.chart.update(); };"; }
		///         set { }
		///     }
		/// }
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			// disable inlining or we lose the calling assembly in GetResourceString().
			[MethodImpl(MethodImplOptions.NoInlining)]
			get { return GetResourceString("Wisej.Web.Ext.ChartJS3.JavaScript.startup.js"); }
			set { }
		}

		/// <summary>
		/// Overridden to return the list of script resources required by the chart.
		/// </summary>
		/// <remarks>
		/// The first time it is read, the list is filled with moment.js 2.29.4, Chart.js 3.5.0 and chartjs-plugin-datalabels,
		/// all loaded from the resources embedded in the extension assembly.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// foreach (var package in this.chartJS31.Packages)
		/// {
		///     System.Diagnostics.Debug.WriteLine(package.Name + ": " + package.Source);
		/// }
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override List<Package> Packages
		{
			// disable inlining or we lose the calling assembly in GetResourceString().
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (base.Packages.Count == 0)
				{
					// initialize the loader with the required libraries.
					base.Packages.AddRange(new[] {
						new Package()
						{
							Name = "moment.js",
							Source = GetResourceURL("Wisej.Web.Ext.ChartJS3.JavaScript.moment-with-locales-2.29.4.js")
						},
						new Package()
						{
							Name = "chart.js",
							Source = GetResourceURL("Wisej.Web.Ext.ChartJS3.JavaScript.chart-3.5.0.js")
						},
						new Package()
						{
							Name = "chartjs-plugin-datalabels.js",
							Source = GetResourceURL("Wisej.Web.Ext.ChartJS3.JavaScript.chartjs-plugin-datalabels.js")
						}
					});
				}

				return base.Packages;
			}
		}

		// If we don't have a dataset and we are in design mode, return
		// a dummy dataset to match the labels just to draw something in the designer.
		private IList<DataSet> DesignDataSets
		{
			get
			{
				// create or re-create the design data set. we'll create only 1 at index 0.
				if (this._designDataSets == null
					|| this._designDataSets[0].Data.Length != this.Labels.Length)
				{
					this._designDataSets = new DataSetCollection(this, null);
					DataSet dataSet = this._designDataSets.CreateDataSet("Sample DataSet");

					dataSet.Data = new object[this.Labels.Length];
					Random random = new Random();
					for (int i = 0; i < dataSet.Data.Length; i++)
						dataSet.Data[i] = random.Next(80);

					this._designDataSets.Add(dataSet);
				}
				return this._designDataSets;
			}
		}
		private DataSetCollection _designDataSets;


		#endregion

		#region Methods

		/// <summary>
		/// Returns the chart as a PNG image.
		/// </summary>
		/// <returns>A task that completes with an <see cref="T:System.Drawing.Image"/> with a representation of the chart, or null if the image could not be retrieved.</returns>
		/// <remarks>
		/// The image is rendered by the client canvas and sent back to the server asynchronously.
		/// The chart is drawn over the <see cref="P:Wisej.Web.Control.BackColor"/> of the control.
		/// The result is null when the chart has not been rendered yet or the image data cannot be decoded.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// private async void buttonSnapshot_Click(object sender, EventArgs e)
		/// {
		///     var image = await this.chartJS31.GetImageAsync();
		///     if (image != null)
		///         this.pictureBox1.Image = image;
		/// }
		/// ]]></code>
		/// </example>
		public async Task<Image> GetImageAsync()
		{
			var tcs = new TaskCompletionSource<Image>();

			GetImage((result) => { tcs.SetResult(result); });

			return await tcs.Task;
		}

		/// <summary>
		/// Retrieves the chart as a PNG image and passes it to the <paramref name="callback"/> method.
		/// </summary>
		/// <param name="callback">Callback method that receives the <see cref="T:System.Drawing.Image"/> with a representation of the chart, or null if the image could not be retrieved.</param>
		/// <remarks>
		/// The image is rendered by the client canvas and the callback is invoked when the client returns it.
		/// The chart is drawn over the <see cref="P:Wisej.Web.Control.BackColor"/> of the control.
		/// Use <see cref="M:Wisej.Web.Ext.ChartJS3.ChartJS3.GetImageAsync"/> to await the image instead.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="callback"/> is null.</exception>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.GetImage(image =>
		/// {
		///     if (image != null)
		///         image.Save(Application.MapPath("chart.png"), System.Drawing.Imaging.ImageFormat.Png);
		/// });
		/// ]]></code>
		/// </example>
		public void GetImage(Action<Image> callback)
		{
			if (callback == null)
				throw new ArgumentNullException("callback");

			Call("getImage", (result) =>
			{
				var image = ImageFromBase64(result as string);
				if (image != null)
				{
					// set the background color.
					var bitmap = new Bitmap(image);
					using (var g = Graphics.FromImage(bitmap))
					{
						g.Clear(this.BackColor);
						g.DrawImageUnscaled(image, 0, 0);
					}
					image = bitmap;
				}

				callback(image);

			}, null);
		}

		/// <summary>
		/// Returns the Image encoded in a base64 string.
		/// </summary>
		/// <param name="base64"></param>
		/// <returns></returns>
		private static Image ImageFromBase64(string base64)
		{
			// data:image/gif;base64,R0lGODlhCQAJAIABAAAAAAAAACH5BAEAAAEALAAAAAAJAAkAAAILjI+py+0NojxyhgIAOw==
			try
			{
				if (String.IsNullOrEmpty(base64))
					return null;

				int pos = base64.IndexOf("base64,");
				if (pos < 0)
					return null;

				base64 = base64.Substring(pos + 7);
				byte[] buffer = Convert.FromBase64String(base64);

				MemoryStream stream = new MemoryStream(buffer);
				return Image.FromStream(stream);
			}
			catch { }

			return null;
		}

		/// <summary>
		/// Causes the chart to update the data sets and labels.
		/// It performs a smooth animated transition from one data set to the new one.
		/// </summary>
		/// <param name="duration">Duration of the update animation in milliseconds, passed to the update call of the client chart. The default is 300 milliseconds.</param>
		/// <remarks>
		/// Only the <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Data"/> values of each data set and the <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.Labels"/>
		/// are updated on the client; other data set properties, including <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Formatted"/>, are not updated.
		/// The data sets must correspond to the ones already displayed: to add or remove data sets, change <see cref="P:Wisej.Web.Ext.ChartJS3.ChartJS3.DataSets"/>,
		/// which redraws the chart. If the control is already scheduled for a full redraw, this method does nothing since the redraw includes the new data.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// private void timer1_Tick(object sender, EventArgs e)
		/// {
		///     var random = new Random();
		///     var data = this.chartJS31.DataSets[0].Data;
		///     for (int i = 0; i < data.Length; i++)
		///         data[i] = random.Next(100);
		///
		///     this.chartJS31.UpdateData(500);
		/// }
		/// ]]></code>
		/// </example>
		public void UpdateData(int duration = 300)
		{
			// if the control is already scheduled for a full update, there is no
			// point in updating the dataset since it will be fully redrawn with the next update.
			IWisejControl me = this;
			if (me.IsDirty)
				return;

			Call("updateData", this.DataSets, this.Labels, duration);
		}

		// Creates a new set of options.
		// Tries to preserve the shared properties from
		// the base class.
		private Options CreateOptions()
		{
			switch (this.ChartType)
			{
				case ChartType.Line:
					return new LineOptions(this, this._options);
				case ChartType.Bar:
				case ChartType.HorizontalBar:
					return new BarOptions(this, this._options);
				case ChartType.Pie:
					return new PieOptions(this, this._options);
				case ChartType.PolarArea:
					return new PolarAreaOptions(this, this._options);
				case ChartType.Doughnut:
					return new DoughnutOptions(this, this._options);
				case ChartType.Radar:
					return new RadarOptions(this, this._options);
				case ChartType.Bubble:
					return new BubbleOptions(this, this._options);
				case ChartType.Scatter:
					return new ScatterOptions(this, this._options);

				default:
					throw new InvalidOperationException("Unknown chart type.");
			}
		}

		// Creates a new data set collection.
		// Tries to preserve the existing data sets.
		private DataSetCollection CreateDataSetCollection()
		{
			return new DataSetCollection(this, this._dataSets);
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Handles events fired by the widget.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			switch (e.Type)
			{
				case "chartClick":
					OnChartClick(new ChartClickEventArgs(this, e));
					break;

				default:
					base.OnWidgetEvent(e);
					break;
			}
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.Options = new
			{
				type = this.ChartType,
				options = this.Options,
				data = new
				{
					labels = this.Labels,
					datasets = ShouldSerializeDataSets()
						? this.DataSets
						: this.DesignMode
							? this.DesignDataSets
							: null
				}
			};

			base.OnWebRender((object)config);
		}

		#endregion
	}
}
