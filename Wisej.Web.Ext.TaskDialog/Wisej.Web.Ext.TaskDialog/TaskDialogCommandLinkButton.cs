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

namespace Wisej.Web.Ext.TaskDialog
{
	/// <summary>
	/// 
	///              Represents a command link button control of a task dialog.
	///            
	///</summary>
	public class TaskDialogCommandLinkButton : TaskDialogButton
	{

		#region Constructors
		/// <summary>
		/// 
		///              Initializes a new instance of the <see cref="TaskDialogButton" /> class.
		///            
		///</summary>
		public TaskDialogCommandLinkButton()
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
		/// <param name="descriptionText">An additional description text that will be displayed in
		///            a separate line when the <see cref="TaskDialogButton" />s of the task dialog are
		///            shown as command links (see <see cref="P:System.Windows.Forms.TaskDialogCommandLinkButton.DescriptionText" />).</param>
		/// <param name="enabled">A value that indicates if the button should be enabled.</param>
		/// <param name="allowCloseDialog">A value that indicates whether the task dialog should close
		///              when this button is clicked.
		///            </param>
		public TaskDialogCommandLinkButton(
			string? text,
			string? descriptionText = null,
			bool enabled = true,
			bool allowCloseDialog = true)
			: base(text, enabled, allowCloseDialog)
		{
			_descriptionText = descriptionText;
		}
		#endregion

		#region Properties
		/// <summary>
		/// 
		///              Gets or sets an additional description text that will be displayed in a separate
		///              line.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              The property is set and this button instance is currently bound to a task dialog.
		///            </exception>
		public string DescriptionText
		{
			get
			{
				return this._descriptionText;
			}
			set
			{
				if ((this._descriptionText != value))
				{
					this._descriptionText = value;
				}
			}
		}

		private string _descriptionText;
		#endregion
	}

}
