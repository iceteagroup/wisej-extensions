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
	/// <para>
	/// Unlike <see cref="WindowsScriptEngine"/> instances, V8ScriptEngine instances do not have
	/// thread affinity. The underlying script engine is not thread-safe, however, so this class
	/// uses internal locks to automatically serialize all script code execution for a given
	/// instance. Script delegates and event handlers are invoked on the calling thread without
	/// marshaling.
	/// </para>
	/// <para>
	/// Because there is no dedicated thread to release, a V8 engine doesn't need the dispatcher
	/// plumbing used by <see cref="JScriptEngine"/> and <see cref="VBScriptEngine"/>. It does hold
	/// unmanaged memory, so it should still be disposed when it's no longer in use.
	/// </para>
	/// <para>
	/// The native V8 libraries are embedded in this assembly and extracted on demand the first time
	/// an engine is created, therefore instantiating this class through
	/// <see cref="ClearScript.Create(EngineType, string, V8RuntimeConstraints, V8ScriptEngineFlags, WindowsScriptEngineFlags)"/>
	/// with <see cref="EngineType.V8"/> is the recommended way to create it.
	/// </para>
	/// </remarks>
	/// <example>
	/// This example creates a V8 engine, exposes a server object and a .NET type to the script,
	/// evaluates an expression and invokes a script function.
	/// <code><![CDATA[
	/// using (var engine = Wisej.Ext.ClearScript.ClearScript.Create(
	///     Wisej.Ext.ClearScript.EngineType.V8, "calculator"))
	/// {
	///     // expose a host object and a host type to the script code.
	///     engine.AddHostObject("customer", this.customer);
	///     engine.AddHostType("Math", typeof(System.Math));
	///
	///     engine.Execute(@"
	///         function discount(total) {
	///             return customer.IsPreferred ? Math.Round(total * 0.9) : total;
	///         }
	///     ");
	///
	///     // evaluate an expression, or invoke a script function.
	///     var net = Convert.ToDouble(engine.Evaluate("discount(1000)"));
	///     var other = engine.Script.discount(250);
	/// }
	/// ]]></code>
	/// </example>
	/// <seealso cref="ClearScript"/>
	/// <seealso cref="JScriptEngine"/>
	/// <seealso cref="VBScriptEngine"/>
	[ApiCategory("ClearScript")]
	public class V8JavaScriptEngine : Microsoft.ClearScript.V8.V8ScriptEngine
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="V8JavaScriptEngine"/> class with the
		/// specified name, resource constraints and options.
		/// </summary>
		/// <param name="name">A user defined name for the engine instance. It is used only for
		/// identification purposes, e.g. in debugger user interfaces. May be null.</param>
		/// <param name="constraints">Resource constraints for the V8 runtime that backs this engine,
		/// or null to use the V8 defaults.</param>
		/// <param name="flags">A bitwise combination of <see cref="V8ScriptEngineFlags"/> values that
		/// configure the engine. Pass <see cref="V8ScriptEngineFlags.None"/> for the defaults.</param>
		/// <remarks>
		/// Unlike the Windows script engines, this engine can be created and used on any thread. The
		/// first instantiation in the process extracts and loads the embedded native V8 libraries.
		/// </remarks>
		/// <example>
		/// Recommended: let <see cref="ClearScript"/> create the engine.
		/// <code><![CDATA[
		/// var engine = (Wisej.Ext.ClearScript.V8JavaScriptEngine)
		///     Wisej.Ext.ClearScript.ClearScript.Create(
		///         Wisej.Ext.ClearScript.EngineType.V8,
		///         "rules",
		///         v8flags: Microsoft.ClearScript.V8.V8ScriptEngineFlags.EnableDebugging);
		///
		/// engine.Execute("var version = 1;");
		/// ]]></code>
		/// Direct instantiation, limiting the memory the script code may use.
		/// <code><![CDATA[
		/// var constraints = new Microsoft.ClearScript.V8.V8RuntimeConstraints
		/// {
		///     MaxOldSpaceSize = 64,   // MB
		///     MaxNewSpaceSize = 8     // MB
		/// };
		///
		/// using (var engine = new Wisej.Ext.ClearScript.V8JavaScriptEngine(
		///     "rules", constraints, Microsoft.ClearScript.V8.V8ScriptEngineFlags.None))
		/// {
		///     var total = engine.Evaluate("1 + 1");
		/// }
		/// ]]></code>
		/// </example>
		public V8JavaScriptEngine(string name, V8RuntimeConstraints constraints, V8ScriptEngineFlags flags)
			: base(name, constraints, flags)
		{
		}
	}
}
