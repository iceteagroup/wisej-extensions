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
using System.Drawing;
using System.Linq;
using Wisej.Base;
using Wisej.Core;
using Wisej.Design;

namespace Wisej.Web.Ext.CountUp
{
	/// <summary>
	/// Represents a control that displays a numeric value with a counting animation, based on
	/// the CountUp.js library <see href="https://github.com/inorganik/countUp.js"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// When the control is first shown it counts from 0 to <see cref="Value"/>. Every time <see cref="Value"/>
	/// changes afterwards, it counts from the currently displayed number to the new value, and fires
	/// <see cref="CountTerminated"/> when the animation ends.
	/// </para>
	/// <para>
	/// The group and decimal separators are taken from <see cref="Application.CurrentCulture"/>.
	/// </para>
	/// </remarks>
	/// <example>
	/// Creating a counter that animates to a new total:
	/// <code><![CDATA[
	/// var counter = new CountUp
	/// {
	///     Duration = 1500,
	///     TextAlign = HorizontalAlignment.Right,
	///     Font = new Font("Segoe UI", 36f)
	/// };
	/// counter.CountTerminated += (s, e) => AlertBox.Show("Done!");
	/// this.Controls.Add(counter);
	/// 
	/// counter.Value = 12500;
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(CountUp))]
	[DefaultProperty(nameof(Value))]
	[DefaultEvent(nameof(CountTerminated))]
	[DefaultBindingProperty(nameof(Value))]
	[Description("CountUp.js is a JavaScript widget that can create animations to display numerical data.")]
	[ApiCategory("CountUp")]
	public class CountUp : Control, IWisejControl
	{
		#region Events

		/// <summary>
		/// Fired when the value changes.
		/// </summary>
		public event EventHandler ValueChanged;

		/// <summary>
		/// Fired when the widget has finished counting.
		/// </summary>
		public event EventHandler CountTerminated
		{
			// lazy event: the client sends "countTerminated" only while there is a handler.
			add { AddHandler(nameof(CountTerminated), value, true); }
			remove { RemoveHandler(nameof(CountTerminated), value, true); }
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.CountUp.ValueChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected virtual void OnValueChanged(EventArgs e)
		{
			if (this.ValueChanged != null)
				ValueChanged(this, e);
		}

		/// <summary>
		/// Fires the <see cref="E:Wisej.Web.Ext.CountUp.CountTerminated" /> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected virtual void OnCountTerminated(EventArgs e)
		{
			((EventHandler)base.Events[nameof(CountTerminated)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets a numeric value to count to.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">The value is <see cref="float.NaN"/> or infinity.</exception>
		/// <remarks>
		/// <para>
		/// Changing the value starts a new animation on the client from the currently displayed number
		/// to the new value; <see cref="CountTerminated"/> is fired when the animation ends.
		/// </para>
		/// <para>
		/// The number is displayed without decimals: fractional values are rounded to the nearest integer.
		/// </para>
		/// </remarks>
		/// <example>
		/// Updating the counter with the result of a query:
		/// <code><![CDATA[
		/// private void buttonRefresh_Click(object sender, EventArgs e)
		/// {
		///     this.countUp1.Value = GetOrderCount();
		/// }
		/// ]]></code>
		/// </example>
		[Bindable(true)]
		[DefaultValue(0f)]
		[DesignerActionList]
		[SRCategory("CatBehavior")]
		[SRDescription(" Returns or sets a numeric value to count to.")]
		public float Value
		{
			get
			{
				return this._value;
			}
			set
			{
				// the counter can't display NaN or infinity, and NaN != NaN would
				// make every assignment of NaN look like a change.
				if (float.IsNaN(value) || float.IsInfinity(value))
					throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be a finite number.");

				if (this._value != value)
				{
					this._value = value;

					Update();
					OnValueChanged(EventArgs.Empty);
				}
			}
		}
		private float _value;

		/// <summary>
		/// Returns or sets the duration of the animation in milliseconds.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">The value is less than 100.</exception>
		/// <remarks>
		/// The default is 2500. A change applies to the next animation started by setting <see cref="Value"/>.
		/// </remarks>
		[DefaultValue(2500)]
		[DesignerActionList]
		[SRCategory("CatBehavior")]
		[SRDescription("Returns or sets the duration of the animation in milliseconds.")]
		public int Duration
		{
			get { return this._duration; }
			set
			{
				if (this._duration != value)
				{
					if (value < 100)
						throw new ArgumentOutOfRangeException("value");

					this._duration = value;
					Update();
				}
			}
		}
		private int _duration = 2500;

		/// <summary>
		/// Returns or sets whether the animation will use easing.
		/// </summary>
		/// <remarks>
		/// When true (default) the counting slows down as it approaches the target value (ease-out exponential);
		/// when false the number changes at a constant rate.
		/// </remarks>
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("Returns or sets whether the animation will use easing.")]
		public bool UseEasing
		{
			get { return this._useEasing; }
			set
			{
				if (this._useEasing != value)
				{
					this._useEasing = value;
					Update();
				}
			}
		}
		private bool _useEasing = true;

		/// <summary>
		/// Returns or sets whether the numeric value will be formatted using the
		/// grouping separator.
		/// </summary>
		/// <remarks>
		/// The separator is the <see cref="System.Globalization.NumberFormatInfo.NumberGroupSeparator"/>
		/// of <see cref="Application.CurrentCulture"/>, i.e. "1,000,000" in en-US and "1.000.000" in de-DE.
		/// </remarks>
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("Returns or sets whether the numeric value will be formatted using the grouping separator.")]
		public bool UseGrouping
		{
			get { return this._useGrouping; }
			set
			{
				if (this._useGrouping != value)
				{
					this._useGrouping = value;
					Update();
				}
			}
		}
		private bool _useGrouping = true;

		/// <summary>
		/// Returns or sets an array of custom numerals used to display the digits from 0 to 9.
		/// </summary>
		/// <exception cref="ArgumentException">The array doesn't contain 0 or 10 elements, or one of the elements is null.</exception>
		/// <remarks>
		/// The array must be empty (the default, uses the standard digits) or contain exactly 10 strings,
		/// where the element at index <c>n</c> replaces the digit <c>n</c>. Setting null resets it to an empty array.
		/// <para>
		/// The control keeps its own copy of the array, and the getter returns a copy: to change a numeral,
		/// edit the array and assign it again.
		/// </para>
		/// </remarks>
		/// <example>
		/// Displaying the value using Eastern Arabic numerals:
		/// <code><![CDATA[
		/// this.countUp1.Numerals = new[] { "٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩" };
		/// ]]></code>
		/// </example>
		[SRCategory("CatAppearance")]
		[Description("Array of custom numerals for the digits from 0 to 9.")]
		public string[] Numerals
		{
			// return a copy, so editing the returned array can't change the stored one.
			get { return (string[])this._numerals.Clone(); }
			set
			{
				if (value == null)
					value = new string[0];

				if (value.Length > 0 && value.Length != 10)
					throw new ArgumentException("Must specify 0 or 10 numerals.", "value");
				if (Array.IndexOf(value, null) > -1)
					throw new ArgumentException("Numerals can't contain null elements.", "value");

				// compare the contents, not the references, and store a copy so a caller
				// editing its array and assigning it again is seen as a change.
				if (!this._numerals.SequenceEqual(value))
				{
					this._numerals = (string[])value.Clone();
					Update();
				}
			}
		}
		private string[] _numerals = new string[0];

		private bool ShouldSerializeNumerals()
		{
			return this._numerals.Length > 0;
		}

		private void ResetNumerals()
		{
			this.Numerals = null;
		}

		/// <summary>
		/// Returns or sets the alignment of the label. The default is <see cref="HorizontalAlignment.Left"/>.
		/// </summary>
		/// <returns>One of the <see cref="HorizontalAlignment"/> values. </returns>
		[DefaultValue(HorizontalAlignment.Left)]
		[Localizable(true)]
		[ResponsiveProperty]
		[SRCategory("CatAppearance")]
		[SRDescription("LabelTextAlignDescr")]
		public virtual HorizontalAlignment TextAlign
		{
			get
			{
				return _textAlign;
			}
			set
			{
				if (this._textAlign != value)
				{
					this._textAlign = value;
					Update();
				}
			}
		}
		private HorizontalAlignment _textAlign = HorizontalAlignment.Left;

		#endregion

		#region Culture

		/// <summary>
		/// Starts listening to <see cref="Application.CultureChanged"/> when the control is created.
		/// </summary>
		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			// the group and decimal separators come from Application.CurrentCulture:
			// update the client when it changes.
			if (!this._cultureChangedWired && !this.DesignMode)
			{
				Application.CultureChanged += this.Application_CultureChanged;
				this._cultureChangedWired = true;
			}
		}
		private bool _cultureChangedWired;

		private void Application_CultureChanged(object sender, EventArgs e)
		{
			Update();
		}

		/// <summary>
		/// Disposes the control and stops listening to <see cref="Application.CultureChanged"/>.
		/// </summary>
		/// <param name="disposing">true when this method is called by the application rather than a finalizer.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && this._cultureChangedWired)
			{
				Application.CultureChanged -= this.Application_CultureChanged;
				this._cultureChangedWired = false;
			}

			base.Dispose(disposing);
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Returns the theme appearance key for this control.
		/// </summary>
		string IWisejControl.AppearanceKey
		{
			get { return this.AppearanceKey ?? "countup"; }
		}

		/// <summary>
		/// Processes events from the client.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "countTerminated":
					OnCountTerminated(EventArgs.Empty);
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

			config.className = "wisej.web.ext.CountUp";
			config.value = this.Value;
			config.align = this.TextAlign;
			config.duration = this.Duration;
			config.numerals = this._numerals;
			config.useEasing = this.UseEasing;
			config.useGrouping = this.UseGrouping;

			var numberFormat = Application.CurrentCulture.NumberFormat;
			config.separator = numberFormat.NumberGroupSeparator;
			config.@decimal = numberFormat.NumberDecimalSeparator;

			// avoid a server round trip at the end of every animation when nothing handles it.
			if (base.Events[nameof(CountTerminated)] != null)
				config.wiredEvents.Add("countTerminated");
		}

		#endregion

	}
}
