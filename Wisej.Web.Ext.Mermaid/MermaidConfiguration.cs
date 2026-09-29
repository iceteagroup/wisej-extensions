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
	/// Represents the Mermaid initialization options passed to <c>mermaid.initialize(...)</c>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Instances of this class are created internally and are bound to a <see cref="Mermaid"/> widget: changing a
	/// property updates the owner widget. The <see cref="Mermaid"/> widget exposes the same settings directly
	/// through <see cref="Mermaid.Look"/>, <see cref="Mermaid.Theme"/>, <see cref="Mermaid.SecurityLevel"/>,
	/// <see cref="Mermaid.LogLevel"/>, <see cref="Mermaid.DeterministicIds"/>, <see cref="Mermaid.FontFamily"/>,
	/// <see cref="Mermaid.ThemeVariables"/> and <see cref="Mermaid.Options"/>.
	/// </para>
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
		/// Returns or sets the diagram look (e.g. <c>classic</c>, <c>neo</c>, <c>handDrawn</c>).
		/// </summary>
		/// <remarks>
		/// This maps to Mermaid's <c>look</c> initialization option.
		/// </remarks>
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
		/// Returns or sets the theme name (e.g. <c>default</c>, <c>dark</c>, <c>forest</c>, <c>neutral</c>, <c>base</c>).
		/// </summary>
		/// <remarks>
		/// This maps to Mermaid's <c>theme</c> initialization option. The default is <c>default</c>.
		/// </remarks>
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
		/// Returns the theme variables (maps to Mermaid's <c>themeVariables</c>).
		/// </summary>
		/// <remarks>
		/// Common keys include <c>fontFamily</c>, <c>fontSize</c>, <c>primaryColor</c> and <c>lineColor</c>. The property is
		/// read-only; add or change entries on the returned instance. Changing the entries doesn't update the owner widget
		/// automatically. Mermaid applies the variables only with the <c>base</c> theme.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Dictionary<string, object> ThemeVariables
		{
			get => _themeVariables;
		}
		Dictionary<string, object> _themeVariables = new Dictionary<string, object>();

		/// <summary>
		/// Returns or sets the security level (maps to Mermaid's <c>securityLevel</c>).
		/// </summary>
		/// <remarks>
		/// The default is <see cref="MermaidSecurityLevel.Strict"/>.
		/// </remarks>
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
		/// Returns or sets the level of the messages logged to the browser console (maps to Mermaid's <c>logLevel</c>).
		/// </summary>
		/// <remarks>
		/// The default is <see cref="MermaidLogLevel.Trace"/>, the most verbose level.
		/// </remarks>
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
		/// Returns or sets whether Mermaid generates deterministic IDs for the rendered elements.
		/// </summary>
		/// <remarks>
		/// Maps to Mermaid's <c>deterministicIds</c> option. The default is true.
		/// </remarks>
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
		/// Returns or sets the maximum number of characters allowed in the diagram source (maps to Mermaid's <c>maxTextSize</c>).
		/// </summary>
		/// <remarks>
		/// The default is 32000. Diagrams with a longer source are not rendered by Mermaid.
		/// </remarks>
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
		/// Returns or sets the font family applied to the diagram text.
		/// </summary>
		/// <remarks>
		/// This maps to Mermaid's <c>fontFamily</c> option and accepts a CSS font-family list. When not set, it returns
		/// the name of the owner widget's <see cref="Control.Font"/>. For the size, use the <c>fontSize</c> key in <see cref="ThemeVariables"/>.
		/// </remarks>
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
		/// Returns the flowchart-specific options (maps to Mermaid's <c>flowchart</c> option).
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MermaidFlowchartConfiguration Flowchart
		{
			get => _flowchart ??= new MermaidFlowchartConfiguration(_owner);
		}
		MermaidFlowchartConfiguration _flowchart;

		/// <summary>
		/// Returns the sequence diagram specific options (maps to Mermaid's <c>sequence</c> option).
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MermaidSequenceConfiguration Sequence
		{
			get => _sequence ??= new MermaidSequenceConfiguration(_owner);
		}
		MermaidSequenceConfiguration _sequence;
	}
}
