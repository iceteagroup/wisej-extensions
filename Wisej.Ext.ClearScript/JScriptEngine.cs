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

using Microsoft.ClearScript.Windows;
using System;
using System.ComponentModel;

namespace Wisej.Ext.ClearScript
{

	/// <summary>
	/// Thread safe implementation of the <see cref="Microsoft.ClearScript.Windows.JScriptEngine"/>
	/// scripting engine: every call into the script engine is automatically marshaled to the
	/// thread that created the engine.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The Microsoft Windows script engines (JScript and VBScript) have thread affinity: they capture
	/// the <see cref="System.Windows.Threading.Dispatcher"/> of the thread that creates them and must
	/// always be used from that thread. This class removes that limitation by routing all script
	/// invocations through <see cref="Microsoft.ClearScript.Windows.WindowsScriptEngine.Dispatcher"/>,
	/// so the engine can be shared safely by the threads of a Wisej session.
	/// </para>
	/// <para>
	/// In most cases you should not instantiate this class directly. Use
	/// <see cref="ClearScript.Create(EngineType, string, Microsoft.ClearScript.V8.V8RuntimeConstraints, Microsoft.ClearScript.V8.V8ScriptEngineFlags, WindowsScriptEngineFlags)"/>
	/// with <see cref="EngineType.JScript"/>: it creates the engine on a dedicated thread bound to the
	/// current Wisej session and starts the dispatcher loop that the engine needs.
	/// </para>
	/// <note type="alert">
	/// Always dispose the engine when you are done with it in order to release the dedicated thread.
	/// If you don't dispose it and don't keep a reference, the thread is terminated when the garbage
	/// collector kicks in.
	/// </note>
	/// </remarks>
	/// <example>
	/// This example creates a JScript engine, exposes a server object to the script, runs a script
	/// and reads back a value.
	/// <code><![CDATA[
	/// using (var engine = Wisej.Ext.ClearScript.ClearScript.Create(
	///     Wisej.Ext.ClearScript.EngineType.JScript, "calculator"))
	/// {
	///     // expose a host object to the script code.
	///     engine.AddHostObject("customer", this.customer);
	///
	///     // run the script.
	///     engine.Execute(@"
	///         function discount(total) {
	///             return customer.IsPreferred ? total * 0.9 : total;
	///         }
	///         var net = discount(1000);
	///     ");
	///
	///     // read a global back, or invoke a script function.
	///     var net = Convert.ToDouble(engine.Script.net);
	///     var other = engine.Script.discount(250);
	/// }
	/// ]]></code>
	/// </example>
	/// <seealso cref="ClearScript"/>
	/// <seealso cref="VBScriptEngine"/>
	/// <seealso cref="V8JavaScriptEngine"/>
	[ApiCategory("ClearScript")]
	public class JScriptEngine : Microsoft.ClearScript.Windows.JScriptEngine
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JScriptEngine"/> class with the
		/// specified name and options.
		/// </summary>
		/// <param name="name">A user defined name for the engine instance. It is used only for
		/// identification purposes, e.g. in debugger user interfaces. May be null.</param>
		/// <param name="flags">A bitwise combination of <see cref="WindowsScriptEngineFlags"/> values
		/// that configure the engine. Pass <see cref="WindowsScriptEngineFlags.None"/> for the defaults.</param>
		/// <remarks>
		/// <para>
		/// The engine binds itself to the thread that executes this constructor: that thread must run a
		/// <see cref="System.Windows.Threading.Dispatcher"/> message loop
		/// (<see cref="System.Windows.Threading.Dispatcher.Run"/>) for as long as the engine is alive,
		/// otherwise the calls marshaled to it will never be serviced.
		/// </para>
		/// <para>
		/// Prefer <see cref="ClearScript.Create(EngineType, string, Microsoft.ClearScript.V8.V8RuntimeConstraints, Microsoft.ClearScript.V8.V8ScriptEngineFlags, WindowsScriptEngineFlags)"/>,
		/// which takes care of creating the thread, starting the dispatcher loop and binding the thread
		/// to the current Wisej session.
		/// </para>
		/// </remarks>
		/// <example>
		/// Recommended: let <see cref="ClearScript"/> create the engine and its thread.
		/// <code><![CDATA[
		/// var engine = (Wisej.Ext.ClearScript.JScriptEngine)
		///     Wisej.Ext.ClearScript.ClearScript.Create(
		///         Wisej.Ext.ClearScript.EngineType.JScript,
		///         "rules",
		///         windowsflags: Microsoft.ClearScript.Windows.WindowsScriptEngineFlags.EnableDebugging);
		///
		/// engine.Execute("var version = 1;");
		/// ]]></code>
		/// Direct instantiation: you are responsible for the thread and for its dispatcher loop.
		/// <code><![CDATA[
		/// Wisej.Ext.ClearScript.JScriptEngine engine = null;
		/// var ready = new System.Threading.ManualResetEventSlim();
		///
		/// Wisej.Web.Application.StartTask(() =>
		/// {
		///     engine = new Wisej.Ext.ClearScript.JScriptEngine(
		///         "rules", Microsoft.ClearScript.Windows.WindowsScriptEngineFlags.None);
		///
		///     ready.Set();
		///
		///     // the engine's thread must pump the dispatcher queue.
		///     System.Windows.Threading.Dispatcher.Run();
		/// });
		///
		/// ready.Wait();
		///
		/// // from here the engine can be used from any thread of the session.
		/// engine.Execute("var version = 1;");
		///
		/// // releases the engine and shuts down its dedicated thread.
		/// engine.Dispose();
		/// ]]></code>
		/// </example>
		public JScriptEngine(string name, WindowsScriptEngineFlags flags)
			: base(name, flags)
		{
		}

		/// <summary>
		/// Executes the specified <paramref name="action"/> on the thread that owns the script engine.
		/// </summary>
		/// <param name="action">The script operation to invoke.</param>
		internal override void ScriptInvoke(Action action)
		{
			this.Dispatcher.Invoke(() =>
			{
				base.ScriptInvoke(action);
			});
		}

		/// <summary>
		/// Executes the specified <paramref name="func"/> on the thread that owns the script engine
		/// and returns its result.
		/// </summary>
		/// <typeparam name="T">The type of the value returned by <paramref name="func"/>.</typeparam>
		/// <param name="func">The script operation to invoke.</param>
		/// <returns>The value returned by <paramref name="func"/>.</returns>
		internal override T ScriptInvoke<T>(Func<T> func)
		{
			return this.Dispatcher.Invoke(() =>
			{
				return base.ScriptInvoke<T>(func);
			});
		}

		/// <summary>
		/// Releases the resources used by the <see cref="JScriptEngine"/> and terminates
		/// the dedicated thread that hosts the engine.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to
		/// release only unmanaged resources.</param>
		/// <remarks>
		/// Shuts down the engine's <see cref="System.Windows.Threading.Dispatcher"/> and then disposes
		/// the base engine on its own thread. After this call the engine can no longer execute script code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// // "using" is the simplest way to guarantee that the engine thread is released.
		/// using (var engine = Wisej.Ext.ClearScript.ClearScript.Create(
		///     Wisej.Ext.ClearScript.EngineType.JScript))
		/// {
		///     engine.Execute("var x = 1 + 1;");
		/// }
		/// ]]></code>
		/// </example>
		protected override void Dispose(bool disposing)
		{
			this.Dispatcher.InvokeShutdown();

			if (disposing)
			{
				this.Dispatcher.Invoke(() =>
				{
					base.Dispose(disposing);
				});
			}
		}
	}
}
