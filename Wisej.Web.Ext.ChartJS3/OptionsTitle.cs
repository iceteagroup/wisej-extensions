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

namespace Wisej.Web.Ext.ChartJS3
{
	/// <summary>
	/// Represents the options for the chart title.
	/// </summary>
	/// <remarks>
	/// Use the <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsPlugins.Title"/> property of <see cref="P:Wisej.Web.Ext.ChartJS3.Options.Plugins"/> to access the title options
	/// of a <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control. The options are serialized as the Chart.js 3 <c>plugins.title</c> option.
	/// When not set, the text, font and color of the title are taken from the <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// OptionsTitle title = this.chartJS31.Options.Plugins.Title;
	/// title.Display = true;
	/// title.Text = "Sales 2024";
	/// title.Color = Color.DarkBlue;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS3")]
	public class OptionsTitle : OptionsBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// Creates a standalone set of title options that can be assigned to <see cref="P:Wisej.Web.Ext.ChartJS3.OptionsPlugins.Title"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = new OptionsTitle();
		/// title.Text = "Inventory";
		/// title.Position = HeaderPosition.Bottom;
		///
		/// this.chartJS31.Options.Plugins.Title = title;
		/// this.chartJS31.Options.Update();
		/// ]]></code>
		/// </example>
		public OptionsTitle()
		{
		}

		/// <summary>
		/// Constructs a new instance of the <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsTitle"/> set.
		/// </summary>
		/// <param name="owner">The <see cref="T:Wisej.Web.Ext.ChartJS3.OptionsBase"/> set of options that owns this set of options.</param>
		/// <remarks>
		/// The <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control that displays the title is resolved through <paramref name="owner"/>.
		/// The instance can be assigned only to the option set specified in <paramref name="owner"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var plugins = this.chartJS31.Options.Plugins;
		/// var title = new OptionsTitle(plugins);
		/// title.Text = "Expenses";
		///
		/// plugins.Title = title;
		/// plugins.Update();
		/// ]]></code>
		/// </example>
		public OptionsTitle(OptionsBase owner)
		{
			this.Owner = owner;
		}

		/// <summary>
		/// Returns or sets the position of the title.
		/// </summary>
		/// <value>One of the <see cref="T:Wisej.Web.HeaderPosition"/> values. The default is <see cref="F:Wisej.Web.HeaderPosition.Top"/>.</value>
		/// <remarks>
		/// Serialized as the Chart.js 3 <c>plugins.title.position</c> option. Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // display the title below the chart.
		/// this.chartJS31.Options.Plugins.Title.Position = HeaderPosition.Bottom;
		/// ]]></code>
		/// </example>
		[DefaultValue(HeaderPosition.Top)]
		[Description("Position of the title.")]
		public HeaderPosition Position
		{
			get { return this._position; }
			set
			{
				if (this._position != value)
				{
					this._position = value;
					Update();
				}
			}
		}
		private HeaderPosition _position = HeaderPosition.Top;

		/// <summary>
		/// Returns or sets the number of pixels to add above and below the title text.
		/// </summary>
		/// <value>The padding in pixels. The default is 10.</value>
		/// <remarks>
		/// Serialized as the Chart.js 3 <c>plugins.title.padding</c> option. Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = this.chartJS31.Options.Plugins.Title;
		/// title.Text = "Weekly Downloads";
		/// title.Padding = 20;
		/// ]]></code>
		/// </example>
		[DefaultValue(10)]
		[Description("Number of pixels to add above and below the title text.")]
		public int Padding
		{
			get { return this._padding; }
			set
			{
				if (this._padding != value)
				{
					this._padding = value;
					Update();
				}
			}
		}
		private int _padding = 10;

		/// <summary>
		/// Returns or sets the font of the title.
		/// </summary>
		/// <value>The <see cref="T:System.Drawing.Font"/> used to render the title. The default is null.</value>
		/// <remarks>
		/// When the value is null, the property returns the <see cref="P:Wisej.Web.Control.Font"/> of the owner <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
		/// The font is converted on the client into the Chart.js 3 <c>font</c> object with the <c>size</c>, <c>family</c> and <c>style</c> fields.
		/// Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Options.Plugins.Title.Font = new Font("Segoe UI", 16, FontStyle.Bold);
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Font of the title.")]
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
		/// Returns or sets whether the title block is shown.
		/// </summary>
		/// <value>true to show the title; otherwise, false. The default is true.</value>
		/// <remarks>
		/// Serialized as the Chart.js 3 <c>plugins.title.display</c> option. Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // hide the chart title.
		/// this.chartJS31.Options.Plugins.Title.Display = false;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		[Description("Show the title block.")]
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
		private bool _display = true;

		/// <summary>
		/// Returns or sets the title text.
		/// </summary>
		/// <value>The text displayed in the title.</value>
		/// <remarks>
		/// When the text is null or empty, the property returns the <see cref="P:Wisej.Web.Control.Text"/> of the owner <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control.
		/// Assigning null sets the text to an empty string. Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// this.chartJS31.Text = "Default Title";
		///
		/// // overrides the text of the control.
		/// this.chartJS31.Options.Plugins.Title.Text = "Revenue by Quarter";
		/// ]]></code>
		/// </example>
		[Description("Title text.")]
		public string Text
		{
			get
			{
				if (string.IsNullOrEmpty(this._text))
				{
					var chart = this.Chart;
					if (chart != null)
						return chart.Text;
				}

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
		private string _text;

		private bool ShouldSerializeText()
		{
			return this._text != null && this._display && this._text.Length > 0;
		}

		private void ResetText()
		{
			this.Text = string.Empty;
		}

		/// <summary>
		/// Returns or sets the title color.
		/// </summary>
		/// <value>The <see cref="T:System.Drawing.Color"/> of the title text. The default is <see cref="F:System.Drawing.Color.Empty"/>.</value>
		/// <remarks>
		/// When the value is <see cref="F:System.Drawing.Color.Empty"/>, the property returns the <see cref="P:Wisej.Web.Control.ForeColor"/>
		/// of the owner <see cref="T:Wisej.Web.Ext.ChartJS3.ChartJS3"/> control. Serialized as the Chart.js 3 <c>plugins.title.color</c> option; themed colors are resolved on the client.
		/// Changing the value calls <see cref="M:Wisej.Web.Ext.ChartJS3.OptionsBase.Update"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var title = this.chartJS31.Options.Plugins.Title;
		/// title.Text = "Alerts";
		/// title.Color = Color.Red;
		/// ]]></code>
		/// </example>
		[DefaultValue(typeof(Color), "")]
		[Description("Title color.")]
		public Color Color
		{
			get
			{
				var chart = this.Chart;
				if (this._fontColor.IsEmpty && chart != null)
					return chart.ForeColor;

				return this._fontColor;
			}
			set
			{
				if (this._fontColor != value)
				{
					this._fontColor = value;
					Update();
				}
			}
		}
		private Color _fontColor;
	}
}
