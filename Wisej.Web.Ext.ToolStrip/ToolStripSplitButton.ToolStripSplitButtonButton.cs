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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.ToolStrip
{
	public partial class ToolStripSplitButton
	{
		private class ToolStripSplitButtonButton : ToolStripButton
		{
			private readonly ToolStripSplitButton _owner;

			public ToolStripSplitButtonButton(ToolStripSplitButton owner)
			{
				_owner = owner;
			}

			public override bool Enabled
			{
				get
				{
					return _owner.Enabled;
				}
				set
				{
					// do nothing
				}
			}

			public override ToolStripItemDisplayStyle DisplayStyle
			{
				get
				{
					return _owner.DisplayStyle;
				}
				set
				{
					// do nothing
				}
			}

			public override Padding Padding
			{
				get
				{
					return _owner.Padding;
				}
				set
				{
					// do nothing
				}
			}

			public override ToolStripTextDirection TextDirection
			{
				get
				{
					return _owner.TextDirection;
				}
			}

			public override Image? Image
			{
				get
				{
					if ((_owner.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image)
					{
						return _owner.Image;
					}
					else
					{
						return null;
					}
				}
				set
				{
					// do nothing
				}
			}

			public override bool Selected
			{
				get
				{
					if (_owner is not null)
					{
						return _owner.Selected;
					}

					return base.Selected;
				}
			}

			public override string? Text
			{
				get
				{
					if ((_owner.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
					{
						return _owner.Text;
					}
					else
					{
						return null;
					}
				}
				set
				{
					// do nothing
				}
			}
		}
	}
}
