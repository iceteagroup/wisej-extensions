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
		public void Clear()
		{
			Clear(false);
		}

		/// <summary>
		/// Removes and disposes all <see cref="NavigationBarItem" /> instances from the collection.
		/// </summary>
		/// <param name="dispose">Indicates whether to dispose the <see cref="NavigationBarItem" /> instances removed from the collection.</param>
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
		///   <paramref name="array" /> is less than zero.</exception>
		/// <exception cref="ArgumentException">
		///   <paramref name="array" /> is multidimensional or the number of elements in the <see cref="NavigationBarItemCollection" /> is greater than the available space from index to the end of <paramref name="array" />.</exception>
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
		public void RemoveAt(int index)
		{
			Remove((NavigationBarItem)this.controls[index]);
		}

		/// <summary>
		/// Removes the <see cref="NavigationBarItem" /> with the specified key from the collection.
		/// </summary>
		/// <param name="key">The name of the <see cref="NavigationBarItem" /> to remove.</param>
		public void RemoveByKey(string key)
		{
			this.controls.RemoveByKey(key);
		}

		/// <summary>
		/// Inserts an existing <see cref="NavigationBarItem" /> into the collection at the specified index. 
		/// </summary>
		/// <param name="index">The zero-based index location where the <see cref="NavigationBarItem" /> is inserted.</param>
		/// <param name="item">The <see cref="NavigationBarItem" /> to insert in the collection.</param>
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
