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
	/// Extender component that adds the <see cref="ARIA"/> (WAI-ARIA) property to all controls in the same container,
	/// allowing the application to assign <c>aria-*</c> attributes to the accessibility element of each control.
	/// </summary>
	/// <remarks>
	/// Drop the component on a container (i.e. a <see cref="Form"/> or <see cref="Page"/>) and use the
	/// "Aria" property added to each control in the designer, or call <see cref="GetAria(Control)"/> in code.
	/// The attributes are applied on the client to the control's accessibility element. Controls that are not created yet
	/// (i.e. not visible) are registered when they are created.
	/// </remarks>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(WebARIA))]
	[ProvideProperty("Aria", typeof(Control))]
	[Description("Represents the set of ARIA properties associated to a control.")]
	[ApiCategory("WebARIA")]
	public class WebARIA : Wisej.Web.Component, IExtenderProvider
	{
		// collection of controls with the related ARIA properties.
		private Dictionary<Control, ARIA> controls;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="WebARIA" /> extender without a specified container.
		/// </summary>
		public WebARIA()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WebARIA" /> extender with a specified container.
		/// </summary>
		/// <param name="container">An <see cref="System.ComponentModel.IContainer" /> that represents the container of the <see cref="WebARIA" /> extender.</param>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
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
		/// <param name="target">The target object to add an extender property to.</param>
		/// <returns>true if <paramref name="target"/> is a <see cref="Control"/>; otherwise, false.</returns>
		/// <remarks>
		/// This method is used by the designer to determine which components receive the "Aria" property.
		/// </remarks>
		/// <example>
		/// Checking whether a component can receive ARIA attributes:
		/// <code><![CDATA[
		/// if (this.webARIA1.CanExtend(this.textBox1))
		/// {
		///     this.webARIA1.GetAria(this.textBox1).Label = "Customer name";
		/// }
		/// ]]></code>
		/// </example>
		public bool CanExtend(object target)
		{
			return (target is Control);
		}

		/// <summary>
		/// Returns the <see cref="ARIA"/> properties for the specified <see cref="Control"/>.
		/// </summary>
		/// <param name="control"><see cref="Control"/> for which to return the <see cref="ARIA"/> properties.</param>
		/// <returns>The <see cref="ARIA"/> instance associated with <paramref name="control"/>. It's created the first time it's requested.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="control"/> is null.</exception>
		/// <remarks>
		/// The returned object is kept by the extender until the control is disposed or <see cref="Clear"/> is called.
		/// The attributes are sent to the client when the extender is rendered. Changing the <see cref="ARIA"/> properties
		/// invalidates the owner control; call <c>Update()</c> on the extender to refresh the attributes
		/// of controls that are already displayed.
		/// </remarks>
		/// <example>
		/// Assigning ARIA attributes to a text box and a numeric field:
		/// <code><![CDATA[
		/// private void Form1_Load(object sender, EventArgs e)
		/// {
		///     var aria = this.webARIA1.GetAria(this.textBoxEmail);
		///     aria.Label = "Email address";
		///     aria.Required = TriState.True;
		///     aria.DescribedBy = this.labelEmailHint;
		///
		///     var quantity = this.webARIA1.GetAria(this.numericUpDownQuantity);
		///     quantity.ValueMin = 1;
		///     quantity.ValueMax = 100;
		///     quantity.ValueNow = 10;
		///
		///     this.webARIA1.Update();
		/// }
		/// ]]></code>
		/// </example>
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
		/// Removes the <see cref="ARIA"/> properties from all the controls managed by this extender.
		/// </summary>
		/// <remarks>
		/// The <c>aria-*</c> attributes previously assigned on the client are removed.
		/// </remarks>
		/// <example>
		/// Removing all the ARIA attributes:
		/// <code><![CDATA[
		/// private void buttonReset_Click(object sender, EventArgs e)
		/// {
		///     this.webARIA1.Clear();
		/// }
		/// ]]></code>
		/// </example>
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
