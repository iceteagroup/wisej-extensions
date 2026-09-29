///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.ComponentModel;
using System.Linq;
using Wisej.Base;

namespace Wisej.Web.Ext.NavigationBar
{
	/// <summary>
	/// Contains a collection of <see cref="NavigationBarItem" /> objects.
	/// </summary>
	[ListBindable(false)]
	[ApiCategory("NavigationBar")]
	public class NavigationBarItemCollection : IList, IList<NavigationBarItem>, IEnumerable<NavigationBarItem>
	{

		private Control owner;
		private Control.ControlCollection controls;

		internal NavigationBarItemCollection(Control owner)
		{
			if (owner == null)
				throw new ArgumentNullException("owner");

			this.owner = owner;
			this.controls = owner.Controls;
		}

		/// <summary>
		/// Returns the number of items in the collection.
		/// </summary>
		/// <returns>The number of items in the collection.</returns>
		public int Count
		{
			get { return this.controls.Count; }
		}

		/// <summary>
		/// Returns or sets a <see cref="NavigationBarItem" /> in the collection at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the highest available index. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <returns>The <see cref="NavigationBarItem" /> at the specified index.</returns>
		/// <remarks>
		/// Setting an item replaces the existing item at <paramref name="index"/>, which is removed from the collection but not disposed.
		/// </remarks>
		/// <example>
		/// Replacing the first item:
		/// <code><![CDATA[
		/// this.navigationBar1.Items[0] = new NavigationBarItem { Name = "home", Text = "Home", Icon = "Images/home.svg" };
		/// ]]></code>
		/// </example>
		public NavigationBarItem this[int index]
		{
			get { return (NavigationBarItem)this.controls[index]; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				if (index < 0 || index > this.Count - 1)
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", "index", index));

				this.controls.Add(value);
				this.controls.RemoveAt(index);
				this.controls.SetChildIndex(value, index);
			}
		}

		/// <summary>
		/// Returns the <see cref="NavigationBarItem" /> with the specified key from the collection.
		/// </summary>
		/// <returns>The <see cref="NavigationBarItem" /> with the specified key.</returns>
		/// <param name="name">The name of the item to retrieve.</param>
		/// <remarks>
		/// Returns null when an item with the specified <paramref name="name"/> is not found. The search is not recursive: only the items in this collection are searched.
		/// </remarks>
		/// <example>
		/// Retrieving an item by name:
		/// <code><![CDATA[
		/// var reports = this.navigationBar1.Items["reports"];
		/// if (reports != null)
		///     reports.Expanded = true;
		/// ]]></code>
		/// </example>
		public NavigationBarItem this[string name]
		{
			get
			{
				int index = IndexOfKey(name);
				return index < 0 ? null : this[index];
			}
		}

		/// <summary>
		/// Returns the index of the <see cref="NavigationBarItem" /> in the collection.
		/// </summary>
		/// <returns>The zero-based index of the item; -1 if it cannot be found.</returns>
		/// <param name="item">The <see cref="NavigationBarItem" /> to locate in the collection. </param>
		/// <exception cref="ArgumentNullException">The value of <paramref name="item" /> is null. </exception>
		/// <example>
		/// Selecting the item that follows the current one:
		/// <code><![CDATA[
		/// var items = this.navigationBar1.Items;
		/// var index = items.IndexOf(this.navigationBar1.SelectedItem);
		/// if (index > -1 && index < items.Count - 1)
		///     this.navigationBar1.SelectedItem = items[index + 1];
		/// ]]></code>
		/// </example>
		public int IndexOf(NavigationBarItem item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			return this.controls.IndexOf(item);
		}

