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
using System.Drawing.Design;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.Polymer
{
	/// <summary>
	/// Represents a polymer (https://elements.polymer-project.org/) widget.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(PolymerWidget))]
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
		/// Enables HTML content in the Text property.
		///</summary>
		///<remarks>
		///	Newlines (CRLF) are converted to &lt;BR/&gt; when <para>allowHtml</para> is false, or when <para>allowHtml</para> is true and the text doesn't contain any html.
		///</remarks>
		[Browsable(false)]
		public bool AllowHtml
		{
			get { return true; }
		}

		/// <summary>
		/// Indicates the border style for the control.
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
		[Editor("Wisej.Design.MultilineStringEditorWithAllowHtml, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
		public override string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		/// <summary>
		/// Returns or sets the polymer element type.
		/// </summary>
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
		/// Returns or sets the polymer element class name.
		/// </summary>
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
		[DesignerActionList]
		[Category("Polymer")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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
		[DesignerActionList]
		[Category("Polymer")]
		[MergableProperty(false)]
		[Editor("Wisej.Design.CodeEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
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
