using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace Brotli
{
    /// <summary>
    /// Provides methods and properties used to compress and decompress streams using the Brotli algorithm.
    /// </summary>
    /// <remarks>
    /// This is the Brotli.NET implementation used by <see cref="T:System.IO.Compression.BrotliStream"/>.
    /// The stream is forward only: it can either be written to (<see cref="CompressionMode.Compress"/>) or read from
    /// (<see cref="CompressionMode.Decompress"/>), and it doesn't support seeking.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// var output = new MemoryStream();
    /// using (var brotli = new Brotli.BrotliStream(output, CompressionMode.Compress, leaveOpen: true))
    /// {
    ///     brotli.SetQuality(11);
    ///     brotli.SetWindow(24);
    ///
    ///     var data = Encoding.UTF8.GetBytes(largeText);
    ///     brotli.Write(data, 0, data.Length);
    /// }
    /// ]]></code>
    /// </example>
    public class BrotliStream : Stream
    {
        const int BufferSize = 64 * 1024;
        protected Stream _stream = null;
        protected MemoryStream _intermediateStream = new MemoryStream();
        protected CompressionMode _mode = CompressionMode.Compress;
        protected IntPtr _state = IntPtr.Zero;
        protected IntPtr _ptrInputBuffer = IntPtr.Zero;
        protected IntPtr _ptrOutputBuffer = IntPtr.Zero;

        protected IntPtr _ptrNextInput = IntPtr.Zero;
        protected IntPtr _ptrNextOutput = IntPtr.Zero;
        protected UInt32 _availableIn = 0;
        protected UInt32 _availableOut = BufferSize;

        protected Byte[] _managedBuffer;
        protected Boolean _endOfStream = false;
        protected int _readOffset = 0;
        protected BrotliDecoderResult _lastDecodeResult = BrotliDecoderResult.NeedsMoreInput;
        protected Boolean _leaveOpen = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrotliStream"/> class using the specified stream and
        /// compression mode, and optionally leaves the stream open.
        /// </summary>
        /// <param name="baseStream">The stream to compress to or decompress from.</param>
        /// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
        /// <param name="leaveOpen"><c>true</c> to leave <paramref name="baseStream"/> open after disposing the <see cref="BrotliStream"/>; otherwise, <c>false</c>.</param>
        /// <remarks>
        /// In <see cref="CompressionMode.Compress"/> mode the encoder uses quality 5 and a window of 22 (4 MB).
        /// Use <see cref="SetQuality"/> and <see cref="SetWindow"/> to change them before writing any data.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="baseStream"/> is null.</exception>
        /// <exception cref="BrotliException">The native Brotli encoder or decoder can't be created.</exception>
        /// <example>
        /// <code><![CDATA[
        /// using (var brotli = new Brotli.BrotliStream(networkStream, CompressionMode.Decompress, leaveOpen: true))
        /// using (var reader = new StreamReader(brotli))
        /// {
        ///     var json = reader.ReadToEnd();
        /// }
        /// ]]></code>
        /// </example>
        public BrotliStream(Stream baseStream, CompressionMode mode, bool leaveOpen)
        {
            if (baseStream == null) throw new ArgumentNullException("baseStream");
            _mode = mode;
            _stream = baseStream;
            _leaveOpen = leaveOpen;
            if (_mode == CompressionMode.Compress)
            {
                _state = Brolib.BrotliEncoderCreateInstance();
                if (_state == IntPtr.Zero)
                {
                    throw new BrotliException("Unable to create brotli encoder instance");
                }
                Brolib.BrotliEncoderSetParameter(_state, BrotliEncoderParameter.Quality, 5);
                Brolib.BrotliEncoderSetParameter(_state, BrotliEncoderParameter.LGWin, 22);
            }
            else
            {
                _state = Brolib.BrotliDecoderCreateInstance();
                if (_state == IntPtr.Zero)
                {
                    throw new BrotliException("Unable to create brotli decoder instance");
                }
            }
            _ptrInputBuffer = Marshal.AllocHGlobal(BufferSize);
            _ptrOutputBuffer = Marshal.AllocHGlobal(BufferSize);
            _ptrNextInput = _ptrInputBuffer;
            _ptrNextOutput = _ptrOutputBuffer;

            _managedBuffer = new Byte[BufferSize];
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BrotliStream"/> class using the specified stream and
        /// compression mode.
        /// </summary>
        /// <param name="baseStream">The stream to compress to or decompress from.</param>
        /// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
        /// <remarks>
        /// <paramref name="baseStream"/> is closed when the <see cref="BrotliStream"/> is disposed.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="baseStream"/> is null.</exception>
        /// <exception cref="BrotliException">The native Brotli encoder or decoder can't be created.</exception>
        /// <example>
        /// <code><![CDATA[
        /// using (var output = File.Create("data.br"))
        /// using (var brotli = new Brotli.BrotliStream(output, CompressionMode.Compress))
        /// {
        ///     brotli.Write(bytes, 0, bytes.Length);
        /// }
        /// ]]></code>
        /// </example>
        public BrotliStream(Stream baseStream, CompressionMode mode) : this(baseStream, mode, false)
        {

        }

        /// <summary>
        /// Sets the compression quality, from 0 (fastest) to 11 (smallest output).
        /// </summary>
        /// <param name="quality">The compression quality, from 0 to 11.</param>
        /// <remarks>
        /// Higher values produce smaller output but take longer to compress: 0 is the fastest and 11 gives the
        /// best compression. The default is 5. Call this method before writing any data. It applies only to streams
        /// created in <see cref="CompressionMode.Compress"/> mode.
        /// </remarks>
        /// <exception cref="ArgumentException"><paramref name="quality"/> is greater than 11.</exception>
        /// <example>
        /// The following example uses the best compression for static files that are compressed once and served many times:
        /// <code><![CDATA[
        /// using (var input = File.OpenRead("app.js"))
        /// using (var output = File.Create("app.js.br"))
        /// using (var brotli = new Brotli.BrotliStream(output, CompressionMode.Compress))
        /// {
        ///     brotli.SetQuality(11);
        ///     input.CopyTo(brotli);
        /// }
        /// ]]></code>
        /// </example>
        public void SetQuality(uint quality)
        {
            if (quality < 0 || quality > 11)
            {
                throw new ArgumentException("quality", "the range of quality is 0~11");
            }
            Brolib.BrotliEncoderSetParameter(_state, BrotliEncoderParameter.Quality, quality);
        }

        /// <summary>
        /// Sets the size of the compression window (LGWin), from 10 to 24.
        /// </summary>
        /// <param name="window">The base 2 logarithm of the window size, from 10 to 24.</param>
        /// <remarks>
        /// The value is the base 2 logarithm of the sliding window size: 10 is a 1 KB window and 24 is a 16 MB window.
        /// The default is 22 (4 MB). A larger window can improve the compression of large data, but the decoder needs
        /// more memory. Call this method before writing any data. It applies only to streams created in
        /// <see cref="CompressionMode.Compress"/> mode.
        /// </remarks>
        /// <exception cref="ArgumentException"><paramref name="window"/> is less than 10 or greater than 24.</exception>
        /// <example>
        /// <code><![CDATA[
        /// using (var brotli = new Brotli.BrotliStream(output, CompressionMode.Compress, leaveOpen: true))
        /// {
        ///     // 64 KB window, to limit the memory needed to decompress the data.
        ///     brotli.SetWindow(16);
        ///     brotli.Write(data, 0, data.Length);
        /// }
        /// ]]></code>
        /// </example>
        public void SetWindow(uint window)
        {
            if (window < 10 || window > 24)
            {
                throw new ArgumentException("window", "the range of window is 10~24");
            }
            Brolib.BrotliEncoderSetParameter(_state, BrotliEncoderParameter.LGWin, window);
        }

        /// <summary>
        /// Returns whether the stream supports reading.
        /// </summary>
        /// <value>
        /// <c>true</c> if the stream was created in <see cref="CompressionMode.Decompress"/> mode and the underlying
        /// stream can be read; otherwise, <c>false</c>.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// using (var brotli = new Brotli.BrotliStream(input, CompressionMode.Decompress))
        /// {
        ///     Console.WriteLine(brotli.CanRead);  // True
        ///     Console.WriteLine(brotli.CanWrite); // False
        /// }
        /// ]]></code>
        /// </example>
        public override bool CanRead
        {
            get
            {
                if (_stream == null)
                {
                    return false;
                }

                return (_mode == System.IO.Compression.CompressionMode.Decompress && _stream.CanRead);
            }
        }

        /// <summary>
        /// Returns whether the stream supports seeking. Always returns <c>false</c>.
        /// </summary>
        /// <value>Always <c>false</c>.</value>
        /// <example>
        /// <code><![CDATA[
        /// using (var brotli = new Brotli.BrotliStream(input, CompressionMode.Decompress))
        /// {
        ///     // Brotli streams are forward only.
        ///     Console.WriteLine(brotli.CanSeek); // False
        /// }
        /// ]]></code>
        /// </example>
        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Returns whether the stream supports writing.
        /// </summary>
        /// <value>
        /// <c>true</c> if the stream was created in <see cref="CompressionMode.Compress"/> mode and the underlying
        /// stream can be written; otherwise, <c>false</c>.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// using (var brotli = new Brotli.BrotliStream(output, CompressionMode.Compress))
        /// {
        ///     if (brotli.CanWrite)
        ///         brotli.Write(data, 0, data.Length);
        /// }
        /// ]]></code>
        /// </example>
        public override bool CanWrite
        {
            get
            {
                if (_stream == null)
                {
                    return false;
                }

                return (_mode == System.IO.Compression.CompressionMode.Compress && _stream.CanWrite);
            }
        }

        /// <summary>
        /// This property is not supported and always throws a <see cref="NotImplementedException"/>.
        /// </summary>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        /// <example>
        /// To get the size of the compressed data, compress to a <see cref="MemoryStream"/> and read its length:
        /// <code><![CDATA[
        /// var output = new MemoryStream();
        /// using (var brotli = new Brotli.BrotliStream(output, CompressionMode.Compress, leaveOpen: true))
        /// {
        ///     brotli.Write(data, 0, data.Length);
        /// }
        ///
        /// Console.WriteLine(output.Length);
        /// ]]></code>
        /// </example>
        public override long Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// This property is not supported and always throws a <see cref="NotImplementedException"/>.
        /// </summary>
        /// <exception cref="NotImplementedException">Always thrown, both when reading and when setting the value.</exception>
        /// <example>
        /// Brotli streams are forward only. To count the decompressed bytes, add up the values returned by <see cref="Read"/>:
        /// <code><![CDATA[
        /// long total = 0;
        /// var buffer = new byte[81920];
        /// int read;
        ///
        /// while ((read = brotli.Read(buffer, 0, buffer.Length)) > 0)
        ///     total += read;
        /// ]]></code>
        /// </example>
        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Writes the data compressed so far to the underlying stream.
        /// </summary>
        /// <remarks>
        /// In <see cref="CompressionMode.Compress"/> mode, the encoder output is flushed so that the data written so far
        /// can be decompressed, but the Brotli stream is not finished: dispose the <see cref="BrotliStream"/> to complete it.
        /// Flushing often reduces the compression ratio. In <see cref="CompressionMode.Decompress"/> mode this method does nothing.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">The underlying stream is null.</exception>
        /// <example>
        /// The following example sends each message as soon as it's written:
        /// <code><![CDATA[
        /// using (var brotli = new Brotli.BrotliStream(networkStream, CompressionMode.Compress, leaveOpen: true))
        /// {
        ///     foreach (var message in messages)
        ///     {
        ///         var bytes = Encoding.UTF8.GetBytes(message);
        ///         brotli.Write(bytes, 0, bytes.Length);
        ///         brotli.Flush();
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        public override void Flush()
        {
            if (_stream == null)
            {
                throw new ObjectDisposedException(null, "Underlying stream is disposed");
            }
            if (_mode == CompressionMode.Compress)
            {
                FlushBrotliStream(false);
            }
        }

        protected virtual void FlushBrotliStream(Boolean finished)
        {
            //test if the resource has been freed
            if (_state == IntPtr.Zero) return;
            if (Brolib.BrotliEncoderIsFinished(_state)) return;
            BrotliEncoderOperation op = finished ? BrotliEncoderOperation.Finish : BrotliEncoderOperation.Flush;
            UInt32 totalOut = 0;
            while (true)
            {
                var compressOK = Brolib.BrotliEncoderCompressStream(_state, op, ref _availableIn, ref _ptrNextInput, ref _availableOut, ref _ptrNextOutput, out totalOut);
                if (!compressOK) throw new BrotliException("Unable to finish encode stream");
                var extraData = _availableOut != BufferSize;
                if (extraData)
                {
                    var bytesWrote = (int)(BufferSize - _availableOut);
                    Marshal.Copy(_ptrOutputBuffer, _managedBuffer, 0, bytesWrote);
                    _stream.Write(_managedBuffer, 0, bytesWrote);
                    _availableOut = BufferSize;
                    _ptrNextOutput = _ptrOutputBuffer;
                }
                if (Brolib.BrotliEncoderIsFinished(_state)) break;
                if (!extraData) break;
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (_mode == CompressionMode.Compress)
            {
                FlushBrotliStream(true);
            }
            base.Dispose(disposing);
            if (!_leaveOpen) _stream.Dispose();
            _intermediateStream.Dispose();
            if (_ptrInputBuffer != IntPtr.Zero) Marshal.FreeHGlobal(_ptrInputBuffer);
            if (_ptrOutputBuffer != IntPtr.Zero) Marshal.FreeHGlobal(_ptrOutputBuffer);
            _managedBuffer = null;
            _ptrInputBuffer = IntPtr.Zero;
            _ptrOutputBuffer = IntPtr.Zero;
            if (_state != IntPtr.Zero)
            {
                if (_mode == CompressionMode.Compress)
                {
                    Brolib.BrotliEncoderDestroyInstance(_state);
                }
                else
                {
                    Brolib.BrotliDecoderDestroyInstance(_state);
                }
                _state = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Removes the specified number of bytes from the beginning of a <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="ms">The <see cref="MemoryStream"/> to truncate. Its internal buffer must be accessible.</param>
        /// <param name="numberOfBytesToRemove">The number of bytes to remove from the beginning of <paramref name="ms"/>.</param>
        /// <remarks>
        /// The remaining bytes are moved to the beginning of the stream and the length of the stream is reduced by
        /// <paramref name="numberOfBytesToRemove"/>. The stream uses this method internally to discard the decompressed data
        /// that has already been read.
        /// </remarks>
        /// <exception cref="UnauthorizedAccessException">The internal buffer of <paramref name="ms"/> is not accessible.</exception>
        /// <example>
        /// <code><![CDATA[
        /// var ms = new MemoryStream();
        /// ms.Write(new byte[] { 1, 2, 3, 4, 5 }, 0, 5);
        ///
        /// brotli.TruncateBeginning(ms, 2);
        ///
        /// Console.WriteLine(string.Join(",", ms.ToArray())); // 3,4,5
        /// ]]></code>
        /// </example>
        public void TruncateBeginning(MemoryStream ms, int numberOfBytesToRemove)
        {
#if NETCORE
            ArraySegment<byte> buf;
            if(ms.TryGetBuffer(out buf))
            {
                Buffer.BlockCopy(buf.Array, numberOfBytesToRemove, buf.Array, 0, (int)ms.Length - numberOfBytesToRemove);
                ms.SetLength(ms.Length - numberOfBytesToRemove);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
#else
            byte[] buf = ms.GetBuffer();
            Buffer.BlockCopy(buf, numberOfBytesToRemove, buf, 0, (int)ms.Length - numberOfBytesToRemove);
            ms.SetLength(ms.Length - numberOfBytesToRemove);
#endif
        }

        /// <summary>
        /// Reads decompressed bytes into the specified byte array.
        /// </summary>
        /// <param name="buffer">The array used to store the decompressed bytes.</param>
        /// <param name="offset">The byte offset in <paramref name="buffer"/> at which the read bytes will be placed.</param>
        /// <param name="count">The maximum number of decompressed bytes to read.</param>
        /// <returns>The number of bytes that were read into <paramref name="buffer"/>, or 0 when the end of the stream has been reached.</returns>
        /// <exception cref="BrotliException">The stream was created in <see cref="CompressionMode.Compress"/> mode, or the compressed data ends unexpectedly.</exception>
        /// <exception cref="BrotliDecodeException">The compressed data is not valid Brotli data.</exception>
        /// <example>
        /// <code><![CDATA[
        /// using (var brotli = new Brotli.BrotliStream(input, CompressionMode.Decompress))
        /// using (var output = new MemoryStream())
        /// {
        ///     var buffer = new byte[81920];
        ///     int read;
        ///
        ///     while ((read = brotli.Read(buffer, 0, buffer.Length)) > 0)
        ///         output.Write(buffer, 0, read);
        ///
        ///     var text = Encoding.UTF8.GetString(output.ToArray());
        /// }
        /// ]]></code>
        /// </example>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_mode != CompressionMode.Decompress) throw new BrotliException("Can't read on this stream");


            int bytesRead = (int)(_intermediateStream.Length - _readOffset);
            uint totalCount = 0;
            Boolean endOfStream = false;
            Boolean errorDetected = false;
            while (bytesRead < count)
            {
                while (true)
                {
                    if (_lastDecodeResult == BrotliDecoderResult.NeedsMoreInput)
                    {
                        _availableIn = (UInt32)_stream.Read(_managedBuffer, 0, (int)BufferSize);
                        _ptrNextInput = _ptrInputBuffer;
                        if (_availableIn <= 0)
                        {
                            endOfStream = true;
                            break;
                        }
                        Marshal.Copy(_managedBuffer, 0, _ptrInputBuffer, (int)_availableIn);
                    }
                    else if (_lastDecodeResult == BrotliDecoderResult.NeedsMoreOutput)
                    {
                        Marshal.Copy(_ptrOutputBuffer, _managedBuffer, 0, BufferSize);
                        _intermediateStream.Write(_managedBuffer, 0, BufferSize);
                        bytesRead += BufferSize;
                        _availableOut = BufferSize;
                        _ptrNextOutput = _ptrOutputBuffer;
                    }
                    else
                    {
                        //Error or OK
                        endOfStream = true;
                        break;
                    }
                    _lastDecodeResult = Brolib.BrotliDecoderDecompressStream(_state, ref _availableIn, ref _ptrNextInput,
                        ref _availableOut, ref _ptrNextOutput, out totalCount);
                    if (bytesRead >= count) break;
                }

                if (endOfStream && !Brolib.BrotliDecoderIsFinished(_state))
                {
                    errorDetected = true;
                }

                if (_lastDecodeResult == BrotliDecoderResult.Error || errorDetected)
                {
                    var error = Brolib.BrotliDecoderGetErrorCode(_state);
                    var text = Brolib.BrotliDecoderErrorString(error);
                    throw new BrotliDecodeException(String.Format("Unable to decode stream,possibly corrupt data.Code={0}({1})", error, text), error, text);
                }

                if (endOfStream && !Brolib.BrotliDecoderIsFinished(_state) && _lastDecodeResult == BrotliDecoderResult.NeedsMoreInput)
                {
                    throw new BrotliException("Unable to decode stream,unexpected EOF");
                }

                if (endOfStream && _ptrNextOutput != _ptrOutputBuffer)
                {
                    int remainBytes = (int)(_ptrNextOutput.ToInt64() - _ptrOutputBuffer.ToInt64());
                    bytesRead += remainBytes;
                    Marshal.Copy(_ptrOutputBuffer, _managedBuffer, 0, remainBytes);
                    _intermediateStream.Write(_managedBuffer, 0, remainBytes);
                    _ptrNextOutput = _ptrOutputBuffer;
                }
                if (endOfStream) break;
            }

            if (_intermediateStream.Length - _readOffset >= count || endOfStream)
            {
                _intermediateStream.Seek(_readOffset, SeekOrigin.Begin);
                var bytesToRead = (int)(_intermediateStream.Length - _readOffset);
                if (bytesToRead > count) bytesToRead = count;
                _intermediateStream.Read(buffer, offset, bytesToRead);
                TruncateBeginning(_intermediateStream, _readOffset + bytesToRead);
                _readOffset = 0;
                return bytesToRead;
            }

            return 0;

        }

        /// <summary>
        /// This method is not supported and always throws a <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="offset">Not used.</param>
        /// <param name="origin">Not used.</param>
        /// <returns>This method doesn't return a value.</returns>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        /// <example>
        /// Check <see cref="CanSeek"/> before seeking a stream of unknown type:
        /// <code><![CDATA[
        /// if (stream.CanSeek)
        ///     stream.Seek(0, SeekOrigin.Begin);
        /// ]]></code>
        /// </example>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is not supported and always throws a <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="value">Not used.</param>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        /// <example>
        /// Code that works with any stream should check <see cref="CanSeek"/> and <see cref="CanWrite"/> before changing its length:
        /// <code><![CDATA[
        /// if (stream.CanSeek && stream.CanWrite)
        ///     stream.SetLength(0);
        /// ]]></code>
        /// </example>
        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compresses the specified bytes and writes them to the underlying stream.
        /// </summary>
        /// <param name="buffer">The array that contains the data to compress.</param>
        /// <param name="offset">The byte offset in <paramref name="buffer"/> from which the bytes will be read.</param>
        /// <param name="count">The number of bytes to compress.</param>
        /// <remarks>
        /// The compressed output may be buffered by the encoder. Call <see cref="Flush"/> to write it to the underlying stream,
        /// and dispose the <see cref="BrotliStream"/> to complete the compressed data.
        /// </remarks>
        /// <exception cref="BrotliException">The stream was created in <see cref="CompressionMode.Decompress"/> mode, or the data can't be compressed.</exception>
        /// <example>
        /// <code><![CDATA[
        /// var bytes = Encoding.UTF8.GetBytes("Hello, Brotli!");
        ///
        /// using (var output = File.Create("hello.br"))
        /// using (var brotli = new Brotli.BrotliStream(output, CompressionMode.Compress))
        /// {
        ///     brotli.Write(bytes, 0, bytes.Length);
        /// }
        /// ]]></code>
        /// </example>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_mode != CompressionMode.Compress) throw new BrotliException("Can't write on this stream");

            UInt32 totalOut = 0;
            int bytesRemain = count;
            int currentOffset = offset;

            Boolean compressOK = true;
            while (bytesRemain > 0)
            {
                int copyLen = bytesRemain > BufferSize ? BufferSize : bytesRemain;
                Marshal.Copy(buffer, currentOffset, _ptrInputBuffer, copyLen);
                bytesRemain -= copyLen;
                currentOffset += copyLen;
                _availableIn = (UInt32)copyLen;
                _ptrNextInput = _ptrInputBuffer;
                while (_availableIn > 0)
                {
                    compressOK = Brolib.BrotliEncoderCompressStream(_state, BrotliEncoderOperation.Process, ref _availableIn, ref _ptrNextInput, ref _availableOut,
                        ref _ptrNextOutput, out totalOut);
                    if (!compressOK) throw new BrotliException("Unable to compress stream");
                    if (_availableOut != BufferSize)
                    {
                        var bytesWrote = (int)(BufferSize - _availableOut);
                        //Byte[] localBuffer = new Byte[bytesWrote];
                        Marshal.Copy(_ptrOutputBuffer, _managedBuffer, 0, bytesWrote);
                        _stream.Write(_managedBuffer, 0, bytesWrote);
                        _availableOut = BufferSize;
                        _ptrNextOutput = _ptrOutputBuffer;
                    }
                }
                if (Brolib.BrotliEncoderIsFinished(_state)) break;
            }
        }
    }
}
