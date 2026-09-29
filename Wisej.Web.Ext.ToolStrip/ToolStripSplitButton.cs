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
	/// Represents a combination of a standard button on the left and a drop-down button on the right, or the other way around if the value of <see cref="ToolStripItem.RightToLeft" /> is <see cref="RightToLeft.Yes" />.
	/// </summary>
	/// <remarks>
	/// Clicking the standard button portion raises the <see cref="ToolStripSplitButton.ButtonClick" /> event; clicking the arrow portion opens the
	/// <see cref="ToolStripDropDownItem.DropDown" />.
	/// </remarks>
	/// <example>
	/// Creating a "Paste" split button with additional paste options:
	/// <code><![CDATA[
	/// var paste = new ToolStripSplitButton("Paste");
	/// paste.ButtonClick += (s, e) => PasteText();
	/// paste.DropDownItems.Add("Paste as plain text", null, (s, e) => PastePlainText());
	/// paste.DropDownItems.Add("Paste special...", null, (s, e) => ShowPasteSpecialDialog());
	/// this.toolStrip1.Items.Add(paste);
	/// ]]></code>
	/// </example>
	public class ToolStripSplitButton : ToolStripDropDownItem
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripSplitButton" /> class.
		///</summary>
		public ToolStripSplitButton()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripSplitButton" /> class with the specified text.
		/// </summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripSplitButton" />.</param>
		public ToolStripSplitButton(string text)
		{
			this.Text = text;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripSplitButton" /> class with the specified image.
		/// </summary>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripSplitButton" />.</param>
		public ToolStripSplitButton(Image image)
		{
			this.Image = image;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripSplitButton" /> class with the specified text and image.
		///</summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripSplitButton" />.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripSplitButton" />.</param>
		public ToolStripSplitButton(string text, Image image)
		{
			this.Text = text;
			this.Image = image;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripSplitButton" /> class with the specified text and image and the items displayed in the drop-down.
		/// </summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripSplitButton" />.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripSplitButton" />.</param>
		/// <param name="dropDownItems">The items to add to <see cref="ToolStripDropDownItem.DropDownItems" />.</param>
		public ToolStripSplitButton(string text, Image image, ToolStripItem[] dropDownItems)
		{
			this.Text = text;
			this.Image = image;
			this.DropDownItems.AddRange(dropDownItems);
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the standard button portion of a <see cref="ToolStripSplitButton" /> is clicked.
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripSplitButtonOnButtonClickDescr")]
		public event EventHandler ButtonClick;

		/// <summary>
		/// Occurs when the standard button portion of a <see cref="ToolStripSplitButton" /> is double-clicked.
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripSplitButtonOnButtonDoubleClickDescr")]
		public event EventHandler ButtonDoubleClick;

		/// <summary>
		/// Occurs when the <see cref="ToolStripSplitButton.DefaultItem" /> has changed.
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripSplitButtonOnDefaultItemChangedDescr")]
		public event EventHandler DefaultItemChanged;

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets a value indicating whether the <see cref="ToolStripItem.Text" /> of the <see cref="ToolStripSplitButton" /> is used as its tooltip when no custom tooltip text is set.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripItem.Text" /> is used as the tooltip; otherwise, false. The default is true.</returns>
		[DefaultValue(true)]
		public bool AutoToolTip
		{
			get
			{
				return this._autoToolTip;
			}
			set
			{
				if ((this._autoToolTip != value))
				{
					this._autoToolTip = value;
				}
			}
		}

		private bool _autoToolTip;

		/// <summary>
		/// Returns the size and location of the standard button portion of a <see cref="ToolStripSplitButton" />.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> that represents the size and location of the standard button portion of a <see cref="ToolStripSplitButton" />.</returns>
		[Browsable(false)]
		public Rectangle ButtonBounds
		{
			get
			{
				return this._buttonBounds;
			}
		}

		private Rectangle _buttonBounds;

		/// <summary>
		/// Returns a value indicating whether the button portion of the <see cref="ToolStripSplitButton" /> is in the pressed state.
		/// </summary>
		/// <returns>true if the button portion of the <see cref="ToolStripSplitButton" /> is in the pressed state; otherwise, false.</returns>
		[Browsable(false)]
		public bool ButtonPressed
		{
			get
			{
				return this._buttonPressed;
			}
		}

		private bool _buttonPressed;

		/// <summary>
		/// Returns a value indicating whether the standard button portion of a <see cref="ToolStripSplitButton" /> is selected or the <see cref="ToolStripSplitButton.DropDownButtonPressed" /> property is true.
		/// </summary>
		/// <returns>true if the button portion of a <see cref="ToolStripSplitButton" /> is selected or <see cref="ToolStripSplitButton.DropDownButtonPressed" /> is true; otherwise, false.</returns>
		[Browsable(false)]
		public bool ButtonSelected
		{
			get
			{
				return this._buttonSelected;
			}
		}

		private bool _buttonSelected;

		/// <summary>
		/// Returns the default value of the <see cref="ToolStripSplitButton.AutoToolTip" /> property.
		/// </summary>
		/// <returns>true in all cases.</returns>
		public override bool DefaultAutoToolTip
		{
			get
			{
				return this._defaultAutoToolTip;
			}
		}

		private bool _defaultAutoToolTip;

		/// <summary>
		/// Returns or sets the drop-down item whose <see cref="ToolStripItem.Click" /> event is raised when the standard button portion of the <see cref="ToolStripSplitButton" /> is clicked.
		/// </summary>
		/// <returns>The <see cref="ToolStripItem" /> activated by the button portion of the <see cref="ToolStripSplitButton" />. The default value is null.</returns>
		/// <remarks>
		/// When set, clicking the button portion raises <see cref="ToolStripSplitButton.ButtonClick" /> and then performs a click on the default item.
		/// Changing the value raises the <see cref="ToolStripSplitButton.DefaultItemChanged" /> event.
		/// </remarks>
		/// <example>
		/// Repeating the last option chosen from the drop-down when the button is clicked:
		/// <code><![CDATA[
		/// private void toolStripSplitButtonExport_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		/// {
		///     this.toolStripSplitButtonExport.DefaultItem = e.ClickedItem;
		///     this.toolStripSplitButtonExport.Text = "Export " + e.ClickedItem.Text;
		/// }
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DefaultValue(null)]
		public ToolStripItem DefaultItem
		{
			get
			{
				return this._defaultItem;
			}
			set
			{
				if ((this._defaultItem != value))
				{
					this._defaultItem = value;
				}
			}
		}

		private ToolStripItem _defaultItem;

		/// <summary>
		/// Returns the size and location of the drop-down button portion of a <see cref="ToolStripSplitButton" />.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> that represents the size and location of the drop-down button portion of a <see cref="ToolStripSplitButton" />.</returns>
		[Browsable(false)]
		public Rectangle DropDownButtonBounds
		{
			get
			{
				return this._dropDownButtonBounds;
			}
		}

		private Rectangle _dropDownButtonBounds;

		/// <summary>
		/// Returns a value indicating whether the drop-down portion of the <see cref="ToolStripSplitButton" /> is in the pressed state.
		/// </summary>
		/// <returns>true if the drop-down portion of the <see cref="ToolStripSplitButton" /> is in the pressed state; otherwise, false.</returns>
		[Browsable(false)]
		public bool DropDownButtonPressed
		{
			get
			{
				return this._dropDownButtonPressed;
			}
		}

		private bool _dropDownButtonPressed;

		/// <summary>
		/// Returns a value indicating whether the drop-down button portion of a <see cref="ToolStripSplitButton" /> is selected.
		/// </summary>
		/// <returns>true if the drop-down button portion of a <see cref="ToolStripSplitButton" /> is selected; otherwise, false.</returns>
		[Browsable(false)]
		public bool DropDownButtonSelected
		{
			get
			{
				return this._dropDownButtonSelected;
			}
		}

		private bool _dropDownButtonSelected;

		/// <summary>
		/// Returns or sets the width, in pixels, of the drop-down button portion of a <see cref="ToolStripSplitButton" />.
		/// </summary>
		/// <returns>An <see cref="System.Int32" /> representing the width in pixels.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">The specified value is less than zero (0).</exception>
		/// <remarks>
		/// Call <see cref="ToolStripSplitButton.ResetDropDownButtonWidth" /> to restore the default width.
		/// </remarks>
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripSplitButtonDropDownButtonWidthDescr")]
		public int DropDownButtonWidth
		{
			get
			{
				return this._dropDownButtonWidth;
			}
			set
			{
				if ((this._dropDownButtonWidth != value))
				{
					this._dropDownButtonWidth = value;
				}
			}
		}

		private int _dropDownButtonWidth;

		/// <summary>
		/// Returns the boundaries of the separator between the standard and drop-down button portions of a <see cref="ToolStripSplitButton" />.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> that represents the size and location of the separator.</returns>
		[Browsable(false)]
		public Rectangle SplitterBounds
		{
			get
			{
				return this._splitterBounds;
			}
		}

		private Rectangle _splitterBounds;

		#endregion

		#region Methods

		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			// TODO: Implement
			return new ToolStripDropDown();
		}

		/// <summary>
		/// Raises the <see cref="ToolStripSplitButton.ButtonClick" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripSplitButtonOnButtonClickDescr")]
		protected virtual void OnButtonClick(System.EventArgs e)
		{
			if ((this.ButtonClick != null))
			{
				ButtonClick(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripSplitButton.ButtonDoubleClick" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripSplitButtonOnButtonDoubleClickDescr")]
		protected virtual void OnButtonDoubleClick(System.EventArgs e)
		{
			if ((this.ButtonDoubleClick != null))
			{
				ButtonDoubleClick(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripSplitButton.DefaultItemChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripSplitButtonOnDefaultItemChangedDescr")]
		protected virtual void OnDefaultItemChanged(System.EventArgs e)
		{
			if ((this.DefaultItemChanged != null))
			{
				DefaultItemChanged(this, e);
			}
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
		/// Raises the <see cref="Control.MouseUp" /> event.
		///</summary>
		/// <param name="e">A <see cref="MouseEventArgs" /> that contains the event data. </param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
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
		/// Raises the <see cref="Control.RightToLeftChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Resets the <see cref="ToolStripSplitButton.DropDownButtonWidth" /> property to its default value.
		/// </summary>
		/// <example>
		/// Restoring the default arrow width:
		/// <code><![CDATA[
		/// this.toolStripSplitButton1.ResetDropDownButtonWidth();
		/// ]]></code>
		/// </example>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetDropDownButtonWidth()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Creates a new accessibility object for the <see cref="ToolStripSplitButton" />.
		///</summary>
		/// <returns>A new accessibility object for the <see cref="ToolStripSplitButton" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			// TODO: Implement
			return new AccessibleObject();
		}

		/// <summary>
		/// Raises the <see cref="ToolStripSplitButton.ButtonClick" /> event, as if the user clicked the standard button portion, if the <see cref="ToolStripItem.Enabled" /> property is true.
		/// </summary>
		/// <example>
		/// Triggering the default action of a split button from code:
		/// <code><![CDATA[
		/// private void buttonSend_Click(object sender, EventArgs e)
		/// {
		///     this.toolStripSplitButtonSend.PerformButtonClick();
		/// }
		/// ]]></code>
		/// </example>
		public void PerformButtonClick()
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
