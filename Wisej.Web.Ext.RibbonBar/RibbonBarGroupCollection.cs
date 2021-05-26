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

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a collection of <see cref="RibbonBarGroup"/> in a <see cref="RibbonBarPage"/>.
	/// </summary>
	public class RibbonBarGroupCollection : RibbonBarCollectionBase<RibbonBarPage, RibbonBarGroup>
	{
		internal RibbonBarGroupCollection(RibbonBarPage owner) : base(owner)
		{
		}

		/// <summary>
		/// Returns the first <see cref="RibbonBarGroup"/> with the specified <paramref name="name"/>.
		/// </summary>
		/// <param name="name">The name of the <see cref="RibbonBarGroup"/> to retrieve.</param>
		/// <returns>The first <see cref="RibbonBarGroup"/> with the specified name or null.</returns>
		public RibbonBarGroup this[string name]
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
		/// <param name="item">The <see cref="RibbonBarGroup"/> to add to the collection.</param>
		public override void Add(RibbonBarGroup item)
		{
			item.Parent = this.Owner;
			base.Add(item);
		}

		/// <summary>
		/// Inserts the specified <para>item</para> in the collection at the
		/// specified <para>index</para>.
		/// </summary>
		/// <param name="index">The position to insert the specified <see cref="RibbonBarGroup"/> at.</param>
		/// <param name="item">The <see cref="RibbonBarGroup"/> to insert in the collection.</param>
		public override void Insert(int index, RibbonBarGroup item)
		{
			item.Parent = this.Owner;
			base.Insert(index, item);
		}
	}
}