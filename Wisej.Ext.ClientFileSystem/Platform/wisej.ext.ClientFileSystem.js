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
 * wisej.ext.ClientFileSystem
 */
qx.Class.define('wisej.ext.ClientFileSystem', {
  type: 'static',
  extend: qx.core.Object,

  statics: {
    /**
     * Displays a directory picker which allows the user to select a directory.
     */
    showDirectoryPicker: function () {
      return (async function () {
        var handle = await window.showDirectoryPicker();
        return {
          name: handle.name,
          hash: new wisej.ext.FileSystemDirectoryHandle(handle).$$hash,
        };
      })();
    },

    /**
     * Shows a file picker that allows a user to select a file or multiple files and returns a handle for the file(s).
     * @param {Boolean} multiple A Boolean. Default false. When set to true multiple files may be selected.
     * @param {String} excludeAcceptAllOption True if there's a pattern to apply; otherwise false
     * @param {String} filter Represents the MIME type and the file extension
     */
    showOpenFilePicker: function (multiple, excludeAcceptAllOption, filter) {
      var types = [];
      var parts = filter.split('|');
      if (parts.length > 2) {
        for (var i = 0; i < parts.length; i += 3) {
          var type = {
            accept: {},
            description: parts[i],
          };
          type.accept[parts[i + 1]] = parts[i + 2].split(';');
          types.push(type);
        }
      }

      return (async function () {
        var options = {
          types,
          multiple,
          excludeAcceptAllOption,
        };

        var array = [];
        var handles = await window.showOpenFilePicker(options);
        for (var i = 0; i < handles.length; i++) {
          var handle = handles[i];
          var file = await handle.getFile();

          array.push({
            size: file.size,
            type: file.type,
            name: handle.name,
            lastModified: file.lastModifiedDate,
            hash: new wisej.ext.FileSystemFileHandle(handle, file).$$hash,
          });
        }
        return array;
      })();
    },

    /**
     * Shows a file picker that allows a user to save a file. Either by selecting an existing file, or entering a name for a new file
     * @param {Boolean} excludeAcceptAllOption True if there's a pattern to apply; otherwise false
     * @param {String} filter Represents the MIME type and the file extension
     * @param {String} suggestedName A name to associate with the file
     */
    showSaveFilePicker: function (excludeAcceptAllOption, filter, suggestedName) {
      var types = [];
      var parts = filter.split('|');
      if (parts.length > 2) {
        for (var i = 0; i < parts.length; i += 3) {
          var type = {
            accept: {},
            description: parts[i],
          };
          type.accept[parts[i + 1]] = parts[i + 2].split(';');
          types.push(type);
        }
      }

      return (async function () {
        var options = {
          types,
          excludeAcceptAllOption,
          suggestedName,
        };

        var handle = await window.showSaveFilePicker(options);
        var file = await handle.getFile();

        return {
          size: file.size,
          type: file.type,
          name: handle.name,
          lastModified: file.lastModifiedDate,
          hash: new wisej.ext.FileSystemFileHandle(handle, file).$$hash,
        };
      })();
    },

    /**
     * Invoke a function on a FileSystemHandle object.
     * @param {any} hash Represents the FileSystemHandle object hash.
     * @param {any} name Represents the function name.
     * @param {any} args The arguments to pass to the function.
     */
    invoke: function (hash, name, args) {
      var target = qx.core.ObjectRegistry.fromHashCode(hash);
      if (!target) throw new Error('Invalid file system handle: ' + hash);

      var func = target[name];
      if (!(func instanceof Function)) throw new Error("Member '" + name + "' is not a function.");

      return func.apply(target, args);
    },

    /**
     * Dispose of a FileSystemHandle object
     * @param {any} hash Represents the FileSystemHandle object hash.
     */
    dispose: function (hash) {
      var obj = qx.core.ObjectRegistry.fromHashCode(hash);
      if (obj) qx.core.ObjectRegistry.unregister(obj);
    },
  },
});
