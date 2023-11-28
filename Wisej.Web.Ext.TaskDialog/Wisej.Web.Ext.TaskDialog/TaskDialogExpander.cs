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
	///              Represents an expander button and the associated expanded area
	///              of a task dialog.
	///            
	///</summary>
	public class TaskDialogExpander : TaskDialogControl
	{

		#region Constructors

		/// <summary>
		/// 
		///              Initializes a new instance of the <see cref="TaskDialogExpander" /> class.
		///            
		///</summary>
		public TaskDialogExpander()
		{
			// TODO: Implement
		}

		/// <summary>
		/// 
		///              Initializes a new instance of the <see cref="TaskDialogExpander" /> class
		///              using the given text.
		///            
		///</summary>
		/// <param name="text">The text to be displayed in the dialog's expanded area.</param>
		public TaskDialogExpander(string text) : this()
		{
			this._text = text;
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// 
		///              Occurs when the value of the <see cref="P:System.Windows.Forms.TaskDialogExpander.Expanded" /> property changes while
		///              this control is shown in a task dialog.
		///            
		///</summary>
		public event EventHandler ExpandedChanged;

		#endregion

		#region Properties

		/// <summary>
		/// 
		///              Gets or sets the text to be displayed in the dialog's expanded area.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set on an expander instance that is currently bound to a task dialog, but it's not visible as its initial
		///              <see cref="P:System.Windows.Forms.TaskDialogExpander.Text" /> property value was <see langword="null" /> or an empty string.
		///              - or -
		///              The property is set on an expander instance that is currently bound to a task dialog, but the dialog
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
		///              Gets or sets the text to be displayed in the expander button when it
		///              is in the expanded state.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this expander instance is currently bound to a task dialog.
		///            </exception>
		public string ExpandedButtonText
		{
			get
			{
				return this._expandedButtonText;
			}
			set
			{
				if ((this._expandedButtonText != value))
				{
					this._expandedButtonText = value;
				}
			}
		}

		private string _expandedButtonText;

		/// <summary>
		/// 
		///              Gets or sets the text to be displayed in the expander button when it
		///              is in the collapsed state.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this expander instance is currently bound to a task dialog.
		///            </exception>
		public string CollapsedButtonText
		{
			get
			{
				return this._collapsedButtonText;
			}
			set
			{
				if ((this._collapsedButtonText != value))
				{
					this._collapsedButtonText = value;
				}
			}
		}

		private string _collapsedButtonText;

		/// <summary>
		/// 
		///              Gets or sets a value that indicates whether the expander button is in the
		///              expanded state (so that the dialog's expanded area is visible).
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this expander instance is currently bound to a task dialog.
		///            </exception>
		public bool Expanded
		{
			get
			{
				return this._expanded;
			}
			set
			{
				if ((this._expanded != value))
				{
					this._expanded = value;
				}
			}
		}

		private bool _expanded;

		/// <summary>
		/// 
		///              Gets or sets the <see cref="TaskDialogExpanderPosition" /> that specifies where
		///              the expanded area of the task dialog is to be displayed.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this expander instance is currently bound to a task dialog.
		///            </exception>
		public TaskDialogExpanderPosition Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if ((this._position != value))
				{
					this._position = value;
				}
			}
		}

		private TaskDialogExpanderPosition _position;

		#endregion

		#region Methods

		/// <summary>
		/// 
		///              Returns a string that represents the current <see cref="TaskDialogExpander" /> control.
		///            
		///</summary>
		/// <returns>A string that contains the control text.</returns>
		public override String ToString()
		{
			// TODO: Implement
			return "";
		}


		protected virtual void OnExpandedChanged(System.EventArgs e)
		{
			if ((this.ExpandedChanged != null))
			{
				ExpandedChanged(this, e);
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
