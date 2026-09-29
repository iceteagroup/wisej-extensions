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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wisej.Ext.ClientFileSystem
{
    /// <summary>
    /// Specifies the different types of actions that can be performed on a writable resource
    /// in the context of a file system operation. This enum is utilized to define the specific
    /// actions that can be taken when interacting with writable file streams.
    /// </summary>
	/// /// <remarks>
	/// For more information on writable file streams, refer to the
	/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/FileSystemWritableFileStream/write">Mozilla Developer Network documentation</see>.
	/// </remarks>
    public enum WritableType
	{
		/// <summary>
		/// Write action.
		/// </summary>
		Write,

		/// <summary>
		/// Seek action.
		/// </summary>
		Seek,

		/// <summary>
		/// Truncate action.
		/// </summary>
		Truncate
	}
}
