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
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.Barcode
{
	/// <summary>
	/// Represents a barcode widget supporting several formats.
	/// </summary>
	/// <remarks>
	/// The value encoded in the barcode is the <see cref="Control.Text"/> of the control. The barcode is drawn on
	/// the server with the ZXing.NET library, sized to fill the control minus its <see cref="Control.Padding"/>, and
	/// uses the <see cref="Control.ForeColor"/>, <see cref="Control.BackColor"/> and <see cref="Control.Font"/> of the control.
	/// </remarks>
	/// <example>
	/// The following example shows a QR code with a link and a Code 128 barcode with a product number:
	/// <code><![CDATA[
	/// var qrCode = new Barcode
	/// {
	///     BarcodeType = BarcodeType.QR,
	///     Text = "https://wisej.com",
	///     ShowLabel = false,
	///     Size = new Size(150, 150)
	/// };
	///
	/// var productCode = new Barcode
	/// {
	///     BarcodeType = BarcodeType.Code_128,
	///     Text = "PRD-004521",
	///     Size = new Size(250, 80),
	///     Location = new Point(0, 160)
	/// };
	///
	/// this.Controls.AddRange(new Control[] { qrCode, productCode });
	/// ]]></code>
	/// </example>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Barcode))]
	[DefaultProperty("Value")]
	[ApiCategory("Barcode")]
	[Description("Represents a barcode widget supporting several formats.")]
	public class Barcode : Control, IWisejControl
	{
		/// <summary>
		/// Creates a new instance of the <see cref="T:Wisej.Web.Ext.Barcode"/> control.
		/// </summary>
		/// <remarks>
		/// The new control shows a Code 39 barcode with its label, uses the Arial 10pt font for the label
		/// and has a default size of 100 x 40 pixels. It can't receive the focus.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var barcode = new Barcode();
		/// barcode.BarcodeType = BarcodeType.EAN_13;
		/// barcode.Text = "5901234123457";
		/// barcode.Dock = DockStyle.Top;
		/// barcode.Height = 100;
		///
		/// this.Controls.Add(barcode);
		/// ]]></code>
		/// </example>
		public Barcode()
		{
			base.TabStop = false;
			base.SetStyle(ControlStyles.Selectable, false);

			this.Paint += Barcode_Paint;
			this.Font = new Font("Arial", 10, FontStyle.Regular); // ZXing.Rendering.BitmapRenderer.DefaultTextFont
		}

		#region Events

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoSizeChanged
		{
			add { base.AutoSizeChanged += value; }
			remove { base.AutoSizeChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add { base.BackgroundImageChanged += value; }
			remove { base.BackgroundImageChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add { base.BackgroundImageLayoutChanged += value; }
			remove { base.BackgroundImageLayoutChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add { base.FontChanged += value; }
			remove { base.FontChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add { base.BackColorChanged += value; }
			remove { base.BackColorChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add { base.ForeColorChanged += value; }
			remove { base.ForeColorChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add { base.ImeModeChanged += value; }
			remove { base.ImeModeChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add { base.TextChanged += value; }
			remove { base.TextChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabIndexChanged
		{
			add { base.TabIndexChanged += value; }
			remove { base.TabIndexChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add { base.TabStopChanged += value; }
			remove { base.TabStopChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Enter
		{
			add { base.Enter += value; }
			remove { base.Enter -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Leave
		{
			add { base.Leave += value; }
			remove { base.Leave -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add { base.RightToLeftChanged += value; }
			remove { base.RightToLeftChanged -= value; }
		}

		/// <summary>
		/// Fired when a key is pressed when the control has focus.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add { base.KeyDown += value; }
			remove { base.KeyDown -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add { base.KeyPress += value; }
			remove { base.KeyPress -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyUp
		{
			add { base.KeyUp += value; }
			remove { base.KeyUp -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CausesValidationChanged
		{
			add { base.CausesValidationChanged += value; }
			remove { base.CausesValidationChanged -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Validated
		{
			add { base.Validated += value; }
			remove { base.Validated -= value; }
		}

		/// <summary>
		/// This event is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event CancelEventHandler Validating
		{
			add { base.Validating += value; }
			remove { base.Validating -= value; }
		}

		#endregion

		#region Properties

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string BackgroundImageSource
		{
			get { return base.BackgroundImageSource; }
			set { base.BackgroundImageSource = value; }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <returns>An <see cref="T:Wisej.Web.ImageLayout" />.</returns>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get { return base.BackgroundImageLayout; }
			set { base.BackgroundImageLayout = value; }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool Enabled
		{
			get { return base.Enabled; }
			set { base.Enabled = value; }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool TabStop
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Focusable
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This property is not relevant for this class.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[DefaultValue(0)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int TabIndex
		{
			get { return base.TabIndex; }
			set { base.TabIndex = value; }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AllowDrag
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AllowDrop
		{
			get { return false; }
			set { }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override RightToLeft RightToLeft
		{
			get { return RightToLeft.No; }
			set { }
		}

		

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string AccessibleName
		{
			get { return string.Empty; }
			set { }
		}

		/// <summary>
		/// This member is not meaningful for this control.
		/// </summary>
		/// <exclude/>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string AccessibleDescription
		{
			get { return string.Empty; }
			set { }
		}

		/// <summary>
		/// Indicates the border style for the control.
		/// </summary>
		/// <returns>One of the <see cref="T:Wisej.Web.BorderStyle" /> values. The default is BorderStyle.None.</returns>
		[DefaultValue(BorderStyle.None)]
		[SRCategory("CatAppearance")]
		[SRDescription("Indicates the border style for the control.")]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				return this._borderStyle;
			}
			set
			{
				if (this._borderStyle != value)
				{
					this._borderStyle = value;

					Refresh();

					OnStyleChanged(EventArgs.Empty);
				}
			}
		}
		private BorderStyle _borderStyle = BorderStyle.None;

		/// <summary>
		/// Returns or sets the barcode symbology to use.
		/// </summary>
		[Description("Gets or sets the barcode symbology to use.")]
		public BarcodeType BarcodeType
		{
			get { return this._barcode; }
			set
			{
				if (this._barcode != value)
				{
					this._barcode = value;
					Update();
				}
			}

		}
		private BarcodeType _barcode = BarcodeType.Code_39;

		/// <summary>
		/// Shows or hides the barcode label.
		/// </summary>
		[DefaultValue(true)]
		[Description("Shows or hides the barcode label.")]
		public bool ShowLabel
		{
			get { return this._showLabel; }
			set
			{
				if (this._showLabel != value)
				{
					this._showLabel = value;
					Update();
				}
			}
		}
		private bool _showLabel = true;

		/// <summary>
		/// The default <see cref="T:System.Drawing.Size" /> of the control.
		/// </summary>
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 40);
			}
		}

		/// <summary>
		/// The number of pixels to offset from the bottom of the barcode.
		/// </summary>
		[DefaultValue(0)]
		[Description("The number of pixels to offset from the bottom of the barcode.")]
		public int VerticalOffset
		{
			get
			{
				return this._verticalOffset;
			}
			set
			{
				if (this._verticalOffset == value)
					return;

				this._verticalOffset = value;
			}
		}
		private int _verticalOffset = 0;

		#endregion

		#region Methods

		/// <summary>
		/// Draws the barcode.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Barcode_Paint(object sender, PaintEventArgs e)
		{
			using (Image bitmap = DrawBarCode())
			{
				if (bitmap != null)
				{
					var x = this.Padding.Left;
					var y = this.Padding.Top;
					e.Graphics.DrawImageUnscaled(bitmap, x, y);
				}
			}
		}

		/// <summary>
		/// Draws the barcode and returns the image object.
		/// </summary>
		/// <remarks>
		/// The image uses the current <see cref="BarcodeType"/>, <see cref="ShowLabel"/>, <see cref="VerticalOffset"/>,
		/// size, padding, colors and font of the control, and encodes the <see cref="Control.Text"/> of the control.
		/// The caller is responsible for disposing the returned image. An exception is thrown when the text
		/// can't be encoded with the selected <see cref="BarcodeType"/>, for example letters in an <see cref="F:Wisej.Web.Ext.Barcode.BarcodeType.EAN_13"/> barcode.
		/// </remarks>
		/// <returns>An <see cref="Image"/> with the barcode, or null if nothing could be drawn.</returns>
		/// <example>
		/// The following example saves the barcode as a PNG file and downloads it to the browser:
		/// <code><![CDATA[
		/// private void buttonDownload_Click(object sender, EventArgs e)
		/// {
		///     using (var image = this.barcode1.DrawBarCode())
		///     using (var stream = new MemoryStream())
		///     {
		///         image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
		///         stream.Position = 0;
		///
		///         Application.Download(stream, "barcode.png");
		///     }
		/// }
		/// ]]></code>
		/// </example>
		public Image DrawBarCode()
		{
			barcode.Format = (ZXing.BarcodeFormat)this.BarcodeType;
			barcode.Options.PureBarcode = !this.ShowLabel;
			barcode.Options.VerticalOffset = this.VerticalOffset;
			barcode.Options.Width = this.Width - this.Padding.Horizontal;
			barcode.Options.Height = this.Height - this.Padding.Vertical;
			ZXing.Rendering.BitmapRenderer renderer = (ZXing.Rendering.BitmapRenderer)barcode.Renderer;
			renderer.Background = ResolveColor(this.BackColor);
			renderer.Foreground = ResolveColor(this.ForeColor);
			renderer.TextFont = this.Font;

			return barcode.Write(this.Text);
		}
		private ZXing.BarcodeWriter barcode = new ZXing.BarcodeWriter();

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Returns the theme appearance key for this control.
		/// </summary>
		string IWisejControl.AppearanceKey
		{
			get { return this.AppearanceKey ?? "panel"; }
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);
			IWisejControl me = this;

			config.className = "wisej.web.Control";
			config.borderStyle = this.BorderStyle;
		}

		#endregion

	}
}
