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
		public ChartJS4()
		{
			_labels = new LabelCollection(this);
			_dataSets = new DataSetCollection(this);
		}

		#region Events

		/// <summary>
		/// Fired when the user clicks a data point on the chart.
		/// </summary>
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
		/// Determines whether the ChartType property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeChartType() => _chartType != ChartType.Line;

		/// <summary>
		/// Resets the ChartType property to its default value.
		/// </summary>
		public void ResetChartType() => _chartType = ChartType.Line;

		/// <summary>
		/// Gets or sets the labels for the chart data.
		/// </summary>
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
		/// Gets or sets the data sets for the chart.
		/// </summary>
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
		/// Gets or sets the chart options.
		/// </summary>
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
		/// Returns the chart as a PNG image.
		/// </summary>
		/// <returns>An <see cref="Image"/> with a representation of the chart.</returns>
		public async Task<Image> GetImageAsync()
		{
			var tcs = new TaskCompletionSource<Image>();
			GetImage((result) => { tcs.SetResult(result); });
			return await tcs.Task;
		}

		/// <summary>
		/// Returns the chart as a PNG image.
		/// </summary>
		/// <param name="callback">Callback method that receives the image.</param>
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
		/// <param name="duration">Duration of the update animation in milliseconds.</param>
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
		/// <param name="mode">The update mode. Can be 'none', 'resize', 'reset', 'hide', 'show', 'normal', or 'active'.</param>
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
		public void Destroy()
		{
			Call("destroy");
		}

		/// <summary>
		/// Resets the chart to its initial state before any user interactions.
		/// </summary>
		public void Reset()
		{
			Call("reset");
		}

		/// <summary>
		/// Triggers a redraw of the chart without animations.
		/// </summary>
		public void Render()
		{
			Call("render");
		}

		/// <summary>
		/// Stops all currently running animations on the chart.
		/// </summary>
		public void Stop()
		{
			Call("stop");
		}

		/// <summary>
		/// Resizes the chart canvas. If no dimensions are provided, detects the new size from the container.
		/// </summary>
		/// <param name="width">Optional width in pixels.</param>
		/// <param name="height">Optional height in pixels.</param>
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
		public void Clear()
		{
			Call("clear");
		}

		/// <summary>
		/// Returns a base64 encoded string of the chart in the requested format.
		/// </summary>
		/// <param name="type">Image format (e.g., 'image/png', 'image/jpeg').</param>
		/// <param name="quality">Quality for lossy formats (0.0 to 1.0).</param>
		/// <param name="callback">Callback that receives the base64 string.</param>
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
		/// <param name="callback">Callback that receives the base64 string.</param>
		public void ToBase64Image(Action<string> callback)
		{
			ToBase64Image("image/png", 1.0, callback);
		}

		/// <summary>
		/// Generates an HTML legend for the chart.
		/// </summary>
		/// <param name="callback">Callback that receives the HTML string.</param>
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
		/// Gets the number of visible datasets.
		/// </summary>
		/// <param name="callback">Callback that receives the count.</param>
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
		/// <param name="callback">Callback that receives the visibility state.</param>
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
		/// <param name="visible">True to show, false to hide.</param>
		public void SetDatasetVisibility(int datasetIndex, bool visible)
		{
			Call("setDatasetVisibility", datasetIndex, visible);
		}

		/// <summary>
		/// Toggles the visibility of data at the specified index across all datasets.
		/// </summary>
		/// <param name="index">Index of the data.</param>
		public void ToggleDataVisibility(int index)
		{
			Call("toggleDataVisibility", index);
		}

		/// <summary>
		/// Gets the visibility state of data at the specified index.
		/// </summary>
		/// <param name="index">Index of the data.</param>
		/// <param name="callback">Callback that receives the visibility state.</param>
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
		public void Hide(int datasetIndex)
		{
			Call("hide", datasetIndex);
		}

		/// <summary>
		/// Hides a specific data element in a dataset.
		/// </summary>
		/// <param name="datasetIndex">Index of the dataset.</param>
		/// <param name="dataIndex">Index of the data element.</param>
		public void Hide(int datasetIndex, int dataIndex)
		{
			Call("hide", datasetIndex, dataIndex);
		}

		/// <summary>
		/// Shows a dataset and triggers the 'show' animation.
		/// </summary>
		/// <param name="datasetIndex">Index of the dataset to show.</param>
		public void Show(int datasetIndex)
		{
			Call("show", datasetIndex);
		}

		/// <summary>
		/// Shows a specific data element in a dataset.
		/// </summary>
		/// <param name="datasetIndex">Index of the dataset.</param>
		/// <param name="dataIndex">Index of the data element.</param>
		public void Show(int datasetIndex, int dataIndex)
		{
			Call("show", datasetIndex, dataIndex);
		}

		/// <summary>
		/// Sets the active (hovered) elements for the chart.
		/// </summary>
		/// <param name="activeElements">Array of active element specifications.</param>
		public void SetActiveElements(object[] activeElements)
		{
			Call("setActiveElements", activeElements.ToList());
		}

		/// <summary>
		/// Gets the currently active (hovered) elements.
		/// </summary>
		/// <param name="callback">Callback that receives the array of active elements.</param>
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
		/// Gets the list of additional plugin packages to load with the chart.
		/// Use this to register custom Chart.js plugins globally (e.g., in Application_Start).
		/// </summary>
		[Browsable(false)]
		public static List<Package> PluginPackages { get; } = new List<Package>();

		/// <summary>
		/// Gets or sets the widget functions that are passed to the chart.
		/// Widget functions allow JavaScript callbacks (e.g., scriptable options) to be defined
		/// server-side and called client-side using the <c>(ctx)=>functionName</c> pattern.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WidgetFunction[]? WidgetFunctions { get; set; }

		/// <summary>
		/// Overridden to return the list of script resources.
		/// </summary>
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
		/// Overridden to create our initialization script.
		/// </summary>
		[Browsable(false)]
		public override string InitScript
		{
			// disable inlining or we lose the calling assembly in GetResourceString().
			[MethodImpl(MethodImplOptions.NoInlining)]
			get { return GetResourceString("Wisej.Web.Ext.ChartJS4.JavaScript.startup.js"); }
			set { }
		}

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
        /// using the <c>(ctx)=>functionName</c> pattern for scriptable options.
        /// </summary>
        public class WidgetFunction
		{
			/// <summary>
			/// The name of the function (referenced in chart options as <c>(ctx)=>Name</c>).
			/// </summary>
			public string Name { get; set; } = string.Empty;

			/// <summary>
			/// The JavaScript source code of the function body.
			/// Should be a valid JavaScript function expression, e.g. <c>"function(ctx) { return ctx.dataIndex % 2 === 0 ? 'red' : 'blue'; }"</c>.
			/// </summary>
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
	/// Delegate for the ChartClick event.
	/// </summary>
	public delegate void ChartClickEventHandler(object sender, ChartClickEventArgs e);

	/// <summary>
	/// Provides data for the ChartClick event.
	/// </summary>
	public class ChartClickEventArgs : EventArgs
	{
		internal ChartClickEventArgs(ChartJS4 chart, WidgetEventArgs e)
		{
			Chart = chart;
			Data = e.Data;
		}

		/// <summary>
		/// Gets the chart that raised the event.
		/// </summary>
		public ChartJS4 Chart { get; }

		/// <summary>
		/// Gets the event data.
		/// </summary>
		public dynamic Data { get; }
	}
}
