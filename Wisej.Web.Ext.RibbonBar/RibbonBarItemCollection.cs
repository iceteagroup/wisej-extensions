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

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a collection of <see cref="RibbonBarItem"/> in a <see cref="RibbonBarGroup"/>
	/// </summary>
	public class RibbonBarItemCollection : RibbonBarCollectionBase<RibbonBarGroup, RibbonBarItem>
	{
		internal RibbonBarItemCollection(RibbonBarGroup owner) : base(owner)
		{
		}

		/// <summary>
		/// Adds the specified <para>item</para> to the collection.
		/// </summary>
		/// <param name="item">The <see cref="RibbonBarItem"/> to add to the collection.</param>
		public override void Add(RibbonBarItem item)
		{
			item.Parent = this.Owner;
			base.Add(item);
		}

		/// <summary>
		/// Inserts the specified <para>item</para> in the collection at the
		/// specified <para>index</para>.
		/// </summary>
		/// <param name="index">The position to insert the specified <see cref="RibbonBarItem"/> at.</param>
		/// <param name="item">The <see cref="RibbonBarItem"/> to insert in the collection.</param>
		public override void Insert(int index, RibbonBarItem item)
		{
			item.Parent = this.Owner;
			base.Insert(index, item);
		}
	}
}