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

using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.Windows;
using System.ComponentModel;

namespace Wisej.Ext.ClearScript
{
	/// <summary>
	/// Represents an instance of the V8 JavaScript engine.
	/// </summary>
	/// <remarks>
	/// Unlike <see cref="WindowsScriptEngine"/> instances, V8ScriptEngine instances do not have
	/// thread affinity. The underlying script engine is not thread-safe, however, so this class
	/// uses internal locks to automatically serialize all script code execution for a given
	/// instance. Script delegates and event handlers are invoked on the calling thread without
	/// marshaling.
	/// </remarks>
	[ApiCategory("ClearScript")]
	public class V8JavaScriptEngine : Microsoft.ClearScript.V8.V8ScriptEngine
	{
		public V8JavaScriptEngine(string name, V8RuntimeConstraints constraints, V8ScriptEngineFlags flags)
			: base(name, constraints, flags)
		{
		}
	}
}
