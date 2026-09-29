///////////////////////////////////////////////////////////////////////////////
//
// (C) 2020 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Wisej.Core;

namespace Wisej.Web.Ext.PullToRefresh
{
	/// <summary>
	/// Adds pull-to-refresh functionality to one or more <see cref="ScrollableControl"/> targets.
	/// </summary>
	/// <remarks>
	/// When the user drags a registered control down while it is scrolled to the top, a loader panel
	/// (see <see cref="ImageSource"/>, <see cref="BackColor"/> and <see cref="DropDownHeight"/>) slides in above
	/// the content and, when the pointer is released, the <see cref="Refresh"/> event is fired on the server
	/// with the refreshed control as the <c>sender</c>.
	/// Use <see cref="SetPullToRefresh"/> to enable the functionality for each control.
	/// </remarks>
	/// <example>
	/// Enabling pull-to-refresh on a panel and reloading its data:
	/// <code><![CDATA[
	/// public partial class Page1 : Page
	/// {
	///     private PullToRefresh pullToRefresh1 = new PullToRefresh();
	///
	///     public Page1()
	///     {
	///         InitializeComponent();
	///
	///         this.pullToRefresh1.SetPullToRefresh(this.panel1, true);
	///         this.pullToRefresh1.Refresh += this.pullToRefresh1_Refresh;
	///     }
	///
	///     private void pullToRefresh1_Refresh(object sender, EventArgs e)
	///     {
	///         var panel = (Panel)sender;
	///         LoadNews(panel);
	///     }
	/// }
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[Description("Adds pull to refresh functionality to any ScrollableContainer.")]
	[ApiCategory("PullToRefresh")]
	public class PullToRefresh : Component, IExtenderProvider
	{

