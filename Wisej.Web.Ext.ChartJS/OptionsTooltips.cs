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
using System.Drawing;

namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Represents the options for the chart tooltips.
	/// </summary>
	/// <remarks>
	/// Use the <see cref="P:Wisej.Web.Ext.ChartJS.Options.Tooltips"/> property to access the tooltip options of a <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
	/// The tooltip label shows the label of the data point followed by its value, using the value in
	/// <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Formatted"/> when available.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// OptionsTooltips tooltips = this.chartJS1.Options.Tooltips;
	/// tooltips.Enabled = true;
	///
	/// this.chartJS1.DataSets[0].Formatted = new[] { "$1,200", "$950", "$1,430" };
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS")]
	public class OptionsTooltips : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone set of tooltip options that can be assigned to <see cref="P:Wisej.Web.Ext.ChartJS.Options.Tooltips"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltips = new OptionsTooltips();
		/// tooltips.Enabled = false;
		///
		/// this.chartJS1.Options.Tooltips = tooltips;
		/// ]]></code>
		/// </example>
		public OptionsTooltips()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsTooltips"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsBase"/> set of options that owns this set of options.</param>
		/// <remarks>
		/// The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control that displays the tooltips is resolved through <paramref name="owner"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltips = new OptionsTooltips(this.chartJS1.Options);
		/// tooltips.Enabled = true;
		/// this.chartJS1.Options.Tooltips = tooltips;
		/// ]]></code>
		/// </example>
		public OptionsTooltips(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Enables or disables tooltips.
		/// </summary>
		/// <value>true to show the tooltips when hovering the data points; otherwise, false. The default is true.</value>
		/// <remarks>
		/// Serialized as the Chart.js <c>tooltips.enabled</c> option.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // disable the tooltips on a busy chart.
		/// this.chartJS1.Options.Tooltips.Enabled = !this.checkBoxTooltips.Checked;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("Enables tooltips.")]
		public bool Enabled
		{
			get { return this._enabled; }
			set
			{
				if (this._enabled != value)
				{
					this._enabled = value;
					Update();
				}
			}
		}
		private bool _enabled = true;
	}
}
