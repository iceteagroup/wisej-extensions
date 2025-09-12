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
	/// 
	///              Represents a button control of a task dialog.
	///            
	///</summary>
	public class TaskDialogButton : Wisej.Web.Button
	{

		#region Constructors
		/// <summary>
		/// 
		///              Initializes a new instance of the <see cref="TaskDialogButton" /> class.
		///            
		///</summary>
		public TaskDialogButton()
		{
			// TODO: Implement
		}

		/// <summary>
		/// 
		///              Initializes a new instance of the <see cref="TaskDialogButton" /> class
		///              using the given text and, optionally, a description text.
		///            
		///</summary>
		/// <param name="text">The text of the control.</param>
		/// <param name="enabled">A value that indicates if the button should be enabled.</param>
		/// <param name="allowCloseDialog">A value that indicates whether the task dialog should close
		///              when this button is clicked.
		///            </param>
		public TaskDialogButton(string text, bool enabled, bool allowCloseDialog)
		{
			this._text = text;
			this._enabled = enabled;
			this._allowCloseDialog = allowCloseDialog;
			// TODO: Implement
		}
		#endregion

		#region Events
		/// <summary>
		/// 
		///              Occurs when the button is clicked.
		///            
		///</summary>
		public event EventHandler Click;
		#endregion

		#region Properties
		/// <summary>
		/// 
		///             Gets a standard <see cref="TaskDialogButton" /> instance representing the <c>OK</c> button.
		///            
		///</summary>
		public TaskDialogButton OK
		{
			get
			{
				return _oK;
			}
		}

		private static TaskDialogButton _oK;

		/// <summary>
		/// 
		///             Gets a standard <see cref="TaskDialogButton" /> instance representing the <c>Cancel</c> button.
		///            
		///</summary>
		public TaskDialogButton Cancel
		{
			get
			{
				return _cancel;
			}
		}

		private static TaskDialogButton _cancel;

		/// <summary>
		/// 
		///             Gets a standard <see cref="TaskDialogButton" /> instance representing the <c>Abort</c> button.
		///            
		///</summary>
		public TaskDialogButton Abort
		{
			get
			{
				return _abort;
			}
		}

		private static TaskDialogButton _abort;

		/// <summary>
		/// 
		///             Gets a standard <see cref="TaskDialogButton" /> instance representing the <c>Retry</c> button.
		///            
		///</summary>
		public TaskDialogButton Retry
		{
			get
			{
				return _retry;
			}
		}

		private static TaskDialogButton _retry;

		/// <summary>
		/// 
		///             Gets a standard <see cref="TaskDialogButton" /> instance representing the <c>Ignore</c> button.
		///            
		///</summary>
		public TaskDialogButton Ignore
		{
			get
			{
				return _ignore;
			}
		}

		private static TaskDialogButton _ignore;

		/// <summary>
		/// 
		///             Gets a standard <see cref="TaskDialogButton" /> instance representing the <c>Yes</c> button.
		///            
		///</summary>
		public TaskDialogButton Yes
		{
			get
			{
				return _yes;
			}
		}

		private static TaskDialogButton _yes;

		/// <summary>
		/// 
		///             Gets a standard <see cref="TaskDialogButton" /> instance representing the <c>No</c> button.
		///            
		///</summary>
		public TaskDialogButton No
		{
			get
			{
				return _no;
			}
		}

		private static TaskDialogButton _no;

		/// <summary>
		/// 
		///             Gets a standard <see cref="TaskDialogButton" /> instance representing the <c>Close</c> button.
		///            
		///</summary>
		public TaskDialogButton Close
		{
			get
			{
				return _close;
			}
		}

		private static TaskDialogButton _close;

		/// <summary>
		/// 
		///             Gets a standard <see cref="TaskDialogButton" /> instance representing the <c>Help</c> button.
		///            
		///</summary>
		public TaskDialogButton Help
		{
			get
			{
				return _help;
			}
		}

		private static TaskDialogButton _help;

		/// <summary>
		/// 
		///             Gets a standard <see cref="TaskDialogButton" /> instance representing the <c>Try Again</c> button.
		///            
		///</summary>
		public TaskDialogButton TryAgain
		{
			get
			{
				return _tryAgain;
			}
		}

		private static TaskDialogButton _tryAgain;

		/// <summary>
		/// 
		///             Gets a standard <see cref="TaskDialogButton" /> instance representing the <c>Continue</c> button.
		///            
		///</summary>
		public TaskDialogButton Continue
		{
			get
			{
				return _continue;
			}
		}

		private static TaskDialogButton _continue;

		/// <summary>
		/// 
		///              Gets or sets a value that indicates whether the task dialog should close
		///              when this button is clicked. Or, if this button is the
		///              <see cref="P:System.Windows.Forms.TaskDialogButton.Help" /> button, indicates whether the
		///              <see cref="E:System.Windows.Forms.TaskDialogPage.HelpRequest" /> should be raised.
		///            
		///</summary>
		public bool AllowCloseDialog
		{
			get
			{
				return this._allowCloseDialog;
			}
			set
			{
				if ((this._allowCloseDialog != value))
				{
					this._allowCloseDialog = value;
				}
			}
		}

		private bool _allowCloseDialog;

		/// <summary>
		/// 
		///              Gets or sets a value indicating whether the button can respond to user interaction.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set on a button that is currently bound to a task dialog, but the dialog
		///              has just started navigating to a different page.
		///            </exception>
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
		/// 
		///              Gets or sets a value that indicates if the User Account Control (UAC) shield icon
		///              should be shown near the button; that is, whether the action invoked by the button
		///              requires elevation.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set on a button that is currently bound to a task dialog, but the dialog
		///              has just started navigating to a different page.
		///            </exception>
		public bool ShowShieldIcon
		{
			get
			{
				return this._showShieldIcon;
			}
			set
			{
				if ((this._showShieldIcon != value))
				{
					this._showShieldIcon = value;
				}
			}
		}

		private bool _showShieldIcon;

		/// <summary>
		/// 
		///              Gets or sets a value that indicates if this
		///              <see cref="TaskDialogButton" /> should be shown when displaying
		///              the task dialog.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this button instance is currently bound to a task dialog.
		///            </exception>
		public bool Visible
		{
			get
			{
				return this._visible;
			}
			set
			{
				if ((this._visible != value))
				{
					this._visible = value;
				}
			}
		}

		private bool _visible;

		/// <summary>
		/// 
		///              Gets or sets the text associated with this control.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this button instance is a standard button, for which the text is provided by the OS.
		///              - or -
		///              The property is set and this button instance is currently bound to a task dialog.
		///            </exception>
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

		internal TaskDialogButtonCollection? Collection { get; set; }
		#endregion

		#region Methods
		/// <summary>
		/// 
		///              Simulates a click on this button.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              This button instance is not currently bound to a task dialog.
		///              - or -
		///              The task dialog has started navigating to a new page containing this button instance, but the <see cref="E:System.Windows.Forms.TaskDialogPage.Created" /> event has not been raised yet.
		///              - or -
		///              This button is currently bound to a task dialog, but the dialog has just started navigating to a different page.
		///            </exception>
		public void PerformClick()
		{
			// TODO: Implement
		}

		public override bool Equals(Object obj)
		{
			// TODO: Implement
			return false;
		}

		public override int GetHashCode()
		{
			// TODO: Implement
			return 0;
		}

		protected virtual void OnClick(System.EventArgs e)
		{
			if ((this.Click != null))
			{
				Click(this, e);
			}
		}

		/// <summary>
		/// 
		///              Returns a string that represents the current <see cref="TaskDialogButton" /> control.
		///            
		///</summary>
		/// <returns>A string that contains the control text.</returns>
		public override String ToString()
		{
			// TODO: Implement
			return "";
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