		// collection of controls with the related pull to refresh value.
		private Dictionary<ScrollableControl, PullToRefreshItem> items;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PullToRefresh" /> class.
		/// </summary>
		public PullToRefresh()
		{
			this.items = new Dictionary<ScrollableControl, PullToRefreshItem>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PullToRefresh" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="IContainer" /> that represents the container for the component.</param>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
		public PullToRefresh(IContainer container)
			: this()
		{
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when the user refreshes the component.
		/// </summary>
		[Description("Fired when the user refreshes the component.")]
		public event EventHandler Refresh
		{
			add { base.AddHandler(nameof(Refresh), value); }
			remove { base.RemoveHandler(nameof(Refresh), value); }
		}

		/// <summary>
		/// Fires the Click event.
		/// </summary>
		/// <param name="sender">A <see cref="object" />The control that triggered the refresh.</param>
		/// <param name="e">A <see cref="EventArgs" />The event data.</param>
		protected virtual void OnRefresh(object sender, EventArgs e)
		{
			((EventHandler)base.Events[nameof(Refresh)])?.Invoke(sender, e);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Removes the pull-to-refresh functionality from all the registered controls.
		/// </summary>
		/// <remarks>
		/// All the controls previously registered with <see cref="SetPullToRefresh"/> are removed from the extender
		/// and the client loaders are removed. To enable the functionality again call <see cref="SetPullToRefresh"/>.
		/// </remarks>
		/// <example>
		/// Removing pull-to-refresh from all the registered controls:
		/// <code><![CDATA[
		/// private void checkBoxEnableRefresh_CheckedChanged(object sender, EventArgs e)
		/// {
		///     if (this.checkBoxEnableRefresh.Checked)
		///         this.pullToRefresh1.SetPullToRefresh(this.panel1, true);
		///     else
		///         this.pullToRefresh1.Clear();
		/// }
		/// ]]></code>
		/// </example>
		public void Clear()
		{
			lock (this.items)
			{
				this.items.ToList().ForEach((o) => {
					o.Key.Disposed -= this.Control_Disposed;
					o.Key.ControlCreated -= this.Control_Created;
				});

				this.items.Clear();

				Update();
			}
		}

		private void Control_Disposed(object sender, EventArgs e)
		{
			var control = (ScrollableControl)sender;
			control.Disposed -= this.Control_Disposed;
			control.ControlCreated -= this.Control_Created;

			// remove the extender values associated with the disposed control.
			lock (this.items)
			{
				this.items.Remove(control);
			}
		}

		private void Control_Created(object sender, EventArgs e)
		{
			// handle the delayed registration of this extender for a control
			// that was not created (not visible) when the extender tried to register it.
			Control control = (Control)sender;
			control.ControlCreated -= this.Control_Created;

			// update the extender, now it will send also this newly created control.
			Update();
		}

		/// <summary>
		/// Returns whether pull-to-refresh is enabled for the given scrollable control.
		/// </summary>
		/// <param name="control">The <see cref="ScrollableControl"/> to check.</param>
		/// <returns><c>true</c> if pull-to-refresh is enabled for <paramref name="control"/>; otherwise <c>false</c>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="control"/> is null.</exception>
		/// <example>
		/// Toggling pull-to-refresh on a panel:
		/// <code><![CDATA[
		/// bool enabled = this.pullToRefresh1.GetPullToRefresh(this.panel1);
		/// this.pullToRefresh1.SetPullToRefresh(this.panel1, !enabled);
		/// ]]></code>
		/// </example>
		public bool GetPullToRefresh(ScrollableControl control)
		{
			return GetContainer(control).Enabled;
		}

		/// <summary>
		/// Sets whether pull-to-refresh is enabled for the given scrollable control.
		/// </summary>
		/// <param name="control">The <see cref="ScrollableControl"/> (i.e. a <see cref="Panel"/> or <see cref="FlowLayoutPanel"/>) to extend.</param>
		/// <param name="enabled"><c>true</c> to enable pull-to-refresh on <paramref name="control"/>; <c>false</c> to disable it.</param>
		/// <exception cref="ArgumentNullException"><paramref name="control"/> is null.</exception>
		/// <remarks>
		/// The pull gesture is recognized only when <paramref name="control"/> is scrolled to the top.
		/// If the control has not been created yet, it is registered on the client as soon as it is created.
		/// Handle the <see cref="Refresh"/> event to reload the content; the event's <c>sender</c> is the refreshed control.
		/// </remarks>
		/// <example>
		/// Enabling pull-to-refresh on a list panel:
		/// <code><![CDATA[
		/// this.pullToRefresh1.SetPullToRefresh(this.flowLayoutPanel1, true);
		/// this.pullToRefresh1.Refresh += (s, e) => ReloadItems((FlowLayoutPanel)s);
		/// ]]></code>
		/// </example>
		public void SetPullToRefresh(ScrollableControl control, bool enabled)
		{
			GetContainer(control).Enabled = enabled;

			Update();
		}

		/// <summary>
		/// Creates or retrieves the extender data entry associated with the control.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		private PullToRefreshItem GetContainer(ScrollableControl control)
		{
			if (control == null)
				throw new ArgumentNullException("control");

			lock (this.items)
			{
				PullToRefreshItem item = null;
				if (!this.items.TryGetValue(control, out item))
				{
					item = new PullToRefreshItem() { Widget = control };
					this.items[control] = item;

					// remove the control from the extender when it's disposed.
					control.Disposed -= this.Control_Disposed;
					control.Disposed += this.Control_Disposed;

				}
				return item;
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the background color of the loader panel displayed while pulling.
		/// </summary>
		/// <remarks>
		/// The default is <see cref="Color.Transparent"/>.
		/// </remarks>
		[Description("Gets or sets the background color of the refresh component.")]
		public Color BackColor
		{
			get
			{
				return this._backColor;
			}
			set
			{
				if (this._backColor != value)
				{
					this._backColor = value;

					Update();
				}
			}
		}
		private Color _backColor = Color.Transparent;

		/// <summary>
		/// Returns or sets the height in pixels of the loader panel displayed while pulling.
		/// </summary>
		/// <remarks>
		/// This value is also the maximum distance the content can be dragged down. The loader image is
		/// scaled to <c>DropDownHeight - 20</c> pixels. Changing this value recreates the loaders on the client.
		/// </remarks>
		/// <example>
		/// Using a taller loader panel:
		/// <code><![CDATA[
		/// this.pullToRefresh1.DropDownHeight = 80; // loader image is 60px
		/// ]]></code>
		/// </example>
		[DefaultValue(50)]
		[Description("Gets or sets the refresh drop down height in pixels.")]
		public int DropDownHeight
		{
			get
			{
				return this._dropDownHeight;
			}
			set
			{
				if (this._dropDownHeight != value)
				{
					this._dropDownHeight = value;

					Update();
				}
			}
		}
		private int _dropDownHeight = 50;

		/// <summary>
		/// Returns or sets the loader image displayed in the loader panel while pulling.
		/// </summary>
		/// <remarks>
		/// The value can be a URL, an application-relative path, or a theme image name. The image is
		/// centered in the loader panel and not animated by the extender; use an animated SVG or GIF
		/// for a spinning effect. The default is <c>"resource.wx/loader.svg"</c>.
		/// </remarks>
		/// <example>
		/// Using a custom animated image from the application folder:
		/// <code><![CDATA[
		/// this.pullToRefresh1.ImageSource = "Images/spinner.gif";
		/// ]]></code>
		/// </example>
		[DefaultValue("resource.wx/loader.svg")]
		[Description("Gets or sets the loader image to use for the pull to refresh component.")]
		[TypeConverter("Wisej.Web.ImageSourceConverter, Wisej.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171")]
		[Editor("Wisej.Design.ImageSourceEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ImageSource
		{
			get
			{
				return this._imageSource;
			}
			set
			{
				if (this._imageSource != value)
				{
					this._imageSource = value;

					Update();
				}
			}
		}
		private string _imageSource = "resource.wx/loader.svg";

		#endregion

		#region IExtenderProvider Implementation

		bool IExtenderProvider.CanExtend(object extendee)
		{
			return extendee is Control;
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{

				case "refresh":
					{
						Control control = e.Parameters.Control;
						if (control != null)
							OnRefresh(control, EventArgs.Empty);
					}
					break;


				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ext.PullToRefresh";

			// TODO: handle theme colors.
			config.backColor = this._backColor;
			config.imageSource = this._imageSource;
			config.dropDownHeight = this._dropDownHeight;
			
			lock (this.items)
			{
				if (this.items.Count > 0)
				{
					var list = new List<object>();
					foreach (var entry in this.items)
					{
						var control = entry.Key;
						var settings = entry.Value;

						// skip controls that are not yet created.
						if (!control.Created || !settings.Enabled)
							continue;

						list.Add(new {
							id = ((IWisejComponent)control).Id,
						});
					}

					config.scrollContainers = list.ToArray();

					// register non-created control for delayed registration.
					this.items.Where(o => !o.Key.Created).ToList().ForEach(o => o.Key.ControlCreated += this.Control_Created);
				}
				else
				{
					config.scrollContainers = null;
				}

				// subscribe only if the event has been attached to since
				// it's unlikely that this class will be extended to
				// override OnRefresh.
				if (base.Events[nameof(Refresh)] != null)
				{
					config.wiredEvents = new Base.WiredEvents();
					config.wiredEvents.Add("refresh(Control)");
				}
			}
		}

		#endregion

		#region PullToRefreshItem

		/// <summary>
		/// Represents a refresh component.
		/// </summary>
		private class PullToRefreshItem
		{
			public bool Enabled;

			public Control Widget;
		}

		#endregion
	}
}
