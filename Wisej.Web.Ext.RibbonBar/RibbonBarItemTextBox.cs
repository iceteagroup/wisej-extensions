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
using System.ComponentModel;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a textbox in a <see cref="RibbonBarGroup"/>.
	/// </summary>
	/// <remarks>
	/// The <see cref="RibbonBarItem.Text"/> and the image are displayed as a label to the left of the edit field; the content of
	/// the field is in the <see cref="Value"/> property. The user can move between the text boxes and combo boxes in the same
	/// <see cref="RibbonBarGroup"/> using the Tab and Shift+Tab keys.
	/// </remarks>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	[ApiCategory("RibbonBar")]
	public class RibbonBarItemTextBox : RibbonBarItem
	{

		#region Events

		/// <summary>
		/// Fired when the <see cref="Value" /> property has changed.
		/// </summary>
		[SRCategory("CatAction")]
		[Description("Fired when the Text property has changed.")]
		public event EventHandler ValueChanged
		{
			add { base.AddHandler(nameof(ValueChanged), value); }
			remove { base.RemoveHandler(nameof(ValueChanged), value); }
		}

		/// <summary>
		/// Fires the <see cref="ValueChanged" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		protected virtual void OnValueChanged(EventArgs e)
		{
			((EventHandler)base.Events[nameof(ValueChanged)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the value in the editable field of the <see cref="RibbonBarItemTextBox"/>.
		/// </summary>
		/// <returns>The text in the edit field. The default is null.</returns>
		/// <remarks>
		/// The value is updated on the server when the user commits the change in the field. Every change, including
		/// assigning the value in code, fires the <see cref="ValueChanged"/> event on the item and the
		/// <see cref="RibbonBar.ItemValueChanged"/> event on the <see cref="RibbonBar"/>.
		/// </remarks>
		/// <example>
		/// Searching the text typed in a ribbon text box:
		/// <code><![CDATA[
		/// private void ribbonBar1_ItemValueChanged(object sender, RibbonBarItemEventArgs e)
		/// {
		///     if (e.Item == this.textBoxSearch)
		///         Search(this.textBoxSearch.Value);
		/// }
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("")]
		public string Value
		{
			get { return this._value; }
			set
			{
				if (this._value != value)
				{
					this._value = value;

					OnValueChanged(EventArgs.Empty);
					this.RibbonBar?.OnItemValueChanged(new RibbonBarItemEventArgs(this));

					Update();
				}
			}
		}
		private string _value = null;

		/// <summary>
		/// Returns or sets the width of the TextBox field inside the <see cref="RibbonBarItemTextBox"/>.
		/// </summary>
		/// <returns>The width of the edit field in pixels, not including the label. The default is 120.</returns>
		[DefaultValue(120)]
		[SRCategory("CatLayout")]
		[Description("Returns or sets the width of the TextBox field inside the RibbonBarItemTextBox.")]
		public int FieldWidth
		{
			get { return this._fieldWidth; }
			set
			{
				if (this._fieldWidth != value)
				{
					this._fieldWidth = value;
					Update();
				}
			}
		}
		private int _fieldWidth = 120;

		#endregion

		#region Wisej Implementation

		// Handles "changeValue" events from the client.
		private void ProcessChangeValueWebEvent(WisejEventArgs e)
		{
			this.Value = e.Parameters.Value ?? string.Empty;
		}

		/// <summary>
		/// Updates the client component using the state information.
		/// </summary>
		/// <param name="state">Dynamic state object.</param>
		protected override void OnWebUpdate(dynamic state)
		{
			string value = state.value;
			if (value != null)
				this.Value = value;

			base.OnWebUpdate((object)state);
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

			config.className = "wisej.web.ribbonBar.ItemTextBox";
			config.fieldWidth = this.FieldWidth;
			config.value = this.Value;

			config.wiredEvents.Add("changeValue(Value)");
		}

		#endregion

	}
}