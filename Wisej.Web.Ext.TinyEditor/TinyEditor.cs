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
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;
using WinForms = System.Windows.Forms;

namespace Wisej.Web.Ext.TinyEditor
{
	/// <summary>
	/// TinyEditor is a simple JavaScript WYSIWYG editor that is both lightweight (8KB) and standalone
	/// from: https://github.com/jessegreathouse/TinyEditor.
	/// 
	/// It can easily be customized to integrate with any website through CSS and the multitude of parameters. 
	/// It handles most of the basic formatting needs and has some functionality built in to help keep the 
	/// rendered markup as clean as possible. 
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(WinForms.RichTextBox))]
	[DefaultProperty("Text")]
	[DefaultEvent("TextChanged")]
	public class TinyEditor : Widget, IWisejControl
	{
		// indicates that the control is ready to update its content.
		private bool initialized;

		#region Properties

		/// <summary>
		/// Returns or sets the HTML text associated with this control.
		/// </summary>
		/// <returns>The HTML text associated with this control.</returns>
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

					if (!((IWisejControl)this).IsNew /* cannot call setText until the widget is created.*/ )
						Call("setText", TextUtils.EscapeText(value, true));
				}
			}
		}
		private string _text = "";

		/// <summary>
		/// Shows or hides the toolbar panel.
		/// </summary>
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
		[DesignerActionList]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("Returns or sets the buttons to show in the toolbar.")]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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
		[DesignerActionList]
		[TypeConverter(typeof(ArrayConverter))]
		[Description("Returns or sets the font names to display in the toolbar.")]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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
		/// Returns the default buttons to show in the toolbar.
		/// </summary>
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
		[DefaultValue("")]
		[Description("Returns or sets the custom css file used by the editor.")]
		[Editor("Wisej.Design.CssFileSourceEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
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
		/// <param name="command">The name of the command to execute. See https://developer.mozilla.org/en-US/docs/Web/API/Document/execCommand for a list of commands.</param>
		/// <param name="showDefaultUI">Indicates whether the default user interface should be shown. This is not implemented in Mozilla.</param>
		/// <param name="argument">For commands which require an input argument (such as insertImage, for which this is the URL of the image to insert), this is a string providing that information. Specify null if no argument is needed.</param>
		/// <remarks>
		/// Most commands affect the document's selection (bold, italics, etc.), while others insert new elements (adding a link) or 
		/// affect an entire line (indenting). When using contentEditable, calling execCommand() will affect the 
		/// currently active editable element.
		/// </remarks>
		public void ExecCommand(string command, bool showDefaultUI = false, string argument = null)
		{
			Call("execCommand", command, showDefaultUI, argument);
		}

		/// <summary>
		/// Executes commands to manipulate the contents of the editable region. 
		/// </summary>
		/// <param name="command">The name of the command to execute. See https://developer.mozilla.org/en-US/docs/Web/API/Document/execCommand for a list of commands.</param>
		/// <param name="argument">For commands which require an input argument (such as insertImage, for which this is the URL of the image to insert), this is a string providing that information. Specify null if no argument is needed.</param>
		/// <remarks>
		/// Most commands affect the document's selection (bold, italics, etc.), while others insert new elements (adding a link) or 
		/// affect an entire line (indenting). When using contentEditable, calling execCommand() will affect the 
		/// currently active editable element.
		/// </remarks>
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
		/// Overridden to create our initialization script.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
			set { }
		}

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
					this.initialized = true;
					if (!String.IsNullOrEmpty(this.Text))
						Call("setText", TextUtils.EscapeText(this.Text, true));

					if (!this.Enabled)
						Call("setEditable", this.Enabled);

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
		public override void Update()
		{
			// when updating and refreshing (IsNew = true)
			// resend the focused cell and update the row selection.

			IWisejComponent me = this;
			if (me.IsNew)
			{
				Call("setText", TextUtils.EscapeText(this.Text, true));
			}

			base.Update();
		}

		#endregion
	}
}
