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
	/// Represents the flowchart configuration options (maps to Mermaid's <c>flowchart</c> option).
	/// </summary>
	/// <remarks>
	/// <para>
	/// Instances of this class are created internally and are bound to a <see cref="Mermaid"/> widget: changing a
	/// property updates the owner widget. The <see cref="Mermaid"/> widget exposes the same settings directly
	/// (see <see cref="Mermaid.Flowchart"/>).
	/// </para>
	/// </remarks>
	public class MermaidFlowchartConfiguration
	{
		private Mermaid _owner;

		internal MermaidFlowchartConfiguration(Mermaid owner)
		{
			_owner = owner;
		}

		/// <summary>
		/// Returns or sets whether the rendered flowchart is scaled to fit the available width (maps to <c>useMaxWidth</c>).
		/// </summary>
		/// <remarks>
		/// When true, the SVG uses the available width as its maximum width and is scaled down accordingly;
		/// when false (default), it's rendered at its natural size.
		/// </remarks>
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
		/// Returns or sets whether the flowchart labels are rendered as HTML instead of SVG text (maps to <c>htmlLabels</c>).
		/// </summary>
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
