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

using System.Drawing;

namespace Wisej.Web.Ext.jSequence
{
	/// <summary>
	/// Represents the method that will handle the <see cref="E:Wisej.Web.Ext.jSequence.Sequence.ElementClick" /> event 
	/// in a <see cref="T:Wisej.Web.Ext.jSequence.Sequence"/> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:Wisej.Web.Ext.jSequence.EventClickEventArgs" /> that contains the event data. </param>
	public delegate void ElementClickEventHandler(object sender, ElementClickEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="E:Wisej.Web.Ext.jSequence.Sequence.ElementClick" /> event of 
	/// the <see cref="T:Wisej.Web.Ext.jSequence.Sequence" /> control.
	/// </summary>
	public class ElementClickEventArgs : MouseEventArgs
	{
		/// <summary>
		///  Constructs a new instance of <see cref="T:Wisej.Web.Ext.jSequence.EventClickEventArgs"/>.
		/// </summary>
		/// <param name="element">The element in the sequence that was clicked by the user.</param>
		/// <param name="button">One of the <see cref="T:Wisej.Web.MouseButtons"/> values that indicate which mouse button was pressed.</param>
		/// <param name="clicks">The number of times a mouse button was pressed.</param>
		/// <param name="location">The location of a pointer click, in pixels.</param>
		internal ElementClickEventArgs(string element, MouseButtons button, int clicks, Point location)
			: base(button, clicks, location.X, location.Y, 0)
		{
			this.Element = element;
		}

		/// <summary>
		/// Returns the element in the sequence that was clicked by the user.
		/// </summary>
		public string Element
		{
			get;
			private set;
		}
	}
}
