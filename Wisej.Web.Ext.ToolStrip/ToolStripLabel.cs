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
	/// Represents a nonselectable <see cref="ToolStripItem" /> that renders text and images and can display hyperlinks.
	///</summary>
	public class ToolStripLabel : ToolStripItem
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripLabel" /> class.
		///</summary>
		public ToolStripLabel()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripLabel" /> class, specifying the text to display.
		///</summary>
		/// <param name="text">The text to display on the <see cref="ToolStripLabel" />.</param>
		public ToolStripLabel(string text)
		{
			this.Text = text;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripLabel" /> class, specifying the image to display.
		///</summary>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to display on the <see cref="ToolStripLabel" />.</param>
		public ToolStripLabel(Image image)
		{
			this.Image = image;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripLabel" /> class, specifying the text and image to display.
		///</summary>
		/// <param name="text">The text to display on the <see cref="ToolStripLabel" />.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to display on the <see cref="ToolStripLabel" />.</param>
		public ToolStripLabel(string text, Image image)
		{
			this.Text = text;
			this.Image = image;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripLabel" /> class, specifying the text and image to display and whether the <see cref="ToolStripLabel" /> acts as a link.
		///</summary>
		/// <param name="text">The text to display on the <see cref="ToolStripLabel" />.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to display on the <see cref="ToolStripLabel" />.</param>
		/// <param name="isLink">true if the <see cref="ToolStripLabel" /> acts as a link; otherwise, false. </param>
		public ToolStripLabel(string text, Image image, bool isLink)
		{
			this.Text = text;
			this.Image = image;
			this._isLink = isLink;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets a value indicating the selectable state of a <see cref="ToolStripLabel" />.
		///</summary>
		/// <returns>false in all cases.</returns>
		[Browsable(false)]
		public override bool CanSelect
		{
			get
			{
				return this._canSelect;
			}
		}

		private bool _canSelect;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="ToolStripLabel" /> is a hyperlink. 
		///</summary>
		/// <returns>true if the <see cref="ToolStripLabel" /> is a hyperlink; otherwise, false. The default is false.</returns>
		[SRDescription("ToolStripLabelIsLinkDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		public bool IsLink
		{
			get
			{
				return this._isLink;
			}
			set
			{
				if ((this._isLink != value))
				{
					this._isLink = value;
				}
			}
		}

		private bool _isLink;

		/// <summary>
		/// Gets or sets the color used to display an active link.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Color" /> that represents the color to display an active link. The default color is specified by the system. Typically, this color is Color.Red.</returns>
		[SRDescription("ToolStripLabelActiveLinkColorDescr")]
		[SRCategory("CatAppearance")]
		public Color ActiveLinkColor
		{
			get
			{
				return this._activeLinkColor;
			}
			set
			{
				if ((this._activeLinkColor != value))
				{
					this._activeLinkColor = value;
				}
			}
		}

		private Color _activeLinkColor;

		/// <summary>
		/// Gets or sets a value that represents the behavior of a link.
		///</summary>
		/// <returns>One of the <see cref="LinkBehavior" /> values. The default is LinkBehavior.SystemDefault.</returns>
		[DefaultValue(LinkBehavior.SystemDefault)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripLabelLinkBehaviorDescr")]
		public LinkBehavior LinkBehavior
		{
			get
			{
				return this._linkBehavior;
			}
			set
			{
				if ((this._linkBehavior != value))
				{
					this._linkBehavior = value;
				}
			}
		}

		private LinkBehavior _linkBehavior;

		/// <summary>
		/// Gets or sets the color used when displaying a normal link.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Color" /> that represents the color used to displaying a normal link. The default color is specified by the system. Typically, this color is Color.Blue.</returns>
		[SRDescription("ToolStripLabelLinkColorDescr")]
		[SRCategory("CatAppearance")]
		public Color LinkColor
		{
			get
			{
				return this._linkColor;
			}
			set
			{
				if ((this._linkColor != value))
				{
					this._linkColor = value;
				}
			}
		}

		private Color _linkColor;

		/// <summary>
		/// Gets or sets a value indicating whether a link should be displayed as though it were visited.
		///</summary>
		/// <returns>true if links should display as though they were visited; otherwise, false. The default is false.</returns>
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("ToolStripLabelLinkVisitedDescr")]
		public bool LinkVisited
		{
			get
			{
				return this._linkVisited;
			}
			set
			{
				if ((this._linkVisited != value))
				{
					this._linkVisited = value;
				}
			}
		}

		private bool _linkVisited;

		/// <summary>
		/// Gets or sets the color used when displaying a link that that has been previously visited.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Color" /> that represents the color used to display links that have been visited. The default color is specified by the system. Typically, this color is Color.Purple.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLabelVisitedLinkColorDescr")]
		public Color VisitedLinkColor
		{
			get
			{
				return this._visitedLinkColor;
			}
			set
			{
				if ((this._visitedLinkColor != value))
				{
					this._visitedLinkColor = value;
				}
			}
		}

		private Color _visitedLinkColor;

		#endregion

		#region Methods

		/// <summary>
		/// Raises the <see cref="Control.FontChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.MouseEnter" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.MouseLeave" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			// TODO: Implement
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			// TODO: Implement
			return new AccessibleObject();
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
