//#Minify=Off
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

/**
 * wisej.ext.ClientClipboard
 */
qx.Class.define("wisej.ext.ClientClipboard", {
	type: "static",
	extend: qx.core.Object,

	statics: {

		/**
		 * Returns the data content of the system clipboard.
		 */
		read: function () {
			return navigator.clipboard.read();
		},

		/**
		 * Returns the textual content of the system clipboard.
		 */
		readText: function () {
			return navigator.clipboard.readText();
		},

		/**
		 * Returns the image content of the system clipboard.
		 */
		readImage: function () {

			return new Promise((resolve, reject) => {
				try {
					navigator.clipboard.read().then((data) => {
						for (let i = 0; i < data.length; i++) {
							if (data[i].types.includes("image/png")) {
								data[i].getType("image/png").then((blob) => {
									var reader = new FileReader();
									reader.onload = () => {
										resolve(reader.result);
									}
									reader.onerror = (error) => {
										reject(error);
									}
									reader.readAsDataURL(blob);
								});
								return;
							}
						}
						resolve(null);
					});
				} catch (ex) {
					reject(ex.error);
				}
			});
		},
			
		/**
		 * Writes the specified text string to the system clipboard.
		 * @param {String} text The text to write to the clipboard.
		 */
		writeText: function (text) {
			return navigator.clipboard.writeText(text);
		},

		/**
		 * Writes arbitrary data to the system clipboard.
		 * @param {any} data The text to write to the clipboard.
		 */
		write: function (data) {
			return navigator.clipboard.writeText(text);
		},

		/**
		 * Writes the base64 image to the system clipboard.
		 * @param {any} base64 The base64 representation of the image to write to the clipboard.
		 */
		writeImage: function (base64) {

			return new Promise((resolve, reject) => {
				fetch(base64).then((result) => {
					result.blob().then(
						(blob) => {
							var data = [new ClipboardItem({ [blob.type]: blob })];
							navigator.clipboard.write(data).then(resolve, reject);
						},
						reject);
				});
			});
		}
	}
});
