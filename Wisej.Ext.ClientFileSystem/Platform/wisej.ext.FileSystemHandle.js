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
 * wisej.ext.FileSystemHandle
 */
qx.Class.define("wisej.ext.FileSystemHandle", {
	extend: qx.core.Object,

	construct: function (handle) {
		this.base(arguments);

		this.handle = handle;
	},

	members: {
		/** the native FileSystemHandle */
		handle: null,

		/**
		 * Queries the current permission state of the current handle.
		 * @param {String} mode Can be either "read" or "readwrite".
		 */
		queryPermission: function (mode) {
			var me = this;
			return (async function () {
				return await me.handle.queryPermission({ mode });
			})();
		},

		/**
		 * Requests read or readwrite permissions for the file handle.
		 * @param {String} mode Can be either "read" or "readwrite".
		 */
		requestPermission: function (mode) {
			var me = this;
			return (async function () {
				return await me.handle.requestPermission({ mode });
			})();
		},
	},
});

/**
 * wisej.ext.FileSystemFileHandle
 */
qx.Class.define("wisej.ext.FileSystemFileHandle", {
	extend: wisej.ext.FileSystemHandle,

	construct: function (handle, file) {
		this.base(arguments, handle);

		this.file = file;
	},

	members: {
		/** the native file object. */
		file: null,

		/**
		 * Opens a text file, reads all the text in the file into a string, and then closes the file.
		 */
		readText: function () {
			var me = this;
			return (async function () {
				return await me.file.text();
			})();
		},

		/**
		 * Reads the specified number of bytes from a file.
		 */
		readBytes: function () {
			var me = this;
			return (async function () {
				var buffer = new Int8Array(await me.file.arrayBuffer());
				return Array.from(buffer);
			})();
		},

		/**
		 * Opens a text file, writes all the text into the file, and then closes the file.
		 * @param {String} text The text to write.
		 * @param {Integer} position The cursor's position.
		 */
		writeText: function (text, position) {
			var me = this;
			return (async function () {
				var writable = await me.handle.createWritable({ keepExistingData: true });
				await writable.write({
					data: text,
					position: position,
				});
				await writable.close();
			})();
		},

		/**
		 * Writes an array of bytes starting from a position in the file.
		 * @param {String} base64 Represents the byte array encoded in base64.
		 * @param {Integer} position The cursor's position.
		 */
		writeBytes: function (base64, position, keepExistingData, type) {
			var me = this;
			return (async function () {
				var writable = await me.handle.createWritable({ keepExistingData: keepExistingData });
				await writable.write({
					type: type,
					position: position,
					data: Uint8Array.from(atob(base64), function (c) { return c.charCodeAt(0); }),
				});
				await writable.close();
			})();
		},

		/**
		 * Resizes the file associated with stream to be size bytes long.
		 * If size is larger than the current file size this pads the file with null bytes, otherwise it truncates the file.
		 *
		 * The file cursor is updated when truncate is called. If the offset is smaller than offset, it remains unchanged.
		 * If the offset is larger than size, the offset is set to size to ensure that subsequent writes do not error.
		 * No changes are written to the actual file on disk until the stream has been closed. Changes are typically written to a temporary file instead.
		 */
		truncate: function (size) {
			var me = this;
			return (async function () {
				var writable = await me.handle.createWritable({ keepExistingData: true });
				await writable.truncate(size);
				await writable.close();
			})();
		},
	},
});

/**
 * wisej.ext.FileSystemDirectoryHandle
 */
qx.Class.define("wisej.ext.FileSystemDirectoryHandle", {
	extend: wisej.ext.FileSystemHandle,

	members: {

		/**
		 * Returns the file within a FileSystemDirectoryHandle object.
		 * @param {String} name Name of the file to return.
		 * @param {Boolean?} create Optional flag to the create the file if it doesn't exist.
		 */
		getFile: function (name, create) {
			var me = this;
			return (async function () {
				var file = await me.handle.getFileHandle(name, { create: create || false });
				return {
					size: file.size ?? 0,
					type: file.type,
					name: file.name,
					lastModified: file.lastModifiedDate ?? new Date(),
					hash: new wisej.ext.FileSystemFileHandle(handle, file).$$hash,
				};
			})();
		},

		/**
		 * Returns the files within a FileSystemDirectoryHandle object.
		 * @param {String} pattern Represents the MIME type and the file extension.
		 */
		getFiles: function (pattern) {
			var me = this;
			var rx = this.__wildCardToRegExp(pattern);

			return (async function () {
				var array = [];
				var values = await me.handle.values();
				for await (handle of values) {
					if (handle.kind === "file") {
						var name = handle.name;
						if (rx.exec(name)) {
							var file = await handle.getFile();
							array.push({
								size: file.size,
								type: file.type,
								name: handle.name,
								lastModified: file.lastModifiedDate,
								hash: new wisej.ext.FileSystemFileHandle(handle, file).$$hash,
							});
						}
					}
				}
				return array;
			})();
		},

		/**
		 * Returns the directories within a FileSystemDirectoryHandle object.
		 * @param {String} pattern Represents the MIME type and the file extension.
		 */
		getDirectories: function (pattern) {
			var me = this;
			var rx = this.__wildCardToRegExp(pattern);

			return (async function () {
				var array = [];
				var values = await me.handle.values();
				for await (handle of values) {
					if (handle.kind === "directory") {
						var name = handle.name;
						if (rx.exec(name)) {
							array.push({
								name: handle.name,
								hash: new wisej.ext.FileSystemDirectoryHandle(handle).$$hash,
							});
						}
					}
				}
				return array;
			})();
		},

		/**
		 * Removes a file and/or a directory.
		 * @param {String} name Represents the file or directory"s name.
		 * @param {Boolean} recursive If name is a directory, set to true to delete recursively; otherwise false.
		 */
		remove: function (name, recursive) {
			var me = this;

			return (async function () {
				await me.handle.removeEntry({
					name,
					recursive,
				});
			})();
		},

		__wildCardToRegExp: function (pattern) {
			pattern = pattern.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
			pattern = pattern.replaceAll("\\*", ".*");
			pattern = pattern.replaceAll("\\?", ".");
			return new RegExp(pattern);
		},

		/**
		 * Requests read or readwrite permissions for the file handle.
		 * @param {String} mode Can be either "read" or "readwrite".
		 */
		requestPermission: function (mode) {
			var me = this;
			return (async function () {
				return await me.handle.requestPermission({ mode });
			})();
		},
	}
});
