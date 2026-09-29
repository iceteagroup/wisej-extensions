using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Brotli
{
	/// <summary>
	/// Provides direct access to the functions of the native Brotli library (brolib_x86.dll or brolib_x64.dll).
	/// </summary>
	/// <remarks>
	/// Each method calls the 32-bit or the 64-bit native library, depending on the process. These are low level functions
	/// that work with unmanaged memory and native encoder or decoder instances: most applications should use
	/// <see cref="T:System.IO.Compression.BrotliStream"/> instead. Every instance created with <see cref="BrotliEncoderCreateInstance"/>
	/// or <see cref="BrotliDecoderCreateInstance"/> must be released with <see cref="BrotliEncoderDestroyInstance"/> or
	/// <see cref="BrotliDecoderDestroyInstance"/>.
	/// </remarks>
	/// <example>
	/// The following example prints the version of the native encoder and decoder:
	/// <code><![CDATA[
	/// // the version is encoded as 0xMMMmmmPPP (major, minor, patch).
	/// uint version = Brolib.BrotliEncoderVersion();
	/// Console.WriteLine($"Encoder {version >> 24}.{(version >> 12) & 0xFFF}.{version & 0xFFF}");
	///
	/// version = Brolib.BrotliDecoderVersion();
	/// Console.WriteLine($"Decoder {version >> 24}.{(version >> 12) & 0xFFF}.{version & 0xFFF}");
	/// ]]></code>
	/// </example>
	public class Brolib
	{
		static bool UseX86 = IntPtr.Size == 4;

		#region Encoder
		/// <summary>
		/// Creates a new native Brotli encoder instance with the default memory allocator.
		/// </summary>
		/// <returns>A handle to the new encoder instance, or <see cref="IntPtr.Zero"/> if the instance can't be created.</returns>
		/// <remarks>
		/// Release the instance with <see cref="BrotliEncoderDestroyInstance"/> when it's no longer needed.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// IntPtr encoder = Brolib.BrotliEncoderCreateInstance();
		/// if (encoder == IntPtr.Zero)
		///     throw new BrotliException("Unable to create brotli encoder instance");
		///
		/// try
		/// {
		///     Brolib.BrotliEncoderSetParameter(encoder, BrotliEncoderParameter.Quality, 9);
		///     // ... compress with BrotliEncoderCompressStream.
		/// }
		/// finally
		/// {
		///     Brolib.BrotliEncoderDestroyInstance(encoder);
		/// }
		/// ]]></code>
		/// </example>
		public static IntPtr BrotliEncoderCreateInstance()
		{
			if (UseX86)
			{
				return Brolib32.BrotliEncoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			}
			else
			{
				return Brolib64.BrotliEncoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			}
		}


		/// <summary>
		/// Returns the base address of a module loaded in the current process.
		/// </summary>
		/// <param name="moduleName">The file name of the module, for example "brolib_x64.dll". The comparison is not case sensitive.</param>
		/// <returns>The base address of the module, or <see cref="IntPtr.Zero"/> if the module is not loaded.</returns>
		/// <example>
		/// <code><![CDATA[
		/// IntPtr handle = Brolib.GetModuleHandle(IntPtr.Size == 4 ? "brolib_x86.dll" : "brolib_x64.dll");
		/// Console.WriteLine(handle != IntPtr.Zero ? "The native Brotli library is loaded." : "Not loaded.");
		/// ]]></code>
		/// </example>
		public static IntPtr GetModuleHandle(String moduleName)
		{
			IntPtr r = IntPtr.Zero;
			foreach (ProcessModule mod in Process.GetCurrentProcess().Modules)
			{
				if (String.Compare(mod.ModuleName, moduleName, true) == 0)
				{
					r = mod.BaseAddress;
					break;
				}
			}
			return r;
		}

		/// <summary>
		/// Unloads the native Brotli library from the current process, if it's loaded.
		/// </summary>
		/// <remarks>
		/// Don't use any Brotli stream or <see cref="Brolib"/> method after calling this method, because the native functions
		/// are no longer available.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // release the native library when the application is shutting down.
		/// Brolib.FreeLibrary();
		/// ]]></code>
		/// </example>
		public static void FreeLibrary()
		{
			IntPtr libHandle = IntPtr.Zero;
			libHandle = GetModuleHandle(UseX86 ? Brolib32.LibName : Brolib64.LibName);
			if (libHandle != IntPtr.Zero)
			{
				NativeMethods.FreeLibrary(libHandle);
			}
		}

		/// <summary>
		/// Sets a compression parameter of the encoder.
		/// </summary>
		/// <param name="state">The encoder instance returned by <see cref="BrotliEncoderCreateInstance"/>.</param>
		/// <param name="parameter">The <see cref="BrotliEncoderParameter"/> to set.</param>
		/// <param name="value">The new value of the parameter.</param>
		/// <returns><c>true</c> if the parameter was set; <c>false</c> if the value is not valid or the encoder has already started compressing.</returns>
		/// <remarks>
		/// Set the parameters before compressing any data.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// Brolib.BrotliEncoderSetParameter(encoder, BrotliEncoderParameter.Quality, 11);
		/// Brolib.BrotliEncoderSetParameter(encoder, BrotliEncoderParameter.LGWin, 24);
		/// ]]></code>
		/// </example>
		public static bool BrotliEncoderSetParameter(IntPtr state, BrotliEncoderParameter parameter, UInt32 value)
		{
			if (UseX86)
			{
				return Brolib32.BrotliEncoderSetParameter(state, parameter, value);
			}
			else
			{
				return Brolib64.BrotliEncoderSetParameter(state, parameter, value);
			}
		}

		/// <summary>
		/// Sets a custom dictionary used by the encoder to improve the compression of data similar to the dictionary.
		/// </summary>
		/// <param name="state">The encoder instance returned by <see cref="BrotliEncoderCreateInstance"/>.</param>
		/// <param name="size">The size of the dictionary in bytes.</param>
		/// <param name="dict">A pointer to the dictionary data in unmanaged memory.</param>
		/// <remarks>
		/// The decoder must use the same dictionary with <see cref="BrotliDecoderSetCustomDictionary"/>. Set the dictionary before
		/// compressing any data, and keep the unmanaged memory allocated until the encoder is destroyed.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var dictionary = Encoding.UTF8.GetBytes("{\"id\":,\"name\":,\"email\":}");
		/// IntPtr ptrDictionary = Marshal.AllocHGlobal(dictionary.Length);
		/// Marshal.Copy(dictionary, 0, ptrDictionary, dictionary.Length);
		///
		/// Brolib.BrotliEncoderSetCustomDictionary(encoder, (uint)dictionary.Length, ptrDictionary);
		///
		/// // ... compress, destroy the encoder, then free the dictionary.
		/// Marshal.FreeHGlobal(ptrDictionary);
		/// ]]></code>
		/// </example>
		public static void BrotliEncoderSetCustomDictionary(IntPtr state, UInt32 size, IntPtr dict)
		{
			if (UseX86)
			{
				Brolib32.BrotliEncoderSetCustomDictionary(state, size, dict);
			}
			else
			{
				Brolib64.BrotliEncoderSetCustomDictionary(state, size, dict);
			}
		}

		/// <summary>
		/// Compresses the available input data and writes the compressed data to the output buffer.
		/// </summary>
		/// <param name="state">The encoder instance returned by <see cref="BrotliEncoderCreateInstance"/>.</param>
		/// <param name="op">The <see cref="BrotliEncoderOperation"/> to perform: process, flush, finish or emit metadata.</param>
		/// <param name="availableIn">On input, the number of bytes available at <paramref name="nextIn"/>. On output, the number of bytes not yet consumed.</param>
		/// <param name="nextIn">On input, a pointer to the next input byte. On output, a pointer to the first byte not yet consumed.</param>
		/// <param name="availableOut">On input, the free space at <paramref name="nextOut"/>. On output, the free space left.</param>
		/// <param name="nextOut">On input, a pointer to the output buffer. On output, a pointer to the next free byte in the output buffer.</param>
		/// <param name="totalOut">Returns the total number of bytes produced by the encoder so far.</param>
		/// <returns><c>true</c> if the operation succeeded; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// Call this method in a loop until all the input is consumed, copying the output buffer whenever it's full.
		/// Use <see cref="BrotliEncoderOperation.Finish"/> after the last input and call it until <see cref="BrotliEncoderIsFinished"/> returns <c>true</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// uint totalOut;
		/// bool ok = Brolib.BrotliEncoderCompressStream(encoder, BrotliEncoderOperation.Process,
		///     ref availableIn, ref ptrNextIn, ref availableOut, ref ptrNextOut, out totalOut);
		///
		/// if (!ok)
		///     throw new BrotliException("Unable to compress stream");
		/// ]]></code>
		/// </example>
		public static bool BrotliEncoderCompressStream(
			IntPtr state, BrotliEncoderOperation op, ref UInt32 availableIn,
			ref IntPtr nextIn, ref UInt32 availableOut, ref IntPtr nextOut, out UInt32 totalOut)
		{
			if (UseX86)
			{
				return Brolib32.BrotliEncoderCompressStream(state, op, ref availableIn, ref nextIn, ref availableOut, ref nextOut, out totalOut);
			}
			else
			{
				UInt64 availableInL = availableIn;
				UInt64 availableOutL = availableOut;
				UInt64 totalOutL = 0;
				var r = Brolib64.BrotliEncoderCompressStream(state, op, ref availableInL, ref nextIn, ref availableOutL, ref nextOut, out totalOutL);
				availableIn = (UInt32)availableInL;
				availableOut = (UInt32)availableOutL;
				totalOut = (UInt32)totalOutL;
				return r;
			}
		}

		/// <summary>
		/// Returns whether the encoder has finished the stream and all the output has been produced.
		/// </summary>
		/// <param name="state">The encoder instance returned by <see cref="BrotliEncoderCreateInstance"/>.</param>
		/// <returns><c>true</c> if the stream is finished; otherwise, <c>false</c>.</returns>
		/// <example>
		/// <code><![CDATA[
		/// while (!Brolib.BrotliEncoderIsFinished(encoder))
		/// {
		///     Brolib.BrotliEncoderCompressStream(encoder, BrotliEncoderOperation.Finish,
		///         ref availableIn, ref ptrNextIn, ref availableOut, ref ptrNextOut, out totalOut);
		///
		///     // ... copy the output buffer.
		/// }
		/// ]]></code>
		/// </example>
		public static bool BrotliEncoderIsFinished(IntPtr state)
		{
			if (UseX86)
			{
				return Brolib32.BrotliEncoderIsFinished(state);
			}
			else
			{
				return Brolib64.BrotliEncoderIsFinished(state);
			}
		}

		/// <summary>
		/// Releases a native encoder instance.
		/// </summary>
		/// <param name="state">The encoder instance returned by <see cref="BrotliEncoderCreateInstance"/>.</param>
		/// <remarks>
		/// Don't use <paramref name="state"/> after calling this method.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// IntPtr encoder = Brolib.BrotliEncoderCreateInstance();
		/// try
		/// {
		///     // ... compress.
		/// }
		/// finally
		/// {
		///     Brolib.BrotliEncoderDestroyInstance(encoder);
		/// }
		/// ]]></code>
		/// </example>
		public static void BrotliEncoderDestroyInstance(IntPtr state)
		{
			if (UseX86)
			{
				Brolib32.BrotliEncoderDestroyInstance(state);
			}
			else
			{
				Brolib64.BrotliEncoderDestroyInstance(state);
			}
		}

		/// <summary>
		/// Returns the version of the native Brotli encoder.
		/// </summary>
		/// <returns>The version encoded as <c>0xMMMmmmPPP</c>: the major version in the top bits, followed by 12 bits for the minor version and 12 bits for the patch number.</returns>
		/// <example>
		/// <code><![CDATA[
		/// uint version = Brolib.BrotliEncoderVersion();
		/// Console.WriteLine($"{version >> 24}.{(version >> 12) & 0xFFF}.{version & 0xFFF}");
		/// ]]></code>
		/// </example>
		public static UInt32 BrotliEncoderVersion()
		{
			if (UseX86)
			{
				return Brolib32.BrotliEncoderVersion();
			}
			else
			{
				return Brolib64.BrotliEncoderVersion();
			}
		}


		/// <summary>
		/// Returns a pointer to the decompressed data buffered by the decoder, without copying it to an output buffer.
		/// </summary>
		/// <param name="state">The decoder instance returned by <see cref="BrotliDecoderCreateInstance"/>.</param>
		/// <param name="size">On input, the maximum number of bytes to take, or 0 to take all the available bytes. On output, the number of bytes returned.</param>
		/// <returns>A pointer to the decompressed data. The data is valid only until the next call to the decoder.</returns>
		/// <example>
		/// <code><![CDATA[
		/// uint size = 0;
		/// IntPtr ptr = Brolib.BrotliDecoderTakeOutput(decoder, ref size);
		///
		/// var output = new byte[size];
		/// Marshal.Copy(ptr, output, 0, (int)size);
		/// ]]></code>
		/// </example>
		public static IntPtr BrotliDecoderTakeOutput(IntPtr state, ref UInt32 size)
		{
			if (UseX86)
			{
				return Brolib32.BrotliDecoderTakeOutput(state, ref size);
			}
			else
			{
				UInt64 longSize = size;
				var r = Brolib64.BrotliDecoderTakeOutput(state, ref longSize);
				size = (UInt32)longSize;
				return r;
			}
		}



		#endregion
		#region Decoder
		/// <summary>
		/// Creates a new native Brotli decoder instance with the default memory allocator.
		/// </summary>
		/// <returns>A handle to the new decoder instance, or <see cref="IntPtr.Zero"/> if the instance can't be created.</returns>
		/// <remarks>
		/// Release the instance with <see cref="BrotliDecoderDestroyInstance"/> when it's no longer needed.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// IntPtr decoder = Brolib.BrotliDecoderCreateInstance();
		/// if (decoder == IntPtr.Zero)
		///     throw new BrotliException("Unable to create brotli decoder instance");
		///
		/// try
		/// {
		///     // ... decompress with BrotliDecoderDecompressStream.
		/// }
		/// finally
		/// {
		///     Brolib.BrotliDecoderDestroyInstance(decoder);
		/// }
		/// ]]></code>
		/// </example>
		public static IntPtr BrotliDecoderCreateInstance()
		{
			if (UseX86)
			{
				return Brolib32.BrotliDecoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			}
			else
			{
				return Brolib64.BrotliDecoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			}
		}

		/// <summary>
		/// Sets the custom dictionary used to decompress data compressed with the same dictionary.
		/// </summary>
		/// <param name="state">The decoder instance returned by <see cref="BrotliDecoderCreateInstance"/>.</param>
		/// <param name="size">The size of the dictionary in bytes.</param>
		/// <param name="dict">A pointer to the dictionary data in unmanaged memory.</param>
		/// <remarks>
		/// Use the same dictionary that was passed to <see cref="BrotliEncoderSetCustomDictionary"/>. Set it before
		/// decompressing any data, and keep the unmanaged memory allocated until the decoder is destroyed.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// Brolib.BrotliDecoderSetCustomDictionary(decoder, (uint)dictionary.Length, ptrDictionary);
		/// ]]></code>
		/// </example>
		public static void BrotliDecoderSetCustomDictionary(IntPtr state, UInt32 size, IntPtr dict)
		{
			if (UseX86)
			{
				Brolib32.BrotliDecoderSetCustomDictionary(state, size, dict);
			}
			else
			{
				Brolib64.BrotliDecoderSetCustomDictionary(state, size, dict);
			}
		}

		/// <summary>
		/// Decompresses the available input data and writes the decompressed data to the output buffer.
		/// </summary>
		/// <param name="state">The decoder instance returned by <see cref="BrotliDecoderCreateInstance"/>.</param>
		/// <param name="availableIn">On input, the number of bytes available at <paramref name="nextIn"/>. On output, the number of bytes not yet consumed.</param>
		/// <param name="nextIn">On input, a pointer to the next compressed byte. On output, a pointer to the first byte not yet consumed.</param>
		/// <param name="availableOut">On input, the free space at <paramref name="nextOut"/>. On output, the free space left.</param>
		/// <param name="nextOut">On input, a pointer to the output buffer. On output, a pointer to the next free byte in the output buffer.</param>
		/// <param name="totalOut">Returns the total number of bytes produced by the decoder so far.</param>
		/// <returns>A <see cref="BrotliDecoderResult"/> that indicates whether the decoder needs more input, needs more output space, has finished or has failed.</returns>
		/// <example>
		/// <code><![CDATA[
		/// uint totalOut;
		/// var result = Brolib.BrotliDecoderDecompressStream(decoder,
		///     ref availableIn, ref ptrNextIn, ref availableOut, ref ptrNextOut, out totalOut);
		///
		/// switch (result)
		/// {
		///     case BrotliDecoderResult.NeedsMoreInput:  /* read more compressed data */ break;
		///     case BrotliDecoderResult.NeedsMoreOutput: /* copy and reset the output buffer */ break;
		///     case BrotliDecoderResult.Success:         /* done */ break;
		///     case BrotliDecoderResult.Error:
		///         var code = Brolib.BrotliDecoderGetErrorCode(decoder);
		///         throw new BrotliDecodeException(code, Brolib.BrotliDecoderErrorString(code));
		/// }
		/// ]]></code>
		/// </example>
		public static BrotliDecoderResult BrotliDecoderDecompressStream(
			IntPtr state, ref UInt32 availableIn,
			ref IntPtr nextIn, ref UInt32 availableOut, ref IntPtr nextOut, out UInt32 totalOut)
		{
			if (UseX86)
			{
				return Brolib32.BrotliDecoderDecompressStream(state, ref availableIn, ref nextIn, ref availableOut, ref nextOut, out totalOut);
			}
			else
			{
				UInt64 availableInL = availableIn;
				UInt64 availableOutL = availableOut;
				UInt64 totalOutL = 0;
				var r = Brolib64.BrotliDecoderDecompressStream(state, ref availableInL, ref nextIn, ref availableOutL, ref nextOut, out totalOutL);
				availableIn = (UInt32)availableInL;
				availableOut = (UInt32)availableOutL;
				totalOut = (UInt32)totalOutL;
				return r;
			}
		}

		/// <summary>
		/// Releases a native decoder instance.
		/// </summary>
		/// <param name="state">The decoder instance returned by <see cref="BrotliDecoderCreateInstance"/>.</param>
		/// <remarks>
		/// Don't use <paramref name="state"/> after calling this method.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// IntPtr decoder = Brolib.BrotliDecoderCreateInstance();
		/// try
		/// {
		///     // ... decompress.
		/// }
		/// finally
		/// {
		///     Brolib.BrotliDecoderDestroyInstance(decoder);
		/// }
		/// ]]></code>
		/// </example>
		public static void BrotliDecoderDestroyInstance(IntPtr state)
		{
			if (UseX86)
			{
				Brolib32.BrotliDecoderDestroyInstance(state);
			}
			else
			{
				Brolib64.BrotliDecoderDestroyInstance(state);
			}
		}

		/// <summary>
		/// Returns the version of the native Brotli decoder.
		/// </summary>
		/// <returns>The version encoded as <c>0xMMMmmmPPP</c>: the major version in the top bits, followed by 12 bits for the minor version and 12 bits for the patch number.</returns>
		/// <example>
		/// <code><![CDATA[
		/// uint version = Brolib.BrotliDecoderVersion();
		/// Console.WriteLine($"{version >> 24}.{(version >> 12) & 0xFFF}.{version & 0xFFF}");
		/// ]]></code>
		/// </example>
		public static UInt32 BrotliDecoderVersion()
		{
			if (UseX86)
			{
				return Brolib32.BrotliDecoderVersion();
			}
			else
			{
				return Brolib64.BrotliDecoderVersion();
			}
		}

		/// <summary>
		/// Returns whether the decoder has already received some input data.
		/// </summary>
		/// <param name="state">The decoder instance returned by <see cref="BrotliDecoderCreateInstance"/>.</param>
		/// <returns><c>true</c> if the decoder has started decompressing; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// A custom dictionary can only be set with <see cref="BrotliDecoderSetCustomDictionary"/> before the decoder is used.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (!Brolib.BrotliDecoderIsUsed(decoder))
		///     Brolib.BrotliDecoderSetCustomDictionary(decoder, (uint)dictionary.Length, ptrDictionary);
		/// ]]></code>
		/// </example>
		public static bool BrotliDecoderIsUsed(IntPtr state)
		{
			if (UseX86)
			{
				return Brolib32.BrotliDecoderIsUsed(state);
			}
			else
			{
				return Brolib64.BrotliDecoderIsUsed(state);
			}
		}
		/// <summary>
		/// Returns whether the decoder has reached the end of the compressed stream and all the output has been produced.
		/// </summary>
		/// <param name="state">The decoder instance returned by <see cref="BrotliDecoderCreateInstance"/>.</param>
		/// <returns><c>true</c> if the stream is finished; otherwise, <c>false</c>.</returns>
		/// <example>
		/// <code><![CDATA[
		/// // the input ended before the end of the Brotli stream.
		/// if (inputEnded && !Brolib.BrotliDecoderIsFinished(decoder))
		///     throw new BrotliException("Unable to decode stream,unexpected EOF");
		/// ]]></code>
		/// </example>
		public static bool BrotliDecoderIsFinished(IntPtr state)
		{
			if (UseX86)
			{
				return Brolib32.BrotliDecoderIsFinished(state);
			}
			else
			{
				return Brolib64.BrotliDecoderIsFinished(state);
			}

		}
		/// <summary>
		/// Returns the error code of the last decoder error.
		/// </summary>
		/// <param name="state">The decoder instance returned by <see cref="BrotliDecoderCreateInstance"/>.</param>
		/// <returns>The native error code. Negative values are errors; use <see cref="BrotliDecoderErrorString"/> to get the description.</returns>
		/// <example>
		/// <code><![CDATA[
		/// int code = Brolib.BrotliDecoderGetErrorCode(decoder);
		/// Console.WriteLine($"Decoding failed: {code} ({Brolib.BrotliDecoderErrorString(code)})");
		/// ]]></code>
		/// </example>
		public static Int32 BrotliDecoderGetErrorCode(IntPtr state)
		{
			if (UseX86)
			{
				return Brolib32.BrotliDecoderGetErrorCode(state);
			}
			else
			{
				return Brolib64.BrotliDecoderGetErrorCode(state);
			}
		}

		/// <summary>
		/// Returns the description of a decoder error code.
		/// </summary>
		/// <param name="code">The error code returned by <see cref="BrotliDecoderGetErrorCode"/>.</param>
		/// <returns>The name of the error, or an empty string if the code is unknown.</returns>
		/// <example>
		/// <code><![CDATA[
		/// var text = Brolib.BrotliDecoderErrorString(Brolib.BrotliDecoderGetErrorCode(decoder));
		/// ]]></code>
		/// </example>
		public static String BrotliDecoderErrorString(Int32 code)
		{
			IntPtr r = IntPtr.Zero;
			if (UseX86)
			{
				r = Brolib32.BrotliDecoderErrorString(code);
			}
			else
			{
				r = Brolib64.BrotliDecoderErrorString(code);
			}

			if (r != IntPtr.Zero)
			{
				return Marshal.PtrToStringAnsi(r);
			}
			return String.Empty;


		}

		/// <summary>
		/// Returns a pointer to the compressed data buffered by the encoder, without copying it to an output buffer.
		/// </summary>
		/// <param name="state">The encoder instance returned by <see cref="BrotliEncoderCreateInstance"/>.</param>
		/// <param name="size">On input, the maximum number of bytes to take, or 0 to take all the available bytes. On output, the number of bytes returned.</param>
		/// <returns>A pointer to the compressed data. The data is valid only until the next call to the encoder.</returns>
		/// <example>
		/// <code><![CDATA[
		/// uint size = 0;
		/// IntPtr ptr = Brolib.BrotliEncoderTakeOutput(encoder, ref size);
		///
		/// var compressed = new byte[size];
		/// Marshal.Copy(ptr, compressed, 0, (int)size);
		/// ]]></code>
		/// </example>
		public static IntPtr BrotliEncoderTakeOutput(IntPtr state, ref UInt32 size)
		{
			if (UseX86)
			{
				return Brolib32.BrotliEncoderTakeOutput(state, ref size);
			}
			else
			{
				UInt64 longSize = size;
				var r = Brolib64.BrotliEncoderTakeOutput(state, ref longSize);
				size = (UInt32)longSize;
				return r;
			}
		}


		#endregion
	}
}
