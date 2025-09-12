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
		/// Gets or sets a value indicating whether default or custom <see cref="ToolTip" /> text is displayed on the <see cref="ToolStripButton" />. 
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
		/// Gets a value indicating whether the <see cref="ToolStripButton" /> can be selected.
		///</summary>
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
		/// Gets or sets a value indicating whether the <see cref="ToolStripButton" /> should automatically appear pressed in and not pressed in when clicked.
		///</summary>
		/// <returns>true if the <see cref="ToolStripButton" /> should automatically appear pressed in and not pressed in when clicked; otherwise, false. The default is false.</returns>
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
		/// Gets or sets a value indicating whether the <see cref="ToolStripButton" /> is pressed or not pressed.
		///</summary>
		/// <returns>true if the <see cref="ToolStripButton" /> is pressed in or not pressed in; otherwise, false. The default is false.</returns>
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
		/// Gets or sets a value indicating whether the <see cref="ToolStripButton" /> is in the pressed or not pressed state by default, or is in an indeterminate state.
		///</summary>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="CheckState" /> values. </exception>
		/// <returns>One of the <see cref="CheckState" /> values. The default is Unchecked.</returns>
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
		/// Gets a value indicating whether to display the ToolTip that is defined as the default. 
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