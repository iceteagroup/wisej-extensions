///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Represents a text box in a <see cref="ToolStrip" /> that allows the user to enter text.
	///</summary>
	public partial class ToolStripTextBox : ToolStripControlHost
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripTextBox" /> class.
		///</summary>
		public ToolStripTextBox()
			: base(CreateControlInstance())
		{
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripTextBox" /> class with the specified name. 
		///</summary>
		/// <param name="name">The name of the <see cref="ToolStripTextBox" />.</param>
		public ToolStripTextBox(string name)
			: this()
		{
			this.Name = name;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripTextBox" /> class derived from a base control.
		///</summary>
		/// <param name="c">The control from which to derive the <see cref="ToolStripTextBox" />. </param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ToolStripTextBox(Control c)
			: base(c)
		{
			throw new NotSupportedException(SR.GetString("ToolStripMustSupplyItsOwnTextBox"));
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripTextBox.AcceptsTab" /> property changes.
		///</summary>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnAcceptsTabChangedDescr")]
		public event EventHandler AcceptsTabChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripTextBox.BorderStyle" /> property changes.
		///</summary>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnBorderStyleChangedDescr")]
		public event EventHandler BorderStyleChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripTextBox.HideSelection" /> property changes.
		///</summary>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnHideSelectionChangedDescr")]
		public event EventHandler HideSelectionChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripTextBox.Modified" /> property changes.
		///</summary>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnModifiedChangedDescr")]
		public event EventHandler ModifiedChanged;

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnMultilineChangedDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler MultilineChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripTextBox.ReadOnly" /> property changes.
		///</summary>
		[SRDescription("TextBoxBaseOnReadOnlyChangedDescr")]
		[SRCategory("CatPropertyChanged")]
		public event EventHandler ReadOnlyChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripTextBox.TextBoxTextAlign" /> property changes.
		///</summary>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripTextBoxTextBoxTextAlignChangedDescr")]
		public event EventHandler TextBoxTextAlignChanged;

		#endregion



		#region Properties

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>An <see cref="System.Drawing.Image" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageDescr")]
		[DefaultValue(null)]
		public override Image BackgroundImage
		{
			get
			{
				return this._backgroundImage;
			}
			set
			{
				if ((this._backgroundImage != value))
				{
					this._backgroundImage = value;
				}
			}
		}

		private Image _backgroundImage;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>An <see cref="ImageLayout" /> value.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[DefaultValue(ImageLayout.Tile)]
		[SRCategory("CatAppearance")]
		[SRDescription("ControlBackgroundImageLayoutDescr")]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return this._backgroundImageLayout;
			}
			set
			{
				if ((this._backgroundImageLayout != value))
				{
					this._backgroundImageLayout = value;
				}
			}
		}

		private ImageLayout _backgroundImageLayout;

		/// <summary>
		/// Gets the default size of the <see cref="ToolStripTextBox" />.
		///</summary>
		/// <returns>The default <see cref="System.Drawing.Size" /> of the <see cref="ToolStripTextBox" /> in pixels. The default size is 100 pixels by 25 pixels.</returns>
		public override Size DefaultSize
		{
			get
			{
				return this._defaultSize;
			}
		}

		private Size _defaultSize;

		/// <summary>
		/// Gets the hosted <see cref="TextBox" /> control.
		///</summary>
		/// <returns>The hosted <see cref="TextBox" />.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TextBox TextBox
		{
			get
			{
				return this._textBox;
			}
		}

		private TextBox _textBox;

		/// <summary>
		/// Gets or sets a value indicating whether pressing the TAB key in a multiline text box control types a TAB character in the control instead of moving the focus to the next control in the tab order.
		///</summary>
		/// <returns>true if users can enter tabs in a multiline text box using the TAB key; false if pressing the TAB key moves the focus. The default is false.</returns>
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxAcceptsTabDescr")]
		public bool AcceptsTab
		{
			get
			{
				return this._acceptsTab;
			}
			set
			{
				if ((this._acceptsTab != value))
				{
					this._acceptsTab = value;
				}
			}
		}

		private bool _acceptsTab;

		/// <summary>
		/// Gets or sets a value indicating whether pressing ENTER in a multiline <see cref="TextBox" /> control creates a new line of text in the control or activates the default button for the form.
		///</summary>
		/// <returns>true if the ENTER key creates a new line of text in a multiline version of the control; false if the ENTER key activates the default button for the form. The default is false.</returns>
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxAcceptsReturnDescr")]
		public bool AcceptsReturn
		{
			get
			{
				return this._acceptsReturn;
			}
			set
			{
				if ((this._acceptsReturn != value))
				{
					this._acceptsReturn = value;
				}
			}
		}

		private bool _acceptsReturn;

		/// <summary>
		/// Gets or sets a custom string collection to use when the <see cref="ToolStripTextBox.AutoCompleteSource" /> property is set to CustomSource.
		///</summary>
		/// <returns>An <see cref="AutoCompleteStringCollection" /> to use with <see cref="TextBox.AutoCompleteSource" />.</returns>
		[Browsable(true)]
		[Localizable(true)]
		[SRDescription("TextBoxAutoCompleteCustomSourceDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public StringCollection AutoCompleteCustomSource
		{
			get
			{
				return this._autoCompleteCustomSource;
			}
			set
			{
				if ((this._autoCompleteCustomSource != value))
				{
					this._autoCompleteCustomSource = value;
				}
			}
		}

		private StringCollection _autoCompleteCustomSource;

		/// <summary>
		/// Gets or sets an option that controls how automatic completion works for the <see cref="ToolStripTextBox" />.
		///</summary>
		/// <returns>One of the <see cref="AutoCompleteMode" /> values. The default is <see cref="AutoCompleteMode.None" />.</returns>
		[DefaultValue(AutoCompleteMode.None)]
		[SRDescription("TextBoxAutoCompleteModeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this._autoCompleteMode;
			}
			set
			{
				if ((this._autoCompleteMode != value))
				{
					this._autoCompleteMode = value;
				}
			}
		}

		private AutoCompleteMode _autoCompleteMode;

		//TODO: Implement in Wisej.
		/// <summary>
		/// Gets or sets a value specifying the source of complete strings used for automatic completion.
		///</summary>
		/// <returns>One of the <see cref="AutoCompleteSource" /> values. The default is <see cref="AutoCompleteSource.None" />.</returns>
		//[Browsable(true)]
		//[SRDescription("TextBoxAutoCompleteSourceDescr")]
		//[DefaultValue(AutoCompleteSource.None)]
		//[EditorBrowsable(EditorBrowsableState.Always)]
		//public AutoCompleteSource AutoCompleteSource
		//{
		//	get
		//	{
		//		return this._autoCompleteSource;
		//	}
		//	set
		//	{
		//		if ((this._autoCompleteSource != value))
		//		{
		//			this._autoCompleteSource = value;
		//		}
		//	}
		//}
		//
		//private AutoCompleteSource _autoCompleteSource;

		/// <summary>
		/// Gets or sets the border type of the <see cref="ToolStripTextBox" /> control.
		///</summary>
		/// <returns>One of the <see cref="BorderStyle" /> values. The default is <see cref="BorderStyle.Double" />.</returns>
		[SRDescription("TextBoxBorderDescr")]
		[DefaultValue(BorderStyle.Double)]
		[SRCategory("CatAppearance")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this._borderStyle;
			}
			set
			{
				if ((this._borderStyle != value))
				{
					this._borderStyle = value;
				}
			}
		}

		private BorderStyle _borderStyle;

		/// <summary>
		/// Gets a value indicating whether the user can undo the previous operation in a <see cref="ToolStripTextBox" /> control.
		///</summary>
		/// <returns>true if the user can undo the previous operation performed in a text box control; otherwise, false.</returns>
		[Browsable(false)]
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxCanUndoDescr")]
		public bool CanUndo
		{
			get
			{
				return this._canUndo;
			}
		}

		private bool _canUndo;

		/// <summary>
		/// Gets or sets whether the <see cref="ToolStripTextBox" /> control modifies the case of characters as they are typed.
		///</summary>
		/// <returns>One of the <see cref="CharacterCasing" /> values. The default is <see cref="CharacterCasing.Normal" />.</returns>
		[SRCategory("CatBehavior")]
		[DefaultValue(CharacterCasing.Normal)]
		[SRDescription("TextBoxCharacterCasingDescr")]
		public CharacterCasing CharacterCasing
		{
			get
			{
				return this._characterCasing;
			}
			set
			{
				if ((this._characterCasing != value))
				{
					this._characterCasing = value;
				}
			}
		}

		private CharacterCasing _characterCasing;

		/// <summary>
		/// Gets or sets a value indicating whether the selected text in the text box control remains highlighted when the control loses focus.
		///</summary>
		/// <returns>true if the selected text does not appear highlighted when the text box control loses focus; false, if the selected text remains highlighted when the text box control loses focus. The default is true.</returns>
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TextBoxHideSelectionDescr")]
		public bool HideSelection
		{
			get
			{
				return this._hideSelection;
			}
			set
			{
				if ((this._hideSelection != value))
				{
					this._hideSelection = value;
				}
			}
		}

		private bool _hideSelection;

		/// <summary>
		/// Gets or sets the lines of text in a <see cref="ToolStripTextBox" /> control.
		///</summary>
		/// <returns>An array of strings that contains the text in a text box control.</returns>
		[SRDescription("TextBoxLinesDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		public String[] Lines
		{
			get
			{
				return this._lines;
			}
			set
			{
				if ((this._lines != value))
				{
					this._lines = value;
				}
			}
		}

		private String[] _lines;

		/// <summary>
		/// Gets or sets the maximum number of characters the user can type or paste into the text box control.
		///</summary>
		/// <returns>The number of characters that can be entered into the control. The default is 32767 characters.</returns>
		[SRCategory("CatBehavior")]
		[DefaultValue(32767)]
		[Localizable(true)]
		[SRDescription("TextBoxMaxLengthDescr")]
		public int MaxLength
		{
			get
			{
				return this._maxLength;
			}
			set
			{
				if ((this._maxLength != value))
				{
					this._maxLength = value;
				}
			}
		}

		private int _maxLength;

		/// <summary>
		/// Gets or sets a value that indicates that the <see cref="ToolStripTextBox" /> control has been modified by the user since the control was created or its contents were last set.
		///</summary>
		/// <returns>true if the control's contents have been modified; otherwise, false. </returns>
		[Browsable(false)]
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxModifiedDescr")]
		public bool Modified
		{
			get
			{
				return this._modified;
			}
			set
			{
				if ((this._modified != value))
				{
					this._modified = value;
				}
			}
		}

		private bool _modified;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		[SRDescription("TextBoxMultilineDescr")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Multiline
		{
			get
			{
				return this._multiline;
			}
			set
			{
				if ((this._multiline != value))
				{
					this._multiline = value;
				}
			}
		}

		private bool _multiline;

		/// <summary>
		/// Gets or sets a value indicating whether text in the <see cref="ToolStripTextBox" /> is read-only.
		///</summary>
		/// <returns>true if the <see cref="ToolStripTextBox" /> is read-only; otherwise, false. The default is false.</returns>
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxReadOnlyDescr")]
		public bool ReadOnly
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				if ((this._readOnly != value))
				{
					this._readOnly = value;
				}
			}
		}

		private bool _readOnly;

		/// <summary>
		/// Gets or sets a value indicating the currently selected text in the control.
		///</summary>
		/// <returns>A string that represents the currently selected text in the text box.</returns>
		[SRDescription("TextBoxSelectedTextDescr")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatAppearance")]
		public string SelectedText
		{
			get
			{
				return this._selectedText;
			}
			set
			{
				if ((this._selectedText != value))
				{
					this._selectedText = value;
				}
			}
		}

		private string _selectedText;

		/// <summary>
		/// Gets or sets the number of characters selected in the<see cref="ToolStripTextBox" />.
		///</summary>
		/// <returns>The number of characters selected in the<see cref="ToolStripTextBox" />.</returns>
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectionLengthDescr")]
		public int SelectionLength
		{
			get
			{
				return this._selectionLength;
			}
			set
			{
				if ((this._selectionLength != value))
				{
					this._selectionLength = value;
				}
			}
		}

		private int _selectionLength;

		/// <summary>
		/// Gets or sets the starting point of text selected in the<see cref="ToolStripTextBox" />.
		///</summary>
		/// <returns>The starting position of text selected in the<see cref="ToolStripTextBox" />.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("TextBoxSelectionStartDescr")]
		public int SelectionStart
		{
			get
			{
				return this._selectionStart;
			}
			set
			{
				if ((this._selectionStart != value))
				{
					this._selectionStart = value;
				}
			}
		}

		private int _selectionStart;

		/// <summary>
		/// Gets or sets a value indicating whether the defined shortcuts are enabled.
		///</summary>
		/// <returns>true to enable the shortcuts; otherwise, false.</returns>
		[SRDescription("TextBoxShortcutsEnabledDescr")]
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		public bool ShortcutsEnabled
		{
			get
			{
				return this._shortcutsEnabled;
			}
			set
			{
				if ((this._shortcutsEnabled != value))
				{
					this._shortcutsEnabled = value;
				}
			}
		}

		private bool _shortcutsEnabled;

		/// <summary>
		/// Gets the length of text in the control.
		///</summary>
		/// <returns>The number of characters contained in the text of the <see cref="ToolStripTextBox" />.</returns>
		[Browsable(false)]
		public int TextLength
		{
			get
			{
				return this._textLength;
			}
		}

		private int _textLength;

		/// <summary>
		/// Gets or sets how text is aligned in a <see cref="TextBox" /> control.
		///</summary>
		/// <returns>One of the <see cref="HorizontalAlignment" /> enumeration values that specifies how text is aligned in the control. The default is <see cref="HorizontalAlignment.Left" />.</returns>
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(HorizontalAlignment.Left)]
		[SRDescription("TextBoxTextAlignDescr")]
		public HorizontalAlignment TextBoxTextAlign
		{
			get
			{
				return this._textBoxTextAlign;
			}
			set
			{
				if ((this._textBoxTextAlign != value))
				{
					this._textBoxTextAlign = value;
				}
			}
		}

		private HorizontalAlignment _textBoxTextAlign;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		[SRDescription("TextBoxWordWrapDescr")]
		[Localizable(true)]
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool WordWrap
		{
			get
			{
				return this._wordWrap;
			}
			set
			{
				if ((this._wordWrap != value))
				{
					this._wordWrap = value;
				}
			}
		}

		private bool _wordWrap;
		private Control _c;

		#endregion

		#region Methods

		private static Control CreateControlInstance()
		{
			TextBox textBox = new ToolStripTextBoxControl
			{
				BorderStyle = BorderStyle.Double,
				AutoSize = true
			};
			return textBox;
		}

		/// <summary>
		/// Raises the <see cref="ToolStripTextBox.AcceptsTabChanged" /> event. 
		///</summary>
		/// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnAcceptsTabChangedDescr")]
		protected virtual void OnAcceptsTabChanged(System.EventArgs e)
		{
			if ((this.AcceptsTabChanged != null))
			{
				AcceptsTabChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripTextBox.BorderStyleChanged" /> event.
		///</summary>
		/// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnBorderStyleChangedDescr")]
		protected virtual void OnBorderStyleChanged(System.EventArgs e)
		{
			if ((this.BorderStyleChanged != null))
			{
				BorderStyleChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripTextBox.HideSelectionChanged" /> event.
		///</summary>
		/// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnHideSelectionChangedDescr")]
		protected virtual void OnHideSelectionChanged(System.EventArgs e)
		{
			if ((this.HideSelectionChanged != null))
			{
				HideSelectionChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripTextBox.ModifiedChanged" /> event.
		///</summary>
		/// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnModifiedChangedDescr")]
		protected virtual void OnModifiedChanged(System.EventArgs e)
		{
			if ((this.ModifiedChanged != null))
			{
				ModifiedChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripTextBox.MultilineChanged" /> event.
		///</summary>
		/// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnMultilineChangedDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnMultilineChanged(System.EventArgs e)
		{
			if ((this.MultilineChanged != null))
			{
				MultilineChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripTextBox.ReadOnlyChanged" /> event.
		///</summary>
		/// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRDescription("TextBoxBaseOnReadOnlyChangedDescr")]
		[SRCategory("CatPropertyChanged")]
		protected virtual void OnReadOnlyChanged(System.EventArgs e)
		{
			if ((this.ReadOnlyChanged != null))
			{
				ReadOnlyChanged(this, e);
			}
		}

		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripTextBoxTextBoxTextAlignChangedDescr")]
		protected virtual void OnTextBoxTextAlignChanged(System.EventArgs e)
		{
			if ((this.TextBoxTextAlignChanged != null))
			{
				TextBoxTextAlignChanged(this, e);
			}
		}

		protected override void OnSubscribeControlEvents(Control control)
		{
			base.OnSubscribeControlEvents(control);
			// TODO: Implement
		}

		protected override void OnUnsubscribeControlEvents(Control control)
		{
			base.OnUnsubscribeControlEvents(control);
			// TODO: Implement
		}

		/// <summary>
		/// Appends text to the current text of the <see cref="ToolStripTextBox" />.
		///</summary>
		/// <param name="text">The text to append to the current contents of the <see cref="ToolStripTextBox" />.</param>
		public void AppendText(string text)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Clears all text from the <see cref="ToolStripTextBox" /> control.
		///</summary>
		public void Clear()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Clears information about the most recent operation from the undo buffer of the <see cref="ToolStripTextBox" />.
		///</summary>
		public void ClearUndo()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Copies the current selection in the <see cref="ToolStripTextBox" /> to the Clipboard.
		///</summary>
		public void Copy()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Moves the current selection in the <see cref="ToolStripTextBox" /> to the Clipboard.
		///</summary>
		public void Cut()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Specifies that the value of the <see cref="ToolStripTextBox.SelectionLength" /> property is zero so that no characters are selected in the control.
		///</summary>
		public void DeselectAll()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Retrieves the character that is closest to the specified location within the control.
		///</summary>
		/// <returns>The character at the specified location.</returns>
		/// <param name="pt">The location from which to seek the nearest character.</param>
		public char GetCharFromPosition(Point pt)
		{
			// TODO: Implement
			throw new NotImplementedException();
		}

		/// <summary>
		/// Retrieves the index of the character nearest to the specified location.
		///</summary>
		/// <returns>The zero-based character index at the specified location.</returns>
		/// <param name="pt">The location to search.</param>
		public int GetCharIndexFromPosition(Point pt)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Retrieves the index of the first character of a given line.
		///</summary>
		/// <returns>The zero-based character index in the specified line.</returns>
		/// <param name="lineNumber">The line for which to get the index of its first character.</param>
		public int GetFirstCharIndexFromLine(int lineNumber)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Retrieves the index of the first character of the current line.
		///</summary>
		/// <returns>The zero-based character index in the current line.</returns>
		public int GetFirstCharIndexOfCurrentLine()
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Retrieves the line number from the specified character position within the text of the control.
		///</summary>
		/// <returns>The zero-based line number in which the character index is located.</returns>
		/// <param name="index">The character index position to search.</param>
		public int GetLineFromCharIndex(int index)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Retrieves the location within the control at the specified character index.
		///</summary>
		/// <returns>The location of the specified character.</returns>
		/// <param name="index">The index of the character for which to retrieve the location.</param>
		public Point GetPositionFromCharIndex(int index)
		{
			// TODO: Implement
			return new System.Drawing.Point();
		}

		/// <summary>
		/// Replaces the current selection in the text box with the contents of the Clipboard.
		///</summary>
		public void Paste()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Scrolls the contents of the control to the current caret position.
		///</summary>
		public void ScrollToCaret()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Selects a range of text in the text box.
		///</summary>
		/// <param name="start">The position of the first character in the current text selection within the text box.</param>
		/// <param name="length">The number of characters to select.</param>
		public void Select(int start, int length)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Selects all text in the text box.
		///</summary>
		public void SelectAll()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Undoes the last edit operation in the text box.
		///</summary>
		public void Undo()
		{
			// TODO: Implement
		}

		#endregion

		#region Wisej Implementation

		protected override void OnWebEvent(WisejEventArgs e)
		{
			base.OnWebEvent(e);
		}

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender(config);
		}

		protected override void OnWebUpdate(dynamic config)
		{
			base.OnWebUpdate(config);
		}

		#endregion

	}

}
