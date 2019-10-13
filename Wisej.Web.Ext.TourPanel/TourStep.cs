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
using System.ComponentModel;
using System.Drawing.Design;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.TourPanel
{
	/// <summary>
	/// Represents a steps in a <see cref="Wisej.Web.Ext.TourPanel"/>.
	/// </summary>
	public class TourStep
	{
		#region Events

		/// <summary>
		/// Fired when the step is shown.
		/// </summary>
		public event EventHandler Show;

		/// <summary>
		/// Fired when the step is hidden.
		/// </summary>
		public event EventHandler Hide;

		/// <summary>
		/// Fires the <see cref="TourStep.Show"/> event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnShow(EventArgs e)
		{
			this.Show?.Invoke(this, e);
			this.IsVisible = true;
		}

		/// <summary>
		/// Fires the <see cref="TourStep.Hide"/> event.
		/// </summary>
		/// <param name="e"></param>
		protected internal virtual void OnHide(EventArgs e)
		{
			this.IsVisible = false;
			this.Hide?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the <see cref="TourPanel"/> that owns
		/// this <see cref="TourStep"/>.
		/// </summary>
		[Browsable(false)]
		public TourPanel Tour
		{
			get;
			internal set;
		}

		/// <summary>
		/// Returns or sets the title to display in the
		/// <see cref="TourPanel"/>.
		/// </summary>
		[DefaultValue("")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("Returns or sets the title to display in the TourPanel,")]
		public string Title
		{
			get { return this._title; }
			set
			{
				value = value ?? string.Empty;

				if (this._title != value)
				{
					this._title = value;
					Update();
				}
			}
		}
		private string _title = string.Empty;

		/// <summary>
		/// Returns or sets the HTML text to display in the
		/// <see cref="TourPanel"/>
		/// </summary>
		[DefaultValue("")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("Returns or sets the HTML text to display in the TourPanel,")]
		[Editor("Wisej.Design.HtmlEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
		public string Text
		{
			get { return this._text; }
			set
			{
				value = value ?? string.Empty;

				if (this._text != value)
				{
					this._text = value;
					Update();
				}
			}
		}
		private string _text = string.Empty;

		/// <summary>
		/// Returns or sets user-defined data associated with the step.
		/// </summary>
		/// <returns>An object representing the data.</returns>
		[Bindable(true)]
		[DefaultValue(null)]
		[Localizable(false)]
		[SRCategory("CatData")]
		[SRDescription("ControlTagDescr")]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get { return this._tag; }
			set { this._tag = value; }
		}
		private object _tag;

		/// <summary>
		/// Returns a dynamic object that can be used to store custom data in relation to this component.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public dynamic UserData
		{
			get { return _userData = _userData ?? new DynamicObject(); }
		}
		private dynamic _userData = null;

		/// <summary>
		/// Enables or disables the step. When a step is disabled it is
		/// skipped from the rotation.
		/// </summary>
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("Enables or disables the step.")]
		public bool Enabled
		{
			get { return this._enabled; }
			set { this._enabled = value; }
		}
		private bool _enabled = true;

		/// <summary>
		/// Identifies the target control within the
		/// parent of the <see cref="TourPanel"/>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The value of the TargetName property uses the following syntax:
		/// controlName.childControlName.childControlName, etc.
		/// </para>
		/// <para>
		/// Nested controls can be reached by specifying the full path. For example, the button child of a panel
		/// can be identified as "panel1.button1".
		/// </para>
		/// <para>
		/// Child widgets (widgets that compose more complex widget) can be reached using the slash separator and the
		/// name of the child widget. For example, the tools added to a control that supports tools can be reached as
		/// "textBox1/tools".
		/// </para>
		/// <para>
		/// Child widgets that are part of the children collection on the client can be reached using the "[]" syntax.
		/// For example, the first tool in a tools widget is addressable as "textBox1/tools[0]".
		/// </para>
		/// </remarks>
		[DefaultValue("")]
		[SRCategory("CatBehavior")]
		[SRDescription("Identifies the target control within the TourPanel.")]
		public string TargetName
		{
			get { return this._targetName; }
			set
			{
				value = value ?? string.Empty;

				if (this._targetName != value)
				{
					this._targetName = value;
					Update();
				}
			}
		}
		private string _targetName = string.Empty;

		/// <summary>
		/// Returns or sets the target for the step. The object can be a reference to a control, a component, or
		/// a string with the numeric ID (the <see cref="Wisej.Web.Control.Handle"/>) of the target.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Target
		{
			get { return this._target; }
			set
			{
				this._target = value;
				this.TargetComponent = value as IWisejComponent;
			}
		}
		private object _target;

		/// <summary>
		/// Reference to target component. Needed to "uncover" the target
		/// before the step. Cannot use the Target property because
		/// it may be transformed to a string path.
		/// </summary>
		internal IWisejComponent TargetComponent
		{
			get;
			set;
		}

		/// <summary>
		/// Returns or sets a value indicating whether pointer events are allowed
		/// on the current target.
		/// </summary>
		[DefaultValue(false)]
		[Description("Returns or sets a value indicating whether pointer events are allowed on the current target.")]
		public bool AllowPointerEvents
		{
			get { return this._allowPointerEvents; }
			set
			{
				if (this._allowPointerEvents != value)
				{
					this._allowPointerEvents = value;
					Update();
				}
			}
		}
		private bool _allowPointerEvents = false;

		/// <summary>
		/// Returns or sets the number of seconds to wait before
		/// showing the next step when the TourPanel is auto playing the steps.
		/// The default value is 0 to use the time set in the 
		/// <see cref="TourPanel.DefaultAutoPlayTime"/> property.
		/// </summary>
		/// <exception cref="ArgumentException">
		/// When the value is less than 0.
		/// </exception>
		[DefaultValue(0)]
		[SRCategory("CatLayout")]
		[SRDescription("Returns or sets the number of seconds before showing the next step.")]
		public int AutoPlayTime
		{
			get { return this._autoPlayTime; }
			set
			{
				if (value < 0)
					throw new ArgumentException(nameof(AutoPlayTime));

				this._autoPlayTime = value;
			}
		}
		private int _autoPlayTime = 0;

		/// <summary>
		/// Returns or sets the alignment side and position of the 
		/// <see cref="TourPanel"/>
		/// when this 
		/// <see cref="TourStep"/> is shown.
		/// </summary>
		[SRCategory("CatLayout")]
		[SRDescription("Returns or sets the alignment side and position of the TourPanel.")]
		public Placement Alignment
		{
			get { return this._alignment; }
			set
			{
				if (this._alignment != value)
				{
					this._alignment = value;
					Update();
				}
			}
		}
		private Placement _alignment = Placement.BottomCenter;

		internal bool ShouldSerializeAlignment()
		{
			if (this.Tour == null)
				return this._alignment != Placement.BottomCenter;
			else
				return this.Tour.DefaultAlignment != this._alignment;
		}

		private void ResetAlignment()
		{
			this._alignment = Placement.BottomCenter;
		}

		/// <summary>
		/// Returns or sets the offset in pixels of the calculated position of the
		/// <see cref="TourPanel"/> when this 
		/// <see cref="TourStep"/> is shown.
		/// </summary>
		[SRCategory("CatLayout")]
		[SRDescription("Returns or sets the offset in pixels of the calculated position of the TourPanel ")]
		public Padding Offset
		{
			get { return this._offset; }
			set
			{
				if (this._offset != value)
				{
					this._offset = value;
					Update();
				}
			}
		}
		private Padding _offset = Padding.Empty;

		internal bool ShouldSerializeOffset()
		{
			if (this.Tour == null)
				return !this._offset.IsEmpty;
			else
				return this._offset != this.Tour.DefaultOffset;
		}

		private void ResetOffset()
		{
			this._offset = Padding.Empty;
		}

		/// <summary>
		/// Determines whether the 
		/// <see cref="TourPanel"/>
		/// shows the <see cref="TourPanel.CloseButton"/> and <see cref="TourPanel.ExitButton"/> buttons when showing this
		/// <see cref="TourStep"/>.
		/// </summary>
		/// <remarks>
		/// The <see cref="TourPanel.ExitButton"/> is always shown on the last step.
		/// </remarks>
		[SRCategory("CatAppearance")]
		[SRDescription("Determines whether the TourPanel shows a close button.")]
		public bool ShowClose
		{
			get { return this._showClose; }
			set
			{
				if (this._showClose != value)
				{
					this._showClose = value;
					Update();
				}
			}
		}
		private bool _showClose = true;

		internal bool ShouldSerializeShowClose()
		{
			if (this.Tour == null)
				return this._showClose != true;
			else
				return this.Tour.DefaultShowClose != this._showClose;
		}

		private void ResetShowClose()
		{
			this._showClose = true;
		}

		/// <summary>
		/// Returns true when this step is currently visible on
		/// the <see cref="TourPanel"/>.
		/// </summary>
		[Browsable(false)]
		public bool IsVisible
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the index of this step in the 
		/// <see cref="TourPanel.Steps"/> list.
		/// </summary>
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.Tour == null)
					return -1;

				return Array.IndexOf(this.Tour.Steps, this);
			}
		}

		#endregion

		#region Methods

		private void Update()
		{
			if (this.IsVisible)
				this.Tour.Update(this);
		}

		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "Step: " + this.Title;
		}

		#endregion
	}
}