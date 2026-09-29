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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Wisej.Core;
using Wisej.Base;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a group of buttons in a <see cref="RibbonBarGroup"/>
	/// organized horizontally.
	/// </summary>
	/// <remarks>
	/// The button group itself doesn't display a text or an image and doesn't fire the click events: each
	/// <see cref="RibbonBarItemButton"/> in <see cref="Buttons"/> fires its own <see cref="RibbonBarItem.Click"/>
	/// event and the <see cref="RibbonBar.ItemClick"/> event.
	/// </remarks>
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[ApiCategory("RibbonBar")]
	public class RibbonBarItemButtonGroup : RibbonBarItem
	{
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

		#region Properties

		#region Not Relevant

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

		/// <summary>
		/// Returns the collection of <see cref="RibbonBarItemButton"/> children.
		/// </summary>
		/// <remarks>
		/// Buttons added to the collection are changed to <see cref="Orientation.Horizontal"/> and their
		/// <see cref="RibbonBarItem.Parent"/> is set to the <see cref="RibbonBarGroup"/> that contains the button group.
		/// Add the button group to its <see cref="RibbonBarGroup"/> before adding the buttons, otherwise the buttons
		/// have no parent and their click events are not fired.
		/// </remarks>
		/// <example>
		/// Creating a row of alignment buttons:
		/// <code><![CDATA[
		/// var alignment = new RibbonBarItemButtonGroup();
		/// this.ribbonBarGroupParagraph.Items.Add(alignment);
		///
		/// alignment.Buttons.Add(new RibbonBarItemButton { Name = "alignLeft", ImageSource = "icon-left", ToolTipText = "Align Left" });
		/// alignment.Buttons.Add(new RibbonBarItemButton { Name = "alignCenter", ImageSource = "icon-center", ToolTipText = "Center" });
		/// alignment.Buttons.Add(new RibbonBarItemButton { Name = "alignRight", ImageSource = "icon-right", ToolTipText = "Align Right" });
		/// ]]></code>
		/// </example>
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Returns the collection of RibbonBarItemButton children.")]
		public ButtonCollection Buttons
		{
			get
			{
				if (this._buttons == null)
					this._buttons = new ButtonCollection(this);

				return this._buttons;
			}
		}
		private ButtonCollection _buttons;

		#endregion

		#region Methods

		/// <summary>
		/// Disposes of the resources (other than memory) used by the <see cref="RibbonBarPage" />.
		/// </summary>
		/// <param name="disposing">true when this method is called by the application rather than a finalizer.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._buttons != null)
				{
					this._buttons.Clear(true);
					this._buttons = null;
				}
			}
			base.Dispose(disposing);
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Returns a collection of referenced components or collection of components.
		/// </summary>
		///<param name="items"></param>
		protected override void OnAddReferences(IList items)
		{
			base.OnAddReferences(items);

			items.Add(this.Buttons);
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);
			IWisejComponent me = this;

			config.className = "wisej.web.ribbonBar.ItemButtonGroup";

			if (me.DesignMode)
			{
				if (me.IsNew || me.IsDirty)
					config.buttons = this.Buttons.Render();
			}
			else
			{
				if (me.IsNew || this.Buttons.IsDirty)
					config.buttons = this.Buttons.Render();
			}
		}

		#endregion

		#region Button Collection

		/// <summary>
		/// Represents a collection of <see cref="RibbonBarItemButton"/> in a <see cref="RibbonBarItemButtonGroup"/>.
		/// </summary>
		public class ButtonCollection : RibbonBarCollectionBase<RibbonBarItemButtonGroup, RibbonBarItemButton>
		{
			internal ButtonCollection(RibbonBarItemButtonGroup owner) : base(owner)
			{
			}

			/// <summary>
			/// Adds the specified <paramref name="item"/> to the collection.
			/// </summary>
			/// <param name="item">The <see cref="RibbonBarItemButton"/> to add to the collection.</param>
			/// <remarks>
			/// The <see cref="RibbonBarItemButton.Orientation"/> of the <paramref name="item"/> is changed to <see cref="Orientation.Horizontal"/>
			/// and its <see cref="RibbonBarItem.Parent"/> is set to the <see cref="RibbonBarGroup"/> that contains the button group.
			/// </remarks>
			/// <example>
			/// Adding a button to a button group:
			/// <code><![CDATA[
			/// this.buttonGroupAlign.Buttons.Add(new RibbonBarItemButton { Name = "justify", ImageSource = "icon-justify" });
			/// ]]></code>
			/// </example>
			public override void Add(RibbonBarItemButton item)
			{
				item.Parent = this.Owner.Parent;
				item.Orientation = Orientation.Horizontal;
				base.Add(item);
			}

			/// <summary>
			/// Inserts the specified <paramref name="item"/> in the collection at the
			/// specified <paramref name="index"/>.
			/// </summary>
			/// <param name="index">The position to insert the specified <see cref="RibbonBarItemButton"/> at.</param>
			/// <param name="item">The <see cref="RibbonBarItemButton"/> to insert in the collection.</param>
			/// <remarks>
			/// The <see cref="RibbonBarItemButton.Orientation"/> of the <paramref name="item"/> is changed to <see cref="Orientation.Horizontal"/>
			/// and its <see cref="RibbonBarItem.Parent"/> is set to the <see cref="RibbonBarGroup"/> that contains the button group.
			/// </remarks>
			/// <example>
			/// Inserting a button as the first button of a button group:
			/// <code><![CDATA[
			/// this.buttonGroupAlign.Buttons.Insert(0, new RibbonBarItemButton { Name = "alignLeft", ImageSource = "icon-left" });
			/// ]]></code>
			/// </example>
			public override void Insert(int index, RibbonBarItemButton item)
			{
				item.Parent = this.Owner.Parent;
				item.Orientation = Orientation.Horizontal;
				base.Insert(index, item);
			}
		}

		#endregion
	}
}