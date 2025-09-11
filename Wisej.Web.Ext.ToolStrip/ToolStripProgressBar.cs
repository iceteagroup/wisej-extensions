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
	/// Represents a Windows progress bar control contained in a <see cref="StatusStrip" />.
	///</summary>
	public partial class ToolStripProgressBar : ToolStripControlHost
	{
		internal static readonly object EventRightToLeftLayoutChanged = new object();

		private static readonly Padding defaultMargin = new Padding(1, 2, 1, 1);
		private static readonly Padding defaultStatusStripMargin = new Padding(1, 3, 1, 3);
		private Padding scaledDefaultMargin = defaultMargin;
		private Padding scaledDefaultStatusStripMargin = defaultStatusStripMargin;

		#region Constructors

		public ToolStripProgressBar()
			: base(CreateControlInstance())
		{
			if (Control is ToolStripProgressBarControl toolStripProgressBarControl)
			{
				toolStripProgressBarControl.Owner = this;
			}
		}

		public ToolStripProgressBar(string name)
			: this()
		{
			Name = name;
		}

		#endregion

		#region Properties

		/// <summary>
		///  Create a strongly typed accessor for the class
		/// </summary>
		/// <value></value>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ProgressBar ProgressBar
		{
			get
			{
				return (ProgressBar)Control;
			}
		}

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get => base.BackgroundImage;
			set => base.BackgroundImage = value;
		}

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ImageLayout BackgroundImageLayout
		{
			get => base.BackgroundImageLayout;
			set => base.BackgroundImageLayout = value;
		}

		/// <summary>
		///  Specify what size you want the item to start out at
		/// </summary>
		/// <value></value>
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
		/// <summary>
		///  This is used for international applications where the language is written from RightToLeft.
		///  When this property is true, and the RightToLeft is true, mirroring will be turned on on
		///  the form, and control placement and text will be from right to left.
		/// </summary>
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
		///  Wrap some commonly used properties
		/// </summary>
		/// <value></value>
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
		/// <summary>
		///  Wrap some commonly used properties
		/// </summary>
		/// <value></value>
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
		///  Hide the property.
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
		///  Wrap some commonly used properties
		/// </summary>
		/// <value></value>
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

		public void Increment(int value)
		{
			ProgressBar.Increment(value);
		}

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
