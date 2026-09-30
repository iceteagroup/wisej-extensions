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
    /// Represents a numeric notification bubble that can be displayed next to any control in the Wisej framework.
    /// </summary>
    /// <remarks>
    /// This class provides functionality to display numeric notifications, enhancing user interaction by informing users of important updates or statuses associated with a specific control.
	/// Since version 3.2.5, it includes support for <see cref="ToolBarButton"/> components, allowing for greater flexibility in user interface design.
    /// </remarks>
	/// /// <example>
	/// The following example demonstrates how to create and display a BubbleNotification next to a Button control:
	/// <code><![CDATA[
	/// var button = new Button("Click Me");
	/// var bubble = new BubbleNotification();
	/// bubble.SetBubbleValue(button, 7);
	/// this.Controls.Add(button);
	/// ]]></code>
	/// </example>
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
        /// Raises the Click event for the Bubble Notification.
        /// </summary>
        /// <param name="e">A <see cref="Wisej.Web.Ext.Bubbles.BubbleEventArgs" /> that contains the event data. </param>
        protected virtual void OnClick(BubbleEventArgs e)
		{
			((BubbleEventHandler)base.Events[nameof(Click)])?.Invoke(this, e);
		}

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the alignment of the bubble notification.
        /// </summary>
        /// <value>
        /// A <see cref="ContentAlignment"/> value that specifies how the bubble notification is aligned.
        /// Possible values include <see cref="ContentAlignment.TopLeft"/>, <see cref="ContentAlignment.TopRight"/>,
        /// <see cref="ContentAlignment.BottomLeft"/>, <see cref="ContentAlignment.BottomRight"/>,
        /// <see cref="ContentAlignment.MiddleLeft"/>, <see cref="ContentAlignment.MiddleRight"/>,
        /// <see cref="ContentAlignment.MiddleCenter"/>, etc.
        /// </value>
        /// <remarks>
        /// The alignment determines the position of the bubble notification relative to its anchor.
        /// Ensure that the chosen alignment fits within the available space of the UI element.
        /// </remarks>
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
        /// Gets or sets the padding margin applied to the bubble notification.
        /// This property defines the space between the content of the bubble and its border.
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
        /// /// <remarks>
        /// The default margin specifies the space between the notification bubble's content and its border,
        /// ensuring appropriate separation for visual clarity.
        /// </remarks>
        /// <returns>A <see cref="Padding" /> that represents the default space between controls.</returns>
        protected virtual Padding DefaultMargin
		{
			get { return _defaultMargin; }
		}
		private static Padding _defaultMargin = new Padding(0);

        /// <summary>
        /// Determines whether the <see cref="Margin"/> property should be serialized.
        /// </summary>
        /// <returns>
        /// Returns <c>true</c> if the <see cref="Margin"/> property has a relevant value that needs to be serialized; otherwise, <c>false</c>.
        /// </returns>
        private bool ShouldSerializeMargin()
		{
			return this.Margin != this.DefaultMargin;
		}

        /// <summary>
        /// Resets the margin of the BubbleNotification to its default value.
        /// </summary>
        /// <remarks>
        /// This method is useful for reapplying the default styling after custom margin adjustments.
        /// </remarks>
        private void ResetMargin()
		{
			this.Margin = this.DefaultMargin;
		}

        /// <summary>
        /// Gets or sets an object that holds programmer-defined data associated with this <see cref="BubbleNotification"/> component.
        /// </summary>
        /// <value>
		/// An <see cref="System.Object"/> representing the user-defined data. The default value is <c>null</c>.
		/// </value>
		/// <remarks>
		/// Any type derived from <see cref="System.Object"/> can be assigned to this property. Its value is
		/// never read or modified by <see cref="BubbleNotification"/>; it is provided so that applications can
		/// associate arbitrary state with the component without deriving from it.
		/// <para>
		/// At design time the property is edited through a <see cref="StringConverter"/>, so only string values
		/// can be entered in the Properties window. Other object types must be assigned in code.
		/// </para>
		/// <para>
		/// The assigned object is held for the lifetime of the component, so avoid storing values that own
		/// unmanaged or otherwise expensive resources.
		/// </para>
		/// </remarks>
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
        /// Determines whether the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification"/> can provide extender properties
        /// to the specified target component.
        /// </summary>
        /// <returns>true if the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification" /> class can offer one or more extender properties; otherwise, false.</returns>
        /// <param name="target">The target object to add an extender property to. </param>
		/// <remarks>
		/// This method is typically used to verify if the target component is compatible with the extender properties
		/// offered by the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification"/> class.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// bubbleNotification.CanExtend(new Button());         // true  - any Control.
		/// bubbleNotification.CanExtend(new ToolBarButton());  // true  - supported since 3.2.5.
		/// bubbleNotification.CanExtend(new Timer());          // false - neither a Control nor a ToolBarButton.
		///
		/// // guard a loosely typed component before assigning a bubble, since
		/// // SetBubbleValue throws when the component is of an unsupported type.
		/// void SetBubble(object component, int value)
		/// {
		///     if (bubbleNotification.CanExtend(component))
		///         bubbleNotification.SetBubbleValue((IWisejComponent)component, value);
		/// }
		/// ]]></code>
		/// </example>
        public bool CanExtend(object target)
		{
			return (target is Control || target is ToolBarButton);
		}

        /// <summary>
        /// Releases the resources used by the <see cref="BubbleNotification"/> class.
        /// </summary>
        /// <param name="disposing">Indicates whether the method was called directly
        /// or indirectly by a user's code (<c>true</c>) or by the runtime (<c>false</c>).</param>
        /// <remarks>
        /// If <paramref name="disposing"/> is <c>true</c>, the method will release
        /// both managed and unmanaged resources. If <c>false</c>, it will only release
        /// unmanaged resources.
        /// </remarks>
        protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Clear();
			}

			base.Dispose(disposing);
		}

        /// <summary>
        /// Removes all bubble notifications from the user interface.
        /// </summary>
		/// /// <remarks>
		/// This method clears all existing bubble notifications, effectively resetting the notification display.
		/// It is useful for scenarios where you want to refresh the notifications or clear old notifications
		/// before triggering new ones.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// bubble.Clear();
		/// AlertBox.Show("All bubble notifications have been cleared.");
		/// ]]></code>
		/// </example>
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
        /// Returns the notification value associated with the specified Wisej component.
        /// </summary>
        /// <returns>A <see cref="System.Int32" /> containing the value to display in the bubble; when 0, the bubble is hidden.</returns>
        /// <param name="component">The <see cref="IWisejComponent" /> for which to retrieve the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification" /> value. </param>
        /// <remarks>
		/// This method is typically used to update the visual representation of the component's status
		/// in the form of a bubble notification based on its value.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// int bubbleValue = bubble.GetBubbleValue(myComponent);
		/// if (bubbleValue > 0)
		/// {
		///     AlertBox.Show($"Bubble value: {bubbleValue}");
		/// }
		/// else
		/// {
		///     AlertBox.Show("Bubble is hidden.");
		/// }
		/// ]]></code>
		/// </example>
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
        /// Displays a bubble notification with a specified value associated with the given Wisej component.
        /// </summary>
        /// <param name="component">The <see cref="IWisejComponent"/> instance that the bubble notification will be linked to.</param>
        /// <param name="value">The value to show in the bubble notification; Use 0 to hide the bubble notification.</param>
        public void SetBubbleValue(IWisejComponent component, int value)
		{
			GetBubble(component).Value = value;
			Update();
		}

        /// <summary>
        /// Retrieves the <see cref="BubbleStyle"/> associated with the specified Wisej component.
        /// This method returns the bubble style for a given component, which determines the
        /// appearance and behavior of the notification bubble. If the bubble style indicates
        /// no notification (e.g., a default or hidden style), the return value will reflect this
        /// and the notification will not be displayed.
        /// </summary>
        /// <param name="component">The <see cref="IWisejComponent" /> for which to retrieve the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification" /> value. </param>
        /// <returns>
		/// A <see cref="BubbleStyle"/> enum value representing the style of the notification bubble.
		/// If the return value corresponds to a hidden state, no bubble will be shown.
		/// </returns>
		/// <example>
		/// <code><![CDATA[
		/// BubbleStyle style = bubble.GetBubbleStyle(myComponent);
		/// AlertBox.Show($"Current bubble style: {style}");
		/// ]]></code>
		/// </example>
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
        /// Displays a bubble notification for the specified Wisej component using the given bubble style.
        /// </summary>
        /// <param name="component">The <see cref="IWisejComponent"/> to which the bubble notification is associated.</param>
		/// <param name="style">The <see cref="BubbleStyle"/> value that defines the appearance of the bubble notification. A value of 0 will hide the bubble.</param>
        public void SetBubbleStyle(IWisejComponent component, BubbleStyle style)
		{
			GetBubble(component).Style = style;
			Update();
		}

        /// <summary>
        /// Determines whether the specified Wisej component is associated with the BubbleNotification extender.
        /// </summary>
        /// <param name="component">The Wisej component to check for an association with the BubbleNotification extender.</param>
		/// <returns>
		/// <c>true</c> if the specified component is associated with the extender; otherwise, <c>false</c>.
		/// </returns>
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
        /// Retrieves the <see cref="Bubble"/> associated with the specified Wisej component,
        /// creating a new instance if it does not already exist.
        /// </summary>
        /// <param name="component">The Wisej component for which the bubble is to be retrieved or created.</param>
		/// <returns>A <see cref="Bubble"/> instance associated with the specified component.</returns>
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

        /// <summary>
        /// Handles the <see cref="System.ComponentModel.Component.Disposed"/> event.
        /// This event is triggered when the BubbleNotification component is disposed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
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

        /// <summary>
        /// Handles the <see cref="Control.Created"/> event for the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> that contains the event data.</param>
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
        /// Returns a string representation of the current instance of the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification"/> control.
        /// </summary>
        /// <returns>
		/// A <see cref="System.String"/> that provides a detailed description of the <see cref="Wisej.Web.Ext.Bubbles.BubbleNotification"/> instance,
		/// including its current properties and state.
		/// </returns>
		/// <example><![CDATA[
		/// // BubbleNotification bubble = new BubbleNotification();
		/// // string description = bubble.ToString();
		/// // AlertBox.Show(description);
		/// ]]></example>
		public override string ToString()
		{
			return base.ToString();
		}

        #endregion

        #region Wisej Implementation

        /// <summary>
        /// Handles and processes events triggered from the client for the BubbleNotification control.
        /// This method receives the event arguments, enabling appropriate responses to client-side actions.
        /// </summary>
        /// <param name="e">An instance of <see cref="WisejEventArgs"/> containing the event data.</param>
		/// <remarks>
		/// This method is typically invoked in response to user interactions, such as clicks or notifications,
		/// and allows for handling logic specific to the event type.
		/// </remarks>
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
        /// Renders the client-side component using the specified dynamic configuration.
        /// This method updates the BubbleNotification display based on the provided settings.
        /// </summary>
        /// <param name="config">An object containing dynamic configuration settings for rendering the component.
		/// This can include properties such as layout, style, and content to be displayed in the bubble notification.
		/// </param>
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
