///////////////////////////////////////////////////////////////////////////////
//
// (C) 2026 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.ComponentModel;
using System.Drawing;

namespace Wisej.Web.Ext.Mermaid
{
	/// <summary>
	/// Represents the method that will handle the <see cref="Mermaid.ElementClick" /> event 
	/// in a <see cref="Mermaid"/> control.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="ElementClickEventArgs" /> that contains the event data. </param>
	public delegate void ElementClickEventHandler(object sender, ElementClickEventArgs e);

	/// <summary>
	/// Provides data for the <see cref="Mermaid.ElementClick" /> event of 
	/// the <see cref="Mermaid" /> control.
	/// </summary>
	[ApiCategory("Mermaid")]
	public class ElementClickEventArgs : MouseEventArgs
	{
		/// <summary>
		///  Constructs a new instance of <see cref="ElementClickEventArgs"/>.
		/// </summary>
		/// <param name="element">The element in the Mermaid diagram that was clicked by the user.</param>
		/// <param name="button">One of the <see cref="MouseButtons"/> values that indicate which mouse button was pressed.</param>
		/// <param name="clicks">The number of times a mouse button was pressed.</param>
		/// <param name="location">The location of a pointer click, in pixels.</param>
		internal ElementClickEventArgs(string element, MouseButtons button, int clicks, Point location)
			: base(button, clicks, location.X, location.Y, 0)
		{
			this.Element = element;
			this.Data = null;
		}

		/// <summary>
		///  Constructs a new instance of <see cref="ElementClickEventArgs"/> with additional data.
		/// </summary>
		/// <param name="element">The element in the Mermaid diagram that was clicked by the user.</param>
		/// <param name="button">One of the <see cref="MouseButtons"/> values that indicate which mouse button was pressed.</param>
		/// <param name="clicks">The number of times a mouse button was pressed.</param>
		/// <param name="location">The location of a pointer click, in pixels.</param>
		/// <param name="data">Additional data about the clicked element (tag name, type, IDs, etc.).</param>
		internal ElementClickEventArgs(string element, MouseButtons button, int clicks, Point location, dynamic data)
			: base(button, clicks, location.X, location.Y, 0)
		{
			this.Element = element;
			this.Data = data;
		}

		/// <summary>
		/// Returns the element in the Mermaid diagram that was clicked by the user.
		/// </summary>
		public string Element
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns additional data about the clicked element.
		/// </summary>
		/// <remarks>
		/// This dynamic object may contain properties like:
		/// <list type="bullet">
		/// <item><description><c>tagName</c> - The HTML tag name of the clicked element</description></item>
		/// <item><description><c>text</c> - Text content of the element</description></item>
		/// <item><description><c>id</c> - Element ID</description></item>
		/// <item><description><c>dataId</c> - Mermaid's data-id attribute</description></item>
		/// <item><description><c>className</c> - CSS class names</description></item>
		/// <item><description><c>elementType</c> - Type of Mermaid element (node, edge, cluster, etc.)</description></item>
		/// <item><description><c>parentId</c> - Parent element's ID</description></item>
		/// <item><description><c>parentDataId</c> - Parent element's data-id</description></item>
		/// <item><description><c>ariaLabel</c> - Aria label (useful for edges)</description></item>
		/// </list>
		/// </remarks>
		public dynamic Data
		{
			get;
			private set;
		}
	}
}
