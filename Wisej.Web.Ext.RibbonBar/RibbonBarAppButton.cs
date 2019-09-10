///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using Wisej.Base;
using Wisej.Core;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents the main application button in a <see cref="RibbonBar"/> displayed
	/// before the first <see cref="RibbonBarPage"/>.
	/// </summary>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	public class RibbonBarAppButton : Wisej.Web.Component
	{
		#region Constructor

		internal RibbonBarAppButton(RibbonBar owner)
		{
			Debug.Assert(owner != null);

			this.Parent = owner;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the 
		/// <see cref="RibbonBar"/> that owns this
		/// <see cref="RibbonBarAppButton"/>.
		/// </summary>
		[Browsable(false)]
		public RibbonBar Parent
		{
			get;
			internal set;
		}

		/// <summary>
		/// Returns the <see cref="RibbonBar"/> that contains this <see cref="RibbonBarAppButton"/>;
		/// </summary>
		[Browsable(false)]
		public RibbonBar RibbonBar
		{
			get { return this.Parent; }
		}

		/// <summary>
		/// Returns or sets whether the <see cref="RibbonBarAppButton"/> is visible or hidden.
		/// </summary>
		[Localizable(true)]
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("Returns or sets whether the RibbonBarApplicationButton is visible or hidden.")]
		public virtual bool Visible
		{
			get { return this._visible; }
			set
			{
				if (this._visible != value)
				{
					this._visible = value;
					Update();
				}
			}
		}
		private bool _visible = false;

		/// <summary>
		/// Returns or sets the background color of the <see cref="RibbonBarAppButton"/> tab button.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[SRCategory("CatAppearance")]
		[SRDescription("Returns or sets the background color of the RibbonBarApplicationButton tab button.")]
		public Color BackColor
		{
			get { return this._backColor; }
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

		/// <summary>
		/// Returns or sets the text color of the <see cref="RibbonBarAppButton"/> tab button.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[SRCategory("CatAppearance")]
		[SRDescription("Returns or sets the text color of the RibbonBarApplicationButton tab button.")]
		public Color ForeColor
		{
			get { return this._foreColor; }
			set
			{
				if (this._foreColor != value)
				{
					this._foreColor = value;
					Update();
				}
			}
		}
		private Color _foreColor = Color.Empty;

		/// <summary>
		/// Returns or sets the text of the <see cref="RibbonBarAppButton"/>.
		///</summary>
		/// <returns>The text displayed in the <see cref="RibbonBarAppButton"/>.</returns>
		[Localizable(true)]
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the text of the RibbonBarApplicationButton.")]
		public virtual string Text
		{
			get
			{
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
		private string _text = string.Empty;

		/// <summary>
		/// Returns or sets the tooltip text for the <see cref="RibbonBarAppButton"/>.
		///</summary>
		/// <returns>The text displayed in a tooltip for the <see cref="RibbonBarAppButton"/>.</returns>
		[Localizable(true)]
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the tooltip for the RibbonBarApplicationButton.")]
		public virtual string ToolTipText
		{
			get
			{
				return this._tooltipText;
			}
			set
			{
				value = value ?? string.Empty;

				if (this._tooltipText != value)
				{
					this._tooltipText = value;
					Update();
				}
			}
		}
		private string _tooltipText = string.Empty;

		/// <summary>
		/// Returns or sets the image that is displayed in the <see cref="RibbonBarAppButton" />.
		///</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> displayed in the <see cref="RibbonBarAppButton" />. The default value is null.</returns>
		[Localizable(true)]
		[PostbackProperty]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the image that is displayed in the RibbonBarApplicationButton.")]
		public Image Image
		{
			get { return this._imageSettings == null ? null : this._imageSettings.Image; }
			set { this.ImageSettings.Image = value; }
		}

		/// <summary>
		/// Returns or sets the theme name or URL for the image to display in the <see cref="RibbonBarAppButton" />.
		/// </summary>
		/// <returns>The theme name or URL for the image to display in the <see cref="RibbonBarAppButton" />.</returns>
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the theme name or URL for the image to display in the RibbonBarApplicationButton.")]
		[TypeConverter("Wisej.Design.ImageSourceConverter, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171")]
		[Editor("Wisej.Design.ImageSourceEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
		public string ImageSource
		{
			get { return this._imageSettings == null ? null : this._imageSettings.ImageSource; }
			set { this.ImageSettings.ImageSource = value; }
		}

		private bool ShouldSerializeImage()
		{
			return this._imageSettings == null ? false : this._imageSettings.ShouldSerializeImage();
		}
		private void ResetImage()
		{
			if (this._imageSettings != null) this._imageSettings.ResetImage();
		}
		private bool ShouldSerializeImageSource()
		{
			return this._imageSettings == null ? false : this._imageSettings.ShouldSerializeImageSource();
		}
		private void ResetImageSource()
		{
			if (this._imageSettings != null) this._imageSettings.ResetImageSource();
		}

		/// <summary>
		/// Returns or sets the index value of the image assigned to the <see cref="RibbonBarAppButton" />.
		///</summary>
		/// <returns>The index value of the <see cref="T:System.Drawing.Image" /> assigned to the <see cref="RibbonBarAppButton" />. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than -1.</exception>
		[DefaultValue(-1)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the index value of the image assigned to the RibbonBarApplicationButton.")]
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("Wisej.Design.ImageIndexEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
		public int ImageIndex
		{
			get { return this._imageSettings == null ? -1 : this._imageSettings.ImageIndex; }
			set { this.ImageSettings.ImageIndex = value; }
		}

		/// <summary>
		/// Returns or sets the name of the image assigned to the <see cref="RibbonBarAppButton" />.
		///</summary>
		/// <returns>The name of the <see cref="T:System.Drawing.Image" /> assigned to the <see cref="RibbonBarAppButton" />.</returns>
		[DefaultValue("")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the name of the image assigned to the RibbonBarApplicationButton.")]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("Wisej.Design.ImageIndexEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
		public string ImageKey
		{
			get { return this._imageSettings == null ? string.Empty : this._imageSettings.ImageKey; }
			set { this.ImageSettings.ImageKey = value; }
		}

		/// <summary>
		/// Creates the property manager for the Image properties on first use.
		/// </summary>
		internal Wisej.Web.ImagePropertySettings ImageSettings
		{
			get
			{
				if (this._imageSettings == null)
					this._imageSettings = new RibbonBarImageProperties(this);

				return this._imageSettings;
			}
		}
		internal Wisej.Web.ImagePropertySettings _imageSettings;

		/// <summary>
		/// Overrides the standard ImagePropertySettings to 
		/// retrieve the ImageList from the parent ToolBar control.
		/// </summary>
		private class RibbonBarImageProperties : ImagePropertySettings
		{
			RibbonBarAppButton item;

			public RibbonBarImageProperties(RibbonBarAppButton owner)
				: base(owner)
			{
				this.item = owner;
			}

			public override ImageList ImageList
			{
				get
				{
					return this.item?.RibbonBar?.ImageList;
				}
				set { }
			}

			protected override void Update()
			{
				this.item?.Update();
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Updates the component on the client.
		/// </summary>
		public override void Update()
		{
			if (this.RibbonBar?.Site?.DesignMode ?? false)
				this.RibbonBar?.Refresh();

			base.Update();
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current object.</returns>
		public override string ToString()
		{
			return this.Text;
		}

		#endregion

		#region Wisej Implementation

		// Handles clicks and taps from the client: "execute" event.
		private void ProcessExecuteWebEvent(WisejEventArgs e)
		{
			// determine if the button can be clicked.
			if (this.Visible)
			{
				this.RibbonBar?.OnAppButtonClick(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(Core.WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "execute":
					ProcessExecuteWebEvent(e);
					break;

				default:
					base.OnWebEvent(e);
					break;
			}
		}

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);
			IWisejComponent me = this;

			config.className = "wisej.web.ribbonBar.AppButton";
			config.visible = this.Visible;
			config.label = TextUtils.EscapeText(this.Text, false, false, false);
			config.toolTipText = this.ToolTipText;
			config.textColor = this.ForeColor;
			config.backgroundColor = this.BackColor;

			if (this._imageSettings != null)
			{
				config.icon = this._imageSettings.GetSource("Image");
			}

			if (!me.DesignMode)
			{
				config.wiredEvents = new WiredEvents();
				config.wiredEvents.Add("execute");
			}
		}

		#endregion

	}
}