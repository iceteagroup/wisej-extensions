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
	/// Represents the sequence diagram configuration options (maps to Mermaid's <c>sequence</c> option).
	/// </summary>
	/// <remarks>
	/// <para>
	/// Instances of this class are created internally and are bound to a <see cref="Mermaid"/> widget: changing a
	/// property updates the owner widget. The <see cref="Mermaid"/> widget exposes the same settings directly
	/// (see <see cref="Mermaid.Sequence"/>).
	/// </para>
	/// </remarks>
	public class MermaidSequenceConfiguration
	{
		private Mermaid _owner;

		internal MermaidSequenceConfiguration(Mermaid owner)
		{
			_owner = owner;
		}

		/// <summary>
		/// Returns or sets whether the messages of the sequence diagram are numbered (maps to <c>showSequenceNumbers</c>).
		/// </summary>
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
