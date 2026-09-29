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
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Represents a progress bar hosted in a <see cref="ToolStrip" />.
	/// </summary>
	/// <remarks>
	/// The hosted <see cref="Wisej.Web.ProgressBar"/> is created by the item with an initial size of 100 x 15 pixels
	/// and is accessible through the <see cref="ProgressBar"/> property. <see cref="Minimum"/>, <see cref="Maximum"/>,
	/// <see cref="Step"/> and <see cref="Value"/> are forwarded to the hosted control.
	/// </remarks>
	public partial class ToolStripProgressBar : ToolStripControlHost
	{
		internal static readonly object EventRightToLeftLayoutChanged = new object();

		private static readonly Padding defaultMargin = new Padding(1, 2, 1, 1);
		private static readonly Padding defaultStatusStripMargin = new Padding(1, 3, 1, 3);
		private Padding scaledDefaultMargin = defaultMargin;
		private Padding scaledDefaultStatusStripMargin = defaultStatusStripMargin;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripProgressBar"/> class.
		/// </summary>
		/// <example>
		/// Adding a progress bar to a tool strip:
		/// <code><![CDATA[
		/// var progress = new ToolStripProgressBar
		/// {
		///     Minimum = 0,
		///     Maximum = 100,
		///     Value = 25
		/// };
		/// this.toolStrip1.Items.Add(progress);
		/// ]]></code>
		/// </example>
		public ToolStripProgressBar()
			: base(CreateControlInstance())
		{
			if (Control is ToolStripProgressBarControl toolStripProgressBarControl)
			{
				toolStripProgressBarControl.Owner = this;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripProgressBar"/> class with the specified name.
		/// </summary>
		/// <param name="name">The name of the <see cref="ToolStripProgressBar"/>.</param>
		public ToolStripProgressBar(string name)
			: this()
		{
			Name = name;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the hosted <see cref="Wisej.Web.ProgressBar"/> control.
		/// </summary>
		/// <remarks>
		/// Use this property to access members of the progress bar that are not exposed by <see cref="ToolStripProgressBar"/>.
		/// </remarks>
		/// <example>
		/// Changing the color of the hosted progress bar:
		/// <code><![CDATA[
		/// this.toolStripProgressBar1.ProgressBar.ForeColor = Color.SeaGreen;
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ProgressBar ProgressBar
		{
			get
			{
				return (ProgressBar)Control;
			}
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get => base.BackgroundImage;
			set => base.BackgroundImage = value;
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ImageLayout BackgroundImageLayout
		{
			get => base.BackgroundImageLayout;
			set => base.BackgroundImageLayout = value;
		}

		/// <summary>
		/// Returns the default size of the <see cref="ToolStripProgressBar"/>, in pixels.
		/// </summary>
		/// <returns>A <see cref="System.Drawing.Size"/> of 100 x 15 pixels.</returns>
		public new Size DefaultSize
		{
			get
			{
				return new Size(100, 15);
			}
		}

		/// <summary>
		///  Specify how far from the edges you want to be
		/// </summary>
		/// <value></value>
		protected internal new Padding DefaultMargin
		{
			get
			{
				//TODO:ITG: Review
				//if (Owner is not null && Owner is StatusStrip)
				//{
				//	return scaledDefaultStatusStripMargin;
				//}
				//else
				//{
				//	return scaledDefaultMargin;
				//}
				throw new NotImplementedException();
			}
		}

		//TODO:ITG: Review
		//[DefaultValue(100)]
		//[SRCategory("CatBehavior")]
		//[SRDescription("ProgressBarMarqueeAnimationSpeed")]
		//public int MarqueeAnimationSpeed
		//{
		//	get { return ProgressBar.MarqueeAnimationSpeed; }
		//	set { ProgressBar.MarqueeAnimationSpeed = value; }
		//}

		/// <summary>
		/// Returns or sets the upper bound of the range of the <see cref="ToolStripProgressBar"/>.
		/// </summary>
		/// <returns>An integer representing the upper bound of the range. The default is 100.</returns>
		[DefaultValue(100)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ProgressBarMaximumDescr")]
		public int Maximum
		{
			get
			{
				return ProgressBar.Maximum;
			}
			set
			{
				ProgressBar.Maximum = value;
			}
		}

		/// <summary>
		/// Returns or sets the lower bound of the range of the <see cref="ToolStripProgressBar"/>.
		/// </summary>
		/// <returns>An integer representing the lower bound of the range. The default is 0.</returns>
		[DefaultValue(0)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ProgressBarMinimumDescr")]
		public int Minimum
		{
			get
			{
				return ProgressBar.Minimum;
			}
			set
			{
				ProgressBar.Minimum = value;
			}
		}

		//ITG:TODO: Review
		//[SRCategory("CatAppearance")]
		//[Localizable(true)]
		//[DefaultValue(false)]
		//[SRDescription("ControlRightToLeftLayoutDescr")]
		//public virtual bool RightToLeftLayout
		//{
		//	get
		//	{
		//		return ProgressBar.RightToLeftLayout;
		//	}

		//	set
		//	{
		//		ProgressBar.RightToLeftLayout = value;
		//	}
		//}

		/// <summary>
		/// Returns or sets the amount by which <see cref="PerformStep"/> increases the current position of the progress bar.
		/// </summary>
		/// <returns>The amount by which to increment the progress bar with each call to <see cref="PerformStep"/>. The default is 10.</returns>
		[DefaultValue(10)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarStepDescr")]
		public int Step
		{
			get
			{
				return ProgressBar.Step;
			}
			set
			{
				ProgressBar.Step = value;
			}
		}
		//ITG:TODO: Review
		//[DefaultValue(ProgressBarStyle.Blocks)]
		//[SRCategory("CatBehavior")]
		//[SRDescription("ProgressBarStyleDescr")]
		//public ProgressBarStyle Style
		//{
		//	get
		//	{
		//		return ProgressBar.GetStyle();
		//	}
		//	set
		//	{
		//		ProgressBar.SetStyle(value);
		//	}
		//}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return Control.Text;
			}
			set
			{
				Control.Text = value;
			}
		}

		/// <summary>
		/// Returns or sets the current position of the <see cref="ToolStripProgressBar"/>.
		/// </summary>
		/// <returns>The position within the range defined by <see cref="Minimum"/> and <see cref="Maximum"/>. The default is 0.</returns>
		[DefaultValue(0)]
		[SRCategory("CatBehavior")]
		[Bindable(true)]
		[SRDescription("ProgressBarValueDescr")]
		public int Value
		{
			get
			{
				return ProgressBar.Value;
			}
			set
			{
				ProgressBar.Value = value;
			}
		}

		#endregion

		#region Events

		/// <summary>
		///  Hide the event.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add => base.KeyDown += value;
			remove => base.KeyDown -= value;
		}

		/// <summary>
		///  Hide the event.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add => base.KeyPress += value;
			remove => base.KeyPress -= value;
		}

		/// <summary>
		///  Hide the event.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyUp
		{
			add => base.KeyUp += value;
			remove => base.KeyUp -= value;
		}

		/// <summary>
		///  Hide the event.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler LocationChanged
		{
			add => base.LocationChanged += value;
			remove => base.LocationChanged -= value;
		}

		/// <summary>
		///  Hide the event.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler OwnerChanged
		{
			add => base.OwnerChanged += value;
			remove => base.OwnerChanged -= value;
		}

		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add => Events.AddHandler(EventRightToLeftLayoutChanged, value);
			remove => Events.RemoveHandler(EventRightToLeftLayoutChanged, value);
		}

		/// <summary>
		///  Hide the event.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add => base.TextChanged += value;
			remove => base.TextChanged -= value;
		}

		/// <summary>
		///  Hide the event.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Validated
		{
			add => base.Validated += value;
			remove => base.Validated -= value;
		}

		/// <summary>
		///  Hide the event.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event CancelEventHandler Validating
		{
			add => base.Validating += value;
			remove => base.Validating -= value;
		}

		#endregion

		#region Methods

		private static Control CreateControlInstance()
		{
			ProgressBar progressBar = new ToolStripProgressBarControl
			{
				Size = new Size(100, 15)
			};
			return progressBar;
		}

		private void HandleRightToLeftLayoutChanged(object sender, EventArgs e)
		{
			OnRightToLeftLayoutChanged(e);
		}

		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			//TODO : Implement
		}

		protected override void OnSubscribeControlEvents(Control control)
		{
			//ITG:TODO: Review
			if (control is ProgressBar bar)
			{
				// Please keep this alphabetized and in sync with Unsubscribe.
				//bar.RightToLeftLayoutChanged += new EventHandler(HandleRightToLeftLayoutChanged);
			}

			base.OnSubscribeControlEvents(control);
		}

		protected override void OnUnsubscribeControlEvents(Control control)
		{
			//ITG:TODO: Review
			if (control is ProgressBar bar)
			{
				// Please keep this alphabetized and in sync with Subscribe.
				//bar.RightToLeftLayoutChanged -= new EventHandler(HandleRightToLeftLayoutChanged);
			}

			base.OnUnsubscribeControlEvents(control);
		}

		/// <summary>
		/// Advances the current position of the progress bar by the specified amount.
		/// </summary>
		/// <param name="value">The amount by which to increment the current position of the progress bar.</param>
		/// <example>
		/// Advancing the progress bar after each file is processed:
		/// <code><![CDATA[
		/// this.toolStripProgressBar1.Maximum = files.Length;
		/// foreach (string file in files)
		/// {
		///     ProcessFile(file);
		///     this.toolStripProgressBar1.Increment(1);
		/// }
		/// ]]></code>
		/// </example>
		public void Increment(int value)
		{
			ProgressBar.Increment(value);
		}

		/// <summary>
		/// Advances the current position of the progress bar by the amount of the <see cref="Step"/> property.
		/// </summary>
		/// <example>
		/// Advancing the progress bar in steps of 20 percent:
		/// <code><![CDATA[
		/// this.toolStripProgressBar1.Step = 20;
		/// for (int i = 0; i < 5; i++)
		/// {
		///     RunPhase(i);
		///     this.toolStripProgressBar1.PerformStep();
		/// }
		/// ]]></code>
		/// </example>
		public void PerformStep()
		{
			ProgressBar.PerformStep();
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
