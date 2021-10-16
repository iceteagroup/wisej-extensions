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
using System.IO;
using Wisej.Core;

namespace Wisej.Web.Ext.MobileIntegration
{
	/// <summary>
	/// Represent an option in the popup shown to the user.
	/// </summary>
	[ApiCategory("API")]
	public class DevicePopupOption : IWisejSerializable
	{
		/// <summary>
		/// Initializes a new <see cref="DevicePopupOption"/>.
		/// </summary>
		public DevicePopupOption(string name, string text)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));

			if (string.IsNullOrEmpty(text))
				throw new ArgumentNullException(nameof(text));

			this.Name = name;
			this.Text = text;
		}

		/// <summary>
		/// The name of the option.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Text to display to the user.
		/// </summary>
		public string Text
		{
			get;
			set;
		}

		/// <summary>
		/// Option type.
		/// </summary>
		public DevicePromptOptionType Type
		{
			get;
			set;
		}

		bool IWisejSerializable.Serialize(TextWriter writer, WisejSerializerOptions options)
		{
			writer.Write(JSON.Stringify(new { 
				name = this.Name,
				text = this.Text,
				type = (int)this.Type
			}));

			return true;
		}
	}

	/// <summary>
	/// Determines the type of the <see cref="DevicePopupOption"/>.
	/// </summary>
	[ApiCategory("API")]
	public enum DevicePromptOptionType
	{
		/// <summary>
		/// The device shows the option using the native Default look.
		/// </summary>
		Default,

		/// <summary>
		/// The device shows the option using the native Cancel look.
		/// </summary>
		Cancel,

		/// <summary>
		/// The device shows the option using the native Destructive look.
		/// </summary>
		Destructive
	}
}
