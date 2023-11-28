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
	///              Represents a page of content of a task dialog.
	///            
	///</summary>
	public class TaskDialogPage : Wisej.Web.Panel
	{

		#region Constructors
		/// <summary>
		/// 
		///              Initializes a new instance of the <see cref="TaskDialogPage" /> class.
		///            
		///</summary>
		public TaskDialogPage()
		{
			// TODO: Implement
		}
		#endregion

		#region Events
		/// <summary>
		/// 
		///              Occurs after this instance is bound to a task dialog and the task dialog
		///              has created the GUI elements represented by this <see cref="TaskDialogPage" /> instance.
		///            
		///</summary>
		public event EventHandler Created;

		/// <summary>
		/// 
		///              Occurs when the task dialog is about to destroy the GUI elements represented
		///              by this <see cref="TaskDialogPage" /> instance and it is about to be
		///              unbound from the task dialog.
		///            
		///</summary>
		public event EventHandler Destroyed;

		/// <summary>
		/// 
		///              Occurs when the user presses F1 while the task dialog has focus, or when the
		///              user clicks the <see cref="P:System.Windows.Forms.TaskDialogButton.Help" /> button.
		///            
		///</summary>
		public event EventHandler HelpRequest;
		#endregion

		#region Properties

		/// <summary>
		/// 
		///              Gets or sets the collection of push buttons
		///              to be shown in this page.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this page instance is currently bound to a task dialog.
		///            </exception>
		public TaskDialogButtonCollection Buttons
		{
			get
			{
				return this._buttons;
			}
			set
			{
				if ((this._buttons != value))
				{
					this._buttons = value;
				}
			}
		}

		private TaskDialogButtonCollection _buttons;

		/// <summary>
		/// 
		///              Gets or sets the default button in the task dialog.
		///            
		///</summary>
		public TaskDialogButton DefaultButton
		{
			get
			{
				return this._defaultButton;
			}
			set
			{
				if ((this._defaultButton != value))
				{
					this._defaultButton = value;
				}
			}
		}

		private TaskDialogButton _defaultButton;

		/// <summary>
		/// 
		///              Gets or sets the collection of radio buttons
		///              to be shown in this page.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this page instance is currently bound to a task dialog.
		///            </exception>
		public TaskDialogRadioButtonCollection RadioButtons
		{
			get
			{
				return this._radioButtons;
			}
			set
			{
				if ((this._radioButtons != value))
				{
					this._radioButtons = value;
				}
			}
		}

		private TaskDialogRadioButtonCollection _radioButtons;

		/// <summary>
		/// 
		///              Gets or sets the verification checkbox to be shown in this page.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this page instance is currently bound to a task dialog.
		///            </exception>
		public TaskDialogVerificationCheckBox Verification
		{
			get
			{
				return this._verification;
			}
			set
			{
				if ((this._verification != value))
				{
					this._verification = value;
				}
			}
		}

		private TaskDialogVerificationCheckBox _verification;

		/// <summary>
		/// 
		///              Gets or sets the dialog expander to be shown in this page.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this page instance is currently bound to a task dialog.
		///            </exception>
		public TaskDialogExpander Expander
		{
			get
			{
				return this._expander;
			}
			set
			{
				if ((this._expander != value))
				{
					this._expander = value;
				}
			}
		}

		private TaskDialogExpander _expander;

		/// <summary>
		/// 
		///              Gets or sets the footnote to be shown in this page.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this page instance is currently bound to a task dialog.
		///            </exception>
		public TaskDialogFootnote Footnote
		{
			get
			{
				return this._footnote;
			}
			set
			{
				if ((this._footnote != value))
				{
					this._footnote = value;
				}
			}
		}

		private TaskDialogFootnote _footnote;

		/// <summary>
		/// 
		///              Gets or sets the progress bar to be shown in this page.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this page instance is currently bound to a task dialog.
		///            </exception>
		public TaskDialogProgressBar ProgressBar
		{
			get
			{
				return this._progressBar;
			}
			set
			{
				if ((this._progressBar != value))
				{
					this._progressBar = value;
				}
			}
		}

		private TaskDialogProgressBar _progressBar;

		/// <summary>
		/// 
		///              Gets or sets the text to display in the title bar of the task dialog.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and the task dialog has started navigating to this page instance,
		///              but the <see cref="E:System.Windows.Forms.TaskDialogPage.Created" /> event has not been raised yet.
		///              - or -
		///              The property is set on a page instance that is currently bound to a task dialog, but the dialog
		///              has just started navigating to a different page.
		///            </exception>
		public string Caption
		{
			get
			{
				return this._caption;
			}
			set
			{
				if ((this._caption != value))
				{
					this._caption = value;
				}
			}
		}

		private string _caption;

		/// <summary>
		/// 
		///              Gets or sets the heading (main instruction).
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set on a page instance that is currently bound to a task dialog, but the dialog
		///              has just started navigating to a different page.
		///            </exception>
		public string Heading
		{
			get
			{
				return this._heading;
			}
			set
			{
				if ((this._heading != value))
				{
					this._heading = value;
				}
			}
		}

		private string _heading;

		/// <summary>
		/// 
		///              Gets or sets the dialog's primary text content.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set on a page instance that is currently bound to a task dialog, but the dialog
		///              has just started navigating to a different page.
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

		/// <summary>
		/// 
		///              Gets or sets the main icon.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and the task dialog has started navigating to this page instance,
		///              but the <see cref="E:System.Windows.Forms.TaskDialogPage.Created" /> event has not been raised yet.
		///              - or -
		///              The property is set on a page instance that is currently bound to a task dialog, but the dialog
		///              has just started navigating to a different page.
		///            </exception>
		public TaskDialogIcon Icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				if ((this._icon != value))
				{
					this._icon = value;
				}
			}
		}

		private TaskDialogIcon _icon;

		/// <summary>
		/// 
		///              Gets or sets a value that indicates whether the task dialog can be closed with
		///              <see cref="P:System.Windows.Forms.TaskDialogButton.Cancel" /> as resulting button by pressing ESC or Alt+F4
		///              or by clicking the title bar's close button, even if a <see cref="P:System.Windows.Forms.TaskDialogButton.Cancel" />
		///              button isn't added to the <see cref="P:System.Windows.Forms.TaskDialogPage.Buttons" /> collection.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this page instance is currently bound to a task dialog.
		///            </exception>
		public bool AllowCancel
		{
			get
			{
				return this._allowCancel;
			}
			set
			{
				if ((this._allowCancel != value))
				{
					this._allowCancel = value;
				}
			}
		}

		private bool _allowCancel;

		/// <summary>
		/// 
		///              Gets or sets a value that indicates whether text and controls are displayed
		///              reading right to left.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this page instance is currently bound to a task dialog.
		///            </exception>
		public bool RightToLeftLayout
		{
			get
			{
				return this._rightToLeftLayout;
			}
			set
			{
				if ((this._rightToLeftLayout != value))
				{
					this._rightToLeftLayout = value;
				}
			}
		}

		private bool _rightToLeftLayout;

		/// <summary>
		/// 
		///              Gets or sets a value that indicates whether the task dialog can be minimized
		///              when it is shown modeless.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this page instance is currently bound to a task dialog.
		///            </exception>
		public bool AllowMinimize
		{
			get
			{
				return this._allowMinimize;
			}
			set
			{
				if ((this._allowMinimize != value))
				{
					this._allowMinimize = value;
				}
			}
		}

		private bool _allowMinimize;

		/// <summary>
		/// 
		///              Indicates that the width of the task dialog is determined by the width
		///              of its content area (similar to Message Box sizing behavior).
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this page instance is currently bound to a task dialog.
		///            </exception>
		public bool SizeToContent
		{
			get
			{
				return this._sizeToContent;
			}
			set
			{
				if ((this._sizeToContent != value))
				{
					this._sizeToContent = value;
				}
			}
		}

		private bool _sizeToContent;

		/// <summary>
		/// 
		///              Gets the <see cref="TaskDialog" /> instance which this page
		///              is currently bound to.
		///            
		///</summary>
		public TaskDialog BoundDialog
		{
			get
			{
				return this._boundDialog;
			}
			set
			{
				if ((this._boundDialog != value))
				{
					this._boundDialog = value;
				}
			}
		}

		private TaskDialog _boundDialog;

		#endregion

		#region Methods

		/// <summary>
		/// 
		///             Shows the new content in the current task dialog.
		///            <paramref name="page" />.
		///            
		///</summary>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="page" /> is <see langword="null" />.
		///            </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///              The page instance is not currently bound to a dialog, <see cref="P:System.Windows.Forms.TaskDialogPage.BoundDialog" /> is <see langword="null" />.
		///              - or -
		///              This page instance contains an invalid configuration.
		///              - or -
		///              This method is called from within the <see cref="E:System.Windows.Forms.TaskDialogRadioButton.CheckedChanged" /> event
		///              of one of the radio buttons of the current task dialog.
		///              - or -
		///              The task dialog has already been closed.
		///            </exception>
		/// <param name="page">
		///              The page instance that contains the contents which this task dialog will display.
		///            </param>
		public void Navigate(TaskDialogPage page)
		{
			// TODO: Implement
		}

		internal void DenyIfBound()
		{
			if (BoundDialog != null)
			{
				throw new InvalidOperationException(SR.TaskDialogCannotSetPropertyOfBoundPage);
			}
		}

		/// <summary>
		/// 
		///              Raises the <see cref="E:System.Windows.Forms.TaskDialogPage.Created" /> event.
		///            
		///</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected virtual void OnCreated(System.EventArgs e)
		{
			if ((this.Created != null))
			{
				Created(this, e);
			}
		}

		/// <summary>
		/// 
		///              Raises the <see cref="E:System.Windows.Forms.TaskDialogPage.Destroyed" /> event.
		///            
		///</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected virtual void OnDestroyed(System.EventArgs e)
		{
			if ((this.Destroyed != null))
			{
				Destroyed(this, e);
			}
		}

		/// <summary>
		/// 
		///              Raises the <see cref="E:System.Windows.Forms.TaskDialogPage.HelpRequest" /> event.
		///            
		///</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected virtual void OnHelpRequest(System.EventArgs e)
		{
			if ((this.HelpRequest != null))
			{
				HelpRequest(this, e);
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
