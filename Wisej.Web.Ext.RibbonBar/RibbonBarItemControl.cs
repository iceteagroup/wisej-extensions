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
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	public class RibbonBarItemControl : RibbonBarItem
	{
		#region Properties

		/// <summary>
		/// Returns or sets the <see cref="Control"/> to be hosted inside the
		/// <see cref="RibbonBarItemControl"/>.
		/// </summary>
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
						oldControl.Parent = null;
						oldControl.Disposed -= control_Disposed;
						oldControl.SetStyle(ControlStyles.Embedded, false);

						if (this.DesignMode)
						{
							if (!oldControl.IsDisposed && !oldControl.Disposing)
							{
								oldControl.Parent = this.RibbonBar?.Parent;
								oldControl.BringToFront();
								((IWisejComponent)oldControl).Updated -= control_Updated;
							}
						}
					}

					// sanity check.
					if (value != null && value == this.RibbonBar)
						throw new ArgumentException(SR.GetString("CircularOwner"));

					var newControl = this._control = value;

					if (newControl != null)
					{
						newControl.SetStyle(ControlStyles.Embedded, true);

						newControl.Parent = this.RibbonBar;
						newControl.CreateControl();
						newControl.Disposed += control_Disposed;

						if (this.DesignMode)
						{
							// hook up to the IWisejComponent.Updated event to update the UI while designing.
							((IWisejComponent)newControl).Updated += control_Updated;
						}
					}

					Update();
				}
			}
		}
		private Control _control;

		private void control_Updated(object sender, EventArgs e)
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