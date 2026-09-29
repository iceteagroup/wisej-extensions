///////////////////////////////////////////////////////////////////////////////
//
// (C) 2026 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.Mermaid
{
	/// <summary>
	/// Represents a widget that renders <see href="https://mermaid.js.org/">Mermaid</see> diagrams (flowcharts,
	/// sequence diagrams, class diagrams, Gantt charts, etc.) from their text definition.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The diagram is rendered in the browser by the Mermaid library. Use <see cref="Diagram"/> for the diagram
	/// source and the <see cref="Look"/>, <see cref="Theme"/>, <see cref="SecurityLevel"/>, <see cref="ThemeVariables"/>
	/// and related properties to customize the Mermaid initialization options.
	/// </para>
	/// <para>
	/// Every change to <see cref="Diagram"/> or to the options re-initializes Mermaid and renders the whole diagram again.
	/// When the diagram cannot be parsed the widget displays the error text and fires the <see cref="Error"/> event.
	/// </para>
	/// </remarks>
	/// <example>
	/// Rendering a flowchart and changing the theme:
	/// <code><![CDATA[
	/// // Basic usage: render a flowchart.
	/// var mermaid = new Wisej.Web.Ext.Mermaid.Mermaid()
	/// {
	///     Dock = Wisej.Web.DockStyle.Fill,
	///     Diagram =
	/// @"flowchart TD
	///     A[Start] --> B{Valid?}
	///     B -- Yes --> C[Continue]
	///     B -- No  --> D[Stop]"
	/// };
	///
	/// // Optional: tweak Mermaid initialization options.
	/// mermaid.Theme = "dark";
	/// mermaid.SecurityLevel = Wisej.Web.Ext.Mermaid.MermaidSecurityLevel.Strict;
	///
	/// this.Controls.Add(mermaid);
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[DefaultEvent("ElementClick")]
	[ApiCategory("Mermaid")]
	public class Mermaid : Widget
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Mermaid"/> widget with an empty diagram.
		/// </summary>
		/// <remarks>
		/// The default options are: <see cref="EnablePanZoom"/> = true, <see cref="DeterministicIds"/> = true,
		/// <see cref="Look"/> = "classic", <see cref="Theme"/> = "default", <see cref="SecurityLevel"/> =
		/// <see cref="MermaidSecurityLevel.Strict"/> and <see cref="LogLevel"/> = <see cref="MermaidLogLevel.Trace"/>.
		/// </remarks>
		/// <example>
		/// Creating the widget and assigning the diagram later:
		/// <code><![CDATA[
		/// var mermaid = new Wisej.Web.Ext.Mermaid.Mermaid();
		/// mermaid.Diagram = "flowchart TD; A-->B";
		/// ]]></code>
		/// </example>
		public Mermaid() : this("")
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Mermaid"/> widget with the specified diagram.
		/// </summary>
		/// <param name="diagram">Initial Mermaid diagram source text. Null is treated as an empty string.</param>
		/// <remarks>
		/// The constructor assigns <see cref="Diagram"/> and applies the same default options as <see cref="Mermaid()"/>.
		/// </remarks>
		/// <example>
		/// Creating the widget with its diagram:
		/// <code><![CDATA[
		/// var mermaid = new Wisej.Web.Ext.Mermaid.Mermaid("flowchart LR; Order-->Invoice-->Payment");
		/// mermaid.Dock = DockStyle.Fill;
		/// this.Controls.Add(mermaid);
		/// ]]></code>
		/// </example>
		public Mermaid(string diagram)
		{
			this.Diagram = diagram;
			
			// defaults
			this.Options.enablePanZoom = true;
			this.Options.deterministicIds = true;
			this.Options.look = "classic";
			this.Options.theme = "default";
			this.Options.securityLevel = MermaidSecurityLevel.Strict;
			this.Options.logLevel = MermaidLogLevel.Trace;
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the diagram is modified, signaling that its state has changed.
		/// </summary>
		/// <remarks>Subscribe to this event to be notified of changes to the diagram. This can be used to update the
		/// user interface, save changes, or perform other actions in response to modifications.</remarks>
		public event EventHandler DiagramChanged
		{
			add { base.Events.AddHandler(nameof(DiagramChanged), value); }
			remove { base.Events.RemoveHandler(nameof(DiagramChanged), value); }
		}

		/// <summary>
		/// Occurs when an element in the Mermaid diagram is clicked.
		/// </summary>
		/// <remarks>
		/// This event provides information about the clicked element, including its text content,
		/// type (node, edge, cluster, etc.), IDs, and mouse button/location data.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.ElementClick += (s, e) =>
		/// {
		///     MessageBox.Show($"Clicked: {e.Element}\nType: {e.Data?.elementType}");
		/// };
		/// ]]></code>
		/// </example>
		[Description("Occurs when an element in the Mermaid diagram is clicked.")]
		public event ElementClickEventHandler ElementClick
		{
			add { base.Events.AddHandler(nameof(ElementClick), value); }
			remove { base.Events.RemoveHandler(nameof(ElementClick), value); }
		}

		/// <summary>
		/// Occurs when the current diagram fails Mermaid validation/parsing in the browser.
		/// </summary>
		/// <remarks>
		/// This event is raised from a client-side error notification when Mermaid cannot parse/validate the diagram.
		/// If you want to proactively validate without rendering, use <see cref="ValidateAsync(string)"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Error += (s, e) =>
		/// {
		///     // e.Message contains a human-readable message.
		///     Wisej.Web.MessageBox.Show(e.Message, "Mermaid error");
		/// };
		/// ]]></code>
		/// </example>
		public event MermaidErrorEventHandler Error
		{
			add { base.Events.AddHandler(nameof(Error), value); }
			remove { base.Events.RemoveHandler(nameof(Error), value); }
		}

		/// <summary>
		/// Raises the <see cref="ElementClick"/> event.
		/// </summary>
		/// <param name="e">Event data.</param>
		/// <remarks>
		/// Override this method to intercept element clicks before subscribers are notified.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// public class MyMermaid : Wisej.Web.Ext.Mermaid.Mermaid
		/// {
		///     protected override void OnElementClick(Wisej.Web.Ext.Mermaid.ElementClickEventArgs e)
		///     {
		///         // Add logging, then forward to base to raise the event.
		///         System.Diagnostics.Debug.WriteLine($"Clicked: {e.Element}");
		///         base.OnElementClick(e);
		///     }
		/// }
		/// ]]></code>
		/// </example>
		protected virtual void OnElementClick(ElementClickEventArgs e)
		{
			((ElementClickEventHandler)base.Events[nameof(ElementClick)])?.Invoke(this, e);
		}

		/// <summary>
		/// Raises the <see cref="Error"/> event.
		/// </summary>
		/// <param name="e">Event data.</param>
		/// <remarks>
		/// Override this method to intercept Mermaid errors before subscribers are notified.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// public class MyMermaid : Wisej.Web.Ext.Mermaid.Mermaid
		/// {
		///     protected override void OnError(Wisej.Web.Ext.Mermaid.MermaidErrorEventArgs e)
		///     {
		///         // Add logging, then forward to base to raise the event.
		///         System.Diagnostics.Debug.WriteLine(e?.Message);
		///         base.OnError(e);
		///     }
		/// }
		/// ]]></code>
		/// </example>
		protected virtual void OnError(MermaidErrorEventArgs e)
		{
			((MermaidErrorEventHandler)base.Events[nameof(Error)])?.Invoke(this, e);
		}

		/// <summary>
		/// Raises the <see cref="DiagramChanged"/> event.
		/// </summary>
		/// <remarks>This method is called to notify subscribers that the diagram has changed.  Derived classes can
		/// override this method to provide additional behavior  when the <see cref="DiagramChanged"/> event is raised. When
		/// overriding,  ensure to call the base implementation to maintain event invocation.</remarks>
		/// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
		protected virtual void OnDiagramChanged(EventArgs e)
		{
			((EventHandler)base.Events[nameof(DiagramChanged)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the Mermaid diagram source text.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The text uses the Mermaid syntax (see <see href="https://mermaid.js.org/intro/syntax-reference.html"/>);
		/// the first line declares the diagram type, i.e. <c>flowchart TD</c>, <c>sequenceDiagram</c>, <c>classDiagram</c>, <c>gantt</c>.
		/// Setting null is the same as setting an empty string; an empty diagram is not rendered.
		/// </para>
		/// <para>
		/// Changing the value fires <see cref="DiagramChanged"/> and renders the diagram again on the client.
		/// Syntax errors are reported asynchronously through the <see cref="Error"/> event.
		/// </para>
		/// </remarks>
		/// <example>
		/// Assigning a sequence diagram:
		/// <code><![CDATA[
		/// mermaid.Diagram =
		/// @"sequenceDiagram
		///     participant A as Alice
		///     participant B as Bob
		///     A->>B: Hello Bob
		///     B-->>A: Hello Alice";
		/// ]]></code>
		/// </example>
		[DefaultValue("")]
		[Category("Mermaid")]
		[DesignerActionList]
		[Editor("Wisej.Design.HtmlEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Diagram
		{
			get => this.Options.diagram ?? "";
			set
			{
				value ??= "";
				if (this.Diagram != value)
				{
					this.Options.diagram = value;
					OnDiagramChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Returns or sets the diagram look (e.g. <c>classic</c>, <c>neo</c>, <c>handDrawn</c>).
		/// </summary>
		/// <remarks>
		/// This maps to Mermaid's <c>look</c> initialization option. The default is <c>classic</c>.
		/// </remarks>
		/// <example>
		/// Rendering the diagram with a sketch-like appearance:
		/// <code><![CDATA[
		/// mermaid.Look = "handDrawn";
		/// ]]></code>
		/// </example>
		[Category("Mermaid")]
		[DefaultValue("classic")]
		public string Look
		{
			get => this.Options.look ?? "classic";
			set
			{
				if (this.Look != value)
					this.Options.look = value;
			}
		}

		/// <summary>
		/// Returns or sets the Mermaid theme name (e.g. <c>default</c>, <c>dark</c>, <c>forest</c>, <c>neutral</c>, <c>base</c>).
		/// </summary>
		/// <remarks>
		/// This maps to Mermaid's <c>theme</c> initialization option. Use the <c>base</c> theme when customizing the
		/// colors with <see cref="ThemeVariables"/>, since it's the only theme Mermaid allows to be modified.
		/// </remarks>
		/// <example>
		/// Customizing the colors of the diagram:
		/// <code><![CDATA[
		/// mermaid.Theme = "base";
		/// mermaid.ThemeVariables.primaryColor = "#BB2528";
		/// mermaid.ThemeVariables.primaryTextColor = "#FFFFFF";
		/// ]]></code>
		/// </example>
		[Category("Mermaid")]
		[DefaultValue("default")]
		public string Theme
		{
			get => this.Options.theme ?? "default";
			set
			{
				if (this.Text != value)
					this.Options.theme = value;
			}
		}

		/// <summary>
		/// Returns or sets the security level (maps to Mermaid's <c>securityLevel</c>).
		/// </summary>
		/// <remarks>
		/// The default is <see cref="MermaidSecurityLevel.Strict"/>, which encodes HTML tags in the diagram text and
		/// disables click interactions defined in the diagram. Use a less restrictive level only with trusted diagram sources.
		/// </remarks>
		[Category("Mermaid")]
		[DefaultValue(MermaidSecurityLevel.Strict)]
		public MermaidSecurityLevel SecurityLevel
		{
			get => this.Options.securityLevel ?? MermaidSecurityLevel.Strict;
			set
			{
				if (this.SecurityLevel != value)
					this.Options.securityLevel = value;
			}
		}

		/// <summary>
		/// Returns or sets the level of the messages that Mermaid logs to the browser console (maps to Mermaid's <c>logLevel</c>).
		/// </summary>
		/// <remarks>
		/// The default is <see cref="MermaidLogLevel.Trace"/>, the most verbose level. Consider using
		/// <see cref="MermaidLogLevel.Error"/> in production.
		/// </remarks>
		[Category("Mermaid")]
		[DefaultValue(MermaidLogLevel.Trace)]
		public MermaidLogLevel LogLevel
		{
			get => this.Options.logLevel ?? MermaidLogLevel.Trace;
			set
			{
				if (this.LogLevel != value)
					this.Options.logLevel = value;
			}
		}

		/// <summary>
		/// Returns or sets a value indicating whether pan and zoom functionality is enabled.
		/// </summary>
		/// <remarks>
		/// When enabled (default), the user can drag the diagram with the left mouse button and zoom it using the mouse wheel
		/// (between 0.02 and 2). The pan offset and zoom level are reset to <see cref="ZoomLevel"/> every time the diagram is rendered.
		/// </remarks>
		[Category("Mermaid")]
		[DefaultValue(true)]
		public bool EnablePanZoom
		{
			get => this.Options.enablePanZoom ?? true;
			set
			{
				if (this.EnablePanZoom != value)
					this.Options.enablePanZoom = value;
			}
		}

		/// <summary>
		/// Returns or sets the initial scale factor of the rendered diagram (1 = 100%).
		/// </summary>
		/// <remarks>
		/// The value is applied only when <see cref="EnablePanZoom"/> is true. Zooming with the mouse wheel changes the
		/// scale on the client only and doesn't update this property.
		/// </remarks>
		/// <example>
		/// Showing a large diagram at half its size:
		/// <code><![CDATA[
		/// mermaid.EnablePanZoom = true;
		/// mermaid.ZoomLevel = 0.5f;
		/// ]]></code>
		/// </example>
		[Category("Mermaid")]
		[DefaultValue(1.0f)]
		public float ZoomLevel
		{
			get => this.Options.zoomLevel ?? 1.0f;
			set
			{
				if (this.ZoomLevel != value)
					this.Options.zoomLevel = value;
			}
		}

		/// <summary>
		/// Returns or sets whether Mermaid generates deterministic IDs for the rendered elements.
		/// </summary>
		/// <remarks>
		/// Maps to Mermaid's <c>deterministicIds</c> option. When true (default), the same diagram always produces the
		/// same element IDs, which are also returned in <see cref="ElementClickEventArgs.Data"/>.
		/// </remarks>
		[Category("Mermaid")]
		[DefaultValue(true)]
		public bool DeterministicIds
		{
			get => this.Options.deterministicIds ?? true;
			set
			{
				if (this.DeterministicIds != value)
					this.Options.deterministicIds = value;
			}
		}

		/// <summary>
		/// Returns or sets the font family applied to the diagram text.
		/// </summary>
		/// <remarks>
		/// This maps to Mermaid's <c>fontFamily</c> option and accepts a CSS font-family list. When not set, the getter
		/// returns the name of the control's <see cref="Control.Font"/> but no font is passed to Mermaid, which then uses
		/// the font of the theme. For the size, use the <c>fontSize</c> key in <see cref="ThemeVariables"/>.
		/// </remarks>
		/// <example>
		/// Using a custom font stack:
		/// <code><![CDATA[
		/// mermaid.FontFamily = "\"Inter\", \"Segoe UI\", sans-serif";
		/// mermaid.ThemeVariables.fontSize = "14px";
		/// ]]></code>
		/// </example>
		[Category("Mermaid")]
		public string FontFamily
		{
			get => _fontFamily ?? this.Font.Name;
			set
			{
				if (this.FontFamily != value)
				{
					_fontFamily = value;
					this.Options.fontFamily = value;
				}
			}
		}
		private string _fontFamily = null;

		private bool ShouldSerializeFontFamily()
			=> _fontFamily != null;

		private void ResetFontFamily()
			=> FontFamily = null;

		/// <summary>
		/// Returns a dynamic object with the flowchart-specific options (maps to Mermaid's <c>flowchart</c> configuration).
		/// </summary>
		/// <remarks>
		/// <para>
		/// Add fields using the Mermaid (camel case) names, i.e. <c>curve</c>, <c>htmlLabels</c>, <c>nodeSpacing</c>,
		/// <c>rankSpacing</c>, <c>useMaxWidth</c>. Setting a field updates the widget.
		/// </para>
		/// <para>See <see href="https://mermaid.js.org/config/schema-docs/config-defs-flowchart-diagram-config.html">Flowchart Configuration</see>
		/// in the Mermaid documentation for the complete list. Flowchart colors are set with <see cref="ThemeVariables"/>.</para>
		/// </remarks>
		/// <example>
		/// Changing the edge style and the spacing of a flowchart:
		/// <code><![CDATA[
		/// mermaid.Flowchart.curve = "linear";
		/// mermaid.Flowchart.nodeSpacing = 60;
		/// mermaid.Flowchart.rankSpacing = 80;
		/// ]]></code>
		/// </example>
		[Category("Mermaid")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[TypeConverter(typeof(DynamicObjectConverter))]
		[Editor("Wisej.Design.DynamicObjectEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WisejSerializerOptions(WisejSerializerOptions.None)]
		public virtual dynamic Flowchart
		{
			get
			{
				if (_flowchart == null)
				{
					_flowchart = new DynamicObject();
					(_flowchart as INotifyPropertyChanged).PropertyChanged += (s, e) => this.Update();
				}
				return _flowchart;
			}
		}
		dynamic _flowchart;

		/// <summary>
		/// Returns a dynamic object with the sequence diagram specific options (maps to Mermaid's <c>sequence</c> configuration).
		/// </summary>
		/// <remarks>
		/// <para>
		/// Add fields using the Mermaid (camel case) names, i.e. <c>showSequenceNumbers</c>, <c>mirrorActors</c>,
		/// <c>actorMargin</c>, <c>wrap</c>. Setting a field updates the widget.
		/// </para>
		/// <para>See <see href="https://mermaid.js.org/config/schema-docs/config-defs-sequence-diagram-config.html">Sequence Diagram Configuration</see>
		/// in the Mermaid documentation for the complete list. Colors such as <c>sequenceNumberColor</c> are set with <see cref="ThemeVariables"/>.</para>
		/// </remarks>
		/// <example>
		/// Numbering the messages of a sequence diagram:
		/// <code><![CDATA[
		/// mermaid.Sequence.showSequenceNumbers = true;
		/// mermaid.Sequence.mirrorActors = false;
		/// ]]></code>
		/// </example>
		[Category("Mermaid")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[TypeConverter(typeof(DynamicObjectConverter))]
		[Editor("Wisej.Design.DynamicObjectEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WisejSerializerOptions(WisejSerializerOptions.None)]
		public virtual dynamic Sequence
		{
			get
			{
				if (_sequence == null)
				{
					_sequence = new DynamicObject();
					(_sequence as INotifyPropertyChanged).PropertyChanged += (s, e) => this.Update();
				}
				return _sequence;
			}
		}
		dynamic _sequence;

		/// <summary>
		/// Returns a dynamic object with the theme variables (maps to Mermaid's <c>themeVariables</c>).
		/// </summary>
		/// <remarks>
		/// <para>
		/// Common keys include <c>darkMode</c>, <c>fontFamily</c>, <c>fontSize</c>, <c>primaryColor</c>, <c>lineColor</c>.
		/// Mermaid applies the variables only when <see cref="Theme"/> is set to <c>base</c>. Colors must be hex values
		/// (i.e. <c>#ff0000</c>), not color names. Setting a field updates the widget.
		/// </para>
		/// <para>
		/// See <see href="https://mermaid.js.org/config/theming.html#theme-variables">Theme Variables</see> in the Mermaid documentation for details and examples.
		/// </para>
		/// </remarks>
		/// <example>
		/// Using a dark color scheme with a larger font:
		/// <code><![CDATA[
		/// mermaid.Theme = "base";
		/// mermaid.ThemeVariables.darkMode = true; 
		/// mermaid.ThemeVariables.fontSize = "16px"; 
		/// ]]></code>
		/// </example>
		[Category("Mermaid")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[TypeConverter(typeof(DynamicObjectConverter))]
		[Editor("Wisej.Design.DynamicObjectEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WisejSerializerOptions(WisejSerializerOptions.None)]
		public virtual dynamic ThemeVariables
		{
			get
			{
				if (_themeVariables == null)
				{
					_themeVariables = new DynamicObject();
					(_themeVariables as INotifyPropertyChanged).PropertyChanged += (s, e) => this.Update();
				}
				return _themeVariables;
			}
		}
		dynamic _themeVariables;

		/// <summary>
		/// Returns or sets the URL from which the Mermaid library is loaded by all the <see cref="Mermaid"/> widgets.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The default (null) is to use the embedded Mermaid script resource, but you can set this property to
		/// load Mermaid from a CDN or custom location if needed.
		/// Ensure that the specified URL points to a valid Mermaid JavaScript file (UMD build that defines <c>window.mermaid</c>)
		/// for the widget to function correctly.
		/// </para>
		/// <para>
		/// This is a static setting shared by all sessions. Set it at startup, before any <see cref="Mermaid"/> widget is created:
		/// widgets that already built their <see cref="Packages"/> list are not affected.
		/// </para>
		/// </remarks>
		/// <example>
		/// Loading Mermaid from a CDN when the application starts:
		/// <code><![CDATA[
		/// static void Main()
		/// {
		///     Wisej.Web.Ext.Mermaid.Mermaid.SourceURL = "https://cdn.jsdelivr.net/npm/mermaid@11/dist/mermaid.min.js";
		/// 
		///     new MainPage().Show();
		/// }
		/// ]]></code>
		/// </example>
		public static string SourceURL
		{
			get;
			set;
		}

		/// <summary>
		/// Returns the script packages required by the widget.
		/// </summary>
		/// <remarks>
		/// The list contains the Mermaid library loaded from <see cref="SourceURL"/> or, when it's null, from the embedded resource.
		/// This override ensures that resource resolution happens in the correct calling assembly.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override List<Package> Packages
		{
			// disable inlining or we lose the calling assembly in GetResourceString()/GetResourceURL().
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				if (base.Packages.Count == 0)
				{
					var source = SourceURL ?? GetResourceURL("Wisej.Web.Ext.Mermaid.JavaScript.mermaid.min.js");

					base.Packages.Add(new Package()
					{
						Name = "mermaid.js",
						Source = source
					});
				}

				return base.Packages;
			}
		}

		/// <summary>
		/// Returns the JavaScript code that initializes the widget on the client.
		/// </summary>
		/// <remarks>
		/// The script is loaded from the embedded <c>startup.js</c> resource; assigning a value has no effect.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get { return GetResourceString("Wisej.Web.Ext.Mermaid.JavaScript.startup.js"); }
			set { }
		}

		/// <summary>
		/// Returns or sets the dynamic options object passed to <c>mermaid.initialize()</c> on the client.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The typed properties (<see cref="Diagram"/>, <see cref="Look"/>, <see cref="Theme"/>, <see cref="SecurityLevel"/>, etc.)
		/// store their values in this object. You can add any other Mermaid configuration option that doesn't have
		/// a dedicated property; the names are serialized in camel case.
		/// </para>
		/// <para>
		/// See <see href="https://mermaid.js.org/config/schema-docs/config.html"/> for the available options.
		/// </para>
		/// </remarks>
		/// <example>
		/// Setting Mermaid options that don't have a dedicated property:
		/// <code><![CDATA[
		/// mermaid.Options.maxTextSize = 100000;
		/// mermaid.Options.fontSize = 14;
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[WisejSerializerOptions(WisejSerializerOptions.CamelCase)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override dynamic Options
		{
			get => base.Options;
			set => base.Options = value;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Downloads the currently rendered diagram as an SVG file.
		/// </summary>
		/// <param name="fileName">Optional file name used by the browser download. The default is "diagram.svg".</param>
		/// <remarks>
		/// This method invokes a client-side download of the currently rendered SVG; the file is created
		/// in the browser and is not sent to the server.
		/// </remarks>
		/// <example>
		/// Downloading the diagram from a button:
		/// <code><![CDATA[
		/// private void buttonSvg_Click(object sender, EventArgs e)
		/// {
		///     this.mermaid1.DownloadSvg("order-process.svg");
		/// }
		/// ]]></code>
		/// </example>
		public void DownloadSvg(string fileName = "diagram.svg")
		{
			this.Call("downloadSvg", fileName ?? "diagram.svg");
		}

		/// <summary>
		/// Asynchronously exports the currently rendered diagram to a PNG image.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains the generated <see cref="Image"/>.</returns>
		/// <remarks>
		/// <para>
		/// The image is captured in the browser using the html2canvas library loaded from the
		/// <c>Wisej.Web.Ext.Html2Canvas</c> extension resources, which must be deployed with the application.
		/// The pan and zoom transformation is reset during the capture and restored afterwards.
		/// </para>
		/// <para>
		/// The diagram must have been rendered before calling this method.
		/// </para>
		/// </remarks>
		/// <example>
		/// Saving the diagram as a PNG file on the server:
		/// <code><![CDATA[
		/// private async void buttonExport_Click(object sender, EventArgs e)
		/// {
		///     var image = await this.mermaid1.ExportToImageAsync();
		///     image.Save(Application.MapPath("Exports/diagram.png"), System.Drawing.Imaging.ImageFormat.Png);
		/// }
		/// ]]></code>
		/// </example>
		public async Task<Image> ExportToImageAsync()
		{
			string result = await CallAsync("getImage");
			byte[] buffer = Convert.FromBase64String(result.Substring("data:image/png;base64,".Length));
			using (var ms = new MemoryStream(buffer))
			{
				return Image.FromStream(ms);
			}
		}

		/// <summary>
		/// Downloads the currently rendered diagram as a PDF file.
		/// </summary>
		/// <param name="fileName">Optional file name used by the browser download (default is "diagram.pdf").</param>
		/// <param name="scale">Optional scale factor for image quality (default is 2 for high DPI).</param>
		/// <param name="backgroundColor">Optional CSS background color for the PDF, i.e. "#f5f5f5" (default is white).</param>
		/// <param name="margin">Optional margin in points around the diagram (default is 20).</param>
		/// <param name="quality">Optional JPEG quality from 0.0 to 1.0 (default is 0.95).</param>
		/// <remarks>
		/// This method rasterizes the SVG diagram to a canvas, then generates a single page PDF with the image embedded as a JPEG.
		/// The page size is the size of the diagram (1 pixel = 1 point) plus the <paramref name="margin"/>.
		/// The PDF is created client-side and downloaded directly in the browser, the file is not sent to the server.
		/// Use <see cref="ExportToPdfAsync"/> to receive the PDF on the server.
		/// </remarks>
		/// <example>
		/// Downloading the diagram as a PDF file:
		/// <code><![CDATA[
		/// // Basic usage with default options.
		/// mermaid.DownloadPdf("flowchart.pdf");
		/// 
		/// // Custom options for higher quality and different background.
		/// mermaid.DownloadPdf("diagram.pdf", scale: 3, backgroundColor: "#f5f5f5", margin: 30);
		/// ]]></code>
		/// </example>
		public void DownloadPdf(
			string fileName = "diagram.pdf",
			double? scale = null,
			string backgroundColor = null,
			double? margin = null,
			double? quality = null)
		{
			this.Call("downloadPdf", fileName, new
			{
				scale,
				backgroundColor,
				margin,
				quality
			});
		}

		/// <summary>
		/// Exports the currently rendered diagram as a PDF and returns it as a <see cref="MemoryStream"/>.
		/// </summary>
		/// <param name="scale">Optional scale factor for image quality (default is 2 for high DPI).</param>
		/// <param name="backgroundColor">Optional CSS background color for the PDF, i.e. "#f5f5f5" (default is white).</param>
		/// <param name="margin">Optional margin in points around the diagram (default is 20).</param>
		/// <param name="quality">Optional JPEG quality from 0.0 to 1.0 (default is 0.95).</param>
		/// <returns>A <see cref="MemoryStream"/> containing the PDF bytes. The caller is responsible for disposing the stream.</returns>
		/// <exception cref="InvalidOperationException">The data returned by the browser is not valid base64.</exception>
		/// <remarks>
		/// This method converts the SVG diagram to a canvas, generates a PDF with the image embedded,
		/// and returns the PDF as a memory stream. The PDF generation happens client-side in the browser,
		/// and the bytes are transferred back to the server.
		/// </remarks>
		/// <example>
		/// Saving the PDF on the server or reading its bytes:
		/// <code><![CDATA[
		/// // Export to memory stream for further processing.
		/// using (var pdfStream = await mermaid.ExportToPdfAsync())
		/// {
		///     // Save to file
		///     using (var fileStream = File.Create("diagram.pdf"))
		///     {
		///         pdfStream.CopyTo(fileStream);
		///     }
		/// }
		/// 
		/// // Export with custom options.
		/// using (var pdfStream = await mermaid.ExportToPdfAsync(scale: 3, backgroundColor: "#f5f5f5"))
		/// {
		///     // Send via email, save to database, etc.
		///     byte[] pdfBytes = pdfStream.ToArray();
		/// }
		/// ]]></code>
		/// </example>
		public async Task<MemoryStream> ExportToPdfAsync(
			double? scale = null,
			string backgroundColor = null,
			double? margin = null,
			double? quality = null)
		{
			// call the JavaScript function that generates PDF and returns base64.
			string result = await this.CallAsync("getPdf", new
			{
				scale,
				backgroundColor,
				margin,
				quality
			});

			try
			{
				return new MemoryStream(Convert.FromBase64String(result));
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Failed to generate PDF: Invalid base64 data.", ex);
			}
		}

		/// <summary>
		/// Validates Mermaid syntax in the browser without rendering.
		/// </summary>
		/// <param name="diagram">Mermaid source text to validate. It doesn't need to be the current <see cref="Diagram"/>.</param>
		/// <returns>A <see cref="ValidationResult"/> containing the validation outcome and optional error details.</returns>
		/// <remarks>
		/// Validation happens in the browser using Mermaid's parser (<c>mermaid.parse()</c>); the widget's diagram
		/// is not changed and the <see cref="Error"/> event is not fired. Check <see cref="ValidationResult.Valid"/> for the outcome.
		/// </remarks>
		/// <example>
		/// Validating user input before applying it:
		/// <code><![CDATA[
		/// var result = await mermaid.ValidateAsync(this.textBoxSource.Text);
		/// if (result.Valid)
		///     mermaid.Diagram = this.textBoxSource.Text;
		/// else
		///     Wisej.Web.MessageBox.Show(result.Message ?? "Diagram is invalid.");
		/// ]]></code>
		/// </example>
		public async Task<ValidationResult> ValidateAsync(string diagram)
			=> new ValidationResult(await this.CallAsync("validate", diagram));

		#endregion

		#region Wisej Implementation

		/// <inheritdoc/>
		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			if (e == null)
				return;

			switch (e.Type)
			{
				case "elementClick":
					ProcessElementClickWebEvent(e);
					break;

				case "mermaidError":
					ProcessMermaidErrorWebEvent(e);
					break;

				default:
					base.OnWidgetEvent(e);
					break;
			}
		}

		// Handles clicks on the inner elements of the Mermaid diagram.
		private void ProcessElementClickWebEvent(WidgetEventArgs e)
		{
			dynamic data = e.Data;
			var element = data?.text ?? data?.element ?? "";

			if (!string.IsNullOrEmpty(element))
			{
				int x = data?.x ?? 0;
				int y = data?.y ?? 0;
				var location = PointToClient(new Point(x, y));
				var button = (data?.button ?? 0) switch
				{
					0 => MouseButtons.Left,
					1 => MouseButtons.Middle,
					2 => MouseButtons.Right,
					_ => MouseButtons.None
				};

				OnElementClick(new ElementClickEventArgs(element, button, 1, location, data));
			}
		}

		// Handles clicks on the inner elements of the Mermaid diagram.
		private void ProcessMermaidErrorWebEvent(WidgetEventArgs e)
		{
			dynamic data = e.Data;
			string message = data?.message;
			object error = data?.error;

			if (string.IsNullOrWhiteSpace(message))
				message = "Invalid Mermaid diagram.";

			OnError(new MermaidErrorEventArgs(message, error));
		}

		#endregion
	}
}
