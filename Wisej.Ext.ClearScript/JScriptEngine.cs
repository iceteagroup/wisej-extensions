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

namespace Wisej.Ext.ClearScript
{

	/// <summary>
	/// Thread safe JScriptEngine implementation of the
	/// <see cref="Microsoft.ClearScript.Windows.JScriptEngine"/> scripting engine.
	/// </summary>
	public class JScriptEngine : Microsoft.ClearScript.Windows.JScriptEngine
	{
		public JScriptEngine(string name, WindowsScriptEngineFlags flags)
			: base(name, flags)
		{
		}

		internal override void ScriptInvoke(Action action)
		{
			this.Dispatcher.Invoke(() =>
			{
				base.ScriptInvoke(action);
			});
		}

		internal override T ScriptInvoke<T>(Func<T> func)
		{
			return this.Dispatcher.Invoke(() =>
			{
				return base.ScriptInvoke<T>(func);
			});
		}

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
