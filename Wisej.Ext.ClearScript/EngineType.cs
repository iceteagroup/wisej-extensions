///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.ComponentModel;

namespace Wisej.Ext.ClearScript
{
	/// <summary>
	/// Indicates the type of scripting engine to create.
	/// </summary>
	[ApiCategory("ClearScript")]
	public enum EngineType
	{
		/// <summary>
		/// VB Script.
		/// </summary>
		VBScript,

		/// <summary>
		/// JavaScript.
		/// </summary>
		JScript,

		/// <summary>
		/// Google V8 JavaScript.
		/// </summary>
		V8
	}
}
