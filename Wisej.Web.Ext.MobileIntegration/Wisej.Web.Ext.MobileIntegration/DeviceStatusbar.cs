///////////////////////////////////////////////////////////////////////////////
//
// (C) 2019 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.ComponentModel;
using System.Drawing;
using Wisej.Core;

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Represents the statusbar of the mobile device.
	/// </summary>
	[ApiCategory("API")]
	public partial class DeviceStatusbar
	{

		#region Constructor

		internal DeviceStatusbar()
		{
			this.Visible = true;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets whether the statusbar is visible on the device.
		/// </summary>
		public bool Visible
		{
			get { return this._visible; }
			set
			{
				if (this._visible != value)
				{
					this._visible = value;
					Update();
				}
			}
		}
		private bool _visible = true;

		/// <summary>
		/// Returns or sets the background color of the statusbar.
		/// </summary>
		public Color BackColor
		{
			get { return this._backColor; }
			set
			{
				if (this.BackColor != value)
				{
					this._backColor = value;
					Update();
				}
			}
		}
		private Color _backColor;

		/// <summary>
		/// Returns or sets the text color of the statusbar.
		/// </summary>
		public StatusBarForeColor ForeColor
		{
			get { return this._foreColor; }
			set
			{
				if (this._foreColor != value)
				{
					this._foreColor = value;
					Update();
				}
			}
		}
		private StatusBarForeColor _foreColor;

		#endregion

		#region Methods

		/// <summary>
		/// Starts updating mode. The device will not receive the updates
		/// until <see cref="EndUpdate"/> is called an equal number of times as
		/// calls to <see cref="BeginUpdate"/>.
		/// </summary>
		public void BeginUpdate()
		{
			this._updating++;
		}
		private int _updating = 0;

		/// <summary>
		/// Ends updating mode and updates the client device.
		/// </summary>
		public void EndUpdate()
		{
			this._updating--;
			if (this._updating == 0)
				Update();
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Updates the device. Sends the difference between the last update and the current options.
		/// </summary>
		internal void Update(bool forceRefresh=false)
		{
			Device.PostMessage("statusbar.options", new
			{
				backgroundColor = DeviceUtils.GetHtmlColor(this.BackColor),
				foregroundColor = this.ForeColor,
				visible = this.Visible
			});
		}

		#endregion

	}
}
