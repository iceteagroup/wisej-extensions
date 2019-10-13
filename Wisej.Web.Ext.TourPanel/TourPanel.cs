///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.TourPanel
{
	/// <summary>
	/// Provides a tour panel template that can be used to create
	/// a guided tour of an application.
	/// </summary>
	[ToolboxItem(false)]
	[DefaultEvent("Load")]
	[DesignerCategory("UserControl")]
	[Designer("Wisej.Design.UserControlDocumentDesigner, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(IRootDesigner))]
	public class TourPanel : ContainerControl, IWisejControl
	{

		// list of buttons hidden by the app.
		private Control[] hiddenButtons = null;

		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public TourPanel()
		{
			InitializeComponent();

			SetTopLevel(true);

			// determine whether the app has hidden
			// the standard buttons.
			List<Control> hidden = new List<Control>();
			if (!this.ExitButton.Visible)
				hidden.Add(ExitButton);
			if (!this.BackButton.Visible)
				hidden.Add(BackButton);
			if (!this.NextButton.Visible)
				hidden.Add(NextButton);
			if (!this.CloseButton.Visible)
				hidden.Add(CloseButton);
			if (!this.PlayButton.Visible)
				hidden.Add(PlayButton);
			this.hiddenButtons = hidden.ToArray();

			this.ExitButton.Click += ExitButton_Click;
			this.BackButton.Click += BackButton_Click;
			this.NextButton.Click += NextButton_Click;
			this.CloseButton.Click += CloseButton_Click;
			this.PlayButton.Click += PlayButton_Click;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TourPanel"/> control
		/// and assigns it to the specified <para>parent</para>.
		/// </summary>
		/// <param name="parent">The parent control that owns this tour panel.</param>
		/// <exception cref="ArgumentNullException">The value of <para>parent</para> is null.</exception>
		public TourPanel(Control parent)
		{
			if (parent == null)
				throw new ArgumentNullException(nameof(parent));

			this.Parent = parent;
		}

		#endregion

		#region Events

		#region Not Relevant

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DragDrop
		{
			add { }
			remove { }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DragEnd
		{
			add { }
			remove { }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DragEnter
		{
			add { }
			remove { }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DragOver
		{
			add { }
			remove { }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DragStart
		{
			add { }
			remove { }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler QueryContinueDrag
		{
			add { }
			remove { }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabIndexChanged
		{
			add { }
			remove { }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add { }
			remove { }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Validated
		{
			add { }
			remove { }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event CancelEventHandler Validating
		{
			add { }
			remove { }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DockChanged
		{
			add { }
			remove { }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CausesValidationChanged
		{
			add { }
			remove { }
		}

		#endregion

		/// <summary>
		/// Fired when the TourPanel shows a new step. This event
		/// is cancelable.
		/// </summary>
		public event TourPanelEventHandler BeforeStep
		{
			add { base.Events.AddHandler(nameof(BeforeStep), value); }
			remove { base.Events.RemoveHandler(nameof(BeforeStep), value); }
		}

		/// <summary>
		/// Fires the <see cref="BeforeStep"/> event.
		/// </summary>
		/// <param name="e">A <see cref="TourPanelEventArgs" /> that contains the event data. </param>
		public virtual void OnBeforeStep(TourPanelEventArgs e)
		{
			Debug.Assert(e.Step != null);

			((TourPanelEventHandler)base.Events[nameof(BeforeStep)])?.Invoke(this, e);

			e.Step.OnShow(e);
		}

		/// <summary>
		/// Fired when the current step is replaced by a new step. This
		/// event is not cancelable.
		/// </summary>
		public event TourPanelEventHandler AfterStep
		{
			add { base.Events.AddHandler(nameof(AfterStep), value); }
			remove { base.Events.RemoveHandler(nameof(AfterStep), value); }
		}

		/// <summary>
		/// Fires the <see cref="AfterStep"/> event.
		/// </summary>
		/// <param name="e">A <see cref="TourPanelEventArgs" /> that contains the event data. </param>
		public virtual void OnAfterStep(TourPanelEventArgs e)
		{
			Debug.Assert(e.Step != null);

			((TourPanelEventHandler)base.Events[nameof(AfterStep)])?.Invoke(this, e);

			e.Step.OnHide(e);
		}

		/// <summary>
		/// Fired when the TourPanel control is closed.
		/// </summary>
		[SRCategory("CatBehavior")]
		[SRDescription("Fired when the TourPanel control is closed.")]
		public event EventHandler Closed
		{
			add { base.AddHandler(nameof(Closed), value); }
			remove { base.RemoveHandler(nameof(Closed), value); }
		}

		/// <summary>
		/// Fires the <see cref="TourPanel.Closed" />
		/// event.
		/// </summary>
		/// <param name="e">A <see cref="EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClosed(EventArgs e)
		{
			((EventHandler)base.Events[nameof(Closed)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires when the TourPanel has finished showing the steps.
		/// </summary>
		public event EventHandler Ended
		{
			add { base.AddHandler(nameof(Ended), value); }
			remove { base.RemoveHandler(nameof(Ended), value); }
		}

		/// <summary>
		/// Fires the <see cref="TourPanel.Ended" /> event.
		/// </summary>
		/// <param name="e">A <see cref="EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnEnded(EventArgs e)
		{
			((EventHandler)base.Events[nameof(Ended)])?.Invoke(this, e);

			if (this.IsPlaying)
				Hide();
		}

		/// <summary>
		/// Fired when the TourPanel has been paused.
		/// </summary>
		public event EventHandler Paused
		{
			add { base.AddHandler(nameof(Paused), value); }
			remove { base.RemoveHandler(nameof(Paused), value); }
		}

		/// <summary>
		/// Fires the <see cref="TourPanel.Paused" /> event.
		/// </summary>
		/// <param name="e">A <see cref="EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnPaused(EventArgs e)
		{
			((EventHandler)base.Events[nameof(Paused)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires when the TourPanel is playing the steps automatically.
		/// </summary>
		public event EventHandler Playing
		{
			add { base.AddHandler(nameof(Playing), value); }
			remove { base.RemoveHandler(nameof(Playing), value); }
		}

		/// <summary>
		/// Fires the <see cref="TourPanel.Playing" /> event.
		/// </summary>
		/// <param name="e">A <see cref="EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnPlaying(EventArgs e)
		{
			((EventHandler)base.Events[nameof(Playing)])?.Invoke(this, e);
		}

		/// <summary>
		/// Fires when the TourPanel cannot find the target widget
		/// specified in the <see cref="TourStep.TargetName"/> property of the
		/// <see cref="CurrentStep"/>.
		/// </summary>
		/// <remarks>
		/// If this event is not handled, the default behavior is to throw an exception.
		/// </remarks>
		public event HandledEventHandler NotFound
		{
			add { base.AddHandler(nameof(NotFound), value); }
			remove { base.RemoveHandler(nameof(NotFound), value); }
		}

		/// <summary>
		/// Fires the <see cref="NotFound" /> event.
		/// </summary>
		/// <param name="e">A <see cref="HandledEventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnNotFound(HandledEventArgs e)
		{
			((HandledEventHandler)base.Events[nameof(NotFound)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		#region Not Relevant

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoScroll
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size AutoScrollMargin
		{
			get { return Size.Empty; }
			set { }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size AutoScrollMinSize
		{
			get { return Size.Empty; }
			set { }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Point AutoScrollOffset
		{
			get { return Point.Empty; }
			set { }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Point AutoScrollPosition
		{
			get { return Point.Empty; }
			set { }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ScrollBars ScrollBars
		{
			get { return ScrollBars.None; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Visible
		{
			get { return base.Visible; }
			set { base.Visible = value; }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Focusable
		{
			get { return base.Focusable; }
			set { base.Focusable = value; }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool Enabled
		{
			get { return base.Enabled; }
			set { base.Enabled = value; }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Movable
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool TabStop
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool CausesValidation
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AllowDrag
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AllowDrop
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int TabIndex
		{
			get { return -1; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override DockStyle Dock
		{
			get { return DockStyle.None; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AnchorStyles Anchor
		{
			get { return AnchorStyles.None; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AnchorStyles ResizableEdges
		{
			get { return AnchorStyles.None; }
			set { }
		}

		#endregion

		/// <summary>
		/// Determines whether the TourPanel automatically adjusts
		/// its dimension when the step changes.
		/// </summary>
		[Browsable(true)]
		[DesignerActionList]
		[DefaultValue(true)]
		[SRCategory("CatLayout")]
		[SRDescription("Determines whether the TourPanel automatically adjusts its dimension when the step changes.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new bool AutoSize
		{
			get { return this._autoSize; }
			set
			{
				if (this._autoSize != value)
				{
					this._autoSize = value;
				}
			}
		}
		private bool _autoSize = true;

		/// <summary>
		/// Determines whether the TourPanel will close automatically 
		/// when the user clicks outside of the control.
		/// </summary>
		[DesignerActionList]
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("Determines whether the TourPanel will close automatically when the user clicks outside of the control.")]
		public bool AutoClose
		{
			get { return this._autoClose; }
			set
			{
				if (this._autoClose != value)
				{
					this._autoClose = value;
					Update();
				}
			}
		}
		private bool _autoClose = false;

		/// <summary>
		/// Determines whether the TourPanel will start showing the steps automatically.
		/// </summary>
		[DesignerActionList]
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("Determines whether the TourPanel will start showing the steps automatically.")]
		public bool AutoPlay
		{
			get { return this._autoPlay; }
			set
			{
				if (this._autoPlay != value)
				{
					this._autoPlay = value;

					if (value)
						Play();
					else
						Pause();
				}
			}
		}
		private bool _autoPlay = false;

		/// <summary>
		/// Returns or sets the current <see cref="TourStep"/>.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TourStep CurrentStep
		{
			get
			{
				var index = this._selectedIndex;
				if (index > -1 && index < this.Steps.Length)
					return this.Steps[index];
				else
					return null;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				var index = Array.IndexOf(this.Steps, value);
				if (index < -1)
					throw new ArgumentException(nameof(value));

				this.SelectedIndex = index;
			}
		}

		/// <summary>
		/// Returns or sets the index of the current <see cref="TourStep"/>
		/// in the <see cref="Steps"/> collection.
		/// </summary>
		[DefaultValue(0)]
		[Browsable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("Returns or sets the index of the current TourStep.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get { return this._selectedIndex; }
			set
			{
				if (value < 0 || value > this.Steps.Length)
					throw new ArgumentOutOfRangeException(nameof(CurrentStep));

				if (this._selectedIndex != value)
				{
					ShowStep(value);
				}
			}
		}
		private int _selectedIndex = -1;

		/// <summary>
		/// Returns or sets the default <see cref="Wisej.Web.Placement"/> of the
		/// TourPanel.
		/// </summary>
		/// <remarks>
		/// Each <see cref="Wisej.Web.Ext.TourPanel.TourStep"/> can override the
		/// DefaultAlignment and change the placement of the TourStep.
		/// </remarks>
		[DefaultValue(Placement.BottomCenter)]
		[SRCategory("CatLayout")]
		[SRDescription("Returns or sets the default Placement of the TourPanel.")]
		public Placement DefaultAlignment
		{
			get { return this._defaultAlignment; }
			set
			{
				if (this._defaultAlignment != value)
				{
					this._defaultAlignment = value;
					Update();
				}
			}
		}
		private Placement _defaultAlignment = Placement.BottomCenter;

		/// <summary>
		/// Returns or sets the default offset from the target, in pixels.
		/// </summary>
		/// <remarks>
		/// Each <see cref="TourStep"/> can override the
		/// DefaultOffset and change the offset of the TourStep.
		/// </remarks>
		[SRCategory("CatLayout")]
		[SRDescription("Returns or sets the default offset from the target, in pixels.")]
		public Padding DefaultOffset
		{
			get { return this._defaultOffset; }
			set
			{
				if (this._defaultOffset != value)
				{
					this._defaultOffset = value;

					Update();
				}
			}
		}
		private Padding _defaultOffset = Padding.Empty;

		private bool ShouldSerializeOffset()
		{
			return !this.DefaultOffset.IsEmpty;
		}

		private void ResetDefaultOffset()
		{
			this.DefaultOffset = Padding.Empty;
		}

		/// <summary>
		/// Returns or sets the default number of seconds before showing the next step
		/// when the <see cref="TourPanel.AutoPlay"/> property
		/// is set to true.
		/// </summary>
		/// <remarks>
		/// Each <see cref="TourStep"/> can override the
		/// DefaultAutoPlayTime and change the AutoPlayTime of the TourStep.
		/// </remarks>
		/// <exception cref="System.ArgumentException">
		/// When the value is less than 1.
		/// </exception>
		[DefaultValue(5)]
		[SRCategory("CatLayout")]
		[SRDescription("Returns or sets the default number of seconds before showing the next step.")]
		public int DefaultAutoPlayTime
		{
			get { return this._defaultAutoPlayTime; }
			set
			{
				if (value < 1)
					throw new ArgumentException(nameof(DefaultAutoPlayTime));

				if (this._defaultAutoPlayTime != value)
				{
					this._defaultAutoPlayTime = value;
					this.AutoPlayTimer.Interval = value * 1000;
				}
			}
		}
		private int _defaultAutoPlayTime = 5;

		/// <summary>
		/// Determines whether the 
		/// <see cref="TourPanel"/>
		/// shows the <see cref="TourPanel.CloseButton"/> and <see cref="TourPanel.ExitButton"/> buttons.
		/// </summary>
		/// <remarks>
		/// The <see cref="TourPanel.ExitButton"/> is always shown on the last step.
		/// </remarks>
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("Determines whether the TourPanel shows a close button.")]
		public bool DefaultShowClose
		{
			get { return this._defaultShowClose; }
			set
			{
				if (this._defaultShowClose != value)
				{
					this._defaultShowClose = value;
					Update();
				}
			}
		}
		private bool _defaultShowClose = true;

		/// <summary>
		/// Collection of the steps to show in this TourPanel.
		/// </summary>
		[DesignerActionList]
		[SRCategory("CatBehavior")]
		[SRDescription("Collection of the steps to show in this TourPanel.")]
		public TourStep[] Steps
		{
			get { return this._steps ?? _emptySteps; }
			set
			{
				if (this._steps != value)
				{
					var old = this._steps;
					if (old != null && old.Length > 0)
					{
						foreach (var s in old)
							s.Tour = null;
					}

					if (value != null && value.Length > 0)
					{
						foreach (var s in value)
							s.Tour = this;
					}
					this._steps = value;

					Update();
				}
			}
		}
		private TourStep[] _steps;
		private static readonly TourStep[] _emptySteps = new TourStep[0];

		private bool ShouldSerializeSteps()
		{
			return this._steps != null && this._steps.Length > 0;
		}

		private void ResetSteps()
		{
			this.Steps = null;
		}

		/// <summary>
		/// Returns or sets a value indicating whether the current target is highlighted.
		/// </summary>
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("Returns or sets a value indicating whether the current target is highlighted.")]
		public bool HighlightTarget
		{
			get { return this._highlightTarget; }
			set
			{
				if (this._highlightTarget != value)
				{
					this._highlightTarget = value;
					Update();
				}
			}
		}
		private bool _highlightTarget = true;

		/// <summary>
		/// Returns or sets the color index for the highlight mask.
		/// Uses the highlightColor set in the theme when this property is <see cref="Color.Empty"/>.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[SRCategory("CatAppearance")]
		[SRDescription(" Returns or sets the color index for the highlighter mask.")]
		public Color HighlightColor
		{
			get { return this._highlightColor; }
			set
			{
				if (this._highlightColor != value)
				{
					this._highlightColor = value;
					Update();
				}
			}
		}
		private Color _highlightColor;

		/// <summary>
		/// Returns whether the TourPanel is currently playing the steps.
		/// </summary>
		[Browsable(false)]
		public bool IsPlaying
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns true if the current step is the first. 
		/// </summary>
		[Browsable(false)]
		private bool IsFirstStep
		{
			get
			{
				foreach (var s in this.Steps)
				{
					if (!s.Enabled)
						continue;

					return s == this.CurrentStep;
				}

				return false;
			}
		}

		/// <summary>
		/// Returns true if the current step is the last one.
		/// </summary>
		[Browsable(false)]
		private bool IsLastStep
		{
			get
			{
				bool last = true;
				foreach (var s in this.Steps)
				{
					if (!s.Enabled)
						continue;

					last = s == this.CurrentStep;
				}

				return last;
			}
		}

		#endregion

		#region Methods

		private void CloseButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void BackButton_Click(object sender, EventArgs e)
		{
			Back();
		}

		private void PlayButton_Click(object sender, EventArgs e)
		{
			if (this.IsPlaying)
				Pause();
			else
				Play();
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			Next();
		}

		private void AutoPlayTimer_Tick(object sender, EventArgs e)
		{
			Next();
		}

		/// <summary>
		/// Shows the first step.
		/// </summary>
		public void First()
		{
			var steps = this.Steps;
			for (int i = 0; i < steps.Length; i++)
			{
				if (steps[i].Enabled)
				{
					this.SelectedIndex = i;
					return;
				}
			}
		}

		/// <summary>
		/// Shows the next step
		/// </summary>
		public void Next()
		{
			var steps = this.Steps;
			for (int i = this.SelectedIndex + 1; i < steps.Length; i++)
			{
				if (steps[i].Enabled)
				{
					this.SelectedIndex = i;
					return;
				}
			}

			OnEnded(EventArgs.Empty);
		}

		/// <summary>
		/// Shows the previous step.
		/// </summary>
		public void Back()
		{
			var steps = this.Steps;
			for (int i = this.SelectedIndex - 1; i >= 0; i--)
			{
				if (steps[i].Enabled)
				{
					this.SelectedIndex = i;
					return;
				}
			}
		}

		/// <summary>
		/// Starts auto-advancing the steps.
		/// </summary>
		public void Play()
		{
			if (this.IsPlaying)
				return;

			// restart?
			if (this.SelectedIndex == this.Steps.Length - 1)
				First();

			this.IsPlaying = true;
			this.AutoPlayTimer.Start();
			this.PlayButton.ImageSource = "icon-pause";

			OnPlaying(EventArgs.Empty);
		}

		/// <summary>
		/// Pauses auto-advancing the steps.
		/// </summary>
		public void Pause()
		{
			if (!this.IsPlaying)
				return;

			this.IsPlaying = false;
			this.AutoPlayTimer.Stop();
			this.PlayButton.ImageSource = "icon-play";

			OnPaused(EventArgs.Empty);
		}

		// Returns the internal auto-play timer.
		private Timer AutoPlayTimer
		{
			get
			{
				if (this._autoPlayTimer == null)
				{
					this._autoPlayTimer = new Timer();
					this._autoPlayTimer.Tick += AutoPlayTimer_Tick;
					this._autoPlayTimer.Interval = this.DefaultAutoPlayTime * 1000;
				}

				return this._autoPlayTimer;
			}
			set
			{
				if (this._autoPlayTimer != null)
					this._autoPlayTimer.Stop();

				this._autoPlayTimer = value;
			}
		}
		private Timer _autoPlayTimer = null;

		/// <summary>
		/// Shows the <see cref="TourPanel"/> without a container.
		/// </summary>
		public new void Show()
		{
			Show(null);
		}

		/// <summary>
		/// Shows the <see cref="TourPanel"/> for the specified container control.
		/// </summary>
		/// <param name="container">
		/// The <see cref="ContainerControl"/> that hosts the controls
		/// that are the target for this <see cref="TourPanel"/>.
		/// </param>
		public void Show(ContainerControl container)
		{
			this.Container = container;

			First();

			if (!this.ContainsFocus)
			{
				if (this.NextButton.Visible)
					this.NextButton.Focus();
				else if (this.BackButton.Visible)
					this.BackButton.Focus();
			}

			if (this.AutoPlay)
				Play();
		}

		/// <summary>
		/// Returns the <see cref="ContainerControl"/> that this TourPanel
		/// is attached to.
		/// </summary>
		public new ContainerControl Container
		{
			get;
			private set;
		}

		/// <summary>
		/// Closes the <see cref="TourPanel"/>.
		/// </summary>
		public virtual void Close()
		{
			OnEnded(EventArgs.Empty);
			Hide();
		}

		/// <summary>
		/// Disposes the TourPanel.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{

			if (disposing)
			{
				this._autoPlayTimer?.Dispose();
			}

			this.Container = null;
			this.AutoPlayTimer = null;

			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TourPanel));
			this.HtmlText = new Wisej.Web.Ext.TourPanel.TourPanel.HtmlPanel();
			this.TitleLabel = new Wisej.Web.Ext.TourPanel.TourPanel.Label();
			this.CloseButton = new Wisej.Web.Button();
			this.ButtonsPanel = new Wisej.Web.FlowLayoutPanel();
			this.NextButton = new Wisej.Web.Button();
			this.BackButton = new Wisej.Web.Button();
			this.ExitButton = new Wisej.Web.Button();
			this.PlayButton = new Wisej.Web.Button();
			this.TitlePanel = new Wisej.Web.Panel();
			this.ButtonsPanel.SuspendLayout();
			this.TitlePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// HtmlText
			// 
			resources.ApplyResources(this.HtmlText, "HtmlText");
			this.HtmlText.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
			this.HtmlText.Name = "HtmlText";
			// 
			// TitleLabel
			// 
			this.TitleLabel.AppearanceKey = "tourpanel/title";
			resources.ApplyResources(this.TitleLabel, "TitleLabel");
			this.TitleLabel.Name = "TitleLabel";
			// 
			// CloseButton
			// 
			this.CloseButton.AppearanceKey = "tourpanel/close";
			this.CloseButton.Display = Wisej.Web.Display.Icon;
			resources.ApplyResources(this.CloseButton, "CloseButton");
			this.CloseButton.Name = "CloseButton";
			// 
			// ButtonsPanel
			// 
			resources.ApplyResources(this.ButtonsPanel, "ButtonsPanel");
			this.ButtonsPanel.Controls.Add(this.NextButton);
			this.ButtonsPanel.Controls.Add(this.BackButton);
			this.ButtonsPanel.Controls.Add(this.ExitButton);
			this.ButtonsPanel.Controls.Add(this.PlayButton);
			this.ButtonsPanel.Name = "ButtonsPanel";
			// 
			// NextButton
			// 
			this.NextButton.AppearanceKey = "tourpanel/next";
			resources.ApplyResources(this.NextButton, "NextButton");
			this.NextButton.Name = "NextButton";
			// 
			// BackButton
			// 
			this.BackButton.AppearanceKey = "tourpanel/back";
			resources.ApplyResources(this.BackButton, "BackButton");
			this.BackButton.Name = "BackButton";
			// 
			// ExitButton
			// 
			this.ExitButton.AppearanceKey = "tourpanel/exit";
			resources.ApplyResources(this.ExitButton, "ExitButton");
			this.ExitButton.Name = "ExitButton";
			// 
			// PlayButton
			// 
			this.PlayButton.AppearanceKey = "tourpanel/play";
			resources.ApplyResources(this.PlayButton, "PlayButton");
			this.PlayButton.Name = "PlayButton";
			// 
			// TitlePanel
			// 
			this.TitlePanel.Controls.Add(this.TitleLabel);
			this.TitlePanel.Controls.Add(this.CloseButton);
			resources.ApplyResources(this.TitlePanel, "TitlePanel");
			this.TitlePanel.Name = "TitlePanel";
			// 
			// TourPanel
			// 
			this.Controls.Add(this.HtmlText);
			this.Controls.Add(this.TitlePanel);
			this.Controls.Add(this.ButtonsPanel);
			resources.ApplyResources(this, "$this");
			this.Name = "TourPanel";
			this.ButtonsPanel.ResumeLayout(false);
			this.TitlePanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		/// <summary>
		/// This is the <see cref="HtmlPanel"/> that shows the
		/// current step's <see cref="TourStep.Text"/>.
		/// </summary>
		public HtmlPanel HtmlText;

		/// <summary>
		/// This is the <see cref="Label"/> that shows the
		/// current step's <see cref="TourStep.Title"/>.
		/// </summary>
		public Label TitleLabel;

		/// <summary>
		/// The close <see cref="Button"/> at the top right of the
		/// <see cref="TourPanel"/>. It's hidden
		/// when the property <see cref="TourStep.ShowClose"/> is false.
		/// </summary>
		public Button CloseButton;

		/// <summary>
		/// A <see cref="FlowLayoutPanel"/> that contains
		/// all the tour buttons.
		/// </summary>
		public FlowLayoutPanel ButtonsPanel;

		/// <summary>
		/// The play <see cref="Button"/>. It starts the auto playing of the steps.
		/// </summary>
		public Button PlayButton;

		/// <summary>
		/// The next <see cref="Button"/>. Shows the next step.
		/// </summary>
		public Button NextButton;

		/// <summary>
		/// The play <see cref="Button"/>.
		/// </summary>
		public Button BackButton;

		/// <summary>
		/// The back <see cref="Button"/>. Shows the previous step.
		/// </summary>
		public Button ExitButton;

		/// <summary>
		/// A <see cref="Panel"/> that contains the TitleLabel and the 
		/// Close button.
		/// </summary>
		public Panel TitlePanel;

		// Shows the indicated step.
		private void ShowStep(int index)
		{
			// fire AfterStep when leaving the current step.
			if (this.CurrentStep != null)
				OnAfterStep(new TourPanelEventArgs(this.CurrentStep, this.SelectedIndex));

			var steps = this.Steps;
			if (index < 0 || index >= steps.Length)
				return;

			var step = steps[index];

			var args = new TourPanelEventArgs(step, index);
			OnBeforeStep(args);
			if (args.Cancel)
			{
				// auto close when canceling the first step.
				if (this.SelectedIndex == 0)
					Close();

				return;
			}

			// exit if the OnBeforeStep handler changed the current step
			// which caused a recursion to ShowStep and ha already updated the step.
			if (args.StepIndex != index)
				return;

			this._selectedIndex = index;

			// find the target control associated with the step.
			if (!FindTarget(step))
				return;

			// show the target, it may be inside a TabPanel, could be scrolled out of the way, could
			// be in a collapsed panel, etc.
			ShowTarget(step);

			if (this.AutoSize && !this.DesignMode)
			{
				TextUtils.MeasureText(step.Text, true, this.HtmlText.Font, (size) =>
				{
					Update(step);
					AdjustSize(size);
				});
			}
			else
			{
				Update(step);
			}
		}

		// Updates the placement and offset using
		// the values specified by the TourStep.
		internal void Update(TourStep step)
		{
			Debug.Assert(step != null);

			if (!this.Visible)
				return;

			if (this.DesignMode)
			{
				this.HtmlText.Html = step.Text;
				this.TitleLabel.Text = step.Title;
				return;
			}

			var showClose = step.ShouldSerializeShowClose() ? step.ShowClose : this.DefaultShowClose;

			this.Visible = true;
			this.HtmlText.Html = step.Text;
			this.TitleLabel.Text = step.Title;
			this.CloseButton.Visible = showClose;
			this.ExitButton.Visible = showClose || this.IsLastStep;

			// adjust the default navigation buttons.
			if (!IsHidden(this.BackButton))
				this.BackButton.Enabled = !this.IsFirstStep;
			if (!IsHidden(this.NextButton))
				this.NextButton.Enabled = !this.IsLastStep;

			// place the panel according to the alignment and offset.
			var target = step.Target;
			var allowPointerEvents = step.AllowPointerEvents;
			var offset = step.ShouldSerializeOffset() ? step.Offset : this.DefaultOffset;
			var alignment = step.ShouldSerializeAlignment() ? step.Alignment : this.DefaultAlignment;
			Call("place", target, alignment, offset, allowPointerEvents);

			// update the AutoPlay timer.
			if (this.IsPlaying)
			{
				this.AutoPlayTimer.Interval =
					1000 * (step.AutoPlayTime > 0 ? step.AutoPlayTime : this.DefaultAutoPlayTime);
			}
		}

		// Checks if the button has been hidden in the template.
		private bool IsHidden(Control button)
		{
			return Array.IndexOf(this.hiddenButtons, button) > -1;
		}

		// Adjusts the panel size maintaining calculating the
		// size from the inner html size calculated using the html text.
		private void AdjustSize(Size htmlSize)
		{
			var panelSize = this.HtmlText.Size;
			var offset = this.Size - panelSize;
			var newSize = htmlSize + offset;

			// make sure it doesn't exceed the browser size.
			var maxSize = Application.Browser.Size;
			newSize.Width = Math.Min(newSize.Width, maxSize.Width);
			newSize.Height = Math.Min(newSize.Height, maxSize.Height);

			this.Size = newSize;
		}

		// Returns the target identified by the path.
		private bool FindTarget(TourStep step)
		{
			Debug.Assert(step != null);

			if (step.Target != null)
				return true;

			var path = step.TargetName;
			var container = this.Container;
			if (container == null && String.IsNullOrEmpty(path))
				return true;

			// remove the child widgets and children index.
			var children = "";
			var pos = path.IndexOfAny("/[".ToCharArray());
			if (pos > -1)
			{
				children = path.Substring(pos);
				path = path.Substring(0, pos);
			}

			// find nested targets: control1.control2....
			var parts = path.Split('.');
			IWisejComponent component = container;
			foreach (string p in parts)
			{
				if (component is Control)
				{
					var control = (Control)component;
					IWisejComponent child = control.Controls[p];

					// find child items.
					if (child == null)
					{
						if (component is DataGridView)
							child = ((DataGridView)component).Columns[p];
						else if (component is ListView)
							child = ((ListView)component).Columns[p];
						else if (component is MenuBar)
							child = ((MenuBar)component).MenuItems[p];
						else if (component is Form && p == "menu")
							child = ((Form)component).Menu;
						else if (component is Form && ((Form)component).IsMdiContainer)
							child = ((Form)component).MdiChildren.FirstOrDefault(f => f.Name == p);
					}
					component = child;
				}
				else if (component is MainMenu)
				{
					component = ((MainMenu)component).MenuItems[p];
				}
				else
				{
					if (container == null)
					{
						// when the container is null or a desktop, the first name
						// in the page can be an open window.
						component = Application.OpenForms[p];
						if (component == null)
							component = Application.OpenPages[p];
						if (component == null)
						{
							switch (p)
							{
								case "Desktop":
									component = Application.Desktop;
									break;
								case "MainPage":
									component = Application.MainPage;
									break;
							}
						}
					}
					else
					{
						component = container.Controls[p];
					}

					// find specially named children.
					if (component == null)
					{
						switch (p)
						{
							case "menu":
								if (container is Form)
									component = ((Form)container).Menu;
								break;
						}
					}
				}

				if (component == null)
					break;

				if (component is Control)
					((Control)component).CreateControl();
			}

			if (component != null)
			{
				// add back the child name and children index.
				step.Target = component.Handle + children;

				// save the component, will be needed in ShowTarget.
				step.TargetComponent = component;
			}

			// failed to find the target?
			if (step.Target == null && !String.IsNullOrEmpty(step.TargetName))
			{
				FireTargetNotFound();
				return false;
			}

			return true;
		}

		// Returns the target identified by the path.
		private void ShowTarget(TourStep step)
		{
			Debug.Assert(step != null);

			if (step.TargetComponent == null)
				return;

			IWisejComponent component = step.TargetComponent;
			do
			{
				try
				{

					if (component is TabPage)
						((TabPage)component).TabControl?.SelectTab((TabPage)component);
					else if (component is AccordionPanel)
						((AccordionPanel)component).Accordion?.SelectPanel((AccordionPanel)component);
					else if (component is Panel)
						((Panel)component).Collapsed = false;
					else if (component is GroupBox)
						((GroupBox)component).Collapsed = false;
					else if (component is Form)
						((Form)component).Activate();
					else if (component is DataGridViewColumn)
						((DataGridViewColumn)component).DataGridView?.ScrollColumnIntoView(((DataGridViewColumn)component));
					else if (component is ColumnHeader)
						((ColumnHeader)component).ListView?.ScrollColumnIntoView(((ColumnHeader)component));

					if (component is Control)
					{
						var control = (Control)component;
						control.Show();
						control.ScrollControlIntoView();

						// go up the hierarchy.
						component = control.Parent;
					}
					else
					{
						break;
					}
				}
				catch (Exception ex)
				{
					LogManager.Log(ex);
					break;
				}
			}
			while (component != null);
		}

		// Fires the NotFound event.
		private bool FireTargetNotFound()
		{
			var args = new HandledEventArgs();
			OnNotFound(args);

			if (!args.Handled)
			{
				Close();
				throw new Exception("Target '" + (this.CurrentStep?.TargetName ?? "null") + "' not found.");
			}

			return args.Handled;
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Returns the theme appearance key for this control.
		/// </summary>
		string IWisejControl.AppearanceKey
		{
			get
			{
				return this.AppearanceKey ?? "tourpanel";
			}
		}

		void IWisejComponent.UpdateWidget()
		{
			Update();

			// update the position of the tour when the
			// browser is refreshed while a tour is being displayed.
			if (this.Visible && this.CurrentStep != null)
				Update(this.CurrentStep);
		}

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "disappear":
					this.Visible = false;
					OnClosed(EventArgs.Empty);
					break;

				case "notfound":
					FireTargetNotFound();
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

			config.className = "wisej.web.ext.TourPanel";
			config.topLevel = true;
			config.autoHide = this.AutoClose;
			config.highlightColor = this.HighlightColor;
			config.highlightTarget = this.HighlightTarget;
			config.container = ((IWisejComponent)this.Container)?.Id;

			config.wiredEvents.Add("disappear", "notfound");
		}

		/// <exclude/>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void RenderScrollableProperties(dynamic config)
		{
			// suppress all Wisej.Web.ScrollableControl specific properties.
		}

		#endregion

		#region Child Classes

		/// <summary>
		/// Label.
		/// </summary>
		/// <exclude/>
		public class Label : Wisej.Web.Label
		{
			/// <summary>
			/// Text property.
			/// </summary>
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public override string Text
			{
				get { return base.Text; }
				set { base.Text = value; }
			}
		}

		/// <summary>
		/// HtmlPanel inside the TourPanel.
		/// </summary>
		/// <exclude/>
		public class HtmlPanel : Wisej.Web.HtmlPanel
		{
			/// <summary>
			/// Html text.
			/// </summary>
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public new string Html
			{
				get { return base.Html; }
				set { base.Html = value; }
			}

			/// <summary>
			/// Anchor property.
			/// </summary>
			[DefaultValue(AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom)]
			public override AnchorStyles Anchor
			{
				get { return base.Anchor; }
				set { base.Anchor = value; }
			}
		}

		#endregion

	}
}