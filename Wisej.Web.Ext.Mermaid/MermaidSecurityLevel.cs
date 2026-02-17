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
	/// Mermaid security levels (maps to Mermaid's <c>securityLevel</c> option).
	/// </summary>
	/// <example>
	/// <code><![CDATA[
	/// // Recommended default for server-rendered apps: strict sanitization.
	/// mermaid.Config.SecurityLevel = Wisej.Web.Ext.Mermaid.MermaidSecurityLevel.Strict;
	/// mermaid.ApplyOptions();
	/// ]]></code>
	/// </example>
	public enum MermaidSecurityLevel
	{
		/// <summary>
		/// Strict mode (default in Mermaid): sanitizes HTML and restricts potentially unsafe features.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.SecurityLevel = Wisej.Web.Ext.Mermaid.MermaidSecurityLevel.Strict;
		/// ]]></code>
		/// </example>
		Strict,

		/// <summary>
		/// Allows a broader set of features/content than <see cref="Strict"/>.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.SecurityLevel = Wisej.Web.Ext.Mermaid.MermaidSecurityLevel.Loose;
		/// ]]></code>
		/// </example>
		Loose,

		/// <summary>
		/// Disables script execution but keeps a more permissive configuration than <see cref="Strict"/>.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.SecurityLevel = Wisej.Web.Ext.Mermaid.MermaidSecurityLevel.Antiscript;
		/// ]]></code>
		/// </example>
		Antiscript,

		/// <summary>
		/// Renders diagrams in a sandboxed iframe.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.SecurityLevel = Wisej.Web.Ext.Mermaid.MermaidSecurityLevel.Sandbox;
		/// ]]></code>
		/// </example>
		Sandbox
	}
}
