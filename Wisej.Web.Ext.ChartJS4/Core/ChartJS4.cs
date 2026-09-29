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
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Wisej.Core;
using Wisej.Web.Ext.ChartJS4.Models;
using Wisej.Web.Ext.ChartJS4.Serialization;

namespace Wisej.Web.Ext.ChartJS4
{
	/// <summary>
	/// ChartJS4 is a modernized, flexible Chart.js 4.x integration for Wisej.NET.
	/// Features improved serialization, better maintainability, and easier customization.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The chart is configured on the server through <see cref="ChartType"/>, <see cref="Labels"/>,
	/// <see cref="DataSets"/> and <see cref="ChartOptions"/>. Changing any of these properties (or the
	/// content of the observable <see cref="LabelCollection"/> and <see cref="DataSetCollection"/>)
	/// schedules a refresh of the client-side chart.
	/// </para>
	/// <para>
	/// Use <see cref="UpdateData"/> to update only the data and labels with an animation, and the
	/// client-call methods (e.g. <see cref="GetImage"/>, <see cref="ToBase64Image(Action{string})"/>,
	/// <see cref="IsDatasetVisible"/>) to invoke the corresponding Chart.js API on the client.
	/// Methods that return a value from the client are asynchronous and deliver the result to a callback.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4 { Dock = DockStyle.Fill, ChartType = ChartType.Bar };
	/// chart.Labels = new[] { "Jan", "Feb", "Mar" };
	/// chart.DataSets.Add(new BarDataSet { Label = "Sales", Data = new object[] { 10, 20, 15 } });
	/// chart.ChartOptions.Plugins.Title.Display = true;
	/// chart.ChartOptions.Plugins.Title.Text = "Quarterly Sales";
	/// this.Controls.Add(chart);
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[DefaultEvent("ChartClick")]
	[Description("Modern Chart.js 4.x integration with improved serialization and maintainability.")]
	public class ChartJS4 : Widget, IWisejControl
	{
		private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
			WriteIndented = true,
			Converters = { new ColorJsonConverter(), new ChartDataSetConverter(), new ChartNaNJsonConverter() },
			Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
		};

		/// <summary>
		/// Constructs a new instance of the <see cref="ChartJS4"/> control.
		/// </summary>
		/// <remarks>
		/// The new control has an empty <see cref="Labels"/> and <see cref="DataSets"/> collection bound to it and
		/// a <see cref="ChartType"/> of <see cref="Wisej.Web.Ext.ChartJS4.ChartType.Line"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4 { Dock = DockStyle.Fill };
		/// this.Controls.Add(chart);
		/// ]]></code>
		/// </example>
		public ChartJS4()
		{
			_labels = new LabelCollection(this);
			_dataSets = new DataSetCollection(this);
		}

		#region Events

		/// <summary>
		/// Fired when the user clicks a data point on the chart.
		/// </summary>
		/// <remarks>
		/// The event is fired only at runtime and only when the click hits at least one chart element.
		/// <see cref="ChartClickEventArgs.Data"/> contains a <c>data</c> array with one entry per
		/// element under the pointer, each with the <c>pointIndex</c> and <c>dataSetIndex</c> fields.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartClick += (s, e) =>
		/// {
		///     var point = e.Data.data[0];
		///     AlertBox.Show($"DataSet {point.dataSetIndex}, point {point.pointIndex}");
		/// };
		/// ]]></code>
		/// </example>
		[Description("Fired when the user clicks a data point on the chart.")]
		public event ChartClickEventHandler? ChartClick
		{
			add { base.Events.AddHandler(nameof(ChartClick), value); }
			remove { base.Events.RemoveHandler(nameof(ChartClick), value); }
		}

