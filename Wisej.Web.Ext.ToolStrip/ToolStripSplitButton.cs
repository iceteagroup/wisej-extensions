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
	/// Represents a selectable <see cref="ToolStripItem" /> that can contain text and images. 
	///</summary>
	/// <summary>
	/// Represents a combination of a standard button on the left and a drop-down button on the right, or the other way around if the value of <see cref="RightToLeft" /> is Yes.
	///</summary>
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
		///</summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripSplitButton" />.</param>
		public ToolStripSplitButton(string text)
		{
			this.Text = text;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripSplitButton" /> class with the specified image. 
		///</summary>
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
		/// Gets or sets a value indicating whether default or custom <see cref="ToolTip" /> text is displayed on the <see cref="ToolStripSplitButton" />.
		///</summary>
		/// <returns>true if default <see cref="ToolTip" /> text is displayed; otherwise, false. The default is true.</returns>
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
		/// Gets the size and location of the standard button portion of a <see cref="ToolStripSplitButton" />.
		///</summary>
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
		/// Gets a value indicating whether the button portion of the <see cref="ToolStripSplitButton" /> is in the pressed state. 
		///</summary>
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
		/// Gets a value indicating whether the standard button portion of a <see cref="ToolStripSplitButton" /> is selected or the <see cref="ToolStripSplitButton.DropDownButtonPressed" /> property is true.
		///</summary>
		/// <returns>true if the button portion of a <see cref="ToolStripSplitButton" /> is selected or whether <see cref="ToolStripSplitButton.DropDownButtonPressed" /> is true; otherwise, false.</returns>
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
		/// Gets a value indicating whether to display the <see cref="ToolTip" /> that is defined as the default. 
		///</summary>
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
		/// Gets or sets the portion of the <see cref="ToolStripSplitButton" /> that is activated when the control is first selected.
		///</summary>
		/// <returns>A Forms.ToolStripItem representing the portion of the <see cref="ToolStripSplitButton" /> that is activated when first selected. The default value is null.</returns>
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
		/// Gets the size and location, in screen coordinates, of the drop-down button portion of a <see cref="ToolStripSplitButton" />.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> that represents the size and location of the drop-down button portion of a <see cref="ToolStripSplitButton" />, in screen coordinates.</returns>
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
		/// Gets a value indicating whether the drop-down portion of the <see cref="ToolStripSplitButton" /> is in the pressed state. 
		///</summary>
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
		/// Gets a value indicating whether the drop-down button portion of a <see cref="ToolStripSplitButton" /> is selected.
		///</summary>
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
		/// The width, in pixels, of the drop-down button portion of a <see cref="ToolStripSplitButton" />.
		///</summary>
		/// <exception cref="System.ArgumentOutOfRangeException">The specified value is less than zero (0). </exception>
		/// <returns>An <see cref="System.Int32" /> representing the width in pixels.</returns>
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
		/// Gets the boundaries of the separator between the standard and drop-down button portions of a <see cref="ToolStripSplitButton" />.
		///</summary>
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
		/// This method is not relevant to this class.
		///</summary>
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
		/// If the <see cref="ToolStripItem.Enabled" /> property is true, calls the <see cref="ToolStripSplitButton.OnButtonClick(System.EventArgs)" /> method.
		///</summary>
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
