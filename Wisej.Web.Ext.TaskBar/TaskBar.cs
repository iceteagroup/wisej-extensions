///////////////////////////////////////////////////////////////////////////////
//
// (C) 2020 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
///
using System.ComponentModel;

namespace Wisej.Web.Ext.TaskBar
{
	/// <summary>
	/// Implements a TaskBar container that can be used outside of a wisej.web.Desktop widget
	/// to manage minimized floating windows without having to use a Desktop or a custom manager.
	/// </summary>
	[ToolboxItem(true)]
	public class TaskBar : Control
	{
		#region Properties

		/// <summary>
		/// Return or sets the position of the taskbar to one of the four sides
		/// indicated by the <see cref="Position"/> values.
		/// </summary>
		[DefaultValue(Position.Bottom)]
		[Category("Layout")]
		public Position TaskbarPosition
		{
			get { return this._taskbarPosition; }
			set
			{
				if (this._taskbarPosition != value)
				{
					this._taskbarPosition = value;
					Update();
				}
			}
		}
		private Position _taskbarPosition = Position.Bottom;

		/// <summary>
		/// Returns or sets whether the taskbar is hidden automatically when there are no
		/// opened windows or no windows with the property ShowInTaskbar set to true.
		/// </summary>
		[DefaultValue(false)]
		[Category("Appearance")]
		public bool AutoHideTaskbar
		{
			get { return this._autoHideTaskbar; }
			set
			{
				if (this._autoHideTaskbar != value)
				{
					this._autoHideTaskbar = value;
					Update();
				}
			}
		}
		private bool _autoHideTaskbar = false;

		#endregion

		#region Wisej Implementation

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ext.TaskBar";

			config.position = this.TaskbarPosition;
			config.autoHide = this.AutoHideTaskbar;
		}

		#endregion
	}
}