///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// Author: Gianluca Pivato
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

namespace Wisej.Web.Ext.TinyEditor
{
	/// <summary>
	/// Represents a lightweight WYSIWYG HTML editor control based on TinyEditor
	/// (<see href="https://github.com/jessegreathouse/TinyEditor"/>).
	/// </summary>
	/// <remarks>
	/// TinyEditor is a simple and standalone JavaScript editor that handles most of the basic formatting needs and
	/// keeps the rendered markup as clean as possible. The HTML content is available in the <see cref="Text"/> property;
	/// the toolbar is configured using <see cref="Toolbar"/> and <see cref="FontNames"/>, and the footer with the
	/// source/WYSIWYG toggle using <see cref="ShowFooter"/>, <see cref="SourceText"/> and <see cref="WysiwygText"/>.
	/// </remarks>
	/// <example>
	/// Creating an editor with a reduced toolbar and saving its content:
	/// <code><![CDATA[
	/// var editor = new TinyEditor
	/// {
	///     Dock = DockStyle.Fill,
	///     ShowFooter = false,
	///     Toolbar = new[] { "bold", "italic", "underline", "|", "orderedlist", "unorderedlist", "|", "link", "unlink" },
	///     Text = "<p>Hello <b>World</b></p>"
	/// };
	/// editor.TextChanged += (s, e) => SaveDocument(editor.Text);
	/// this.Controls.Add(editor);
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Control), "RichTextBox.bmp")]
	[DefaultProperty("Text")]
	[DefaultEvent("TextChanged")]
	[ApiCategory("TinyEditor")]
	public class TinyEditor : Widget, IWisejControl
	{
		// indicates that the control is ready to update its content.
		private bool initialized;

		#region Properties

		/// <summary>
		/// Returns or sets the HTML text associated with this control.
		/// </summary>
		/// <returns>The HTML text associated with this control.</returns>
		/// <remarks>
		/// While the user types, the new text is sent to the server together with the next event fired by the
		/// browser, not on every keystroke; other changes (formatting, paste, <see cref="ExecCommand(string, string)"/>)
		/// update the text right away. <see cref="Control.TextChanged"/> is fired every time the value changes.
		/// A null value is converted to an empty string.
		/// </remarks>
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
		/// Returns or sets whether the toolbar panel is displayed.
		/// </summary>
		/// <remarks>
		/// The buttons in the toolbar are defined by the <see cref="Toolbar"/> property.
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
		/// Returns or sets whether the footer panel is displayed.
		/// </summary>
		/// <remarks>
		/// The footer contains the button that toggles between the WYSIWYG view and the HTML source view,
		/// labeled using <see cref="SourceText"/> and <see cref="WysiwygText"/>.
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
		/// Returns or sets the text to use in the SOURCE button in the footer panel.
		/// </summary>
		/// <remarks>
		/// The button is displayed while the editor is in WYSIWYG mode and switches to the HTML source view.
		/// The default is <c>"source"</c>.
		/// </remarks>
		/// <example>
		/// Localizing the footer toggle button:
		/// <code><![CDATA[
		/// this.tinyEditor1.SourceText = "HTML";
		/// this.tinyEditor1.WysiwygText = "Editor";
		/// ]]></code>
		/// </example>
		[DefaultValue("source")]
		[Description("Returns or sets the text to use in the SOURCE button in the footer panel.")]
		public string SourceText
		{
			get { return this._sourceText; }
			set
			{
				if (this._sourceText != value)
				{
					this._sourceText = value;
					Update();
				}
			}
		}
		private string _sourceText = "source";

		/// <summary>
		/// Returns or sets the text to use in the WYSIWYG button in the footer panel.
		/// </summary>
		/// <remarks>
		/// The button is displayed while the editor is in HTML source mode and switches back to the WYSIWYG view.
		/// The default is <c>"wysiwyg"</c>.
		/// </remarks>
		[DefaultValue("wysiwyg")]
		[Description("Returns or sets the text to use in the WYSIWYG button in the footer panel.")]
		public string WysiwygText
		{
			get { return this._wysiwygText; }
			set
			{
				if (this._wysiwygText != value)
				{
					this._wysiwygText = value;
					Update();
				}
			}
		}
		private string _wysiwygText = "wysiwyg";

		/// <summary>
		/// Returns or sets the buttons to show in the toolbar.
		/// </summary>
		/// <remarks>
		/// The available buttons are: <c>cut</c>, <c>copy</c>, <c>paste</c>, <c>bold</c>, <c>italic</c>, <c>underline</c>,
		/// <c>strikethrough</c>, <c>subscript</c>, <c>superscript</c>, <c>orderedlist</c>, <c>unorderedlist</c>, <c>outdent</c>,
		/// <c>indent</c>, <c>leftalign</c>, <c>centeralign</c>, <c>rightalign</c>, <c>blockjustify</c>, <c>undo</c>, <c>redo</c>,
		/// <c>image</c>, <c>hr</c>, <c>link</c>, <c>unlink</c>, <c>unformat</c> and <c>print</c>, and the drop-downs <c>font</c>
		/// (see <see cref="FontNames"/>), <c>size</c> and <c>style</c>. Use <c>"|"</c> to insert a divider and <c>"n"</c>
		/// to start a new toolbar row. The default is <see cref="DefaultToolbar"/>.
		/// </remarks>
		/// <example>
		/// Defining a two-row toolbar:
		/// <code><![CDATA[
		/// this.tinyEditor1.Toolbar = new[] {
		///     "undo", "redo", "|", "bold", "italic", "underline", "n",
		///     "font", "size", "style", "|", "leftalign", "centeralign", "rightalign"
		/// };
		/// ]]></code>
		/// </example>
		[DesignerActionList]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("Returns or sets the buttons to show in the toolbar.")]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string[] Toolbar
		{
			get { return this._toolbar; }
			set
			{
				if (this._toolbar != value)
				{
					this._toolbar = value;
					Update();
				}
			}
		}
		private string[] _toolbar = DefaultToolbar;

		private bool ShouldSerializeToolbar()
		{
			return this._toolbar != DefaultToolbar;
		}

		private void ResetToolbar()
		{
			this._toolbar = DefaultToolbar;
			Update();
		}

		/// <summary>
		/// Returns the default buttons to show in the toolbar.
		/// </summary>
		/// <remarks>
		/// This is the initial value of the <see cref="Toolbar"/> property. The returned array is shared by all the editors
		/// that use the default toolbar: don't modify its elements, copy it instead.
		/// </remarks>
		/// <example>
		/// Adding the paste button to the default toolbar:
		/// <code><![CDATA[
		/// var buttons = new List<string>(TinyEditor.DefaultToolbar);
		/// buttons.Insert(buttons.IndexOf("copy") + 1, "paste");
		/// this.tinyEditor1.Toolbar = buttons.ToArray();
		/// ]]></code>
		/// </example>
		public static string[] DefaultToolbar
		{
			get
			{
				return _defaultToolbar;
			}
		}
		private static string[] _defaultToolbar = new[] {
					"cut", "copy", "|", "undo", "redo","|", "orderedlist", "unorderedlist", "|", "outdent", "indent", "|", "leftalign", "centeralign", "rightalign", "blockjustify", "|", "hr", "|", "print", "n",
					"bold", "italic", "underline", "strikethrough", "|", "subscript", "superscript", "|", "font", "size", "style", "|", "unformat", "|", "image", "link", "unlink"};

		/// <summary>
		/// Returns or sets the font names to display in the toolbar.
		/// </summary>
		/// <remarks>
		/// The names are listed in the <c>font</c> drop-down of the toolbar, which must be included in <see cref="Toolbar"/>.
		/// The default is <see cref="DefaultFontNames"/>.
		/// </remarks>
		/// <example>
		/// Offering a custom list of fonts:
		/// <code><![CDATA[
		/// this.tinyEditor1.FontNames = new[] { "Segoe UI", "Arial", "Times New Roman", "Courier New" };
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
		/// Returns the default font names to display in the toolbar.
		/// </summary>
		/// <remarks>
		/// This is the initial value of the <see cref="FontNames"/> property: Verdana, Arial, Georgia and Trebuchet MS.
		/// The returned array is shared: don't modify its elements.
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
		/// Returns or sets the custom css file used by the editor.
		/// </summary>
		/// <remarks>
		/// The style sheet is linked in the document of the editable area (an IFrame), so it styles the edited content,
		/// not the toolbar. The value is the URL or application-relative path of the css file.
		/// A null value is converted to an empty string.
		/// </remarks>
		/// <example>
		/// Styling the edited content with the application's css:
		/// <code><![CDATA[
		/// this.tinyEditor1.StyleSheetSource = "Content/editor-content.css";
		/// ]]></code>
		/// </example>
		[DefaultValue("")]
		[Description("Returns or sets the custom css file used by the editor.")]
		[Editor("Wisej.Design.CssFileSourceEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string StyleSheetSource
		{
			get { return this._styleSheetSource ?? string.Empty; }
			set
			{
				value = value ?? string.Empty;
				if (this._styleSheetSource != value)
				{
					this._styleSheetSource = value;
					Update();
				}
			}
		}
		private string _styleSheetSource = string.Empty;

		#endregion

		#region Methods

		/// <summary>
		/// Executes commands to manipulate the contents of the editable region. 
		/// </summary>
		/// <param name="command">The name of the command to execute. See <see href="https://developer.mozilla.org/en-US/docs/Web/API/Document/execCommand"/> for a list of commands.</param>
		/// <param name="showDefaultUI">Indicates whether the default user interface should be shown. This is not implemented in Mozilla.</param>
		/// <param name="argument">For commands which require an input argument (such as insertImage, for which this is the URL of the image to insert), this is a string providing that information. Specify null if no argument is needed.</param>
		/// <remarks>
		/// Most commands affect the document's selection (bold, italics, etc.), while others insert new elements (adding a link) or 
		/// affect an entire line (indenting). When using contentEditable, calling execCommand() will affect the 
		/// currently active editable element.
		/// The <see cref="Text"/> property is updated asynchronously shortly after the command runs.
		/// </remarks>
		/// <example>
		/// Inserting an image at the caret position:
		/// <code><![CDATA[
		/// private void buttonInsertLogo_Click(object sender, EventArgs e)
		/// {
		///     this.tinyEditor1.ExecCommand("insertImage", false, "Images/logo.png");
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
		/// <param name="command">The name of the command to execute. See <see href="https://developer.mozilla.org/en-US/docs/Web/API/Document/execCommand"/> for a list of commands.</param>
		/// <param name="argument">For commands which require an input argument (such as insertImage, for which this is the URL of the image to insert), this is a string providing that information. Specify null if no argument is needed.</param>
		/// <remarks>
		/// Most commands affect the document's selection (bold, italics, etc.), while others insert new elements (adding a link) or 
		/// affect an entire line (indenting). When using contentEditable, calling execCommand() will affect the 
		/// currently active editable element.
		/// The <see cref="Text"/> property is updated asynchronously shortly after the command runs.
		/// </remarks>
		/// <example>
		/// Executing commands from buttons outside of the editor:
		/// <code><![CDATA[
		/// private void buttonBold_Click(object sender, EventArgs e)
		/// {
		///     this.tinyEditor1.ExecCommand("bold");
		/// }
		///
		/// private void buttonRed_Click(object sender, EventArgs e)
		/// {
		///     this.tinyEditor1.ExecCommand("foreColor", "#FF0000");
		/// }
		/// ]]></code>
		/// </example>
		public void ExecCommand(string command, string argument = null)
		{
			ExecCommand(command, false, argument);
		}

		/// <summary>
		/// Performs additional configuration to the widget.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnEnabledChanged(EventArgs e)
		{
			this.Call("setEditable", this.Enabled);

			base.OnEnabledChanged(e);
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Returns the theme appearance key for this control.
		/// </summary>
		string IWisejControl.AppearanceKey
		{
			get { return this.AppearanceKey ?? "tinyeditor"; }
		}

		/// <summary>
		/// Returns the initialization script that creates the editor on the client.
		/// </summary>
		/// <remarks>
		/// The script is built from the embedded <c>startup.js</c> resource and the current property values. The setter is ignored.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
			set { }
		}

		/// <summary>
		/// Returns the list of packages (the <c>tiny.editor.js</c> library) loaded on the client before the editor is created.
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
					base.Packages.Add(new Package()
					{
						Name = "tinyeditor.js",
						Source = GetResourceURL("Wisej.Web.Ext.TinyEditor.JavaScript.tiny.editor.js")
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
			string script = GetResourceString("Wisej.Web.Ext.TinyEditor.JavaScript.startup.js");

			options.sourceText = this.SourceText;
			options.wysiwygText = this.WysiwygText;
			options.cssfile = GetResourceURL("Wisej.Web.Ext.TinyEditor.Resources.tiny.editor.css");
			options.fonts = this.FontNames;
			options.controls = this.Toolbar;
			options.header = this.ShowToolbar;
			options.footer = this.ShowFooter;
			options.cssfile = this.StyleSheetSource;
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

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Control.WidgetEvent" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.WidgetEventArgs" /> that contains the event data. </param>
		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			switch (e.Type)
			{
				case "load":
					this.initialized = true;
					Call("setEditable", this.Enabled);
					Call("setText", TextUtils.EscapeText(this.Text, true));
					break;

				case "changeText":
					this.Text = e.Data ?? "";
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

		/// <summary>
		/// Causes the control to update the corresponding client side widget.
		/// When in design mode, causes the rendered control to update its
		/// entire surface in the designer.
		/// </summary>
		/// <remarks>
		/// When the widget is recreated on the client, the current <see cref="Text"/> is sent again to the new editor.
		/// </remarks>
		/// <example>
		/// Recreating the editor after changing several properties:
		/// <code><![CDATA[
		/// this.tinyEditor1.ShowFooter = false;
		/// this.tinyEditor1.FontNames = new[] { "Arial", "Tahoma" };
		/// this.tinyEditor1.Update();
		/// ]]></code>
		/// </example>
		public override void Update()
		{
			// when updating and refreshing (IsNew = true)
			// resend the focused cell and update the row selection.

			IWisejComponent me = this;
			if (me.IsNew)
			{
				this.initialized = false;

				Call("setText", TextUtils.EscapeText(this.Text, true));
			}

			base.Update();
		}

		#endregion
	}
}
