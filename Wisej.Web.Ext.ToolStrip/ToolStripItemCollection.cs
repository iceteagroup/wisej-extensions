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
	/// </summary>
	/// <remarks>
	/// The collection is exposed by <see cref="ToolStrip.Items" /> and <see cref="ToolStripDropDownItem.DropDownItems" />.
	/// Adding an item to the collection sets its <see cref="ToolStripItem.Owner" />.
	/// </remarks>
	/// <example>
	/// Populating a tool bar and a drop-down menu:
	/// <code><![CDATA[
	/// this.toolStrip1.Items.Add(new ToolStripButton("New"));
	/// this.toolStrip1.Items.Add(new ToolStripSeparator());
	///
	/// var file = new ToolStripDropDownButton("File");
	/// file.DropDownItems.AddRange(new ToolStripItem[] {
	///     new ToolStripMenuItem("Open"),
	///     new ToolStripMenuItem("Save")
	/// });
	/// this.toolStrip1.Items.Add(file);
	/// ]]></code>
	/// </example>
	public class ToolStripItemCollection : IList
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripItemCollection" /> class with the specified owner and initial items.
		/// </summary>
		/// <param name="owner">The <see cref="ToolStrip" /> that owns the collection.</param>
		/// <param name="value">An array of <see cref="ToolStripItem" /> objects used to populate the collection.</param>
		public ToolStripItemCollection(ToolStrip owner, ToolStripItem[] value)
		{
			//this._owner = owner;
			//this._value = value;
			// TODO: Implement
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns a value indicating whether the <see cref="ToolStripItemCollection" /> is read-only.
		/// </summary>
		/// <returns>true if the <see cref="ToolStripItemCollection" /> is read-only; otherwise, false.</returns>
		/// <remarks>
		/// Methods that modify a read-only collection throw a <see cref="System.NotSupportedException" />.
		/// </remarks>
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		/// <summary>
		/// Returns a value indicating whether the <see cref="ToolStripItemCollection" /> has a fixed size.
		/// </summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		public bool IsFixedSize => throw new NotImplementedException();

		/// <summary>
		/// Returns the number of items in the collection.
		/// </summary>
		/// <returns>The number of <see cref="ToolStripItem" /> objects in the collection.</returns>
		public int Count => throw new NotImplementedException();

		/// <summary>
		/// Returns a value indicating whether access to the collection is synchronized (thread safe).
		/// </summary>
		/// <returns>true if access to the collection is synchronized; otherwise, false.</returns>
		public bool IsSynchronized => throw new NotImplementedException();

		/// <summary>
		/// Returns an object that can be used to synchronize access to the collection.
		/// </summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		public object SyncRoot => throw new NotImplementedException();

		object IList.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		/// <summary>
		/// Returns or sets the <see cref="ToolStripItem" /> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the item.</param>
		/// <returns>The <see cref="ToolStripItem" /> at the specified index.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="index" /> is less than zero or greater than or equal to <see cref="ToolStripItemCollection.Count" />.</exception>
		/// <example>
		/// Disabling all the items after the first one:
		/// <code><![CDATA[
		/// for (int i = 1; i < this.toolStrip1.Items.Count; i++)
		/// {
		///     this.toolStrip1.Items[i].Enabled = false;
		/// }
		/// ]]></code>
		/// </example>
		public ToolStripItem this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		private bool _isReadOnly;

		#endregion

		#region Methods

		/// <summary>
		/// Adds a new <see cref="ToolStripItem" /> that displays the specified text to the collection.
		/// </summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripItem" />.</param>
		/// <returns>The new <see cref="ToolStripItem" />.</returns>
		/// <remarks>
		/// The type of the item created is determined by the owner of the collection, for example a <see cref="ToolStripButton" />
		/// on a <see cref="ToolStrip" /> or a <see cref="ToolStripMenuItem" /> on a <see cref="ToolStripDropDownMenu" />.
		/// </remarks>
		/// <example>
		/// Adding a text item and handling its click:
		/// <code><![CDATA[
		/// ToolStripItem refresh = this.toolStrip1.Items.Add("Refresh");
		/// refresh.Click += (s, e) => RefreshData();
		/// ]]></code>
		/// </example>
		public ToolStripItem Add(string text)
		{
			// TODO: Implement
			//return new Wisej.Web.Ext.ToolStripItem();
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds a new <see cref="ToolStripItem" /> that displays the specified image to the collection.
		/// </summary>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripItem" />.</param>
		/// <returns>The new <see cref="ToolStripItem" />.</returns>
		/// <remarks>
		/// The type of the item created is determined by the owner of the collection.
		/// </remarks>
		/// <example>
		/// Adding an image-only item:
		/// <code><![CDATA[
		/// ToolStripItem print = this.toolStrip1.Items.Add(Image.FromFile(Application.MapPath("Images/print.png")));
		/// print.Name = "buttonPrint";
		/// ]]></code>
		/// </example>
		public ToolStripItem Add(Image image)
		{
			// TODO: Implement
			//return new Wisej.Web.Ext.ToolStripItem();
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds a new <see cref="ToolStripItem" /> that displays the specified image and text to the collection.
		/// </summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripItem" />.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripItem" />.</param>
		/// <returns>The new <see cref="ToolStripItem" />.</returns>
		/// <remarks>
		/// The type of the item created is determined by the owner of the collection.
		/// </remarks>
		/// <example>
		/// Adding an item with text and image:
		/// <code><![CDATA[
		/// this.toolStrip1.Items.Add("Save", Image.FromFile(Application.MapPath("Images/save.png")));
		/// ]]></code>
		/// </example>
		public ToolStripItem Add(string text, Image image)
		{
			// TODO: Implement
			//return new Wisej.Web.Ext.ToolStripItem();
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds a new <see cref="ToolStripItem" /> that displays the specified image and text to the collection and attaches the specified handler to its <see cref="ToolStripItem.Click" /> event.
		/// </summary>
		/// <param name="text">The text to be displayed on the <see cref="ToolStripItem" />.</param>
		/// <param name="image">The <see cref="System.Drawing.Image" /> to be displayed on the <see cref="ToolStripItem" />, or null.</param>
		/// <param name="onClick">The event handler attached to the <see cref="ToolStripItem.Click" /> event.</param>
		/// <returns>The new <see cref="ToolStripItem" />.</returns>
		/// <remarks>
		/// The type of the item created is determined by the owner of the collection.
		/// </remarks>
		/// <example>
		/// Adding an item together with its click handler:
		/// <code><![CDATA[
		/// this.toolStrip1.Items.Add("Delete", null, this.toolStripDelete_Click);
		///
		/// private void toolStripDelete_Click(object sender, EventArgs e)
		/// {
		///     MessageBox.Show("Delete the selected record?", "Delete", MessageBoxButtons.YesNo);
		/// }
		/// ]]></code>
		/// </example>
		public ToolStripItem Add(string text, Image image, EventHandler onClick)
		{
			// TODO: Implement
			//return new Wisej.Web.Ext.ToolStripItem();
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds the specified item to the end of the collection.
		/// </summary>
		/// <param name="value">The <see cref="ToolStripItem" /> to add to the end of the collection.</param>
		/// <returns>An <see cref="System.Int32" /> representing the zero-based index of the new item in the collection.</returns>
		/// <exception cref="System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <remarks>
		/// An item can belong to only one collection: adding it to a different collection removes it from its current owner.
		/// </remarks>
		/// <example>
		/// Adding a button created in code:
		/// <code><![CDATA[
		/// var button = new ToolStripButton("Export");
		/// button.Name = "buttonExport";
		/// int index = this.toolStrip1.Items.Add(button);
		/// ]]></code>
		/// </example>
		public int Add(ToolStripItem value)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Adds an array of <see cref="ToolStripItem" /> objects to the collection.
		/// </summary>
		/// <param name="toolStripItems">An array of <see cref="ToolStripItem" /> objects to add to the collection.</param>
		/// <exception cref="System.ArgumentNullException">The <paramref name="toolStripItems" /> parameter is null.</exception>
		/// <exception cref="System.NotSupportedException">The <see cref="ToolStripItemCollection" /> is read-only.</exception>
		/// <example>
		/// Adding several items at once:
		/// <code><![CDATA[
		/// this.toolStrip1.Items.AddRange(new ToolStripItem[] {
		///     new ToolStripButton("Cut"),
		///     new ToolStripButton("Copy"),
		///     new ToolStripButton("Paste")
		/// });
		/// ]]></code>
		/// </example>
		public void AddRange(ToolStripItem[] toolStripItems)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Adds the items of another <see cref="ToolStripItemCollection" /> to the current collection.
		/// </summary>
		/// <param name="toolStripItems">The <see cref="ToolStripItemCollection" /> to be added to the current collection.</param>
		/// <exception cref="System.ArgumentNullException">The <paramref name="toolStripItems" /> parameter is null.</exception>
		/// <exception cref="System.NotSupportedException">The <see cref="ToolStripItemCollection" /> is read-only.</exception>
		/// <remarks>
		/// Since an item can belong to only one collection, the added items are moved from <paramref name="toolStripItems" /> to the current collection.
		/// </remarks>
		/// <example>
		/// Moving all the items of a tool bar into a drop-down button:
		/// <code><![CDATA[
		/// this.toolStripDropDownButtonMore.DropDownItems.AddRange(this.toolStrip2.Items);
		/// ]]></code>
		/// </example>
		public void AddRange(ToolStripItemCollection toolStripItems)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Determines whether the specified item is a member of the collection.
		/// </summary>
		/// <param name="value">The <see cref="ToolStripItem" /> to search for in the <see cref="ToolStripItemCollection" />.</param>
		/// <returns>true if the <see cref="ToolStripItem" /> is a member of the current <see cref="ToolStripItemCollection" />; otherwise, false.</returns>
		/// <example>
		/// Adding a button only once:
		/// <code><![CDATA[
		/// if (!this.toolStrip1.Items.Contains(this.toolStripButtonAdmin))
		///     this.toolStrip1.Items.Add(this.toolStripButtonAdmin);
		/// ]]></code>
		/// </example>
		public bool Contains(ToolStripItem value)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Removes all items from the collection.
		/// </summary>
		/// <exception cref="System.NotSupportedException">The <see cref="ToolStripItemCollection" /> is read-only.</exception>
		/// <remarks>
		/// The removed items are not disposed.
		/// </remarks>
		/// <example>
		/// Rebuilding a drop-down menu:
		/// <code><![CDATA[
		/// this.toolStripMenuItemRecent.DropDownItems.Clear();
		/// foreach (string file in GetRecentFiles())
		/// {
		///     this.toolStripMenuItemRecent.DropDownItems.Add(file);
		/// }
		/// ]]></code>
		/// </example>
		public virtual void Clear()
		{
			// TODO: Implement
		}

		/// <summary>
		/// Determines whether the collection contains an item with the specified key.
		/// </summary>
		/// <param name="key">The key to locate in the <see cref="ToolStripItemCollection" />.</param>
		/// <returns>true if the <see cref="ToolStripItemCollection" /> contains a <see cref="ToolStripItem" /> with the specified key; otherwise, false.</returns>
		/// <remarks>
		/// The key is compared to the <see cref="ToolStripItem.Name" /> of the items, ignoring case.
		/// </remarks>
		/// <example>
		/// Adding an item only if no item with the same name exists:
		/// <code><![CDATA[
		/// if (!this.toolStrip1.Items.ContainsKey("buttonExport"))
		/// {
		///     var export = new ToolStripButton("Export");
		///     export.Name = "buttonExport";
		///     this.toolStrip1.Items.Add(export);
		/// }
		/// ]]></code>
		/// </example>
		public virtual bool ContainsKey(string key)
		{
			// TODO: Implement
			return false;
		}

		/// <summary>
		/// Inserts the specified item into the collection at the specified index.
		/// </summary>
		/// <param name="index">The location in the <see cref="ToolStripItemCollection" /> at which to insert the <see cref="ToolStripItem" />.</param>
		/// <param name="value">The <see cref="ToolStripItem" /> to insert.</param>
		/// <exception cref="System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <example>
		/// Inserting a button as the first item:
		/// <code><![CDATA[
		/// this.toolStrip1.Items.Insert(0, new ToolStripButton("Home"));
		/// ]]></code>
		/// </example>
		public void Insert(int index, ToolStripItem value)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Retrieves the index of the specified item in the collection.
		/// </summary>
		/// <param name="value">The <see cref="ToolStripItem" /> to locate in the <see cref="ToolStripItemCollection" />.</param>
		/// <returns>A zero-based index value that represents the position of the specified <see cref="ToolStripItem" /> in the <see cref="ToolStripItemCollection" />, if found; otherwise, -1.</returns>
		/// <example>
		/// Inserting a new button right after an existing one:
		/// <code><![CDATA[
		/// int index = this.toolStrip1.Items.IndexOf(this.toolStripButtonSave);
		/// this.toolStrip1.Items.Insert(index + 1, new ToolStripButton("Save As"));
		/// ]]></code>
		/// </example>
		public int IndexOf(ToolStripItem value)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Retrieves the index of the first item in the collection with the specified key.
		/// </summary>
		/// <param name="key">The <see cref="ToolStripItem.Name" /> of the <see cref="ToolStripItem" /> to search for.</param>
		/// <returns>A zero-based index value that represents the position of the first occurrence of the <see cref="ToolStripItem" /> specified by the <paramref name="key" /> parameter, if found; otherwise, -1.</returns>
		/// <remarks>
		/// The key is compared to the <see cref="ToolStripItem.Name" /> of the items, ignoring case.
		/// </remarks>
		/// <example>
		/// Retrieving an item by name:
		/// <code><![CDATA[
		/// int index = this.toolStrip1.Items.IndexOfKey("buttonExport");
		/// if (index > -1)
		///     this.toolStrip1.Items[index].Enabled = false;
		/// ]]></code>
		/// </example>
		public virtual int IndexOfKey(string key)
		{
			// TODO: Implement
			return 0;
		}

		/// <summary>
		/// Removes the specified item from the collection.
		/// </summary>
		/// <param name="value">The <see cref="ToolStripItem" /> to remove from the <see cref="ToolStripItemCollection" />.</param>
		/// <exception cref="System.NotSupportedException">The <see cref="ToolStripItemCollection" /> is read-only.</exception>
		/// <remarks>
		/// The removed item is not disposed.
		/// </remarks>
		/// <example>
		/// Removing a button:
		/// <code><![CDATA[
		/// this.toolStrip1.Items.Remove(this.toolStripButtonAdmin);
		/// ]]></code>
		/// </example>
		public void Remove(ToolStripItem value)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Removes an item from the specified index in the collection.
		/// </summary>
		/// <param name="index">The index value of the <see cref="ToolStripItem" /> to remove.</param>
		/// <exception cref="System.NotSupportedException">The <see cref="ToolStripItemCollection" /> is read-only.</exception>
		/// <example>
		/// Removing the last item:
		/// <code><![CDATA[
		/// if (this.toolStrip1.Items.Count > 0)
		///     this.toolStrip1.Items.RemoveAt(this.toolStrip1.Items.Count - 1);
		/// ]]></code>
		/// </example>
		public void RemoveAt(int index)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Removes the item that has the specified key.
		/// </summary>
		/// <param name="key">The <see cref="ToolStripItem.Name" /> of the <see cref="ToolStripItem" /> to remove.</param>
		/// <exception cref="System.NotSupportedException">The <see cref="ToolStripItemCollection" /> is read-only.</exception>
		/// <example>
		/// Removing an item by name:
		/// <code><![CDATA[
		/// this.toolStrip1.Items.RemoveByKey("buttonExport");
		/// ]]></code>
		/// </example>
		public virtual void RemoveByKey(string key)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Copies the collection into the specified array, starting at the specified index.
		/// </summary>
		/// <param name="array">The destination array of <see cref="ToolStripItem" /> objects.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <example>
		/// Copying the items to an array:
		/// <code><![CDATA[
		/// var items = new ToolStripItem[this.toolStrip1.Items.Count];
		/// this.toolStrip1.Items.CopyTo(items, 0);
		/// ]]></code>
		/// </example>
		public void CopyTo(ToolStripItem[] array, int index)
		{
			// TODO: Implement
		}

		/// <summary>
		/// Searches for items by their name and returns an array of all matching items.
		/// </summary>
		/// <param name="key">The item name to search the <see cref="ToolStripItemCollection" /> for.</param>
		/// <param name="searchAllChildren">true to also search the <see cref="ToolStripDropDownItem.DropDownItems" /> of the items in the collection, recursively; otherwise, false.</param>
		/// <returns>A <see cref="ToolStripItem" /> array of the search results.</returns>
		/// <exception cref="System.ArgumentNullException">The <paramref name="key" /> parameter is null or empty.</exception>
		/// <remarks>
		/// The key is compared to the <see cref="ToolStripItem.Name" /> of the items, ignoring case.
		/// </remarks>
		/// <example>
		/// Disabling a menu item nested anywhere in the tool bar's drop-downs:
		/// <code><![CDATA[
		/// foreach (ToolStripItem item in this.toolStrip1.Items.Find("menuItemDelete", true))
		/// {
		///     item.Enabled = false;
		/// }
		/// ]]></code>
		/// </example>
		public ToolStripItem[] Find(string key, bool searchAllChildren)
		{
			// TODO: Implement
			//return new Wisej.Web.Ext.ToolStripItem[]();
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds the specified object, which must be a <see cref="ToolStripItem" />, to the end of the collection.
		/// </summary>
		/// <param name="value">The <see cref="ToolStripItem" /> to add.</param>
		/// <returns>The zero-based index of the new item in the collection.</returns>
		/// <remarks>
		/// Implements <see cref="System.Collections.IList.Add" />. Use <see cref="ToolStripItemCollection.Add(ToolStripItem)" /> in typed code.
		/// </remarks>
		/// <example>
		/// Adding an item through the <see cref="System.Collections.IList" /> interface:
		/// <code><![CDATA[
		/// IList list = this.toolStrip1.Items;
		/// list.Add(new ToolStripButton("Help"));
		/// ]]></code>
		/// </example>
		public int Add(object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Determines whether the specified object is a member of the collection.
		/// </summary>
		/// <param name="value">The object to search for.</param>
		/// <returns>true if <paramref name="value" /> is a <see cref="ToolStripItem" /> in the collection; otherwise, false.</returns>
		/// <remarks>
		/// Implements <see cref="System.Collections.IList.Contains" />. Use <see cref="ToolStripItemCollection.Contains(ToolStripItem)" /> in typed code.
		/// </remarks>
		/// <example>
		/// Checking for an item through the <see cref="System.Collections.IList" /> interface:
		/// <code><![CDATA[
		/// IList list = this.toolStrip1.Items;
		/// bool found = list.Contains(this.toolStripButtonSave);
		/// ]]></code>
		/// </example>
		public bool Contains(object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Retrieves the index of the specified object in the collection.
		/// </summary>
		/// <param name="value">The object to locate.</param>
		/// <returns>The zero-based index of <paramref name="value" />, if found; otherwise, -1.</returns>
		/// <remarks>
		/// Implements <see cref="System.Collections.IList.IndexOf" />. Use <see cref="ToolStripItemCollection.IndexOf(ToolStripItem)" /> in typed code.
		/// </remarks>
		/// <example>
		/// Locating an item through the <see cref="System.Collections.IList" /> interface:
		/// <code><![CDATA[
		/// IList list = this.toolStrip1.Items;
		/// int index = list.IndexOf(this.toolStripButtonSave);
		/// ]]></code>
		/// </example>
		public int IndexOf(object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Inserts the specified object, which must be a <see cref="ToolStripItem" />, into the collection at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The <see cref="ToolStripItem" /> to insert.</param>
		/// <remarks>
		/// Implements <see cref="System.Collections.IList.Insert" />. Use <see cref="ToolStripItemCollection.Insert(int, ToolStripItem)" /> in typed code.
		/// </remarks>
		/// <example>
		/// Inserting an item through the <see cref="System.Collections.IList" /> interface:
		/// <code><![CDATA[
		/// IList list = this.toolStrip1.Items;
		/// list.Insert(0, new ToolStripButton("Home"));
		/// ]]></code>
		/// </example>
		public void Insert(int index, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Removes the specified object from the collection.
		/// </summary>
		/// <param name="value">The <see cref="ToolStripItem" /> to remove.</param>
		/// <remarks>
		/// Implements <see cref="System.Collections.IList.Remove" />. Use <see cref="ToolStripItemCollection.Remove(ToolStripItem)" /> in typed code.
		/// </remarks>
		/// <example>
		/// Removing an item through the <see cref="System.Collections.IList" /> interface:
		/// <code><![CDATA[
		/// IList list = this.toolStrip1.Items;
		/// list.Remove(this.toolStripButtonAdmin);
		/// ]]></code>
		/// </example>
		public void Remove(object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Copies the collection into the specified array, starting at the specified index.
		/// </summary>
		/// <param name="array">The destination array.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <remarks>
		/// Implements <see cref="System.Collections.ICollection.CopyTo" />. Use <see cref="ToolStripItemCollection.CopyTo(ToolStripItem[], int)" /> in typed code.
		/// </remarks>
		/// <example>
		/// Copying the items to an untyped array:
		/// <code><![CDATA[
		/// Array items = new object[this.toolStrip1.Items.Count];
		/// this.toolStrip1.Items.CopyTo(items, 0);
		/// ]]></code>
		/// </example>
		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>An <see cref="System.Collections.IEnumerator" /> for the collection.</returns>
		/// <example>
		/// Enumerating the items of a tool bar:
		/// <code><![CDATA[
		/// foreach (ToolStripItem item in this.toolStrip1.Items)
		/// {
		///     item.Enabled = !this.readOnlyMode;
		/// }
		/// ]]></code>
		/// </example>
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}

}

