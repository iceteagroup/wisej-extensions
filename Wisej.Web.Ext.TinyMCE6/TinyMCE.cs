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
using System.Runtime.CompilerServices;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.TinyMCE6
{
	/// <summary>
	/// TinyMCE is a platform-independent, browser-based WYSIWYG HTML editor control.
	/// </summary>
	/// <remarks>
	/// This control wraps TinyMCE 6 (<see href="https://www.tiny.cloud/"/>). By default the library is loaded from
	/// the CDN set in <see cref="BaseUrl"/>. The HTML content is exchanged with the server through the <see cref="Text"/> property
	/// and the editor is configured using the <see cref="Options"/> property, which is passed to <c>tinymce.init()</c>.
	/// </remarks>
	[ApiCategory("TinyMCE")]
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Control), "RichTextBox.bmp")]
	[DefaultProperty("Text")]
	[DefaultEvent("TextChanged")]
	public class TinyMCE : Widget, IWisejControl
	{
		// indicates that the control is ready to update its content.
		private bool initialized;

		#region Events

		/// <summary>
		/// Fired after the editor executes a command.
		/// </summary>
		public event CommandEventHandler Command
		{
			add { base.AddHandler(nameof(Command), value); }
			remove { base.RemoveHandler(nameof(Command), value); }
		}

		/// <summary>
		/// Fires the Command event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnCommand(CommandEventArgs e)
		{
			((CommandEventHandler)base.Events[nameof(Command)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets whether the editor is enabled.
		/// </summary>
		/// <remarks>
		/// When disabled, the editor is switched to TinyMCE's "readonly" mode; when enabled it's switched back to "design" mode.
		/// </remarks>
		public new bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if (this._enabled != value)
				{
					this._enabled = value;
					OnEnabledChanged(EventArgs.Empty);
					Call("setEnabled", value);
				}
			}
		}
		private bool _enabled = true;

		/// <summary>
		/// Returns or sets the HTML text associated with this control.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Setting the text replaces the content of the editor. If the editor is not loaded yet, the content is applied as soon as it's ready.
		/// </para>
		/// <para>
		/// Changes made by the user are sent back to the server with the next request after the editor changes, a key is pressed
		/// or the editor loses the focus. <see cref="ExecCommand(string, string)"/> updates the text shortly after the command is executed.
		/// <c>TextChanged</c> is fired every time the value changes.
		/// </para>
		/// </remarks>
		/// <example>
		/// Loading and saving the HTML content:
		/// <code><![CDATA[
		/// private void Form1_Load(object sender, EventArgs e)
		/// {
		///     this.tinyMCE1.Text = "<h1>Monthly Report</h1><p>Write the summary here.</p>";
		/// }
		///
		/// private void buttonSave_Click(object sender, EventArgs e)
		/// {
		///     SaveDocument(this.tinyMCE1.Text);
		/// }
		/// ]]></code>
		/// </example>
		[DefaultValue("")]
		public override string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				value = value ?? string.Empty;

				if (this._text != value)
				{
					this._text = value;
					OnTextChanged(EventArgs.Empty);
					Call("setText", TextUtils.EscapeText(value, true));
				}
			}
		}
		private string _text = "";

		/// <summary>
		/// Returns or sets whether the menu bar is visible.
		/// </summary>
		/// <remarks>
		/// When set to false it overrides the <c>menubar</c> setting in <see cref="Options"/>. Changing this property recreates the editor.
		/// </remarks>
		[DesignerActionList]
		[DefaultValue(true)]
		[Description("Shows or hides the menu panel.")]
		public bool ShowMenuBar
		{
			get { return this._showMenuBar; }

			set
			{
				if (this._showMenuBar != value)
				{
					this._showMenuBar = value;
					Update();
				}
			}
		}
		private bool _showMenuBar = true;

		/// <summary>
		/// Returns or sets whether the toolbar is visible.
		/// </summary>
		/// <remarks>
		/// When set to false it overrides the <c>toolbar</c> setting in <see cref="Options"/>. Changing this property recreates the editor.
		/// </remarks>
		[DesignerActionList]
		[DefaultValue(true)]
		[Description("Shows or hides the toolbar panel.")]
		public bool ShowToolbar
		{
			get { return this._showToolbar; }

			set
			{
				if (this._showToolbar != value)
				{
					this._showToolbar = value;
					Update();
				}
			}
		}
		private bool _showToolbar = true;

		/// <summary>
		/// Returns or sets whether the footer (status bar) is visible.
		/// </summary>
		/// <remarks>
		/// When set to false it overrides the <c>statusbar</c> setting in <see cref="Options"/>. Changing this property recreates the editor.
		/// </remarks>
		[DesignerActionList]
		[DefaultValue(true)]
		[Description("Shows or hides the footer panel.")]
		public bool ShowFooter
		{
			get { return this._showFooter; }

			set
			{
				if (this._showFooter != value)
				{
					this._showFooter = value;
					Update();
				}
			}
		}
		private bool _showFooter = true;

		/// <summary>
		/// Returns or sets the configuration to use for this instance of the editor: <see href="https://www.tinymce.com/docs/configure/"/>.
		/// </summary>
		[MergableProperty(false)]
		[Editor("Wisej.Design.DynamicObjectEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		/// <summary>
		/// Returns or sets the configuration to use for this instance of the editor: <see href="https://www.tiny.cloud/docs/tinymce/6/"/>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The object is serialized and passed to <c>tinymce.init()</c>. Use the TinyMCE option names, i.e. <c>plugins</c>, <c>toolbar</c>,
		/// <c>content_style</c>. The <c>selector</c> and <c>resize</c> options are always set by the control.
		/// </para>
		/// <para>
		/// Assigning the property updates the widget. When changing the members of the existing object, call <see cref="Update"/>
		/// to apply the new configuration. TinyMCE cannot be reconfigured after creation, so the editor is always destroyed and recreated.
		/// </para>
		/// </remarks>
		/// <example>
		/// Configuring the plugins and the toolbar:
		/// <code><![CDATA[
		/// public Form1()
		/// {
		///     InitializeComponent();
		///
		///     this.tinyMCE1.Options.plugins = "lists link image table";
		///     this.tinyMCE1.Options.toolbar = "undo redo | bold italic underline | bullist numlist | link image";
		///     this.tinyMCE1.Options.content_style = "body { font-family: Arial; font-size: 14px; }";
		///     this.tinyMCE1.Update();
		/// }
		/// ]]></code>
		/// </example>
		public new virtual dynamic Options
		{
			get
			{
				return this._options;
			}
			set
			{
				this._options = value;
				Update();
			}
		}
		private dynamic _options = new DynamicObject();

		/// <summary>
		/// Returns or sets the font names to display in the toolbar.
		/// </summary>
		[DesignerActionList]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("Returns or sets the font names to display in the toolbar.")]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		/// <summary>
		/// Returns or sets the font names to display in the toolbar.
		/// </summary>
		/// <remarks>
		/// The names are sent to the client with the widget options, but the startup script doesn't apply them to the editor.
		/// To change the list of fonts, set the TinyMCE <c>font_family_formats</c> option in <see cref="Options"/>.
		/// </remarks>
		/// <example>
		/// Setting the fonts using the TinyMCE option:
		/// <code><![CDATA[
		/// this.tinyMCE1.Options.font_family_formats = "Arial=arial,helvetica,sans-serif; Georgia=georgia,serif; Verdana=verdana,geneva";
		/// this.tinyMCE1.Update();
		/// ]]></code>
		/// </example>
		public string[] FontNames
		{
			get { return this._fontNames; }
			set
			{
				if (this._fontNames != value)
				{
					this._fontNames = value;
					Update();
				}
			}
		}
		private string[] _fontNames = DefaultFontNames;

		private bool ShouldSerializeFontNames()
		{
			return this._fontNames != DefaultFontNames;
		}

		private void ResetFontNames()
		{
			this._fontNames = DefaultFontNames;
			Update();
		}

		/// <summary>
		/// Returns the default font names used to initialize <see cref="FontNames"/>.
		/// </summary>
		/// <remarks>
		/// The default names are "Verdana", "Arial", "Georgia" and "Trebuchet MS".
		/// </remarks>
		public static string[] DefaultFontNames
		{
			get
			{
				return _defaultFontNames;
			}
		}
		private static string[] _defaultFontNames = new[] { "Verdana", "Arial", "Georgia", "Trebuchet MS" };

		/// <summary>
		/// Returns or sets the collection of external (local) plugins to register with the TinyMCE control.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Each plugin is loaded from <c>Url</c> + "/" + <c>FileName</c> using <c>tinymce.PluginManager.load()</c> before the editor is created.
		/// Plugins with an empty <see cref="ExternalPlugin.Name"/>, <see cref="ExternalPlugin.Url"/> or <see cref="ExternalPlugin.FileName"/>
		/// are ignored. The plugin must also be listed in the <c>plugins</c> option in <see cref="Options"/> to be activated.
		/// </para>
		/// <para>
		/// Assigning the property recreates the editor.
		/// </para>
		/// </remarks>
		/// <example>
		/// Registering a plugin deployed with the application:
		/// <code><![CDATA[
		/// this.tinyMCE1.ExternalPlugins = new[]
		/// {
		///     new ExternalPlugin { Name = "wordcounter", Url = "Plugins/wordcounter", FileName = "plugin.js" }
		/// };
		/// this.tinyMCE1.Options.plugins = "lists link wordcounter";
		/// this.tinyMCE1.Update();
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ExternalPlugin[] ExternalPlugins
		{
			get
			{
				return this._externalPlugins;
			}
			set
			{
				if (value != this._externalPlugins)
				{
					this._externalPlugins = value;
					Update();
				}
			}
		}
		private ExternalPlugin[] _externalPlugins;

		#endregion

		#region Methods

		/// <summary>
		/// Executes commands to manipulate the contents of the editable region.
		/// </summary>
		/// <param name="command">The name of the command to execute, i.e. "Bold", "Undo", "mceInsertContent". See <see href="https://www.tiny.cloud/docs/tinymce/6/editor-command-identifiers/"/> for a list of commands.</param>
		/// <param name="showDefaultUI">Indicates whether the default user interface (i.e. a dialog) should be shown, when the command has one.</param>
		/// <param name="argument">For commands which require an input argument (such as "mceInsertContent", for which this is the HTML to insert), this is a string providing that information. Specify null if no argument is needed.</param>
		/// <remarks>
		/// <para>
		/// Most commands affect the current selection (bold, italic, etc.), while others insert new elements (adding a link) or
		/// affect an entire line (indenting).
		/// </para>
		/// <para>
		/// The command is executed asynchronously on the client; if the editor is not ready yet, it's executed after it's initialized.
		/// The <see cref="Text"/> property is updated shortly after the command runs.
		/// </para>
		/// </remarks>
		/// <example>
		/// Inserting a link at the caret position:
		/// <code><![CDATA[
		/// private void buttonInsertLink_Click(object sender, EventArgs e)
		/// {
		///     this.tinyMCE1.ExecCommand("mceInsertContent", false, "<a href=\"https://wisej.com\">Wisej.NET</a>");
		/// }
		/// ]]></code>
		/// </example>
		public void ExecCommand(string command, bool showDefaultUI = false, string argument = null)
		{
			Call("execCommand", command, showDefaultUI, argument);
		}

		/// <summary>
		/// Executes commands to manipulate the contents of the editable region.
		/// </summary>
		/// <param name="command">The name of the command to execute, i.e. "Bold", "Undo", "mceInsertContent". See <see href="https://www.tiny.cloud/docs/tinymce/6/editor-command-identifiers/"/> for a list of commands.</param>
		/// <param name="argument">For commands which require an input argument (such as "mceInsertContent", for which this is the HTML to insert), this is a string providing that information. Specify null if no argument is needed.</param>
		/// <remarks>
		/// <para>
		/// Most commands affect the current selection (bold, italic, etc.), while others insert new elements (adding a link) or
		/// affect an entire line (indenting).
		/// </para>
		/// <para>
		/// The command is executed asynchronously on the client without showing its default user interface.
		/// The <see cref="Text"/> property is updated shortly after the command runs.
		/// </para>
		/// </remarks>
		/// <example>
		/// Executing commands from buttons outside of the editor:
		/// <code><![CDATA[
		/// private void buttonBold_Click(object sender, EventArgs e)
		/// {
		///     this.tinyMCE1.ExecCommand("Bold");
		/// }
		///
		/// private void buttonUndo_Click(object sender, EventArgs e)
		/// {
		///     this.tinyMCE1.ExecCommand("Undo");
		/// }
		/// ]]></code>
		/// </example>
		public void ExecCommand(string command, string argument = null)
		{
			ExecCommand(command, false, argument);
		}

		/// <summary>
		/// Updates the client widget with the current properties and <see cref="Options"/>.
		/// </summary>
		/// <remarks>
		/// When the configuration has changed, the TinyMCE editor is destroyed and recreated; the <see cref="Text"/> and the enabled state are
		/// applied again once the new editor is loaded.
		/// </remarks>
		/// <example>
		/// Applying changes made to the <see cref="Options"/> object:
		/// <code><![CDATA[
		/// this.tinyMCE1.Options.menubar = "edit insert format table";
		/// this.tinyMCE1.Update();
		/// ]]></code>
		/// </example>
		public override void Update()
		{
			IWisejControl me = this;

			if (me.IsNew)
				this.initialized = false;

			base.Update();
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Returns the designer timeout for rendering this control.
		/// </summary>
		int IWisejControl.DesignerTimeout { get { return 8000; } }

		/// <summary>
		/// Returns the theme appearance key for this control.
		/// </summary>
		string IWisejControl.AppearanceKey
		{
			get { return this.AppearanceKey ?? "tinymce"; }
		}

		/// <summary>
		/// Overridden to create the initialization script. Setting this property has no effect.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
			set { }
		}

		/// <summary>
		/// Returns or sets the base URL of the TinyMCE installation.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The default is "https://cdnjs.cloudflare.com/ajax/libs/tinymce/6.0.3/". The URL must end with "/" since "tinymce.min.js"
		/// is appended to it. This is a static setting shared by all sessions; set it at startup, before any editor is created.
		/// </para>
		/// <para>
		/// To use a local installation, deploy the TinyMCE files in a folder of the application and set the relative URL.
		/// </para>
		/// </remarks>
		/// <example>
		/// Using a local copy of TinyMCE:
		/// <code><![CDATA[
		/// static void Main()
		/// {
		///     TinyMCE.BaseUrl = "Scripts/tinymce/";
		///
		///     Application.MainPage = new MainPage();
		/// }
		/// ]]></code>
		/// </example>
		public static string BaseUrl
		{
			get { return _baseUrl; }
			set { _baseUrl = value; }
		}
		private static string _baseUrl = "https://cdnjs.cloudflare.com/ajax/libs/tinymce/6.0.3/";

		/// <summary>
		/// Overridden to return the TinyMCE script loaded from <see cref="BaseUrl"/>.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override List<Package> Packages
		{
			get
			{
				if (base.Packages.Count == 0)
				{
					// initialize the loader with the required libraries.
					base.Packages.Add(new Package()
					{
						Name = "tinymce.js",
						Source = $"{BaseUrl}tinymce.min.js"
					});
				}
				return base.Packages;
			}
		}

		// disable inlining or we lose the calling assembly in GetResourceString().
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string BuildInitScript()
		{
			IWisejControl me = this;
			dynamic options = new DynamicObject();
			string script = GetResourceString("Wisej.Web.Ext.TinyMCE6.JavaScript.startup.js");

			options.config = this.Options;

			options.fonts = this.FontNames;
			options.showFooter = this.ShowFooter;
			options.showMenubar = this.ShowMenuBar;
			options.showToolbar = this.ShowToolbar;
			options.externalPlugins = this.ExternalPlugins;
			script = script.Replace("$options", options.ToJSON(WisejSerializerOptions.CamelCase));

			return script;
		}

		/// <summary>
		/// Updates the client component using the state information.
		/// </summary>
		/// <param name="state">Dynamic state object.</param>
		protected override void OnWebUpdate(dynamic state)
		{
			if (state.text != null && this.initialized)
			{
				if (this._text != state.text)
				{
					this._text = state.text;
					OnTextChanged(EventArgs.Empty);
				}
			}

			state.Delete("text");

			base.OnWebUpdate((object)state);
		}

		// Process the load event.
		private void ProcessLoad()
		{
			this.initialized = true;

			Call("setEnabled", this.Enabled);
			Call("setText", TextUtils.EscapeText(this.Text, true));
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Control.WidgetEvent" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.WidgetEventArgs" /> that contains the event data. </param>
		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			switch (e.Type)
			{
				case "load":
					ProcessLoad();
					break;

				case "changeText":
					this.Text = e.Data ?? "";
					break;

				case "command":
					ProcessCommandWebEvent(e);
					break;

				case "focus":
					ProcessFocusWebEvent(e);
					break;
			}
			base.OnWidgetEvent(e);
		}

		// Handles the "focus" event from the client.
		private void ProcessFocusWebEvent(WidgetEventArgs e)
		{
			// HTML editors focus a child IFrame which causes the
			// container widget to lose the focus.
			Focus();

			// activate and bring to top the parent form.
			var form = FindForm();
			if (form != null && !form.Active)
				form.Activate();
		}

		// Handles the "command" event from the client.
		private void ProcessCommandWebEvent(WidgetEventArgs e)
		{
			OnCommand(new CommandEventArgs(e.Data));
		}

		#endregion
	}
}
