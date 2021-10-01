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
	public class OptionsPlugins : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsPlugins()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsPlugins"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> that owns this set of options.</param>
		public OptionsPlugins(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Options for the data labels.
		/// </summary>
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
	}
}
