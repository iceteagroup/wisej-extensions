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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wisej.Core;

namespace Wisej.Web.Ext.TaskDialog
{
	/// <summary>
	/// Represents a radio button control of a task dialog.
	/// </summary>
	/// <remarks>
	/// Add instances to the <see cref="TaskDialogPage.RadioButtons"/> collection of a page. Only one
	/// radio button of a page can be checked at a time.
	/// </remarks>
	public class TaskDialogRadioButton : TaskDialogControl
	{

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TaskDialogRadioButton" /> class.
		/// </summary>
		public TaskDialogRadioButton()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskDialogRadioButton" /> class
		/// using the given <paramref name="text" />.
		/// </summary>
		/// <param name="text">The text associated with the radio button.</param>
		public TaskDialogRadioButton(string text)
		{
			this._text = text;
			// TODO: Implement
		}
		#endregion

		#region Events
		/// <summary>
		/// 
		///              Occurs when the value of the <see cref="P:System.Windows.Forms.TaskDialogRadioButton.Checked" /> property changes
		///              while this control is shown in a task dialog.
		///            
		///</summary>
		public event EventHandler CheckedChanged;
		#endregion

		#region Properties
		/// <summary>
		/// Returns or sets a value indicating whether the button can respond to user interaction.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// The property is set on a radio button that is currently bound to a task dialog, but the dialog
		/// has just started navigating to a different page.
		/// </exception>
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if ((this._enabled != value))
				{
					this._enabled = value;
				}
			}
		}

		private bool _enabled;

		/// <summary>
		/// Returns or sets the text associated with this control.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// The property is set and this radio button instance is currently bound to a task dialog.
		/// </exception>
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if ((this._text != value))
				{
					this._text = value;
				}
			}
		}

		private string _text;

		/// <summary>
		/// Returns or sets a value indicating whether the <see cref="TaskDialogRadioButton" /> is
		/// in the checked state.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// The property is set and the task dialog has started navigating to a new page containing this radio button instance,
		/// but the <see cref="TaskDialogPage.Created" /> event has not been raised yet.
		/// - or -
		/// The property is set on a radio button instance that is currently bound to a task dialog,
		/// but the value to be set is <see langword="false" />.
		/// - or -
		/// The property is set within the <see cref="CheckedChanged" /> event of one of the radio buttons of the currently bound task dialog.
		/// - or -
		/// The property is set on a radio button instance that is currently bound to a task dialog, but the dialog
		/// has just started navigating to a different page.
		/// </exception>
		/// <remarks>
		/// While the page is shown in a task dialog, a radio button can only be checked; it is unchecked
		/// automatically when another radio button of the page is checked.
		/// </remarks>
		/// <example>
		/// Pre-selecting an option and reacting when the user changes it:
		/// <code><![CDATA[
		/// var daily = page.RadioButtons.Add("Daily");
		/// var weekly = page.RadioButtons.Add("Weekly");
		/// weekly.Checked = true;
		/// daily.CheckedChanged += (s, e) =>
		/// {
		///     if (daily.Checked)
		///         this.schedule = "daily";
		/// };
		/// ]]></code>
		/// </example>
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
		#endregion

		#region Methods
		/// <summary>
		/// Returns a string that represents the current <see cref="TaskDialogRadioButton" /> control.
		/// </summary>
		/// <returns>A string that contains the control text.</returns>
		/// <remarks>
		/// This method is not implemented yet in this version and returns an empty string.
		/// </remarks>
		/// <example>
		/// Getting the text of the checked radio button:
		/// <code><![CDATA[
		/// var selected = page.RadioButtons.FirstOrDefault(r => r.Checked)?.ToString();
		/// ]]></code>
		/// </example>
		public override String ToString()
		{
			// TODO: Implement
			return "";
		}
		
		protected virtual void OnCheckedChanged(System.EventArgs e)
		{
			if ((this.CheckedChanged != null))
			{
				CheckedChanged(this, e);
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
