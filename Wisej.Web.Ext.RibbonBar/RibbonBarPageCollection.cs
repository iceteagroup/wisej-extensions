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
		/// Adds the specified <para>item</para> to the collection.
		/// </summary>
		/// <param name="item">The <see cref="RibbonBarPage"/> to add to the collection.</param>
		public override void Add(RibbonBarPage item)
		{
			item.Parent = this.Owner;
			base.Add(item);
		}

		/// <summary>
		/// Inserts the specified <para>item</para> in the collection at the
		/// specified <para>index</para>.
		/// </summary>
		/// <param name="index">The position to insert the specified <see cref="RibbonBarPage"/> at.</param>
		/// <param name="item">The <see cref="RibbonBarPage"/> to insert in the collection.</param>
		public override void Insert(int index, RibbonBarPage item)
		{
			item.Parent = this.Owner;
			base.Insert(index, item);
		}
	}
}