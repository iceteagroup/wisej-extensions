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

using System;
using System.ComponentModel;
using System.Drawing;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.ToolStrip
{
	/// <summary>
	/// Represents a line used to group items of a <see cref="ToolStrip" /> or the drop-down items of a <see cref="MenuStrip" /> or <see cref="ContextMenuStrip" /> or other <see cref="ToolStripDropDown" /> control.
	///</summary>
	public class ToolStripSeparator : ToolStripItem
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolStripSeparator" /> class. 
		///</summary>
		public ToolStripSeparator()
		{
			// TODO: Implement
		}

		#endregion

		#region Events

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler EnabledChanged;

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler DisplayStyleChanged;

		/// <summary>
		/// This event is not relevant to this class.
		///</summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler TextChanged;

		#endregion

		/// <summary>
		/// Raises the <see cref="ToolStripItem.EnabledChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnEnabledChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.EnabledChanged != null))
			{
				EnabledChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.DisplayStyleChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnDisplayStyleChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.DisplayStyleChanged != null))
			{
				DisplayStyleChanged(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="ToolStripItem.TextChanged" /> event.
		///</summary>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data. </param>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void OnTextChanged(System.EventArgs e)
		{
			// TODO: Add new to the method, it hides a base event.
			if ((this.TextChanged != null))
			{
				TextChanged(this, e);
			}
		}

		#region Properties

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if enabled; otherwise, false. </returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AutoToolTip
		{
			get
			{
				return this._autoToolTip;
			}
			set
			{
				if ((this._autoToolTip != value))
				{
					this._autoToolTip = value;
				}
			}
		}

		private bool _autoToolTip;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>An <see cref="System.Drawing.Image" />.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[SRDescription("ToolStripItemImageDescr")]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(null)]
		public override Image BackgroundImage
		{
			get
			{
				return this._backgroundImage;
			}
			set
			{
				if ((this._backgroundImage != value))
				{
					this._backgroundImage = value;
				}
			}
		}

		private Image _backgroundImage;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>An <see cref="ImageLayout" /> value.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatAppearance")]
		[DefaultValue(ImageLayout.Tile)]
		[Localizable(true)]
		[SRDescription("ControlBackgroundImageLayoutDescr")]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return this._backgroundImageLayout;
			}
			set
			{
				if ((this._backgroundImageLayout != value))
				{
					this._backgroundImageLayout = value;
				}
			}
		}

		private ImageLayout _backgroundImageLayout;

		/// <summary>
		/// Gets a value indicating whether the <see cref="ToolStripSeparator" /> can be selected. 
		///</summary>
		/// <returns>true if the component using the <see cref="ToolStripSeparator" /> is in design mode; otherwise, false.</returns>
		[Browsable(false)]
		public override bool CanSelect
		{
			get
			{
				return this._canSelect;
			}
		}

		private bool _canSelect;

		public override Size DefaultSize
		{
			get
			{
				return this._defaultSize;
			}
		}

		private Size _defaultSize;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if enabled; otherwise, false. </returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool DoubleClickEnabled
		{
			get
			{
				return this._doubleClickEnabled;
			}
			set
			{
				if ((this._doubleClickEnabled != value))
				{
					this._doubleClickEnabled = value;
				}
			}
		}

		private bool _doubleClickEnabled;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemEnabledDescr")]
		[DefaultValue(true)]
		public override bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if ((this._enabled != value))
				{
					this._enabled = value;
				}
			}
		}

		private bool _enabled;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="ToolStripItemDisplayStyle" /> value.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripItemDisplayStyle DisplayStyle
		{
			get
			{
				return this._displayStyle;
			}
			set
			{
				if ((this._displayStyle != value))
				{
					this._displayStyle = value;
				}
			}
		}

		private ToolStripItemDisplayStyle _displayStyle;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Font" /> value.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ToolStripItemFontDescr")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		public override Font Font
		{
			get
			{
				return this._font;
			}
			set
			{
				if ((this._font != value))
				{
					this._font = value;
				}
			}
		}

		private Font _font;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="VisualStyles.ContentAlignment" /> value.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ContentAlignment ImageAlign
		{
			get
			{
				return this._imageAlign;
			}
			set
			{
				if ((this._imageAlign != value))
				{
					this._imageAlign = value;
				}
			}
		}

		private ContentAlignment _imageAlign;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>An <see cref="System.Drawing.Image" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageDescr")]
		public override Image Image
		{
			get
			{
				return this._image;
			}
			set
			{
				if ((this._image != value))
				{
					this._image = value;
				}
			}
		}

		private Image _image;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>An <see cref="System.Int32" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ImageIndex
		{
			get
			{
				return this._imageIndex;
			}
			set
			{
				if ((this._imageIndex != value))
				{
					this._imageIndex = value;
				}
			}
		}

		private int _imageIndex;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.String" />.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ImageKey
		{
			get
			{
				return this._imageKey;
			}
			set
			{
				if ((this._imageKey != value))
				{
					this._imageKey = value;
				}
			}
		}

		private string _imageKey;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.Color" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color ImageTransparentColor
		{
			get
			{
				return this._imageTransparentColor;
			}
			set
			{
				if ((this._imageTransparentColor != value))
				{
					this._imageTransparentColor = value;
				}
			}
		}

		private Color _imageTransparentColor;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="ToolStripItemImageScaling" /> value.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripItemImageScaling ImageScaling
		{
			get
			{
				return this._imageScaling;
			}
			set
			{
				if ((this._imageScaling != value))
				{
					this._imageScaling = value;
				}
			}
		}

		private ToolStripItemImageScaling _imageScaling;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.String" />.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatAppearance")]
		[DefaultValue("")]
		[Localizable(true)]
		[SRDescription("ToolStripItemTextDescr")]
		public override string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if ((this._text != value))
				{
					this._text = value;
				}
			}
		}

		private string _text;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="System.Drawing.ContentAlignment" /> value.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ContentAlignment TextAlign
		{
			get
			{
				return this._textAlign;
			}
			set
			{
				if ((this._textAlign != value))
				{
					this._textAlign = value;
				}
			}
		}

		private ContentAlignment _textAlign;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="ToolStripTextDirection" /> value.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		[SRDescription("ToolStripTextDirectionDescr")]
		[SRCategory("CatAppearance")]
		public override ToolStripTextDirection TextDirection
		{
			get
			{
				return this._textDirection;
			}
			set
			{
				if ((this._textDirection != value))
				{
					this._textDirection = value;
				}
			}
		}

		private ToolStripTextDirection _textDirection;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A <see cref="TextImageRelation" /> value.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TextImageRelation TextImageRelation
		{
			get
			{
				return this._textImageRelation;
			}
			set
			{
				if ((this._textImageRelation != value))
				{
					this._textImageRelation = value;
				}
			}
		}

		private TextImageRelation _textImageRelation;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>A string.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ToolTipText
		{
			get
			{
				return this._toolTipText;
			}
			set
			{
				if ((this._toolTipText != value))
				{
					this._toolTipText = value;
				}
			}
		}

		private string _toolTipText;

		/// <summary>
		/// This property is not relevant to this class.
		///</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool RightToLeftAutoMirrorImage
		{
			get
			{
				return this._rightToLeftAutoMirrorImage;
			}
			set
			{
				if ((this._rightToLeftAutoMirrorImage != value))
				{
					this._rightToLeftAutoMirrorImage = value;
				}
			}
		}

		private bool _rightToLeftAutoMirrorImage;

		#endregion

		#region Methods

		/// <summary>
		/// This method is not relevant to this class.
		///</summary>
		/// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			// TODO: Implement
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			// TODO: Implement
			return new Wisej.Web.Ext.ToolStrip.Compatibility.AccessibleObject();
		}

		#endregion

		#region Wisej Implementation

		protected override void OnWebEvent(WisejEventArgs e)
		{
			base.OnWebEvent(e);
		}

		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender(config);
		}

		protected override void OnWebUpdate(dynamic config)
		{
			base.OnWebUpdate(config);
		}

		#endregion
	}
}