///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

/**
 * WisejInflate.
 *
 * Replaces the original WisejInflate implementation for GZip encoding. 
 *
 * This class uses and incorporates Google's Brotli JavaScript decoder: https://github.com/google/brotli/tree/master/js
 *
 * All methods in this class are static.
 *
 */

WisejInflate = {};

/**
 * Inflates a compressed ArrayBuffer or Blob
 *
 * @param {ArrayBuffer | Blob} compressed The compressed data.
 * @param {Function} callback Callback, invoked when the data is inflated. Receives one argument: the inflated data as a string.
 */
WisejInflate.inflate = function (compressed, callback) {

	if (callback === null)
		return;

	if (compressed === null)
		callback("");

	if (compressed instanceof ArrayBuffer) {

		WisejInflate.__inflateArrayBuffer(compressed, callback);
	}
	else if (compressed instanceof Blob) {

		WisejInflate.__inflateBlob(compressed, callback);
	}
	else {
		throw new Error("Unknown binary format.");
	}
};

WisejInflate.__inflateBlob = function (blob, callback) {

	// Blobs must be converted to UInt8Array using the FileReader.
	// the result comes back asynchronously through the onload event.
	var reader = new FileReader();
	reader.onload = function (e) {

		try {
			var compressed = new Int8Array(e.target.result);
			var plainText = new TextDecoder("utf-8").decode(BrotliDecode(compressed));
			callback(plainText);
		}
		catch (ex) {

			Wisej.onException(ex);
		}
	};
	reader.readAsArrayBuffer(blob);
};

WisejInflate.__inflateArrayBuffer = function (arrayBuffer, callback) {

	try {
		var compressed = new Int8Array(arrayBuffer);
		var plainText = new TextDecoder("utf-8").decode(BrotliDecode(compressed));
		callback(plainText);
	}
	catch (ex) {

		Wisej.onException(ex);
	}
};
