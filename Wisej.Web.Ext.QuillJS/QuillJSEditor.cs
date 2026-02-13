using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using Wisej.Core;

namespace Wisej.Web.Ext.QuillJS
{
	/// <summary>
	/// QuillJSEditor is a modern WYSIWYG HTML editor with powerful features.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Control), "RichTextBox.bmp")]
	[DefaultProperty("Text")]
	[DefaultEvent("TextChanged")]
	[ApiCategory("QuillJSEditor")]
	public partial class QuillJSEditor : Widget, IWisejControl
	{

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the QuillJSEditor class.
		/// </summary>
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

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override dynamic Options
		{
			get => base.Options;
			set => base.Options = value;
		}

		/// <inheritdoc/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
			=> GetResourceString("Wisej.Web.Ext.QuillJS.JavaScript.startup.js");

		/// <summary>
		/// Gets or sets the HTML content of the editor.
		/// </summary>
		[DefaultValue(null)]
		[Description("Gets or sets the HTML content of the editor.")]
		public string Html
		{
			get => this.Options.html;
			set => this.Options.html = value;
		}

		/// <inheritdoc/>
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
		/// Gets or sets whether the editor is read-only.
		/// </summary>
		[DefaultValue(false)]
		[Description("Determines whether the editor content can be modified.")]
		public bool ReadOnly
		{
			get => this.Options.readOnly;
			set => this.Options.readOnly = value;
		}

		/// <summary>
		/// Gets or sets the placeholder text when the editor is empty.
		/// </summary>
		[DefaultValue("")]
		[Description("The placeholder text to show when the editor is empty.")]
		public string Placeholder
		{
			get => this.Options.placeholder;
			set => this.Options.placeholder = value;
		}

		/// <summary>
		/// Gets or sets the toolbar configuration for the editor.
		/// </summary>
		[Description("Configures the toolbar options for the editor.")]
		[TypeConverter(typeof(DynamicObjectConverter))]
		[Editor("Wisej.Design.DynamicObjectEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WisejSerializerOptions(WisejSerializerOptions.None)]
		public dynamic Toolbar
		{
			get => this.Options.modules.toolbar;
			set => this.Options.modules.toolbar = value;
		}

		/// <summary>
		/// Gets or sets whether the editor should get focus when the page loads.
		/// </summary>
		[DefaultValue(false)]
		[Description("Determines whether the editor gets focus when the page loads.")]
		public bool AutoFocus
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the theme to use for the editor.
		/// </summary>
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
		/// Focuses the editor.
		/// </summary>
		public new void Focus()
		{
			base.Focus();

			Call("focus");
		}

		/// <summary>
		/// Gets the current selection range.
		/// </summary>
		public async Task<QuillSelection> GetSelectionAsync()
		{
			var range = await CallAsync("getSelection");
			if (range == null)
				return null;

			return new QuillSelection() { Index = range.index, Length = range.length };
		}

		/// <summary>
		/// Gets the delta (changes) of the editor content.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains the QuillDelta object.</returns>
		public async Task<QuillDelta> GetDeltaAsync()
		{
			var delta = await this.CallAsync("getDelta");
			return QuillDelta.Parse(delta.ops);
		}

		/// <summary>
		/// Sets the delta (changes) of the editor content asynchronously.
		/// </summary>
		/// <param name="delta">The delta to set in the editor.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task SetDeltaAsync(QuillDelta delta)
		{
			await this.CallAsync("setDelta", delta);
		}

		/// <summary>
		/// Sets the selection range.
		/// </summary>
		/// <param name="index">Selection index</param>
		/// <param name="length">Selection length</param>
		public void SetSelection(int index, int length)
		{
			Call("setSelection", index, length);
		}

		/// <summary>
		/// Formats text at the current selection.
		/// </summary>
		/// <param name="format">Format name like "bold", "italic", etc.</param>
		/// <param name="value">Value of the format.</param>
		public void Format(string format, object value)
		{
			Call("format", format, value);
		}

		/// <summary>
		/// Formats text in the editor.
		/// </summary>
		/// <param name="index">The starting position of the text to format.</param>
		/// <param name="length">The number of characters to format.</param>
		/// <param name="format">Format name like "bold", "italic", etc.</param>
		/// <param name="value">Value of the format.</param>
		public void FormatText(int index, int length, string format, object value)
		{
			Call("formatText", index, length, format, value);
		}

		/// <summary>
		/// Formats the lines at the specified range.
		/// Has no effect when called with inline formats.
		/// </summary>
		/// <param name="index">The starting position of the range to format.</param>
		/// <param name="length">The number of characters in the range to format.</param>
		/// <param name="format">Format name like "bold", "italic", etc.</param>
		/// <param name="value">Value of the format.</param>
		public void FormatLine(int index, int length, string format, object value)
		{
			Call("formatLine", index, length, format, value);
		}

		/// <summary>
		/// Removes all formatting from the selection.
		/// </summary>
		public void RemoveFormat()
		{
			Call("removeFormat");
		}

		/// <summary>
		/// Removes formatting from the specified range.
		/// </summary>
		/// <param name="index">The starting position of the range to remove formatting from.</param>
		/// <param name="length">The number of characters to remove formatting from.</param>
		public void RemoveFormat(int index, int length)
		{
			Call("removeFormat", index, length);
		}

		/// <summary>
		/// Updates the content of the editor with the specified delta.
		/// </summary>
		/// <param name="delta">The delta to update the content with.</param>
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
