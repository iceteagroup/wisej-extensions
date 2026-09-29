///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using Wisej.Core;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Base implementation of the component collections managed in the <see cref="RibbonBar"/> control.
	/// </summary>
	/// <typeparam name="TOwner">Type of the component that owns the collection.</typeparam>
	/// <typeparam name="TElement">Type of the components stored in the collection.</typeparam>
	/// <remarks>
	/// Adding, inserting or removing elements updates the owner on the client automatically.
	/// Elements cannot be replaced using the indexer; remove the element and insert the new one instead.
	/// </remarks>
	/// <exclude/>
	[ApiCategory("RibbonBar")]
	[ListBindable(false)]
	[Editor("Wisej.Design.DefaultCollectionEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class RibbonBarCollectionBase<TOwner, TElement> : IList<TElement>, IList
		where TOwner : IWisejComponent
		where TElement : IWisejComponent
	{
		private SynchronizedList<TElement> items;

		internal RibbonBarCollectionBase(TOwner owner)
		{
			if (owner == null)
				throw new ArgumentNullException(nameof(owner));

			this.Owner = owner;
			this.items = new SynchronizedList<TElement>();
		}

		/// <summary>
		/// Fired when the collection changes.
		/// </summary>
		public event CollectionChangeEventHandler CollectionChanged;

		/// <summary>
		/// Returns the <typeparamref name="TOwner"/> object.
		/// </summary>
		internal TOwner Owner
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns or sets the dirty state for the list.
		/// 
		/// When true, the list of items must be sent back to the client.
		/// </summary>
		internal bool IsDirty { get; set; }

		/// <summary>
		/// Returns or sets the new state for the list.
		/// 
		/// When true, the entire list is cleared and reloaded on the client.
		/// </summary>
		internal bool IsNew { get; set; }

		/// <summary>
		/// Returns the <typeparamref name="TElement"/> at the specified location.
		/// </summary>
		/// <param name="index">The index of the <typeparamref name="TElement"/> to retrieve.</param>
		/// <returns>The <typeparamref name="TElement"/> at the specified index.</returns>
		/// <exception cref="NotSupportedException">Cannot assign a <typeparamref name="TElement"/> item.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.</exception>
		/// <example>
		/// Iterating the pages in a <see cref="RibbonBar"/> by index:
		/// <code><![CDATA[
		/// for (int i = 0; i < this.ribbonBar1.Pages.Count; i++)
		/// {
		///     this.ribbonBar1.Pages[i].Enabled = (i == 0);
		/// }
		/// ]]></code>
		/// </example>
		public TElement this[int index]
		{
			get { return this.items[index]; }
			set { throw new NotSupportedException(); }
		}

		/// <summary>
		/// Returns the number of <typeparamref name="TElement"/> items in the collection.
		/// </summary>
		public int Count
		{
			get { return this.items.Count; }
		}

		/// <summary>
		/// Fires the <see cref="CollectionChanged" /> event.
		/// </summary>
		/// <param name="e">A <see cref="CollectionChangeEventArgs" /> that contains the event data.</param>
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.CollectionChanged != null)
				CollectionChanged(this, e);
		}

		/// <summary>
		/// Adds the specified <paramref name="item"/> to the collection.
		/// </summary>
		/// <param name="item">The <typeparamref name="TElement"/> to add to the collection.</param>
		/// <example>
		/// Adding a button to a group:
		/// <code><![CDATA[
		/// this.ribbonBarGroup1.Items.Add(new RibbonBarItemButton { Text = "Refresh", ImageSource = "icon-refresh" });
		/// ]]></code>
		/// </example>
		public virtual void Add(TElement item)
		{
			this.items.Add(item);

			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, item));
			Update();
		}

		/// <summary>
		/// Removes all the items from the collection.
		/// </summary>
		/// <remarks>
		/// The removed items are not disposed. Use <see cref="Clear(bool)"/> to dispose them.
		/// </remarks>
		/// <example>
		/// Removing all the items from a group to rebuild it:
		/// <code><![CDATA[
		/// this.ribbonBarGroup1.Items.Clear();
		/// ]]></code>
		/// </example>
		public virtual void Clear()
		{
			Clear(false);
		}

		/// <summary>
		/// Removes and optionally disposes all the items from the collection.
		/// </summary>
		/// <param name="dispose">true to dispose the removed items; false to only remove them.</param>
		/// <example>
		/// Removing and disposing all the groups in a page:
		/// <code><![CDATA[
		/// this.ribbonBarPage1.Groups.Clear(true);
		/// ]]></code>
		/// </example>
		public virtual void Clear(bool dispose)
		{
			TElement[] items = null;
			if (dispose)
				items = this.items.ToArray();

			this.items.Clear();
			this.items.TrimExcess();

			if (dispose && items != null && items.Length > 0)
			{
				foreach (var item in items)
				{
					if (item is IDisposable)
						((IDisposable)item).Dispose();
				}
			}

			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
			Update();
		}

		/// <summary>
		/// Checks whether the specified <paramref name="item"/> exists in the collection.
		/// </summary>
		/// <param name="item">The <typeparamref name="TElement"/> to find in the collection.</param>
		/// <returns>true if the specified <paramref name="item"/> exists in the collection; otherwise, false.</returns>
		/// <example>
		/// Adding a page only if it's not already in the <see cref="RibbonBar"/>:
		/// <code><![CDATA[
		/// if (!this.ribbonBar1.Pages.Contains(this.pageReview))
		///     this.ribbonBar1.Pages.Add(this.pageReview);
		/// ]]></code>
		/// </example>
		public virtual bool Contains(TElement item)
		{
			return this.items.Contains(item);
		}

		/// <summary>
		/// Copies all the items in the collection to the specified <paramref name="array"/> starting at the
		/// specified <paramref name="index"/>.
		/// </summary>
		/// <param name="array">The target array.</param>
		/// <param name="index">The starting index in the target array.</param>
		/// <example>
		/// Copying the items of a group to an array:
		/// <code><![CDATA[
		/// var items = new RibbonBarItem[this.ribbonBarGroup1.Items.Count];
		/// this.ribbonBarGroup1.Items.CopyTo(items, 0);
		/// ]]></code>
		/// </example>
		public void CopyTo(TElement[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>
		/// Returns an <see cref="IEnumerator{T}"/> to iterate the items in the collection.
		/// </summary>
		/// <returns>An instance of <see cref="IEnumerator{T}"/>.</returns>
		/// <example>
		/// Disabling all the items in a group:
		/// <code><![CDATA[
		/// foreach (var item in this.ribbonBarGroup1.Items)
		/// {
		///     item.Enabled = false;
		/// }
		/// ]]></code>
		/// </example>
		public IEnumerator<TElement> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>
		/// Returns the position of the specified <paramref name="item"/> in the collection.
		/// </summary>
		/// <param name="item">The <typeparamref name="TElement"/> to find in the collection.</param>
		/// <returns>The index of the <paramref name="item"/> or -1 if not found.</returns>
		/// <example>
		/// Inserting a new button right after an existing one:
		/// <code><![CDATA[
		/// var index = this.ribbonBarGroup1.Items.IndexOf(this.buttonCopy);
		/// if (index > -1)
		///     this.ribbonBarGroup1.Items.Insert(index + 1, new RibbonBarItemButton { Text = "Copy Format" });
		/// ]]></code>
		/// </example>
		public virtual int IndexOf(TElement item)
		{
			return this.items.IndexOf(item);
		}

		/// <summary>
		/// Inserts the specified <paramref name="item"/> in the collection at the
		/// specified <paramref name="index"/>.
		/// </summary>
		/// <param name="index">The position to insert the specified <typeparamref name="TElement"/> at.</param>
		/// <param name="item">The <typeparamref name="TElement"/> to insert in the collection.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than <see cref="Count"/>.</exception>
		/// <example>
		/// Inserting a page as the first tab:
		/// <code><![CDATA[
		/// this.ribbonBar1.Pages.Insert(0, new RibbonBarPage { Text = "Start" });
		/// ]]></code>
		/// </example>
		public virtual void Insert(int index, TElement item)
		{
			this.items.Insert(index, item);

			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, item));
			Update();
		}

		/// <summary>
		/// Removes the specified <typeparamref name="TElement"/> from the collection.
		/// </summary>
		/// <param name="item">The <typeparamref name="TElement"/> to remove from the collection.</param>
		/// <returns>true if the specified <paramref name="item"/> has been removed from the collection; false if it was not found.</returns>
		/// <remarks>
		/// The removed item is not disposed. Call <see cref="IDisposable.Dispose"/> on the item instead to remove it and dispose it.
		/// </remarks>
		/// <example>
		/// Removing a button from its group:
		/// <code><![CDATA[
		/// this.ribbonBarGroup1.Items.Remove(this.buttonMacros);
		/// ]]></code>
		/// </example>
		public virtual bool Remove(TElement item)
		{
			if (!this.items.Remove(item))
				return false;

			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, item));
			Update();
			return true;
		}

		/// <summary>
		/// Removes the <typeparamref name="TElement"/> at the specified position.
		/// </summary>
		/// <param name="index">The index of the <typeparamref name="TElement"/> to remove from the collection.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.</exception>
		/// <remarks>
		/// The removed item is not disposed.
		/// </remarks>
		/// <example>
		/// Removing the last page of the <see cref="RibbonBar"/>:
		/// <code><![CDATA[
		/// var pages = this.ribbonBar1.Pages;
		/// if (pages.Count > 0)
		///     pages.RemoveAt(pages.Count - 1);
		/// ]]></code>
		/// </example>
		public virtual void RemoveAt(int index)
		{
			var item = this[index];
			this.items.RemoveAt(index);

			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, item));
			Update();
		}

		#region IList, ICollection, IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		bool IList.IsReadOnly
		{
			get { return false; }
		}
		bool ICollection<TElement>.IsReadOnly
		{
			get { return false; }
		}

		bool IList.IsFixedSize
		{
			get { return false; }
		}

		object ICollection.SyncRoot
		{
			get { return this; }
		}

		bool ICollection.IsSynchronized
		{
			get { return true; }
		}

		object IList.this[int index]
		{
			get { return this[index]; }
			set { }
		}

		#endregion

		#region Wisej Implementation

		private void Update()
		{
			this.IsDirty = true;
			this.Owner?.UpdateWidget();
		}

		/// <summary>
		/// Renders the list to the json definition for the client.
		/// </summary>
		/// <returns></returns>
		internal object Render()
		{
			lock (this)
			{
				object config = null;

				IWisejComponent owner = this.Owner;

				if (!owner.DesignMode)
				{
					// render the full list?
					bool isNew = (this.IsNew || owner.IsNew);
					if (!isNew && !this.IsDirty)
						return null;
				}

				// reset the state of the list.
				this.IsNew = false;
				this.IsDirty = false;

				if (owner.DesignMode)
				{
					// in design mode we render the child components (the items)
					// directly into the parent's configuration package to render
					// them in one shot on the designer surface.
					List<dynamic> list = new List<dynamic>();
					foreach (IWisejComponent item in this.items)
					{
						dynamic itemConfig = new DynamicObject();
						item.DesignMode = true;
						item.Render(itemConfig);
						list.Add(itemConfig);
					}
					config = list;
				}
				else
				{
					// at runtime, we only render the ids of the items.
					// each button is a component by itself.
					List<string> list = new List<string>();
					foreach (IWisejComponent item in this.items)
					{
						list.Add(item.Id);
					}
					config = list;
				}
				return config;
			}
		}

		int IList.Add(object value)
		{
			Add((TElement)value);
			return this.Count - 1;
		}

		bool IList.Contains(object value)
		{
			return Contains((TElement)value);
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((TElement)value);
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (TElement)value);
		}

		void IList.Remove(object value)
		{
			Remove((TElement)value);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			((IList)this.items).CopyTo(array, index);
		}

		#endregion

	}
}