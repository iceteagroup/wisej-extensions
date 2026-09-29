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
///
using System.ComponentModel;
using System.Drawing;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.SideButton
{
	/// <summary>
	/// Represents a retractable animated
	/// button that can be used to expand or collapse other panels.
	/// </summary>
	/// <remarks>
	/// When <see cref="Collapsed"/> is true the button shrinks to the collapsed width defined by the
	/// theme (appearance "side-button") and slides back out to its full <see cref="Control.Width"/>
	/// when the pointer hovers over it. The height of the button is always taken from the theme.
	/// </remarks>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(SideButton))]
	[Description("Retractable animated side button.")]
	[ApiCategory("SideButton")]
	public class SideButton : Button, IWisejControl
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="SideButton"/> class.
		/// </summary>
		public SideButton()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the alignment and direction of the <see cref="SideButton"/>.
		/// </summary>
		/// <remarks>
		/// Indicates the side the button retracts to when <see cref="Collapsed"/> is true:
		/// <see cref="LeftRightAlignment.Left"/> retracts toward the left edge and
		/// <see cref="LeftRightAlignment.Right"/> keeps the button anchored to its right end
		/// and retracts toward the right. The value is also applied as a "left" or "right"
		/// state that the theme can style.
		/// </remarks>
		/// <example>
		/// Placing a side button on the right edge of a form that retracts to the right:
		/// <code><![CDATA[
		/// this.sideButton1.Alignment = LeftRightAlignment.Right;
		/// this.sideButton1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		/// this.sideButton1.Collapsed = true;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the collapsed state of the <see cref="SideButton"/>.
		/// </summary>
		/// <remarks>
		/// When collapsed, the button is reduced to the collapsed width defined by the theme and
		/// expands temporarily to its full <see cref="Control.Width"/> while the pointer is over it.
		/// Changing this property also changes the height of the button to the theme height of the
		/// "collapsed" or "default" state.
		/// </remarks>
		/// <example>
		/// Toggling a side panel and the collapsed state of the button when it is clicked:
		/// <code><![CDATA[
		/// private void sideButton1_Click(object sender, EventArgs e)
		/// {
		///     this.panelNavigation.Visible = !this.panelNavigation.Visible;
		///     this.sideButton1.Collapsed = !this.panelNavigation.Visible;
		/// }
		/// ]]></code>
		/// </example>
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
				var state = this.Collapsed ? "collapsed" : "default";
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