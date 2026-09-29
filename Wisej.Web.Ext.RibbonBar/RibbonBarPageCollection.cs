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
using System.ComponentModel;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a collection of <see cref="RibbonBarPage"/> in a <see cref="RibbonBar"/> control.
	/// </summary>
	[ApiCategory("RibbonBar")]
	public class RibbonBarPageCollection : RibbonBarCollectionBase<RibbonBar, RibbonBarPage>
	{
		internal RibbonBarPageCollection(RibbonBar owner) : base(owner)
		{
		}

		/// <summary>
		/// Returns the first <see cref="RibbonBarPage"/> with the specified <paramref name="name"/>.
		/// </summary>
		/// <param name="name">The name of the <see cref="RibbonBarPage"/> to retrieve.</param>
		/// <returns>The first <see cref="RibbonBarPage"/> with the specified name or null.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
		/// <remarks>
		/// The comparison with the <see cref="RibbonBarPage.Name"/> property is case insensitive.
		/// </remarks>
		/// <example>
		/// Retrieving a <see cref="RibbonBarPage"/> by name:
		/// <code><![CDATA[
		/// this.ribbonBar1.Pages["review"].Visible = false;
		/// ]]></code>
		/// </example>
		public RibbonBarPage this[string name]
		{
			get
			{
				if (name == null)
					throw new ArgumentNullException(nameof(name));

				var count = this.Count;
				for (var i = 0; i < count; i++)
				{
					if (String.Compare(name, this[i].Name, true) == 0)
						return this[i];
				}

				return null;
			}
		}

		/// <summary>
		/// Adds the specified <paramref name="item"/> to the collection.
		/// </summary>
		/// <param name="item">The <see cref="RibbonBarPage"/> to add to the collection.</param>
		/// <remarks>
		/// The <see cref="RibbonBarPage.Parent"/> of the <paramref name="item"/> is set to the <see cref="RibbonBar"/> that owns this collection.
		/// </remarks>
		/// <example>
		/// Adding a new <see cref="RibbonBarPage"/>:
		/// <code><![CDATA[
		/// this.ribbonBar1.Pages.Add(new RibbonBarPage { Name = "view", Text = "&View" });
		/// ]]></code>
		/// </example>
		public override void Add(RibbonBarPage item)
		{
			item.Parent = this.Owner;
			base.Add(item);
		}

		/// <summary>
		/// Inserts the specified <paramref name="item"/> in the collection at the
		/// specified <paramref name="index"/>.
		/// </summary>
		/// <param name="index">The position to insert the specified <see cref="RibbonBarPage"/> at.</param>
		/// <param name="item">The <see cref="RibbonBarPage"/> to insert in the collection.</param>
		/// <remarks>
		/// The <see cref="RibbonBarPage.Parent"/> of the <paramref name="item"/> is set to the <see cref="RibbonBar"/> that owns this collection.
		/// </remarks>
		/// <example>
		/// Inserting a <see cref="RibbonBarPage"/> at a specific position:
		/// <code><![CDATA[
		/// this.ribbonBar1.Pages.Insert(1, new RibbonBarPage { Name = "design", Text = "&Design" });
		/// ]]></code>
		/// </example>
		public override void Insert(int index, RibbonBarPage item)
		{
			item.Parent = this.Owner;
			base.Insert(index, item);
		}
	}
}