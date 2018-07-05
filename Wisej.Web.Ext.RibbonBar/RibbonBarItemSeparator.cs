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
using System.Drawing;
using System.ComponentModel;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a vertical separator in a <see cref="RibbonBarGroup"/>.
	/// </summary>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	public class RibbonBarItemSeparator : RibbonBarItem
	{

		#region Events

		#region Events

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		public new event EventHandler Click
		{
			add { AddHandler(nameof(Click), value); }
			remove { RemoveHandler(nameof(Click), value); }
		}

		/// <summary>
		/// This method is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal override void OnClick(EventArgs e)
		{
		}

		#endregion

		#endregion

		#region Properties

		/// <summary>
		/// This property doesn't apply.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool ColumnBreak
		{
			get { return true; }
			set { }
		}

		/// <summary>
		/// This property doesn't apply.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get { return string.Empty; }
			set { }
		}

		/// <summary>
		/// This property doesn't apply.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ToolTipText
		{
			get { return string.Empty; }
			set { }
		}

		/// <summary>
		/// This property doesn't apply.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Enabled
		{
			get { return true; }
			set { }
		}

		/// <summary>
		/// This property doesn't apply.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image Image
		{
			get { return null; }
			set { }
		}

		/// <summary>
		/// This property doesn't apply.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string ImageSource
		{
			get { return null; }
			set { }
		}

		/// <summary>
		/// This property doesn't apply.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int ImageIndex
		{
			get { return -1; }
			set { }
		}

		/// <summary>
		/// This property doesn't apply.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string ImageKey
		{
			get { return null; }
			set { }
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.Delete("icon");
			config.Delete("label");
			config.Delete("enabled");
			config.Delete("iconSize");
			config.className = "wisej.web.ribbonBar.ItemSeparator";
		}

		#endregion

	}
}