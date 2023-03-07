///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.Bubbles
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.Bubbles.BubbleNotification.Click"/> event.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.Bubbles.BubbleEventArgs" /> that contains the event data. </param>
	public delegate void BubbleEventHandler(object sender, BubbleEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.Bubbles.BubbleNotification.Click" /> events.
	///</summary>
	[ApiCategory("Bubbles")]
	public class BubbleEventArgs : EventArgs
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Wisej.Web.Ext.Bubbles.BubbleEventArgs" /> class.
		///</summary>
		/// <param name="control">The control associated with the clicked bubble.</param>
		/// <param name="value">The value in the bubble.</param>
		public BubbleEventArgs(Control control, int value)
		{
			this.Control = control;
			this.Value = value;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the control associated with the bubble notification.
		///</summary>
		/// <returns>The <see cref="T:Wisej.Web.Control" /> associated with the bubble.</returns>
		public Control Control { get; private set; }

		/// <summary>
		/// Returns the value displayed in the clicked bubble notification.
		///</summary>
		/// <returns>an integer value.</returns>
		public int Value { get; private  set;}

		#endregion

		#region Methods

		/// <summary>
		/// Returns a string that represents the current <see cref="T:Wisej.Web.Ext.Bubbles.BubbleEventArgs" /> value.
		/// </summary>
		/// <returns>A string that states the control type and the state of the <see cref="P:Wisej.Web.Ext.Bubbles.BubbleEventArgs" /> properties.</returns>
		public override string ToString()
		{
			return String.Concat(
				base.ToString(),
				", Control: ", this.Control,
				", Value: ", this.Value);
		}

		#endregion
	}
}
