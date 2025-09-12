///////////////////////////////////////////////////////////////////////////////
//
// (C) 2024 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Wisej.Base;

namespace Wisej.Web.Ext.ApexCharts
{
	public class ApexChart : Widget
	{

		#region Properties

		/// <summary>
		/// Returns or sets the list of events that are fired by the widget wrapper.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string[] WiredEvents
		{
			get { return base.WiredEvents; }
			set { base.WiredEvents = value; }
		}

		/// <summary>
		/// Property for required EJ2 packages.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override List<Package> Packages
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				var packages = base.Packages;
				if (packages.Count == 0)
				{
					dynamic config = GetConfiguration();

					// select the source.
					dynamic source = config.sources[config.source];

					string root = source.root;

					var configPackages = source.packages;
					foreach (var p in configPackages)
					{
						string packageName = p.Name;
						string packageSource = p.Value;

						packageSource = packageSource.Replace("%root%", root);

						packages.Add(new Package()
						{
							Name = packageName,
							Source = packageSource
						});
					}
				}
				return packages;
			}
		}

		/// <summary>
		/// Disposes of resources.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{
			if (this.DesignMode)
			{
				_configuration = null;
			}

			base.Dispose(disposing);
		}

		// Finds the configuration file.
		private dynamic GetConfiguration()
		{
			if (_configuration == null)
			{
				lock (typeof(ApexChart))
				{
					if (_configuration == null)
					{
						var name = $"{typeof(ApexChart).Namespace}.json";

						// try the project root first.
						var filePath = Application.MapPath(name);
						if (File.Exists(filePath))
						{
							_configuration = JSON.Parse(File.ReadAllText(filePath));
						}
						else
						{
							var endName = $".{name}";

							// try in the application's embedded resources.
							var assembly = (Assembly)typeof(Application).InvokeMember("GetMainAssembly",
								BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod,
								null, null, null);
							var resourceName = assembly?.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(endName));

							// try in the custom library, if the control was subclassed.
							if (resourceName == null)
							{
								assembly = GetType().Assembly;
								resourceName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(endName));
							}

							// try in this assembly (the extension).
							if (resourceName == null)
							{
								assembly = typeof(ApexChart).Assembly;
								resourceName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(endName));
							}

							// load the config file if found.
							if (resourceName != null)
							{
								using (var stream = assembly.GetManifestResourceStream(resourceName))
								{
									_configuration = JSON.Parse(stream);
								}
							}
						}

						if (_configuration == null)
							throw new Exception($"Missing configuration file: {name}. See https://docs.wisej.com/extensions/premium-extensions.");
					}
				}
			}
			return _configuration;
		}
		private static dynamic _configuration;

		/// <summary>
		/// optional HTML to use as the container.
		/// </summary>
		private string WidgetHtml { get; set; }

		/// <summary>
		/// The script used to initialize the widget
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			// disable inlining or we lose the calling assembly in GetResourceString().
			[MethodImpl(MethodImplOptions.NoInlining)]
			get { return this.GetResourceString($"{typeof(ApexChart).Namespace}.{this.GetControlType().Name}.js"); }
			set { }
		}

		/// <summary>
		/// The JS functions used in the widget
		/// </summary>
		[DefaultValue(null)]
		public WidgetFunction[] WidgetFunctions
		{
			get;
			set;
		}

		/// <summary>
		/// The widget event handlers.
		/// </summary>
		[DefaultValue(null)]
		public WidgetEventHandler[] WidgetEvents
		{
			get;
			set;
		}

		/// <summary>
		/// The templates used by the widget
		/// </summary>
		[DefaultValue(null)]
		public WidgetTemplate[] WidgetTemplates
		{
			get;
			set;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns the type derived from ejBase.
		/// </summary>
		private Type GetControlType()
		{
			var type = this.GetType();
			while (type.BaseType != typeof(ApexChart) && type.BaseType != null)
			{
				type = type.BaseType;
			}
			return type;
		}

		/// <summary>
		/// Returns the Image encoded in a base64 string.
		/// </summary>
		/// <param name="base64">base64 string to convert to an image.</param>
		/// <returns>An <see cref="Image"/>.</returns>
		internal static Image ImageFromBase64(string base64)
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

		#endregion

		#region WidgetTemplate

		/// <summary>
		/// Represents a Syncfusion template.
		/// </summary>
		/// <remarks>
		/// Templates in the Syncfusion library are either plain global JavaScript functions, or
		/// HTML snippets using their template syntax.
		/// </remarks>
		public class WidgetTemplate
		{
			/// <summary>
			/// The identifier of the <see cref="WidgetTemplate"/>
			/// </summary>
			[DefaultValue("")]
			public string Id
			{
				get { return this._id; }
				set
				{
					value = value ?? string.Empty;
					this._id = value;
				}
			}
			private string _id = string.Empty;

			/// <summary>
			/// The template used by the <see cref="WidgetTemplate"/>
			/// </summary>
			[DefaultValue("")]
			[Editor("Wisej.Design.HtmlEditor, Wisej.Framework.Design, Version=3.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
					"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
			public string Template
			{
				get { return this._template; }
				set
				{
					value = value ?? string.Empty;
					this._template = value;
				}
			}
			private string _template = string.Empty;

			/// <summary>
			/// Type of the template. Use "text/x-jsrender" for HTML templates.
			/// </summary>
			[DefaultValue("text/x-jsrender")]
			public string Type
			{
				get { return this._type; }
				set
				{
					value = value ?? string.Empty;
					this._type = value;
				}
			}
			private string _type = "text/x-jsrender";

			/// <summary>
			/// String representation of <see cref="WidgetTemplate"/>
			/// </summary>
			/// <returns></returns>
			public override string ToString()
			{
				return this.Id ?? SR.GetString("toStringNone");
			}

		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Wire the client config to the server
		/// </summary>
		/// <param name="config"></param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ext.ApexChartsWidget";
			config.widgetHtml = this.WidgetHtml;
			config.widgetEvents = this.WidgetEvents;
			config.widgetFunctions = this.WidgetFunctions;
			config.widgetTemplates = this.WidgetTemplates;
		}

		#endregion


		#region WidgetEventHandler

		/// <summary>
		/// Represents a JavScript handler attached to an event fired by the
		/// third party widget.
		/// </summary>
		/// <remarks>
		/// Refer to "this" in the context of the function to target the wisej.web.Widget instance, and to
		/// "this.widget" to refer to the third party widget.
		/// </remarks>
		public class WidgetEventHandler
		{
			/// <summary>
			/// The name of the <see cref="WidgetEventHandler"/>
			/// </summary>
			[DefaultValue("")]
			public string Name
			{
				get { return this._name; }
				set
				{
					value = value ?? string.Empty;
					this._name = value;
				}
			}
			private string _name = string.Empty;

			/// <summary>
			/// The source code of the <see cref="WidgetEventHandler"/>
			/// </summary>
			[Editor("Wisej.Design.CodeEditor, Wisej.Framework.Design, Version=3.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
					"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
			public string Source
			{
				get { return this._source; }
				set
				{
					value = value ?? string.Empty;
					this._source = value;
				}
			}
			private string _source = string.Empty;

			/// <summary>
			/// Gets the string representation of <see cref="WidgetEventHandler"/>
			/// </summary>
			/// <returns></returns>
			public override string ToString()
			{
				return this.Name ?? SR.GetString("toStringNone");
			}
		}

		#endregion

		#region WidgetFunction

		/// <summary>
		/// Represents a JavaScript function attached to the widget on the browser.
		/// </summary>
		/// <remarks>
		/// Refer to "this" in the context of the function to target the wisej.web.Widget instance, and to
		/// "this.widget" to refer to the third party widget.
		/// </remarks>
		public class WidgetFunction
		{
			/// <summary>
			/// The name of the <see cref="WidgetFunction"/>
			/// </summary>
			[DefaultValue("")]
			public string Name
			{
				get { return this._name; }
				set
				{
					value = value ?? string.Empty;
					this._name = value;
				}
			}
			private string _name = string.Empty;

			/// <summary>
			/// The source code for the <see cref="WidgetFunction"/>
			/// </summary>
			[Editor("Wisej.Design.CodeEditor, Wisej.Framework.Design, Version=3.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
					"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
			public string Source
			{
				get { return this._source; }
				set
				{
					value = value ?? string.Empty;
					this._source = value;
				}
			}
			private string _source = string.Empty;

			/// <summary>
			/// String representation of <see cref="WidgetFunction"/>
			/// </summary>
			/// <returns></returns>
			public override string ToString()
			{
				return this.Name ?? SR.GetString("toStringNone");
			}
		}

		#endregion

	}
}