		/// <summary>
		/// Fires the <see cref="ChartClick"/> event.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected virtual void OnChartClick(ChartClickEventArgs e)
		{
			((ChartClickEventHandler?)base.Events[nameof(ChartClick)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the <see cref="ChartJS4.ChartType"/>.
		/// </summary>
		/// <value>
		/// One of the <see cref="Wisej.Web.Ext.ChartJS4.ChartType"/> values. The default is <see cref="Wisej.Web.Ext.ChartJS4.ChartType.Line"/>.
		/// </value>
		/// <remarks>
		/// The value is sent to Chart.js as the lower-case <c>type</c> of the chart configuration,
		/// unless <see cref="Models.ChartOptions.Type"/> is set, in which case that value takes precedence.
		/// Changing the value refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartType = ChartType.Doughnut;
		/// ]]></code>
		/// </example>
		[DefaultValue(ChartType.Line)]
		[RefreshProperties(RefreshProperties.All)]
		[Description("Returns or sets the chart type.")]
		public ChartType ChartType
		{
			get { return _chartType; }
			set
			{
				if (_chartType != value)
				{
					_chartType = value;
					Update();
				}
			}
		}
		private ChartType _chartType = ChartType.Line;

		/// <summary>
		/// Determines whether the <see cref="ChartType"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="ChartType"/> differs from <see cref="Wisej.Web.Ext.ChartJS4.ChartType.Line"/>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the designer and the code serializer.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (chart.ShouldSerializeChartType())
		///     chart.ResetChartType();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeChartType() => _chartType != ChartType.Line;

		/// <summary>
		/// Resets the <see cref="ChartType"/> property to its default value (<see cref="Wisej.Web.Ext.ChartJS4.ChartType.Line"/>).
		/// </summary>
		/// <remarks>
		/// Used by the designer. This method assigns the backing field directly and does not refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ResetChartType();
		/// ]]></code>
		/// </example>
		public void ResetChartType() => _chartType = ChartType.Line;

		/// <summary>
		/// Returns or sets the labels for the chart data (Chart.js <c>data.labels</c>).
		/// </summary>
		/// <value>
		/// A <see cref="LabelCollection"/>. Assigning <c>null</c> replaces it with a new empty collection.
		/// </value>
		/// <remarks>
		/// <see cref="LabelCollection"/> supports implicit conversion from <c>string[]</c> and
		/// <c>List&lt;string&gt;</c>. Assigning the property, or adding, removing or changing labels,
		/// refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.Labels = new[] { "Mon", "Tue", "Wed" };
		/// chart.Labels.Add("Thu");
		/// ]]></code>
		/// </example>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets or sets the labels for the chart data.")]
		public LabelCollection Labels
		{
			get { return _labels; }
			set
			{
				_labels = value ?? new LabelCollection();
				_labels.Chart = this;
				Update();
			}
		}
		private LabelCollection _labels;

		/// <summary>
		/// Returns or sets the data sets for the chart (Chart.js <c>data.datasets</c>).
		/// </summary>
		/// <value>
		/// A <see cref="DataSetCollection"/> of <see cref="ChartDataSet"/> objects (e.g. <see cref="LineDataSet"/>,
		/// <see cref="BarDataSet"/>, <see cref="PieDataSet"/>). Assigning <c>null</c> replaces it with a new empty collection.
		/// </value>
		/// <remarks>
		/// <see cref="DataSetCollection"/> supports implicit conversion from <c>ChartDataSet[]</c> and
		/// <c>List&lt;ChartDataSet&gt;</c>. Assigning the property, or adding, removing or replacing data sets,
		/// refreshes the chart. Use <see cref="UpdateData"/> to push data changes with an animation.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.DataSets.Add(new LineDataSet
		/// {
		///     Label = "Visitors",
		///     Data = new object[] { 12, 19, 3, 5 },
		///     BorderColor = Color.SteelBlue
		/// });
		/// ]]></code>
		/// </example>
		[Description("Gets or sets the data sets for the chart.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataSetCollection DataSets
		{
			get { return _dataSets; }
			set
			{
				_dataSets = value ?? new DataSetCollection();
				_dataSets.Chart = this;
				Update();
			}
		}
		private DataSetCollection _dataSets;

		/// <summary>
		/// Returns or sets the chart options (Chart.js <c>options</c>).
		/// </summary>
		/// <value>
		/// A <see cref="Models.ChartOptions"/> instance. The getter lazily creates a new instance when none is set.
		/// </value>
		/// <remarks>
		/// Nested option objects (e.g. <c>Plugins</c>, <c>Scales</c>) are also created lazily on first access,
		/// so they can be configured directly. When rendered, the options are serialized to JSON and properties that
		/// match their default values, empty objects and <c>null</c> values are removed. Assigning the property refreshes the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Legend.Position = "bottom";
		/// chart.ChartOptions.Plugins.Title.Display = true;
		/// chart.ChartOptions.Plugins.Title.Text = "Monthly Revenue";
		/// ]]></code>
		/// </example>
		[Description("Gets or sets the chart options.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new ChartOptions ChartOptions
		{
			get
			{
				if (_options == null)
					_options = new ChartOptions { Chart = this };
				return _options;
			}
			set
			{
				_options = value;
				if (_options != null)
					_options.Chart = this;
				Update();
			}
		}
		private ChartOptions? _options;

		#endregion

		#region Methods

		/// <summary>
		/// Asynchronously returns the chart as a PNG image.
		/// </summary>
		/// <returns>
		/// A task that completes with an <see cref="Image"/> of the chart rendered over the control's
		/// <see cref="Control.BackColor"/>, or <c>null</c> if the client could not produce an image.
		/// </returns>
		/// <remarks>
		/// This is the awaitable version of <see cref="GetImage"/>. The image is produced by the browser
		/// from the chart's canvas.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// private async void buttonSave_Click(object sender, EventArgs e)
		/// {
		///     var image = await this.chartJS41.GetImageAsync();
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
		/// Retrieves the chart as a PNG image and passes it to the specified callback.
		/// </summary>
		/// <param name="callback">Callback method that receives the <see cref="Image"/> of the chart,
		/// rendered over the control's <see cref="Control.BackColor"/>, or <c>null</c> if the client could not produce an image.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <c>null</c>.</exception>
		/// <remarks>
		/// The image is produced asynchronously by the browser from the chart's canvas.
		/// See <see cref="GetImageAsync"/> for the awaitable version.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.GetImage(image =>
		/// {
		///     if (image != null)
		///         image.Save(Application.MapPath("chart.png"));
		/// });
		/// ]]></code>
		/// </example>
		public void GetImage(Action<Image> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Call("getImage", (result) =>
			{
				var image = ImageFromBase64(result as string);
				if (image != null)
				{
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
		/// Converts a base64 string to an Image.
		/// </summary>
		private static Image? ImageFromBase64(string? base64)
		{
			try
			{
				if (string.IsNullOrEmpty(base64))
					return null;

				int pos = base64.IndexOf("base64,");
				if (pos < 0)
					return null;

				base64 = base64.Substring(pos + 7);
				byte[] buffer = Convert.FromBase64String(base64);
				MemoryStream stream = new MemoryStream(buffer);
				return Image.FromStream(stream);
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Causes the chart to update the data set and labels with animation.
		/// </summary>
		/// <param name="duration">Duration of the update animation in milliseconds. The default is 300.</param>
		/// <remarks>
		/// Sends the current <see cref="DataSets"/> and <see cref="Labels"/> to the client and updates the
		/// existing Chart.js data in place, allowing smooth transitions. The call is skipped when the control
		/// is already scheduled for a full refresh.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.DataSets[0].Data = new object[] { 5, 8, 13, 21 };
		/// chart.UpdateData(500);
		/// ]]></code>
		/// </example>
		public void UpdateData(int duration = 300)
		{
			IWisejControl me = this;
			if (me.IsDirty)
				return;

			Call("updateData", SerializeDataSets(), this.Labels, duration);
		}

		/// <summary>
		/// Triggers an update of the chart. This will update all scales, legends, and re-render the chart.
		/// </summary>
		/// <param name="mode">The update mode. Can be <c>"none"</c>, <c>"resize"</c>, <c>"reset"</c>, <c>"hide"</c>,
		/// <c>"show"</c>, <c>"normal"</c> or <c>"active"</c>. When <c>null</c> or empty, the default Chart.js update is performed.</param>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.update(mode)</c> method on the client. Use <c>"none"</c> to update without animation.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.UpdateChart();
		/// chart.UpdateChart("none");
		/// ]]></code>
		/// </example>
		public void UpdateChart(string? mode = null)
		{
			if (string.IsNullOrEmpty(mode))
				Call("updateChart");
			else
				Call("updateChart", mode);
		}

		/// <summary>
		/// Destroys the chart instance, cleaning up any references and event listeners.
		/// </summary>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.destroy()</c> method on the client. The control itself is not disposed;
		/// the chart is re-created the next time the control is refreshed (e.g. by calling <c>Update()</c>).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.Destroy();
		/// ]]></code>
		/// </example>
		public void Destroy()
		{
			Call("destroy");
		}

		/// <summary>
		/// Resets the chart to its state before the initial animation.
		/// </summary>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.reset()</c> method on the client. A subsequent
		/// <see cref="UpdateChart"/> call runs the initial animation again.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.Reset();
		/// chart.UpdateChart();
		/// ]]></code>
		/// </example>
		public void Reset()
		{
			Call("reset");
		}

		/// <summary>
		/// Triggers a redraw of all chart elements.
		/// </summary>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.render()</c> method on the client. Unlike <see cref="UpdateChart"/>,
		/// it does not update elements with new data; use it to redraw after changes that don't affect the data.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.Render();
		/// ]]></code>
		/// </example>
		public void Render()
		{
			Call("render");
		}

		/// <summary>
		/// Stops all currently running animations on the chart.
		/// </summary>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.stop()</c> method on the client.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.Stop();
		/// ]]></code>
		/// </example>
		public void Stop()
		{
			Call("stop");
		}

		/// <summary>
		/// Resizes the chart canvas. If no dimensions are provided, detects the new size from the container.
		/// </summary>
		/// <param name="width">Optional width in pixels.</param>
		/// <param name="height">Optional height in pixels.</param>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.resize()</c> method on the client. Both <paramref name="width"/> and
		/// <paramref name="height"/> must be specified to set an explicit size; if either is omitted the size
		/// is detected from the container.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.Resize();
		/// chart.Resize(640, 480);
		/// ]]></code>
		/// </example>
		public void Resize(int? width = null, int? height = null)
		{
			if (width.HasValue && height.HasValue)
				Call("resize", width.Value, height.Value);
			else
				Call("resize");
		}

		/// <summary>
		/// Clears the chart canvas.
		/// </summary>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.clear()</c> method on the client. The chart is drawn again on the next
		/// update or render.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.Clear();
		/// ]]></code>
		/// </example>
		public void Clear()
		{
			Call("clear");
		}

		/// <summary>
		/// Returns a base64 encoded string of the chart in the requested format.
		/// </summary>
		/// <param name="type">Image MIME type (e.g., <c>"image/png"</c>, <c>"image/jpeg"</c>, <c>"image/webp"</c>).</param>
		/// <param name="quality">Quality for lossy formats (0.0 to 1.0).</param>
		/// <param name="callback">Callback that receives the image as a data URL
		/// (<c>data:&lt;type&gt;;base64,…</c>), or an empty string if the chart is not available.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <c>null</c>.</exception>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.toBase64Image(type, quality)</c> method on the client.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ToBase64Image("image/jpeg", 0.8, dataUrl =>
		/// {
		///     this.pictureBox1.ImageSource = dataUrl;
		/// });
		/// ]]></code>
		/// </example>
		public void ToBase64Image(string type, double quality, Action<string> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Call("toBase64Image", (Action<dynamic>)((result) =>
			{
				callback(result as string ?? string.Empty);
			}), type, quality);
		}

		/// <summary>
		/// Returns a base64 encoded string of the chart in PNG format.
		/// </summary>
		/// <param name="callback">Callback that receives the PNG image as a data URL
		/// (<c>data:image/png;base64,…</c>), or an empty string if the chart is not available.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <c>null</c>.</exception>
		/// <remarks>
		/// Equivalent to calling <see cref="ToBase64Image(string, double, Action{string})"/> with
		/// <c>"image/png"</c> and a quality of <c>1.0</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ToBase64Image(dataUrl =>
		/// {
		///     this.pictureBox1.ImageSource = dataUrl;
		/// });
		/// ]]></code>
		/// </example>
		public void ToBase64Image(Action<string> callback)
		{
			ToBase64Image("image/png", 1.0, callback);
		}

		/// <summary>
		/// Generates an HTML legend for the chart.
		/// </summary>
		/// <param name="callback">Callback that receives the HTML string, or an empty string if the chart is not available.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <c>null</c>.</exception>
		/// <remarks>
		/// Invokes <c>chart.generateLegend()</c> on the client. Note that Chart.js 3 and later no longer
		/// provide this method natively; it works only when it has been added to the chart (for example by a plugin),
		/// otherwise the client call fails. For custom HTML legends in Chart.js 4 consider an HTML legend plugin.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.GenerateLegend(html =>
		/// {
		///     this.htmlPanel1.Html = html;
		/// });
		/// ]]></code>
		/// </example>
		public void GenerateLegend(Action<string> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Call("generateLegend", (Action<dynamic>)((result) =>
			{
				callback(result as string ?? string.Empty);
			}), null);
		}

		/// <summary>
		/// Retrieves the number of visible datasets.
		/// </summary>
		/// <param name="callback">Callback that receives the count of visible datasets.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <c>null</c>.</exception>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.getVisibleDatasetCount()</c> method on the client.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.GetVisibleDatasetCount(count =>
		/// {
		///     this.labelInfo.Text = $"{count} of {chart.DataSets.Count} datasets visible";
		/// });
		/// ]]></code>
		/// </example>
		public void GetVisibleDatasetCount(Action<int> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Call("getVisibleDatasetCount", (Action<dynamic>)((result) =>
			{
				callback(Convert.ToInt32(result));
			}), null);
		}

		/// <summary>
		/// Checks if a dataset is visible.
		/// </summary>
		/// <param name="datasetIndex">Index of the dataset.</param>
		/// <param name="callback">Callback that receives <c>true</c> if the dataset is visible; otherwise <c>false</c>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <c>null</c>.</exception>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.isDatasetVisible(datasetIndex)</c> method on the client.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.IsDatasetVisible(0, visible =>
		/// {
		///     chart.SetDatasetVisibility(0, !visible);
		///     chart.UpdateChart();
		/// });
		/// ]]></code>
		/// </example>
		public void IsDatasetVisible(int datasetIndex, Action<bool> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Call("isDatasetVisible", (Action<dynamic>)((result) =>
			{
				callback(Convert.ToBoolean(result));
			}), datasetIndex);
		}

		/// <summary>
		/// Sets the visibility of a dataset.
		/// </summary>
		/// <param name="datasetIndex">Index of the dataset.</param>
		/// <param name="visible"><c>true</c> to show, <c>false</c> to hide.</param>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.setDatasetVisibility(datasetIndex, visible)</c> method on the client.
		/// The change is not drawn until the chart is updated, e.g. with <see cref="UpdateChart"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.SetDatasetVisibility(1, false);
		/// chart.UpdateChart();
		/// ]]></code>
		/// </example>
		public void SetDatasetVisibility(int datasetIndex, bool visible)
		{
			Call("setDatasetVisibility", datasetIndex, visible);
		}

		/// <summary>
		/// Toggles the visibility of data at the specified index across all datasets.
		/// </summary>
		/// <param name="index">Index of the data.</param>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.toggleDataVisibility(index)</c> method on the client. This is mostly
		/// useful for charts where each data item has its own legend entry, such as pie, doughnut and polar area charts.
		/// The change is not drawn until the chart is updated, e.g. with <see cref="UpdateChart"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ToggleDataVisibility(2);
		/// chart.UpdateChart();
		/// ]]></code>
		/// </example>
		public void ToggleDataVisibility(int index)
		{
			Call("toggleDataVisibility", index);
		}

		/// <summary>
		/// Retrieves the visibility state of data at the specified index.
		/// </summary>
		/// <param name="index">Index of the data.</param>
		/// <param name="callback">Callback that receives <c>true</c> if the data at <paramref name="index"/> is visible; otherwise <c>false</c>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <c>null</c>.</exception>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.getDataVisibility(index)</c> method on the client.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.GetDataVisibility(2, isVisible =>
		/// {
		///     AlertBox.Show(isVisible ? "Visible" : "Hidden");
		/// });
		/// ]]></code>
		/// </example>
		public void GetDataVisibility(int index, Action<bool> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Call("getDataVisibility", (Action<dynamic>)((result) =>
			{
				callback(Convert.ToBoolean(result));
			}), index);
		}

		/// <summary>
		/// Hides a dataset and triggers the 'hide' animation.
		/// </summary>
		/// <param name="datasetIndex">Index of the dataset to hide.</param>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.hide(datasetIndex)</c> method on the client. The chart is updated
		/// automatically; no additional update call is needed.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.Hide(0);
		/// ]]></code>
		/// </example>
		public void Hide(int datasetIndex)
		{
			Call("hide", datasetIndex);
		}

		/// <summary>
		/// Hides a specific data element in a dataset and triggers the 'hide' animation.
		/// </summary>
		/// <param name="datasetIndex">Index of the dataset.</param>
		/// <param name="dataIndex">Index of the data element.</param>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.hide(datasetIndex, dataIndex)</c> method on the client.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // hide the third slice of a pie chart.
		/// chart.Hide(0, 2);
		/// ]]></code>
		/// </example>
		public void Hide(int datasetIndex, int dataIndex)
		{
			Call("hide", datasetIndex, dataIndex);
		}

		/// <summary>
		/// Shows a dataset and triggers the 'show' animation.
		/// </summary>
		/// <param name="datasetIndex">Index of the dataset to show.</param>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.show(datasetIndex)</c> method on the client. The chart is updated
		/// automatically; no additional update call is needed.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.Show(0);
		/// ]]></code>
		/// </example>
		public void Show(int datasetIndex)
		{
			Call("show", datasetIndex);
		}

		/// <summary>
		/// Shows a specific data element in a dataset and triggers the 'show' animation.
		/// </summary>
		/// <param name="datasetIndex">Index of the dataset.</param>
		/// <param name="dataIndex">Index of the data element.</param>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.show(datasetIndex, dataIndex)</c> method on the client.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.Show(0, 2);
		/// ]]></code>
		/// </example>
		public void Show(int datasetIndex, int dataIndex)
		{
			Call("show", datasetIndex, dataIndex);
		}

		/// <summary>
		/// Sets the active (hovered) elements for the chart.
		/// </summary>
		/// <param name="activeElements">Array of active element specifications. Each item is an object with the
		/// <c>datasetIndex</c> and <c>index</c> fields. An empty array clears the active elements.</param>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.setActiveElements()</c> method on the client. This sets the hover state
		/// of the elements; to also show the tooltip use the Chart.js tooltip API on the client.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.SetActiveElements(new object[]
		/// {
		///     new { datasetIndex = 0, index = 1 },
		///     new { datasetIndex = 1, index = 1 }
		/// });
		/// ]]></code>
		/// </example>
		public void SetActiveElements(object[] activeElements)
		{
			Call("setActiveElements", activeElements.ToList());
		}

		/// <summary>
		/// Retrieves the currently active (hovered) elements.
		/// </summary>
		/// <param name="callback">Callback that receives the array of active elements, or an empty array when
		/// there are none or the result could not be read.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is <c>null</c>.</exception>
		/// <remarks>
		/// Invokes the Chart.js <c>chart.getActiveElements()</c> method on the client. Each element is a dynamic
		/// object that typically exposes the <c>datasetIndex</c> and <c>index</c> fields.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.GetActiveElements(elements =>
		/// {
		///     this.labelInfo.Text = $"{elements.Length} active element(s)";
		/// });
		/// ]]></code>
		/// </example>
		public void GetActiveElements(Action<object[]> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			Call("getActiveElements", (Action<dynamic>)((result) =>
			{
				if (result is object[] arr)
					callback(arr);
				else
					callback(Array.Empty<object>());
			}), null);
		}

		/// <summary>
		/// Serializes the data sets to a format suitable for Chart.js.
		/// </summary>
		private object SerializeDataSets()
		{
			return WisejSerializer.Parse(
				JsonSerializer.Serialize(DataSets, _jsonOptions));
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Handles events fired by the widget.
		/// </summary>
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
		/// Returns the list of additional plugin packages to load with the chart.
		/// Use this to register custom Chart.js plugins globally (e.g., in Application_Start).
		/// </summary>
		/// <value>
		/// A static, application-wide list of <see cref="Package"/> objects. Empty by default.
		/// </value>
		/// <remarks>
		/// <para>
		/// The packages are appended to <see cref="Packages"/> after the built-in scripts (Chart.js,
		/// chartjs-plugin-datalabels, the date-fns adapter and moment.js) and after the scripts discovered
		/// automatically from embedded resources placed in a <c>ChartJsPlugins</c> folder of any loaded assembly.
		/// </para>
		/// <para>
		/// Because <see cref="Packages"/> is built the first time it is read, register plugin packages before
		/// the first chart is created.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// ChartJS4.PluginPackages.Add(new Package
		/// {
		///     Name = "chartjs-plugin-zoom.js",
		///     Source = "https://cdn.jsdelivr.net/npm/chartjs-plugin-zoom@2/dist/chartjs-plugin-zoom.min.js"
		/// });
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		public static List<Package> PluginPackages { get; } = new List<Package>();

		/// <summary>
		/// Returns or sets the widget functions that are passed to the chart.
		/// Widget functions allow JavaScript callbacks (e.g., scriptable options) to be defined
		/// server-side and called client-side using the <c>(ctx)=&gt;functionName</c> pattern.
		/// </summary>
		/// <value>
		/// An array of <see cref="WidgetFunction"/> objects, or <c>null</c> (the default).
		/// </value>
		/// <remarks>
		/// <para>
		/// Any string option value in <see cref="ChartOptions"/> or <see cref="DataSets"/> matching
		/// <c>(args)=&gt;Name</c> is replaced on the client by a function whose parameters are <c>args</c> and whose
		/// body is the <see cref="WidgetFunction.Source"/> of the function with the same <see cref="WidgetFunction.Name"/>.
		/// The form <c>(args)=&gt;Name(values)</c> instead invokes the function once and assigns the returned value.
		/// </para>
		/// <para>
		/// Each function is also registered on the client widget under its name.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.WidgetFunctions = new[]
		/// {
		///     new ChartJS4.WidgetFunction
		///     {
		///         Name = "barColor",
		///         Source = "return ctx.raw > 10 ? 'green' : 'red';"
		///     }
		/// };
		/// chart.DataSets.Add(new BarDataSet { Data = new object[] { 5, 15, 8 }, BackgroundColor = "(ctx)=>barColor" });
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WidgetFunction[]? WidgetFunctions { get; set; }

		/// <summary>
		/// Returns the list of script packages required by the chart.
		/// </summary>
		/// <value>
		/// The list of <see cref="Package"/> objects loaded on the client before the chart widget is created.
		/// </value>
		/// <remarks>
		/// <para>
		/// The list is built on first access and contains, in order:
		/// </para>
		/// <list type="number">
		/// <item><description>The embedded Chart.js 4 library, chartjs-plugin-datalabels, the chartjs-adapter-date-fns bundle and moment.js.</description></item>
		/// <item><description>Every embedded resource found in a <c>ChartJsPlugins</c> folder of any loaded assembly (other than this one).</description></item>
		/// <item><description>The packages registered in <see cref="PluginPackages"/>.</description></item>
		/// </list>
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// foreach (var package in chart.Packages)
		///     System.Diagnostics.Debug.WriteLine($"{package.Name}: {package.Source}");
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override List<Package> Packages
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				if (base.Packages.Count == 0)
				{
					base.Packages.AddRange(new[]
					{
						new Package
						{
							Name = "chart.js",
							Source = GetResourceURL("Wisej.Web.Ext.ChartJS4.JavaScript.chart.min.js")
						},
						new Package
						{
							Name = "chartjs-plugin-datalabels.js",
							Source = GetResourceURL("Wisej.Web.Ext.ChartJS4.JavaScript.chartjs-plugin-datalabels.js")
						},
						new Package
						{
							Name = "chartjs-adapter-date-fns.js",
							Source = GetResourceURL("Wisej.Web.Ext.ChartJS4.JavaScript.chartjs-adapter-date-fns-3.0.0.bundle.min.js")
						},
						new Package
						{
							Name = "moment.js",
							Source = GetResourceURL("Wisej.Web.Ext.ChartJS4.JavaScript.moment-with-locales-2.29.4.js")
						},
					});

					// Auto-discover embedded resources from "ChartJsPlugins" folders in loaded assemblies.
					base.Packages.AddRange(DiscoverPluginPackages());

					if (PluginPackages?.Count > 0)
						base.Packages.AddRange(PluginPackages);
				}

				return base.Packages;
			}
		}

		/// <summary>
		/// Returns the JavaScript initialization script of the chart widget.
		/// </summary>
		/// <value>
		/// The content of the embedded <c>startup.js</c> resource, which creates the Chart.js instance on the client
		/// and implements the client-side methods invoked by this control.
		/// </value>
		/// <remarks>
		/// The setter is ignored: the initialization script is always loaded from the embedded resource.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var script = chart.InitScript;
		/// System.Diagnostics.Debug.WriteLine(script.Length);
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		public override string InitScript
		{
			// disable inlining or we lose the calling assembly in GetResourceString().
			[MethodImpl(MethodImplOptions.NoInlining)]
			get { return GetResourceString("Wisej.Web.Ext.ChartJS4.JavaScript.startup.js"); }
			set { }
		}

        /// <summary>
        /// Returns or sets the raw configuration object sent to the client widget.
        /// </summary>
        /// <value>
        /// A dynamic object with the <c>type</c>, <c>options</c>, <c>widgetFunctions</c> and <c>data</c> fields
        /// passed to the Chart.js constructor.
        /// </value>
        /// <remarks>
        /// This property is hidden from the designer. It is regenerated every time the control renders from
        /// <see cref="ChartType"/>, <see cref="ChartOptions"/>, <see cref="WidgetFunctions"/>, <see cref="Labels"/>
        /// and <see cref="DataSets"/>, so values assigned directly are overwritten. Use <see cref="ChartOptions"/> to
        /// configure the chart instead.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// // Configure the chart through ChartOptions rather than Options.
        /// chart.ChartOptions.Plugins.Legend.Position = "bottom";
        /// chart.Update();
        /// ]]></code>
        /// </example>
        [Browsable(false)]
        public override dynamic Options { get => base.Options; set => base.Options = value; }

		/// <summary>
		/// Renders the client component.
		/// </summary>
		protected override void OnWebRender(dynamic config)
		{
			// Serialize ChartOptions to JsonDocument, then convert to plain object structure
			// This avoids the "ValueKind: Object" issue with ExpandoObject deserialization
			var optionsJson = JsonSerializer.SerializeToDocument(ChartOptions, _jsonOptions);
			var optionsObject = JsonDocumentConverter.ConvertToObject(optionsJson);
			
			// Remove properties that match their DefaultValue attributes
			// System.Text.Json only removes CLR default values, not custom [DefaultValue] attributes
			optionsObject = JsonDocumentConverter.RemoveDefaultValues(optionsObject, ChartOptions?.GetType());
			
			// Remove empty objects and null values to reduce payload size
			optionsObject = JsonDocumentConverter.RemoveEmptyObjects(optionsObject);

			base.Options = new
			{
				type = ChartOptions?.Type ?? ChartType.ToString().ToLowerInvariant(),
				options = optionsObject,
				widgetFunctions = WidgetFunctions,
					
				data = new
				{
					labels = this.DesignMode ? new string[7] {"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"} :  Labels,
					datasets = this.DesignMode ? GeneratePreviewData() : SerializeDataSets()
				}
			};

			base.OnWebRender((object)config);
		}

		private object[] GeneratePreviewData()
		{
			return new[]
			{
				new
				{
					label = "Sample Dataset",
					data = new[] { 10, 20, 15, 25, 18, 30, 22 },
					backgroundColor = "rgba(75, 192, 192, 0.2)",
					borderColor = "rgba(75, 192, 192, 1)",
					borderWidth = 1
				}
			};
        }

        #endregion

        /// <summary>
        /// Defines a named JavaScript function that can be referenced in chart options
        /// using the <c>(ctx)=&gt;functionName</c> pattern for scriptable options.
        /// </summary>
        /// <remarks>
        /// Assign instances to <see cref="ChartJS4.WidgetFunctions"/>. On the client, an option value such as
        /// <c>"(ctx)=&gt;myFunction"</c> is replaced by a function that declares the parameters listed in the
        /// parentheses and uses <see cref="Source"/> as its body.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// var alternate = new ChartJS4.WidgetFunction
        /// {
        ///     Name = "alternateColor",
        ///     Source = "return ctx.dataIndex % 2 === 0 ? 'red' : 'blue';"
        /// };
        /// chart.WidgetFunctions = new[] { alternate };
        /// chart.DataSets[0].BackgroundColor = "(ctx)=>alternateColor";
        /// ]]></code>
        /// </example>
        public class WidgetFunction
		{
			/// <summary>
			/// Returns or sets the name of the function (referenced in chart options as <c>(ctx)=&gt;Name</c>).
			/// </summary>
			/// <value>The function name. The default is an empty string.</value>
			/// <remarks>
			/// The name must not contain white space or parentheses. The function is also registered on the client
			/// widget under this name; a warning is logged if it overrides an existing member.
			/// </remarks>
			/// <example>
			/// <code><![CDATA[
			/// var fn = new ChartJS4.WidgetFunction { Name = "borderColor", Source = "return ctx.raw < 0 ? 'red' : 'green';" };
			/// chart.WidgetFunctions = new[] { fn };
			/// chart.DataSets[0].BorderColor = "(ctx)=>borderColor";
			/// ]]></code>
			/// </example>
			public string Name { get; set; } = string.Empty;

			/// <summary>
			/// Returns or sets the JavaScript source code of the function body.
			/// </summary>
			/// <value>The body of the JavaScript function. The default is an empty string.</value>
			/// <remarks>
			/// The source is used as the body of a function created with the JavaScript <c>Function</c> constructor,
			/// so it must not include the <c>function(…) { }</c> declaration. The parameter names are the ones declared
			/// in the referencing option value, e.g. <c>ctx</c> for <c>"(ctx)=&gt;Name"</c>; they are also available
			/// through <c>arguments</c>. Use <c>return</c> to return the value to Chart.js.
			/// </remarks>
			/// <example>
			/// <code><![CDATA[
			/// var fn = new ChartJS4.WidgetFunction
			/// {
			///     Name = "alternateColor",
			///     Source = "return ctx.dataIndex % 2 === 0 ? 'red' : 'blue';"
			/// };
			/// ]]></code>
			/// </example>
			public string Source { get; set; } = string.Empty;
		}

		/// <summary>
		/// Scans all loaded assemblies for embedded resources located in a virtual folder
		/// named <c>ChartJsPlugins</c> (i.e. resource names containing <c>.ChartJsPlugins.</c>)
		/// and returns a <see cref="Package"/> for each discovered script file.
		/// </summary>
		/// <remarks>
		/// To register a Chart.js plugin, add the JavaScript file to a folder called
		/// <c>ChartJsPlugins</c> in your project and set its <c>Build Action</c> to
		/// <c>Embedded Resource</c>. The file will be discovered and loaded automatically.
		/// </remarks>
		private IEnumerable<Package> DiscoverPluginPackages()
		{
			const string folderMarker = ".ChartJsPlugins.";
			var thisAssembly = typeof(ChartJS4).Assembly;

			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (assembly == thisAssembly || assembly.IsDynamic)
					continue;

				string[] resourceNames;
				try
				{
					resourceNames = assembly.GetManifestResourceNames();
				}
				catch
				{
					continue;
				}

				foreach (var resourceName in resourceNames)
				{
					if (resourceName.IndexOf(folderMarker, StringComparison.OrdinalIgnoreCase) < 0)
						continue;

					var packageName = resourceName.Substring(
						resourceName.LastIndexOf(folderMarker, StringComparison.OrdinalIgnoreCase) + folderMarker.Length);

					yield return new Package
					{
						Name = packageName,
						Source = GetResourceURL(assembly, resourceName)
					};
				}
			}
		}
	}

	/// <summary>
	/// Represents the method that will handle the <see cref="ChartJS4.ChartClick"/> event.
	/// </summary>
	/// <param name="sender">The <see cref="ChartJS4"/> control that raised the event.</param>
	/// <param name="e">A <see cref="ChartClickEventArgs"/> that contains the event data.</param>
	/// <example>
	/// <code><![CDATA[
	/// chart.ChartClick += new ChartClickEventHandler(Chart_ChartClick);
	///
	/// private void Chart_ChartClick(object sender, ChartClickEventArgs e)
	/// {
	///     AlertBox.Show($"Clicked point {e.Data.data[0].pointIndex}");
	/// }
	/// ]]></code>
	/// </example>
	public delegate void ChartClickEventHandler(object sender, ChartClickEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="ChartJS4.ChartClick"/> event.
	/// </summary>
	/// <example>
	/// <code><![CDATA[
	/// chart.ChartClick += (s, e) =>
	/// {
	///     foreach (var item in e.Data.data)
	///         e.Chart.Hide((int)item.dataSetIndex, (int)item.pointIndex);
	/// };
	/// ]]></code>
	/// </example>
	public class ChartClickEventArgs : EventArgs
	{
		internal ChartClickEventArgs(ChartJS4 chart, WidgetEventArgs e)
		{
			Chart = chart;
			Data = e.Data;
		}

		/// <summary>
		/// Returns the chart that raised the event.
		/// </summary>
		/// <value>The <see cref="ChartJS4"/> control that was clicked.</value>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartClick += (s, e) => e.Chart.UpdateChart();
		/// ]]></code>
		/// </example>
		public ChartJS4 Chart { get; }

		/// <summary>
		/// Returns the event data sent by the client.
		/// </summary>
		/// <value>
		/// A dynamic object with a <c>data</c> array containing one entry for each chart element under the pointer.
		/// Each entry has the <c>pointIndex</c> (index of the data point) and <c>dataSetIndex</c> (index of the dataset) fields.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartClick += (s, e) =>
		/// {
		///     int dataSetIndex = e.Data.data[0].dataSetIndex;
		///     int pointIndex = e.Data.data[0].pointIndex;
		///     var value = e.Chart.DataSets[dataSetIndex].Data[pointIndex];
		/// };
		/// ]]></code>
		/// </example>
		public dynamic Data { get; }
	}
}
