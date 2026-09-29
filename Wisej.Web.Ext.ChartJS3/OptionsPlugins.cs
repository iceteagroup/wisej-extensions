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

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// Represents the options for the plugins.
	/// </summary>
	/// <remarks>
	/// Use the <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/> property to access the plugin options of a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
	/// The options are serialized as the Chart.js 3 <c>plugins</c> option and group the title, legend and tooltip options,
	/// together with the options of the chartjs-plugin-datalabels plugin.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// OptionsPlugins plugins = this.chartJS31.Options.Plugins;
	/// plugins.Title.Text = "Traffic Sources";
	/// plugins.Legend.Position = HeaderPosition.Right;
	/// plugins.DataLabels.Display = true;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS3")]
	public class OptionsPlugins : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone set of plugin options that can be assigned to <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var plugins = new OptionsPlugins();
		/// plugins.Title.Text = "Downloads";
		/// plugins.Legend.Display = false;
		///
		/// this.chartJS31.Options.Plugins = plugins;
		/// this.chartJS31.Options.Update();
		/// ]]></code>
		/// </example>
		public OptionsPlugins()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsPlugins"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> set of options that owns this set of options.</param>
		/// <remarks>
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control is resolved through <paramref name="owner"/>.
		/// The instance can be assigned only to the option set specified in <paramref name="owner"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = this.chartJS31.Options;
		/// var plugins = new OptionsPlugins(options);
		/// plugins.Tooltip.Enabled = false;
		///
		/// options.Plugins = plugins;
		/// options.Update();
		/// ]]></code>
		/// </example>
		public OptionsPlugins(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Options for the data labels.
		/// </summary>
		/// <value>An <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsDataLabels"/> instance. It is created automatically the first time the property is read.</value>
		/// <remarks>
		/// Configures the chartjs-plugin-datalabels plugin. The options are serialized as <c>plugins.dataLabels</c> and renamed on the client
		/// to the <c>plugins.datalabels</c> option expected by the plugin. When the data set provides formatted values, the labels show them instead of the raw values.
		/// Assigning a new instance doesn't update the chart, call <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh it.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsDataLabels"/> instance assigned already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var labels = this.chartJS31.Options.Plugins.DataLabels;
		/// labels.Display = true;
		/// labels.Anchor = DataLabelAnchor.End;
		/// labels.Color = Color.White;
		/// labels.BackgroundColor = Color.SteelBlue;
		/// ]]></code>
		/// </example>
		[Description("Options for the data labels.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsDataLabels DataLabels
		{
			get
			{
				if (this._dataLabels == null)
					this._dataLabels = new OptionsDataLabels(this);

				return this._dataLabels;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._dataLabels = value;
			}
		}
		private OptionsDataLabels _dataLabels;

		/// <summary>
		/// Options for the chart legend.
		/// </summary>
		/// <value>An <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsLegend"/> instance. It is created automatically the first time the property is read.</value>
		/// <remarks>
		/// Serialized as the Chart.js 3 <c>plugins.legend</c> option. Assigning a new instance doesn't update the chart, call <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh it.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsLegend"/> instance assigned already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var legend = this.chartJS31.Options.Plugins.Legend;
		/// legend.Display = true;
		/// legend.Position = HeaderPosition.Right;
		/// legend.Labels.UsePointStyle = true;
		/// ]]></code>
		/// </example>
		[Description("Options for the chart legend.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsLegend Legend
		{
			get
			{
				if (this._legend == null)
					this._legend = new OptionsLegend(this);

				return this._legend;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._legend = value;
			}
		}
		private OptionsLegend _legend;

		/// <summary>
		/// Options for the chart title.
		/// </summary>
		/// <value>An <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsTitle"/> instance. It is created automatically the first time the property is read.</value>
		/// <remarks>
		/// Serialized as the Chart.js 3 <c>plugins.title</c> option. Assigning a new instance doesn't update the chart, call <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh it.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsTitle"/> instance assigned already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var title = this.chartJS31.Options.Plugins.Title;
		/// title.Display = true;
		/// title.Text = "Quarterly Results";
		/// title.Position = HeaderPosition.Bottom;
		/// ]]></code>
		/// </example>
		[Description("Options for the chart title.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsTitle Title
		{
			get
			{
				if (this._title == null)
					this._title = new OptionsTitle(this);

				return this._title;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._title = value;
			}
		}
		private OptionsTitle _title;

		/// <summary>
		/// Options for the chart tooltip.
		/// </summary>
		/// <value>An <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsTooltips"/> instance. It is created automatically the first time the property is read.</value>
		/// <remarks>
		/// Serialized as the Chart.js 3 <c>plugins.tooltip</c> option. Assigning a new instance doesn't update the chart, call <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh it.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">The value assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsTooltips"/> instance assigned already belongs to another set of options.</exception>
		/// <example>
		/// <code><![CDATA[
		/// // hide the tooltips when the user hovers the data points.
		/// this.chartJS31.Options.Plugins.Tooltip.Enabled = false;
		/// ]]></code>
		/// </example>
		[Description("Options for the chart tooltip.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsTooltips Tooltip
		{
			get
			{
				if (this._tooltip == null)
					this._tooltip = new OptionsTooltips(this);

				return this._tooltip;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._tooltip = value;
			}
		}
		private OptionsTooltips _tooltip;
	}
}
