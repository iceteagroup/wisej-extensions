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
using Wisej.Web.Layout;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Provides basic functionality for the <see cref="ContextMenuStrip" /> control. Although <see cref="ToolStripDropDownMenu" /> and <see cref="ToolStripDropDown" /> replace and add functionality to the <see cref="Menu" /> control of previous versions, <see cref="Menu" /> is retained for both backward compatibility and future use if you choose.
	///</summary>
	public class ToolStripDropDownMenu : ToolStripDropDown
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDownMenu" /> class. 
		///</summary>
		public ToolStripDropDownMenu()
		{
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the internal spacing, in pixels, of the control.
		///</summary>
		/// <returns>A Padding object representing the spacing.</returns>
		public new Padding DefaultPadding
		{
			get
			{
				return this._defaultPadding;
			}
		}

		private Padding _defaultPadding;

		/// <summary>
		/// Gets the rectangle that represents the display area of the <see cref="ToolStripDropDownMenu" />.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Rectangle" /> that represents the display area.</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlDisplayRectangleDescr")]
		public override Rectangle DisplayRectangle
		{
			get
			{
				return this._displayRectangle;
			}
		}

		private Rectangle _displayRectangle;

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return this._layoutEngine;
			}
		}

		private LayoutEngine _layoutEngine;

		/// <summary>
		/// Gets or sets a value indicating how the items of <see cref="ContextMenuStrip" /> are displayed.
		///</summary>
		/// <returns>One of the <see cref="ToolStripLayoutStyle" /> values. The default is <see cref="ToolStripLayoutStyle.Flow" />.</returns>
		[DefaultValue(ToolStripLayoutStyle.Flow)]
		public ToolStripLayoutStyle LayoutStyle
		{
			get
			{
				return this._layoutStyle;
			}
			set
			{
				if ((this._layoutStyle != value))
				{
					this._layoutStyle = value;
				}
			}
		}

		private ToolStripLayoutStyle _layoutStyle;

		/// <summary>
		/// Gets or sets a value indicating whether space for an image is shown on the left edge of the <see cref="ToolStripMenuItem" />.
		///</summary>
		/// <returns>true if the image margin is shown; otherwise, false. The default is true.</returns>
		[SRDescription("ToolStripDropDownMenuShowImageMarginDescr")]
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		public bool ShowImageMargin
		{
			get
			{
				return this._showImageMargin;
			}
			set
			{
				if ((this._showImageMargin != value))
				{
					this._showImageMargin = value;
				}
			}
		}

		private bool _showImageMargin;

		/// <summary>
		/// Gets or sets a value indicating whether space for a check mark is shown on the left edge of the <see cref="ToolStripMenuItem" />. 
		///</summary>
		/// <returns>true if the check margin is shown; otherwise, false. The default is false.</returns>
		[DefaultValue(false)]
		[SRDescription("ToolStripDropDownMenuShowCheckMarginDescr")]
		[SRCategory("CatAppearance")]
		public bool ShowCheckMargin
		{
			get
			{
				return this._showCheckMargin;
			}
			set
			{
				if ((this._showCheckMargin != value))
				{
					this._showCheckMargin = value;
				}
			}
		}

		private bool _showCheckMargin;

		#endregion

		#region Methods

		/// <summary>
		/// Raises the <see cref="Control.Layout" /> event.
		///</summary>
		/// <param name="e">A <see cref="LayoutEventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="ToolStripDropDown.FontChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Resets the collection of displayed and overflow items after a layout is done.
		///</summary>
		protected override void SetDisplayedItems()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Paints the background of the control.
		///</summary>
		/// <param name="e">A <see cref="PaintEventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
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
