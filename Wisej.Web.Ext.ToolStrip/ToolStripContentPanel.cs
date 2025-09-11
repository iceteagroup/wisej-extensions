///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.ComponentModel;
using System.Drawing;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Represents the center panel of a <see cref="ToolStripContainer" /> control.
	///</summary>
	public class ToolStripContentPanel : Wisej.Web.Panel
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripContentPanel" /> class. 
		///</summary>
		public ToolStripContentPanel()
		{
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public event EventHandler AutoSizeChanged
		{
			add => base.AutoSizeChanged += value; 
			remove => base.AutoSizeChanged -= value;
		}

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler CausesValidationChanged
		{
			add => base.CausesValidationChanged += value;
			remove => base.CausesValidationChanged -= value;
		}

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public event EventHandler DockChanged
		{
			add => base.DockChanged += value; 
			remove => base.DockChanged -= value;
		}

		/// <summary>
		/// Occurs when the content panel loads.
		///</summary>
		[SRDescription("ToolStripContentPanelOnLoadDescr")]
		[SRCategory("CatBehavior")]
		public event EventHandler Appear
		{
			add => base.Appear += value; 
			remove => base.Appear -= value;
		}

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public event EventHandler LocationChanged
		{
			add => base.LocationChanged += value;
			remove => base.LocationChanged -= value;
		}

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler TabIndexChanged
		{
			add => base.TabIndexChanged += value;
			remove => base.TabIndexChanged -= value;
		}

		/// <summary>
		/// This event is not relevant for this class.
		///</summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event EventHandler TabStopChanged
		{
			add => base.TabStopChanged += value;
			remove => base.TabStopChanged -= value;
		}

		/// <summary>
		/// Occurs when the value of the <see cref="ToolStripContentPanel.Renderer" /> property changes.
		///</summary>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripRendererChanged")]
		public event EventHandler RendererChanged;

		#endregion

		#region Properties

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>An <see cref="AutoSizeMode" />.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Localizable(false)]
		[DefaultValue(AutoSizeMode.GrowOnly)]
		[SRCategory("CatLayout")]
		[SRDescription("ControlAutoSizeModeDescr")]
		public override AutoSizeMode AutoSizeMode
		{
			get => base.AutoSizeMode;
			set => base.AutoSizeMode = value;
		}
		
		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>An <see cref="AnchorStyles" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[SRCategory("CatLayout")]
		[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
		[SRDescription("ControlAnchorDescr")]
		public override AnchorStyles Anchor
		{
			get => base.Anchor;
			set => base.Anchor = value;
		}
		
		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true to enable automatic scrolling; otherwise, false.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("FormAutoScrollDescr")]
		public override bool AutoScroll
		{
			get => base.AutoScroll;
			set => base.AutoScroll = value;
		}
		
		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Size" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Size AutoScrollMargin
		{
			get => base.AutoScrollMargin;
			set => base.AutoScrollMargin = value;
		}
		
		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Size" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Size AutoScrollMinSize
		{
			get => base.AutoScrollMinSize;
			set => base.AutoScrollMinSize = value;
		}
		
		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true to enable automatic sizing; otherwise, false.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlAutoSizeDescr")]
		public override bool AutoSize
		{
			get => base.AutoSize;
			set => base.AutoSize = value;
		}
		
		/// <summary>
		/// Overridden to ensure that the background color of the <see cref="ToolStripContainer" /> reflects the background color of the <see cref="ToolStripContentPanel" />.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Color" /> structure representing the background color of the <see cref="ToolStripContentPanel" />.</returns>
		[SRCategory("CatAppearance")]
		[SRDescription("ControlBackColorDescr")]
		public override Color BackColor
		{
			get => base.BackColor;
			set => base.BackColor = value;
		}
		
		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if the control causes validation; otherwise, false.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CausesValidation
		{
			get => base.CausesValidation;
			set => base.CausesValidation = value;
		}
		
		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="DockStyle" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[SRCategory("CatLayout")]
		[DefaultValue(DockStyle.None)]
		[SRDescription("ControlDockDescr")]
		public override DockStyle Dock
		{
			get => base.Dock;
			set => base.Dock = value;
		}
		
		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Point" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Point Location
		{
			get => base.Location;
			set => base.Location = value;
		}
		
		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Size" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ControlMinimumSizeDescr")]
		public override Size MinimumSize
		{
			get => base.MinimumSize;
			set => base.MinimumSize = value;
		}
		
		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Size" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ControlMaximumSizeDescr")]
		public override Size MaximumSize
		{
			get => base.MaximumSize;
			set => base.MaximumSize = value;
		}
		
		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.String" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string Name
		{
			get => base.Name;
			set => base.Name = value;
		}		

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>An <see cref="System.Int32" />.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public int TabIndex
		{
			get => base.TabIndex;
			set => base.TabIndex = value;
		}

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if the <see cref="ToolStripContentPanel" /> can be tabbed to; otherwise, false.</returns>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool TabStop
		{
			get => base.TabStop;
			set => base.TabStop = value;
		}

		#endregion

		#region Methods

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			// TODO: Implement
		}

		/// <summary>
		/// Raises the <see cref="ToolStripContentPanel.RendererChanged" /> event. 
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripRendererChanged")]
		protected virtual void OnRendererChanged(System.EventArgs e)
		{
			if ((this.RendererChanged != null))
			{
				RendererChanged(this, e);
			}
		}

		#endregion

		#region Wisej Implementation

		protected override void OnWebEvent(WisejEventArgs e)
		{
			base.OnWebEvent(e);
		}

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender(config);
		}

		protected override void OnWebUpdate(dynamic config)
		{
			base.OnWebUpdate(config);
		}

		#endregion

	}

}