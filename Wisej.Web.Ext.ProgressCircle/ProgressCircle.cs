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
using Wisej.Core;

namespace Wisej.Web.Ext.ProgressCircle
{
	/// <summary>
	/// Represents a circular progress indicator that displays a percentage value as an arc
	/// drawn around the perimeter of a circle.
	/// </summary>
	/// <remarks>
	/// The ProgressCircle control is an example on how to create a custom
	/// component in Wisej by extending the <see cref="T:Wisej.Web.Canvas"/> control.
	/// The component is entirely drawn on the client side on an HTML5 canvas element
	/// using the drawing instructions sent from the server every time a property changes.
	/// </remarks>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(ProgressCircle))]
	[ApiCategory("ProgressCircle")]
	public class ProgressCircle : Canvas
	{
		/// <summary>
		/// Returns or sets the appearance key for the theme engine.
		/// </summary>
		/// <remarks>
		/// Overridden to change the default appearance key to "progress-circle" without
		/// serializing the new default in the designer. The theme appearance supplies the
		/// default values for <see cref="BackColor"/> ("backgroundColor") and <see cref="LineWidth"/> ("lineWidth").
		/// </remarks>
		[DefaultValue("progress-circle")]
		public override string AppearanceKey
		{
			get { return base.AppearanceKey ?? "progress-circle"; }
			set { base.AppearanceKey = value; }
		}

		/// <summary>
		/// Returns or sets the font of the percentage text displayed in the middle of the circle.
		/// </summary>
		/// <returns>A <see cref="T:System.Drawing.Font" />.</returns>
		/// <remarks>
		/// The text is drawn only when <see cref="ShowValue"/> is true.
		/// </remarks>
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override Font Font
		{
			get { return base.Font; }
			set { base.Font = value; }
		}

		/// <summary>
		/// Returns or sets the foreground color of the control.
		/// </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		/// <remarks>
		/// The foreground color is used to draw the circle outline, the progress arc and the percentage text.
		/// </remarks>
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}

		/// <summary>
		/// Returns or sets the fill color used when the property <see cref="FillCircle"/> is set to true.
		/// </summary>
		/// <remarks>
		/// When the value is <see cref="Color.Empty"/> (the default), the color is read from the
		/// "backgroundColor" property of the theme appearance identified by <see cref="AppearanceKey"/>.
		/// The color fills only the inner area of the circle, inside the progress ring.
		/// </remarks>
		/// <example>
		/// Filling the inner area of the circle with a custom color:
		/// <code><![CDATA[
		/// this.progressCircle1.FillCircle = true;
		/// this.progressCircle1.BackColor = Color.LightYellow;
		/// ]]></code>
		/// </example>
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override Color BackColor
		{
			get
			{
				return this._backColor.IsEmpty
					? GetThemeBackColor()
					: this._backColor;
			}
			set
			{
				if (this._backColor != value)
				{
					this._backColor = value;
					Update();
				}
			}
		}
		private Color _backColor = Color.Empty;

		private bool ShouldSerializeBackColor()
		{
			return !this._backColor.IsEmpty;
		}

		private new void ResetBackColor()
		{
			this._backColor = Color.Empty;
			Update();
		}

		private Color GetThemeBackColor()
		{
			IWisejControl me = this;
			return me.Theme.GetColor(this.AppearanceKey, "backgroundColor");
		}

