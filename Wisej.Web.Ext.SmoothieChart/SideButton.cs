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
using System.Drawing;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.SideButton
{
	/// <summary>
	/// Represents a retractable animated
	/// button that can be used expand or collapse other panels.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(SideButton))]
	[Description("Retractable animated side button.")]
	public class SideButton : Button, IWisejControl
	{
		#region Constructor

		public SideButton()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the alignment and direction of the <see cref="SideButton"/>.
		/// </summary>
		[DefaultValue(LeftRightAlignment.Left)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the alignment and direction of the SideButton.")]
		public LeftRightAlignment Alignment
		{
			get
			{
				return this._alignment;
			}
			set
			{
				if (this._alignment != value)
				{
					this._alignment = value;
					Update();
				}
			}
		}
		private LeftRightAlignment _alignment;

		/// <summary>
		/// Returns or sets the collapsed state of the <see cref="SideButton"/>
		/// </summary>
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets the collapsed state of the SideButton.")]
		public bool Collapsed
		{
			get
			{
				return this._collapsed;
			}
			set
			{
				if (this._collapsed != value)
				{
					this._collapsed = value;
					Update();
				}
			}
		}
		private bool _collapsed;

		private int ThemeHeight
		{
			get
			{
				var state = this.Collapsed ? "collapsed" : null;
				return Application.Theme.GetProperty<int>(
					((IWisejControl)this).AppearanceKey, "height", state);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns the theme appearance key for this control.
		/// </summary>
		string IWisejControl.AppearanceKey
		{
			get
			{
				return
					this.AppearanceKey ?? "side-button";
			}
		}
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			height = this.ThemeHeight;
			base.SetBoundsCore(x, y, width, height, specified);
		}

		#endregion

		#region Wisej Implementation

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.className = "wisej.web.ext.SideButton";
			config.collapsed = this.Collapsed;
			config.alignment = this.Alignment;
		}

		#endregion
	}
}