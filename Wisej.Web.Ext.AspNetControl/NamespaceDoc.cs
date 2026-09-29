///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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


namespace Wisej.Web.Ext.AspNetControl
{
	/// <summary>
	/// <para>
	/// AspNetControl extension. Adds the AspNetWrapper&lt;T&gt; generic class, able to wrap any ASP.NET control class.
	/// </para>
	/// </summary>
	/// <example>
	/// The following example wraps the ASP.NET <see cref="T:System.Web.UI.WebControls.Calendar"/> control
	/// and exposes the selected date to the Wisej application:
	/// <code><![CDATA[
	/// public class CalendarWrapper : AspNetWrapper<System.Web.UI.WebControls.Calendar>
	/// {
	///     public DateTime SelectedDate { get; set; } = DateTime.Today;
	///
	///     public event EventHandler SelectionChanged;
	///
	///     protected override void OnInit(EventArgs e)
	///     {
	///         base.OnInit(e);
	///
	///         // The wrapped control is only available during the page life cycle events.
	///         this.WrappedControl.SelectionChanged += (s, args) =>
	///         {
	///             this.SelectedDate = this.WrappedControl.SelectedDate;
	///             this.SelectionChanged?.Invoke(this, EventArgs.Empty);
	///         };
	///     }
	///
	///     protected override void OnLoad(EventArgs e)
	///     {
	///         base.OnLoad(e);
	///
	///         if (!this.IsPostBack)
	///             this.WrappedControl.SelectedDate = this.SelectedDate;
	///     }
	/// }
	///
	/// // Use the wrapper like any other Wisej control:
	/// var calendar = new CalendarWrapper { Dock = DockStyle.Fill };
	/// calendar.SelectionChanged += (s, e) => AlertBox.Show(calendar.SelectedDate.ToShortDateString());
	/// this.Controls.Add(calendar);
	/// ]]></code>
	/// </example>
	internal class NamespaceDoc
	{
	}
}
