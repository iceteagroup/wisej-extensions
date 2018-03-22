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
	public class OptionsTitle : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsTitle()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsTitle"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> that owns this set of options.</param>
		public OptionsTitle(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Position of the title.
		/// </summary>
		[DefaultValue(HeaderPosition.Top)]
		[Description("Position of the title.")]
		public HeaderPosition Position
		{
			get { return this._position; }
			set
			{
				if (this._position != value)
				{
					this._position = value;
					Update();
				}
			}
		}
		private HeaderPosition _position = HeaderPosition.Top;

		/// <summary>
		/// Number of pixels to add above and below the title text.
		/// </summary>
		[DefaultValue(10)]
		[Description("Number of pixels to add above and below the title text.")]
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
		/// Show the title block.
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
		/// Title text.
		/// </summary>
		[Description("Title text.")]
		public string Text
		{
			get
			{
				if (string.IsNullOrEmpty(this._text))
				{
					var chart = this.Chart;
					if (chart != null)
						return chart.Text;
				}

				return this._text;
			}
			set
			{
				value = value ?? string.Empty;
				if (this._text != value)
				{
					this._text = value;
					Update();
				}
			}
		}
		private string _text;

		private bool ShouldSerializeText()
		{
			return this._text != null && this._display && this._text.Length > 0;
		}

		private void ResetText()
		{
			this.Text = string.Empty;
		}

		/// <summary>
		/// Title color.
		/// </summary>
		[Description("Title color.")]
		public Color FontColor
		{
			get
			{
				var chart = this.Chart;
				if (this._fontColor.IsEmpty && chart != null)
					return chart.ForeColor;

				return this._fontColor;
			}
			set
			{
				if (this._fontColor != value)
				{
					this._fontColor = value;
					Update();
				}
			}
		}
		private Color _fontColor;

		private bool ShouldSerializeFontColor()
		{
			return !this._fontColor.IsEmpty;
		}

		private void ResetFontColor()
		{
			this.FontColor = Color.Empty;
		}
	}
}
