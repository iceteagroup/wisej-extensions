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
	/// Represents a control that when clicked displays an associated <see cref="ToolStripDropDown" /> from which the user can select a single item.
	///</summary>
	public class ToolStripDropDownButton : ToolStripDropDownItem
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDownButton" /> class. 
		///</summary>
		public ToolStripDropDownButton()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDownButton" /> class that displays the specified text.
		///</summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripDropDownButton" />.</param>
		public ToolStripDropDownButton(string text)
		{
			this.Text = text;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDownButton" /> class that displays the specified image.
		///</summary>
		/// <param name="image">An <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripDropDownButton" />.</param>
		public ToolStripDropDownButton(Image image)
		{
			this.Image = image;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDownButton" /> class that displays the specified text and image.
		///</summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripDropDownButton" />.</param>
		public ToolStripDropDownButton(string text, Image image)
		{
			this.Text = text;
			this.Image = image;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDownButton" /> class that has the specified name, displays the specified text and image, and raises the Click event.
		///</summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripDropDownButton" />.</param>
		/// <param name="onClick">The event handler for the <see cref="Control.Click" /> event.</param>
		/// <param name="name">The name of the <see cref="ToolStripDropDownButton" />.</param>
		public ToolStripDropDownButton(string text, Image image, string name)
		{
			this.Text = text;
			this.Image = image;
			this.Name = name;
			// TODO: Implement
		}

		public ToolStripDropDownButton(string text, Image image, ToolStripItem[] dropDownItems)
		{
			this.Text = text;
			this.Image = image;
			//this.DropDownItems = dropDownItems;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets a value indicating whether to use the Text property or the <see cref="ToolStripItem.ToolTipText" /> property for the <see cref="ToolStripDropDownButton" /> ToolTip.
		///</summary>
		/// <returns>true to use the <see cref="Control.Text" /> property for the ToolTip; otherwise, false. The default is true.</returns>
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
		/// Gets or sets a value indicating whether an arrow is displayed on the <see cref="ToolStripDropDownButton" />, which indicates that further options are available in a drop-down list.
		///</summary>
		/// <returns>true to show an arrow on the <see cref="ToolStripDropDownButton" />; otherwise, false. The default is true.</returns>
		[DefaultValue(true)]
		[SRDescription("ToolStripDropDownButtonShowDropDownArrowDescr")]
		[SRCategory("CatAppearance")]
		public bool ShowDropDownArrow
		{
			get
			{
				return this._showDropDownArrow;
			}
			set
			{
				if ((this._showDropDownArrow != value))
				{
					this._showDropDownArrow = value;
				}
			}
		}

		private bool _showDropDownArrow;

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
