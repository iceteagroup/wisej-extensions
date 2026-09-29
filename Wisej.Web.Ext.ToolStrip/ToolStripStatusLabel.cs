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
	/// Represents a panel in a <see cref="ToolStrip" /> used as a status bar.
	/// </summary>
	/// <example>
	/// Building a status bar with a stretched message panel and a fixed panel:
	/// <code><![CDATA[
	/// var message = new ToolStripStatusLabel("Ready");
	/// message.Spring = true;
	/// message.TextAlign = ContentAlignment.MiddleLeft;
	///
	/// var user = new ToolStripStatusLabel(Application.Session.UserName);
	/// user.BorderSides = ToolStripStatusLabelBorderSides.Left;
	///
	/// this.statusToolStrip.Items.AddRange(new ToolStripItem[] { message, user });
	/// ]]></code>
	/// </example>
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
		/// </summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripStatusLabel" />.</param>
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
		/// </summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripStatusLabel" />.</param>
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
		/// Returns or sets a value that determines where the <see cref="ToolStripStatusLabel" /> is aligned on the status bar.
		/// </summary>
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
		/// Returns or sets the border style of the <see cref="ToolStripStatusLabel" />.
		/// </summary>
		/// <returns>One of the <see cref="BorderStyle" /> values. The default is <see cref="BorderStyle.Solid" />.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value of <see cref="ToolStripStatusLabel.BorderStyle" /> is not one of the <see cref="BorderStyle" /> values.</exception>
		/// <remarks>
		/// The border is drawn only on the sides specified by <see cref="ToolStripStatusLabel.BorderSides" />, which is <see cref="ToolStripStatusLabelBorderSides.None" /> by default.
		/// </remarks>
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
		/// Returns or sets a value that indicates which sides of the <see cref="ToolStripStatusLabel" /> show borders.
		/// </summary>
		/// <returns>A combination of the <see cref="ToolStripStatusLabelBorderSides" /> values. The default is <see cref="ToolStripStatusLabelBorderSides.None" />.</returns>
		/// <remarks>
		/// The values can be combined; the style of the border is determined by <see cref="ToolStripStatusLabel.BorderStyle" />.
		/// </remarks>
		/// <example>
		/// Separating a status panel from its neighbors with vertical lines:
		/// <code><![CDATA[
		/// this.toolStripStatusLabelUser.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
		/// this.toolStripStatusLabelUser.BorderStyle = BorderStyle.Solid;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripStatusLabel" /> automatically fills the available space on the status bar as the form is resized.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripStatusLabel" /> automatically fills the available space; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// When more than one label has <see cref="ToolStripStatusLabel.Spring" /> set to true, the available space is shared equally among them.
		/// </remarks>
		/// <example>
		/// Letting the message panel take all the remaining space:
		/// <code><![CDATA[
		/// this.toolStripStatusLabelMessage.Spring = true;
		/// this.toolStripStatusLabelMessage.TextAlign = ContentAlignment.MiddleLeft;
		/// ]]></code>
		/// </example>
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