		/// <summary>
		/// Returns the index of the first occurrence of the <see cref="NavigationBarItem" /> with the specified key.
		/// </summary>
		/// <returns>The zero-based index of the first occurrence of a <see cref="NavigationBarItem" /> with the specified key, if found; otherwise, -1.</returns>
		/// <param name="key">The name of the <see cref="NavigationBarItem" /> to find in the collection.</param>
		/// <remarks>
		/// Returns -1 when <paramref name="key"/> is null or empty.
		/// </remarks>
		/// <example>
		/// Inserting an item after the item named "reports":
		/// <code><![CDATA[
		/// var index = this.navigationBar1.Items.IndexOfKey("reports");
		/// this.navigationBar1.Items.Insert(index + 1, "analytics", "Analytics");
		/// ]]></code>
		/// </example>
		public int IndexOfKey(string key)
		{
			if (String.IsNullOrEmpty(key))
				return -1;

			return this.controls.IndexOfKey(key);
		}

		/// <summary>
		/// Adds a <see cref="NavigationBarItem" /> to the collection.
		/// </summary>
		/// <param name="item">The <see cref="NavigationBarItem" /> to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="item" /> is null. </exception>
		/// <example>
		/// Adding an item created in code:
		/// <code><![CDATA[
		/// var item = new NavigationBarItem
		/// {
		///     Name = "inbox",
		///     Text = "Inbox",
		///     Icon = "Images/inbox.svg",
		///     InfoText = "5"
		/// };
		/// this.navigationBar1.Items.Add(item);
		/// ]]></code>
		/// </example>
		public void Add(NavigationBarItem item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			this.controls.Add(item);
			item.UpdateIndentation();
		}

		/// <summary>
		/// Creates a <see cref="NavigationBarItem" /> with the specified text, and adds it to the collection.
		/// </summary>
		/// <param name="text">The text to display on the <see cref="NavigationBarItem" />.</param>
		/// <example>
		/// Adding an item with only a title:
		/// <code><![CDATA[
		/// this.navigationBar1.Items.Add("Help");
		/// ]]></code>
		/// </example>
		public void Add(string text)
		{
			Add(new NavigationBarItem()
			{
				Text = text
			});
		}

		/// <summary>
		/// Creates a <see cref="NavigationBarItem" /> with the specified key and text and adds it to the collection.
		/// </summary>
		/// <param name="key">The name of the <see cref="NavigationBarItem" />.</param>
		/// <param name="text">The text to display on the <see cref="NavigationBarItem" />.</param>
		/// <example>
		/// Adding an item that can be retrieved by name:
		/// <code><![CDATA[
		/// this.navigationBar1.Items.Add("help", "Help");
		/// this.navigationBar1.Items["help"].Icon = "Images/help.svg";
		/// ]]></code>
		/// </example>
		public void Add(string key, string text)
		{
			var item = new NavigationBarItem()
			{
				Name = key,
				Text = text
			};
			Add(item);
		}

		/// <summary>
		/// Creates a <see cref="NavigationBarItem" /> with the specified key, text, and image, and adds it to the collection.
		/// </summary>
		/// <param name="key">The name of the <see cref="NavigationBarItem" />.</param>
		/// <param name="text">The text to display on the <see cref="NavigationBarItem" />.</param>
		/// <param name="icon">The Url or name of the icon to display on the <see cref="NavigationBarItem" />.</param>
		/// <example>
		/// Adding an item with an icon:
		/// <code><![CDATA[
		/// this.navigationBar1.Items.Add("settings", "Settings", "Images/settings.svg");
		/// ]]></code>
		/// </example>
		public void Add(string key, string text, string icon)
		{
			var item = new NavigationBarItem()
			{
				Name = key,
				Text = text,
				Icon = icon
			};
			Add(item);
		}

		/// <summary>
		/// Adds a set <see cref="NavigationBarItem" /> objects to the collection.
		/// </summary>
		/// <param name="items">An array of type <see cref="NavigationBarItem" /> that contains the <see cref="NavigationBarItem" /> to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of items is null. </exception>
		/// <example>
		/// Adding several items at once:
		/// <code><![CDATA[
		/// this.navigationBar1.Items.AddRange(new[]
		/// {
		///     new NavigationBarItem { Name = "dashboard", Text = "Dashboard", Icon = "Images/dashboard.svg" },
		///     new NavigationBarItem { Name = "orders", Text = "Orders", Icon = "Images/orders.svg" },
		///     new NavigationBarItem { Name = "customers", Text = "Customers", Icon = "Images/customers.svg" }
		/// });
		/// ]]></code>
		/// </example>
		public void AddRange(NavigationBarItem[] items)
		{
			if (items == null)
				throw new ArgumentNullException("items");

			foreach (var item in items)
			{
				Add(item);
			}
		}

