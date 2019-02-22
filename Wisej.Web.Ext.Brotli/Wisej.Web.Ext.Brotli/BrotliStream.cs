///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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



using System.Diagnostics;

namespace System.IO.Compression
{
	/// <summary>
	/// Represents a Brotli stream for compression or decompression.
	/// </summary>
	public class BrotliStream : Brotli.BrotliStream
	{
		/// <summary>
		/// Extract the native brotli libraries.
		/// </summary>
		static BrotliStream()
		{
			try
			{
				var fileName = (IntPtr.Size == 4 ? "brolib32.dll" : "brolib64.dll");
				var resName = "Wisej.Web.Ext.Brotli.Native." + fileName;

				var asm = typeof(BrotliStream).Assembly;
				var tempPath = Path.Combine(Path.GetTempPath(), fileName);
				using (var stream = asm.GetManifestResourceStream(resName))
				using (var writer = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
				{
					stream.CopyTo(writer);
				}

				// load it now that we know where it is.
				Brotli.NativeMethods.LoadLibrary(tempPath);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BrotliStream"/> class using the specified stream and
		/// compression level, and optionally leaves the stream open.
		/// </summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="level">One of <see cref="CompressionLevel"/> values that indicates the level of compression to adopt.</param>
		/// <param name="leaveOpen"><c>true</c> to leave the stream open after disposing the <see cref="BrotliStream"/> object; otherwise, <c>false</c>.</param>
		public BrotliStream(Stream stream, CompressionLevel level, bool leaveOpen)
			: base(stream, CompressionMode.Compress, leaveOpen)
		{
			switch (level)
			{
				case CompressionLevel.Fastest:
					SetQuality(1);
					break;

				case CompressionLevel.Optimal:
					SetQuality(9);
					break;

				case CompressionLevel.NoCompression:
					SetQuality(0);
					break;

				default:
					throw new ArgumentException(nameof(level));
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BrotliStream"/> class using the specified stream and
		/// compression mode, and optionally leaves the stream open.
		/// </summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="level">One of <see cref="CompressionLevel"/> values that indicates the level of compression to adopt.</param>
		public BrotliStream(Stream stream, CompressionLevel level)
			: this(stream, level, false)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BrotliStream"/> class using the specified stream and
		/// compression mode, and optionally leaves the stream open.
		/// </summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
		/// <param name="leaveOpen"><c>true</c> to leave the stream open after disposing the <see cref="BrotliStream"/> object; otherwise, <c>false</c>.</param>
		public BrotliStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: base(stream, mode, leaveOpen)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BrotliStream"/> class using the specified stream and
		/// compression mode.
		/// </summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
		public BrotliStream(Stream stream, CompressionMode mode)
			: base(stream, mode, false)
		{
		}
	}
}
