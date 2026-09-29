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

namespace Wisej.Web.Ext.ChartJS
{
	/// <summary>
	/// Represents the options for the data labels.
	/// </summary>
	/// <remarks>
	/// An instance of this class is exposed by <see cref="P:Wisej.Web.Ext.ChartJS.Options.DataLabel"/>.
	/// On the client, the options are passed to the chartjs-plugin-datalabels plugin as <c>options.plugins.datalabels</c>.
	/// The text of each label is taken from <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Formatted"/> when it contains
	/// a value for the data point, otherwise the raw data value is displayed.
	/// See <see href="https://chartjs-plugin-datalabels.netlify.app/guide/options.html"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var dataLabel = this.chartJS1.Options.DataLabel;
	/// dataLabel.Display = true;
	/// dataLabel.Anchor = DataLabelAnchor.End;
	/// dataLabel.Align = DataLabelAlign.Top;
	/// dataLabel.Color = Color.DimGray;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS")]
	public class OptionsDataLabel : OptionsBase
	{

		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a set of data label options that is not yet attached to an owner.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabel = new OptionsDataLabel();
		/// dataLabel.Display = true;
		/// dataLabel.BackgroundColor = Color.White;
		/// this.chartJS1.Options.DataLabel = dataLabel;
		/// ]]></code>
		/// </example>
		public OptionsDataLabel()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS.OptionsDataLabel"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS.OptionsBase"/> instance (usually the chart <see cref="T:Wisej.Web.Ext.ChartJS.Options"/>) that owns this set of options.</param>
		/// <exception cref="T:System.InvalidOperationException">Thrown when the owner is already assigned to a different instance.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var options = this.chartJS1.Options;
		/// var dataLabel = new OptionsDataLabel(options);
		/// dataLabel.Display = true;
		/// options.DataLabel = dataLabel;
		/// ]]></code>
		/// </example>
		public OptionsDataLabel(OptionsBase owner)
		{
			this.Owner = owner;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the default alignment of the data labels relative to the anchor point.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS.DataLabelAlign"/> values. The default is <see cref="F:Wisej.Web.Ext.ChartJS.DataLabelAlign.Center"/>.
		/// </value>
		/// <remarks>
		/// Maps to the <c>align</c> option of chartjs-plugin-datalabels. The anchor point is set by <see cref="P:Wisej.Web.Ext.ChartJS.OptionsDataLabel.Anchor"/>
		/// and the distance from it by <see cref="P:Wisej.Web.Ext.ChartJS.OptionsDataLabel.Offset"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // place the labels above the top of each bar.
		/// this.chartJS1.Options.DataLabel.Display = true;
		/// this.chartJS1.Options.DataLabel.Anchor = DataLabelAnchor.End;
		/// this.chartJS1.Options.DataLabel.Align = DataLabelAlign.End;
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
		/// Returns or sets the default anchoring of the data labels on the associated element.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS.DataLabelAnchor"/> values. The default is <see cref="F:Wisej.Web.Ext.ChartJS.DataLabelAnchor.Center"/>.
		/// </value>
		/// <remarks>
		/// Maps to the <c>anchor</c> option of chartjs-plugin-datalabels.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // anchor the labels at the base of each bar.
		/// this.chartJS1.Options.DataLabel.Display = true;
		/// this.chartJS1.Options.DataLabel.Anchor = DataLabelAnchor.Start;
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
		/// <example>
		/// <code><![CDATA[
		/// var dataLabel = this.chartJS1.Options.DataLabel;
		/// dataLabel.Display = true;
		/// dataLabel.BackgroundColor = Color.FromArgb(200, Color.White);
		/// dataLabel.BorderRadius = 4;
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
		/// The default is <see cref="F:System.Drawing.Color.Empty"/>.
		/// </value>
		/// <remarks>
		/// The border is drawn only when <see cref="P:Wisej.Web.Ext.ChartJS.OptionsDataLabel.BorderWidth"/> is greater than 0.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabel = this.chartJS1.Options.DataLabel;
		/// dataLabel.BorderColor = Color.SteelBlue;
		/// dataLabel.BorderWidth = 1;
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
		/// Returns or sets the radius of the data labels' border corners.
		/// </summary>
		/// <value>
		/// The radius in pixels. The default is 0 (square corners).
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabel = this.chartJS1.Options.DataLabel;
		/// dataLabel.BackgroundColor = Color.Gold;
		/// dataLabel.BorderRadius = 10;
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
		/// Returns or sets the width of the data labels' border.
		/// </summary>
		/// <value>
		/// The width in pixels. The default is 0 (no border).
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabel = this.chartJS1.Options.DataLabel;
		/// dataLabel.BorderWidth = 2;
		/// dataLabel.BorderColor = Color.Black;
		/// ]]></code>
		/// </example>
		[DefaultValue(0)]
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
		private int _borderWidth = 0;

		/// <summary>
		/// Returns or sets whether the anchor position should be calculated based
		/// on the visible geometry of the associated element (i.e. the part inside the chart area).
		/// </summary>
		/// <value>
		/// The default is false.
		/// </value>
		/// <remarks>
		/// Maps to the <c>clamp</c> option of chartjs-plugin-datalabels. Useful to keep labels
		/// visible when the associated element is partially outside of the chart area.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.DataLabel.Display = true;
		/// this.chartJS1.Options.DataLabel.Clamp = true;
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
		/// Returns or sets whether the part of the labels which is outside the chart area will be masked.
		/// </summary>
		/// <value>
		/// The default is false.
		/// </value>
		/// <remarks>
		/// Unlike the other properties of this class, changing this value doesn't
		/// refresh the chart automatically; the new value is used the next time the chart is updated.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.DataLabel.Clip = true;
		/// this.chartJS1.Update();
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
		/// Returns or sets the text color of the data labels.
		/// </summary>
		/// <value>
		/// The default is <see cref="F:System.Drawing.Color.Empty"/>, which uses the plugin's default color.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabel = this.chartJS1.Options.DataLabel;
		/// dataLabel.Display = true;
		/// dataLabel.Color = Color.White;
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
		/// Shows or hides the data labels.
		/// </summary>
		/// <value>
		/// true to show the data labels; false (default) to hide them.
		/// </value>
		/// <remarks>
		/// The text displayed for each data point is taken from <see cref="P:Wisej.Web.Ext.ChartJS.DataSet.Formatted"/>
		/// when available, otherwise the data value itself is shown.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataSet = this.chartJS1.DataSets[0];
		/// dataSet.Data = new object[] { 1250, 980, 1430 };
		/// dataSet.Formatted = new[] { "$1,250", "$980", "$1,430" };
		///
		/// this.chartJS1.Options.DataLabel.Display = true;
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
		/// When not set (null), returns the <b>Font</b> of the owner <see cref="T:Wisej.Web.Ext.ChartJS.ChartJS"/> control.
		/// </value>
		/// <remarks>
		/// The size, family and bold style of the font are sent to the plugin's <c>font</c> option.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.DataLabel.Display = true;
		/// this.chartJS1.Options.DataLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
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
		/// Returns or sets the distance (in pixels) to pull the labels away from the anchor point.
		/// </summary>
		/// <value>
		/// The distance in pixels. The default is 4.
		/// </value>
		/// <remarks>
		/// According to the chartjs-plugin-datalabels documentation, this option is not applicable
		/// when <see cref="P:Wisej.Web.Ext.ChartJS.OptionsDataLabel.Align"/> is <see cref="F:Wisej.Web.Ext.ChartJS.DataLabelAlign.Center"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabel = this.chartJS1.Options.DataLabel;
		/// dataLabel.Anchor = DataLabelAnchor.End;
		/// dataLabel.Align = DataLabelAlign.Top;
		/// dataLabel.Offset = 8;
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
		/// A value from 0 (fully transparent) to 1 (fully opaque). The default is 1.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.DataLabel.Display = true;
		/// this.chartJS1.Options.DataLabel.Opacity = 0.7f;
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
		/// Returns or sets the padding inside the data labels.
		/// </summary>
		/// <value>
		/// The padding in pixels. The default is 4 on all sides.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabel = this.chartJS1.Options.DataLabel;
		/// dataLabel.BackgroundColor = Color.LightYellow;
		/// dataLabel.Padding = new Padding(6, 2, 6, 2);
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
		/// Returns or sets the clockwise rotation of the data labels.
		/// </summary>
		/// <value>
		/// The rotation angle in degrees. The default is 0.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// // vertical labels inside the bars.
		/// this.chartJS1.Options.DataLabel.Display = true;
		/// this.chartJS1.Options.DataLabel.Rotation = -90;
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
		/// Returns or sets the alignment of the text inside the data labels.
		/// </summary>
		/// <value>
		/// One of the <see cref="T:Wisej.Web.Ext.ChartJS.DataLabelTextAlignment"/> values. The default is <see cref="F:Wisej.Web.Ext.ChartJS.DataLabelTextAlignment.Start"/>.
		/// </value>
		/// <remarks>
		/// Maps to the <c>textAlign</c> option of chartjs-plugin-datalabels and is mostly
		/// visible on labels with multiple lines of text.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.DataSets[0].Formatted = new[] { "Q1\n1,250", "Q2\n980" };
		/// this.chartJS1.Options.DataLabel.Display = true;
		/// this.chartJS1.Options.DataLabel.TextAlign = DataLabelTextAlignment.Center;
		/// ]]></code>
		/// </example>
		[DefaultValue(DataLabelTextAlignment.Start)]
		[Description("Specifies the text alignment for the data labels.")]
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
		/// Returns or sets the stroke (outline) color of the data label text.
		/// </summary>
		/// <value>
		/// The default is <see cref="F:System.Drawing.Color.Empty"/>.
		/// </value>
		/// <remarks>
		/// The stroke is drawn only when <see cref="P:Wisej.Web.Ext.ChartJS.OptionsDataLabel.TextStrokeWidth"/> is greater than 0.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabel = this.chartJS1.Options.DataLabel;
		/// dataLabel.Color = Color.White;
		/// dataLabel.TextStrokeColor = Color.Black;
		/// dataLabel.TextStrokeWidth = 2;
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
		/// Returns or sets the width of the stroke (outline) of the data label text.
		/// </summary>
		/// <value>
		/// The width in pixels. The default is 0 (no stroke).
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.DataLabel.TextStrokeColor = Color.Navy;
		/// this.chartJS1.Options.DataLabel.TextStrokeWidth = 1;
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
		/// The blur amount in pixels. The default is 0 (no shadow).
		/// </value>
		/// <remarks>
		/// The color of the shadow is set by <see cref="P:Wisej.Web.Ext.ChartJS.OptionsDataLabel.TextShadowColor"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dataLabel = this.chartJS1.Options.DataLabel;
		/// dataLabel.TextShadowColor = Color.Black;
		/// dataLabel.TextShadowBlur = 4;
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
		/// The shadow is visible only when <see cref="P:Wisej.Web.Ext.ChartJS.OptionsDataLabel.TextShadowBlur"/> is greater than 0.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS1.Options.DataLabel.Color = Color.White;
		/// this.chartJS1.Options.DataLabel.TextShadowColor = Color.FromArgb(128, Color.Black);
		/// this.chartJS1.Options.DataLabel.TextShadowBlur = 6;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color),"")]
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
