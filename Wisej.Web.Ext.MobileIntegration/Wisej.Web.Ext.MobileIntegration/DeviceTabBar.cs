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

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Represents the native TabBar on the mobile device.
	/// </summary>
	[ApiCategory("API")]
	public partial class DeviceTabBar
	{

		#region Constructor

		internal DeviceTabBar()
		{
		}

		#endregion

		#region Events

		/// <summary>
		/// Fired when the user changes the selected tab button.
		/// </summary>
		public event DeviceTabBar.SelectedEventHandler Selected;

		/// <summary>
		/// Fires the <see cref="Selected"/> event.
		/// </summary>
		/// <param name="index">Index of the selected button.</param>
		internal void RaiseSelected(int index)
		{
			if (index < 0 || index >= this.Buttons.Length)
				return;

			var button = this.Buttons[index];

			foreach (var b in this.Buttons)
			{
				if (b != button)
					b.Selected = false;
			}

			button.Selected = true;
			this.Selected?.Invoke(this, new SelectedEventArgs(button));
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets whether the tabBar is visible on the device.
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
		/// Returns or sets the background color of the TabBar.
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
		/// Returns or sets the text color of the items in the TabBar.
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
		/// Returns or sets the text color of the selected item in the TabBar.
		/// </summary>
		public Color SelectedColor
		{
			get { return this._selectedcColor; }
			set
			{
				if (this._selectedcColor != value)
				{
					this._selectedcColor = value;
					Update();
				}
			}
		}
		private Color _selectedcColor = Color.FromArgb(52, 122, 246);

		/// <summary>
		/// Returns or sets the collection of TabBar buttons.
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

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Updates the tabBar on the device.
		/// </summary>
		public void Update(bool refresh=false)
		{
			if (this._buttonsChanged || refresh)
			{
				Device.PostMessage("tabbar.options", new
				{
					visible = this.Visible,
					buttons = this._buttons,
					color = DeviceUtils.GetHtmlColor(this.Color),
					backgroundColor = DeviceUtils.GetHtmlColor(this.BackColor),
					selectedColor = DeviceUtils.GetHtmlColor(this.SelectedColor)
				});

				this._buttonsChanged = false;
			}
			else
			{
				Device.PostMessage("tabbar.options", new
				{
					visible = this.Visible,
					color = DeviceUtils.GetHtmlColor(this.Color),
					backgroundColor = DeviceUtils.GetHtmlColor(this.BackColor),
					selectedColor = DeviceUtils.GetHtmlColor(this.SelectedColor)
				}); ;
			}
		}

		#endregion

	}
}
