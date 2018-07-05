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
	/// The ProgressCircle control is an example on how to create a custom
	/// component in Wisej by extending the <see cref="T:Wisej.Web.Canvas"/> control.
	/// 
	/// The new component is entirely drawn on the client side on a HTML5 Canvas element
	/// receiving the drawing instructions from the server.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(ProgressCircle))]
	public class ProgressCircle : Canvas
	{
		/// <summary>
		/// Sets the appearance key for the theme engine.
		/// </summary>
		/// <remarks>
		/// Overridden to change the appearance key without
		/// serializing the new default in the designer.
		/// </remarks>
		[DefaultValue("progress-circle")]
		public override string AppearanceKey
		{
			get { return base.AppearanceKey ?? "progress-circle"; }
			set { base.AppearanceKey = value; }
		}

		/// <summary>
		///  Gets or sets the font of the text displayed by the control.
		/// </summary>
		/// <returns>A <see cref="T:System.Drawing.Font" />.</returns>
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
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}

		/// <summary>
		/// Returns or sets the fill color when the property <see cref="P:Wisej.Web.Ext.ProgressCircle.FillCircle"/> is set to true.
		/// </summary>
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
		/// Returns or sets the line width use to draw the circle.
		/// </summary>
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
		/// Sets or returns the style of the end caps for a line.
		/// </summary>
		[Browsable(true)]
		[DefaultValue(CanvasLineCap.Butt)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Sets or returns the style of the end caps for a line.")]
		public new CanvasLineCap LineCap
		{
			get { return base.LineCap; }
			set
			{
				if (base.LineCap != value)
				{
					base.LineCap = value;
					Update();
				}
			}
		}

		/// <summary>
		/// Returns or sets the progress value between 0 and 100.
		/// </summary>
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
		/// Returns or sets a value that indicates whether the text is displayed in the middle of the circle.
		/// </summary>
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
