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
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a user defined control in a <see cref="RibbonBarGroup"/>.
	/// </summary>
	/// <remarks>
	/// The hosted <see cref="Control"/> fills the item and is resized together with it. The <see cref="RibbonBarItem.Text"/>
	/// and image properties of the item are not displayed.
	/// </remarks>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	[ApiCategory("RibbonBar")]
	public class RibbonBarItemControl : RibbonBarItem
	{
		#region Properties

		/// <summary>
		/// Returns or sets the <see cref="Control"/> to be hosted inside the
		/// <see cref="RibbonBarItemControl"/>.
		/// </summary>
		/// <returns>The hosted <see cref="Wisej.Web.Control"/>, or null. The default is null.</returns>
		/// <exception cref="ArgumentException">The value is the <see cref="RibbonBar"/> that contains this item.</exception>
		/// <remarks>
		/// <para>
		/// The control is moved into the <see cref="RibbonBar"/> that contains this item, its <see cref="Wisej.Web.Control.AutoSize"/>
		/// property is set to false and its size follows the size of the item on the client. Assign the control after the item has
		/// been added to a <see cref="RibbonBarGroup"/> that is already part of a <see cref="RibbonBar"/>, otherwise the control
		/// has no parent.
		/// </para><para>
		/// When a different control is assigned, the previous control is moved to the parent of the <see cref="RibbonBar"/>.
		/// When the hosted control is disposed, this property is reset to null; when the item is disposed, the hosted control is disposed as well.
		/// </para>
		/// </remarks>
		/// <example>
		/// Hosting a <see cref="T:Wisej.Web.DateTimePicker"/> in a ribbon group:
		/// <code><![CDATA[
		/// var item = new RibbonBarItemControl { Orientation = Orientation.Horizontal };
		/// this.ribbonBarGroupFilter.Items.Add(item);
		///
		/// item.Control = new DateTimePicker
		/// {
		///     Width = 140,
		///     Format = DateTimePickerFormat.Short
		/// };
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the Control to be hosted inside the RibbonBarItemControl.")]
		public Control Control
		{
			get { return _control; }
			set
			{
				var oldControl = this._control;
				if (oldControl != value)
				{
					if (oldControl != null)
					{
						oldControl.SetStyle(ControlStyles.Embedded, false);
						oldControl.Parent = this.RibbonBar?.Parent;
						oldControl.Disposed -= control_Disposed;

						if (this.DesignMode)
						{
							if (!oldControl.IsDisposed && !oldControl.Disposing)
							{
								oldControl.Visible = true;
								oldControl.BringToFront();
								((IWisejComponent)oldControl).Updated -= Control_Updated;

								Update();
							}
						}
					}

					// sanity check.
					if (value != null && value == this.RibbonBar)
						throw new ArgumentException(SR.GetString("CircularOwner"));

					var newControl = this._control = value;

					if (newControl != null)
					{
						newControl.Parent = null;
						newControl.SetStyle(ControlStyles.Embedded, true);
						newControl.Parent = this.RibbonBar;

						newControl.CreateControl();
						newControl.AutoSize = false;
						newControl.Disposed += control_Disposed;

						if (this.DesignMode)
						{
							newControl.Visible = false;

							// hook up to the IWisejComponent.Updated event to update the UI while designing.
							((IWisejComponent)newControl).Updated += Control_Updated;
						}
					}

					Update();
				}
			}
		}
		private Control _control;

		private void Control_Updated(object sender, EventArgs e)
		{
			Update();
		}

		void control_Disposed(object sender, EventArgs e)
		{
			this.Control = null;
		}

		/// <summary>
		/// Returns or sets the layout orientation of the <see cref="RibbonBarItemControl"/>.
		/// </summary>
		/// <returns>One of the <see cref="Orientation"/> values. The default is <see cref="Orientation.Vertical"/>.</returns>
		/// <remarks>
		/// <see cref="Orientation.Vertical"/> makes the item fill the height of the group in a column of its own.
		/// <see cref="Orientation.Horizontal"/> uses the height of the hosted control and stacks the item with the other small
		/// items in the same column until an item has <see cref="ColumnBreak"/> set to true.
		/// </remarks>
		[DefaultValue(Orientation.Vertical)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the orientation of the RibbonBarItemButton.")]
		public Orientation Orientation
		{
			get { return this._orientation; }
			set
			{
				if (this._orientation != value)
				{
					this._orientation = value;
					Update();
				}
			}
		}
		private Orientation _orientation = Orientation.Vertical;

		/// <summary>
		/// Returns or sets a value indicating whether a new column starts after
		/// this <see cref="RibbonBarItem"/>.
		/// </summary>
		/// <returns>true if the next item in the <see cref="RibbonBarGroup"/> is placed in a new column; otherwise, false.</returns>
		/// <remarks>
		/// Always returns true when <see cref="Orientation"/> is <see cref="Orientation.Vertical"/>. The value set is used only
		/// when <see cref="Orientation"/> is <see cref="Orientation.Horizontal"/>.
		/// </remarks>
		public override bool ColumnBreak
		{
			get { return base.ColumnBreak || this.Orientation == Orientation.Vertical; }
			set { base.ColumnBreak = value; }
		}

		private bool ShouldSerializeColumnBreak()
		{
			return base.ColumnBreak && this.Orientation == Orientation.Horizontal;
		}

		private void ResetColumnBreak()
		{
			base.ColumnBreak = false;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Disposes the page and related resources.
		/// </summary>
		/// <param name="disposing">true when this method is called by the application rather than a finalizer.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._control != null)
				{
					this._control.Disposed -= control_Disposed;
					this._control.Dispose();
					this._control = null;
				}
			}

			base.Dispose(disposing);
		}

		#endregion

		#region Wisej Implementation

		// Handles "controlResize" events from the client widget.
		private void ProcessControlResizeWebEvent(WisejEventArgs e)
		{
			dynamic size = e.Parameters.Size;
			if (size != null && this._control != null)
			{
				this._control.Size = new Size(
					Convert.ToInt32(size.width),
					Convert.ToInt32(size.height));
			}
		}

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(Core.WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "controlResize":
					ProcessControlResizeWebEvent(e);
					break;

				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Adds references components to the list. Referenced components
		/// can be added individually or as a reference to a collection.
		/// </summary>
		/// <param name="items">Container for the referenced components or collections.</param>
		protected override void OnAddReferences(IList items)
		{
			base.OnAddReferences(items);

			if (this._control != null)
				items.Add(this._control);
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);
			IWisejComponent me = this;

			config.className = "wisej.web.ribbonBar.ItemControl";
			config.orientation = this.Orientation;

			if (me.DesignMode)
			{
				if (this.Control != null)
				{
					dynamic controlConfig = new DynamicObject();
					((IWisejComponent)this.Control).Render(controlConfig);
					config.control = controlConfig;
				}
			}
			else
			{
				config.control = ((IWisejControl)this.Control)?.Id;
				config.wiredEvents.Add("controlResize(Size)");
			}
		}

		#endregion

	}
}