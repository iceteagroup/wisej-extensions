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

using System.Collections.Generic;
using Wisej.Web.Ext.ToolStrip;

namespace System.Windows.Forms
{
	internal class ToolStripCustomIComparer : IComparer<ToolStrip>
	{
		public int Compare(ToolStrip? x, ToolStrip? y)
		{
			if (x is null && y is null)
			{
				return 0;
			}

			if (x is null)
			{
				return -1;
			}

			if (y is null)
			{
				return 1;
			}

			if (x.GetType() == y.GetType())
			{
				return 0; // same type
			}

			if (x.GetType().IsAssignableFrom(y.GetType()))
			{
				return 1;
			}

			if (y.GetType().IsAssignableFrom(x.GetType()))
			{
				return -1;
			}

			return 0; // not the same type, not in each other inheritance chain
		}
	}
}
