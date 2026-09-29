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



using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.IO.Compression
{
	/// <summary>
	/// Represents a Brotli stream for compression or decompression.
	/// </summary>
	/// <remarks>
	/// This class uses the same name and constructors as the <c>System.IO.Compression.BrotliStream</c> class of .NET Core,
	/// so that code written for .NET Core also compiles on .NET Framework, which doesn't include Brotli support.
	/// The compression is performed by the native Brotli library (brolib_x86.dll or brolib_x64.dll), which is embedded
	/// in this assembly and extracted to the temporary folder the first time the class is used. The native library
	/// is a Windows DLL, so this class works only on Windows.
	/// <para>
	/// The extension also includes a Brotli decoder for the browser, which replaces the default GZip decoder used by Wisej.
	/// </para>
	/// </remarks>
	/// <example>
	/// The following example compresses a string and decompresses it again:
	/// <code><![CDATA[
	/// using System.IO;
	/// using System.IO.Compression;
	/// using System.Text;
	///
	/// var data = Encoding.UTF8.GetBytes("Hello, Brotli! Hello, Brotli! Hello, Brotli!");
	///
	/// // compress.
	/// var compressed = new MemoryStream();
	/// using (var brotli = new BrotliStream(compressed, CompressionLevel.Optimal, leaveOpen: true))
	/// {
	///     brotli.Write(data, 0, data.Length);
	/// }
	///
	/// // decompress.
	/// compressed.Position = 0;
	/// using (var brotli = new BrotliStream(compressed, CompressionMode.Decompress))
	/// using (var reader = new StreamReader(brotli, Encoding.UTF8))
	/// {
	///     Console.WriteLine(reader.ReadToEnd()); // Hello, Brotli! Hello, Brotli! Hello, Brotli!
	/// }
	/// ]]></code>
	/// </example>
	public class BrotliStream : Brotli.BrotliStream
	{
		/// <summary>
		/// Extract the native brotli libraries.
		/// </summary>
		static BrotliStream()
		{
			var fileName = "brolib_x" + (IntPtr.Size == 4 ? "86" : "64") + ".dll";
			var resName = "Wisej.Web.Ext.Brotli.Native." + fileName;
			var asm = typeof(BrotliStream).Assembly;
			var tempPath = Path.Combine(Path.GetTempPath(), fileName);

			try
			{
				using (var stream = asm.GetManifestResourceStream(resName))
				using (var writer = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
				{
					stream.CopyTo(writer);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			if (Brotli.NativeMethods.LoadLibrary(tempPath) == IntPtr.Zero)
				throw new Exception($"Failed to load {tempPath} with error code {Marshal.GetLastWin32Error()}.");
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BrotliStream"/> class using the specified stream and
		/// compression level, and optionally leaves the stream open.
		/// </summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="level">One of <see cref="CompressionLevel"/> values that indicates the level of compression to adopt.</param>
		/// <param name="leaveOpen"><c>true</c> to leave the stream open after disposing the <see cref="BrotliStream"/> object; otherwise, <c>false</c>.</param>
		/// <remarks>
		/// The stream is created in <see cref="CompressionMode.Compress"/> mode. The compression level is mapped to
		/// the Brotli quality: <see cref="CompressionLevel.NoCompression"/> uses quality 0, <see cref="CompressionLevel.Fastest"/>
		/// uses quality 1 and <see cref="CompressionLevel.Optimal"/> uses quality 9. Use <see cref="Brotli.BrotliStream.SetQuality"/>
		/// to select a different quality between 0 and 11.
		/// <para>
		/// The compressed data is completed when the <see cref="BrotliStream"/> is disposed. Set <paramref name="leaveOpen"/> to
		/// <c>true</c> to read the compressed data from <paramref name="stream"/> after disposing the <see cref="BrotliStream"/>.
		/// </para>
		/// </remarks>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="level"/> is not <see cref="CompressionLevel.NoCompression"/>,
		/// <see cref="CompressionLevel.Fastest"/> or <see cref="CompressionLevel.Optimal"/>.</exception>
		/// <exception cref="Brotli.BrotliException">The native Brotli encoder can't be created.</exception>
		/// <example>
		/// The following example compresses a file and keeps the output stream open to read its length:
		/// <code><![CDATA[
		/// using (var input = File.OpenRead("report.json"))
		/// using (var output = new MemoryStream())
		/// {
		///     using (var brotli = new BrotliStream(output, CompressionLevel.Fastest, leaveOpen: true))
		///     {
		///         input.CopyTo(brotli);
		///     }
		///
		///     Console.WriteLine($"Compressed {input.Length} bytes to {output.Length} bytes.");
		/// }
		/// ]]></code>
		/// </example>
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
		/// compression level.
		/// </summary>
		/// <param name="stream">The stream to compress.</param>
		/// <param name="level">One of <see cref="CompressionLevel"/> values that indicates the level of compression to adopt.</param>
		/// <remarks>
		/// The stream is created in <see cref="CompressionMode.Compress"/> mode and <paramref name="stream"/> is closed when
		/// the <see cref="BrotliStream"/> is disposed. See <see cref="BrotliStream(Stream, CompressionLevel, bool)"/> for how
		/// <paramref name="level"/> is mapped to the Brotli quality.
		/// </remarks>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="level"/> is not <see cref="CompressionLevel.NoCompression"/>,
		/// <see cref="CompressionLevel.Fastest"/> or <see cref="CompressionLevel.Optimal"/>.</exception>
		/// <exception cref="Brotli.BrotliException">The native Brotli encoder can't be created.</exception>
		/// <example>
		/// The following example saves a Brotli compressed copy of a file:
		/// <code><![CDATA[
		/// using (var input = File.OpenRead("styles.css"))
		/// using (var output = File.Create("styles.css.br"))
		/// using (var brotli = new BrotliStream(output, CompressionLevel.Optimal))
		/// {
		///     input.CopyTo(brotli);
		/// }
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// In <see cref="CompressionMode.Compress"/> mode, write the data to the <see cref="BrotliStream"/> and the compressed
		/// data is written to <paramref name="stream"/>; the default quality is 5. In <see cref="CompressionMode.Decompress"/> mode,
		/// read from the <see cref="BrotliStream"/> to get the decompressed data from <paramref name="stream"/>.
		/// </remarks>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is null.</exception>
		/// <exception cref="Brotli.BrotliException">The native Brotli encoder or decoder can't be created.</exception>
		/// <example>
		/// The following example decompresses the Brotli data received in a request without closing the request stream:
		/// <code><![CDATA[
		/// private string ReadBrotliBody(Stream body)
		/// {
		///     using (var brotli = new BrotliStream(body, CompressionMode.Decompress, leaveOpen: true))
		///     using (var reader = new StreamReader(brotli))
		///     {
		///         return reader.ReadToEnd();
		///     }
		/// }
		/// ]]></code>
		/// </example>
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
		/// <remarks>
		/// <paramref name="stream"/> is closed when the <see cref="BrotliStream"/> is disposed.
		/// </remarks>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is null.</exception>
		/// <exception cref="Brotli.BrotliException">The native Brotli encoder or decoder can't be created.</exception>
		/// <example>
		/// The following example restores a file compressed with Brotli:
		/// <code><![CDATA[
		/// using (var input = File.OpenRead("styles.css.br"))
		/// using (var brotli = new BrotliStream(input, CompressionMode.Decompress))
		/// using (var output = File.Create("styles.css"))
		/// {
		///     brotli.CopyTo(output);
		/// }
		/// ]]></code>
		/// </example>
		public BrotliStream(Stream stream, CompressionMode mode)
			: base(stream, mode, false)
		{
		}
	}
}
