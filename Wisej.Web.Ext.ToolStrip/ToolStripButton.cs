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
	/// </summary>
	/// <remarks>
	/// A <see cref="ToolStripButton" /> can also work as a toggle button: set <see cref="ToolStripButton.CheckOnClick" /> to true to
	/// switch the <see cref="ToolStripButton.Checked" /> state every time the button is clicked.
	/// </remarks>
	/// <example>
	/// Adding a "Bold" toggle button to a tool bar:
	/// <code><![CDATA[
	/// var bold = new ToolStripButton("Bold");
	/// bold.CheckOnClick = true;
	/// bold.CheckedChanged += (s, e) => this.textBox1.Font = new Font(this.textBox1.Font, bold.Checked ? FontStyle.Bold : FontStyle.Regular);
	/// this.toolStrip1.Items.Add(bold);
	/// ]]></code>
	/// </example>
	public class ToolStripButton : ToolStripItem
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripButton" /> class.
		///</summary>
		public ToolStripButton()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripButton" /> class that displays the specified text.
		///</summary>
		/// <param name="text">The text to display on the <see cref="ToolStripButton" />.</param>
		public ToolStripButton(string text)
		{
			this.Text = text;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripButton" /> class that displays the specified image.
		///</summary>
		/// <param name="image">The image to display on the <see cref="ToolStripButton" />.</param>
		public ToolStripButton(Image image)
		{
			this.Image = image;
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripButton" /> class that displays the specified text and image.
		///</summary>
		/// <param name="text">The text to display on the <see cref="ToolStripButton" />.</param>
		/// <param name="image">The image to display on the <see cref="ToolStripButton" />.</param>
		public ToolStripButton(string text, Image image)
		{
			this.Text = text;
			this.Image = image;
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripButton.Checked" /> property changes.
		///</summary>
		[SRDescription("CheckBoxOnCheckedChangedDescr")]
		public event EventHandler CheckedChanged;

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripButton.CheckState" /> property changes.
		///</summary>
		[SRDescription("CheckBoxOnCheckStateChangedDescr")]
		public event EventHandler CheckStateChanged;

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets a value indicating whether the <see cref="ToolStripItem.Text" /> of the <see cref="ToolStripButton" /> is used as its tooltip when no custom tooltip text is set.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripItem.Text" /> is used as the tooltip; otherwise, false. The default is true.</returns>
		/// <remarks>
		/// This is useful when <see cref="ToolStripItem.DisplayStyle" /> is <see cref="ToolStripItemDisplayStyle.Image" /> and the text is not visible.
		/// </remarks>
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
		/// Returns a value indicating whether the <see cref="ToolStripButton" /> can be selected.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripButton" /> can be selected; otherwise, false.</returns>
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripButton" /> should automatically toggle between the pressed and not pressed state when clicked.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripButton" /> toggles its <see cref="ToolStripButton.Checked" /> state when clicked; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// When true, each click toggles <see cref="ToolStripButton.Checked" /> and raises the <see cref="ToolStripButton.CheckedChanged" /> and
		/// <see cref="ToolStripButton.CheckStateChanged" /> events before the <see cref="ToolStripItem.Click" /> event.
		/// </remarks>
		/// <example>
		/// Using a button as a toggle to show or hide a panel:
		/// <code><![CDATA[
		/// this.toolStripButtonPreview.CheckOnClick = true;
		/// this.toolStripButtonPreview.CheckedChanged += (s, e) =>
		/// {
		///     this.panelPreview.Visible = this.toolStripButtonPreview.Checked;
		/// };
		/// ]]></code>
		/// </example>
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripButton" /> is pressed or not pressed.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripButton" /> is pressed; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// <see cref="ToolStripButton.Checked" /> and <see cref="ToolStripButton.CheckState" /> are synchronized: <see cref="ToolStripButton.Checked" />
		/// returns true when <see cref="ToolStripButton.CheckState" /> is <c>Checked</c> or <c>Indeterminate</c>.
		/// Changing the value raises the <see cref="ToolStripButton.CheckedChanged" /> event.
		/// </remarks>
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripButtonCheckedDescr")]
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
		/// Returns or sets a value indicating whether the <see cref="ToolStripButton" /> is in the pressed or not pressed state, or is in an indeterminate state.
		/// </summary>
		/// <returns>One of the <see cref="CheckState" /> values. The default is <see cref="CheckState.Unchecked" />.</returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="CheckState" /> values.</exception>
		/// <remarks>
		/// Setting <see cref="ToolStripButton.CheckState" /> also updates <see cref="ToolStripButton.Checked" />. Changing the value raises the
		/// <see cref="ToolStripButton.CheckStateChanged" /> event.
		/// </remarks>
		/// <example>
		/// Showing a mixed state when the selection contains both bold and regular text:
		/// <code><![CDATA[
		/// this.toolStripButtonBold.CheckState = selectionIsMixed
		///     ? CheckState.Indeterminate
		///     : (selectionIsBold ? CheckState.Checked : CheckState.Unchecked);
		/// ]]></code>
		/// </example>
		[SRCategory("CatAppearance")]
		[DefaultValue(CheckState.Unchecked)]
		[SRDescription("CheckBoxCheckStateDescr")]
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
		/// Returns the default value of the <see cref="ToolStripButton.AutoToolTip" /> property.
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

		#endregion

		#region Methods

		/// <summary>
		/// Raises the <see cref="Control.Click" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="ToolStripButton.CheckedChanged" /> event.
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
		/// Raises the <see cref="ToolStripButton.CheckStateChanged" /> event.
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