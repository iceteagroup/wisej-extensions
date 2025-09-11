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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Web.Ext.ToolStrip
{
	public partial class ToolStripContainer
	{
		internal class ToolStripContainerTypedControlCollection : Control.ControlCollection
		{
			private readonly ToolStripContainer _owner;
			private readonly Type _contentPanelType = typeof(ToolStripContentPanel);
			private readonly Type _panelType = typeof(ToolStripPanel);

			public ToolStripContainerTypedControlCollection(ToolStripContainer owner) : base(owner)
			{
				_owner = owner;
			}

			public override void Add(Control value)
			{
				ArgumentNullException.ThrowIfNull(value);

				if (IsReadOnly)
				{
					throw new NotSupportedException(/*SR.ToolStripContainerUseContentPanel*/);
				}

				Type controlType = value.GetType();
				if (!_contentPanelType.IsAssignableFrom(controlType) && !_panelType.IsAssignableFrom(controlType))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, string.Format(SR.TypedControlCollectionShouldBeOfTypes, _contentPanelType.Name, _panelType.Name)), value.GetType().Name);
				}

				base.Add(value);
			}

			public override void Remove(Control? value)
			{
				if (value is ToolStripPanel || value is ToolStripContentPanel)
				{
					if (!_owner.DesignMode)
					{
						if (IsReadOnly)
						{
							throw new NotSupportedException(SR.ReadonlyControlsCollection);
						}
					}
				}

				base.Remove(value);
			}

			internal void SetChildIndexInternal(Control child, int newIndex)
			{
				if (child is ToolStripPanel || child is ToolStripContentPanel)
				{
					if (!_owner.DesignMode)
					{
						if (IsReadOnly)
						{
							throw new NotSupportedException(SR.ReadonlyControlsCollection);
						}
					}
					else
					{
						// just no-op it at DT.
						return;
					}
				}
			}
		}
	}
}
