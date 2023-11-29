namespace System.Drawing.Imaging
{
	//
	// Summary:
	//     Specifies the attributes of the pixel data contained in an System.Drawing.Image
	//     object. The System.Drawing.Image.Flags property returns a member of this enumeration.
	[Flags]
	public enum ImageFlags
	{
		//
		// Summary:
		//     There is no format information.
		None = 0x0,
		//
		// Summary:
		//     The pixel data is scalable.
		Scalable = 0x1,
		//
		// Summary:
		//     The pixel data contains alpha information.
		HasAlpha = 0x2,
		//
		// Summary:
		//     Specifies that the pixel data has alpha values other than 0 (transparent) and
		//     255 (opaque).
		HasTranslucent = 0x4,
		//
		// Summary:
		//     The pixel data is partially scalable, but there are some limitations.
		PartiallyScalable = 0x8,
		//
		// Summary:
		//     The pixel data uses an RGB color space.
		ColorSpaceRgb = 0x10,
		//
		// Summary:
		//     The pixel data uses a CMYK color space.
		ColorSpaceCmyk = 0x20,
		//
		// Summary:
		//     The pixel data is grayscale.
		ColorSpaceGray = 0x40,
		//
		// Summary:
		//     Specifies that the image is stored using a YCBCR color space.
		ColorSpaceYcbcr = 0x80,
		//
		// Summary:
		//     Specifies that the image is stored using a YCCK color space.
		ColorSpaceYcck = 0x100,
		//
		// Summary:
		//     Specifies that dots per inch information is stored in the image.
		HasRealDpi = 0x1000,
		//
		// Summary:
		//     Specifies that the pixel size is stored in the image.
		HasRealPixelSize = 0x2000,
		//
		// Summary:
		//     The pixel data is read-only.
		ReadOnly = 0x10000,
		//
		// Summary:
		//     The pixel data can be cached for faster access.
		Caching = 0x20000
	}
}
