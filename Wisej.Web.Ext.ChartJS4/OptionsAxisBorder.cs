///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.ChartJS4
{
	/// <summary>
	/// Specifies the border configuration for each axis.
	/// </summary>
	/// <remarks>
	/// See: https://www.chartjs.org/docs/latest/axes/styling.html#border-configuration.
	/// </remarks>
	[ApiCategory("ChartJS4")]
	public class OptionsAxisBorder : OptionsBase
	{
		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsAxisBorder()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS4.OptionsAxisBorder"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS4.ChartJS4"/> that owns this set of options.</param>
		public OptionsAxisBorder(OptionsBase owner)
		{
			this.Owner = owner;
		}

		#endregion

		#region Properties

		/// <summary>
		/// If true, draw a border at the edge between the axis and the chart area.
		/// </summary>
		[DefaultValue(true)]
		[Description("If true, draw a border at the edge between the axis and the chart area.")]
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
		/// The color of the border line.
		/// </summary>
		[Description("The color of the border line.")]
		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				if (this._color != value) 
				{
					this._color = value;
					Update();
				}
			}
		}
		private Color _color = Color.FromArgb(25, 0, 0, 0);

		/// <summary>
		/// The width of the border line.
		/// </summary>
		[DefaultValue(1)]
		[Description("The width of the border line.")]
		public int Width
		{
			get
			{
				return this._width;
			}
			set
			{
				if (this._width != value)
				{
					this._width = value;
					Update();
				}
			}
		}
		private int _width = 1;

		/// <summary>
		/// Gets or sets the length and spacing of dashes on grid lines.
		/// </summary>
		[DefaultValue(new int[0])]
		[Description("Gets or sets the length and spacing of dashes on grid lines.")]
		public int[] Dash
		{
			get
			{
				return this._dash;
			}
			set
			{
				if (this._dash != value)
				{
					this._dash = value;
					Update();
				}
			}
		}
		private int[] _dash = new int[0];

		/// <summary>
		/// Offset for line dashes.
		/// </summary>
		[DefaultValue(0F)]
		[Description("Offset for line dashes.")]
		public float DashOffset
		{
			get
			{
				return this._dashOffset;
			}
			set
			{
				if (this._dashOffset != value)
				{

				}
			}
		}
		private float _dashOffset = 0F;

		/// <summary>
		/// Z-index of the border layer.
		/// </summary>
		/// <remarks>
		/// Values less than or equal to 0 are drawn under datasets, greater than 0 on top.
		/// </remarks>
		[DefaultValue(0)]
		[Description("Z-index of the border layer.")]
		public int Z
		{
			get
			{
				return this._z;
			}
			set
			{
				if (this._z != value)
				{
					this._z = value;
					Update();
				}
			}
		}
		private int _z = 0;

		#endregion
	}
}
