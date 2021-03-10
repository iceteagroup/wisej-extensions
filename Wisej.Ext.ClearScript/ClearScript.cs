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
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Threading;
using Wisej.Web;

namespace Wisej.Ext.ClearScript
{
	/// <summary>
	/// Creates and manages instances of the ClearScript scripting engines. The documentation on how to use the
	/// scripting engine is available here: <see href="https://microsoft.github.io/ClearScript/Reference/html/R_Project_Reference.htm"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// ClearScript Windows engines (VBScript and JScript) are thread-bound. Once the engine has been created
	/// it must always run within the thread that created it.
	/// </para>
	/// <para>
	/// When <see cref="ClearScript.Create"/> is called passing <see cref="EngineType.VBScript"/> or
	/// <see cref="EngineType.JScript"/>, it creates the new scripting engine on a new dedicated thread and
	/// marshals all calls to the thread the engine is bound to.
	/// </para>
	/// <para>
	/// When <see cref="ClearScript.Create"/> is called passing <see cref="EngineType.V8"/>
	/// if creates the new scripting engine without a dedicated thread since the VB engine can process
	/// requests from any thread.
	/// </para>
	/// <note type="alert">
	/// <para>
	/// When using <see cref="EngineType.VBScript"/> or <see cref="EngineType.JScript"/>
	/// DO NOT FORGET TO DISPOSE the instance when you are done with the scripting engine to release the
	/// dedicated thread.
	/// </para>
	/// <para>
	/// If you don't dispose and don't keep a reference, the thread will be automatically terminated when the
	/// garbage collector kicks in.
	/// </para>
	/// </note>
	/// </remarks>
	public static class ClearScript
	{
		private static readonly object syncLock = new object();

		static ClearScript()
		{
			// install our assembly resolution procedure to extract the
			// embedded v8 engine on demand.
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (args.Name == "ClearScriptV8")
				return LoadClearScriptV8(args.Name);

			return null;
		}

		private static Assembly LoadClearScriptV8(string name)
		{
			var path = ExtractEmbeddedV8();
			var fileName = name + (Environment.Is64BitProcess ? "-64.dll" : "-32.dll");
			var assemblyPath= Path.Combine(path, fileName);
			return Assembly.LoadFile(assemblyPath);
		}

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
		/// Creates a new instance of the specified <paramref name="type"/>.
		/// </summary>
		/// <param name="type">The <see cref="EncodingType"/> to create.</param>
		/// <param name="name">A name to associate with the instance. Currently this name is used only as a label in presentation contexts such as debugger user interfaces.</param>
		/// <param name="v8constraints">An optional instance of <see cref="V8RuntimeConstraints"/> used to initialize the <see cref="EngineType.V8"/> script engine.</param>
		/// <param name="v8flags">An optional combination of <see cref="V8ScriptEngineFlags"/> flags used to initialize the <see cref="EngineType.V8"/> script engine.</param>
		/// <param name="windowsflags">An optional combination of <see cref="WindowsScriptEngineFlags"/> flags used to initialize the
		///		<see cref="EngineType.JScript"/> or <see cref="EngineType.VBScript"/> engines.
		/// </param>
		/// <returns>The requested <see cref="ScriptEngine"/> implementation.</returns>
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

		// creates the Microsoft engines on their own
		// thread bound to the current Wisej session.
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
