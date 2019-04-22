using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Brotli
{
	public class Brolib
	{
		#region Encoder
		public static IntPtr BrotliEncoderCreateInstance()
		{
			return Brolib64.BrotliEncoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
		}


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

		public static void FreeLibrary()
		{
			IntPtr libHandle = IntPtr.Zero;
			libHandle = GetModuleHandle(Brolib64.LibName);
			if (libHandle != IntPtr.Zero)
			{
				NativeMethods.FreeLibrary(libHandle);
			}
		}

		public static bool BrotliEncoderSetParameter(IntPtr state, BrotliEncoderParameter parameter, UInt32 value)
		{
			return Brolib64.BrotliEncoderSetParameter(state, parameter, value);
		}

		public static void BrotliEncoderSetCustomDictionary(IntPtr state, UInt32 size, IntPtr dict)
		{
			Brolib64.BrotliEncoderSetCustomDictionary(state, size, dict);
		}

		public static bool BrotliEncoderCompressStream(
			IntPtr state, BrotliEncoderOperation op, ref UInt32 availableIn,
			ref IntPtr nextIn, ref UInt32 availableOut, ref IntPtr nextOut, out UInt32 totalOut)
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

		public static bool BrotliEncoderIsFinished(IntPtr state)
		{
			return Brolib64.BrotliEncoderIsFinished(state);
		}

		public static void BrotliEncoderDestroyInstance(IntPtr state)
		{
			Brolib64.BrotliEncoderDestroyInstance(state);
		}

		public static UInt32 BrotliEncoderVersion()
		{
			return Brolib64.BrotliEncoderVersion();
		}


		public static IntPtr BrotliDecoderTakeOutput(IntPtr state, ref UInt32 size)
		{
			UInt64 longSize = size;
			var r = Brolib64.BrotliDecoderTakeOutput(state, ref longSize);
			size = (UInt32)longSize;
			return r;
		}



		#endregion
		#region Decoder
		public static IntPtr BrotliDecoderCreateInstance()
		{
			return Brolib64.BrotliDecoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
		}

		public static void BrotliDecoderSetCustomDictionary(IntPtr state, UInt32 size, IntPtr dict)
		{
			Brolib64.BrotliDecoderSetCustomDictionary(state, size, dict);
		}

		public static BrotliDecoderResult BrotliDecoderDecompressStream(
			IntPtr state, ref UInt32 availableIn,
			ref IntPtr nextIn, ref UInt32 availableOut, ref IntPtr nextOut, out UInt32 totalOut)
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

		public static void BrotliDecoderDestroyInstance(IntPtr state)
		{
			Brolib64.BrotliDecoderDestroyInstance(state);
		}

		public static UInt32 BrotliDecoderVersion()
		{
			return Brolib64.BrotliDecoderVersion();
		}

		public static bool BrotliDecoderIsUsed(IntPtr state)
		{
			return Brolib64.BrotliDecoderIsUsed(state);
		}
		public static bool BrotliDecoderIsFinished(IntPtr state)
		{
			return Brolib64.BrotliDecoderIsFinished(state);
		}
		public static Int32 BrotliDecoderGetErrorCode(IntPtr state)
		{
			return Brolib64.BrotliDecoderGetErrorCode(state);
		}

		public static String BrotliDecoderErrorString(Int32 code)
		{
			IntPtr r = IntPtr.Zero;
			r = Brolib64.BrotliDecoderErrorString(code);

			if (r != IntPtr.Zero)
			{
				return Marshal.PtrToStringAnsi(r);
			}
			return String.Empty;
		}

		public static IntPtr BrotliEncoderTakeOutput(IntPtr state, ref UInt32 size)
		{
			UInt64 longSize = size;
			var r = Brolib64.BrotliEncoderTakeOutput(state, ref longSize);
			size = (UInt32)longSize;
			return r;
		}

		#endregion
	}
}
