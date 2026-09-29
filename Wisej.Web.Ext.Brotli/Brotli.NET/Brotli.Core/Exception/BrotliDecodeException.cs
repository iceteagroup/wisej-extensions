using System;

namespace Brotli
{
    /// <summary>
    /// The exception that is thrown when the data read by a Brotli stream is not valid Brotli data.
    /// </summary>
    /// <remarks>
    /// <see cref="Code"/> and <see cref="ErrorText"/> contain the error code and description returned by the native decoder.
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
    public class BrotliDecodeException : BrotliException
    {
        /// <summary>
        /// Returns or sets the error code returned by the native Brotli decoder.
        /// </summary>
        /// <value>The native error code. Brotli decoder errors are negative numbers.</value>
        /// <example>
        /// <code><![CDATA[
        /// catch (BrotliDecodeException ex)
        /// {
        ///     Trace.TraceError("Brotli decoder error {0}", ex.Code);
        /// }
        /// ]]></code>
        /// </example>
        public int Code { get; set; }
        /// <summary>
        /// Returns or sets the description of the error returned by the native Brotli decoder.
        /// </summary>
        /// <value>The name of the decoder error, for example "_ERROR_FORMAT_PADDING_1", or an empty string if it's unknown.</value>
        /// <example>
        /// <code><![CDATA[
        /// catch (BrotliDecodeException ex)
        /// {
        ///     AlertBox.Show($"The file is not valid Brotli data ({ex.ErrorText}).", MessageBoxIcon.Error);
        /// }
        /// ]]></code>
        /// </example>
        public String ErrorText { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="BrotliDecodeException"/> class with the specified decoder error.
        /// </summary>
        /// <param name="code">The error code returned by <see cref="Brolib.BrotliDecoderGetErrorCode"/>.</param>
        /// <param name="errorText">The error description returned by <see cref="Brolib.BrotliDecoderErrorString"/>.</param>
        /// <example>
        /// <code><![CDATA[
        /// var code = Brolib.BrotliDecoderGetErrorCode(decoder);
        /// throw new BrotliDecodeException(code, Brolib.BrotliDecoderErrorString(code));
        /// ]]></code>
        /// </example>
        public BrotliDecodeException(int code, String errorText) : base() {
            Code = code;
            ErrorText = errorText;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrotliDecodeException"/> class with the specified error message and decoder error.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="code">The error code returned by <see cref="Brolib.BrotliDecoderGetErrorCode"/>.</param>
        /// <param name="errorText">The error description returned by <see cref="Brolib.BrotliDecoderErrorString"/>.</param>
        /// <example>
        /// <code><![CDATA[
        /// var code = Brolib.BrotliDecoderGetErrorCode(decoder);
        /// var text = Brolib.BrotliDecoderErrorString(code);
        ///
        /// throw new BrotliDecodeException($"Unable to decode stream, possibly corrupt data. Code={code}({text})", code, text);
        /// ]]></code>
        /// </example>
        public BrotliDecodeException(String message, int code, String errorText) : base(message) {
            Code = code;
            ErrorText = errorText;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BrotliDecodeException"/> class with the specified error message,
        /// the exception that caused it and the decoder error.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused the current exception, or null.</param>
        /// <param name="code">The error code returned by <see cref="Brolib.BrotliDecoderGetErrorCode"/>.</param>
        /// <param name="errorText">The error description returned by <see cref="Brolib.BrotliDecoderErrorString"/>.</param>
        /// <example>
        /// <code><![CDATA[
        /// catch (BrotliDecodeException ex)
        /// {
        ///     throw new BrotliDecodeException($"Unable to read {fileName}.", ex, ex.Code, ex.ErrorText);
        /// }
        /// ]]></code>
        /// </example>
        public BrotliDecodeException(String message, Exception innerException, int code, String errorText) : base(message, innerException) {
            Code = code;
            ErrorText = errorText;
        }
    }
}
