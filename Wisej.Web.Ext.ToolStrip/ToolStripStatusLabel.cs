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
	/// Represents a panel in a <see cref="StatusStrip" /> control. 
	///</summary>
	public class ToolStripStatusLabel : ToolStripLabel
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripStatusLabel" /> class. 
		///</summary>
		public ToolStripStatusLabel()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripStatusLabel" /> class that displays the specified text.
		///</summary>
		/// <param name="text">A <see cref="System.String" /> representing the text to be displayed on the <see cref="ToolStripStatusLabel" />.</param>
		public ToolStripStatusLabel(string text)
		{
			this.Text = text;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripStatusLabel" /> class that displays the specified image. 
		///</summary>
		/// <param name="image">An <see cref="System.Drawing.Image" /> that is displayed on the <see cref="ToolStripStatusLabel" />.</param>
		public ToolStripStatusLabel(Image image)
		{
			this.Image = image;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripStatusLabel" /> class that displays the specified image and text.
		///</summary>
		/// <param name="text">A <see cref="System.String" /> representing the text to be displayed on the <see cref="ToolStripStatusLabel" />.</param>
		/// <param name="image">An <see cref="System.Drawing.Image" /> that is displayed on the <see cref="ToolStripStatusLabel" />.</param>
		public ToolStripStatusLabel(string text, Image image)
		{
			this.Text = text;
			this.Image = image;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets a value that determines where the <see cref="ToolStripStatusLabel" /> is aligned on the <see cref="StatusStrip" />.
		///</summary>
		/// <returns>One of the <see cref="ToolStripItemAlignment" /> values.</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public ToolStripItemAlignment Alignment
		{
			get
			{
				return this._alignment;
			}
			set
			{
				if ((this._alignment != value))
				{
					this._alignment = value;
				}
			}
		}

		private ToolStripItemAlignment _alignment;

		/// <summary>
		/// Gets or sets the border style of the <see cref="ToolStripStatusLabel" />.
		///</summary>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value of <see cref="ToolStripStatusLabel.BorderStyle" /> is not one of the <see cref="BorderStyle" /> values.</exception>
		/// <returns>One of the <see cref="BorderStyle" /> values. The default is <see cref="BorderStyle.Flat" />.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripStatusLabelBorderStyleDescr")]
		[DefaultValue(BorderStyle.Solid)]
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
		/// Gets or sets a value that indicates which sides of the <see cref="ToolStripStatusLabel" /> show borders.
		///</summary>
		/// <returns>One of the <see cref="ToolStripStatusLabelBorderSides" /> values. The default is <see cref="ToolStripStatusLabelBorderSides.None" />.</returns>
		[DefaultValue(ToolStripStatusLabelBorderSides.None)]
		[SRDescription("ToolStripStatusLabelBorderSidesDescr")]
		[SRCategory("CatAppearance")]
		public ToolStripStatusLabelBorderSides BorderSides
		{
			get
			{
				return this._borderSides;
			}
			set
			{
				if ((this._borderSides != value))
				{
					this._borderSides = value;
				}
			}
		}

		private ToolStripStatusLabelBorderSides _borderSides;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="ToolStripStatusLabel" /> automatically fills the available space on the <see cref="StatusStrip" /> as the form is resized. 
		///</summary>
		/// <returns>true if the <see cref="ToolStripStatusLabel" /> automatically fills the available space on the <see cref="StatusStrip" /> as the form is resized; otherwise, false. The default is false.</returns>
		[SRDescription("ToolStripStatusLabelSpringDescr")]
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		public bool Spring
		{
			get
			{
				return this._spring;
			}
			set
			{
				if ((this._spring != value))
				{
					this._spring = value;
				}
			}
		}

		private bool _spring;

		#endregion

		#region Methods

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
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
