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
	/// Represents a collection of <see cref="RibbonBarItem"/> in a <see cref="RibbonBarGroup"/>.
	/// </summary>
	[ApiCategory("RibbonBar")]
	public class RibbonBarItemCollection : RibbonBarCollectionBase<RibbonBarGroup, RibbonBarItem>
	{
		internal RibbonBarItemCollection(RibbonBarGroup owner) : base(owner)
		{
		}

		/// <summary>
		/// Returns the first <see cref="RibbonBarItem"/> with the specified <paramref name="name"/>.
		/// </summary>
		/// <param name="name">The name of the <see cref="RibbonBarItem"/> to retrieve.</param>
		/// <returns>The first <see cref="RibbonBarItem"/> with the specified name or null.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
		/// <remarks>
		/// The comparison with the <see cref="RibbonBarItem.Name"/> property is case insensitive.
		/// </remarks>
		/// <example>
		/// Retrieving a <see cref="RibbonBarItem"/> by name:
		/// <code><![CDATA[
		/// var bold = (RibbonBarItemButton)this.ribbonBarGroup1.Items["bold"];
		/// bold.Pushed = true;
		/// ]]></code>
		/// </example>
		public RibbonBarItem this[string name]
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
		/// <param name="item">The <see cref="RibbonBarItem"/> to add to the collection.</param>
		/// <remarks>
		/// The <see cref="RibbonBarItem.Parent"/> of the <paramref name="item"/> is set to the <see cref="RibbonBarGroup"/> that owns this collection.
		/// </remarks>
		/// <example>
		/// Adding a new <see cref="RibbonBarItem"/>:
		/// <code><![CDATA[
		/// this.ribbonBarGroup1.Items.Add(new RibbonBarItemCheckBox { Name = "ruler", Text = "Ruler" });
		/// ]]></code>
		/// </example>
		public override void Add(RibbonBarItem item)
		{
			item.Parent = this.Owner;
			base.Add(item);
		}

		/// <summary>
		/// Inserts the specified <paramref name="item"/> in the collection at the
		/// specified <paramref name="index"/>.
		/// </summary>
		/// <param name="index">The position to insert the specified <see cref="RibbonBarItem"/> at.</param>
		/// <param name="item">The <see cref="RibbonBarItem"/> to insert in the collection.</param>
		/// <remarks>
		/// The <see cref="RibbonBarItem.Parent"/> of the <paramref name="item"/> is set to the <see cref="RibbonBarGroup"/> that owns this collection.
		/// </remarks>
		/// <example>
		/// Inserting a <see cref="RibbonBarItem"/> at a specific position:
		/// <code><![CDATA[
		/// this.ribbonBarGroup1.Items.Insert(0, new RibbonBarItemButton { Name = "paste", Text = "Paste" });
		/// ]]></code>
		/// </example>
		public override void Insert(int index, RibbonBarItem item)
		{
			item.Parent = this.Owner;
			base.Insert(index, item);
		}
	}
}