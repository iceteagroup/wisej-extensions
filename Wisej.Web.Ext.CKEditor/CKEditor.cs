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
using System.Linq;
using System.Runtime.CompilerServices;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.CKEditor
{
	/// <summary>
	/// CKEditor (formerly FCKeditor) is an open source WYSIWYG text editor designed to bring common word processor
	/// features directly to web pages, simplifying their content creation.
	///
	/// from: http://ckeditor.com/
	/// </summary>
	/// <example>
	/// Creating an editor in code, loading HTML content and reacting to changes:
	/// <code><![CDATA[
	/// var editor = new Wisej.Web.Ext.CKEditor.CKEditor
	/// {
	///     Dock = DockStyle.Fill,
	///     ShowFooter = false,
	///     Text = "<p>Hello <strong>World</strong>!</p>"
	/// };
	///
	/// editor.TextChanged += (s, e) => this.buttonSave.Enabled = true;
	/// this.Controls.Add(editor);
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Control), "RichTextBox.bmp")]
	[DefaultProperty("Text")]
	[DefaultEvent("TextChanged")]
	[ApiCategory("CKEditor")]
	public class CKEditor : Widget, IWisejControl
	{
		// indicates that the control is ready to update its content.
		private bool initialized;

		#region Events

		/// <summary>
		/// Fired after the editor executes a command.
		/// </summary>
		/// <remarks>
		/// The event is fired for commands executed from the toolbar, from keyboard shortcuts (i.e. Ctrl+B) and from
		/// <see cref="ExecCommand"/>. <see cref="P:Wisej.Web.Ext.CKEditor.CommandEventArgs.Command"/> is the CKEditor command name,
		/// i.e. "bold", "save", "undo". The "applyFormatting" command is not reported because CKEditor fires it too often.
		/// </remarks>
		/// <example>
		/// Handling the Save and Source buttons of the toolbar on the server:
		/// <code><![CDATA[
		/// this.ckEditor1.Command += ckEditor1_Command;
		///
		/// private void ckEditor1_Command(object sender, CommandEventArgs e)
		/// {
		///     switch (e.Command)
		///     {
		///         case "save":
		///             SaveDocument(this.ckEditor1.Text);
		///             AlertBox.Show("Document saved.");
		///             break;
		///
		///         case "source":
		///             this.labelStatus.Text = "Switched between design and HTML source view.";
		///             break;
		///     }
		/// }
		/// ]]></code>
		/// </example>
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

		/// <summary>
		/// Fired after a link is clicked in the editor.
		/// </summary>
		/// <remarks>
		/// <see cref="P:Wisej.Web.Ext.CKEditor.LinkClickedEventArgs.Link"/> is the value of the href attribute as it's written
		/// in the HTML, so it can be a relative URL, an anchor (#section) or a "mailto:" link. Clicking a link in the editor doesn't
		/// navigate to it, use this event to open it. The event is fired only when the clicked element itself has an href attribute:
		/// clicking formatted text inside a link (i.e. a bold word) doesn't fire it. It's not fired at design time.
		/// </remarks>
		/// <example>
		/// Opening the clicked link in a new browser tab:
		/// <code><![CDATA[
		/// this.ckEditor1.LinkClicked += ckEditor1_LinkClicked;
		///
		/// private void ckEditor1_LinkClicked(object sender, LinkClickedEventArgs e)
		/// {
		///     if (e.Link.StartsWith("http://") || e.Link.StartsWith("https://"))
		///         Application.Navigate(e.Link, "_blank");
		/// }
		/// ]]></code>
		/// </example>
		public event LinkClickedEventHandler LinkClicked
		{
			add { base.AddHandler(nameof(LinkClicked), value); }
			remove { base.RemoveHandler(nameof(LinkClicked), value); }
		}

		/// <summary>
		/// Fires the LinkClicked event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnLinkClicked(LinkClickedEventArgs e)
		{
			((LinkClickedEventHandler)base.Events[nameof(LinkClicked)])?.Invoke(this, e);
		}

		#endregion Events

		#region Properties

		/// <summary>
		/// Returns or sets the HTML text associated with this control.
		/// </summary>
		/// <returns>The HTML text associated with this control.</returns>
		/// <remarks>
		/// The value is HTML markup, not plain text. Setting it replaces the content of the editor and fires the
		/// <see cref="E:Wisej.Web.Control.TextChanged"/> event. Changes made by the user are sent back to the server
		/// when the editor loses the focus or with the next request that carries the state of the control.
		/// CKEditor may normalize the markup (for example, wrapping loose text in a paragraph), so the value read back
		/// can differ from the value that was assigned.
		/// </remarks>
		/// <example>
		/// Loading HTML into the editor and reading it back when it changes:
		/// <code><![CDATA[
		/// this.ckEditor1.Text = "<h1>Title</h1><p>Some <strong>bold</strong> text.</p>";
		///
		/// private void ckEditor1_TextChanged(object sender, EventArgs e)
		/// {
		///     this.labelSize.Text = $"{this.ckEditor1.Text.Length} characters of HTML";
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
		/// Returns or sets whether the control is enabled.
		/// </summary>
		/// <remarks>
		/// This property hides <see cref="P:Wisej.Web.Control.Enabled"/> and is mapped to the inverse of <see cref="ReadOnly"/>:
		/// setting <see cref="Enabled"/> to false doesn't disable the control, it makes the editor read-only.
		/// Reading <see cref="Enabled"/> always returns the opposite of <see cref="ReadOnly"/>.
		/// </remarks>
		/// <example>
		/// The two statements below have the same effect:
		/// <code><![CDATA[
		/// this.ckEditor1.Enabled = false;
		///
		/// // is the same as:
		/// this.ckEditor1.ReadOnly = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("Returns or sets whether the control is enabled.")]
		public new bool Enabled
		{
			get { return !this.ReadOnly; }
			set
			{
				if (this.ReadOnly != !value)
					this.ReadOnly = !value;
			}
		}

		/// <summary>
		/// Returns or sets whether the user can interact with the editor.
		/// </summary>
		/// <remarks>
		/// When true, the content cannot be edited and the editing commands in the toolbar are disabled.
		/// Changing this property doesn't re-create the editor. <see cref="Enabled"/> returns the inverse of this property.
		/// </remarks>
		[DesignerActionList]
		[DefaultValue(false)]
		[Description("Returns or sets whether the user can interact with the editor.")]
		public bool ReadOnly
		{
			get { return this._readOnly; }

			set
			{
				if (this._readOnly != value)
				{
					this._readOnly = value;
					Call("setReadOnly", value);
				}
			}
		}

		private bool _readOnly = false;

		/// <summary>
		/// Shows or hides the toolbar panel.
		/// </summary>
		/// <remarks>
		/// Changing this property after the editor is displayed re-creates the editor on the client using the current
		/// <see cref="Text"/>.
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
		/// Shows or hides the footer panel.
		/// </summary>
		/// <remarks>
		/// The footer panel shows the path of the HTML elements at the caret position. Hiding it removes the
		/// "elementspath" plugin from the editor configuration. Changing this property after the editor is displayed
		/// re-creates the editor on the client using the current <see cref="Text"/>.
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
		/// Returns or sets the CKEDITOR.config to use for this instance of the editor: <see href="http://docs.ckeditor.com/#!/api/CKEDITOR.config"/>.
		/// Use the toolbar configuration tool at <see href="http://ckeditor.com/tmp/4.5.0-beta/ckeditor/samples/toolbarconfigurator/index.html#basic"/>
		/// and copy the json into the Options definition.
		/// </summary>
		/// <remarks>
		/// The members of this dynamic object are sent to the client as-is and passed to CKEditor as its configuration object,
		/// so the member names must match the CKEDITOR.config names exactly (they are case sensitive).
		/// <para>
		/// Assigning a new object to <see cref="Options"/> re-creates the editor. Setting a member of the existing object
		/// (i.e. <c>ckEditor1.Options.language = "fr"</c>) doesn't notify the control: call <see cref="Update"/>
		/// afterwards when the editor is already displayed.
		/// </para>
		/// <para>
		/// The control always sets <c>resize_enabled</c> to false, sets <c>allowedContent</c> to true when it's not specified,
		/// and replaces <c>versionCheck</c> with the value of <see cref="VersionCheck"/>. The font list is set by <see cref="FontNames"/>.
		/// </para>
		/// </remarks>
		/// <example>
		/// Configuring the language, the color and a custom toolbar:
		/// <code><![CDATA[
		/// dynamic options = new Wisej.Core.DynamicObject();
		/// options.language = "de";
		/// options.uiColor = "#E8F0FE";
		/// options.removeButtons = "Subscript,Superscript";
		/// options.toolbar = new object[]
		/// {
		///     new { name = "basicstyles", items = new[] { "Bold", "Italic", "Underline", "-", "RemoveFormat" } },
		///     new { name = "paragraph", items = new[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent" } },
		///     "/",
		///     new { name = "links", items = new[] { "Link", "Unlink" } }
		/// };
		///
		/// this.ckEditor1.Options = options;
		/// ]]></code>
		/// Changing a single option of an editor that is already displayed:
		/// <code><![CDATA[
		/// this.ckEditor1.Options.language = "fr";
		/// this.ckEditor1.Update();
		/// ]]></code>
		/// </example>
		[MergableProperty(false)]
		[Editor("Wisej.Design.DynamicObjectEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual new dynamic Options
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
		/// <remarks>
		/// The names are passed to CKEditor's <c>font_names</c> configuration. Each entry can be a single font name or
		/// a "Display Name/font-family list" pair, which shows the display name in the font list and applies the font-family list
		/// to the text. Set it to null or to an empty array to use the built-in font list of CKEditor.
		/// The default value is <see cref="DefaultFontNames"/>. Changing this property re-creates the editor.
		/// </remarks>
		/// <example>
		/// Showing a mix of plain font names and named font stacks:
		/// <code><![CDATA[
		/// this.ckEditor1.FontNames = new[]
		/// {
		///     "Arial",
		///     "Georgia",
		///     "Sans Serif/Helvetica Neue, Helvetica, Arial, sans-serif",
		///     "Monospace/Consolas, Courier New, monospace"
		/// };
		/// ]]></code>
		/// </example>
		[DesignerActionList]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("Returns or sets the font names to display in the toolbar.")]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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
		/// Returns the default font names to display in the toolbar: Verdana, Arial, Georgia and Trebuchet MS.
		/// </summary>
		/// <remarks>
		/// The returned array is shared by all the <see cref="CKEditor"/> controls. To use a different list, assign a new array
		/// to <see cref="FontNames"/> instead of changing the items of this array.
		/// </remarks>
		/// <example>
		/// Adding a font to the default list:
		/// <code><![CDATA[
		/// this.ckEditor1.FontNames = CKEditor.DefaultFontNames.Concat(new[] { "Roboto" }).ToArray();
		/// ]]></code>
		/// </example>
		public static string[] DefaultFontNames
		{
			get
			{
				return _defaultFontNames;
			}
		}

		private static string[] _defaultFontNames = new[] { "Verdana", "Arial", "Georgia", "Trebuchet MS" };

		/// <summary>
		/// Returns or sets the version check alert.
		/// </summary>
		/// <remarks>
		/// Maps to CKEditor's <c>versionCheck</c> configuration. When true, CKEditor checks whether the loaded version
		/// is up to date and secure and shows a notification in the editor when it isn't. This value always replaces
		/// <c>versionCheck</c> in <see cref="Options"/>. Changing this property re-creates the editor.
		/// </remarks>
		[DefaultValue(false)]
		public bool VersionCheck
		{
			get
			{
				return this._versionCheck;
			}
			set
			{
				if (this._versionCheck != value)
					this._versionCheck = value;

				Update();
			}
		}

		private bool _versionCheck;

		/// <summary>
		/// Collection of external (local) plugins to register with the CKEditor control.
		/// </summary>
		/// <remarks>
		/// Each plugin is registered with <c>CKEDITOR.plugins.addExternal()</c> before the editor is created. Registering a plugin
		/// doesn't load it: add its <see cref="P:Wisej.Web.Ext.CKEditor.ExternalPlugin.Name"/> to the <c>extraPlugins</c>
		/// option in <see cref="Options"/> (a comma separated list) to use it. Plugins with an empty
		/// <see cref="P:Wisej.Web.Ext.CKEditor.ExternalPlugin.Name"/>, <see cref="P:Wisej.Web.Ext.CKEditor.ExternalPlugin.Url"/>
		/// or <see cref="P:Wisej.Web.Ext.CKEditor.ExternalPlugin.FileName"/> are ignored. Relative URLs are resolved against the URL of the application.
		/// </remarks>
		/// <example>
		/// Registering and loading a plugin deployed with the application in /Plugins/timestamp/plugin.js:
		/// <code><![CDATA[
		/// this.ckEditor1.Options.extraPlugins = "timestamp";
		/// this.ckEditor1.ExternalPlugins = new[]
		/// {
		///     new ExternalPlugin
		///     {
		///         Name = "timestamp",
		///         Url = "Plugins/timestamp/",
		///         FileName = "plugin.js"
		///     }
		/// };
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

		#endregion Properties

		#region Methods

		/// <summary>
		/// Executes commands to manipulate the contents of the editable region.
		/// </summary>
		/// <param name="command">The name of the CKEditor command to execute, i.e. "bold", "undo", "selectAll", "removeFormat". Command names are case sensitive
		/// and depend on the plugins loaded in the editor. See <see href="https://ckeditor.com/docs/ckeditor4/latest/api/CKEDITOR_editor.html#method-execCommand"/>.</param>
		/// <param name="argument">For commands which require an input argument, this is a string providing that information. Specify null if no argument is needed.</param>
		/// <remarks>
		/// Most commands affect the current selection (bold, italics, etc.), while others insert new elements (adding a link) or
		/// affect an entire line (indenting). If the editor is not ready yet, the command is executed as soon as it's initialized.
		/// <para>
		/// The <see cref="Text"/> property is updated asynchronously shortly after the command runs, so the new HTML is not
		/// available in the same server call that invoked <see cref="ExecCommand"/>. Executing a command also fires the <see cref="Command"/> event.
		/// </para>
		/// </remarks>
		/// <example>
		/// Executing commands from buttons outside of the editor:
		/// <code><![CDATA[
		/// private void buttonBold_Click(object sender, EventArgs e)
		/// {
		///     this.ckEditor1.ExecCommand("bold");
		/// }
		///
		/// private void buttonClearFormat_Click(object sender, EventArgs e)
		/// {
		///     this.ckEditor1.ExecCommand("selectAll");
		///     this.ckEditor1.ExecCommand("removeFormat");
		/// }
		/// ]]></code>
		/// </example>
		public void ExecCommand(string command, string argument = null)
		{
			Call("execCommand", command, argument);
		}

		/// <summary>
		/// Enables the specified command on the toolbar.
		/// </summary>
		/// <param name="command">The name of the command to enable: i.e. "save", "bold", ...</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="command"/> is null.</exception>
		/// <remarks>
		/// The command name is the CKEditor command name (case sensitive), not the name of the toolbar button. An empty string
		/// and unknown commands are ignored. The state is remembered and applied again when the editor is re-created,
		/// for example when the page is refreshed. Use <see cref="DisableCommand"/> to disable the command.
		/// </remarks>
		/// <example>
		/// Enabling the Save button only after the content has changed:
		/// <code><![CDATA[
		/// private void Form1_Load(object sender, EventArgs e)
		/// {
		///     this.ckEditor1.DisableCommand("save");
		/// }
		///
		/// private void ckEditor1_TextChanged(object sender, EventArgs e)
		/// {
		///     this.ckEditor1.EnableCommand("save");
		/// }
		/// ]]></code>
		/// </example>
		public void EnableCommand(string command)
		{
			if (command == null)
				throw new ArgumentNullException("command");

			if (command == "")
				return;

			this._commands = this._commands ?? new Dictionary<string, bool>();
			this._commands[command] = true;

			Call("enableCommand", command, true);
		}

		/// <summary>
		/// Disables the specified command on the toolbar.
		/// </summary>
		/// <param name="command">The name of the command to disable: i.e. "save", "bold", ...</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="command"/> is null.</exception>
		/// <remarks>
		/// The command name is the CKEditor command name (case sensitive), not the name of the toolbar button. An empty string
		/// and unknown commands are ignored. The state is remembered and applied again when the editor is re-created,
		/// for example when the page is refreshed. Use <see cref="EnableCommand"/> to enable the command again.
		/// To prevent all editing, use <see cref="ReadOnly"/> instead.
		/// </remarks>
		/// <example>
		/// Preventing the user from editing the HTML source or printing the document:
		/// <code><![CDATA[
		/// this.ckEditor1.DisableCommand("source");
		/// this.ckEditor1.DisableCommand("print");
		/// ]]></code>
		/// </example>
		public void DisableCommand(string command)
		{
			if (command == null)
				throw new ArgumentNullException("command");

			if (command == "")
				return;

			this._commands = this._commands ?? new Dictionary<string, bool>();
			this._commands[command] = false;

			Call("enableCommand", command, false);
		}

		// keeps the list of commands that have been enabled/disabled in this instance
		// of the CKEditor control. we must re-send the "enableCommand" call when the
		// page is refreshed.
		private Dictionary<string, bool> _commands;

		/// <summary>
		/// Updates the client component.
		/// </summary>
		/// <remarks>
		/// Overridden to apply again the commands enabled or disabled with <see cref="EnableCommand"/> and <see cref="DisableCommand"/>
		/// when the client widget is re-created. Call this method after changing members of the existing <see cref="Options"/> object,
		/// which doesn't notify the control on its own. When the configuration has changed, the editor is re-created
		/// on the client using the current <see cref="Text"/>.
		/// </remarks>
		/// <example>
		/// Applying new options to an editor that is already displayed:
		/// <code><![CDATA[
		/// this.ckEditor1.Options.language = "fr";
		/// this.ckEditor1.Options.uiColor = "#FFF3E0";
		/// this.ckEditor1.Update();
		/// ]]></code>
		/// </example>
		public override void Update()
		{
			IWisejControl me = this;

			if (me.IsNew)
				this.initialized = false;

			if (me.IsNew && this._commands != null)
			{
				var enabled = this._commands.Where(o => o.Value == true).Select(o => o.Key);
				if (enabled.Count() > 0)
					Call("enableCommand", enabled, true);

				var disabled = this._commands.Where(o => o.Value == false).Select(o => o.Key);
				if (disabled.Count() > 0)
					Call("enableCommand", disabled, false);
			}

			base.Update();
		}

		#endregion Methods

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
			get { return this.AppearanceKey ?? "ckeditor"; }
		}

		/// <summary>
		/// Overridden to create our initialization script.
		/// </summary>
		/// <remarks>
		/// The script is generated from the properties of the control. This property is used by the framework
		/// and is not meant to be set by the application: assigned values are ignored.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
			set { }
		}

		/// <summary>
		/// Returns or sets the base url for the CKEditor installation
		/// </summary>
		/// <remarks>
		/// This value is shared by all the <see cref="CKEditor"/> controls in the application. The default is
		/// "https://cdn.ckeditor.com/4.21.0/full-all/". Use it to load a different version or build of CKEditor, or a copy
		/// hosted with the application. The URL must end with "/" since "ckeditor.js" is appended to it. Set it once at startup,
		/// before any <see cref="CKEditor"/> control is created.
		/// </remarks>
		/// <example>
		/// Using a copy of CKEditor deployed with the application in the /ckeditor folder:
		/// <code><![CDATA[
		/// static void Main()
		/// {
		///     Wisej.Web.Ext.CKEditor.CKEditor.BaseUrl = "/ckeditor/";
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
		private static string _baseUrl = "https://cdn.ckeditor.com/4.21.0/full-all/";

		/// <summary>
		/// Overridden to return our list of script resources.
		/// </summary>
		/// <remarks>
		/// Returns the "ckeditor.js" package loaded from <see cref="BaseUrl"/>. This property is used by the framework to load the
		/// CKEditor library and it's not meant to be used by the application.
		/// </remarks>
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
						Name = "ckeditor.js",
						Source = $"{CKEditor.BaseUrl}ckeditor.js"
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
			string script = GetResourceString("Wisej.Web.Ext.CKEditor.JavaScript.startup.js");

			options.config = this.Options;
			options.config.versionCheck = this.VersionCheck;

			options.fonts = this.FontNames;
			options.basePath = CKEditor.BaseUrl;
			options.showFooter = this.ShowFooter;
			options.showToolbar = this.ShowToolbar;
			options.externalPlugins = this.ExternalPlugins;

			script = script.Replace("$options", options.ToString());

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

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Control.WidgetEvent" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.WidgetEventArgs" /> that contains the event data. </param>
		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			switch (e.Type)
			{
				case "load":
					ProcessLoadEvent();
					break;

				case "changeText":
					this._text = e.Data ?? "";
					break;

				case "command":
					ProcessCommandWebEvent(e);
					break;

				case "focus":
					ProcessFocusWebEvent(e);
					break;

				case "linkClick":
					ProcessLinkClickedWebEvent(e);
					break;
			}
			base.OnWidgetEvent(e);
		}

		// processes the initialization event from the client.
		private void ProcessLoadEvent()
		{
			this.initialized = true;

			Call("setReadOnly", this.ReadOnly);
			Call("setText", TextUtils.EscapeText(this.Text, true));
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

		private void ProcessLinkClickedWebEvent(WidgetEventArgs e)
		{
			OnLinkClicked(new LinkClickedEventArgs(e.Data));
		}

		#endregion Wisej Implementation
	}
}