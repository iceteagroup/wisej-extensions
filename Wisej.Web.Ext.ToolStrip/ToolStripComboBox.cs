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
using System.ComponentModel;
using System.Drawing;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Represents a <see cref="ToolStripComboBox" /> that is properly rendered in a <see cref="ToolStrip" />.
	///</summary>
	public partial class ToolStripComboBox : ToolStripControlHost
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripComboBox" /> class.
		///</summary>
		public ToolStripComboBox()
			: base(CreateControlInstance())
		{
			ToolStripComboBoxControl combo = (ToolStripComboBoxControl)Control;
			combo.Owner = this;
		}



		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripComboBox" /> class with the specified name. 
		///</summary>
		/// <param name="name">The name of the <see cref="ToolStripComboBox" />.</param>
		public ToolStripComboBox(string name)
			: this()
		{
			this.Name = name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripComboBox" /> class derived from a base control.
		///</summary>
		/// <exception cref="System.NotSupportedException">The operation is not supported. </exception>
		/// <param name="c">The base control. </param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ToolStripComboBox(Control c)
			: base(c)
		{
			throw new NotSupportedException();
		}

		#endregion

		#region Events

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler DoubleClick;

		/// <summary>
		/// Occurs when the drop-down portion of a <see cref="ToolStripComboBox" /> is shown.
		///</summary>
		[SRDescription("ComboBoxOnDropDownDescr")]
		[SRCategory("CatBehavior")]
		public event EventHandler DropDown;

		/// <summary>
		/// Occurs when the drop-down portion of the <see cref="ToolStripComboBox" /> has closed.
		///</summary>
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnDropDownClosedDescr")]
		public event EventHandler DropDownClosed;

		/// <summary>
		/// Occurs when the <see cref="ToolStripComboBox.DropDownStyle" /> property has changed.
		///</summary>
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownStyleChangedDescr")]
		public event EventHandler DropDownStyleChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripComboBox.SelectedIndex" /> property has changed.
		///</summary>
		[SRCategory("CatBehavior")]
		[SRDescription("selectedIndexChangedEventDescr")]
		public event EventHandler SelectedIndexChanged;

		/// <summary>
		/// Occurs when the <see cref="ToolStripComboBox" /> text has changed.
		///</summary>
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnTextUpdateDescr")]
		public event EventHandler TextUpdate;

		#endregion

		#region Properties

		//TODO: (Alaa) Implement?
		/// <summary>
		/// Gets or sets the custom string collection to use when the <see cref="ToolStripComboBox.AutoCompleteSource" /> property is set to <see cref="AutoCompleteSource.CustomSource" />.
		///</summary>
		/// <returns>An <see cref="AutoCompletestringCollection" /> that contains the strings.</returns>
		//[Localizable(true)]
		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		//[SRDescription("ComboBoxAutoCompleteCustomSourceDescr")]
		//[Browsable(true)]
		//[EditorBrowsable(EditorBrowsableState.Always)]
		//public AutoCompleteStringCollection AutoCompleteCustomSource
		//{
		//	get
		//	{
		//		return this._autoCompleteCustomSource;
		//	}
		//	set
		//	{
		//		if ((this._autoCompleteCustomSource != value))
		//		{
		//			this._autoCompleteCustomSource = value;
		//		}
		//	}
		//}

		//private AutoCompletestringCollection _autoCompleteCustomSource;

		/// <summary>
		/// Gets or sets a value that indicates the text completion behavior of the <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>One of the <see cref="AutoCompleteMode" /> values. The default is <see cref="AutoCompleteMode.None" />.</returns>
		[DefaultValue(AutoCompleteMode.None)]
		[SRDescription("ComboBoxAutoCompleteModeDescr")]
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

		//TODO: Implement in Wisej?
		/// <summary>
		/// Gets or sets the source of complete strings used for automatic completion.
		///</summary>
		/// <returns>One of the <see cref="AutoCompleteSource" /> values. The default is <see cref="AutoCompleteSource.None" />.</returns>
		//[SRDescription("ComboBoxAutoCompleteSourceDescr")]
		//[DefaultValue(AutoCompleteSource.None)]
		//[Browsable(true)]
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
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>An <see cref="System.Drawing.Image" />.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
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
		/// <returns>An <see cref="ImageLayout" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
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
		/// Gets a <see cref="ComboBox" /> in which the user can enter text, along with a list from which the user can select.
		///</summary>
		/// <returns>A <see cref="ComboBox" /> for a <see cref="ToolStrip" />.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ComboBox ComboBox
		{
			get
			{
				return this._comboBox;
			}
		}

		private ComboBox _comboBox;

		/// <summary>
		/// Gets the default size of the <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>The default <see cref="System.Drawing.Size" /> of the <see cref="ToolStripTextBox" /> in pixels. The default size is 100 x 20 pixels.</returns>
		public override Size DefaultSize
		{
			get
			{
				return this._defaultSize;
			}
		}

		private Size _defaultSize;

		/// <summary>
		/// Gets or sets the height, in pixels, of the drop-down portion box of a <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>The height, in pixels, of the drop-down box.</returns>
		[Browsable(true)]
		[SRDescription("ComboBoxDropDownHeightDescr")]
		[SRCategory("CatBehavior")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(106)]
		public int DropDownHeight
		{
			get
			{
				return this._dropDownHeight;
			}
			set
			{
				if ((this._dropDownHeight != value))
				{
					this._dropDownHeight = value;
				}
			}
		}

		private int _dropDownHeight;

		/// <summary>
		/// Gets or sets a value specifying the style of the <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>One of the <see cref="ComboBoxStyle" /> values. The default is <see cref="ComboBoxStyle.DropDown" />.</returns>
		[SRCategory("CatAppearance")]
		[DefaultValue(ComboBoxStyle.DropDown)]
		[SRDescription("ComboBoxStyleDescr")]
		public ComboBoxStyle DropDownStyle
		{
			get
			{
				return this._dropDownStyle;
			}
			set
			{
				if ((this._dropDownStyle != value))
				{
					this._dropDownStyle = value;
				}
			}
		}

		private ComboBoxStyle _dropDownStyle;

		/// <summary>
		/// Gets or sets the width, in pixels, of the drop-down portion of a <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>The width, in pixels, of the drop-down box.</returns>
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownWidthDescr")]
		public int DropDownWidth
		{
			get
			{
				return this._dropDownWidth;
			}
			set
			{
				if ((this._dropDownWidth != value))
				{
					this._dropDownWidth = value;
				}
			}
		}

		private int _dropDownWidth;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="ToolStripComboBox" /> currently displays its drop-down portion.
		///</summary>
		/// <returns>true if the <see cref="ToolStripComboBox" /> currently displays its drop-down portion; otherwise, false.</returns>
		[SRDescription("ComboBoxDroppedDownDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool DroppedDown
		{
			get
			{
				return this._droppedDown;
			}
			set
			{
				if ((this._droppedDown != value))
				{
					this._droppedDown = value;
				}
			}
		}

		private bool _droppedDown;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="ToolStripComboBox" /> should resize to avoid showing partial items.
		///</summary>
		/// <returns>true if the list portion can contain only complete items; otherwise, false. The default is true.</returns>
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("ComboBoxIntegralHeightDescr")]
		public bool IntegralHeight
		{
			get
			{
				return this._integralHeight;
			}
			set
			{
				if ((this._integralHeight != value))
				{
					this._integralHeight = value;
				}
			}
		}

		private bool _integralHeight;

		/// <summary>
		/// Gets a collection of the items contained in this <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>A collection of items.</returns>
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ComboBoxItemsDescr")]
		public ComboBox.ObjectCollection Items
		{
			get
			{
				return this._items;
			}
		}

		private ComboBox.ObjectCollection _items;

		/// <summary>
		/// Gets or sets the maximum number of items to be shown in the drop-down portion of the <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>The maximum number of items in the drop-down portion. The minimum for this property is 1 and the maximum is 100.</returns>
		[SRCategory("CatBehavior")]
		[DefaultValue(8)]
		[Localizable(true)]
		[SRDescription("ComboBoxMaxDropDownItemsDescr")]
		public int MaxDropDownItems
		{
			get
			{
				return this._maxDropDownItems;
			}
			set
			{
				if ((this._maxDropDownItems != value))
				{
					this._maxDropDownItems = value;
				}
			}
		}

		private int _maxDropDownItems;

		/// <summary>
		/// Gets or sets the maximum number of characters allowed in the editable portion of a combo box.
		///</summary>
		/// <returns>The maximum number of characters the user can enter. Values of less than zero are reset to zero, which is the default value.</returns>
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Localizable(true)]
		[SRDescription("ComboBoxMaxLengthDescr")]
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
		/// Gets or sets the index specifying the currently selected item.
		///</summary>
		/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
		[SRDescription("ComboBoxSelectedIndexDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int SelectedIndex
		{
			get
			{
				return this._selectedIndex;
			}
			set
			{
				if ((this._selectedIndex != value))
				{
					this._selectedIndex = value;
				}
			}
		}

		private int _selectedIndex;

		/// <summary>
		/// Gets or sets currently selected item in the <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>The object that is the currently selected item or null if there is no currently selected item.</returns>
		[SRDescription("ComboBoxSelectedItemDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public object SelectedItem
		{
			get
			{
				return this._selectedItem;
			}
			set
			{
				if ((this._selectedItem != value))
				{
					this._selectedItem = value;
				}
			}
		}

		private object _selectedItem;

		/// <summary>
		/// Gets or sets the text that is selected in the editable portion of a <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>A string that represents the currently selected text in the combo box. If <see cref="ToolStripComboBox.DropDownStyle" /> is set to DropDownList, the return value is an empty string ("").</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[SRDescription("ComboBoxSelectedTextDescr")]
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
		/// Gets or sets the number of characters selected in the editable portion of the <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>The number of characters selected in the <see cref="ToolStripComboBox" />.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectionLengthDescr")]
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
		/// Gets or sets the starting index of text selected in the <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>The zero-based index of the first character in the string of the current text selection.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[SRDescription("ComboBoxSelectionStartDescr")]
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
		/// Gets or sets a value indicating whether the items in the <see cref="ToolStripComboBox" /> are sorted.
		///</summary>
		/// <returns>true if the combo box is sorted; otherwise, false. The default is false.</returns>
		[SRDescription("ComboBoxSortedDescr")]
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		public bool Sorted
		{
			get
			{
				return this._sorted;
			}
			set
			{
				if ((this._sorted != value))
				{
					this._sorted = value;
				}
			}
		}

		private bool _sorted;

		#endregion

		#region Methods

		private static Control CreateControlInstance()
		{
			ComboBox comboBox = new ToolStripComboBoxControl();
			return comboBox;
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.DoubleClick" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnDoubleClick(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.DoubleClick != null))
			{
				DoubleClick(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripComboBox.DropDown" /> event. 
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRDescription("ComboBoxOnDropDownDescr")]
		[SRCategory("CatBehavior")]
		protected virtual void OnDropDown(System.EventArgs e)
		{
			if ((this.DropDown != null))
			{
				DropDown(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripComboBox.DropDownClosed" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnDropDownClosedDescr")]
		protected virtual void OnDropDownClosed(System.EventArgs e)
		{
			if ((this.DropDownClosed != null))
			{
				DropDownClosed(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripComboBox.DropDownStyleChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownStyleChangedDescr")]
		protected virtual void OnDropDownStyleChanged(System.EventArgs e)
		{
			if ((this.DropDownStyleChanged != null))
			{
				DropDownStyleChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripComboBox.SelectedIndexChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatBehavior")]
		[SRDescription("selectedIndexChangedEventDescr")]
		protected virtual void OnSelectedIndexChanged(System.EventArgs e)
		{
			if ((this.SelectedIndexChanged != null))
			{
				SelectedIndexChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripComboBox.TextUpdate" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnTextUpdateDescr")]
		protected virtual void OnTextUpdate(System.EventArgs e)
		{
			if ((this.TextUpdate != null))
			{
				TextUpdate(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ComboBox.SelectionChangeCommitted" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		protected virtual void OnSelectionChangeCommitted(EventArgs e)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Maintains performance when items are added to the <see cref="ToolStripComboBox" /> one at a time.
		///</summary>
		public void BeginUpdate()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Resumes painting the <see cref="ToolStripComboBox" /> control after painting is suspended by the <see cref="ToolStripComboBox.BeginUpdate" /> method.
		///</summary>
		public void EndUpdate()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Finds the first item in the <see cref="ToolStripComboBox" /> that starts with the specified string.
		///</summary>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
		/// <param name="s">The <see cref="System.string" /> to search for.</param>
		public int Findstring(string s)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Finds the first item after the given index which starts with the given string. 
		///</summary>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
		/// <param name="s">The <see cref="System.string" /> to search for.</param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
		public int Findstring(string s, int startIndex)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Finds the first item in the <see cref="ToolStripComboBox" /> that exactly matches the specified string.
		///</summary>
		/// <returns>The zero-based index of the first item found; -1 if no match is found.</returns>
		/// <param name="s">The <see cref="System.string" /> to search for.</param>
		public int FindstringExact(string s)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Finds the first item after the specified index that exactly matches the specified string.
		///</summary>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
		/// <param name="s">The <see cref="System.string" /> to search for.</param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
		public int FindstringExact(string s, int startIndex)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Returns the height, in pixels, of an item in the <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>The height, in pixels, of the item at the specified index.</returns>
		/// <param name="index">The index of the item to return the height of.</param>
		public int GetItemHeight(int index)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Selects a range of text in the editable portion of the <see cref="ToolStripComboBox" />.
		///</summary>
		/// <exception cref="System.ArgumentException">The <paramref name="start" /> is less than zero.-or- <paramref name="start" /> minus <paramref name="length" /> is less than zero. </exception>
		/// <param name="start">The position of the first character in the current text selection within the text box.</param>
		/// <param name="length">The number of characters to select.</param>
		public void Select(int start, int length)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Selects all the text in the editable portion of the <see cref="ToolStripComboBox" />.
		///</summary>
		public void SelectAll()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Unsubscribes events from the specified control.
		///</summary>
		/// <param name="control">The control from which to unsubscribe events.</param>
		protected override void OnUnsubscribeControlEvents(Control control)
		{
			base.OnUnsubscribeControlEvents(control);
			// TODO: Implement
		}

		/// <summary>
		/// Returns a string representation of the <see cref="ToolStripComboBox" />.
		///</summary>
		/// <returns>A string that represents the <see cref="ToolStripComboBox" />.</returns>
		public override string ToString()
		{
			// TODO: Implement
			return "";
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
