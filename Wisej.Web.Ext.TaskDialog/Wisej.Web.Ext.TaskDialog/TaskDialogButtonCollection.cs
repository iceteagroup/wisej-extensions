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
using System.Collections;
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
	///              Represents a collection of <see cref="TaskDialogButton" /> objects.
	///            
	///</summary>
	public class TaskDialogButtonCollection : Collection<TaskDialogButton>
	{
		// HashSet to detect duplicate items.
		private readonly HashSet<TaskDialogButton> _itemSet = new HashSet<TaskDialogButton>();

		#region Constructors
		/// <summary>
		/// 
		///              Initializes a new instance of the <see cref="TaskDialogButtonCollection" /> class.
		///            
		///</summary>
		public TaskDialogButtonCollection()
		{
			// TODO: Implement
		}
		#endregion

		#region Properties

		internal TaskDialogPage? BoundPage { get; set; }

		#endregion

		#region Methods
		/// <summary>
		/// 
		///              Creates and adds a <see cref="TaskDialogButton" /> to the collection.
		///            
		///</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///              This collection is currently bound to a task dialog.
		///            </exception>
		/// <returns>The created <see cref="TaskDialogButton" />.</returns>
		/// <param name="text">The text of the custom button.</param>
		/// <param name="enabled">A value indicating whether the button can respond to user interaction.</param>
		/// <param name="allowCloseDialog">A value that indicates whether the task dialog should close
		///              when this button is clicked.
		///            </param>
		public TaskDialogButton Add(string text, bool enabled, bool allowCloseDialog)
		{
			var button = new TaskDialogButton(text, enabled, allowCloseDialog);
			Add(button);

			return button;
		}

		protected override void SetItem(int index, TaskDialogButton item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}

			// Disallow collection modification, so that we don't need to copy it
			// when binding the TaskDialogPage.
			BoundPage?.DenyIfBound();
			DenyIfHasOtherCollection(item);

			TaskDialogButton oldItem = this[index];
			if (oldItem != item)
			{
				// First, add the new item (which will throw if it is a duplicate entry),
				// then remove the old one.
				if (!_itemSet.Add(item))
				{
					throw new ArgumentException(SR.TaskDialogControlAlreadyAddedToCollection);
				}

				_itemSet.Remove(oldItem);

				oldItem.Collection = null;
				item.Collection = this;
			}

			base.SetItem(index, item);
		}

		protected override void InsertItem(int index, TaskDialogButton item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}

			// Disallow collection modification, so that we don't need to copy it
			// when binding the TaskDialogPage.
			BoundPage?.DenyIfBound();
			DenyIfHasOtherCollection(item);

			if (!_itemSet.Add(item))
			{
				throw new ArgumentException(SR.TaskDialogControlAlreadyAddedToCollection);
			}

			item.Collection = this;
			base.InsertItem(index, item);
		}

		protected override void RemoveItem(int index)
		{
			BoundPage?.DenyIfBound();

			TaskDialogButton oldItem = this[index];
			oldItem.Collection = null;
			_itemSet.Remove(oldItem);
			base.RemoveItem(index);
		}

		protected override void ClearItems()
		{
			BoundPage?.DenyIfBound();

			foreach (TaskDialogButton button in this)
			{
				button.Collection = null;
			}

			_itemSet.Clear();
			base.ClearItems();
		}

		private void DenyIfHasOtherCollection(TaskDialogButton item)
		{
			if (item.Collection != null && item.Collection != this)
				throw new InvalidOperationException(SR.TaskDialogControlIsPartOfOtherCollection);
		}

		#endregion
	}

}
