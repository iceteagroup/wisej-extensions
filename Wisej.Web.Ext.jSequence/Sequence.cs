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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Configuration;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.jSequence
{
	/// <summary>
	/// The Sequence control turns text into UML sequence diagrams: https://bramp.github.io/js-sequence-diagrams/.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Sequence))]
	[DefaultEvent("ElementClick")]
	public class Sequence : Widget
	{
		/// <summary>
		/// Constructs a new <see cref="T: Wisej.Web.Ext.jSequence.Sequence"/> control.
		/// </summary>
		public Sequence()
		{
		}

		#region Events

		/// <summary>
		/// Triggered when the user clicks an element in the sequence.
		/// </summary>
		[Description("Triggered when the user clicks an element in the sequence.")]
		public event ElementClickEventHandler ElementClick
		{
			add { base.Events.AddHandler(nameof(ElementClick), value); }
			remove { base.Events.RemoveHandler(nameof(ElementClick), value); }
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.jSequence.Sequence.ElementClick"/> event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnElementClick(ElementClickEventArgs e)
		{
			((ElementClickEventHandler)base.Events[nameof(ElementClick)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the UML definition of the diagram using this syntax: https://github.com/bramp/js-sequence-diagrams/blob/master/src/grammar.jison.
		/// </summary>
		[DefaultValue("")]
		[DesignerActionList]
		[Editor("Wisej.Design.HtmlEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
		public string UML
		{
			get { return this._uml; }
			set
			{
				value = value ?? string.Empty;
				if (this._uml != value)
				{
					this._uml = value;
					Update();
				}
			}
		}
		private string _uml = string.Empty;

		/// <summary>
		/// Returns or sets the name of the theme to use to draw the UML diagram.
		/// </summary>
		[DesignerActionList]
		[DefaultValue("Simple")]
		[TypeConverter(typeof(ThemeTypeConverter))]
		public string Theme
		{
			get { return this._theme; }
			set
			{
				value = value ?? string.Empty;
				if (this._theme != value)
				{
					this._theme = value;
					Update();
				}
			}
		}
		private string _theme = "Simple";

		/// <summary>
		/// Overridden to return our list of script resources.
		/// </summary>
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

					base.Packages.AddRange(new Package[] {
						new Package() {
							Name = "jquery.js",
							Source = GetResourceURL("Wisej.Web.Ext.jSequence.JavaScript.jquery-3.1.1.js")
						},
						new Package() {
							Name = "webfont.js",
							Source = GetResourceURL("Wisej.Web.Ext.jSequence.JavaScript.webfont-min.js")
						},
						new Package() {
							Name = "snap.svg.js",
							Source = GetResourceURL("Wisej.Web.Ext.jSequence.JavaScript.snap.svg-min.js")
						},
						new Package() {
							Name = "underscore.js",
							Source = GetResourceURL("Wisej.Web.Ext.jSequence.JavaScript.underscore-min.js")
						},
						new Package() {
							Name = "sequence-diagram.js",
							Source = GetResourceURL("Wisej.Web.Ext.jSequence.JavaScript.sequence-diagram-min.js")
						}
					});
				}

				return base.Packages;
			}
		}

		/// <summary>
		/// Overridden to create our initialization script.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
			set { }
		}

		// disable inlining or we lose the calling assembly in GetResourceString().
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string BuildInitScript()
		{
			dynamic options = new DynamicObject();
			string script = GetResourceString("Wisej.Web.Ext.jSequence.JavaScript.startup.js");
			options.uml = this.UML;
			options.theme = this.Theme.ToLower();

			script = script.Replace("$options", options.ToString());

			return script;
		}

		/// <summary>
		/// Handles events fired by the widget.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			switch (e.Type)
			{
				case "elementClick":
					ProcessElementClickWebEvent(e);
					break;

				default:
					base.OnWidgetEvent(e);
					break;
			}
		}

		// Handles clicks on the inner elements of the sequence.
		private void ProcessElementClickWebEvent(WidgetEventArgs e)
		{
			dynamic data = e.Data;

			var element = data.element ?? "";
			if (!String.IsNullOrEmpty(element))
			{
				int x = data.x ?? 0;
				int y = data.y ?? 0;
				var location = PointToClient(new Point(x, y));
				MouseButtons button = GetMouseButton(data.button ?? 0);

				OnElementClick(new ElementClickEventArgs(element, button, 1, location));
			}
		}

		private static MouseButtons GetMouseButton(int button)
		{
			switch (button)
			{
				case 0: return MouseButtons.Left;
				case 1: return MouseButtons.Middle;
				case 2: return MouseButtons.Right;
				default:
					return MouseButtons.None;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns the sequence image.
		/// </summary>
		/// <param name="callback">Callback method that receives the <see cref="Image"/> object.</param>
		/// <exception cref="ArgumentNullException">If any of the arguments is null.</exception>
		public void GetImage(Action<Image> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			GetImageCore((result) => {

				if (result is Exception)
					throw (Exception)result;
				else
					callback(result as Image);
			});
		}

		/// <summary>
		/// Asynchronously returns the sequence image.
		/// </summary>
		/// <returns>An awaitable <see cref="Task"/> that contains the image.</returns>
		public Task<Image> GetImageAsync()
		{
			var tcs = new TaskCompletionSource<Image>();

			GetImageCore((result) => {

				if (result is Exception)
					tcs.SetException((Exception)result);
				else if (result is Image)
					tcs.SetResult((Image)result);
				else
					tcs.SetResult(null);
			});

			return tcs.Task;
		}

		// Implementation
		private void GetImageCore(Action<object> callback)
		{
			Call("getImage",
				(result) =>
				{
					if (result is string)
						result = ImageFromBase64((string)result);

					callback(result);
				}, null);
		}

		/// <summary>
		/// Returns the Image encoded in a base64 string.
		/// </summary>
		/// <param name="base64">The base64 string representation of the image from the client.</param>
		/// <returns>An <see cref="Image"/> created from the <paramref name="base64"/> string.</returns>
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
				return new Bitmap(stream);
			}
			catch { }

			return null;
		}

		#endregion

		#region Theme TypeConverter

		private class ThemeTypeConverter : TypeConverter
		{
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return true;
			}

			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				return new StandardValuesCollection(new[] { "Simple", "Hand" });
			}
		}

		#endregion
	}
}
