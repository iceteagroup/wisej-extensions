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

using System.ComponentModel;

namespace Wisej.Web.Ext.Mermaid
{
	/// <summary>
	/// Flowchart configuration options (maps to Mermaid's <c>flowchart</c> option).
	/// </summary>
	/// <example>
	/// <code><![CDATA[
	/// mermaid.Config.Flowchart.HtmlLabels = true;
	/// ]]></code>
	/// </example>
	public class MermaidFlowchartConfiguration
	{
		private Mermaid _owner;

		internal MermaidFlowchartConfiguration(Mermaid owner)
		{
			_owner = owner;
		}

		/// <summary>
		/// When set, constrains rendered diagrams to max-width.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.Flowchart.UseMaxWidth = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		public bool UseMaxWidth
		{
			get => _useMaxWidth;
			set
			{
				if (_useMaxWidth != value)
				{
					_useMaxWidth = value;
					_owner.Update();
				}
			}
		}
		bool _useMaxWidth;

		/// <summary>
		/// When set, enables HTML labels in flowcharts.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.Flowchart.HtmlLabels = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		public bool HtmlLabels
		{
			get => _htmlLabels;
			set
			{
				if (_htmlLabels != value)
				{
					_htmlLabels = value;
					_owner.Update();
				}
			}
		}
		bool _htmlLabels;
	}
}
