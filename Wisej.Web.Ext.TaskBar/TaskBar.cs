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
using System;
using System.ComponentModel;
using System.Drawing;
using Wisej.Core;

namespace Wisej.Web.Ext.TaskBar
{
	/// <summary>
	/// Implements a TaskBar container that can be used outside of a wisej.web.Desktop widget
	/// to manage minimized floating windows without having to use a Desktop or a custom manager.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(TaskBar))]
	public class TaskBar : Control, IWisejControl
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskBar" /> class.
		///</summary>
		public TaskBar()
		{
			this.Dock = DockStyle.Bottom;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the default size for the TaskBar control.
		/// </summary>
		protected override Size DefaultSize
		{
			get { return new Size(120, 48); }
		}

		/// <summary>
		/// Returns or sets the docking behavior of the <see cref="TaskBar" /> control.
		///</summary>
		/// <returns>One of the <see cref="Wisej.Web.DockStyle" /> values. The default is Bottom.</returns>
		[Localizable(true)]
		[DefaultValue(DockStyle.Bottom)]
		public override DockStyle Dock
		{
			get { return base.Dock; }
			set
			{
				switch (value)
				{
					case DockStyle.Bottom:
						this.TaskbarPosition = Position.Bottom;
						if (base.Dock == DockStyle.Left || base.Dock == DockStyle.Right)
							this.Height = this.Width;
						break;

					case DockStyle.Top:
						this.TaskbarPosition = Position.Top;
						if (base.Dock == DockStyle.Left || base.Dock == DockStyle.Right)
							this.Height = this.Width;
						break;

					case DockStyle.Left:
						this.TaskbarPosition = Position.Left;
						if (base.Dock == DockStyle.Top || base.Dock == DockStyle.Bottom)
							this.Width = this.Height;
						break;

					case DockStyle.Right:
						this.TaskbarPosition = Position.Right;
						if (base.Dock == DockStyle.Top || base.Dock == DockStyle.Bottom)
							this.Width = this.Height;
						break;
				}

				base.Dock = value;
			}
		}

		/// <summary>
		/// Return or sets the position of the TaskBar to one of the four sides
		/// indicated by the <see cref="Position"/> values.
		/// </summary>
		[DefaultValue(Position.Bottom)]
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
		/// Returns or sets whether the TaskBar is hidden automatically when there are no
		/// opened windows or no windows with the property ShowInTaskbar set to true.
		/// </summary>
		[DefaultValue(false)]
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

		#region Methods

		/// <summary>
		/// Raises the <see cref="Wisej.Web.Control.ParentChanged" /> event.
		/// </summary>
		/// <param name="e">A System.EventArgs that contains the event data.</param>
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);

			if (this.Parent != null && !(this.Parent is Page))
				throw new InvalidOperationException("The TaskBar control can only be a child of a Page.");
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Returns the theme appearance key for this control.
		/// </summary>
		string IWisejControl.AppearanceKey
		{
			get { return this.AppearanceKey ?? "desktop/taskbar"; }
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
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