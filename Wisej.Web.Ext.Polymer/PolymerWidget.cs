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
using System.ComponentModel;
using System.Drawing;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.Polymer
{
	/// <summary>
	/// Represents a control that hosts a Polymer element (<see href="https://www.polymer-project.org"/>).
	/// </summary>
	/// <remarks>
	/// The element identified by <see cref="ElementType"/> is imported on demand from <see cref="PolymerComponent.PolymerBaseUrl"/>,
	/// created inside the control and initialized with the values in <see cref="Properties"/>. The element events listed
	/// in <see cref="Events"/> are routed to the server through the <see cref="PolymerEvent"/> event.
	/// </remarks>
	/// <example>
	/// Creating a Polymer slider and reading its value when it changes:
	/// <code><![CDATA[
	/// var slider = new PolymerWidget
	/// {
	///     ElementType = "paper-slider",
	///     Events = new[] { "change" },
	///     Size = new Size(300, 40)
	/// };
	/// slider.Properties.min = 0;
	/// slider.Properties.max = 100;
	/// slider.Properties.value = 25;
	/// slider.PolymerEvent += (s, e) =>
	/// {
	///     if (e.Type == "change")
	///         AlertBox.Show("Value: " + slider.Properties.value);
	/// };
	/// this.Controls.Add(slider);
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(PolymerWidget))]
	[ApiCategory("Polymer")]
	public class PolymerWidget : Control
	{
		#region Events

		/// <summary>
		/// Fired when the widget fires an event.
		/// </summary>
		[SRCategory("CatAction")]
		[SRDescription("WidgetEventDescr")]
		public event WidgetEventHandler PolymerEvent
		{
			add { base.AddHandler(nameof(PolymerEvent), value); }
			remove { base.RemoveHandler(nameof(PolymerEvent), value); }
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Control.PolymerEvent" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:Wisej.Web.WidgetEventArgs" /> that contains the event data. </param>
		protected virtual void OnPolymerEvent(WidgetEventArgs e)
		{
			((WidgetEventHandler)base.Events[nameof(PolymerEvent)])?.Invoke(this, e);

		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns whether the <see cref="Text"/> property can contain HTML content. Always returns <c>true</c>.
		/// </summary>
		/// <remarks>
		/// The <see cref="Text"/> is always rendered as HTML inside the Polymer element. Newlines (CRLF) are converted to
		/// <c>&lt;br/&gt;</c> only when the text doesn't contain any HTML.
		/// </remarks>
		[Browsable(false)]
		public bool AllowHtml
		{
			get { return true; }
		}

		/// <summary>
		/// Returns or sets the border style for the control.
		/// </summary>
		/// <returns>One of the <see cref="T:Wisej.Web.BorderStyle" /> values. The default is BorderStyle.None.</returns>
		[DefaultValue(BorderStyle.None)]
		[SRCategory("CatAppearance")]
		[SRDescription("PanelBorderStyleDescr")]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				return this._borderStyle;
			}
			set
			{
				if (this._borderStyle != value)
				{
					this._borderStyle = value;

					Refresh();
					OnStyleChanged(EventArgs.Empty);
				}
			}
		}
		private BorderStyle _borderStyle = BorderStyle.None;

		/// <summary>
		/// Returns or sets the HTML content associated with this polymer widget.
		/// </summary>
		/// <returns>The inner HTML content of the polymer widget.</returns>
		/// <remarks>
		/// The text is assigned as the inner HTML (light DOM) of the Polymer element, i.e. the label of a
		/// <c>paper-button</c> or the items of a <c>paper-listbox</c>.
		/// </remarks>
		/// <example>
		/// Setting the label of a Polymer button:
		/// <code><![CDATA[
		/// this.polymerWidget1.ElementType = "paper-button";
		/// this.polymerWidget1.Text = "<iron-icon icon='check'></iron-icon>Accept";
		/// ]]></code>
		/// </example>
		[Editor("Wisej.Design.MultilineStringEditorWithAllowHtml, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public override string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		/// <summary>
		/// Returns or sets the polymer element type.
		/// </summary>
		/// <remarks>
		/// The value is the tag name of the Polymer element to create, i.e. <c>"paper-button"</c> or <c>"paper-slider"</c>.
		/// If the element is not registered yet, it is imported from
		/// <c>PolymerBaseUrl + ElementType + "/" + ElementType + ".html"</c> (see <see cref="PolymerComponent.PolymerBaseUrl"/>).
		/// </remarks>
		/// <example>
		/// Creating a Polymer toggle button:
		/// <code><![CDATA[
		/// this.polymerWidget1.ElementType = "paper-toggle-button";
		/// this.polymerWidget1.Properties.@checked = true;
		/// ]]></code>
		/// </example>
		[DefaultValue("")]
		[Category("Polymer")]
		[DesignerActionList]
		public virtual string ElementType
		{
			get { return this._elementType; }
			set
			{
				value = value ?? string.Empty;

				if (this._elementType != value)
				{
					this._elementType = value;
					Update();
				}
			}
		}
		private string _elementType = string.Empty;

		/// <summary>
		/// Returns or sets the CSS class name added to the polymer element.
		/// </summary>
		/// <remarks>
		/// The class name is appended to the default class names of the element. Use it to apply
		/// custom CSS styles defined in the application.
		/// </remarks>
		[DefaultValue("")]
		[Category("Polymer")]
		[DesignerActionList]
		public virtual string ElementClassName
		{
			get { return this._elementClassName; }
			set
			{
				value = value ?? string.Empty;

				if (this._elementClassName != value)
				{
					this._elementClassName = value;
					Update();
				}
			}
		}
		private string _elementClassName = string.Empty;

		/// <summary>
		/// Returns or sets the events from the polymer widget to handle on the server side.
		/// </summary>
		/// <remarks>
		/// Each entry is the name of a DOM event fired by the Polymer element, i.e. <c>"tap"</c>, <c>"change"</c> or
		/// <c>"iron-select"</c>. When one of these events occurs, the <see cref="PolymerEvent"/> event is fired on the server
		/// with the event name in <see cref="WidgetEventArgs.Type"/> and the changed values of the <see cref="Properties"/>
		/// in <see cref="WidgetEventArgs.Data"/>.
		/// This property hides the inherited <c>Events</c> list of event handlers.
		/// </remarks>
		/// <example>
		/// Handling the "tap" event of a Polymer button:
		/// <code><![CDATA[
		/// this.polymerWidget1.ElementType = "paper-button";
		/// this.polymerWidget1.Events = new[] { "tap" };
		/// this.polymerWidget1.PolymerEvent += (s, e) =>
		/// {
		///     if (e.Type == "tap")
		///         AlertBox.Show("Tapped!");
		/// };
		/// ]]></code>
		/// </example>
		[DesignerActionList]
		[Category("Polymer")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new virtual string[] Events
		{
			get { return this._events; }
			set
			{
				this._events = value;
				Update();
			}
		}
		private string[] _events;

		/// <summary>
		/// Returns or sets the specified property values on the polymer widget
		/// and defines the properties to receive back when an event is fired.
		/// </summary>
		/// <remarks>
		/// Each field of this dynamic object is assigned to the property with the same name of the Polymer element.
		/// When one of the <see cref="Events"/> is fired, the fields whose value has changed on the client are sent back,
		/// updated in this object and passed in <see cref="WidgetEventArgs.Data"/>. Only properties listed here are sent back:
		/// add a field with its initial value to track a property.
		/// Changing a field of the existing object doesn't update the client automatically; call <see cref="Update"/>.
		/// The <c>disabled</c> field is managed automatically according to the <see cref="Control.Enabled"/> property.
		/// </remarks>
		/// <example>
		/// Changing the properties of a Polymer checkbox and sending them to the client:
		/// <code><![CDATA[
		/// this.polymerWidget1.ElementType = "paper-checkbox";
		/// this.polymerWidget1.Events = new[] { "change" };
		/// this.polymerWidget1.Properties.@checked = false;
		/// this.polymerWidget1.Update();
		///
		/// // later, after the "change" event:
		/// bool isChecked = this.polymerWidget1.Properties.@checked;
		/// ]]></code>
		/// </example>
		[DesignerActionList]
		[Category("Polymer")]
		[MergableProperty(false)]
		[Editor("Wisej.Design.DynamicObjectEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual dynamic Properties
		{
			get
			{
				if (this._properties == null)
					this._properties = new DynamicObject();

				return this._properties;
			}
			set
			{
				this._properties = value;
				Update();
			}
		}
		private dynamic _properties;

		private bool ShouldSerializeProperties()
		{
			return (!this._properties?.IsEmpty()) ?? false;
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Fires the <see cref="E:Wisej.Base.ControlBase.EnabledChanged" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		protected override void OnEnabledChanged(EventArgs e)
		{
			// polymer widgets use the disabled property.
			this.Properties.disabled = !this.Enabled;

			base.OnEnabledChanged(e);
		}

		/// <summary>
		/// Updates the widget definition on the client.
		/// </summary>
		/// <remarks>
		/// The whole <see cref="Properties"/> object is always sent to the client, not only the changed values.
		/// Call this method after changing the fields of <see cref="Properties"/>.
		/// </remarks>
		/// <example>
		/// Changing a property of the Polymer element:
		/// <code><![CDATA[
		/// this.polymerWidget1.Properties.value = 75;
		/// this.polymerWidget1.Update();
		/// ]]></code>
		/// </example>
		public override void Update()
		{
			// make sure that the properties are sent back in full
			// without the differential comparison with the prior values.
			base.Update("properties");
		}

		// Handles incoming events from the polymer widget.
		private void ProcessPolymerWebEvent(WisejEventArgs e)
		{
			dynamic ev = e.Parameters.Event;

			// update the properties from the polymer widget.
			var data = ev.data as DynamicObject;
			if (data != null)
			{
				// update the properties.
				foreach (var field in data)
					this._properties[field.Name] = field.Value;
			}

			OnPolymerEvent(new WidgetEventArgs(ev.type, data));
		}

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "polymerEvent":
					ProcessPolymerWebEvent(e);
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
			IWisejControl me = this;

			config.className = "wisej.web.ext.PolymerWidget";

			config.events = this.Events;
			config.properties = this.Properties;
			config.elementType = this.ElementType;
			config.borderStyle = this.BorderStyle;
			config.elementClassName = this.ElementClassName;
			config.content = TextUtils.EscapeText(this.Text, true);
			config.wiredEvents.Add("polymerEvent(Event)");
		}

		#endregion
	}
}
