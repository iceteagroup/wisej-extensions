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

using System.ComponentModel;
using System.Drawing.Design;
using Wisej.Core;
using Wisej.Base;
using System;

namespace Wisej.Web.Ext.RibbonBar
{
	/// <summary>
	/// Represents a combobox in a <see cref="RibbonBarGroup"/>.
	/// </summary>
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	[ApiCategory("RibbonBar")]
	public class RibbonBarItemComboBox : RibbonBarItem, IWisejComponent
	{
		#region Events

		/// <summary>
		/// Fired when the value of the <see cref="Value" /> property changes.
		/// </summary>
		[SRCategory("CatAction")]
		[Description("Fired when the Value property changes.")]
		public event EventHandler ValueChanged
		{
			add { base.AddHandler(nameof(ValueChanged), value); }
			remove { base.RemoveHandler(nameof(ValueChanged), value); }
		}

		/// <summary>
		/// Fires the <see cref="ValueChanged" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		protected virtual void OnValueChanged(EventArgs e)
		{
			((EventHandler)base.Events[nameof(ValueChanged)])?.Invoke(this, e);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns or sets the currently selected item or the
		/// value in the editable field of the <see cref="RibbonBarItemComboBox"/>.
		/// </summary>
		[DefaultValue(null)]
		[Description("")]
		public string Value
		{
			get { return this._value; }
			set
			{
				if (this._value != value)
				{
					this._value = value;
					OnValueChanged(EventArgs.Empty);
					Update();
				}
			}
		}
		private string _value = null;

		/// <summary>
		/// Returns or sets whether the user can edit the combobox. When this property
		/// is false, the user can only select one of the drop down items. The default  is false.
		/// </summary>
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[Description("Returns or sets whether the user can edit the combobox. The default is false.")]
		public bool Editable
		{
			get { return this._editable; }
			set
			{
				if (this._editable != value)
				{
					this._editable = value;
					Update();
				}
			}
		}
		private bool _editable = false;

		/// <summary>
		/// Returns or sets the width of the ComboBox field inside the <see cref="RibbonBarItemComboBox"/>.
		/// </summary>
		[DefaultValue(120)]
		[SRCategory("CatLayout")]
		[Description("Returns or sets the width of the TextBox field inside the RibbonBarItemComboBox.")]
		public int FieldWidth
		{
			get { return this._fieldWidth; }
			set
			{
				if (this._fieldWidth != value)
				{
					this._fieldWidth = value;
					Update();
				}
			}
		}
		private int _fieldWidth = 120;

		/// <summary>
		/// Returns the array of items displayed in this <see cref="RibbonBarItemComboBox" />.
		/// </summary>
		/// <returns>A an array containing the items in the <see cref="RibbonBarItemComboBox" />.</returns>
		[Localizable(true)]
		[MergableProperty(false)]
		[SRCategory("CatData")]
		[Description("Returns the collection of items contained in this RibbonBarItemComboBox.")]
		[TypeConverter(typeof(ArrayConverter))]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public virtual string[] Items
		{
			get
			{
				return this._items;
			}
			set
			{
				this._items = value ?? new string[0];
				Update();
			}
		}
		private string[] _items;

		private bool ShouldSerializeItems()
		{
			return this._items != null && this._items.Length > 0;
		}
		private void ResetItem()
		{
			this.Items = null;
		}

		#endregion

		#region Methods

		#endregion

		#region Wisej Implementation

		// Handles "changeValue" events from the client.
		private void ProcessChangeValueWebEvent(WisejEventArgs e)
		{
			this.Value = e.Parameters.Value ?? string.Empty;

			this.RibbonBar?.OnItemValueChanged(new RibbonBarItemEventArgs(this));
		}

		/// <summary>
		/// Processes the event from the client.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnWebEvent(WisejEventArgs e)
		{
			switch (e.Type)
			{
				case "changeValue":
					ProcessChangeValueWebEvent(e);
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

			config.className = "wisej.web.ribbonBar.ItemComboBox";
			config.fieldWidth = this.FieldWidth;
			config.editable = this.Editable;
			config.items = this.Items;
			config.value = this.Value;

			config.wiredEvents.Add("changeValue(Value)");
		}

		/// <summary>
		/// Stores the last configuration for the component.
		/// 
		/// Overridden to remove the items list. There is no need to
		/// store and diff the list of items.
		/// </summary>
		dynamic IWisejComponent.Configuration
		{
			get { return this._configuration; }

			set
			{
				if (value != null)
				{
					DynamicObject config = value;
					value = config.Clone().Delete("items");
				}

				this._configuration = value;
			}
		}
		private dynamic _configuration;

		#endregion

	}
}