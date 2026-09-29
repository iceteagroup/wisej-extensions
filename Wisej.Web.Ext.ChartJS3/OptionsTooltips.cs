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

using System;
using System.ComponentModel;
using System.Drawing;

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// Represents the options for the chart tooltips.
	/// </summary>
	/// <remarks>
	/// Use the <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsPlugins.Tooltip"/> property of <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/> to access the tooltip options
	/// of a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control. The options are serialized as the Chart.js 3 <c>plugins.tooltip</c> option.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// OptionsTooltips tooltip = this.chartJS31.Options.Plugins.Tooltip;
	///
	/// // show the tooltips only when the data labels are hidden.
	/// tooltip.Enabled = !this.chartJS31.Options.Plugins.DataLabels.Display;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS3")]
	public class OptionsTooltips : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone set of tooltip options that can be assigned to <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsPlugins.Tooltip"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var tooltip = new OptionsTooltips();
		/// tooltip.Enabled = false;
		///
		/// this.chartJS31.Options.Plugins.Tooltip = tooltip;
		/// this.chartJS31.Options.Update();
		/// ]]></code>
		/// </example>
		public OptionsTooltips()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsTooltips"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> set of options that owns this set of options.</param>
		/// <remarks>
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control that displays the tooltips is resolved through <paramref name="owner"/>.
		/// The instance can be assigned only to the option set specified in <paramref name="owner"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var plugins = this.chartJS31.Options.Plugins;
		/// plugins.Tooltip = new OptionsTooltips(plugins) { Enabled = false };
		/// plugins.Update();
		/// ]]></code>
		/// </example>
		public OptionsTooltips(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Returns or sets whether the tooltips are enabled.
		/// </summary>
		/// <value>true to show a tooltip when the user hovers a data point; otherwise, false. The default is true.</value>
		/// <remarks>
		/// Serialized as the Chart.js 3 <c>plugins.tooltip.enabled</c> option. Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // let the user toggle the tooltips.
		/// private void checkBoxTooltips_CheckedChanged(object sender, EventArgs e)
		/// {
		/// 	this.chartJS31.Options.Plugins.Tooltip.Enabled = this.checkBoxTooltips.Checked;
		/// }
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
