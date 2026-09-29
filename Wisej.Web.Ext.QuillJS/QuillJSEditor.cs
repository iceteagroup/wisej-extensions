using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using Wisej.Core;

namespace Wisej.Web.Ext.QuillJS
{
	/// <summary>
	/// Represents a WYSIWYG rich text editor based on the QuillJS (https://quilljs.com/) JavaScript library.
	/// </summary>
	/// <remarks>
	/// The content can be accessed as HTML (<see cref="Html"/>), as plain text (<see cref="Text"/>) or as a
	/// <see cref="QuillDelta"/> (<see cref="GetDeltaAsync"/>, <see cref="SetDeltaAsync"/>, <see cref="UpdateContent"/>).
	/// <see cref="Html"/> and <see cref="Text"/> are updated on the server every time the content changes on the client.
	/// </remarks>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Control), "RichTextBox.bmp")]
	[DefaultProperty("Text")]
	[DefaultEvent("TextChanged")]
	[ApiCategory("QuillJSEditor")]
	public partial class QuillJSEditor : Widget, IWisejControl
	{

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="QuillJSEditor"/> class.
		/// </summary>
		/// <remarks>
		/// The new instance uses the "snow" <see cref="Theme"/> and an empty <see cref="Toolbar"/> configuration.
		/// </remarks>
		public QuillJSEditor()
		{
			this.Theme = "snow";

			this.Options.modules = new DynamicObject();
			this.Options.modules.toolbar = new DynamicObject();
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when the selection changes in the editor.
		/// </summary>
		public event SelectionChangedEventHandler SelectionChanged;

		/// <summary>
		/// Delegate for the SelectionChanged event.
		/// </summary>
		public delegate void SelectionChangedEventHandler(object sender, SelectionChangedEventArgs e);

		/// <summary>
		/// Raises the SelectionChanged event.
		/// </summary>
		protected virtual void OnSelectionChanged(SelectionChangedEventArgs e)
		{
			this.SelectionChanged?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when a link is clicked in the editor.
		/// </summary>
		public event LinkClickEventHandler LinkClick;

		/// <summary>
		/// Delegate for the LinkClick event.
		/// </summary>
		public delegate void LinkClickEventHandler(object sender, LinkClickEventArgs e);

		/// <summary>
		/// Raises the LinkClick event.
		/// </summary>
		protected virtual void OnLinkClick(LinkClickEventArgs e)
		{
			this.LinkClick?.Invoke(this, e);
		}

		/// <summary>
		/// Fired when the editor content changes.
		/// </summary>
		public event DeltaChangedEventHandler DeltaChanged;

		/// <summary>
		/// Delegate for the DeltaChanged event.
		/// </summary>
		public delegate void DeltaChangedEventHandler(object sender, DeltaChangedEventArgs e);

		/// <summary>
		/// Raises the DeltaChanged event.
		/// </summary>
		protected virtual void OnDeltaChanged(DeltaChangedEventArgs e)
		{
			this.DeltaChanged?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the list of packages loaded by the widget.
		/// </summary>
		/// <remarks>
		/// The list is populated on first access with the embedded "quill.min.js" library and the "quill.snow.min.css" stylesheet.
		/// </remarks>
		/// <example>
		/// Adding the stylesheet of the "bubble" theme, which is not included in the default packages:
		/// <code><![CDATA[
		/// this.quillJSEditor1.Packages.Add(new Package
		/// {
		///     Name = "QuillBubbleTheme",
		///     Source = "https://cdn.jsdelivr.net/npm/quill@2/dist/quill.bubble.css"
		/// });
		/// this.quillJSEditor1.Theme = "bubble";
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override List<Package> Packages
		{
			get
			{
				if (base.Packages.Count <= 0)
				{
					base.Packages.AddRange(
						new Package[]
						{
							new Package()
							{
								Name = "QuillJS",
								Source = GetResourceURL("Wisej.Web.Ext.QuillJS.JavaScript.quill.min.js")
							},
							new Package()
							{
								Name = "QuillSnowTheme",
								Source = GetResourceURL("Wisej.Web.Ext.QuillJS.JavaScript.quill.snow.min.css")
							}
						}
					);
				}

				return base.Packages;
			}
		}

		/// <summary>
		/// Returns or sets the options object passed to the QuillJS editor when it's created.
		/// </summary>
		/// <remarks>
		/// The object is the QuillJS configuration (theme, placeholder, readOnly, modules, ...) plus the "html" and "text"
		/// values used to initialize the content. <see cref="Html"/>, <see cref="Text"/>, <see cref="ReadOnly"/>,
		/// <see cref="Placeholder"/>, <see cref="Toolbar"/> and <see cref="Theme"/> are stored in this object.
		/// Configuration values are read only when the editor is created on the client.
		/// </remarks>
		/// <example>
		/// Enabling a QuillJS module that has no dedicated property:
		/// <code><![CDATA[
		/// this.quillJSEditor1.Options.modules.history = new DynamicObject();
		/// this.quillJSEditor1.Options.modules.history.delay = 2000;
		/// this.quillJSEditor1.Options.modules.history.maxStack = 500;
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override dynamic Options
		{
			get => base.Options;
			set => base.Options = value;
		}

		/// <summary>
		/// Returns the JavaScript code that initializes the editor on the client.
		/// </summary>
		/// <remarks>
		/// The script is loaded from the embedded "startup.js" resource and cannot be changed.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
			=> GetResourceString("Wisej.Web.Ext.QuillJS.JavaScript.startup.js");

		/// <summary>
		/// Returns or sets the HTML content of the editor.
		/// </summary>
		/// <remarks>
		/// The value is updated on the server every time the content changes on the client. When both
		/// <see cref="Html"/> and <see cref="Text"/> are set, the client uses <see cref="Html"/>.
		/// QuillJS normalizes the HTML to the formats it supports, therefore reading the property may return
		/// markup that is different from the value that was assigned.
		/// </remarks>
		/// <example>
		/// Loading and saving the HTML content:
		/// <code><![CDATA[
		/// this.quillJSEditor1.Html = "<h1>Title</h1><p>Some <strong>bold</strong> text.</p>";
		///
		/// private void buttonSave_Click(object sender, EventArgs e)
		/// {
		///     SaveDocument(this.quillJSEditor1.Html);
		/// }
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Gets or sets the HTML content of the editor.")]
		public string Html
		{
			get => this.Options.html ?? String.Empty;
			set => this.Options.html = value;
		}

		/// <summary>
		/// Returns or sets the plain text content of the editor.
		/// </summary>
		/// <remarks>
		/// The value is updated on the server every time the content changes on the client and always ends with a
		/// new line character. Setting the text replaces the content and removes all formatting, but it's ignored by the
		/// client when <see cref="Html"/> also has a value, since <see cref="Html"/> takes precedence.
		/// </remarks>
		public override string Text
		{
			get => base.Text;
			set
			{
				base.Text = value;
				this.Options.text = value;
			}
		}

		/// <summary>
		/// Returns or sets a value indicating whether the editor is read-only.
		/// </summary>
		/// <remarks>
		/// The value is passed to QuillJS as the "readOnly" option when the editor is created on the client.
		/// </remarks>
		[DefaultValue(false)]
		[Description("Determines whether the editor content can be modified.")]
		public bool ReadOnly
		{
			get => this.Options.readOnly ?? false;
			set => this.Options.readOnly = value;
		}

		/// <summary>
		/// Returns or sets the placeholder text displayed when the editor is empty.
		/// </summary>
		/// <remarks>
		/// The value is passed to QuillJS as the "placeholder" option when the editor is created on the client.
		/// </remarks>
		[DefaultValue("")]
		[Description("The placeholder text to show when the editor is empty.")]
		public string Placeholder
		{
			get => this.Options.placeholder ?? String.Empty;
			set => this.Options.placeholder = value;
		}

		/// <summary>
		/// Returns or sets the toolbar configuration for the editor.
		/// </summary>
		/// <remarks>
		/// The value is the QuillJS toolbar module configuration (<c>modules.toolbar</c>): an array of groups of
		/// format names, where a format can also be an object mapping the format name to a value or to an array of
		/// values for a drop down. The default is an empty object, which doesn't display any toolbar.
		/// The value is serialized without changing the case of the member names and is read only when the editor
		/// is created on the client.
		/// </remarks>
		/// <example>
		/// Displaying a toolbar with text formats, headers, lists and links:
		/// <code><![CDATA[
		/// this.quillJSEditor1.Toolbar = new object[]
		/// {
		///     new object[] { QuillFormat.Formats.Bold, QuillFormat.Formats.Italic, QuillFormat.Formats.Underline },
		///     new object[] { new { header = new object[] { 1, 2, 3, false } } },
		///     new object[] { new { list = QuillFormat.Lists.Ordered }, new { list = QuillFormat.Lists.Bullet } },
		///     new object[] { QuillFormat.Formats.Link, QuillFormat.Formats.Clean }
		/// };
		/// ]]></code>
		/// </example>
		[Description("Configures the toolbar options for the editor.")]
		[TypeConverter(typeof(DynamicObjectConverter))]
		[Editor("Wisej.Design.DynamicObjectEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WisejSerializerOptions(WisejSerializerOptions.None)]
		public dynamic Toolbar
		{
			get => this.Options?.modules?.toolbar;
			set => this.Options.modules.toolbar = value;
		}

		/// <summary>
		/// Returns or sets a value indicating whether the editor should get the focus when it's loaded.
		/// </summary>
		/// <remarks>
		/// When true, <see cref="Focus"/> is called after the editor has been initialized on the client.
		/// </remarks>
		[DefaultValue(false)]
		[Description("Determines whether the editor gets focus when the page loads.")]
		public bool AutoFocus
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets the QuillJS theme to use for the editor.
		/// </summary>
		/// <remarks>
		/// QuillJS provides the "snow" theme (with a toolbar) and the "bubble" theme (with a floating tooltip toolbar).
		/// Only the stylesheet of the "snow" theme is included in the default <see cref="Packages"/>.
		/// The value is read only when the editor is created on the client.
		/// </remarks>
		[DefaultValue("snow")]
		[Description("The theme to use for the editor (snow or bubble).")]
		public string Theme
		{
			get => this.Options.theme;
			set => this.Options.theme = value;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Moves the focus to the editor.
		/// </summary>
		/// <remarks>
		/// Focuses the control and the editable area of the QuillJS editor on the client.
		/// </remarks>
		/// <example>
		/// Focusing the editor after loading a document:
		/// <code><![CDATA[
		/// this.quillJSEditor1.Html = LoadDocument();
		/// this.quillJSEditor1.Focus();
		/// ]]></code>
		/// </example>
		public new void Focus()
		{
			base.Focus();

			Call("focus");
		}

		/// <summary>
		/// Returns the current selection range asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="QuillSelection"/>, or null when the editor doesn't have the focus.</returns>
		/// <example>
		/// Making the selected text bold:
		/// <code><![CDATA[
		/// private async void buttonBold_Click(object sender, EventArgs e)
		/// {
		///     var selection = await this.quillJSEditor1.GetSelectionAsync();
		///     if (selection != null && selection.Length > 0)
		///         this.quillJSEditor1.FormatText(selection.Index, selection.Length, QuillFormat.Formats.Bold, true);
		/// }
		/// ]]></code>
		/// </example>
		public async Task<QuillSelection> GetSelectionAsync()
		{
			var range = await CallAsync("getSelection");
			if (range == null)
				return null;

			return new QuillSelection() { Index = range.index, Length = range.length };
		}

		/// <summary>
		/// Returns the content of the editor as a <see cref="QuillDelta"/> asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="QuillDelta"/> describing the whole content.</returns>
		/// <remarks>
		/// The returned delta contains only insert operations, together with their formatting attributes.
		/// </remarks>
		/// <example>
		/// Saving the content as a delta:
		/// <code><![CDATA[
		/// private async void buttonSave_Click(object sender, EventArgs e)
		/// {
		///     var delta = await this.quillJSEditor1.GetDeltaAsync();
		///     SaveDocument(JSON.Stringify(delta));
		/// }
		/// ]]></code>
		/// </example>
		public async Task<QuillDelta> GetDeltaAsync()
		{
			var delta = await this.CallAsync("getDelta");
			return QuillDelta.Parse(delta.ops);
		}

		/// <summary>
		/// Replaces the content of the editor with the specified <see cref="QuillDelta"/> asynchronously.
		/// </summary>
		/// <param name="delta">The delta describing the new content, made of insert operations.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		/// <remarks>
		/// The whole content is replaced. Use <see cref="UpdateContent"/> to apply a change to the existing content.
		/// </remarks>
		/// <example>
		/// Loading a new document:
		/// <code><![CDATA[
		/// var delta = new QuillDelta();
		/// delta.Insert("Dear customer,\n");
		/// delta.Insert("Thank you for your order.\n");
		/// await this.quillJSEditor1.SetDeltaAsync(delta);
		/// ]]></code>
		/// </example>
		public async Task SetDeltaAsync(QuillDelta delta)
		{
			await this.CallAsync("setDelta", delta);
		}

		/// <summary>
		/// Sets the selection range.
		/// </summary>
		/// <param name="index">The zero-based starting position of the selection.</param>
		/// <param name="length">The number of characters to select, or 0 to move the caret to <paramref name="index"/>.</param>
		/// <remarks>
		/// QuillJS also moves the focus to the editor when the selection is set.
		/// </remarks>
		/// <example>
		/// Selecting a word found in the text:
		/// <code><![CDATA[
		/// int index = this.quillJSEditor1.Text.IndexOf("invoice");
		/// if (index > -1)
		///     this.quillJSEditor1.SetSelection(index, "invoice".Length);
		/// ]]></code>
		/// </example>
		public void SetSelection(int index, int length)
		{
			Call("setSelection", index, length);
		}

		/// <summary>
		/// Formats the text at the current selection.
		/// </summary>
		/// <param name="format">Format name like "bold", "italic", etc. See <see cref="QuillFormat.Formats"/>.</param>
		/// <param name="value">Value of the format, i.e. true for "bold", "#ff0000" for "color", or false to remove the format.</param>
		/// <remarks>
		/// When the selection is empty, the format is applied to the text typed next at the caret position.
		/// </remarks>
		/// <example>
		/// Applying formats from buttons outside of the editor:
		/// <code><![CDATA[
		/// private void buttonBold_Click(object sender, EventArgs e)
		/// {
		///     this.quillJSEditor1.Format(QuillFormat.Formats.Bold, true);
		/// }
		///
		/// private void buttonRed_Click(object sender, EventArgs e)
		/// {
		///     this.quillJSEditor1.Format(QuillFormat.Formats.Color, "#ff0000");
		/// }
		/// ]]></code>
		/// </example>
		public void Format(string format, object value)
		{
			Call("format", format, value);
		}

		/// <summary>
		/// Formats the text in the specified range.
		/// </summary>
		/// <param name="index">The zero-based starting position of the text to format.</param>
		/// <param name="length">The number of characters to format.</param>
		/// <param name="format">Format name like "bold", "italic", etc. See <see cref="QuillFormat.Formats"/>.</param>
		/// <param name="value">Value of the format, or false to remove the format.</param>
		/// <example>
		/// Highlighting the first 10 characters:
		/// <code><![CDATA[
		/// this.quillJSEditor1.FormatText(0, 10, QuillFormat.Formats.Background, "#ffff00");
		/// ]]></code>
		/// </example>
		public void FormatText(int index, int length, string format, object value)
		{
			Call("formatText", index, length, format, value);
		}

		/// <summary>
		/// Formats all the lines in the specified range.
		/// </summary>
		/// <param name="index">The zero-based starting position of the range to format.</param>
		/// <param name="length">The number of characters in the range to format.</param>
		/// <param name="format">Line format name like "header", "align", "list", etc.</param>
		/// <param name="value">Value of the format, or false to remove the format.</param>
		/// <remarks>
		/// Every line that intersects the range is formatted. Has no effect when called with inline formats such as "bold".
		/// </remarks>
		/// <example>
		/// Centering the first line and turning it into a header:
		/// <code><![CDATA[
		/// this.quillJSEditor1.FormatLine(0, 1, QuillFormat.Formats.Align, QuillFormat.Alignments.Center);
		/// this.quillJSEditor1.FormatLine(0, 1, QuillFormat.Formats.Header, 1);
		/// ]]></code>
		/// </example>
		public void FormatLine(int index, int length, string format, object value)
		{
			Call("formatLine", index, length, format, value);
		}

		/// <summary>
		/// Removes all formatting from the current selection.
		/// </summary>
		/// <remarks>
		/// Both inline and line formats are removed. Nothing happens when the editor doesn't have a selection.
		/// </remarks>
		/// <example>
		/// Clearing the formatting from a button:
		/// <code><![CDATA[
		/// private void buttonClear_Click(object sender, EventArgs e)
		/// {
		///     this.quillJSEditor1.RemoveFormat();
		/// }
		/// ]]></code>
		/// </example>
		public void RemoveFormat()
		{
			Call("removeFormat");
		}

		/// <summary>
		/// Removes all formatting from the specified range.
		/// </summary>
		/// <param name="index">The zero-based starting position of the range to remove formatting from.</param>
		/// <param name="length">The number of characters to remove formatting from.</param>
		/// <example>
		/// Clearing the formatting of the whole content:
		/// <code><![CDATA[
		/// this.quillJSEditor1.RemoveFormat(0, this.quillJSEditor1.Text.Length);
		/// ]]></code>
		/// </example>
		public void RemoveFormat(int index, int length)
		{
			Call("removeFormat", index, length);
		}

		/// <summary>
		/// Applies the changes described by the specified delta to the content of the editor.
		/// </summary>
		/// <param name="delta">The delta describing the changes, made of retain, insert and delete operations.</param>
		/// <remarks>
		/// Unlike <see cref="SetDeltaAsync"/>, the existing content is preserved and only modified as described by the delta.
		/// </remarks>
		/// <example>
		/// Inserting italic text after the first 20 characters:
		/// <code><![CDATA[
		/// var delta = new QuillDelta();
		/// delta.Retain(20);
		/// delta.Insert("Best regards", new Dictionary<string, object> { { QuillFormat.Formats.Italic, true } });
		/// this.quillJSEditor1.UpdateContent(delta);
		/// ]]></code>
		/// </example>
		public void UpdateContent(QuillDelta delta)
		{
			Call("updateContent", delta);
		}

		/// <summary>
		/// Called when the widget is initialized on the client.
		/// </summary>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (this.AutoFocus)
				Focus();
		}

		#endregion

		#region Wisej Implementation

		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			var data = e.Data;

			switch (e.Type)
			{
				case "selectionChange":
					OnSelectionChanged(new SelectionChangedEventArgs(data.index, data.length));
					break;

				case "textChanged":
					ProcessTextChanged(data);
					break;

				case "linkClick":
					OnLinkClick(new LinkClickEventArgs(data?.url));
					break;

				case "deltaChanged":
					OnDeltaChanged(new DeltaChangedEventArgs(data.oldDelta, data.newDelta, data.source));
					break;

				default:
					base.OnWidgetEvent(e);
					break;
			}
		}

		private void ProcessTextChanged(dynamic data)
		{
			var me = (IWisejControl)this;
			var dirty = me.IsDirty;
			this.Html = data.html as string;
			this.Text = data.text as string;
			me.IsDirty = dirty;
		}

		#endregion
	}
}
