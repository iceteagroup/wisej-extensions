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
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.TourPanel
{
	/// <summary>
	/// Provides a tour panel template that can be used to create
	/// a guided tour of an application.
	/// </summary>
	/// <remarks>
	/// <para>
	/// A <see cref="TourPanel"/> is a top-level popup that displays the <see cref="TourStep.Title"/> and
	/// <see cref="TourStep.Text"/> of each <see cref="TourStep"/> in the <see cref="Steps"/> array next to the
	/// step's target control, optionally highlighting the target with a mask that covers the rest of the page.
	/// </para>
	/// <para>
	/// The usual approach is to add a new class that inherits from <see cref="TourPanel"/> to the project, design
	/// it in the Visual Studio designer (change the buttons, colors, layout) and define the steps in the
	/// <see cref="Steps"/> property. The tour is then started calling <see cref="Show(ContainerControl)"/>.
	/// </para>
	/// </remarks>
	/// <example>
	/// Creating a tour in code and starting it for the current form:
	/// <code><![CDATA[
	/// private void buttonHelp_Click(object sender, EventArgs e)
	/// {
	///     var tour = new TourPanel();
	///     tour.Steps = new[]
	///     {
	///         new TourStep { Title = "Welcome", Text = "This short tour shows you the main features." },
	///         new TourStep { Title = "Search", Text = "Type here to search the <b>customers</b>.", TargetName = "textBoxSearch" },
	///         new TourStep { Title = "Save", Text = "Click to save your changes.", TargetName = "panelTools.buttonSave", Alignment = Placement.RightMiddle }
	///     };
	///     tour.Ended += (s, args) => AlertBox.Show("Tour completed!");
	///     tour.Show(this);
	/// }
	/// ]]></code>
	/// </example>
	[ToolboxItem(false)]
	[DefaultEvent("Load")]
	[DesignerCategory("UserControl")]
	[Designer("Wisej.Design.UserControlDocumentDesigner, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(IRootDesigner))]
	[ApiCategory("TourPanel")]
	public class TourPanel : ContainerControl, IWisejControl
	{

		// list of buttons hidden by the app.
		private Control[] hiddenButtons = null;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TourPanel"/> class.
		/// </summary>
		/// <remarks>
		/// The standard buttons (<see cref="ExitButton"/>, <see cref="BackButton"/>, <see cref="NextButton"/>,
		/// <see cref="CloseButton"/> and <see cref="PlayButton"/>) that are hidden in the designer of a derived class are
		/// never shown by the <see cref="TourPanel"/>.
		/// </remarks>
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
		/// and assigns it to the specified <paramref name="parent"/>.
		/// </summary>
		/// <param name="parent">The parent control that owns this tour panel.</param>
		/// <exception cref="ArgumentNullException">The value of <paramref name="parent"/> is null.</exception>
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
		/// <remarks>
		/// After the <see cref="BeforeStep"/> handlers run, this method also fires the <see cref="TourStep.Show"/> event
		/// of the step in <see cref="TourPanelEventArgs.Step"/> and sets its <see cref="TourStep.IsVisible"/> to true.
		/// Setting <see cref="CancelEventArgs.Cancel"/> to true in a handler prevents the step from being shown.
		/// It's called by the <see cref="TourPanel"/> when changing steps; override it in a derived class to customize the behavior.
		/// </remarks>
		/// <example>
		/// Overriding the method in a custom tour to load the content of a step on demand:
		/// <code><![CDATA[
		/// public class MyTour : TourPanel
		/// {
		///     public override void OnBeforeStep(TourPanelEventArgs e)
		///     {
		///         if (String.IsNullOrEmpty(e.Step.Text))
		///             e.Step.Text = LoadHelpText(e.Step.TargetName);
		///
		///         base.OnBeforeStep(e);
		///     }
		/// }
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// After the <see cref="AfterStep"/> handlers run, this method also fires the <see cref="TourStep.Hide"/> event
		/// of the step in <see cref="TourPanelEventArgs.Step"/> and sets its <see cref="TourStep.IsVisible"/> to false.
		/// It's called by the <see cref="TourPanel"/> when leaving a step; override it in a derived class to customize the behavior.
		/// </remarks>
		/// <example>
		/// Overriding the method in a custom tour to track the steps the user has seen:
		/// <code><![CDATA[
		/// public class MyTour : TourPanel
		/// {
		///     public override void OnAfterStep(TourPanelEventArgs e)
		///     {
		///         base.OnAfterStep(e);
		///
		///         Application.Session.lastTourStep = e.StepIndex;
		///     }
		/// }
		/// ]]></code>
		/// </example>
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
		/// Returns or sets whether the <see cref="TourPanel"/> automatically adjusts
		/// its size when the step changes.
		/// </summary>
		/// <remarks>
		/// When true, the size is calculated asynchronously by measuring the HTML in <see cref="TourStep.Text"/>
		/// using the font of <see cref="HtmlText"/>, and it's limited to the size of the browser.
		/// </remarks>
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
		/// Returns or sets whether the <see cref="TourPanel"/> closes automatically
		/// when the user clicks outside of the control.
		/// </summary>
		/// <remarks>
		/// When the panel is closed this way, the <see cref="Closed"/> event is fired but <see cref="Ended"/> is not.
		/// </remarks>
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
		/// Returns or sets whether the <see cref="TourPanel"/> advances the steps automatically.
		/// </summary>
		/// <remarks>
		/// Setting this property to true immediately calls <see cref="Play"/> and setting it to false calls <see cref="Pause"/>.
		/// When true, <see cref="Show(ContainerControl)"/> also starts playing. The time each step is displayed is set by
		/// <see cref="TourStep.AutoPlayTime"/> or <see cref="DefaultAutoPlayTime"/>.
		/// </remarks>
		/// <example>
		/// Running a tour that advances every 3 seconds:
		/// <code><![CDATA[
		/// var tour = new MyTour();
		/// tour.DefaultAutoPlayTime = 3;
		/// tour.Show(this);
		/// tour.AutoPlay = true;
		/// ]]></code>
		/// </example>
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
		/// <exception cref="ArgumentNullException">The value is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The value is not in the <see cref="Steps"/> array.</exception>
		/// <remarks>
		/// Returns null when no step is displayed. Setting this property is equivalent to setting <see cref="SelectedIndex"/>
		/// to the index of the step in <see cref="Steps"/>, which fires <see cref="AfterStep"/> and <see cref="BeforeStep"/>.
		/// </remarks>
		/// <example>
		/// Jumping to a specific step:
		/// <code><![CDATA[
		/// this.tour.CurrentStep = this.tour.Steps.First(s => s.TargetName == "buttonSave");
		/// ]]></code>
		/// </example>
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
		/// <exception cref="ArgumentOutOfRangeException">The value is less than 0 or greater than the length of <see cref="Steps"/>.</exception>
		/// <remarks>
		/// The value is -1 before the first step is shown. Setting a new index fires <see cref="AfterStep"/> for the current step
		/// and <see cref="BeforeStep"/> for the new one, then resolves the target, makes it visible (selecting its tab page,
		/// expanding its panel, scrolling it into view, etc.) and places the panel next to it. The new step is shown even if
		/// <see cref="TourStep.Enabled"/> is false; use <see cref="Next"/> and <see cref="Back"/> to skip disabled steps.
		/// If the <see cref="BeforeStep"/> event is canceled, the index doesn't change.
		/// </remarks>
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
		/// <see cref="TourPanel"/> in relation to the target of the step.
		/// </summary>
		/// <remarks>
		/// Each <see cref="TourStep"/> can override the default value using the <see cref="TourStep.Alignment"/> property.
		/// The panel is moved to a different side automatically when there isn't enough space in the browser.
		/// Steps without a target are always shown at the top of the <see cref="Container"/>.
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
		/// The <see cref="Padding"/> sides are used according to the placement: for example <see cref="Padding.Top"/> adds space
		/// between the target and a panel placed below it, and <see cref="Padding.Left"/> between the target and a panel placed
		/// at its right. Each <see cref="TourStep"/> can override the default value using the <see cref="TourStep.Offset"/> property.
		/// </remarks>
		/// <example>
		/// Leaving a 10 pixels gap between the panel and the target:
		/// <code><![CDATA[
		/// this.tour.DefaultOffset = new Padding(10);
		/// ]]></code>
		/// </example>
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
		/// when the <see cref="TourPanel"/> is playing the steps automatically
		/// (see <see cref="AutoPlay"/> and <see cref="Play"/>).
		/// </summary>
		/// <remarks>
		/// Each <see cref="TourStep"/> can override the default value using the <see cref="TourStep.AutoPlayTime"/> property.
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
		/// Returns or sets whether the <see cref="TourPanel"/>
		/// shows the <see cref="CloseButton"/> and <see cref="ExitButton"/> buttons.
		/// </summary>
		/// <remarks>
		/// The <see cref="ExitButton"/> is always shown on the last step. Each <see cref="TourStep"/> can override
		/// the default value using the <see cref="TourStep.ShowClose"/> property.
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
		/// Returns or sets the steps to show in this <see cref="TourPanel"/>.
		/// </summary>
		/// <remarks>
		/// Never returns null: when no steps are set it returns an empty array. Assigning a new array sets the
		/// <see cref="TourStep.Tour"/> property of each step; since the value is an array, replacing or adding
		/// single elements doesn't update the owner of the steps, so always assign a new array.
		/// Steps with <see cref="TourStep.Enabled"/> set to false are skipped by <see cref="First"/>, <see cref="Next"/> and <see cref="Back"/>.
		/// </remarks>
		/// <example>
		/// Adding a step to an existing tour:
		/// <code><![CDATA[
		/// var steps = this.tour.Steps.ToList();
		/// steps.Add(new TourStep { Title = "Reports", Text = "Open the reports from here.", TargetName = "menuBar1.menuReports" });
		/// this.tour.Steps = steps.ToArray();
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// When true, the page is covered by a mask of <see cref="HighlightColor"/> with a hole over the target of the current step.
		/// When false and <see cref="TourStep.AllowPointerEvents"/> is also false, a transparent mask still blocks the pointer events on the page.
		/// </remarks>
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
		/// Returns or sets the color of the highlight mask.
		/// </summary>
		/// <remarks>
		/// Uses the highlightColor set in the theme when this property is <see cref="Color.Empty"/>.
		/// Use a semi-transparent color to let the user see the page under the mask.
		/// </remarks>
		/// <example>
		/// Using a semi-transparent dark mask:
		/// <code><![CDATA[
		/// this.tour.HighlightTarget = true;
		/// this.tour.HighlightColor = Color.FromArgb(128, 0, 0, 0);
		/// ]]></code>
		/// </example>
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
		/// Returns whether the <see cref="TourPanel"/> is currently playing the steps automatically.
		/// </summary>
		/// <remarks>
		/// The value is changed by <see cref="Play"/> and <see cref="Pause"/>, and by the play button.
		/// </remarks>
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
		/// <remarks>
		/// Shows the first step in <see cref="Steps"/> with <see cref="TourStep.Enabled"/> set to true.
		/// Nothing happens if there are no enabled steps or if the first enabled step is already the current step.
		/// </remarks>
		/// <example>
		/// Restarting the tour from the beginning:
		/// <code><![CDATA[
		/// private void buttonRestart_Click(object sender, EventArgs e)
		/// {
		///     this.tour.First();
		/// }
		/// ]]></code>
		/// </example>
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
		/// Shows the next step.
		/// </summary>
		/// <remarks>
		/// Skips the steps with <see cref="TourStep.Enabled"/> set to false. When there are no more steps it fires the
		/// <see cref="Ended"/> event and, if the tour is playing, hides the <see cref="TourPanel"/>.
		/// </remarks>
		/// <example>
		/// Advancing the tour when the user completes the action described in the current step:
		/// <code><![CDATA[
		/// private void textBoxSearch_TextChanged(object sender, EventArgs e)
		/// {
		///     if (this.tour.CurrentStep?.TargetName == "textBoxSearch")
		///         this.tour.Next();
		/// }
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// Skips the steps with <see cref="TourStep.Enabled"/> set to false. Nothing happens when the current step is the first enabled step.
		/// </remarks>
		/// <example>
		/// Going back one step from a custom button:
		/// <code><![CDATA[
		/// private void buttonPrevious_Click(object sender, EventArgs e)
		/// {
		///     this.tour.Back();
		/// }
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// Starts a timer that calls <see cref="Next"/> after the number of seconds set in <see cref="TourStep.AutoPlayTime"/>
		/// or <see cref="DefaultAutoPlayTime"/>, sets <see cref="IsPlaying"/>, changes the icon of the <see cref="PlayButton"/>
		/// to "icon-pause" and fires the <see cref="Playing"/> event. If the current step is the last one, the tour restarts from
		/// the first step. Nothing happens if the tour is already playing.
		/// </remarks>
		/// <example>
		/// Showing a tour and playing it automatically:
		/// <code><![CDATA[
		/// var tour = new MyTour();
		/// tour.Show(this);
		/// tour.Play();
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// Stops the timer, resets <see cref="IsPlaying"/>, changes the icon of the <see cref="PlayButton"/>
		/// to "icon-play" and fires the <see cref="Paused"/> event. Nothing happens if the tour is not playing.
		/// Call <see cref="Play"/> to resume from the current step.
		/// </remarks>
		/// <example>
		/// Pausing the tour while the user interacts with a target:
		/// <code><![CDATA[
		/// private void textBoxSearch_Enter(object sender, EventArgs e)
		/// {
		///     if (this.tour.IsPlaying)
		///         this.tour.Pause();
		/// }
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// Without a container, the first name in <see cref="TourStep.TargetName"/> is resolved as the name of an open
		/// <see cref="Form"/> or <see cref="Page"/>, or as "Desktop" or "MainPage". All the steps should specify a
		/// <see cref="TourStep.TargetName"/> or a <see cref="TourStep.Target"/>, otherwise the <see cref="NotFound"/> event is fired.
		/// </remarks>
		/// <example>
		/// Touring controls in different windows:
		/// <code><![CDATA[
		/// var tour = new TourPanel();
		/// tour.Steps = new[]
		/// {
		///     new TourStep { Title = "Navigation", Text = "Use this menu to navigate.", TargetName = "MainPage.navigationBar1" },
		///     new TourStep { Title = "Customer", Text = "Edit the customer here.", TargetName = "CustomerForm.textBoxName" }
		/// };
		/// tour.Show();
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// The <see cref="TourStep.TargetName"/> of each step is resolved starting from the controls in <paramref name="container"/>,
		/// and steps without a target are shown at the top of the <paramref name="container"/>.
		/// This method sets <see cref="Container"/>, shows the first enabled step, moves the focus to the <see cref="NextButton"/>
		/// (or the <see cref="BackButton"/>) and starts playing when <see cref="AutoPlay"/> is true.
		/// </remarks>
		/// <example>
		/// Starting the tour of the current page:
		/// <code><![CDATA[
		/// private void Page1_Load(object sender, EventArgs e)
		/// {
		///     var tour = new MyTour();
		///     tour.Show(this);
		/// }
		/// ]]></code>
		/// </example>
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
		/// Returns the <see cref="ContainerControl"/> that this <see cref="TourPanel"/>
		/// is attached to.
		/// </summary>
		/// <remarks>
		/// The value is set by <see cref="Show(ContainerControl)"/> and it's null when the tour was started with <see cref="Show()"/>.
		/// </remarks>
		public new ContainerControl Container
		{
			get;
			private set;
		}

		/// <summary>
		/// Closes the <see cref="TourPanel"/>.
		/// </summary>
		/// <remarks>
		/// Fires the <see cref="Ended"/> event and hides the panel, which fires the <see cref="Closed"/> event
		/// when the client widget disappears. The close and exit buttons call this method. The <see cref="TourPanel"/> is not disposed.
		/// </remarks>
		/// <example>
		/// Closing the tour from a custom button:
		/// <code><![CDATA[
		/// private void buttonSkipTour_Click(object sender, EventArgs e)
		/// {
		///     this.tour.Close();
		/// }
		/// ]]></code>
		/// </example>
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
			Wisej.Resources.ComponentResourceManager resources = new Wisej.Resources.ComponentResourceManager(typeof(TourPanel));
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
						else if (component is RibbonBar.RibbonBar)
							child = ((RibbonBar.RibbonBar)component).Pages[p];
						else if (component is Form && ((Form)component).IsMdiContainer)
							child = ((Form)component).MdiChildren.FirstOrDefault(f => f.Name == p);
					}
					component = child;
				}
				else if (component is MainMenu)
				{
					component = ((MainMenu)component).MenuItems[p];
				}
				else if (component is RibbonBar.RibbonBarPage)
				{
					component = ((RibbonBar.RibbonBarPage)component).Groups[p];
				}
				else if (component is RibbonBar.RibbonBarGroup)
				{
					component = ((RibbonBar.RibbonBarGroup)component).Items[p];
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
					else if (component is RibbonBar.RibbonBarPage)
						((RibbonBar.RibbonBarPage)component).Selected = true;
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
					else if (component is DataGridViewColumn)
					{
						component = ((DataGridViewColumn)component).DataGridView;
					}
					else if (component is ColumnHeader)
					{
						component = ((ColumnHeader)component).ListView;
					}
					else if (component is RibbonBar.RibbonBarPage)
					{
						component = ((RibbonBar.RibbonBarPage)component).RibbonBar;
					}
					else if (component is RibbonBar.RibbonBarGroup)
					{
						component = ((RibbonBar.RibbonBarGroup)component).RibbonBar;
					}
					else if (component is RibbonBar.RibbonBarItem)
					{
						component = ((RibbonBar.RibbonBarItem)component).RibbonBar;
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
		/// Represents the label that displays the <see cref="TourStep.Title"/> of the current step in the <see cref="TourPanel"/>.
		/// </summary>
		/// <exclude/>
		public class Label : Wisej.Web.Label
		{
			/// <summary>
			/// Returns or sets the text of the label.
			/// </summary>
			/// <remarks>
			/// The text is set automatically by the <see cref="TourPanel"/> using the <see cref="TourStep.Title"/> of the current step
			/// and it's not serialized by the designer.
			/// </remarks>
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public override string Text
			{
				get { return base.Text; }
				set { base.Text = value; }
			}
		}

		/// <summary>
		/// Represents the <see cref="Wisej.Web.HtmlPanel"/> that displays the <see cref="TourStep.Text"/> of the current step in the <see cref="TourPanel"/>.
		/// </summary>
		/// <exclude/>
		public class HtmlPanel : Wisej.Web.HtmlPanel
		{
			/// <summary>
			/// Returns or sets the HTML text displayed in the panel.
			/// </summary>
			/// <remarks>
			/// The HTML is set automatically by the <see cref="TourPanel"/> using the <see cref="TourStep.Text"/> of the current step
			/// and it's not serialized by the designer.
			/// </remarks>
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public new string Html
			{
				get { return base.Html; }
				set { base.Html = value; }
			}

			/// <summary>
			/// Returns or sets the edges of the container to which the panel is bound.
			/// </summary>
			/// <remarks>
			/// The default value anchors the panel to all four edges of the <see cref="TourPanel"/>.
			/// </remarks>
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