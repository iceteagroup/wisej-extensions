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
	/// Provides basic functionality for controls that display a <see cref="ToolStripDropDown" /> when a <see cref="ToolStripDropDownButton" />, <see cref="ToolStripMenuItem" />, or <see cref="ToolStripSplitButton" /> control is clicked.
	///</summary>
	public class ToolStripDropDownItem : ToolStripItem
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDownItem" /> class. 
		///</summary>
		public ToolStripDropDownItem()
		{
			// TODO: Implement
		}

		public ToolStripDropDownItem(string text, Image image, ToolStripItem[] dropDownItems)
		{
			this.Text = text;
			this.Image = image;
			this.DropDownItems.AddRange(dropDownItems);
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the <see cref="ToolStripDropDown" /> closes. 
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownClosedDecr")]
		public event EventHandler DropDownClosed;

		/// <summary>
		/// Occurs as the <see cref="ToolStripDropDown" /> is opening.
		///</summary>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownOpeningDescr")]
		public event EventHandler DropDownOpening;

		/// <summary>
		/// Occurs when the <see cref="ToolStripDropDown" /> has opened.
		///</summary>
		[SRDescription("ToolStripDropDownOpenedDescr")]
		[SRCategory("CatAction")]
		public event EventHandler DropDownOpened;

		/// <summary>
		/// Occurs when the <see cref="ToolStripDropDown" /> is clicked.
		///</summary>
		[SRCategory("CatAction")]
		public event ToolStripItemClickedEventHandler DropDownItemClicked;

		#endregion

		/// <summary>
		/// Raises the <see cref="ToolStripDropDownItem.DropDownClosed" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownClosedDecr")]
		protected virtual void OnDropDownClosed(System.EventArgs e)
		{
			if ((this.DropDownClosed != null))
			{
				DropDownClosed(this, e);
			}
		}

		[SRCategory("CatAction")]
		[SRDescription("ToolStripDropDownOpeningDescr")]
		protected virtual void OnDropDownOpening(System.EventArgs e)
		{
			if ((this.DropDownOpening != null))
			{
				DropDownOpening(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripDropDownItem.DropDownOpened" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRDescription("ToolStripDropDownOpenedDescr")]
		[SRCategory("CatAction")]
		protected virtual void OnDropDownOpened(System.EventArgs e)
		{
			if ((this.DropDownOpened != null))
			{
				DropDownOpened(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripDropDownItem.DropDownItemClicked" /> event.
		///</summary>
		/// <param name="e">A <see cref="ToolStripItemClickedEventArgs" /> that contains the event data.</param>
		[SRCategory("CatAction")]
		protected virtual void OnDropDownItemClicked(ToolStripItemClickedEventArgs e)
		{
			if ((this.DropDownItemClicked != null))
			{
				DropDownItemClicked(this, e);
			}
		}

		#region Properties

		/// <summary>
		/// Gets or sets the <see cref="ToolStripDropDown" /> that will be displayed when this <see cref="ToolStripDropDownItem" /> is clicked.
		///</summary>
		/// <returns>A <see cref="ToolStripDropDown" /> that is associated with the <see cref="ToolStripDropDownItem" />.</returns>
		[SRDescription("ToolStripDropDownDescr")]
		[SRCategory("CatData")]
		public ToolStripDropDown DropDown
		{
			get
			{
				return this._dropDown;
			}
			set
			{
				if ((this._dropDown != value))
				{
					this._dropDown = value;
				}
			}
		}

		private ToolStripDropDown _dropDown;

		/// <summary>
		/// Gets or sets a value indicating the direction in which the <see cref="ToolStripDropDownItem" /> emerges from its parent container.
		///</summary>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The property is set to a value that is not one of the <see cref="ToolStripDropDownDirection" /> values.</exception>
		/// <returns>One of the <see cref="ToolStripDropDownDirection" /> values.</returns>
		[Browsable(false)]
		[SRDescription("ToolStripDropDownItemDropDownDirectionDescr")]
		[SRCategory("CatBehavior")]
		public ToolStripDropDownDirection DropDownDirection
		{
			get
			{
				return this._dropDownDirection;
			}
			set
			{
				if ((this._dropDownDirection != value))
				{
					this._dropDownDirection = value;
				}
			}
		}

		private ToolStripDropDownDirection _dropDownDirection;

		/// <summary>
		/// Gets the collection of items in the <see cref="ToolStripDropDown" /> that is associated with this <see cref="ToolStripDropDownItem" />.
		///</summary>
		/// <returns>A <see cref="ToolStripItemCollection" /> of controls.</returns>
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("ToolStripDropDownItemsDescr")]
		public ToolStripItemCollection DropDownItems
		{
			get
			{
				return this._dropDownItems;
			}
		}

		private ToolStripItemCollection _dropDownItems;

		/// <summary>
		/// Gets a value indicating whether the <see cref="ToolStripDropDownItem" /> has <see cref="ToolStripDropDown" /> controls associated with it. 
		///</summary>
		/// <returns>true if the <see cref="ToolStripDropDownItem" /> has <see cref="ToolStripDropDown" /> controls; otherwise, false.</returns>
		[Browsable(false)]
		public virtual bool HasDropDownItems
		{
			get
			{
				return this._hasDropDownItems;
			}
		}

		private bool _hasDropDownItems;

		[Browsable(false)]
		public bool HasDropDown
		{
			get
			{
				return this._hasDropDown;
			}
		}

		private bool _hasDropDown;

		/// <summary>
		/// Gets a value indicating whether the <see cref="ToolStripDropDownItem" /> is in the pressed state.
		///</summary>
		/// <returns>true if the <see cref="ToolStripDropDownItem" /> is in the pressed state; otherwise, false. </returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Pressed
		{
			get
			{
				return this._pressed;
			}
		}

		private bool _pressed;

		#endregion

		#region Methods

		/// <summary>
		/// Creates a generic <see cref="ToolStripDropDown" /> for which events can be defined.
		///</summary>
		/// <returns>A <see cref="ToolStripDropDown" />.</returns>
		protected virtual ToolStripDropDown CreateDefaultDropDown()
		{
			// TODO: Implement
			return new ToolStripDropDown();
		}

		/// <summary>
		/// Raises the <see cref="ToolStripDropDown.FontChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			// TODO: Implement
		}

		//ITG:TODO: Review
		//protected override void OnBoundsChanged()
		//{
		//	base.OnBoundsChanged();
		//	// TODO: Implement
		//}

		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raised in response to the <see cref="ToolStripDropDownItem.HideDropDown" /> method.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		protected virtual void OnDropDownHide(EventArgs e)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Raised in response to the <see cref="ToolStripDropDownItem.ShowDropDown" /> method.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		protected virtual void OnDropDownShow(EventArgs e)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Makes a visible <see cref="ToolStripDropDown" /> hidden.
		///</summary>
		public void HideDropDown()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Displays the <see cref="ToolStripDropDownItem" /> control associated with this <see cref="ToolStripDropDownItem" />.
		///</summary>
		/// <exception cref="System.InvalidOperationException">The <see cref="ToolStripDropDownItem" /> is the same as the parent <see cref="ToolStrip" />.</exception>
		public void ShowDropDown()
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
