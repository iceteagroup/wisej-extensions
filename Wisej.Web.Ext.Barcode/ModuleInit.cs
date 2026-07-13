///////////////////////////////////////////////////////////////////////////////
//
// (C) 2026 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

#if !NETFRAMEWORK

using System.Runtime.CompilerServices;
using System.Text;

namespace Wisej.Web.Ext.Barcode
{
	/// <summary>
	/// Registers the legacy code-pages encodings on .NET Core.
	/// </summary>
	internal static class ModuleInit
	{
		/// <summary>
		/// The bundled ZXing library requests legacy code pages: CP437 in the PDF417
		/// encoder's type initializer, Shift_JIS/GB2312 for QR Kanji/Hanzi detection,
		/// and various code pages when decoding ECI segments. .NET Core only provides
		/// the Unicode/ASCII encodings by default, and a failed type initializer is
		/// cached for the lifetime of the process, so the provider must be registered
		/// before any encoder type is touched.
		/// </summary>
		[ModuleInitializer]
		internal static void RegisterCodePages()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		}
	}
}

#endif
