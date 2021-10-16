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

using System.Drawing;
using System.ComponentModel;

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Represents the native toolbar on the mobile device.
	/// </summary>
	[ApiCategory("API")]
	public partial class DeviceToolbar
	{
		internal DeviceToolbar()
		{
		}

		/// <summary>
		/// Fired when the user clicks a toolbar button.
		/// </summary>
		public event DeviceToolbar.ClickEventHandler Click;

		/// <summary>
		/// Fires the <see cref="Click"/> event.
		/// </summary>
		/// <param name="index">Index of the clicked button.</param>
		internal void RaiseClick(int index)
		{
			if (index < 0 || index >= this.Buttons.Length)
				return;

			var button = this.Buttons[index];
			this.Click?.Invoke(this, new ClickEventArgs(button));
		}

		/// <summary>
		/// Returns or sets whether the toolbar is visible on the device.
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
		private bool _visible;

		/// <summary>
		/// Returns or sets the background color of the toolbar.
		/// </summary>
		public Color BackColor
		{
			get { return this._backColor; }
			set
			{
				if (this._backColor != value)
				{
					this._backColor = value;
					Update();
				}
			}
		}
		private Color _backColor = Color.FromArgb(255, 255, 255);

		/// <summary>
		/// Returns or sets the text color of the items in the toolbar.
		/// </summary>
		public Color Color
		{
			get { return this._color; }
			set
			{
				if (this._color != value)
				{
					this._color = value;
					Update();
				}
			}
		}
		private Color _color = Color.FromArgb(153, 153, 153);

		/// <summary>
		/// Returns or sets the collection of toolbar buttons.
		/// </summary>
		public Button[] Buttons
		{
			get
			{
				return this._buttons ?? _empty;
			}
			set
			{
				if (this._buttons != value)
				{
					this._buttons = value;
					this._buttonsChanged = true;
					Update();
				}
			}
		}
		private Button[] _buttons;
		private bool _buttonsChanged;
		private static Button[] _empty = new Button[0];

		/// <summary>
		/// Updates the device. Sends the difference between the last update and the current options.
		/// </summary>
		internal void Update()
		{
			if (this._buttonsChanged)
			{
				Device.PostMessage("toolbar.options", new
				{
					visible = this.Visible,
					buttons = this._buttons,
					color = DeviceUtils.GetHtmlColor(this.Color),
					backgroundColor = DeviceUtils.GetHtmlColor(this.BackColor)
				});

				this._buttonsChanged = false;
			}
			else
			{
				Device.PostMessage("toolbar.options", new
				{
					visible = this.Visible,
					color = DeviceUtils.GetHtmlColor(this.Color),
					backgroundColor = DeviceUtils.GetHtmlColor(this.BackColor)
				});
			}
		}
	}
}
