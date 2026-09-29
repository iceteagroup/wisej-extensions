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
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.TourPanel
{
	/// <summary>
	/// Represents a step in a <see cref="TourPanel"/>.
	/// </summary>
	/// <remarks>
	/// Each step displays its <see cref="Title"/> and <see cref="Text"/> in the <see cref="TourPanel"/>, placed next to
	/// the control identified by <see cref="TargetName"/> or <see cref="Target"/>. Changes to a step that is currently
	/// visible are applied immediately.
	/// </remarks>
	/// <example>
	/// Defining a step that points to a button inside a panel:
	/// <code><![CDATA[
	/// var step = new TourStep
	/// {
	///     Title = "Save",
	///     Text = "Click <b>Save</b> to store your changes.",
	///     TargetName = "panelTools.buttonSave",
	///     Alignment = Placement.RightMiddle,
	///     AutoPlayTime = 8
	/// };
	/// step.Show += (s, e) => this.buttonSave.Enabled = true;
	/// ]]></code>
	/// </example>
	[ApiCategory("TourPanel")]
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
		/// <remarks>
		/// The value is set when the step is assigned to the <see cref="TourPanel.Steps"/> property; it's null otherwise.
		/// </remarks>
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
		/// <remarks>
		/// The title is displayed in <see cref="TourPanel.TitleLabel"/>. Setting it to null sets it to an empty string.
		/// </remarks>
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
		/// <see cref="TourPanel"/>.
		/// </summary>
		/// <remarks>
		/// The text is displayed in <see cref="TourPanel.HtmlText"/> and can contain HTML markup.
		/// When <see cref="TourPanel.AutoSize"/> is true, the panel is resized to fit the text.
		/// Setting it to null sets it to an empty string.
		/// </remarks>
		/// <example>
		/// Using HTML to format the text of a step:
		/// <code><![CDATA[
		/// this.tourStep1.Text = "Use the <b>search</b> box to find a customer by:<ul><li>Name</li><li>Email</li></ul>";
		/// ]]></code>
		/// </example>
		[DefaultValue("")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("Returns or sets the HTML text to display in the TourPanel,")]
		[Editor("Wisej.Design.HtmlEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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
		/// <remarks>
		/// The object is created on first access and it's never sent to the client.
		/// </remarks>
		/// <example>
		/// Storing and reading custom values on a step:
		/// <code><![CDATA[
		/// this.tourStep1.UserData.HelpTopic = "customers";
		///
		/// private void tour_BeforeStep(object sender, TourPanelEventArgs e)
		/// {
		///     string topic = e.Step.UserData.HelpTopic;
		///     if (topic != null)
		///         LoadHelpTopic(topic);
		/// }
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public dynamic UserData
		{
			get { return _userData = _userData ?? new DynamicObject(); }
		}
		private dynamic _userData = null;

		/// <summary>
		/// Returns or sets whether the step is enabled. When a step is disabled it is
		/// skipped from the rotation.
		/// </summary>
		/// <remarks>
		/// Disabled steps are skipped by <see cref="TourPanel.First"/>, <see cref="TourPanel.Next"/> and <see cref="TourPanel.Back"/>,
		/// but they can still be shown by setting <see cref="TourPanel.SelectedIndex"/> or <see cref="TourPanel.CurrentStep"/>.
		/// </remarks>
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
		/// Returns or sets the name or path that identifies the target control within the
		/// <see cref="TourPanel.Container"/> of the <see cref="TourPanel"/>.
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
		/// <para>
		/// The names in the path can also refer to the columns of a <see cref="DataGridView"/> or <see cref="ListView"/>, the items of a
		/// <see cref="MenuBar"/> or <see cref="MainMenu"/>, the "menu" of a <see cref="Form"/> and MDI child forms.
		/// When the tour is started with <see cref="TourPanel.Show()"/> (no container), the first name is the name of an open
		/// <see cref="Form"/> or <see cref="Page"/>, or "Desktop" or "MainPage".
		/// </para>
		/// <para>
		/// When the target is not found, the <see cref="TourPanel.NotFound"/> event is fired.
		/// The target is resolved only the first time the step is shown, the result is stored in <see cref="Target"/>.
		/// </para>
		/// </remarks>
		/// <example>
		/// Pointing to nested controls, tools and grid columns:
		/// <code><![CDATA[
		/// this.tour.Steps = new[]
		/// {
		///     new TourStep { Title = "Name", Text = "Enter the name here.", TargetName = "panelDetails.textBoxName" },
		///     new TourStep { Title = "Tools", Text = "Use the first tool to clear the field.", TargetName = "textBoxSearch/tools[0]" },
		///     new TourStep { Title = "Total", Text = "This column shows the total.", TargetName = "dataGridView1.colTotal" }
		/// };
		/// this.tour.Show(this);
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// When set, <see cref="TargetName"/> is ignored. When the target is resolved from <see cref="TargetName"/>,
		/// this property is set to a string with the handle of the target followed by the child path (i.e. "12/tools[0]").
		/// When the target is a control or component, the <see cref="TourPanel"/> makes it visible before showing the step
		/// (selects its tab page, expands its panel, scrolls it into view, etc.).
		/// </remarks>
		/// <example>
		/// Targeting a control created at runtime, which doesn't have a designer name:
		/// <code><![CDATA[
		/// var button = new Button { Text = "Export" };
		/// this.panelTools.Controls.Add(button);
		///
		/// this.tourStep3.Target = button;
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// When false (default), the mask displayed over the page blocks all pointer events while this step is shown.
		/// When true, the mask lets pointer events through and the user can interact with the target.
		/// </remarks>
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
		/// showing the next step when the <see cref="TourPanel"/> is auto playing the steps.
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
		/// <see cref="TourPanel"/> in relation to the target
		/// when this <see cref="TourStep"/> is shown.
		/// </summary>
		/// <remarks>
		/// The default value is <see cref="Placement.BottomCenter"/>. When the value is the same as
		/// <see cref="TourPanel.DefaultAlignment"/>, the default alignment of the <see cref="TourPanel"/> is used.
		/// </remarks>
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
		/// <remarks>
		/// The <see cref="Padding"/> sides are used according to the placement: for example <see cref="Padding.Top"/> adds space
		/// between the target and a panel placed below it, and <see cref="Padding.Left"/> between the target and a panel placed
		/// at its right. When the value is the same as <see cref="TourPanel.DefaultOffset"/>, the default offset of the
		/// <see cref="TourPanel"/> is used.
		/// </remarks>
		/// <example>
		/// Moving the panel 20 pixels away from the bottom of the target:
		/// <code><![CDATA[
		/// this.tourStep1.Alignment = Placement.BottomCenter;
		/// this.tourStep1.Offset = new Padding(0, 20, 0, 0);
		/// ]]></code>
		/// </example>
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
		/// Returns or sets whether the <see cref="TourPanel"/>
		/// shows the <see cref="TourPanel.CloseButton"/> and <see cref="TourPanel.ExitButton"/> buttons when showing this
		/// <see cref="TourStep"/>.
		/// </summary>
		/// <remarks>
		/// The <see cref="TourPanel.ExitButton"/> is always shown on the last step. When the value is the same as
		/// <see cref="TourPanel.DefaultShowClose"/>, the default value of the <see cref="TourPanel"/> is used.
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
		/// <remarks>
		/// The value is set to true before the <see cref="Show"/> event and to false after the <see cref="Hide"/> event.
		/// </remarks>
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
		/// <remarks>
		/// Returns -1 when the step doesn't belong to a <see cref="TourPanel"/>.
		/// </remarks>
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
		/// <returns>A string in the format "Step: " followed by the <see cref="Title"/>.</returns>
		/// <example>
		/// Logging the current step:
		/// <code><![CDATA[
		/// System.Diagnostics.Debug.WriteLine(this.tour.CurrentStep?.ToString());
		/// ]]></code>
		/// </example>
		public override string ToString()
		{
			return "Step: " + this.Title;
		}

		#endregion
	}
}