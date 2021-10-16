///////////////////////////////////////////////////////////////////////////////
//
// (C) 2019 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.IO;
using Wisej.Core;

namespace Wisej.Web.Ext.MobileIntegration
{
	public partial class DeviceToolbar
	{
		/// <summary>
		/// Represents a button in the <see cref="Device.Toolbar"/>.
		/// </summary>
		[ApiCategory("API")]
		public class Button : IWisejSerializable
		{

			/// <summary>
			/// Returns or sets the icon displayed in the button.
			/// </summary>
			public Image Icon
			{
				get;
				set;
			}

			/// <summary>
			/// Returns or sets the icon displayed in the button.
			/// </summary>
			public string IconSource
			{
				get;
				set;
			}

			/// <summary>
			/// Returns or sets the text of the button.
			/// </summary>
			public string Text
			{
				get;
				set;
			}

			/// <summary>
			/// Returns or sets the type of button to display in the toolbar.
			/// </summary>
			public ButtonType Type
			{
				get;
				set;
			}

			/// <summary>
			/// Returns or sets the width of the button.
			/// </summary>
			public int Width
			{
				get;
				set;
			}
			bool IWisejSerializable.Serialize(TextWriter writer, WisejSerializerOptions options)
			{
				writer.Write(JSON.Stringify(new
				{
					text = this.Text,
					width = this.Width,
					icon = GetImageUrl(),
					type = (int)this.Type,
				}));

				return true;
			}

			/// <summary>
			/// Gets a base64 encoded url representation of the image.
			/// </summary>
			/// <returns>The base64 encoded image url.</returns>
			private string GetImageUrl()
			{
				if (this.Icon != null)
					return DeviceUtils.GetImageUrl(this.Icon);
				else
					return DeviceUtils.GetImageUrl(this.IconSource);
			}
		}
	}
}
