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
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Ext.Camera
{
	/// <summary>
	/// The Camera component makes it possible to take pictures with the device's camera and upload them to the server.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Camera))]
	[Description("The Camera component makes it possible to take pictures with the device's camera and upload them to the server.")]
	public class Camera : Wisej.Base.Component, IComponent
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.Camera" /> class.
		/// </summary>
		public Camera()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.Camera" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the component. </param>
		public Camera(IContainer container)
            : this()
        {
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Events

		#endregion

		#region Properties

		#endregion

		#region Methods

		#endregion

		#region IComponent

		/// <summary>
		/// Returns or sets the <see cref="T:System.ComponentModel.ISite" /> associated with 
		/// the <see cref="T:System.ComponentModel.IComponent" />.
		/// </summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> object associated with the component; or null, if the component does not have a site.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ISite Site
		{
			get { return this._site; }
			set
			{
				this._site = value;
				((IWisejComponent)this).DesignMode = value == null ? false : value.DesignMode;
			}
		}
		private ISite _site;

		/// <summary>
		/// Returns a value that indicates whether the <see cref="T:System.ComponentModel.IComponent" /> is currently in design mode.
		/// </summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.IComponent" /> is in design mode; otherwise, false.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected bool DesignMode
		{
			get { return this._site != null && this._site.DesignMode; }
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

			config.className = "wisej.ext.Camera";
		}

		#endregion


	}
}