		/// <summary>
		/// Removes all the <see cref="NavigationBarItem" /> instances from the collection.
		/// </summary>
		/// <remarks>
		/// The removed items are not disposed. Use <see cref="Clear(bool)"/> to dispose them.
		/// </remarks>
		/// <example>
		/// Removing all the items before rebuilding the menu:
		/// <code><![CDATA[
		/// this.navigationBar1.Items.Clear();
		/// this.navigationBar1.Items.Add("home", "Home", "Images/home.svg");
		/// ]]></code>
		/// </example>
		public void Clear()
		{
			Clear(false);
		}

		/// <summary>
		/// Removes all the <see cref="NavigationBarItem" /> instances from the collection and optionally disposes them.
		/// </summary>
		/// <param name="dispose">Indicates whether to dispose the <see cref="NavigationBarItem" /> instances removed from the collection.</param>
		/// <example>
		/// Removing and disposing all the items:
		/// <code><![CDATA[
		/// this.navigationBar1.Items.Clear(true);
		/// ]]></code>
		/// </example>
		public void Clear(bool dispose)
		{
			if (this.owner is NavigationBar)
				((NavigationBar)this.owner).SelectedItem = null;

			this.controls.Clear(dispose);
		}

		/// <summary>
		/// Determines whether a specified <see cref="NavigationBarItem" /> is in the collection.
		/// </summary>
		/// <returns>true if the specified <see cref="NavigationBarItem" /> is in the collection; otherwise, false.</returns>
		/// <param name="item">The <see cref="NavigationBarItem" /> to locate in the collection. </param>
		/// <exception cref="ArgumentNullException">The value of <paramref name="item" /> is null. </exception>
		/// <example>
		/// Checking whether an item is a top-level item:
		/// <code><![CDATA[
		/// if (this.navigationBar1.Items.Contains(this.navigationBar1.SelectedItem))
		///     AlertBox.Show("Top-level item selected.");
		/// ]]></code>
		/// </example>
		public bool Contains(NavigationBarItem item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			return this.controls.Contains(item);
		}

		/// <summary>
		/// Determines whether the collection contains a <see cref="NavigationBarItem" /> with the specified key.
		/// </summary>
		/// <returns>true to indicate a <see cref="NavigationBarItem" /> with the specified key was found in the collection; otherwise, false. </returns>
		/// <param name="key">The name of the <see cref="NavigationBarItem" /> to search for.</param>
		/// <example>
		/// Adding an item only once:
		/// <code><![CDATA[
		/// if (!this.navigationBar1.Items.ContainsKey("admin"))
		///     this.navigationBar1.Items.Add("admin", "Administration", "Images/admin.svg");
		/// ]]></code>
		/// </example>
		public virtual bool ContainsKey(string key)
		{
			return this.controls.ContainsKey(key);
		}

		/// <summary>
		/// Copies the <see cref="NavigationBarItem" /> instances in the collection to the specified array, starting at the specified index.
		/// </summary>
		/// <param name="array">The one-dimensional array that is the destination of the items copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <exception cref="ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="ArgumentException">
		///   <paramref name="array" /> is multidimensional or the number of elements in the <see cref="NavigationBarItemCollection" /> is greater than the available space from index to the end of <paramref name="array" />.</exception>
		/// <example>
		/// Copying the items to an array:
		/// <code><![CDATA[
		/// var items = new NavigationBarItem[this.navigationBar1.Items.Count];
		/// this.navigationBar1.Items.CopyTo(items, 0);
		/// ]]></code>
		/// </example>
		public void CopyTo(NavigationBarItem[] array, int index)
		{
			if (array == null)
				throw new ArgumentNullException("array");

			this.controls.CopyTo(array, index);
		}

