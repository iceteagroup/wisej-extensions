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

using System.Collections.Generic;
using System.ComponentModel;
using Wisej.Core;

namespace Wisej.Web.Ext.Mermaid
{
	/// <summary>
	/// Mermaid initialization options passed to <c>mermaid.initialize(...)</c>.
	/// </summary>
	/// <remarks>
	/// Set properties on this object to control Mermaid initialization.
	/// </remarks>
	[WisejSerializerOptions(WisejSerializerOptions.CamelCase)]
	public class MermaidConfiguration
	{
		private Mermaid _owner;

		internal MermaidConfiguration(Mermaid owner)
		{
			_owner = owner;
		}

		/// <summary>
		/// Diagram look (e.g. <c>classic</c>, <c>neo</c>, <c>handDrawn</c>).
		/// </summary>
		/// <remarks>
		/// This maps to Mermaid's <c>look</c> initialization option.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.Look = "handDrawn";
		/// ]]></code>
		/// </example>
		[DefaultValue("classic")]
		public string Look
		{
			get => _look;
			set
			{
				if (_look != value)
				{
					_look = value;
					_owner.Update();
				}
			}
		}
		string _look = "classic";

		/// <summary>
		/// Theme name (e.g. <c>default</c>, <c>dark</c>, <c>forest</c>, <c>neutral</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.Theme = "neutral";
		/// ]]></code>
		/// </example>
		[DefaultValue("default")]
		public string Theme
		{
			get => _theme;
			set
			{
				if (_theme != value)
				{
					_theme = value;
					_owner.Update();
				}
			}
		}
		string _theme = "default";

		/// <summary>
		/// Theme variables (maps to Mermaid's <c>themeVariables</c>).
		/// </summary>
		/// <remarks>
		/// Common keys include <c>fontFamily</c> and <c>fontSize</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.ThemeVariables = new Dictionary<string, object>
		/// {
		///     ["fontFamily"] = "\"Segoe UI\", sans-serif",
		///     ["fontSize"] = "14px",
		///     ["lineColor"] = "#999999"
		/// };
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Dictionary<string, object> ThemeVariables
		{
			get => _themeVariables;
		}
		Dictionary<string, object> _themeVariables = new Dictionary<string, object>();

		/// <summary>
		/// Security level (maps to Mermaid's <c>securityLevel</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.SecurityLevel = Wisej.Web.Ext.Mermaid.MermaidSecurityLevel.Strict;
		/// ]]></code>
		/// </example>
		[DefaultValue(MermaidSecurityLevel.Strict)]
		public MermaidSecurityLevel SecurityLevel
		{
			get => _securityLevel;
			set
			{
				if (_securityLevel != value)
				{
					_securityLevel = value;
					_owner.Update();
				}
			}
		}
		MermaidSecurityLevel _securityLevel = MermaidSecurityLevel.Strict;

		/// <summary>
		/// Log level (maps to Mermaid's <c>logLevel</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.LogLevel = Wisej.Web.Ext.Mermaid.MermaidLogLevel.Info;
		/// ]]></code>
		/// </example>
		[DefaultValue(MermaidLogLevel.Trace)]
		public MermaidLogLevel LogLevel
		{
			get => _logLevel;
			set
			{
				if (_logLevel != value)
				{
					_logLevel = value;
					_owner.Update();
				}
			}
		}
		MermaidLogLevel _logLevel = MermaidLogLevel.Trace;

		/// <summary>
		/// When set, Mermaid generates deterministic IDs for rendered elements.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.DeterministicIds = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(true)]
		public bool DeterministicIds
		{
			get => _deterministicIds;
			set
			{
				if (_deterministicIds != value)
				{
					_deterministicIds = value;
					_owner.Update();
				}
			}
		}
		bool _deterministicIds = true;

		/// <summary>
		/// Maximum text size allowed in diagrams (maps to Mermaid's <c>maxTextSize</c>).
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// // Increase the maximum allowed size when working with very large diagrams.
		/// mermaid.Config.MaxTextSize = 250_000;
		/// ]]></code>
		/// </example>
		[DefaultValue(32000)]
		public int MaxTextSize
		{
			get => _maxTextSize;
			set
			{
				if (_maxTextSize != value)
				{
					_maxTextSize = value;
					_owner.Update();
				}
			}
		}
		int _maxTextSize = 32000;

		/// <summary>
		/// Font family applied to the diagram text.
		/// </summary>
		/// <remarks>
		/// This maps to Mermaid's <c>fontFamily</c> option. For size, use <see cref="ThemeVariables"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.FontFamily = "\"Inter\", \"Segoe UI\", sans-serif";
		/// ]]></code>
		/// </example>
		public string FontFamily
		{
			get => _fontFamily ?? _owner.Font.Name;
			set
			{
				if (_fontFamily != value)
				{
					_fontFamily = value;
					_owner.Update();
				}
			}
		}
		string _fontFamily = null;

		private bool ShouldSerializeFontFamily() 
			=> _fontFamily != null;

		private void ResetFontFamily() 
			=> FontFamily = null;

		/// <summary>
		/// Flowchart-specific options.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.Flowchart.HtmlLabels = true;
		/// ]]></code>
		/// </example>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MermaidFlowchartConfiguration Flowchart
		{
			get => _flowchart ??= new MermaidFlowchartConfiguration(_owner);
		}
		MermaidFlowchartConfiguration _flowchart;

		/// <summary>
		/// Sequence diagram specific options.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.Sequence.ShowSequenceNumbers = true;
		/// ]]></code>
		/// </example>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MermaidSequenceConfiguration Sequence
		{
			get => _sequence ??= new MermaidSequenceConfiguration(_owner);
		}
		MermaidSequenceConfiguration _sequence;
	}
}
