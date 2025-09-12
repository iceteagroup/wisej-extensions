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
	/// Represents a selectable option displayed on a <see cref="MenuStrip" /> or <see cref="ContextMenuStrip" />. Although <see cref="ToolStripMenuItem" /> replaces and adds functionality to the <see cref="MenuItem" /> control of previous versions, <see cref="MenuItem" /> is retained for both backward compatibility and future use if you choose.
	///</summary>
	public class ToolStripMenuItem : ToolStripDropDownItem
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripMenuItem" /> class.
		///</summary>
		public ToolStripMenuItem()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripMenuItem" /> class that displays the specified text.
		///</summary>
		/// <param name="text">The text to display on the menu item.</param>
		public ToolStripMenuItem(string text)
		{
			this.Text = text;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripMenuItem" /> class that displays the specified <see cref="System.Drawing.Image" />.
		///</summary>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to display on the control.</param>
		public ToolStripMenuItem(Image image)
		{
			this.Image = image;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripMenuItem" /> class that displays the specified text and image.
		///</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to display on the control.</param>
		public ToolStripMenuItem(string text, Image image)
		{
			this.Text = text;
			this.Image = image;
			// TODO: Implement
		}

		public ToolStripMenuItem(string text, Image image, ToolStripItem[] dropDownItems)
		{
			this.Text = text;
			this.Image = image;
			this.DropDownItems.AddRange(dropDownItems);
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripMenuItem.Checked" /> property changes.
		///</summary>
		[SRDescription("CheckBoxOnCheckedChangedDescr")]
		public event EventHandler CheckedChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripMenuItem.CheckState" /> property changes.
		///</summary>
		[SRDescription("CheckBoxOnCheckStateChangedDescr")]
		public event EventHandler CheckStateChanged;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the default size of the <see cref="ToolStripMenuItem" />.
		///</summary>
		/// <returns>The <see cref="System.Drawing.Size" /> of the <see cref="ToolStripMenuItem" />, measured in pixels. The default is 100 pixels horizontally.</returns>
		public override Size DefaultSize
		{
			get
			{
				return this._defaultSize;
			}
		}

		private Size _defaultSize;

		/// <summary>
		/// Gets the internal spacing within the <see cref="ToolStripMenuItem" />.
		///</summary>
		/// <returns>A <see cref="Padding" /> value representing the spacing.</returns>
		public new Padding DefaultPadding
		{
			get
			{
				return this._defaultPadding;
			}
		}

		private Padding _defaultPadding;

		/// <summary>
		/// Gets or sets a value indicating whether the control is enabled. 
		///</summary>
		/// <returns>true if the control is enabled; otherwise, false. The default is true.</returns>
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemEnabledDescr")]
		[DefaultValue(true)]
		public override bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if ((this._enabled != value))
				{
					this._enabled = value;
				}
			}
		}

		private bool _enabled;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="ToolStripMenuItem" /> is checked.
		///</summary>
		/// <returns>true if the <see cref="ToolStripMenuItem" /> is checked or is in an indeterminate state; otherwise, false. The default is false.</returns>
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("CheckBoxCheckedDescr")]
		public bool Checked
		{
			get
			{
				return this._checked;
			}
			set
			{
				if ((this._checked != value))
				{
					this._checked = value;
				}
			}
		}

		private bool _checked;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="ToolStripMenuItem" /> should automatically appear checked and unchecked when clicked.
		///</summary>
		/// <returns>true if the <see cref="ToolStripMenuItem" /> should automatically appear checked when clicked; otherwise, false. The default is false.</returns>
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripButtonCheckOnClickDescr")]
		public bool CheckOnClick
		{
			get
			{
				return this._checkOnClick;
			}
			set
			{
				if ((this._checkOnClick != value))
				{
					this._checkOnClick = value;
				}
			}
		}

		private bool _checkOnClick;

		/// <summary>
		/// Gets or sets a value indicating whether a <see cref="ToolStripMenuItem" /> is in the checked, unchecked, or indeterminate state.
		///</summary>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The <see cref="ToolStripMenuItem.CheckState" /> property is not set to one of the <see cref="CheckState" /> values. </exception>
		/// <returns>One of the <see cref="CheckState" /> values. The default is Unchecked.</returns>
		[SRDescription("CheckBoxCheckStateDescr")]
		[SRCategory("CatAppearance")]
		[DefaultValue(CheckState.Unchecked)]
		public CheckState CheckState
		{
			get
			{
				return this._checkState;
			}
			set
			{
				if ((this._checkState != value))
				{
					this._checkState = value;
				}
			}
		}

		private CheckState _checkState;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="ToolStripMenuItem" /> is attached to the <see cref="ToolStrip" /> or the <see cref="ToolStripOverflowButton" /> or whether it can float between the two.
		///</summary>
		/// <returns>One of the <see cref="ToolStripItemOverflow" /> values. The default is Never.</returns>
		[DefaultValue(ToolStripItemOverflow.Never)]
		[SRDescription("ToolStripItemOverflowDescr")]
		[SRCategory("CatLayout")]
		public ToolStripItemOverflow Overflow
		{
			get
			{
				return this._overflow;
			}
			set
			{
				if ((this._overflow != value))
				{
					this._overflow = value;
				}
			}
		}

		private ToolStripItemOverflow _overflow;

		/// <summary>
		/// Gets or sets the shortcut keys associated with the <see cref="ToolStripMenuItem" />.
		///</summary>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The property was not set to one of the <see cref="Keys" /> values.</exception>
		/// <returns>One of the <see cref="Keys" /> values. The default is <see cref="Keys.None" />.</returns>
		[Localizable(true)]
		[DefaultValue(Keys.None)]
		[SRDescription("MenuItemShortCutDescr")]
		public Keys ShortcutKeys
		{
			get
			{
				return this._shortcutKeys;
			}
			set
			{
				if ((this._shortcutKeys != value))
				{
					this._shortcutKeys = value;
				}
			}
		}

		private Keys _shortcutKeys;

		/// <summary>
		/// Gets or sets the shortcut key text.
		///</summary>
		/// <returns>A <see cref="System.String" /> representing the shortcut key.</returns>
		[SRDescription("ToolStripMenuItemShortcutKeyDisplayStringDescr")]
		[SRCategory("CatAppearance")]
		[DefaultValue(null)]
		[Localizable(true)]
		public string ShortcutKeyDisplayString
		{
			get
			{
				return this._shortcutKeyDisplayString;
			}
			set
			{
				if ((this._shortcutKeyDisplayString != value))
				{
					this._shortcutKeyDisplayString = value;
				}
			}
		}

		private string _shortcutKeyDisplayString;

		/// <summary>
		/// Gets or sets a value indicating whether the shortcut keys that are associated with the <see cref="ToolStripMenuItem" /> are displayed next to the <see cref="ToolStripMenuItem" />. 
		///</summary>
		/// <returns>true if the shortcut keys are shown; otherwise, false. The default is true.</returns>
		[Localizable(true)]
		[DefaultValue(true)]
		[SRDescription("MenuItemShowShortCutDescr")]
		public bool ShowShortcutKeys
		{
			get
			{
				return this._showShortcutKeys;
			}
			set
			{
				if ((this._showShortcutKeys != value))
				{
					this._showShortcutKeys = value;
				}
			}
		}

		private bool _showShortcutKeys;

		/// <summary>
		/// Gets a value indicating whether the <see cref="ToolStripMenuItem" /> appears on a multiple document interface (MDI) window list.
		///</summary>
		/// <returns>true if the <see cref="ToolStripMenuItem" /> appears on a MDI window list; otherwise, false.</returns>
		[Browsable(false)]
		public bool IsMdiWindowListEntry
		{
			get
			{
				return this._isMdiWindowListEntry;
			}
		}

		private bool _isMdiWindowListEntry;

		#endregion

		#region Methods

		/// <summary>
		/// Creates a generic <see cref="ToolStripDropDown" /> for which events can be defined.
		///</summary>
		/// <returns>A <see cref="ToolStripDropDown" />.</returns>
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			// TODO: Implement
			return new ToolStripDropDown();
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="ToolStripMenuItem" /> and optionally releases the managed resources. 
		///</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		protected override void Dispose(bool disposing)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.Click" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raised in response to the <see cref="ToolStripDropDownItem.HideDropDown" /> method.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		protected override void OnDropDownHide(EventArgs e)
		{
			base.OnDropDownHide(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raised in response to the <see cref="ToolStripDropDownItem.ShowDropDown" /> method.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		protected override void OnDropDownShow(EventArgs e)
		{
			base.OnDropDownShow(e);
			// TODO: Implement
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.MouseDown" /> event.
		///</summary>
		/// <param name="e">A <see cref="MouseEventArgs" /> that contains the event data. </param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.MouseUp" /> event.
		///</summary>
		/// <param name="e">A <see cref="MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.MouseEnter" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="Control.MouseLeave" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.OwnerChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		protected override void OnOwnerChanged(EventArgs e)
		{
			base.OnOwnerChanged(e);
			// TODO: Implement
		}


		/// <summary>
		/// Raises the <see cref="ToolStripMenuItem.CheckedChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRDescription("CheckBoxOnCheckedChangedDescr")]
		protected virtual void OnCheckedChanged(System.EventArgs e)
		{
			if ((this.CheckedChanged != null))
			{
				CheckedChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripMenuItem.CheckStateChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRDescription("CheckBoxOnCheckStateChangedDescr")]
		protected virtual void OnCheckStateChanged(System.EventArgs e)
		{
			if ((this.CheckStateChanged != null))
			{
				CheckStateChanged(this, e);
			}
		}

		/// <summary>
		/// Creates a new accessibility object for the <see cref="ToolStripMenuItem" />.
		///</summary>
		/// <returns>A new <see cref="Compatibility.AccessibleObject" /> for the <see cref="ToolStripMenuItem" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			// TODO: Implement
			return new Compatibility.AccessibleObject();
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
