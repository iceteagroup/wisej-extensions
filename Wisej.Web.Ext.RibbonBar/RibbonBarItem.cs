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
using Wisej.Base;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a single item in a <see cref="RibbonBarGroup"/>.
	/// </summary>
	/// <remarks>
	/// This is the abstract base class of all the ribbon items: <see cref="RibbonBarItemButton"/>, <see cref="RibbonBarItemSplitButton"/>,
	/// <see cref="RibbonBarItemButtonGroup"/>, <see cref="RibbonBarItemCheckBox"/>, <see cref="RibbonBarItemRadioButton"/>,
	/// <see cref="RibbonBarItemTextBox"/>, <see cref="RibbonBarItemComboBox"/>, <see cref="RibbonBarItemControl"/> and
	/// <see cref="RibbonBarItemSeparator"/>. Items are arranged vertically in columns inside the group, see <see cref="ColumnBreak"/>.
	/// </remarks>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	[ApiCategory("RibbonBar")]
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
		/// Returns the <see cref="RibbonBar"/> that contains this <see cref="RibbonBarItem"/>.
		/// </summary>
		/// <remarks>
		/// Returns null until the item is added to a <see cref="RibbonBarGroup"/> that belongs to a <see cref="RibbonBarPage"/>
		/// in a <see cref="RibbonBar"/>. The events of an item that is not connected to a <see cref="RibbonBar"/> are not fired.
		/// </remarks>
		[Browsable(false)]
		public RibbonBar RibbonBar
		{
			get { return this.Parent?.Parent?.Parent; }
		}

		/// <summary>
		/// Returns or sets a value indicating whether a new column starts after
		/// this <see cref="RibbonBarItem"/>.
		/// </summary>
		/// <returns>true if the next item in the <see cref="RibbonBarGroup"/> is placed in a new column; otherwise, false. The default is false.</returns>
		/// <remarks>
		/// The items in a <see cref="RibbonBarGroup"/> are stacked vertically in a column; a new column is started
		/// after an item that has <see cref="ColumnBreak"/> set to true. Large (vertical) buttons and controls and separators
		/// always occupy a column of their own.
		/// </remarks>
		/// <example>
		/// Stacking three small buttons in a column followed by a second column:
		/// <code><![CDATA[
		/// var group = new RibbonBarGroup { Text = "Font" };
		/// group.Items.Add(new RibbonBarItemButton { Text = "Bold", Orientation = Orientation.Horizontal });
		/// group.Items.Add(new RibbonBarItemButton { Text = "Italic", Orientation = Orientation.Horizontal });
		/// group.Items.Add(new RibbonBarItemButton { Text = "Underline", Orientation = Orientation.Horizontal, ColumnBreak = true });
		/// group.Items.Add(new RibbonBarItemCheckBox { Text = "Superscript" });
		/// group.Items.Add(new RibbonBarItemCheckBox { Text = "Subscript" });
		/// ]]></code>
		/// </example>
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
		/// <returns>true if the item can respond to user interaction; otherwise, false. The default is true.</returns>
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
		/// <returns>true if the item is visible; otherwise, false. The default is true.</returns>
		/// <remarks>
		/// A hidden item doesn't take any space in the <see cref="RibbonBarGroup"/> layout.
		/// </remarks>
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
		/// Returns or sets the object that contains data about the control.
		/// </summary>
		/// <returns>An object that contains user-defined data about the control. The default is null.</returns>
		[Bindable(true)]
		[DefaultValue(null)]
		[Localizable(false)]
		[SRCategory("CatData")]
		[SRDescription("ControlTagDescr")]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get;
			set;
		}

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
		/// <remarks>
		/// The name can be used to retrieve the item from the <see cref="RibbonBarGroup.Items"/> collection
		/// (case insensitive) and to identify the item in the <see cref="RibbonBar.ItemClick"/> event.
		/// At design time it's the name of the component in the designer.
		/// </remarks>
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
		/// <example>
		/// Using a theme icon or a URL as the image of an item:
		/// <code><![CDATA[
		/// this.buttonSave.ImageSource = "icon-save";
		/// this.buttonExport.ImageSource = "Images/export.png";
		/// ]]></code>
		/// </example>
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the theme name or URL for the image to display next to a RibbonBarItem.")]
		[TypeConverter("Wisej.Web.ImageSourceConverter, Wisej.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171")]
		[Editor("Wisej.Design.ImageSourceEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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
		/// <remarks>
		/// The index refers to an image in the <see cref="RibbonBar.ImageList"/> of the <see cref="RibbonBar"/> that
		/// contains this item.
		/// </remarks>
		[DefaultValue(-1)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the index value of the image assigned to the RibbonBarItem.")]
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("Wisej.Design.ImageIndexEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public int ImageIndex
		{
			get { return this._imageSettings == null ? -1 : this._imageSettings.ImageIndex; }
			set { this.ImageSettings.ImageIndex = value; }
		}

		/// <summary>
		/// Returns or sets the name of the image assigned to the <see cref="RibbonBarItem" />.
		///</summary>
		/// <returns>The name of the <see cref="T:System.Drawing.Image" /> assigned to the <see cref="RibbonBarItem" />.</returns>
		/// <remarks>
		/// The key refers to an image in the <see cref="RibbonBar.ImageList"/> of the <see cref="RibbonBar"/> that
		/// contains this item.
		/// </remarks>
		[DefaultValue("")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[Description("Returns or sets the name of the image assigned to the RibbonBarItem.")]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("Wisej.Design.ImageIndexEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171", 
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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
				base.Update();
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
		/// <remarks>
		/// Changing the properties of the item already updates the client. At design time the entire
		/// <see cref="RibbonBar"/> is updated.
		/// </remarks>
		/// <example>
		/// Forcing the item to be updated on the client:
		/// <code><![CDATA[
		/// this.buttonSave.Update();
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// The returned string contains the value returned by the base implementation followed by the <see cref="Text"/> property.
		/// </remarks>
		/// <example>
		/// Logging the item that was clicked:
		/// <code><![CDATA[
		/// private void ribbonBar1_ItemClick(object sender, RibbonBarItemEventArgs e)
		/// {
		///     System.Diagnostics.Debug.WriteLine(e.Item.ToString());
		/// }
		/// ]]></code>
		/// </example>
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