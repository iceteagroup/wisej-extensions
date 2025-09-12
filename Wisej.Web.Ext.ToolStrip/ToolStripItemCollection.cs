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
using System.Drawing;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Represents a collection of <see cref="ToolStripItem" /> objects.
	///</summary>
	public class ToolStripItemCollection : IList
	{

		#region Constructors

		public ToolStripItemCollection(ToolStrip owner, ToolStripItem[] value)
		{
			//this._owner = owner;
			//this._value = value;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets a value indicating whether the <see cref="ToolStripItemCollection" /> is read-only.
		///</summary>
		/// <returns>true if the <see cref="ToolStripItemCollection" /> is read-only; otherwise, false.</returns>
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		public bool IsFixedSize => throw new NotImplementedException();

		public int Count => throw new NotImplementedException();

		public bool IsSynchronized => throw new NotImplementedException();

		public object SyncRoot => throw new NotImplementedException();

		object IList.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public ToolStripItem this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		private bool _isReadOnly;

		#endregion

		#region Methods

		/// <summary>
		/// Adds a <see cref="ToolStripItem" /> that displays the specified text to the collection.
		///</summary>
		/// <returns>The new <see cref="ToolStripItem" />.</returns>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripItem" />.</param>
		public ToolStripItem Add(string text)
		{
			// TODO: Implement
			//return new Wisej.Web.Ext.ToolStripItem();
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds a <see cref="ToolStripItem" /> that displays the specified image to the collection.
		///</summary>
		/// <returns>The new <see cref="ToolStripItem" />.</returns>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripItem" />.</param>
		public ToolStripItem Add(Image image)
		{
			// TODO: Implement
			//return new Wisej.Web.Ext.ToolStripItem();
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds a <see cref="ToolStripItem" /> that displays the specified image and text to the collection.
		///</summary>
		/// <returns>The new <see cref="ToolStripItem" />.</returns>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripItem" />.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripItem" />.</param>
		public ToolStripItem Add(string text, Image image)
		{
			// TODO: Implement
			//return new Wisej.Web.Ext.ToolStripItem();
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds a <see cref="ToolStripItem" /> that displays the specified image and text to the collection and that raises the <see cref="ToolStripItem.Click" /> event.
		///</summary>
		/// <returns>The new <see cref="ToolStripItem" />.</returns>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripItem" />.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripItem" />.</param>
		/// <param name="onClick">Raises the <see cref="ToolStripItem.Click" /> event.</param>
		public ToolStripItem Add(string text, Image image, EventHandler onClick)
		{
			// TODO: Implement
			//return new Wisej.Web.Ext.ToolStripItem();
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds the specified item to the end of the collection.
		///</summary>
		/// <exception cref="System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <returns>An <see cref="System.Int32" /> representing the zero-based index of the new item in the collection.</returns>
		/// <param name="value">The <see cref="ToolStripItem" /> to add to the end of the collection. </param>
		public int Add(ToolStripItem value)
		{
			// TODO: Implement
			return 0;
		}

		public void AddRange(ToolStripItem[] toolStripItems)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Adds a <see cref="ToolStripItemCollection" /> to the current collection.
		///</summary>
		/// <exception cref="System.ArgumentNullException">The <paramref name="toolStripItems" /> parameter is null. </exception>
		/// <exception cref="System.NotSupportedException">The <see cref="ToolStripItemCollection" /> is read-only.</exception>
		/// <param name="toolStripItems">The <see cref="ToolStripItemCollection" /> to be added to the current collection. </param>
		public void AddRange(ToolStripItemCollection toolStripItems)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Determines whether the specified item is a member of the collection.
		///</summary>
		/// <returns>true if the <see cref="ToolStripItem" /> is a member of the current <see cref="ToolStripItemCollection" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="ToolStripItem" /> to search for in the <see cref="ToolStripItemCollection" />. </param>
		public bool Contains(ToolStripItem value)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Removes all items from the collection.
		///</summary>
		/// <exception cref="System.NotSupportedException">The <see cref="ToolStripItemCollection" /> is read-only.</exception>
		public virtual void Clear()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Determines whether the collection contains an item with the specified key.
		///</summary>
		/// <returns>true if the <see cref="ToolStripItemCollection" /> contains a <see cref="ToolStripItem" /> with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="ToolStripItemCollection" />. </param>
		public virtual bool ContainsKey(string key)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Inserts the specified item into the collection at the specified index.
		///</summary>
		/// <exception cref="System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <param name="index">The location in the <see cref="ToolStripItemCollection" /> at which to insert the <see cref="ToolStripItem" />. </param>
		/// <param name="value">The <see cref="ToolStripItem" /> to insert. </param>
		public void Insert(int index, ToolStripItem value)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Retrieves the index of the specified item in the collection.
		///</summary>
		/// <returns>A zero-based index value that represents the position of the specified <see cref="ToolStripItem" /> in the <see cref="ToolStripItemCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="ToolStripItem" /> to locate in the <see cref="ToolStripItemCollection" />. </param>
		public int IndexOf(ToolStripItem value)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Retrieves the index of the first occurrence of the specified item within the collection.
		///</summary>
		/// <returns>A zero-based index value that represents the position of the first occurrence of the <see cref="ToolStripItem" /> specified by the <paramref name="key" /> parameter, if found; otherwise, -1.</returns>
		/// <param name="key">The name of the <see cref="ToolStripItem" /> to search for. </param>
		public virtual int IndexOfKey(string key)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Removes the specified item from the collection.
		///</summary>
		/// <exception cref="System.NotSupportedException">The <see cref="ToolStripItemCollection" /> is read-only.</exception>
		/// <param name="value">The <see cref="ToolStripItem" /> to remove from the <see cref="ToolStripItemCollection" />. </param>
		public void Remove(ToolStripItem value)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Removes an item from the specified index in the collection.
		///</summary>
		/// <exception cref="System.NotSupportedException">The <see cref="ToolStripItemCollection" /> is read-only.</exception>
		/// <param name="index">The index value of the <see cref="ToolStripItem" /> to remove. </param>
		public void RemoveAt(int index)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Removes the item that has the specified key.
		///</summary>
		/// <exception cref="System.NotSupportedException">The <see cref="ToolStripItemCollection" /> is read-only.</exception>
		/// <param name="key">The key of the <see cref="ToolStripItem" /> to remove. </param>
		public virtual void RemoveByKey(string key)
		{
			// TODO: Implement
		}

		public void CopyTo(ToolStripItem[] array, int index)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Searches for items by their name and returns an array of all matching controls.
		///</summary>
		/// <exception cref="System.ArgumentNullException">The <paramref name="key" /> parameter is null or empty.</exception>
		/// <returns>A <see cref="ToolStripItem" /> array of the search results.</returns>
		/// <param name="key">The item name to search the <see cref="ToolStripItemCollection" /> for.</param>
		/// <param name="searchAllChildren">true to search child items of the <see cref="ToolStripItem" /> specified by the <paramref name="key" /> parameter; otherwise, false. </param>
		public ToolStripItem[] Find(string key, bool searchAllChildren)
		{
			// TODO: Implement
			//return new Wisej.Web.Ext.ToolStripItem[]();
			throw new NotImplementedException();
		}

		public int Add(object value)
		{
			throw new NotImplementedException();
		}

		public bool Contains(object value)
		{
			throw new NotImplementedException();
		}

		public int IndexOf(object value)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, object value)
		{
			throw new NotImplementedException();
		}

		public void Remove(object value)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}

}