		/// <summary>
		/// Returns or sets the line width, in pixels, used to draw the progress ring.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">The value is less than 0.</exception>
		/// <remarks>
		/// When not set, the width is read from the "lineWidth" property of the theme appearance
		/// identified by <see cref="AppearanceKey"/>; if the theme doesn't define a value of at least 1, the default is 5 pixels.
		/// </remarks>
		[Browsable(true)]
		[Description("Gets or sets the line width use to draw the circle.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new int LineWidth
		{
			get
			{
				return this._lineWidth == -1
					? GetThemeLineWidth()
					: this._lineWidth;
			}
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("LineWidth");

				if (this._lineWidth != value)
				{
					this._lineWidth = value;
					Update();
				}
			}
		}
		private int _lineWidth = -1;

		private bool ShouldSerializeLineWidth()
		{
			return this._lineWidth > -1;
		}

		private void ResetLineWidth()
		{
			this._lineWidth = -1;
			Update();
		}

		private int GetThemeLineWidth()
		{
			IWisejControl me = this;
			int width = me.Theme.GetProperty<int>(this.AppearanceKey, "lineWidth");
			return width < 1 ? 5 : width;
		}

		/// <summary>
		/// Returns or sets the style of the end caps of the progress arc.
		/// </summary>
		[Browsable(true)]
		[DefaultValue(CanvasLineCap.Butt)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Sets or returns the style of the end caps for a line.")]
		public new CanvasLineCap LineCap
		{
			get { return this._lineCap ?? base.LineCap; }
			set
			{
				if (this._lineCap != value)
				{
					this._lineCap = value;
					base.LineCap = value;
					
					Update();
				}
			}
		}
		private CanvasLineCap? _lineCap;

		/// <summary>
		/// Returns or sets the progress value between 0 and 100.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">The value is less than 0 or greater than 100.</exception>
		/// <remarks>
		/// The value is a percentage: the progress arc spans 360 * <see cref="Value"/> / 100 degrees and,
		/// when <see cref="ShowValue"/> is true, the text in the middle of the circle shows the value followed by "%".
		/// Every change redraws the control.
		/// </remarks>
		/// <example>
		/// Converting a count of processed items to a percentage:
		/// <code><![CDATA[
		/// private void UpdateProgress(int processed, int total)
		/// {
		///     this.progressCircle1.Value = Math.Min(100, processed * 100 / total);
		/// }
		/// ]]></code>
		/// </example>
		[DefaultValue(0)]
		[Description("Gets or sets the progress value between 0 and 100.")]
		public int Value
		{
			get { return this._value; }
			set
			{
				if (value < 0 || value > 100)
					throw new ArgumentOutOfRangeException("Value");

				if (this._value != value)
				{
					this._value = value;
					Update();
				}
			}
		}
		private int _value = 0;

		/// <summary>
		/// Returns or sets a value that indicates whether the circle is filled with the background color.
		/// </summary>
		/// <remarks>
		/// The fill color is the value of the <see cref="BackColor"/> property.
		/// </remarks>
		[DefaultValue(false)]
		[Description("Gets or sets a value that indicates whether the circle is filled with the background color.")]
		public bool FillCircle
		{
			get { return this._fillCircle; }
			set
			{
				if (this._fillCircle != value)
				{
					this._fillCircle = value;
					Update();
				}
			}
		}
		private bool _fillCircle = false;

		/// <summary>
		/// Returns or sets a value that indicates whether the percentage text is displayed in the middle of the circle.
		/// </summary>
		/// <remarks>
		/// The text is the <see cref="Value"/> followed by "%", drawn using <see cref="Font"/> and <see cref="ForeColor"/>.
		/// </remarks>
		[DefaultValue(true)]
		[Description("Gets or sets a value that indicates whether the text is displayed in the middle of the circle.")]
		public bool ShowValue
		{
			get { return this._showText; }
			set
			{
				if (this._showText != value)
				{
					this._showText = value;
					Update();
				}
			}
		}
		private bool _showText = true;

		/// <summary>
		/// Draws the circle using the HTML5 canvas instructions.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRedraw(EventArgs e)
		{
			base.OnRedraw(e);

			// clear the canvas.
			base.ClearRect(this.DisplayRectangle);

			// calculate the position and radius.
			int lineWidth = this.LineWidth;
			int centerX = this.Width / 2;
			int centerY = this.Height / 2;
			int radius = Math.Min(centerX, centerY) - (lineWidth / 2);

			// fill the circle?
			if (this._fillCircle)
			{
				base.Arc(centerX, centerY, radius - lineWidth, 0, 360);
				base.FillStyle = this.BackColor;
				base.Fill();
			}

			// draw the circle empty perimeter.
			base.LineWidth = 1;
			base.StrokeStyle = this.ForeColor;
			base.BeginPath();
			base.Arc(centerX, centerY, radius, 0, 360);
			base.Stroke();
			base.BeginPath();
			base.Arc(centerX, centerY, radius - LineWidth, 0, 360);
			base.Stroke();

			// fill the perimeter line proportional to the value.
			base.LineWidth = lineWidth;
			base.LineCap = this.LineCap;
			base.BeginPath();
			base.Arc(centerX, centerY, radius - lineWidth / 2, 0, 360 * this.Value / 100);
			base.Stroke();

			// draw the text?
			if (this._showText)
			{
				base.TextFont = this.Font;
				base.FillStyle = this.ForeColor;
				base.TextAlign = CanvasTextAlign.Center;
				base.TextBaseline = CanvasTextBaseline.Middle;
				string percent = this.Value.ToString() + "%";
				base.FillText(percent, centerX, centerY);
			}
		}
	}
}
