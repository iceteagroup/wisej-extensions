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
	/// Represents the options for the data labels.
	/// </summary>
	/// <remarks>
	/// An instance of this class is exposed by the <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsPlugins.DataLabels"/> property
	/// and maps to the <c>plugins.datalabels</c> configuration of the chartjs-plugin-datalabels plugin.
	/// See <see href="https://chartjs-plugin-datalabels.netlify.app/guide/options.html"/>.
	/// The data labels are hidden by default; set <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.Display"/> to true to show them.
	/// The text of each label is taken from <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Formatted"/> when it is set,
	/// otherwise it is the data value rounded to the nearest integer.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var dataLabels = this.chartJS31.Options.Plugins.DataLabels;
	/// dataLabels.Display = true;
	/// dataLabels.Anchor = DataLabelAnchor.End;
	/// dataLabels.Align = DataLabelAlign.Top;
	/// dataLabels.Color = Color.DimGray;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS3")]
	public class OptionsDataLabels : OptionsBase
	{

		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a set of data label options that is not attached to an owner.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabels = new OptionsDataLabels();
		/// dataLabels.Display = true;
		/// dataLabels.Color = Color.White;
		/// this.chartJS31.Options.Plugins.DataLabels = dataLabels;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
		public OptionsDataLabels()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsDataLabels"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> instance (usually the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsPlugins"/>) that owns this set of options.</param>
		/// <example>
		/// <code><![CDATA[
		/// var plugins = this.chartJS31.Options.Plugins;
		/// var dataLabels = new OptionsDataLabels(plugins);
		/// dataLabels.Display = true;
		/// plugins.DataLabels = dataLabels;
		/// ]]></code>
		/// </example>
		public OptionsDataLabels(OptionsBase owner)
		{
			this.Owner = owner;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the default alignment of the data labels relative to the anchor point.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS3.DataLabelAlign"/> values. The default is <see cref="F:Wisej.Web.Ext.ChartJS3.DataLabelAlign.Center"/>.
		/// </value>
		/// <remarks>
		/// The distance from the anchor point is set by <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.Offset"/>.
		/// See <see href="https://chartjs-plugin-datalabels.netlify.app/guide/positioning.html#alignment-and-offset"/>.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // place the labels above the top of the bars.
		/// var dataLabels = this.chartJS31.Options.Plugins.DataLabels;
		/// dataLabels.Display = true;
		/// dataLabels.Anchor = DataLabelAnchor.End;
		/// dataLabels.Align = DataLabelAlign.End;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the default anchor point of the data labels on the associated element.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS3.DataLabelAnchor"/> values. The default is <see cref="F:Wisej.Web.Ext.ChartJS3.DataLabelAnchor.Center"/>.
		/// </value>
		/// <remarks>
		/// See <see href="https://chartjs-plugin-datalabels.netlify.app/guide/positioning.html#anchoring"/>.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.ChartType = ChartType.Bar;
		/// this.chartJS31.Options.Plugins.DataLabels.Display = true;
		/// this.chartJS31.Options.Plugins.DataLabels.Anchor = DataLabelAnchor.Start;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the background color of the data labels.
		/// </summary>
		/// <value>
		/// The default is <see cref="F:System.Drawing.Color.Empty"/> (no background).
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabels = this.chartJS31.Options.Plugins.DataLabels;
		/// dataLabels.Display = true;
		/// dataLabels.BackgroundColor = Color.FromArgb(200, Color.White);
		/// dataLabels.BorderRadius = 4;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
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
		/// Returns or sets the border color of the data labels.
		/// </summary>
		/// <value>
		/// The default is <see cref="F:System.Drawing.Color.Empty"/> (no border).
		/// </value>
		/// <remarks>
		/// The width of the border is set by <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.BorderWidth"/>.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabels = this.chartJS31.Options.Plugins.DataLabels;
		/// dataLabels.Display = true;
		/// dataLabels.BorderColor = Color.SteelBlue;
		/// dataLabels.BorderWidth = 2;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
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
		/// Returns or sets the radius of the data label's border corners.
		/// </summary>
		/// <value>
		/// The radius in pixels. The default is 0 (square corners).
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabels = this.chartJS31.Options.Plugins.DataLabels;
		/// dataLabels.Display = true;
		/// dataLabels.BackgroundColor = Color.Orange;
		/// dataLabels.BorderRadius = 10;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the width of the data label's border.
		/// </summary>
		/// <value>
		/// The width in pixels. The default is 1.
		/// </value>
		/// <remarks>
		/// The border is drawn only when <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.BorderColor"/> is set.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabels = this.chartJS31.Options.Plugins.DataLabels;
		/// dataLabels.BorderColor = Color.Black;
		/// dataLabels.BorderWidth = 3;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets whether the anchor position should be calculated based
		/// on the visible geometry of the associated element (i.e. the part inside the chart area).
		/// </summary>
		/// <value>
		/// The default is false.
		/// </value>
		/// <remarks>
		/// Useful to keep the labels visible when the elements are partially outside of the chart area,
		/// for example when the axis <see cref="P:Wisej.Web.Ext.ChartJS3.OptionScalesAxes.Max"/> is lower than the data values.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Scales.yAxes[0].Max = 50;
		/// this.chartJS31.Options.Plugins.DataLabels.Display = true;
		/// this.chartJS31.Options.Plugins.DataLabels.Clamp = true;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets whether the part of the label which is outside the chart area is masked.
		/// </summary>
		/// <value>
		/// The default is false.
		/// </value>
		/// <remarks>
		/// Unlike the other properties of this class, the setter doesn't refresh the chart:
		/// call <c>Update()</c> on the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control to apply the change.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Plugins.DataLabels.Clip = true;
		/// this.chartJS31.Update();
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the color of the data label text.
		/// </summary>
		/// <value>
		/// The default is <see cref="F:System.Drawing.Color.Empty"/>, which uses the plugin's default color.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // white labels centered inside the pie slices.
		/// this.chartJS31.ChartType = ChartType.Pie;
		/// this.chartJS31.Options.Plugins.DataLabels.Display = true;
		/// this.chartJS31.Options.Plugins.DataLabels.Color = Color.White;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
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
		private Color _color;

		/// <summary>
		/// Returns or sets whether the data labels are displayed.
		/// </summary>
		/// <value>
		/// The default is false.
		/// </value>
		/// <remarks>
		/// The text of each label is taken from the <see cref="P:Wisej.Web.Ext.ChartJS3.DataSet.Formatted"/> array
		/// of the data set when it's set, otherwise it is the data value rounded to the nearest integer.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = this.chartJS31.DataSets[0];
		/// dataSet.Data = new object[] { 1250.5, 980.25, 1720.75 };
		/// dataSet.Formatted = new[] { "$1,250.50", "$980.25", "$1,720.75" };
		/// this.chartJS31.Options.Plugins.DataLabels.Display = true;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the font of the data labels.
		/// </summary>
		/// <value>
		/// When not set (null), returns the <see cref="P:Wisej.Web.Control.Font"/> of the chart control.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabels = this.chartJS31.Options.Plugins.DataLabels;
		/// dataLabels.Display = true;
		/// dataLabels.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
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

		/// <summary>
		/// Returns or sets the distance (in pixels) to pull the label away from the anchor point.
		/// </summary>
		/// <value>
		/// The distance in pixels. The default is 4.
		/// </value>
		/// <remarks>
		/// The direction is determined by <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.Align"/>; the offset has no effect when
		/// <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.Align"/> is <see cref="F:Wisej.Web.Ext.ChartJS3.DataLabelAlign.Center"/>.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabels = this.chartJS31.Options.Plugins.DataLabels;
		/// dataLabels.Display = true;
		/// dataLabels.Align = DataLabelAlign.Top;
		/// dataLabels.Offset = 10;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the opacity of the data labels.
		/// </summary>
		/// <value>
		/// A value from 0 (transparent) to 1 (opaque). The default is 1.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Plugins.DataLabels.Display = true;
		/// this.chartJS31.Options.Plugins.DataLabels.Opacity = 0.7F;
		/// ]]></code>
		/// </example>
		[DefaultValue(1f)]
		[Description("Specifies the opacity of the data labels.")]
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
		/// Returns or sets the padding around the text of the data labels.
		/// </summary>
		/// <value>
		/// A <see cref="T:Wisej.Web.Padding"/> value in pixels. The default is 4 on all sides.
		/// </value>
		/// <remarks>
		/// The padding is visible when <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.BackgroundColor"/> or <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.BorderColor"/> is set.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabels = this.chartJS31.Options.Plugins.DataLabels;
		/// dataLabels.Display = true;
		/// dataLabels.BackgroundColor = Color.LightYellow;
		/// dataLabels.Padding = new Padding(6, 2, 6, 2);
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the clockwise rotation angle of the data labels, in degrees.
		/// </summary>
		/// <value>
		/// The angle in degrees. The default is 0.
		/// </value>
		/// <remarks>
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // vertical labels.
		/// this.chartJS31.Options.Plugins.DataLabels.Display = true;
		/// this.chartJS31.Options.Plugins.DataLabels.Rotation = -90;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the text alignment of multi-line data labels.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS3.DataLabelTextAlignment"/> values. The default is <see cref="F:Wisej.Web.Ext.ChartJS3.DataLabelTextAlignment.Start"/>.
		/// </value>
		/// <remarks>
		/// The alignment affects only labels that span multiple lines. It doesn't change the
		/// position of the label, which is controlled by <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.Anchor"/> and <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.Align"/>.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.DataSets[0].Formatted = new[] { "Q1\n120", "Q2\n95", "Q3\n143" };
		/// this.chartJS31.Options.Plugins.DataLabels.Display = true;
		/// this.chartJS31.Options.Plugins.DataLabels.TextAlign = DataLabelTextAlignment.Center;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the stroke color of the data label text.
		/// </summary>
		/// <value>
		/// The default is <see cref="F:System.Drawing.Color.Empty"/>.
		/// </value>
		/// <remarks>
		/// The stroke is drawn only when <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.TextStrokeWidth"/> is greater than 0.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabels = this.chartJS31.Options.Plugins.DataLabels;
		/// dataLabels.Display = true;
		/// dataLabels.Color = Color.White;
		/// dataLabels.TextStrokeColor = Color.Black;
		/// dataLabels.TextStrokeWidth = 2;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
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
		/// Returns or sets the width of the stroke drawn around the data label text.
		/// </summary>
		/// <value>
		/// The width in pixels. The default is 0 (no stroke).
		/// </value>
		/// <remarks>
		/// The color of the stroke is set by <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.TextStrokeColor"/>.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Plugins.DataLabels.TextStrokeColor = Color.White;
		/// this.chartJS31.Options.Plugins.DataLabels.TextStrokeWidth = 3;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the blur value of the text's shadow.
		/// </summary>
		/// <value>
		/// The blur level in pixels. The default is 0 (no blur).
		/// </value>
		/// <remarks>
		/// The color of the shadow is set by <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.TextShadowColor"/>.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabels = this.chartJS31.Options.Plugins.DataLabels;
		/// dataLabels.Display = true;
		/// dataLabels.TextShadowColor = Color.Gray;
		/// dataLabels.TextShadowBlur = 6;
		/// ]]></code>
		/// </example>
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
		/// Returns or sets the color of the text's shadow.
		/// </summary>
		/// <value>
		/// The default is <see cref="F:System.Drawing.Color.Empty"/>.
		/// </value>
		/// <remarks>
		/// Use together with <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsDataLabels.TextShadowBlur"/> to make the shadow visible.
		/// Setting this property calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/> to refresh the chart.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Plugins.DataLabels.TextShadowColor = Color.Black;
		/// this.chartJS31.Options.Plugins.DataLabels.TextShadowBlur = 4;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
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
