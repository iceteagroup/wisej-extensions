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
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.jSequence
{
	/// <summary>
	/// Represents a control that turns text into UML sequence diagrams using the js-sequence-diagrams library:
	/// <see href="https://bramp.github.io/js-sequence-diagrams/"/>.
	/// </summary>
	/// <remarks>
	/// The diagram is defined by the <see cref="UML"/> text and rendered as SVG on the client using the
	/// selected <see cref="Theme"/>. Clicking a text element of the diagram fires the <see cref="ElementClick"/> event.
	/// </remarks>
	/// <example>
	/// Creating a simple sequence diagram:
	/// <code><![CDATA[
	/// var sequence = new Sequence
	/// {
	///     Dock = DockStyle.Fill,
	///     Theme = "Hand",
	///     UML = "Browser->Server: GET /orders\n" +
	///           "Server->Database: SELECT * FROM Orders\n" +
	///           "Database-->Server: rows\n" +
	///           "Server-->Browser: 200 OK"
	/// };
	/// sequence.ElementClick += (s, e) => AlertBox.Show("Clicked: " + e.Element);
	/// this.Controls.Add(sequence);
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Sequence))]
	[DefaultEvent("ElementClick")]
	[ApiCategory("jSequence")]
	public class Sequence : Widget
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Sequence"/> control.
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
		/// Returns or sets the UML definition of the diagram using this syntax: <see href="https://github.com/bramp/js-sequence-diagrams/blob/master/src/grammar.jison"/>.
		/// </summary>
		/// <remarks>
		/// Each line defines a participant, a message (<c>A-&gt;B: text</c> for a solid line, <c>A--&gt;B: text</c> for a dashed line,
		/// <c>A-&gt;&gt;B: text</c> for an open arrow), a note (<c>Note left of A: text</c>, <c>Note over A,B: text</c>)
		/// or a title (<c>Title: text</c>). Setting this property redraws the whole diagram. A null value is converted to an empty string.
		/// </remarks>
		/// <example>
		/// Defining a diagram with a title, participants, messages and a note:
		/// <code><![CDATA[
		/// this.sequence1.UML = string.Join("\n",
		///     "Title: Login",
		///     "participant User",
		///     "participant App",
		///     "User->App: Enter credentials",
		///     "Note right of App: Validate password",
		///     "App-->User: Welcome!");
		/// ]]></code>
		/// </example>
		[DefaultValue("")]
		[DesignerActionList]
		[Editor("Wisej.Design.HtmlEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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
		/// <remarks>
		/// The supported values are <c>"Simple"</c> (the default) and <c>"Hand"</c> (hand-drawn look).
		/// The name is case insensitive.
		/// </remarks>
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
		/// Returns the list of packages (jQuery, WebFont, Snap.svg, Underscore and js-sequence-diagrams) loaded on the client before the diagram is created.
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
		/// Returns the initialization script that renders the diagram on the client.
		/// </summary>
		/// <remarks>
		/// The script is built from the embedded <c>startup.js</c> resource using the current <see cref="UML"/> and
		/// <see cref="Theme"/> values. The setter is ignored.
		/// </remarks>
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

			script = script.Replace("$options", options.ToJSON(WisejSerializerOptions.CamelCase));

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
		/// Retrieves an image of the rendered diagram and passes it to the <paramref name="callback"/> method.
		/// </summary>
		/// <param name="callback">Callback method that receives the <see cref="Image"/> object, or null if the diagram is empty.</param>
		/// <exception cref="ArgumentNullException"><paramref name="callback"/> is null.</exception>
		/// <remarks>
		/// The SVG diagram is rasterized in the browser to a PNG image with the current size of the control,
		/// and sent back to the server. The <paramref name="callback"/> is invoked asynchronously when the image is received.
		/// </remarks>
		/// <example>
		/// Saving the diagram image to a file:
		/// <code><![CDATA[
		/// private void buttonSave_Click(object sender, EventArgs e)
		/// {
		///     this.sequence1.GetImage(image =>
		///     {
		///         if (image != null)
		///             image.Save(Application.MapPath("Diagrams/login.png"), ImageFormat.Png);
		///     });
		/// }
		/// ]]></code>
		/// </example>
		public void GetImage(Action<Image> callback)
		{
			if (callback == null)
				throw new ArgumentNullException(nameof(callback));

			GetImageCore((result) =>
			{

				if (result is Exception)
					throw (Exception)result;
				else
					callback(result as Image);
			});
		}

		/// <summary>
		/// Asynchronously returns an image of the rendered diagram.
		/// </summary>
		/// <returns>An awaitable <see cref="Task"/> that contains the <see cref="Image"/>, or null if the diagram is empty.</returns>
		/// <remarks>
		/// The SVG diagram is rasterized in the browser to a PNG image with the current size of the control.
		/// </remarks>
		/// <example>
		/// Displaying the diagram image in a <see cref="PictureBox"/>:
		/// <code><![CDATA[
		/// private async void buttonSnapshot_Click(object sender, EventArgs e)
		/// {
		///     this.pictureBox1.Image = await this.sequence1.GetImageAsync();
		/// }
		/// ]]></code>
		/// </example>
		public Task<Image> GetImageAsync()
		{
			var tcs = new TaskCompletionSource<Image>();

			GetImageCore((result) =>
			{

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
