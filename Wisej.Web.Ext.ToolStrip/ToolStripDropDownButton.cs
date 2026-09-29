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
	/// </summary>
	/// <remarks>
	/// Unlike <see cref="ToolStripSplitButton" />, the whole button opens the drop-down. Handle <see cref="ToolStripDropDownItem.DropDownItemClicked" />
	/// or the <see cref="ToolStripItem.Click" /> event of the single drop-down items to respond to the selection.
	/// </remarks>
	/// <example>
	/// Creating a drop-down button with three options:
	/// <code><![CDATA[
	/// var export = new ToolStripDropDownButton("Export");
	/// export.DropDownItems.Add("PDF");
	/// export.DropDownItems.Add("Excel");
	/// export.DropDownItems.Add("CSV");
	/// export.DropDownItemClicked += (s, e) => ExportData(e.ClickedItem.Text);
	/// this.toolStrip1.Items.Add(export);
	/// ]]></code>
	/// </example>
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
		/// Initializes a new instance of the <see cref="ToolStripDropDownButton" /> class that has the specified name and displays the specified text and image.
		/// </summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripDropDownButton" />.</param>
		/// <param name="name">The name of the <see cref="ToolStripDropDownButton" />, see <see cref="ToolStripItem.Name" />.</param>
		public ToolStripDropDownButton(string text, Image image, string name)
		{
			this.Text = text;
			this.Image = image;
			this.Name = name;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripDropDownButton" /> class that displays the specified text and image and opens the specified items when clicked.
		/// </summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripDropDownButton" />.</param>
		/// <param name="dropDownItems">The items to add to <see cref="ToolStripDropDownItem.DropDownItems" />.</param>
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripItem.Text" /> of the <see cref="ToolStripDropDownButton" /> is used as its tooltip when no custom tooltip text is set.
		/// </summary>
		/// <returns>true to use the <see cref="ToolStripItem.Text" /> property for the tooltip; otherwise, false. The default is true.</returns>
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
		/// Returns the default value of the <see cref="ToolStripDropDownButton.AutoToolTip" /> property.
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
		/// Returns or sets a value indicating whether an arrow is displayed on the <see cref="ToolStripDropDownButton" />, which indicates that further options are available in a drop-down list.
		/// </summary>
		/// <returns>true to show an arrow on the <see cref="ToolStripDropDownButton" />; otherwise, false. The default is true.</returns>
		/// <remarks>
		/// Hiding the arrow doesn't change the behavior: clicking the button still opens the drop-down.
		/// </remarks>
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
