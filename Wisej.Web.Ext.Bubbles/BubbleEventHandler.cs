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

    /// Represents the event data for the <see cref="E:Wisej.Web.Ext.Bubbles.BubbleNotification.Click"/> event,
    /// providing information about the click action on a bubble notification.
    /// </summary>
    /// <remarks>
    /// This class contains properties that may provide details regarding the bubble notification's state
    /// and the context in which the click event occurred. It is primarily used to handle user interactions
    /// with bubble notifications, enabling further actions based on the user's input.
    /// </remarks>
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
        /// <remarks>
        /// This property is utilized to retrieve the control that triggered the bubble event,
        /// providing context for further processing or handling.
        /// </remarks>
        /// <example>
        /// The following example attaches a bubble to a button, shows the value 7 using the
        /// <see cref="BubbleStyle.Critical"/> style, and handles the
        /// <see cref="BubbleNotification.Click"/> event.
        /// <code><![CDATA[
        /// var button = new Button
        /// {
        ///     Name = "buttonMessages",
        ///     Text = "Messages",
        ///     Location = new Point(50, 50),
        ///     Size = new Size(120, 40)
        /// };
        /// this.Controls.Add(button);   // add first, so the control is created.
        ///
        /// var bubbles = new BubbleNotification
        /// {
        ///     Alignment = ContentAlignment.TopRight,
        ///     Margin = new Padding(4)
        /// };
        ///
        /// bubbles.SetBubbleValue(button, 7);                    // 0 hides the bubble.
        /// bubbles.SetBubbleStyle(button, BubbleStyle.Critical);
        ///
        /// bubbles.Click += (s, e) =>
        /// {
        ///     if (e.Control == button)
        ///         AlertBox.Show($"Bubble clicked on {e.Control.Name}");
        /// };
        /// ]]></code>
        /// </example>
        public Control Control { get; private set; }

        /// <summary>
        /// Gets the integer value displayed in the bubble notification that was clicked.
        /// </summary>
        /// <returns>
        /// An integer representing the value contained in the clicked bubble notification.
        /// </returns>
        /// <remarks>
        /// This property is part of the BubbleEventArgs class and is used to retrieve the value relevant to the event that triggered the bubble notification.
        /// </remarks>
        /// <example>
        /// The following example attaches a bubble to a button, shows the value 7 using the
        /// <see cref="BubbleStyle.Critical"/> style, and handles the
        /// <see cref="BubbleNotification.Click"/> event.
        /// <code><![CDATA[
        /// var button = new Button
        /// {
        ///     Name = "buttonMessages",
        ///     Text = "Messages",
        ///     Location = new Point(50, 50),
        ///     Size = new Size(120, 40)
        /// };
        /// this.Controls.Add(button);   // add first, so the control is created.
        ///
        /// var bubbles = new BubbleNotification
        /// {
        ///     Alignment = ContentAlignment.TopRight,
        ///     Margin = new Padding(4)
        /// };
        ///
        /// bubbles.SetBubbleValue(button, 7);                    // 0 hides the bubble.
        /// bubbles.SetBubbleStyle(button, BubbleStyle.Critical);
        ///
        /// bubbles.Click += (s, e) =>
        /// {
        ///     if (e.Control == button)
        ///         AlertBox.Show($"{e.Value} new messages");
        /// };
        /// ]]></code>
        /// </example>
        public int Value { get; private  set;}

        #endregion

        #region Methods

        /// <summary>
        /// Returns a string representation of the current <see cref="T:Wisej.Web.Ext.Bubbles.BubbleEventArgs"/> instance.
        /// </summary>
        /// <returns>
		/// A string that describes the type of control and the current state of the properties within the <see cref="P:Wisej.Web.Ext.Bubbles.BubbleEventArgs"/>.
		/// This includes relevant information that can aid in debugging or logging the state of the BubbleEventArgs object.
		/// </returns>
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
