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

using System.ComponentModel;
using System.Drawing;

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// Represents the options for the data label.
	/// </summary>
	public class OptionsDataLabels : OptionsBase
	{

		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public OptionsDataLabels()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsDataLabel"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> that owns this set of options.</param>
		public OptionsDataLabels(OptionsBase owner)
		{
			this.Owner = owner;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Specifies the default alignment for the chart's data labels.
		/// </summary>
		[DefaultValue(DataLabelAlign.Center)]
		[Description("Provides the default alignment for the chart's data labels.")]
		public DataLabelAlign Align
		{
			get
			{
				return this._align;
			}
			set
			{
				if (this._align != value)
				{
					this._align = value;
					Update();
				}
			}
		}
		private DataLabelAlign _align = DataLabelAlign.Center;

		/// <summary>
		/// Specifies the default anchoring of the data labels.
		/// </summary>
		[DefaultValue(DataLabelAnchor.Center)]
		[Description("The anchoring of the data labels.")]
		public DataLabelAnchor Anchor
		{
			get
			{
				return this._anchor;
			}
			set
			{
				if (this._anchor != value)
				{
					this._anchor = value;
					Update();
				}
			}
		}
		private DataLabelAnchor _anchor = DataLabelAnchor.Center;

		/// <summary>
		/// The background color of the data label.
		/// </summary>
		[DefaultValue(null)]
		[Description("The background color of the data label.")]
		public Color BackgroundColor
		{
			get
			{
				return this._backgroundColor;
			}
			set
			{
				if (this._backgroundColor != value)
				{
					this._backgroundColor = value;
					Update();
				}
			}
		}
		private Color _backgroundColor;

		/// <summary>
		/// The border color of the data label.
		/// </summary>
		[DefaultValue(null)]
		[Description("The border color of the data label.")]
		public Color BorderColor
		{
			get
			{
				return this._borderColor;
			}
			set
			{
				if (this._borderColor != value)
				{
					this._borderColor = value;
					Update();
				}
			}
		}
		private Color _borderColor;

		/// <summary>
		/// The radius of the data label's border.
		/// </summary>
		[DefaultValue(0)]
		[Description("The radius of the data label's border.")]
		public int BorderRadius
		{
			get
			{
				return this._borderRadius;
			}
			set
			{
				if (this._borderRadius != value)
				{
					this._borderRadius = value;
					Update();
				}
			}
		}
		private int _borderRadius = 0;

		/// <summary>
		/// The width of the data label's border.
		/// </summary>
		[DefaultValue(1)]
		[Description("The width of the data label's border.")]
		public int BorderWidth
		{
			get
			{
				return this._borderWidth;
			}
			set
			{
				if (this._borderWidth != value)
				{
					this._borderWidth = value;
					Update();
				}
			}
		}
		private int _borderWidth = 1;

		/// <summary>
		/// Specifies if the anchor position should be calculated based 
		/// on the visible geometry of the associated element (i.e. part inside the chart area).
		/// </summary>
		[DefaultValue(false)]
		[Description("Specifies if the anchor position should be calculated based on the visible geometry of the associated element.")]
		public bool Clamp
		{
			get
			{
				return this._clamp;
			}
			set
			{
				if (this._clamp != value)
				{
					this._clamp = value;
					Update();
				}	
			}
		}
		private bool _clamp = false;

		/// <summary>
		/// Specifies if the part of the label which is outside the chart area will be masked.
		/// </summary>
		[DefaultValue(false)]
		[Description("Specifies if the part of the label which is outside the chart area will be masked.")]
		public bool Clip
		{
			get 
			{  
				return this._clip; 
			}
			set
			{
				if (this._clip != value)
				{
					this._clip = value;
				}	
			}
		}
		private bool _clip = false;

		/// <summary>
		/// Color of the data label.
		/// </summary>
		[Description("The color of the data label.")]
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
		private Color _color = Color.Black;

		private bool ShouldSerializeColor()
		{
			return this._color != null;
		}

		private void ResetColor()
		{
			this.Color = Color.Black;
		}

		/// <summary>
		/// Show the data label.
		/// </summary>
		[DefaultValue(false)]
		[Description("Show the data label.")]
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
		private bool _display = false;

		/// <summary>
		/// Font of the data label.
		/// </summary>
		[Description("The font used to display the data label.")]
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
		/// Specifies the distance (in pixels) to pull the label away from the anchor point.
		/// </summary>
		[DefaultValue(4)]
		[Description("Specifies the distance (in pixels) to pull the label away from the anchor point.")]
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				if (this._offset != value)
				{
					this._offset = value;
					Update();
				}
			}
		}
		private int _offset = 4;

		/// <summary>
		/// Specifies the opacity of the data labels.
		/// </summary>
		public float Opacity
		{
			get
			{
				return this._opacity;
			}
			set
			{
				if (this._opacity != value)
				{
					this._opacity = value;
					Update();
				}
			}
		}
		private float _opacity = 1;

		/// <summary>
		/// Specifies the padding on the data labels.
		/// </summary>
		[Description("Specifies the padding on the data labels.")]
		public Padding Padding
		{
			get
			{
				return this._padding;
			}
			set
			{
				if (this._padding != value)
				{
					this._padding = value;
					Update();
				}
			}
		}
		private Padding _padding = new Padding(4);

		private bool ShouldSerializePadding()
		{
			return this._padding.All != 4;
		}

		private void ResetPadding()
		{
			this.Padding = new Padding(4);
		}

		/// <summary>
		/// Specifies the rotation of the data labels.
		/// </summary>
		[DefaultValue(0)]
		[Description("Specifies the rotation of the data labels.")]
		public int Rotation
		{
			get
			{
				return this._rotation;
			}
			set
			{
				if (this._rotation != value)
				{
					this._rotation = value;
					Update();
				}
			}
		}
		private int _rotation = 0;

		/// <summary>
		/// Specifies the text alignment for the data labels.
		/// </summary>
		public DataLabelTextAlignment TextAlign
		{
			get
			{
				return this._textAlign;
			}
			set
			{
				if (this._textAlign != value)
				{
					this._textAlign = value;
					Update();
				}	
			}
		}
		private DataLabelTextAlignment _textAlign = DataLabelTextAlignment.Start;

		/// <summary>
		/// Specifies the stroke color of the data label text.
		/// </summary>
		[DefaultValue(null)]
		[Description("The stroke color of the data label text.")]
		public Color TextStrokeColor
		{
			get
			{
				return this._textStrokeColor;
			}
			set
			{
				if (this._textStrokeColor != value)
				{
					this._textStrokeColor = value;
					Update();
				}
			}
		}
		private Color _textStrokeColor;

		/// <summary>
		/// Specifies the width of the stroke.
		/// </summary>
		[DefaultValue(0)]
		[Description("Specifies the width of the stroke.")]
		public int TextStrokeWidth
		{
			get
			{
				return this._textStrokeWidth;
			}
			set
			{
				if (this._textStrokeWidth != value)
				{
					this._textStrokeWidth = value;
					Update();
				}
			}
		}
		private int _textStrokeWidth = 0;

		/// <summary>
		/// Specifies the blur value of the text's shadow.
		/// </summary>
		[DefaultValue(0)]
		[Description("Specifies the blur value of the text's shadow.")]
		public int TextShadowBlur
		{
			get
			{
				return this._textShadowBlur;
			}
			set
			{
				if (this._textShadowBlur != value)
				{
					this._textShadowBlur = value;
					Update();
				}
			}
		}
		private int _textShadowBlur = 0;

		/// <summary>
		/// Specifies the color of the text's shadow.
		/// </summary>
		[DefaultValue(null)]
		[Description("Specifies the color of the text's shadow.")]
		public Color TextShadowColor
		{
			get
			{
				return this._textShadowColor;
			}
			set
			{
				if (this._textShadowColor != value)
				{
					this._textShadowColor = value;
					Update();
				}	
			}
		}
		private Color _textShadowColor;

		#endregion

	}
}
