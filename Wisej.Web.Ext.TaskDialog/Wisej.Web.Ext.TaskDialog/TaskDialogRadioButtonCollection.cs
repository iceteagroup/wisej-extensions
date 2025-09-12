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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.TaskDialog
{
	//TODO: fix missing properties
	/// <summary>
	/// 
	///              Represents a collection of <see cref="TaskDialogRadioButton" /> objects.
	///            
	///</summary>
	public class TaskDialogRadioButtonCollection : Collection<TaskDialogRadioButton>
	{

		#region Constructors
		/// <summary>
		/// 
		///              Initializes a new instance of the <see cref="TaskDialogRadioButtonCollection" /> class.
		///            
		///</summary>
		public TaskDialogRadioButtonCollection()
		{
			// TODO: Implement
		}
		#endregion

		#region Methods
		/// <summary>
		/// 
		///              Creates and adds a <see cref="TaskDialogRadioButton" /> to the collection.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              This collection is currently bound to a task dialog.
		///            </exception>
		/// <returns>The created <see cref="TaskDialogRadioButton" />.</returns>
		/// <param name="text">The text of the radio button.</param>
		public TaskDialogRadioButton Add(string text)
		{
			// TODO: Implement
			return new System.Windows.Forms.TaskDialogRadioButton();
		}

		protected override void SetItem(int index, TaskDialogRadioButton item)
		{
			// TODO: Implement
		}

		protected override void InsertItem(int index, TaskDialogRadioButton item)
		{
			// TODO: Implement
		}

		protected override void RemoveItem(int index)
		{
			// TODO: Implement
		}

		protected override void ClearItems()
		{
			// TODO: Implement
		}
		#endregion
	}

}