		/// <summary>
		/// Removes the <see cref="NavigationBarItem" /> from the collection.
		/// </summary>
		/// <param name="item">The <see cref="NavigationBarItem" /> to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter is null. </exception>
		/// <remarks>
		/// If the removed item is the <see cref="NavigationBar.SelectedItem"/>, the selection is cleared. The item is not disposed.
		/// </remarks>
		/// <example>
		/// Removing the selected top-level item:
		/// <code><![CDATA[
		/// var item = this.navigationBar1.SelectedItem;
		/// if (item != null && item.Parent == null)
		///     this.navigationBar1.Items.Remove(item);
		/// ]]></code>
		/// </example>
		public void Remove(NavigationBarItem item)
		{
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			if (item.NavigationBar != null && item.NavigationBar.SelectedItem == item)
				item.NavigationBar.SelectedItem = null;

			this.controls.Remove(item);
		}

		/// <summary>
		/// Removes the <see cref="NavigationBarItem" /> at the specified index from the collection.
		/// </summary>
		/// <param name="index">The zero-based index of the <see cref="NavigationBarItem" /> to remove. </param>
		/// <remarks>
		/// If the removed item is the <see cref="NavigationBar.SelectedItem"/>, the selection is cleared. The item is not disposed.
		/// </remarks>
		/// <example>
		/// Removing the last item:
		/// <code><![CDATA[
		/// var items = this.navigationBar1.Items;
		/// if (items.Count > 0)
		///     items.RemoveAt(items.Count - 1);
		/// ]]></code>
		/// </example>
		public void RemoveAt(int index)
		{
			Remove((NavigationBarItem)this.controls[index]);
		}

		/// <summary>
		/// Removes the <see cref="NavigationBarItem" /> with the specified key from the collection.
		/// </summary>
		/// <param name="key">The name of the <see cref="NavigationBarItem" /> to remove.</param>
		/// <remarks>
		/// Unlike <see cref="Remove"/>, this method doesn't clear <see cref="NavigationBar.SelectedItem"/> when the removed item is selected.
		/// </remarks>
		/// <example>
		/// Removing an item by name:
		/// <code><![CDATA[
		/// this.navigationBar1.Items.RemoveByKey("admin");
		/// ]]></code>
		/// </example>
		public void RemoveByKey(string key)
		{
			this.controls.RemoveByKey(key);
		}

		/// <summary>
		/// Inserts an existing <see cref="NavigationBarItem" /> into the collection at the specified index. 
		/// </summary>
		/// <param name="index">The zero-based index location where the <see cref="NavigationBarItem" /> is inserted.</param>
		/// <param name="item">The <see cref="NavigationBarItem" /> to insert in the collection.</param>
		/// <example>
		/// Inserting an item at the top:
		/// <code><![CDATA[
		/// this.navigationBar1.Items.Insert(0, new NavigationBarItem { Name = "home", Text = "Home", Icon = "Images/home.svg" });
		/// ]]></code>
		/// </example>
		public void Insert(int index, NavigationBarItem item)
		{
			Add(item);
			this.controls.SetChildIndex(item, index);
		}

		/// <summary>
		/// Creates a new <see cref="NavigationBarItem" /> with the specified text and inserts it into the collection at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index location where the <see cref="NavigationBarItem" /> is inserted.</param>
		/// <param name="text">The text to display in the <see cref="NavigationBarItem" />.</param>
		/// <example>
		/// Inserting an item with only a title at the top:
		/// <code><![CDATA[
		/// this.navigationBar1.Items.Insert(0, "Home");
		/// ]]></code>
		/// </example>
		public void Insert(int index, string text)
		{
			Insert(index, new NavigationBarItem()
			{
				Text = text
			});
		}

