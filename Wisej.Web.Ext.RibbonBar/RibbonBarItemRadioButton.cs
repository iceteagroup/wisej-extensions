///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using Wisej.Base;
using Wisej.Core;
using System.ComponentModel;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a radio button in a <see cref="RibbonBarGroup"/>.
	/// </summary>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	public class RibbonBarItemRadioButton : RibbonBarItem
	{

		#region Events

		/// <summary>
		/// Fired when the value of the <see cref="Checked" /> property changes.
		/// </summary>
		[SRCategory("CatAction")]
		[Description("Fired when the value of the Checked property changes.")]
		public event EventHandler CheckedChanged
		{
			add { base.AddHandler(nameof(CheckedChanged), value); }
			remove { base.RemoveHandler(nameof(CheckedChanged), value); }
		}

		/// <summary>
		/// Fires the <see cref="CheckedChanged" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			((EventHandler)base.Events[nameof(CheckedChanged)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or whether the <see cref="RibbonBarItemRadioButton" /> is in the checked state.
		/// </summary>
		/// <returns>true if the <see cref="RibbonBarItemRadioButton" /> is in the checked state; otherwise, false.</returns>
		[DefaultValue(false)]
		[Category("CatAppearance")]
		[Description("Returns or whether the RibbonBarItemRadioButton is in the checked state.")]
		public bool Checked
		{
			get
			{
				return this._checked;
			}
			set
			{
				if (value != this._checked)
				{
					this._checked = value;
					Update();
					OnCheckedChanged(EventArgs.Empty);
				}
			}
		}
		private bool _checked = false;

		#endregion

		#region Wisej Implementation

		// Handles "changeValue" events from the client. The check box changes
		// its value when it's checked or unchecked.
		//
		// This is handled differently from the regular CheckBox control 
		// which is managed by the server and can decline to check or uncheck.
		private void ProcessChangeValueWebEvent(WisejEventArgs e)
		{
			// determine if the button can be clicked.
			if (this.Enabled && this.Visible)
			{
				this.Checked = e.Parameters.Checked ?? false;

				this.RibbonBar?.OnItemClick(new RibbonBarItemEventArgs(this));
			}
		}

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "changeValue":
					ProcessChangeValueWebEvent(e);
					break;

				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ribbonBar.ItemRadioButton";
			config.value = this.Checked;
			config.wiredEvents.Add("changeValue(Checked)");
		}

		#endregion

	}
}