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
	/// Sequence diagram configuration options (maps to Mermaid's <c>sequence</c> option).
	/// </summary>
	/// <example>
	/// <code><![CDATA[
	/// mermaid.Config.Sequence = new Wisej.Web.Ext.Mermaid.MermaidSequenceConfiguration
	/// {
	///     ShowSequenceNumbers = true
	/// };
	/// mermaid.ApplyOptions();
	/// ]]></code>
	/// </example>
	public class MermaidSequenceConfiguration
	{
		private Mermaid _owner;

		internal MermaidSequenceConfiguration(Mermaid owner)
		{
			_owner = owner;
		}

		/// <summary>
		/// When set, shows sequence numbers.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// mermaid.Config.Sequence ??= new Wisej.Web.Ext.Mermaid.MermaidSequenceConfiguration();
		/// mermaid.Config.Sequence.ShowSequenceNumbers = true;
		/// ]]></code>
		/// </example>
		[DefaultValue(false)]
		public bool ShowSequenceNumbers
		{
			get => _showSequenceNumbers;
			set
			{
				if (_showSequenceNumbers != value)
				{
					_showSequenceNumbers = value;
					_owner.Update();
				}
			}
		}
		bool _showSequenceNumbers;
	}
}
