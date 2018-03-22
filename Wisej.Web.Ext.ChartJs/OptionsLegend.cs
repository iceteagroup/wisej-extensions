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
	/// Represents the options for the chart title.
	/// </summary>
	public class OptionsLegend : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsLegend()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsLegend"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		public OptionsLegend(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Position of the legend.
		/// </summary>
		[DefaultValue(HeaderPosition.Top)]
		[Description("Position of the title.")]
		public HeaderPosition Position
		{
			get { return this._position; }
			set
			{
				if (value == HeaderPosition.Left || value == HeaderPosition.Right)
					throw new ArgumentException("The legend position can only be Top or Bottom.");

				if (this._position != value)
				{
					this._position = value;
					Update();
				}
			}
		}
		private HeaderPosition _position = HeaderPosition.Top;

		/// <summary>
		/// Show the legend block.
		/// </summary>
		[DefaultValue(true)]
		[Description("Show the title block.")]
		public bool Display
		{
			get { return this._display; }
			set
			{
				if (this._display != value)
				{
					this._display = value;
					Update();
				}
			}
		}
		private bool _display = true;

		/// <summary>
		/// Options for the labels in the legend.
		/// </summary>
		[Description("Options for the labels in the legend.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OptionsLegendLabels Labels
		{
			get
			{
				if (this._labels == null)
					this._labels = new OptionsLegendLabels(this);

				return this._labels;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				value.Owner = this;
				this._labels = value;
			}
		}
		private OptionsLegendLabels _labels;

	}

	/// <summary>
	/// Represents the options for the labels in the chart legend.
	/// </summary>
	public class OptionsLegendLabels : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsLegendLabels()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsLegendLabels"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		public OptionsLegendLabels(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Font of the title.
		/// </summary>
		public Font Font
		{
			get
			{
				var chart = this.Chart;
				if (this._font == null && chart != null)
					return chart.Font;

				return this._font;
			}
			set
			{
				if (this._font != value)
				{
					this._font = value;
					Update();
				}
			}
		}
		private Font _font;

		private bool ShouldSerializeFont()
		{
			return this._font != null;
		}

		private void ResetFont()
		{
			this.Font = null;
		}

		/// <summary>
		/// Padding between labels (rows of colored boxes.)
		/// </summary>
		[DefaultValue(10)]
		[Description("Padding between labels (rows of colored boxes.)")]
		public int Padding
		{
			get { return this._padding; }
			set
			{
				if (this._padding != value)
				{
					this._padding = value;
					Update();
				}
			}
		}
		private int _padding = 10;
	}
}
