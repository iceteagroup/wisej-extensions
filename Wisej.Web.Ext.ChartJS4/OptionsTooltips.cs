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

namespace Wisej.Web.Ext.ChartJS4
{
	/// <summary>
	/// Represents the options for the chart tooltips.
	/// </summary>
	[ApiCategory("ChartJS4")]
	public class OptionsTooltips : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsTooltips()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.OptionsTooltips"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		public OptionsTooltips(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Enables or disables tooltips.
		/// </summary>
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

		/// <summary>
		/// Use the corresponding point style (from dataset options) instead of color boxes, 
		/// ex: star, triangle etc. (size is based on the minimum value between boxWidth and boxHeight).
		/// </summary>
		[DefaultValue(false)]
		[Description("Use the corresponding point style (from dataset options) instead of color boxes")]
		public bool UsePointStyle
		{
			get { return this._usePointStyle; }
			set
			{
				if(this._usePointStyle != value)
				{
					this._usePointStyle = value;
					Update();
				}
			}
		}
		private bool _usePointStyle;

		/// <summary>
		/// The mode for positioning the tooltip.
		/// </summary>
		/// <remarks>
		/// 'average' mode will place the tooltip at the average position of the items displayed in the tooltip. 
		/// 'nearest' will place the tooltip at the position of the element closest to the event position.
		/// </remarks>
		[DefaultValue("average")]
		[Description("The mode for positioning the tooltip.")]
		public string Position
		{
			get { return this._position; }
			set
			{
				if(this._position != value)
				{
					this._position = value;
					Update();
				}
			}
		}
		private string _position;

		/// <summary>
		/// The mode for the tooltip.
		/// </summary>
		[DefaultValue("index")]
		[Description("The mode for the tooltip.")]
		public string Mode
		{
			get { return this._mode; }
			set
			{
				if (this._mode != value)
				{
					this._mode = value;
					Update();
				}
			}
		}
		private string _mode = "index";
	}
}
