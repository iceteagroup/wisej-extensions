using System;

namespace Brotli
{
    /// <summary>
    /// The exception that is thrown when a Brotli stream can't compress or decompress data.
    /// </summary>
    /// <remarks>
    /// <see cref="BrotliDecodeException"/> is thrown instead when the compressed data is not valid.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// try
    /// {
    ///     using (var brotli = new System.IO.Compression.BrotliStream(input, CompressionMode.Decompress))
    ///     {
    ///         brotli.CopyTo(output);
    ///     }
    /// }
    /// catch (BrotliDecodeException ex)
    /// {
    ///     Console.WriteLine($"Corrupt data: {ex.Code} {ex.ErrorText}");
    /// }
    /// catch (BrotliException ex)
    /// {
    ///     Console.WriteLine($"Brotli error: {ex.Message}");
    /// }
    /// ]]></code>
    /// </example>
    public class BrotliException:Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrotliException"/> class.
        /// </summary>
        /// <example>
        /// <code><![CDATA[
        /// throw new BrotliException();
        /// ]]></code>
        /// </example>
        public BrotliException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="BrotliException"/> class with the specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <example>
        /// <code><![CDATA[
        /// if (encoder == IntPtr.Zero)
        ///     throw new BrotliException("Unable to create brotli encoder instance");
        /// ]]></code>
        /// </example>
        public BrotliException(String message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="BrotliException"/> class with the specified error message and
        /// a reference to the exception that caused it.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused the current exception, or null.</param>
        /// <example>
        /// <code><![CDATA[
        /// try
        /// {
        ///     brotli.Write(data, 0, data.Length);
        /// }
        /// catch (IOException ex)
        /// {
        ///     throw new BrotliException("Unable to write the compressed data.", ex);
        /// }
        /// ]]></code>
        /// </example>
        public BrotliException(String message, Exception innerException) : base(message, innerException) { }
    }
}
