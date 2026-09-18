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


using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript.Windows;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Threading;
using Wisej.Web;

namespace Wisej.Ext.ClearScript
{
    /// <summary>
    /// Provides functionality for creating and managing instances of the ClearScript scripting engines.
    /// For comprehensive documentation on the usage of the scripting engine, please refer to the following link:
    /// <see href="https://microsoft.github.io/ClearScript/Reference/html/R_Project_Reference.htm"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ClearScript Windows engines (VBScript and JScript) are tied to a specific thread.
    /// Once an engine instance has been created, it must always run within the thread that instantiated it.
    /// </para>
    /// <para>
    /// When <see cref="ClearScript.Create"/> is invoked with <see cref="EngineType.VBScript"/> or
    /// <see cref="EngineType.JScript"/>, a new scripting engine is instantiated on a dedicated thread,
    /// and all operations are marshaled to this engine-bound thread.
    /// </para>
    /// <para>
    /// Conversely, when <see cref="ClearScript.Create"/> is called with <see cref="EngineType.V8"/>,
    /// the new scripting engine runs without being assigned a dedicated thread, as the V8 engine can handle
    /// </para>
    /// <note type="alert">
    /// <para>
    /// When using the <see cref="EngineType.VBScript"/> or <see cref="EngineType.JScript"/> engines,
	/// it is essential to DISPOSE of the engine instance once it is no longer in use.
	/// This action is necessary to release the dedicated thread associated with the engine.
    /// </para>
    /// <para>
    /// If you don't dispose and don't keep a reference, the thread will be automatically terminated when the
    /// garbage collector kicks in.
    /// </para>
    /// </note>
    /// </remarks>
    [ApiCategory("ClearScript")]
	public static class ClearScript
	{
        /// <summary>
        /// Gets or sets the synchronization lock object used for thread safety.
        /// </summary>
        private static readonly object syncLock = new object();

		static ClearScript()
		{
			// install our assembly resolution procedure to extract the
			// embedded v8 engine on demand.
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		}

        /// <summary>
        /// Handles the <see cref="AppDomain.AssemblyResolve"/> event for the current application domain.
        /// This event occurs when the common language runtime (CLR) cannot find an assembly required by the application.
        /// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="args">An instance of <see cref="ResolveEventArgs"/> containing the event data.</param>
		/// <returns>The resolved <see cref="Assembly"/> or null if the assembly could not be resolved.</returns>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (args.Name == "ClearScriptV8")
				return LoadClearScriptV8(args.Name);

			return null;
		}

        /// <summary>
        /// Loads the ClearScript V8 assembly with the specified name.
        /// </summary>
		/// /// <param name="name">The name of the assembly to load.</param>
		/// <returns>The loaded <see cref="Assembly"/> object for the specified ClearScript V8 assembly.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> is null or empty.</exception>
		/// <exception cref="FileNotFoundException">Thrown when the specified assembly cannot be found.</exception>
        private static Assembly LoadClearScriptV8(string name)
		{
			var path = ExtractEmbeddedV8();
			var fileName = name + (Environment.Is64BitProcess ? "-64.dll" : "-32.dll");
			var assemblyPath= Path.Combine(path, fileName);
			return Assembly.LoadFile(assemblyPath);
		}