		/// <summary>
		/// Creates a new <see cref="NavigationBarItem" /> with the specified key and text, and inserts it into the collection at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index location where the <see cref="NavigationBarItem" /> is inserted.</param>
		/// <param name="key">The name of the <see cref="NavigationBarItem" />.</param>
		/// <param name="text">The text to display on the <see cref="NavigationBarItem" />.</param>
		/// <example>
		/// Inserting a named item in the second position:
		/// <code><![CDATA[
		/// this.navigationBar1.Items.Insert(1, "news", "News");
		/// ]]></code>
		/// </example>
		public void Insert(int index, string key, string text)
		{
			NavigationBarItem item = new NavigationBarItem()
			{
				Name = key,
				Text = text
			};
			Insert(index, item);
		}

		/// <summary>
		/// Creates a <see cref="NavigationBarItem" /> with the specified key, text, and image, and inserts it into the collection at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index location where the <see cref="NavigationBarItem" /> is inserted.</param>
		/// <param name="key">The name of the item.</param>
		/// <param name="text">The text to display on the <see cref="NavigationBarItem" />.</param>
		/// <param name="icon">The Url or name of the icon to display on the <see cref="NavigationBarItem" />.</param>
		/// <example>
		/// Inserting an item with an icon in the second position:
		/// <code><![CDATA[
		/// this.navigationBar1.Items.Insert(1, "news", "News", "Images/news.svg");
		/// ]]></code>
		/// </example>
		public void Insert(int index, string key, string text, string icon)
		{
			NavigationBarItem item = new NavigationBarItem()
			{
				Name = key,
				Text = text,
				Icon = icon
			};
			Insert(index, item);
		}

		#region IList

		int IList.Add(object value)
		{
			Add((NavigationBarItem)value);
			return IndexOf((NavigationBarItem)value);
		}

		void IList.Clear()
		{
			Clear();
		}

		bool IList.Contains(object value)
		{
			return Contains((NavigationBarItem)value);
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((NavigationBarItem)value);
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (NavigationBarItem)value);
		}

		bool IList.IsFixedSize
		{
			get { return false; }
		}

		bool IList.IsReadOnly
		{
			get { return false; }
		}

		void IList.Remove(object value)
		{
			Remove((NavigationBarItem)value);
		}

		object IList.this[int index]
		{
			get { return this[index]; }
			set { }
		}

		void ICollection.CopyTo(Array array, int index)
		{
			for (int i = 0; i < this.Count; i++)
				array.SetValue(this[i], i + index);
		}

		int ICollection.Count
		{
			get { return this.Count; }
		}

		bool ICollection.IsSynchronized
		{
			get { return true; }
		}

		object ICollection.SyncRoot
		{
			get { return this; }
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.controls.GetEnumerator();
		}

		#endregion

		#region ICollection<NavigationBarItem>, IList<NavigationBarItem>

		void ICollection<NavigationBarItem>.Add(NavigationBarItem value)
		{
			Add(value);
		}

		void ICollection<NavigationBarItem>.Clear()
		{
			Clear();
		}

		bool ICollection<NavigationBarItem>.Contains(NavigationBarItem value)
		{
			return Contains(value);
		}

		bool ICollection<NavigationBarItem>.IsReadOnly
		{
			get { return false; }
		}

		bool ICollection<NavigationBarItem>.Remove(NavigationBarItem value)
		{
			var index = IndexOf(value);
			if (index < 0)
				return false;

			RemoveAt(index);
			return true;
		}

		int IList<NavigationBarItem>.IndexOf(NavigationBarItem value)
		{
			return IndexOf(value);
		}

		void IList<NavigationBarItem>.Insert(int index, NavigationBarItem value)
		{
			Insert(index, value);
		}

		NavigationBarItem IList<NavigationBarItem>.this[int index]
		{
			get { return this[index]; }
			set { }
		}

		void ICollection<NavigationBarItem>.CopyTo(NavigationBarItem[] array, int index)
		{
			for (int i = 0; i < this.Count; i++)
				array.SetValue(this[i], i + index);
		}

		int ICollection<NavigationBarItem>.Count
		{
			get { return this.Count; }
		}

		IEnumerator<NavigationBarItem> IEnumerable<NavigationBarItem>.GetEnumerator()
		{
			return this.ToList().GetEnumerator();
		}

		#endregion
	}
}
