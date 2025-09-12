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
	///              Represents the footnote area of a task dialog.
	///            
	///</summary>
	public class TaskDialogFootnote : TaskDialogControl
	{

		#region Constructors
		/// <summary>
		/// 
		///              Initializes a new instance of the <see cref="TaskDialogFootnote" /> class.
		///            
		///</summary>
		public TaskDialogFootnote()
		{
			// TODO: Implement
		}

		/// <summary>
		/// 
		///              Initializes a new instance of the <see cref="TaskDialogFootnote" /> class
		///              using the given <paramref name="text" />.
		///            
		///</summary>
		/// <param name="text">The text to be displayed in the dialog's footnote area.</param>
		public TaskDialogFootnote(string text)
		{
			this._text = text;
			// TODO: Implement
		}
		#endregion

		#region Properties
		/// <summary>
		/// 
		///              Gets or sets the text to be displayed in the dialog's footnote area.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set on a footnote instance that is currently bound to a task dialog, but it's not visible as its initial
		///              <see cref="P:System.Windows.Forms.TaskDialogFootnote.Text" /> property value was <see langword="null" /> or an empty string.
		///              - or -
		///              The property is set on a footnote instance that is currently bound to a task dialog, but the dialog
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
		///              Gets or sets the footnote icon.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set on a footnote instance that is currently bound to a task dialog, but it's not visible as its initial
		///              <see cref="P:System.Windows.Forms.TaskDialogFootnote.Text" /> property value was <see langword="null" /> or an empty string.
		///              - or -
		///              The property is set and the task dialog has started navigating to a new page containing this footnote instance, but the
		///              <see cref="E:System.Windows.Forms.TaskDialogPage.Created" /> event has not been raised yet.
		///              - or -
		///              The property is set on a footnote instance that is currently bound to a task dialog, but the dialog
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
		#endregion

		#region Methods
		/// <summary>
		/// 
		///              Returns a string that represents the current <see cref="TaskDialogFootnote" /> control.
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
