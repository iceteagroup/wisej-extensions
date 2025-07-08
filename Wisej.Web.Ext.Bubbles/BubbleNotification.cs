///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.Bubbles
{
	/// <summary>
	/// Represents a numeric notification bubble that can be displayed next to any control.
	/// </summary>
	/// <remarks>
	/// Since version 3.2.5 it supports <see cref="ToolBarButton"/> components.
	/// </remarks>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(BubbleNotification))]
	[ProvideProperty("BubbleValue", typeof(Control))]
	[ProvideProperty("BubbleStyle", typeof(Control))]
	[ProvideProperty("BubbleValue", typeof(ToolBarButton))]
	[ProvideProperty("BubbleStyle", typeof(ToolBarButton))]
	[Description("Represents a numeric notification bubble that can be displayed next to any control.")]
	[ApiCategory("Bubbles")]
	public class BubbleNotification : Wisej.Web.Component, IExtenderProvider
	{
		// collection of controls with the related bubble notification value.
		private Dictionary<IWisejComponent, Bubble> bubbles;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification" /> without a specified container.
		/// </summary>
		public BubbleNotification()
		{
			this.bubbles = new Dictionary<IWisejComponent, Bubble>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification" /> class with a specified container.
		/// </summary>
		/// <param name="container">An <see cref="System.ComponentModel.IContainer" />container. </param>
		public BubbleNotification(IContainer container)
			: this()
		{
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when the user clicks on a bubble notification.
		/// </summary>
		public event BubbleEventHandler Click
		{
			add { base.AddHandler(nameof(Click), value); }
			remove { base.RemoveHandler(nameof(Click), value); }
		}

		/// <summary>
		/// Fires the Click event.
		/// </summary>
		/// <param name="e">A <see cref="Wisej.Web.Ext.Bubbles.BubbleEventArgs" /> that contains the event data. </param>
		protected virtual void OnClick(BubbleEventArgs e)
		{
			((BubbleEventHandler)base.Events[nameof(Click)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Specifies the alignment of the bubble notification.
		/// </summary>
		[SRCategory("CatLayout")]
		[DefaultValue(ContentAlignment.TopRight)]
		[Description("Specifies the alignment of the bubble notification.")]
		public ContentAlignment Alignment
		{
			get { return this._alignment; }
			set
			{
				this._alignment = value;
				Update();
			}
		}
		private ContentAlignment _alignment = ContentAlignment.TopRight;

		/// <summary>
		/// Specifies the offset of the bubble notification.
		/// </summary>
		[Localizable(true)]
		[SRCategory("CatLayout")]
		[Description("Specifies the offset of the bubble notification.")]
		public Padding Margin
		{
			get { return this._margin; }
			set
			{
				this._margin = value;
				Update();
			}
		}
		private Padding _margin = new Padding(0);

		/// <summary>
		/// Returns the default value for the <see cref="Margin"/> property.
		/// </summary>
		/// <returns>A <see cref="Padding" /> that represents the default space between controls.</returns>
		protected virtual Padding DefaultMargin
		{
			get { return _defaultMargin; }
		}
		private static Padding _defaultMargin = new Padding(0);

		private bool ShouldSerializeMargin()
		{
			return this.Margin != this.DefaultMargin;
		}

		private void ResetMargin()
		{
			this.Margin = this.DefaultMargin;
		}

		/// <summary>
		/// Returns or sets the object that contains programmer-supplied data associated with this component.
		/// </summary>
		/// <returns>An <see cref="System.Object" /> that contains user data. The default is null.</returns>
		[DefaultValue(null)]
		[Localizable(false)]
		[SRCategory("CatData")]
		[SRDescription("ControlTagDescr")]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get { return this._tag; }
			set { this._tag = value; }
		}
		private object _tag;

		#endregion

		#region Methods

		/// <summary>
		/// Returns true if <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification" /> can offer an extender property to the specified target component.
		/// </summary>
		/// <returns>true if the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification" /> class can offer one or more extender properties; otherwise, false.</returns>
		/// <param name="target">The target object to add an extender property to. </param>
		public bool CanExtend(object target)
		{
			return (target is Control || target is ToolBarButton);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Clear();
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Removes all bubbles.
		/// </summary>
		public void Clear()
		{
			lock (this.bubbles)
			{
				this.bubbles.ToList().ForEach((o) =>
				{
					if (o.Key is Control control)
					{
						control.Disposed -= this.Component_Disposed;
						control.ControlCreated -= this.Control_Created;
					}
					else if (o.Key is ToolBarButton button)
					{
						button.Disposed -= this.Component_Disposed;
					}
				});

				this.bubbles.Clear();

				Update();
			}
		}

		/// <summary>
		/// Returns the notification value associated with the specified control.
		/// </summary>
		/// <returns>A <see cref="System.Int32" /> containing the value to display in the bubble; when 0, the bubble is hidden.</returns>
		/// <param name="component">The <see cref="IWisejComponent" /> for which to retrieve the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification" /> value. </param>
		[DefaultValue(0)]
		[DisplayName("BubbleValue")]
		[Description("Gets or sets the notification value associated with the specified control.")]
		public int GetBubbleValue(IWisejComponent component)
		{
			if (!HasBubbleEntry(component))
				return 0;

			return GetBubble(component).Value;
		}

		/// <summary>
		/// Shows the bubble notification value with the specified control.
		/// </summary>
		/// <param name="component">The <see cref="IWisejComponent" /> to associate the JavaScript with. </param>
		/// <param name="value">The value to show in the bubble notification; 0 hides the bubble.</param>
		public void SetBubbleValue(IWisejComponent component, int value)
		{
			GetBubble(component).Value = value;
			Update();
		}

		/// <summary>
		/// Returns the notification value associated with the specified control.
		/// </summary>
		/// <returns>A <see cref="System.Int32" /> containing the value to display in the bubble; when 0, the bubble is hidden.</returns>
		/// <param name="component">The <see cref="IWisejComponent" /> for which to retrieve the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification" /> value. </param>
		[DefaultValue(BubbleStyle.Alert)]
		[DisplayName("BubbleStyle")]
		[Description("Gets or sets the notification style associated with the specified control.")]
		public BubbleStyle GetBubbleStyle(IWisejComponent component)
		{
			if (!HasBubbleEntry(component))
				return BubbleStyle.Alert;

			return GetBubble(component).Style;
		}

		/// <summary>
		/// Shows the bubble notification value with the specified control.
		/// </summary>
		/// <param name="component">The <see cref="IWisejComponent" /> to associate the JavaScript with. </param>
		/// <param name="style">The value to show in the bubble notification; 0 hides the bubble.</param>
		public void SetBubbleStyle(IWisejComponent component, BubbleStyle style)
		{
			GetBubble(component).Style = style;
			Update();
		}

		/// <summary>
		/// Returns if the control is associated to the extender.
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		private bool HasBubbleEntry(IWisejComponent component)
		{
			if (component == null)
				throw new ArgumentNullException(nameof(component));

			lock (this.bubbles)
			{
				return this.bubbles.ContainsKey(component);
			}

		}

		/// <summary>
		/// Creates or retrieves the extender data entry associated with the control.
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		private Bubble GetBubble(IWisejComponent component)
		{
			if (component == null)
				throw new ArgumentNullException(nameof(component));

			if (!(component is Control || component is ToolBarButton))
				throw new ArgumentNullException(nameof(component), "Invalid control type.");

			lock (this.bubbles)
			{
				Bubble bubble = null;
				if (!this.bubbles.TryGetValue(component, out bubble))
				{
					bubble = new Bubble() { Widget = component };
					this.bubbles[component] = bubble;

					// remove the control from the extender when it's disposed.
					if (component is Control control)
					{
						control.Disposed -= this.Component_Disposed;
						control.Disposed += this.Component_Disposed;
					}
					else if (component is ToolBarButton button)
					{
						button.Disposed -= this.Component_Disposed;
						button.Disposed += this.Component_Disposed;
					}
				}
				return bubble;
			}
		}

		private void Component_Disposed(object sender, EventArgs e)
		{
			if (sender is Control control)
			{
				control.Disposed -= this.Component_Disposed;
				control.ControlCreated -= this.Control_Created;
			}
			else if (sender is ToolBarButton button)
			{
				button.Disposed -= this.Component_Disposed;
			}

			// remove the extender values associated with the disposed control.
			lock (this.bubbles)
			{
				this.bubbles.Remove((IWisejComponent)sender);
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
		/// Returns a string representation for this control.
		/// </summary>
		/// <returns>A <see cref="System.String" /> containing a description of the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification" />.</returns>
		public override string ToString()
		{
			return base.ToString();
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

				case "bubbleClick":
					{
						Control control = e.Parameters.Control;
						if (control != null)
							OnClick(new BubbleEventArgs(control, GetBubbleValue(control)));
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

			config.className = "wisej.web.extender.bubbles.BubbleNotifications";

			lock (this.bubbles)
			{
				if (this.bubbles.Count > 0)
				{
					List<object> list = new List<object>();
					foreach (var entry in this.bubbles)
					{
						var settings = entry.Value;

						if (entry.Key is Control control)
						{
							// skip controls that are not yet created.
							if (!control.Created)
							{
								control.ControlCreated += this.Control_Created;
								continue;
							}
						}

						if (settings.Value > 0)
						{
							list.Add(new
							{
								id = entry.Key.Id,
								value = settings.Value,
								style = settings.Style
							});
						}
					}

					config.margin = this._margin;
					config.alignment = this._alignment;
					config.bubbles = list.ToArray();
				}
				else
				{
					config.bubbles = null;
				}

				// subscribe only if the event has been attached to since
				// it's unlikely that this class will be extended to
				// override OnClick.
				if (base.Events[nameof(Click)] != null)
				{
					config.wiredEvents = new WiredEvents();
					config.wiredEvents.Add("bubbleClick(Control)");
				}
			}
		}

		#endregion

		#region Bubble

		/// <summary>
		/// Represents a bubble component.
		/// </summary>
		private class Bubble
		{
			public int Value;
			public IWisejComponent Widget;
			public BubbleStyle Style;
		}

		#endregion

	}
}
