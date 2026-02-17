///////////////////////////////////////////////////////////////////////////////
//
// (C) 2026 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

namespace Wisej.Web.Ext.Mermaid
{
	/// <summary>
	/// Mermaid log level (maps to Mermaid's <c>logLevel</c> option).
	/// </summary>
	/// <example>
	/// <code><![CDATA[
	/// // Turn on warnings and above.
	/// mermaid.Config.LogLevel = Wisej.Web.Ext.Mermaid.MermaidLogLevel.Warn;
	/// ]]></code>
	/// </example>
	public enum MermaidLogLevel
	{
		/// <summary>
		/// Trace output.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.LogLevel = Wisej.Web.Ext.Mermaid.MermaidLogLevel.Trace;
		/// ]]></code>
		/// </example>
		Trace,

		/// <summary>
		/// Debug output.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.LogLevel = Wisej.Web.Ext.Mermaid.MermaidLogLevel.Debug;
		/// ]]></code>
		/// </example>
		Debug,

		/// <summary>
		/// Informational output.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.LogLevel = Wisej.Web.Ext.Mermaid.MermaidLogLevel.Info;
		/// ]]></code>
		/// </example>
		Info,

		/// <summary>
		/// Warning output.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.LogLevel = Wisej.Web.Ext.Mermaid.MermaidLogLevel.Warn;
		/// ]]></code>
		/// </example>
		Warn,

		/// <summary>
		/// Error output.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.LogLevel = Wisej.Web.Ext.Mermaid.MermaidLogLevel.Error;
		/// ]]></code>
		/// </example>
		Error,

		/// <summary>
		/// Fatal output.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.LogLevel = Wisej.Web.Ext.Mermaid.MermaidLogLevel.Fatal;
		/// ]]></code>
		/// </example>
		Fatal
	}
}
