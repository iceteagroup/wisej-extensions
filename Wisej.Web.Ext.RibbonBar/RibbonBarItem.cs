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
using System.Drawing;
using System.Drawing.Design;
using Wisej.Base;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a single item in a <see cref="RibbonBarGroup"/>.
	/// </summary>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	public abstract class RibbonBarItem : Wisej.Web.Component
	{
		#region Events

		/// <summary>
		/// Fired when the <see cref="RibbonBarItem"/> is clicked.
		/// </summary>
		[SRCategory("CatAction")]
		[Description("Fired when the RibbonBarItem is clicked.")]
		public event EventHandler Click
		{
			add { AddHandler(nameof(Click), value); }
			remove { RemoveHandler(nameof(Click), value); }
		}

		/// <summary>
		/// Fires the <see cref="Click" /> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void OnClick(EventArgs e)
		{
			((EventHandler)this.Events[nameof(Click)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the 
		/// <see cref="RibbonBarGroup"/> that owns this
		/// <see cref="RibbonBarItem"/>.
		/// </summary>
		[Browsable(false)]
		public virtual RibbonBarGroup Parent
		{
			get { return this._parent; }
			internal set { this._parent = value; }
		}
		private RibbonBarGroup _parent;

		/// <summary>
		/// Returns the <see cref="RibbonBar"/> that contains this <see cref="RibbonBarItem"/>;
		/// </summary>
		[Browsable(false)]
		public RibbonBar RibbonBar
		{
			get { return this.Parent?.Parent?.Parent; }
		}

		/// <summary>
		/// Returns or sets a value indicating whether a new column starts after
		/// this <see cref="RibbonBarItem"/>.
		/// </summary>
		[SRCategory("CatLayout")]
		[Description("Returns or sets a value indicating whether a new column starts after this RibbonBarItem.")]
		public virtual bool ColumnBreak
		{
			get { return this._columnBreak; }
			set
			{
				if (this._columnBreak != value)
				{
					this._columnBreak = value;
					Update();
				}
			}
		}
		private bool _columnBreak = false;

		private bool ShouldSerializeColumnBreak()
		{
			return this._columnBreak;
		}

		private void ResetColumnBreak()
		{
			this.ColumnBreak = false;
		}

		/// <summary>
		/// Returns or sets whether the <see cref="RibbonBarItem"/> can respond to user interaction.
		/// </summary>
		[Localizable(true)]
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("Returns or sets whether the RibbonBarItem can respond to user interaction.")]
		public virtual bool Enabled
		{
			get { return this._enabled; }
			set
			{
				if (this._enabled != value)
				{
					this._enabled = value;
					Update();
				}
			}
		}
		private bool _enabled = true;

		/// <summary>
		/// Returns or sets whether the <see cref="RibbonBarItem"/> is visible or hidden.
		/// </summary>
		[Localizable(true)]
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("Returns or sets whether the RibbonBarItem is visible or hidden.")]
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
		private bool _visible = true;

		/// <summary>
		/// Returns or sets the text of the <see cref="RibbonBarItem"/>.
		///</summary>
		/// <returns>The text displayed in the <see cref="RibbonBarItem"/>.</returns>
		[Localizable(true)]
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the text of the RibbonBarItem.")]
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
		/// Returns or sets the name of the <see cref="RibbonBarItem" />. 
		///</summary>
		/// <returns>The name of the <see cref="RibbonBarItem" />.</returns>
		[Browsable(false)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the name for the RibbonBarItem.")]
		public string Name
		{
			get
			{
				return this.Site != null
				  ? this.Site.Name
				  : this._name;
			}
			set
			{
				this._name = value ?? string.Empty;

				if (this.Site != null)
					this.Site.Name = this._name;
			}
		}
		private string _name = string.Empty;

		/// <summary>
		/// Returns or sets the tooltip text for the <see cref="RibbonBarItem"/>.
		///</summary>
		/// <returns>The text displayed in a tooltip for the <see cref="RibbonBarItem"/>.</returns>
		[Localizable(true)]
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the tooltip for the RibbonBarItem.")]
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
		/// Returns or sets the image that is displayed next to a <see cref="RibbonBarItem" />.
		///</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> displayed next to the <see cref="RibbonBarItem" />. The default value is null.</returns>
		[Localizable(true)]
		[PostbackProperty]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the image that is displayed next to a RibbonBarItem.")]
		public Image Image
		{
			get { return this._imageSettings == null ? null : this._imageSettings.Image; }
			set { this.ImageSettings.Image = value; }
		}

		/// <summary>
		/// Returns or sets the theme name or URL for the image to display next to a <see cref="RibbonBarItem" />.
		/// </summary>
		/// <returns>The theme name or URL for the image to display next to the <see cref="RibbonBarItem" />.</returns>
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the theme name or URL for the image to display next to a RibbonBarItem.")]
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
		/// Returns or sets the index value of the image assigned to the <see cref="RibbonBarItem" />.
		///</summary>
		/// <returns>The index value of the <see cref="T:System.Drawing.Image" /> assigned to the <see cref="RibbonBarItem" />. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than -1.</exception>
		[DefaultValue(-1)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the index value of the image assigned to the RibbonBarItem.")]
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("Wisej.Design.ImageIndexEditor, Wisej.Framework.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", typeof(UITypeEditor))]
		public int ImageIndex
		{
			get { return this._imageSettings == null ? -1 : this._imageSettings.ImageIndex; }
			set { this.ImageSettings.ImageIndex = value; }
		}

		/// <summary>
		/// Returns or sets the name of the image assigned to the <see cref="RibbonBarItem" />.
		///</summary>
		/// <returns>The name of the <see cref="T:System.Drawing.Image" /> assigned to the <see cref="RibbonBarItem" />.</returns>
		[DefaultValue("")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the name of the image assigned to the RibbonBarItem.")]
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
			RibbonBarItem item;

			public RibbonBarImageProperties(RibbonBarItem owner)
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
		/// Disposes of the resources (other than memory) used by the <see cref="RibbonBarGroup" />.
		/// </summary>
		/// <param name="disposing">true when this method is called by the application rather than a finalizer.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Parent?.Items.Remove(this);
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Validates the current control.
		/// </summary>
		/// <returns>true if the active control is validated.</returns>
		protected bool ValidateActiveControl()
		{
			return this.RibbonBar?.ValidateActiveControl() ?? false;
		}

		/// <summary>
		/// Updates the component on the client.
		/// </summary>
		public override void Update()
		{
			if (this.DesignMode)
				this.RibbonBar?.Update();

			base.Update();
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current object.</returns>
		public override string ToString()
		{
			return String.Concat(base.ToString(), ", Text: ", this.Text);
		}

		#endregion

		#region Wisej Implementation

		/// <summary>
		/// Renders the client component.
		/// </summary>
		/// <param name="config">Dynamic configuration object.</param>
		protected override void OnWebRender(dynamic config)
		{
			base.OnWebRender((object)config);

			config.name = this.Name;
			config.enabled = this.Enabled;
			config.visible = this.Visible;
			config.columnBreak = this.ColumnBreak;
			config.label = TextUtils.EscapeText(this.Text, false, false, false);
			config.toolTipText = this.ToolTipText;

			if (this._imageSettings != null)
			{
				config.icon = this._imageSettings.GetSource("Image");
			}

			config.wiredEvents = new WiredEvents();
		}

		#endregion

	}
}