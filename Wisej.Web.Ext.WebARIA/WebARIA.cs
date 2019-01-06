///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Wisej.Core;

namespace Wisej.Web.Ext.WebARIA
{
	/// <summary>
	/// Represents the set of ARIA properties associated to a control.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(WebARIA))]
	[ProvideProperty("Aria", typeof(Control))]
	[Description("Represents the set of ARIA properties associated to a control.")]
	public class WebARIA : Wisej.Web.Component, IExtenderProvider
	{
		// collection of controls with the related ARIA properties.
		private Dictionary<Control, ARIA> controls;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="WebARIA" /> without a specified container.
		/// </summary>
		public WebARIA()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WebARIA" /> class with a specified container.
		/// </summary>
		/// <param name="container">An <see cref="System.ComponentModel.IContainer" />container. </param>
		public WebARIA(IContainer container)
			: this()
		{
			if (container == null)
				throw new ArgumentNullException(nameof(container));

			container.Add(this);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns true if <see cref="WebARIA" /> can offer an extender property to the specified target component.
		/// </summary>
		/// <returns>true if the <see cref="WebARIA" /> class can offer one or more extender properties; otherwise, false.</returns>
		/// <param name="target">The target object to add an extender property to. </param>
		public bool CanExtend(object target)
		{
			return (target is Control);
		}

		/// <summary>
		/// Returns the <see cref="ARIA"/>  properties for the specified <see cref="Control"/>.
		/// </summary>
		/// <param name="control"><see cref="Control"/> for which to return the <see cref="ARIA"/> properties.</param>
		/// <returns></returns>
		[DisplayName("Aria")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ARIA GetAria(Control control)
		{
			if (control == null)
				throw new ArgumentNullException(nameof(control));

			if (this.controls == null)
				this.controls = new Dictionary<Control, ARIA>();

			ARIA aria = null;
			if (!this.controls.TryGetValue(control, out aria))
			{
				aria = new ARIA(control);
				controls[control] = aria;
			}

			// remove the control from the extender when it's disposed.
			control.Disposed -= this.Control_Disposed;
			control.Disposed += this.Control_Disposed;

			return aria;
		}

		private bool ShouldSerializeAria(Control control)
		{
			Debug.Assert(control != null);

			return
				this.controls != null &&
				this.controls.ContainsKey(control);
		}

		private void ResetAria(Control control)
		{
			Debug.Assert(control != null);

			if (this.controls != null)
				this.controls.Remove(control);
		}

		private void Control_Disposed(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			control.Disposed -= this.Control_Disposed;
			control.ControlCreated -= this.Control_Created;

			// remove the extender values associated with the disposed control.
			if (this.controls != null)
				this.controls.Remove(control);
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
			if (this.controls != null)
			{
				this.controls.ToList().ForEach((o) => {
					o.Key.Disposed -= this.Control_Disposed;
					o.Key.ControlCreated -= this.Control_Created;
				});

				this.controls.Clear();

				Update();
			}
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

			config.className = "wisej.web.extender.WebAria";

			if (this.controls != null)
			{
				List<object> list = new List<object>();
				foreach (var entry in this.controls)
				{
					var control = entry.Key;
					var settings = entry.Value;

					// skip controls that are not yet created.
					if (!control.Created)
						continue;

					if (settings != null)
					{
						list.Add(new
						{
							id = ((IWisejComponent)control).Id,
							attributes = settings.Render()
						});
					}
				}
				config.controls = list.ToArray();

				// register non-created control for delayed registration.
				this.controls.Where(o => !o.Key.Created).ToList().ForEach(o => o.Key.ControlCreated += this.Control_Created);
			}
	}

		#endregion
	}
}