        /// <summary>
        /// Extracts and returns the embedded V8 script as a string.
        /// </summary>
        /// <returns>
        /// A string containing the embedded V8 script.
        /// </returns>
        /// <remarks>
        /// This method is part of the <see cref="Wisej.Ext.ClearScript.ClearScript"/> class,
        /// which provides functionality to work with the ClearScript library for running V8 scripts.
        /// </remarks>
        private static string ExtractEmbeddedV8()
		{
			var assembly = typeof(ClearScript).Assembly;
			var tempPath = Path.Combine(Path.GetTempPath(), "Wisej", "ClearScriptV8");
			Directory.CreateDirectory(tempPath);
			var names = typeof(ClearScript).Assembly.GetManifestResourceNames();
			var root = assembly.GetName().Name + ".ClearScript.V8.V8." + (Environment.Is64BitProcess ? "x64" : "x86") + ".";
			foreach (var n in names)
			{
				if (n.StartsWith(root))
				{
					var fileName = n.Substring(root.Length);
					var filePath = Path.Combine(tempPath, fileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
					{
						using (var resourceStream = assembly.GetManifestResourceStream(n))
						{
							resourceStream.CopyTo(fileStream);
						}
					}
				}
			}
			return tempPath;
		}

        /// <summary>
        /// Creates and initializes a new instance of the specified <paramref name="type"/> script engine.
        /// </summary>
        /// <param name="type">The <see cref="EngineType"/> enum value specifying the type of script engine to create.</param>
		/// <param name="name">A user-defined name for the script engine instance. This name is primarily used for identification purposes in presentation contexts, such as debugger interfaces.</param>
		/// <param name="v8constraints">An optional instance of <see cref="V8RuntimeConstraints"/> that defines constraints for initializing the <see cref="EngineType.V8"/> script engine. Pass null to use default constraints.</param>
		/// <param name="v8flags">An optional bitwise combination of <see cref="V8ScriptEngineFlags"/> flags for customizing the behavior of the <see cref="EngineType.V8"/> script engine. Pass 0 for default behavior.</param>
		/// <param name="windowsflags">An optional bitwise combination of <see cref="WindowsScriptEngineFlags"/> flags for configuring the <see cref="EngineType.JScript"/> or <see cref="EngineType.VBScript"/> engines. Pass 0 for default settings.</param>
		/// <returns>A new instance of the <see cref="ScriptEngine"/> corresponding to the specified <paramref name="type"/>.</returns>
        /// <exception cref="NotSupportedException">Thrown when <paramref name="type"/> is not one of the
        /// values defined by <see cref="EngineType"/>.</exception>
        /// <remarks>
        /// <para>
        /// <see cref="EngineType.JScript"/> and <see cref="EngineType.VBScript"/> engines are created on a
        /// dedicated thread bound to the current Wisej session and must be disposed to release that thread.
        /// <see cref="EngineType.V8"/> engines have no thread affinity, but should still be disposed since
        /// they hold unmanaged memory.
        /// </para>
        /// <para>
        /// The <paramref name="v8constraints"/> and <paramref name="v8flags"/> arguments are ignored unless
        /// <paramref name="type"/> is <see cref="EngineType.V8"/>; <paramref name="windowsflags"/> is ignored
        /// unless it is <see cref="EngineType.JScript"/> or <see cref="EngineType.VBScript"/>.
        /// </para>
        /// </remarks>
        /// <example>
        /// Running a script in a V8 engine, exposing a server object and a .NET type to the script code:
        /// <code><![CDATA[
        /// using (var engine = Wisej.Ext.ClearScript.ClearScript.Create(
        ///     Wisej.Ext.ClearScript.EngineType.V8, "calculator"))
        /// {
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
        /// The same code in a thread-bound JScript engine, with debugging enabled. The engine runs on its
        /// own thread, but can be used from any thread of the session:
        /// <code><![CDATA[
        /// using (var engine = Wisej.Ext.ClearScript.ClearScript.Create(
        ///     Wisej.Ext.ClearScript.EngineType.JScript,
        ///     "calculator",
        ///     windowsflags: WindowsScriptEngineFlags.EnableDebugging))
        /// {
        ///     engine.AddHostObject("customer", this.customer);
        ///     engine.Execute("function discount(total) { return customer.IsPreferred ? total * 0.9 : total; }");
        ///
        ///     var net = Convert.ToDouble(engine.Evaluate("discount(1000)"));
        /// }
        /// ]]></code>
        /// Limiting the memory a V8 engine may use, and keeping the engine alive for the whole session:
        /// <code><![CDATA[
        /// // Application.Session is a dynamic object: the engine lives until the session ends.
        /// Application.Session.Engine = Wisej.Ext.ClearScript.ClearScript.Create(
        ///     Wisej.Ext.ClearScript.EngineType.V8,
        ///     "rules",
        ///     v8constraints: new V8RuntimeConstraints { MaxOldSpaceSize = 64, MaxNewSpaceSize = 8 },
        ///     v8flags: V8ScriptEngineFlags.EnableDebugging);
        /// ]]></code>
        /// </example>
        public static ScriptEngine Create(
			EngineType type, 
			string name = null, 
			V8RuntimeConstraints v8constraints = null,
			V8ScriptEngineFlags v8flags = V8ScriptEngineFlags.None,
			WindowsScriptEngineFlags windowsflags = WindowsScriptEngineFlags.None)
		{
			switch (type)
			{
				case EngineType.JScript:
				case EngineType.VBScript:
					return CreateThreadBoundEngine(type, name, windowsflags);

				case EngineType.V8:
					return new V8JavaScriptEngine(name, v8constraints, v8flags);

				default:
					throw new NotSupportedException();
			}
		}

        /// <summary>
        /// Creates the Microsoft engines on their own thread bound to the current Wisej session.
        /// </summary>
        /// <param name="type">The type of the script engine to create.</param>
        /// <param name="name">The name of the script engine instance.</param>
        /// <param name="flags">Flags that control the behavior of the script engine.</param>
        /// <returns>A <see cref="ScriptEngine"/> instance that is thread-bound to the current context.</returns>
        /// <remarks>
        /// The engine is created inside a task started with <see cref="Application.StartTask(Action)"/>,
        /// so its thread remains bound to the current Wisej session. The method blocks until the engine
        /// has been constructed, then leaves that thread running <see cref="Dispatcher.Run"/> to service
        /// the calls that <see cref="JScriptEngine"/> and <see cref="VBScriptEngine"/> marshal to it.
        /// The thread terminates when the engine is disposed.
        /// </remarks>
        /// <example>
        /// How <see cref="Create(EngineType, string, V8RuntimeConstraints, V8ScriptEngineFlags, WindowsScriptEngineFlags)"/>
        /// uses this method for the two thread-bound Windows engines:
        /// <code><![CDATA[
        /// case EngineType.JScript:
        /// case EngineType.VBScript:
        ///     return CreateThreadBoundEngine(type, name, windowsflags);
        /// ]]></code>
        /// The returned engine can be used from any thread of the session and must be disposed in order
        /// to release its dedicated thread:
        /// <code><![CDATA[
        /// using (var engine = CreateThreadBoundEngine(
        ///     EngineType.JScript, "rules", WindowsScriptEngineFlags.EnableDebugging))
        /// {
        ///     engine.AddHostObject("customer", customer);
        ///     var net = engine.Evaluate("customer.IsPreferred ? 900 : 1000");
        /// }
        /// ]]></code>
        /// </example>
        private static ScriptEngine CreateThreadBoundEngine(EngineType type, string name, WindowsScriptEngineFlags flags)
		{
			WindowsScriptEngine engine = null;
			var checkPoint = new ManualResetEventSlim();
			Application.StartTask(() => {

				switch (type)
				{
					case EngineType.JScript:
						engine = new JScriptEngine(name, flags);
						break;

					case EngineType.VBScript:
						engine = new VBScriptEngine(name, flags);
						break;

					default:
						throw new InvalidOperationException();
				}

				checkPoint.Set();
				Dispatcher.Run();
			});

			checkPoint.Wait();
			checkPoint.Reset();

			return engine;
		}
	}
}
