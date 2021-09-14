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
using System.Threading.Tasks;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Ext.Geolocation
{
	/// <summary>
	/// The Geolocation component represents an object able to programmatically obtain the position of the device.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Geolocation))]
	[ToolboxItemFilter("Wisej.Web", ToolboxItemFilterType.Require)]
	[ToolboxItemFilter("Wisej.Mobile", ToolboxItemFilterType.Require)]
	[Description("The Geolocation component represents an object able to programmatically obtain the position of the device.")]
	[ApiCategory("Geolocation")]
	public class Geolocation : Wisej.Base.Component, IComponent
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.Geolocation.Geolocation" /> class.
		/// </summary>
		public Geolocation()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Ext.Geolocation.Geolocation" /> class together with the specified container.
		/// </summary>
		/// <param name="container">A <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the component. </param>
		public Geolocation(IContainer container)
			: this()
		{
			if (container == null)
				throw new ArgumentNullException("container");

			container.Add(this);
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when the location of the browser changes while this component has an active watch.
		/// </summary>
		public event EventHandler PositionChanged;

		/// <summary>
		/// Fires the PositionChanged event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnPositionChanged(EventArgs e)
		{
			if (this.PositionChanged != null)
				PositionChanged(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Enables or disables active watch mode for this geolocation component.
		/// When enabled, the <see cref="E:Wisej.Ext.Geolocation.PositionChanged"/> event be fired automatically each time the position of the device changes.
		/// </summary>
		[DefaultValue(false)]
		[Description("Enables or disables active watch mode for this geolocation component.")]
		public bool ActiveWatch
		{
			get { return this._activeWatch; }
			set
			{
				if (this._activeWatch != value)
				{
					this._activeWatch = value;
					Update();
				}
			}
		}
		private bool _activeWatch = false;

		/// <summary>
		///  Indicates the application would like to receive the best possible results.
		/// </summary>
		/// <remarks>
		///  If true and if the device is able to provide a more accurate position, it will do so. 
		///  Note that this can result in slower response times or increased power consumption (with a GPS chip on a mobile device for example).
		///  On the other hand, if false, the device can take the liberty to save resources by responding more quickly and/or using less power. 
		/// </remarks>

		[DefaultValue(false)]
		[Description("Indicates the application would like to receive the best possible results.")]
		public bool EnableHighAccuracy
		{
			get { return this._enableHighAccuracy; }
			set
			{
				if (this._enableHighAccuracy != value)
				{
					this._enableHighAccuracy = value;
					Update();
				}
			}
		}
		private bool _enableHighAccuracy = false;

		/// <summary>
		/// Sets or gets the maximum length of time (in milliseconds) the device is allowed to take in order to return a position.
		/// </summary>
		[DefaultValue(-1)]
		[Description("Sets or gets the maximum length of time (in milliseconds) the device is allowed to take in order to return a position.")]
		public long Timeout
		{
			get { return this._timeout; }
			set
			{
				if (this._timeout != value)
				{
					this._timeout = value;
					Update();
				}
			}
		}
		private long _timeout = -1;

		/// <summary>
		/// Indicates the maximum age in milliseconds of a possible cached position that is acceptable to return.
		/// </summary>
		/// <remarks>
		/// If set to 0, it means that the device cannot use a cached position and must attempt to retrieve the real current position.
		/// If set to -1 the device must return a cached position regardless of its age.
		/// </remarks>
		[DefaultValue(-1)]
		[Description("Indicates the maximum age in milliseconds of a possible cached position that is acceptable to return. If set to -1 the device must return a cached position regardless of its age.")]
		public long MaximumAge
		{
			get { return this._maxAge; }
			set
			{
				if (this._maxAge != value)
				{
					this._maxAge = value;
					Update();
				}
			}
		}
		private long _maxAge = -1;

		/// <summary>
		/// Returns the last position detected by the device.
		/// </summary>
		[Browsable(false)]
		public Position LastPosition
		{
			get;
			private set;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns the current position of the device.
		/// </summary>
		/// <param name="callback"></param>
		public void GetCurrentPosition(Action<Position> callback)
		{
			if (callback == null)
				throw new ArgumentNullException("callback");

			GetCurrentPositionCore(callback);
		}

		/// <summary>
		/// Asynchronously returns the current position of the device.
		/// </summary>
		public Task<Position> GetCurrentPositionAsync()
		{
			var tcs = new TaskCompletionSource<Position>();

			GetCurrentPositionCore((position) => {

				tcs.SetResult(position);

			});

			return tcs.Task;
		}

		// Implementation
		private void GetCurrentPositionCore(Action<Position> callback)
		{
			// save the callback in the dictionary and issue a geCurrentPosition request
			// using the hash of the callback object to identify the async response.
			if (this._callbacks == null)
				this._callbacks = new Dictionary<int, Action<Position>>();

			int id = callback.GetHashCode();
			this._callbacks[id] = callback;

			Call("getPosition", id);
		}

		private Dictionary<int, Action<Position>> _callbacks = null;

		/// <summary>
		/// Process the getCurrentPosition response from the client.
		/// </summary>
		/// <param name="e"></param>
		private void ProcessCallbackWebEvent(WisejEventArgs e)
		{
			dynamic data = e.Parameters.Data;

			// find the corresponding request.
			if (this._callbacks != null)
			{
				int id = data.id ?? 0;
				Position position = new Position(data);

				Action<Position> callback = null;
				if (this._callbacks.TryGetValue(id, out callback))
				{
					this._callbacks.Remove(id);
					callback(position);
				}
			}
		}

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
				case "positionChanged":
					this.LastPosition = new Position(e.Parameters.Data);
					OnPositionChanged(EventArgs.Empty);
					break;

				case "callback":
					ProcessCallbackWebEvent(e);
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

			config.className = "wisej.ext.Geolocation";
			config.activeWatch = this.ActiveWatch;
			config.highAccuracy = this.EnableHighAccuracy;
			config.timeout = this.Timeout;
			config.maxAge = this.MaximumAge;

			WiredEvents events = new WiredEvents();
			events.Add("positionChanged(Data)", "callback(Data)");
			config.wiredEvents = events;
		}

		#endregion


	}
}
